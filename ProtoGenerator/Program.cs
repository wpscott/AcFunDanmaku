using System.Text.Json;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;

HashSet<string> BuiltInTypes = new()
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

foreach (var name in args)
{
    var file = File.OpenRead(name);
    var json = await JsonDocument.ParseAsync(file);
    Process(json.RootElement, ImmutableArray<string>.Empty);
}

void Process(in JsonElement json, in ImmutableArray<string> args)
{
    foreach (var obj in json.EnumerateObject())
    {
        var newArgs = args;
        if (obj.Name != "Kuaishou")
        {
            newArgs = args.Add(obj.Name);
        }

        if (!obj.Value.TryGetProperty("nested", out var nested)) continue;
        if (obj.Value.TryGetProperty("options", out _))
        {
            Generate(nested, newArgs);
        }
        else
        {
            Process(nested, newArgs);
        }
    }
}

const string Header = "syntax = \"proto3\";\r\n\r\n";

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
        builder.Append(Header);
        builder.Append($"package {package};\r\n\r\n");
        var content = ProcessPackage(message, imports);
        if (imports.Count > 0)
        {
            foreach (var import in imports.OrderBy(import => import))
            {
                builder.Append(import);
                builder.Append("\r\n");
            }

            builder.Append("\r\n");
        }

        builder.Append(content);
        builder.Append("\r\n");

        writer.Write(builder.ToString());
        Console.WriteLine(builder.ToString());
        Console.WriteLine("================");
    }
}

string ProcessPackage(in JsonProperty message, in SortedSet<string> imports, in int indentation = 0)
{
    var import = @$"import ""{message.Name}.proto"";";
    if (imports.Contains(import))
    {
        imports.Remove(import);
    }

    var builder = new StringBuilder();
    if (message.Value.TryGetProperty("values", out var values))
    {
        ProcessEnum(builder, indentation, message, values);
    }
    else if (message.Value.TryGetProperty("oneofs", out var oneofs))
    {
        ProcessOneof(builder, imports, indentation, message, oneofs);
    }
    else if (message.Value.TryGetProperty("fields", out var fields))
    {
        ProcessMessage(builder, imports, indentation, message, fields);
    }

    return builder.ToString();
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
void ProcessEnum(StringBuilder builder, in int indentation, in JsonProperty message, in JsonElement values)
{
    Indent(builder, indentation);
    builder.Append($"enum {message.Name} {{\r\n");

    if (message.Value.TryGetProperty("options", out var options))
    {
        foreach (var option in options.EnumerateObject())
        {
            Indent(builder, indentation + 1);
            builder.Append($"option {option.Name} = {option.Value.ToString().ToLower()};\r\n");
        }
    }

    foreach (var value in values.EnumerateObject())
    {
        Indent(builder, indentation + 1);

        builder.Append($"{value.Name} = {value.Value.GetDecimal()};\r\n");
    }

    Indent(builder, indentation);
    builder.Append("}");
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
void ProcessOneof(StringBuilder builder, in SortedSet<string> imports, in int indentation, in JsonProperty message,
    in JsonElement oneofs)
{
    var fields = message.Value.GetProperty("fields");

    Indent(builder, indentation);
    builder.Append($"message {message.Name} {{\r\n");
    var used = new HashSet<string>();
    var hasFields = false;
    foreach (var oneof in oneofs.EnumerateObject())
    {
        Indent(builder, indentation + 1);
        builder.Append($"oneof {oneof.Name} {{\r\n");

        foreach (var value in oneof.Value.GetProperty("oneof").EnumerateArray())
        {
            Indent(builder, indentation + 2);
            var field = fields.GetProperty(value.ToString());
            var type = CheckImports(imports, field);
            var id = field.GetProperty("id").GetInt32();

            builder.Append($"{type} {value} = {id};\r\n");
            used.Add(value.ToString());
        }

        Indent(builder, indentation + 1);
        builder.Append("}\r\n");

        foreach (var field in fields.EnumerateObject().Where(field => !used.Contains(field.Name)))
        {
            var type = CheckImports(imports, field.Value);

            var id = field.Value.GetProperty("id").GetInt32();

            hasFields = true;
            builder.Append("\r\n");
            Indent(builder, indentation + 1);
            builder.Append($"{type} {field.Name} = {id};");
        }
    }

    if (hasFields)
    {
        builder.Append("\r\n");
    }

    Indent(builder, indentation);
    builder.Append("}");
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
void ProcessMessage(StringBuilder builder, in SortedSet<string> imports, in int indentation, in JsonProperty message,
    in JsonElement fields)
{
    Indent(builder, indentation);
    builder.Append($"message {message.Name} {{");
    foreach (var field in fields.EnumerateObject())
    {
        builder.Append("\r\n");
        Indent(builder, indentation + 1);
        var type = CheckImports(imports, field.Value);
        var id = field.Value.GetProperty("id").GetInt32();
        if (field.Value.TryGetProperty("keyType", out var keyType))
        {
            builder.Append($"map<{keyType},{type}> {field.Name} = {id};");
        }
        else
        {
            if (field.Value.TryGetProperty("rule", out var rule))
            {
                builder.Append($"{rule} ");
            }

            builder.Append($"{type} {field.Name} = {id};");
        }
    }

    builder.Append("\r\n");

    if (message.Value.TryGetProperty("nested", out var nested))
    {
        builder.Append("\r\n");
        foreach (var nestedMessage in nested.EnumerateObject())
        {
            builder.Append(ProcessPackage(nestedMessage, imports, indentation + 1));
            builder.Append("\r\n");
        }
    }

    Indent(builder, indentation);
    builder.Append("}");
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
void Indent(in StringBuilder builder, in int indentation)
{
    for (var i = 0; i < indentation; i++)
    {
        builder.Append('\t');
    }
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
string CheckImports(in SortedSet<string> imports, in JsonElement field)
{
    var type = field.GetProperty("type").ToString().Replace("Kuaishou.", string.Empty);
    if (BuiltInTypes.Contains(type)) return type;
    var file = type.Replace("Im.Basic.", string.Empty).Replace("Im.Message.", string.Empty)
        .Replace("Im.Cloud.Channel.", string.Empty).Replace("Im.Cloud.Config.", string.Empty)
        .Replace("Im.Cloud.Data.Update.", string.Empty).Replace("Im.Cloud.Message.", string.Empty)
        .Replace("Im.Cloud.Profile.", string.Empty).Replace("Im.Cloud.Search.", string.Empty)
        .Replace("Im.Cloud.SessionFolder.", string.Empty).Replace("Im.Cloud.SessionTag.", string.Empty)
        .Replace("Im.Cloud.Voice.Call.", string.Empty).Split('.').First();
    imports.Add(@$"import ""{file}.proto"";");

    return type;
}