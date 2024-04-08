using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkAabb : IHavokObject, IEquatable<hkAabb?>
{
    public virtual uint Signature => 0;
    public Vector4 _min;
    public Vector4 _max;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _min = des.ReadVector4(br);
        _max = des.ReadVector4(br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteVector4(bw, _min);
        s.WriteVector4(bw, _max);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteVector4(xe, nameof(_min), _min);
        xs.WriteVector4(xe, nameof(_max), _max);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkAabb);
    }
    public bool Equals(hkAabb? other)
    {
        return other is not null && _min.Equals(other._min) && _max.Equals(other._max) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_min);
        code.Add(_max);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
