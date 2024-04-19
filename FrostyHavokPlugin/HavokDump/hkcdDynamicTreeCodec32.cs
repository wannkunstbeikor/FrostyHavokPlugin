using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkcdDynamicTreeCodec32 : IHavokObject
{
    public virtual uint Signature => 0;
    public hkAabb? _aabb;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _aabb = new hkAabb();
        _aabb.Read(des, br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        _aabb.Write(s, bw);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClass(xe, nameof(_aabb), _aabb);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkcdDynamicTreeCodec32 other && _aabb == other._aabb && Signature == other.Signature;
    }
    public static bool operator ==(hkcdDynamicTreeCodec32? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkcdDynamicTreeCodec32? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_aabb);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
