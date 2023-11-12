using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using ProtoGenerator;

const string header = "syntax = \"proto3\";";

HashSet<string> builtInTypes = new()
{
    "double",
    "float",
    "int32",
    "int64",
    "uint32",
    "uint64",
    "sint32",
    "sint64",
    "fixed32",
    "fixed64",
    "sfixed32",
    "sfixed64",
    "bool",
    "string",
    "bytes"
};

var importsToRemove = new[]
{
    "Im.Basic.",
    "Im.Message.",
    "Im.Cloud.Channel.",
    "Im.Cloud.Config.",
    "Im.Cloud.Data.Update.",
    "Im.Cloud.Message.",
    "Im.Cloud.Profile.",
    "Im.Cloud.Search.",
    "Im.Cloud.SessionFolder.",
    "Im.Cloud.SessionTag.",
    "Im.Cloud.Voice.Call.",
};

foreach (var name in args)
{
    var file = File.OpenRead(name);
    var json = await JsonDocument.ParseAsync(file);
    Process(json.RootElement, ImmutableArray<string>.Empty);
}

return;

void Process(in JsonElement json, in ImmutableArray<string> args)
{
    foreach (var obj in json.EnumerateObject())
    {
        var newArgs = args;
        if (obj.Name != "Kuaishou") newArgs = args.Add(obj.Name);

        if (!obj.Value.TryGetProperty("nested", out var nested)) continue;
        if (obj.Value.TryGetProperty("options", out _))
            Generate(nested, newArgs);
        else
            Process(nested, newArgs);
    }
}

void Generate(in JsonElement json, in ImmutableArray<string> args)
{
    var path = "../../../../AcFunDanmu/protos/" + string.Join('/', args);
    Directory.CreateDirectory(path);
    foreach (var message in json.EnumerateObject())
    {
        Console.WriteLine("================");
        var filepath = path + $"/{message.Name}.proto";
        using var file = File.Open(filepath, FileMode.Create, FileAccess.Write, FileShare.Read);
        var package = "AcFunDanmu." + string.Join('.', args);
        using var writer = new StreamWriter(file);

        var builder = new StringBuilder();
        var imports = new SortedSet<string>();
        builder
            .Append(header)
            .AppendLine()
            .AppendLine()
            .Append($"package {package};")
            .AppendLine()
            .AppendLine();
        var content = ProcessPackage(message, imports);
        if (imports.Count > 0)
        {
            foreach (var import in imports.OrderBy(import => import))
            {
                builder
                    .Append(import)
                    .AppendLine();
            }

            builder.AppendLine();
        }

        builder
            .Append(content)
            .AppendLine();

        writer.Write(builder.ToString());
        Console.WriteLine(builder.ToString());
        Console.WriteLine("================");
    }
}

string ProcessPackage(in JsonProperty message, in SortedSet<string> imports, in int indentation = 0)
{
    var import = @$"import ""{message.Name}.proto"";";
    if (imports.Contains(import)) imports.Remove(import);

    var builder = new StringBuilder();
    if (message.Value.TryGetProperty("values", out var values))
        ProcessEnum(builder, indentation, message, values);
    else if (message.Value.TryGetProperty("oneofs", out var oneofs))
        ProcessOneof(builder, imports, indentation, message, oneofs);
    else if (message.Value.TryGetProperty("fields", out var fields))
        ProcessMessage(builder, imports, indentation, message, fields);

    return builder.ToString();
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
void ProcessEnum(StringBuilder builder, in int indentation, in JsonProperty message, in JsonElement values)
{
    builder
        .AddIndention(indentation)
        .Append($"enum {message.Name} {{")
        .AppendLine();

    if (message.Value.TryGetProperty("options", out var options))
        foreach (var option in options.EnumerateObject())
        {
            builder
                .AddIndention(indentation + 1)
                .Append($"option {option.Name} = {option.Value.ToString().ToLower()};")
                .AppendLine();
        }

    foreach (var value in values.EnumerateObject())
    {
        builder
            .AddIndention(indentation + 1)
            .Append($"{value.Name} = {value.Value.GetDecimal()};")
            .AppendLine();
    }

    builder
        .AddIndention(indentation)
        .Append('}');
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
void ProcessOneof(StringBuilder builder, in SortedSet<string> imports, in int indentation, in JsonProperty message,
    in JsonElement oneofs)
{
    var fields = message.Value.GetProperty("fields");

    builder
        .AddIndention(indentation)
        .Append($"message {message.Name} {{")
        .AppendLine();
    var used = new HashSet<string>();
    var hasFields = false;
    foreach (var oneof in oneofs.EnumerateObject())
    {
        builder
            .AddIndention(indentation + 1)
            .Append($"oneof {oneof.Name} {{")
            .AppendLine();

        foreach (var value in oneof.Value.GetProperty("oneof").EnumerateArray())
        {
            var field = fields.GetProperty(value.ToString());
            var type = CheckImports(imports, field);
            var id = field.GetProperty("id").GetInt32();

            builder
                .AddIndention(indentation + 2)
                .Append($"{type} {value} = {id};")
                .AppendLine();
            used.Add(value.ToString());
        }

        builder
            .AddIndention(indentation + 1)
            .Append('}')
            .AppendLine();

        foreach (var field in fields.EnumerateObject().Where(field => !used.Contains(field.Name)))
        {
            var type = CheckImports(imports, field.Value);

            var id = field.Value.GetProperty("id").GetInt32();

            hasFields = true;
            builder
                .AppendLine()
                .AddIndention(indentation + 1)
                .Append($"{type} {field.Name} = {id};");
        }
    }

    if (hasFields) builder.AppendLine();

    builder
        .AddIndention(indentation)
        .Append('}');
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
void ProcessMessage(StringBuilder builder, in SortedSet<string> imports, in int indentation, in JsonProperty message,
    in JsonElement fields)
{
    builder.AddIndention(indentation).Append($"message {message.Name} {{");
    foreach (var field in fields.EnumerateObject())
    {
        builder
            .AppendLine()
            .AddIndention(indentation + 1);
        var type = CheckImports(imports, field.Value);
        var id = field.Value.GetProperty("id").GetInt32();
        if (field.Value.TryGetProperty("keyType", out var keyType))
        {
            builder.Append($"map<{keyType},{type}> {field.Name} = {id};");
        }
        else
        {
            if (field.Value.TryGetProperty("rule", out var rule)) builder.Append($"{rule} ");

            builder.Append($"{type} {field.Name} = {id};");
        }
    }

    builder.AppendLine();

    if (message.Value.TryGetProperty("nested", out var nested))
    {
        builder.AppendLine();
        foreach (var nestedMessage in nested.EnumerateObject())
        {
            builder.Append(ProcessPackage(nestedMessage, imports, indentation + 1));
            builder.AppendLine();
        }
    }

    builder
        .AddIndention(indentation)
        .Append('}');
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
string CheckImports(in SortedSet<string> imports, in JsonElement field)
{
    var type = field.GetProperty("type").ToString().Replace("Kuaishou.", string.Empty);
    if (builtInTypes.Contains(type)) return type;

    var file = importsToRemove
        .Aggregate(type, (current, import) => current.Replace(import, string.Empty))
        .Split('.')
        .First();

    imports.Add(@$"import ""{file}.proto"";");

    return type;
}