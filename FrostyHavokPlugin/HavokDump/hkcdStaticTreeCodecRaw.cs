using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkcdStaticTreeCodecRaw : IHavokObject, IEquatable<hkcdStaticTreeCodecRaw?>
{
    public virtual uint Signature => 0;
    public hkAabb _aabb;
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
        return Equals(obj as hkcdStaticTreeCodecRaw);
    }
    public bool Equals(hkcdStaticTreeCodecRaw? other)
    {
        return other is not null && _aabb.Equals(other._aabb) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_aabb);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
