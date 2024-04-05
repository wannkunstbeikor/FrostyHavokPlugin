using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkcdDynamicTreeCodecRawunsignedint : IHavokObject, IEquatable<hkcdDynamicTreeCodecRawunsignedint?>
{
    public virtual uint Signature => 0;
    public hkAabb _aabb;
    public uint _parent;
    public uint[] _children = new uint[2];
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _aabb = new hkAabb();
        _aabb.Read(des, br);
        _parent = br.ReadUInt32();
        _children = des.ReadUInt32CStyleArray(br, 2);
        br.Position += 4; // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClass(xe, nameof(_aabb), _aabb);
        xs.WriteNumber(xe, nameof(_parent), _parent);
        xs.WriteNumberArray(xe, nameof(_children), _children);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkcdDynamicTreeCodecRawunsignedint);
    }
    public bool Equals(hkcdDynamicTreeCodecRawunsignedint? other)
    {
        return other is not null && _aabb.Equals(other._aabb) && _parent.Equals(other._parent) && _children.Equals(other._children) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_aabb);
        code.Add(_parent);
        code.Add(_children);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
