using System;
using System.IO;
using System.Linq;
using Frosty.Sdk.IO;
using OpenTK.Mathematics;

namespace FrostyHavokPlugin.Utils;

public static class DataStreamExtensions
{
    public static T AssertValue<T>(this DataStream stream, T value, string typeName, string valueFormat, T[] options) where T : IEquatable<T>
    {
        foreach (var option in options)
            if (value.Equals(option))
                return value;

        var strValue = string.Format(valueFormat, value);
        var strOptions = string.Join(", ", options.Select(o => string.Format(valueFormat, o)));
        throw new InvalidDataException(
            $"Read {typeName}: {strValue} | Expected: {strOptions} | Ending position: 0x{stream.Position:X}");
    }

    public static uint AssertUInt32(this DataStream stream, params uint[] options)
    {
        return stream.AssertValue(stream.ReadUInt32(), "UInt32", "0x{0:X}", options);
    }

    public static int AssertInt32(this DataStream stream, params int[] options)
    {
        return stream.AssertValue(stream.ReadInt32(), "Int32", "0x{0:X}", options);
    }

    public static byte AssertByte(this DataStream stream, params byte[] options)
    {
        return stream.AssertValue(stream.ReadByte(), "Byte", "0x{0:X}", options);
    }

    public static Vector4 ReadVector4(this DataStream stream)
    {
        var x = stream.ReadSingle();
        var y = stream.ReadSingle();
        var z = stream.ReadSingle();
        var w = stream.ReadSingle();
        return new Vector4(x, y, z, w);
    }
}