using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkcdDynamicTreeCodecInt16 : IHavokObject, IEquatable<hkcdDynamicTreeCodecInt16?>
{
    public virtual uint Signature => 0;
    public hkcdDynamicTreeCodecInt16IntAabb _aabb;
    public uint _parent;
    public uint[] _children = new uint[2];
    public uint _pad;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _aabb = new hkcdDynamicTreeCodecInt16IntAabb();
        _aabb.Read(des, br);
        _parent = br.ReadUInt32();
        _children = des.ReadUInt32CStyleArray(br, 2);
        _pad = br.ReadUInt32();
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClass(xe, nameof(_aabb), _aabb);
        xs.WriteNumber(xe, nameof(_parent), _parent);
        xs.WriteNumberArray(xe, nameof(_children), _children);
        xs.WriteNumber(xe, nameof(_pad), _pad);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkcdDynamicTreeCodecInt16);
    }
    public bool Equals(hkcdDynamicTreeCodecInt16? other)
    {
        return other is not null && _aabb.Equals(other._aabb) && _parent.Equals(other._parent) && _children.Equals(other._children) && _pad.Equals(other._pad) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_aabb);
        code.Add(_parent);
        code.Add(_children);
        code.Add(_pad);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
