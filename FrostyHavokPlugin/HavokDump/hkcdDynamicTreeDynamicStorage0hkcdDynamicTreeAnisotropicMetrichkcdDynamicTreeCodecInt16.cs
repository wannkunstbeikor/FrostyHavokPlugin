using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkcdDynamicTreeDynamicStorage0hkcdDynamicTreeAnisotropicMetrichkcdDynamicTreeCodecInt16 : hkcdDynamicTreeAnisotropicMetric, IEquatable<hkcdDynamicTreeDynamicStorage0hkcdDynamicTreeAnisotropicMetrichkcdDynamicTreeCodecInt16?>
{
    public override uint Signature => 0;
    public List<hkcdDynamicTreeCodecInt16> _nodes;
    public uint _firstFree;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _nodes = des.ReadClassArray<hkcdDynamicTreeCodecInt16>(br);
        _firstFree = br.ReadUInt32();
        br.Position += 3; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteClassArray<hkcdDynamicTreeCodecInt16>(bw, _nodes);
        bw.WriteUInt32(_firstFree);
        for (int i = 0; i < 3; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClassArray<hkcdDynamicTreeCodecInt16>(xe, nameof(_nodes), _nodes);
        xs.WriteNumber(xe, nameof(_firstFree), _firstFree);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkcdDynamicTreeDynamicStorage0hkcdDynamicTreeAnisotropicMetrichkcdDynamicTreeCodecInt16);
    }
    public bool Equals(hkcdDynamicTreeDynamicStorage0hkcdDynamicTreeAnisotropicMetrichkcdDynamicTreeCodecInt16? other)
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
