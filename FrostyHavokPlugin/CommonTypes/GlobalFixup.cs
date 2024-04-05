using System;
using Frosty.Sdk.IO;
using FrostyHavokPlugin.Interfaces;

namespace FrostyHavokPlugin.CommonTypes;

public class GlobalFixup : IFixup
{
    public GlobalFixup()
    {
    }

    internal GlobalFixup(DataStream br)
    {
        Src = br.ReadUInt32();
        DstSectionIndex = br.ReadUInt32();
        Dst = br.ReadUInt32();
    }

    public uint Src { get; set; }
    public uint DstSectionIndex { get; set; }
    public uint Dst { get; set; }

    internal void Write(DataStream bw)
    {
        bw.WriteUInt32(Src);
        bw.WriteUInt32(DstSectionIndex);
        bw.WriteUInt32(Dst);
    }

    private bool Equals(GlobalFixup other)
    {
        return Src == other.Src && Dst == other.Dst;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((GlobalFixup)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Src, Dst);
    }
}