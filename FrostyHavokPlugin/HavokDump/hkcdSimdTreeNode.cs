using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkcdSimdTreeNode : hkcdFourAabb
{
    public override uint Signature => 0;
    public uint[] _data = new uint[4];
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _data = des.ReadUInt32CStyleArray(br, 4);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteUInt32CStyleArray(bw, _data);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteNumberArray(xe, nameof(_data), _data);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkcdSimdTreeNode other && base.Equals(other) && _data == other._data && Signature == other.Signature;
    }
    public static bool operator ==(hkcdSimdTreeNode? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkcdSimdTreeNode? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_data);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
