using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Frosty.Sdk.IO;
using OpenTK.Mathematics;

namespace FrostyHavokPlugin.Utils;

public static class DataStreamExtensions
{
    private static readonly Dictionary<string, long> reservations = new();

    public static T AssertValue<T>(this DataStream stream, T value, string typeName, string valueFormat, T[] options) where T : IEquatable<T>
    {
        foreach (T option in options)
        {
            if (value.Equals(option))
            {
                return value;
            }
        }

        string strValue = string.Format(valueFormat, value);
        string strOptions = string.Join(", ", options.Select(o => string.Format(valueFormat, o)));
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
        float x = stream.ReadSingle();
        float y = stream.ReadSingle();
        float z = stream.ReadSingle();
        float w = stream.ReadSingle();
        return new Vector4(x, y, z, w);
    }

    public static Vector3 ReadVector3(this DataStream stream)
    {
        float x = stream.ReadSingle();
        float y = stream.ReadSingle();
        float z = stream.ReadSingle();
        stream.Position += 4; // pad
        return new Vector3(x, y, z);
    }

    public static Box3 ReadAabb(this DataStream stream)
    {
        return new Box3(stream.ReadVector3(), stream.ReadVector3());
    }

    private static void Reserve(this DataStream stream, string inName, string inTypeName)
    {
        inName = $"{inName}:{inTypeName}";
        if (!reservations.TryAdd(inName, stream.Position))
        {
            throw new ArgumentException("Key already reserved: " + inName);
        }
    }

    public static void ReserveUInt32(this DataStream stream, string inName)
    {
        stream.Reserve(inName, "UInt32");
        stream.WriteUInt32(0xDEADBEEF, Endian.Big);
    }

    private static long Fill(string inName, string inTypeName)
    {
        inName = $"{inName}:{inTypeName}";
        if (!reservations.Remove(inName, out long jump))
        {
            throw new ArgumentException("Key is not reserved: " + inName);
        }

        return jump;
    }

    public static void FillUInt32(this DataStream stream, string inName, uint inValue)
    {
        stream.StepIn(Fill(inName, "UInt32"));
        stream.WriteUInt32(inValue);
        stream.StepOut();
    }

    public static void WriteVector4(this DataStream stream, Vector4 value)
    {
        stream.WriteSingle(value.X);
        stream.WriteSingle(value.Y);
        stream.WriteSingle(value.Z);
        stream.WriteSingle(value.W);
    }

    public static void WriteVector3(this DataStream stream, Vector3 value)
    {
        stream.WriteSingle(value.X);
        stream.WriteSingle(value.Y);
        stream.WriteSingle(value.Z);
        stream.WriteUInt32(0); // pad
    }

    public static void WriteAabb(this DataStream stream, Box3 value)
    {
        stream.WriteVector3(value.Min);
        stream.WriteVector3(value.Max);
    }
}