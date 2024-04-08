using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkcdStaticTreeDynamicStoragehkcdStaticTreeCodecRaw : IHavokObject, IEquatable<hkcdStaticTreeDynamicStoragehkcdStaticTreeCodecRaw?>
{
    public virtual uint Signature => 0;
    public List<hkcdStaticTreeCodecRaw> _nodes;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _nodes = des.ReadClassArray<hkcdStaticTreeCodecRaw>(br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteClassArray<hkcdStaticTreeCodecRaw>(bw, _nodes);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClassArray<hkcdStaticTreeCodecRaw>(xe, nameof(_nodes), _nodes);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkcdStaticTreeDynamicStoragehkcdStaticTreeCodecRaw);
    }
    public bool Equals(hkcdStaticTreeDynamicStoragehkcdStaticTreeCodecRaw? other)
    {
        return other is not null && _nodes.Equals(other._nodes) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_nodes);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
