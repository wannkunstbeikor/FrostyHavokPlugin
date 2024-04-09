using System;
using System.Collections.Generic;
using Frosty.Sdk.IO;

namespace FrostyHavokPlugin.Utils;

public static class RelocPtrExtension
{
    private static Dictionary<string, long> m_mapping = new();
    private static HashSet<long> m_table = new();

    public static void WriteRelocPtr(this DataStream stream, string inName)
    {
        stream.Pad(4);
        if (!m_mapping.TryAdd(inName, stream.Position))
        {
            throw new ArgumentException("RelocPtr already reserved: " + inName);
        }

        m_table.Add(stream.Position);
        stream.WriteInt64(0);
    }

    public static void AddRelocData(this DataStream stream, string inName)
    {
        if (!m_mapping.Remove(inName, out long jump))
        {
            throw new ArgumentException("RelocPtr is not reserved: " + inName);
        }

        long value = stream.Position;

        stream.StepIn(jump);
        stream.WriteInt64(value);
        stream.StepOut();
    }

    public static void WriteRelocTable(this DataStream stream)
    {
        stream.Pad(4);
        foreach (long offset in m_table)
        {
            stream.WriteUInt32((uint)offset);
        }
        m_table.Clear();
    }

    public static void ReadRelocPtr(this DataStream stream, Action<DataStream> inReadFunc)
    {
        stream.Pad(4);
        stream.StepIn(stream.ReadInt64());
        inReadFunc(stream);
        stream.StepOut();
    }
}