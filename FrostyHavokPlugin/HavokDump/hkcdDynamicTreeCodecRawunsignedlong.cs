using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkcdDynamicTreeCodecRawunsignedlong : IHavokObject
{
    public virtual uint Signature => 0;
    public hkAabb? _aabb;
    public ulong _parent;
    public ulong[] _children = new ulong[2];
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _aabb = new hkAabb();
        _aabb.Read(des, br);
        _parent = br.ReadUInt64();
        _children = des.ReadUInt64CStyleArray(br, 2);
        br.Position += 8; // padding
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        _aabb.Write(s, bw);
        bw.WriteUInt64(_parent);
        s.WriteUInt64CStyleArray(bw, _children);
        for (int i = 0; i < 8; i++) bw.WriteByte(0); // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClass(xe, nameof(_aabb), _aabb);
        xs.WriteNumber(xe, nameof(_parent), _parent);
        xs.WriteNumberArray(xe, nameof(_children), _children);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkcdDynamicTreeCodecRawunsignedlong other && _aabb == other._aabb && _parent == other._parent && _children == other._children && Signature == other.Signature;
    }
    public static bool operator ==(hkcdDynamicTreeCodecRawunsignedlong? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkcdDynamicTreeCodecRawunsignedlong? a, object? b) => !(a == b);
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
