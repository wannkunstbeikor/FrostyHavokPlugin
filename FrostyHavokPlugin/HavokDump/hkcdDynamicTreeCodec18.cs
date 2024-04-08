using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkcdDynamicTreeCodec18 : IHavokObject, IEquatable<hkcdDynamicTreeCodec18?>
{
    public virtual uint Signature => 0;
    public hkAabbHalf _aabb;
    public ushort _parent;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _aabb = new hkAabbHalf();
        _aabb.Read(des, br);
        _parent = br.ReadUInt16();
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        _aabb.Write(s, bw);
        bw.WriteUInt16(_parent);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClass(xe, nameof(_aabb), _aabb);
        xs.WriteNumber(xe, nameof(_parent), _parent);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkcdDynamicTreeCodec18);
    }
    public bool Equals(hkcdDynamicTreeCodec18? other)
    {
        return other is not null && _aabb.Equals(other._aabb) && _parent.Equals(other._parent) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_aabb);
        code.Add(_parent);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
