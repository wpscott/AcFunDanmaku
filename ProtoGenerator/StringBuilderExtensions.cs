using System.Runtime.CompilerServices;
using System.Text;

namespace ProtoGenerator;

public static class StringBuilderExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static StringBuilder AddIndention(this StringBuilder builder, in int indentation)
    {
        for (var i = 0; i < indentation; i++) builder.Append('\t');
        return builder;
    }
}