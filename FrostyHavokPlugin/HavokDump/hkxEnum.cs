using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxEnum : hkReferencedObject
{
    public override uint Signature => 0;
    public List<hkxEnumItem?> _items = new();
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _items = des.ReadClassArray<hkxEnumItem>(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteClassArray<hkxEnumItem>(bw, _items);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClassArray<hkxEnumItem>(xe, nameof(_items), _items);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkxEnum other && base.Equals(other) && _items.SequenceEqual(other._items) && Signature == other.Signature;
    }
    public static bool operator ==(hkxEnum? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkxEnum? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_items);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
