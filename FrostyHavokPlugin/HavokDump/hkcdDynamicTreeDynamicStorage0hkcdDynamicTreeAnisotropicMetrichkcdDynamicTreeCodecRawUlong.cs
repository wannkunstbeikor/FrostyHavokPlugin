using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkcdDynamicTreeDynamicStorage0hkcdDynamicTreeAnisotropicMetrichkcdDynamicTreeCodecRawUlong : hkcdDynamicTreeAnisotropicMetric, IEquatable<hkcdDynamicTreeDynamicStorage0hkcdDynamicTreeAnisotropicMetrichkcdDynamicTreeCodecRawUlong?>
{
    public override uint Signature => 0;
    public List<hkcdDynamicTreeCodecRawUlong> _nodes;
    public ulong _firstFree;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _nodes = des.ReadClassArray<hkcdDynamicTreeCodecRawUlong>(br);
        _firstFree = br.ReadUInt64();
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteClassArray<hkcdDynamicTreeCodecRawUlong>(bw, _nodes);
        bw.WriteUInt64(_firstFree);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClassArray<hkcdDynamicTreeCodecRawUlong>(xe, nameof(_nodes), _nodes);
        xs.WriteNumber(xe, nameof(_firstFree), _firstFree);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkcdDynamicTreeDynamicStorage0hkcdDynamicTreeAnisotropicMetrichkcdDynamicTreeCodecRawUlong);
    }
    public bool Equals(hkcdDynamicTreeDynamicStorage0hkcdDynamicTreeAnisotropicMetrichkcdDynamicTreeCodecRawUlong? other)
    {
        return other is not null && _nodes.Equals(other._nodes) && _firstFree.Equals(other._firstFree) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_nodes);
        code.Add(_firstFree);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
