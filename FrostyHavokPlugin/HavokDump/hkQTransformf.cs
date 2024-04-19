using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkQTransformf : IHavokObject
{
    public virtual uint Signature => 0;
    public Quaternion _rotation;
    public Vector4 _translation;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _rotation = des.ReadQuaternion(br);
        _translation = des.ReadVector4(br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteQuaternion(bw, _rotation);
        s.WriteVector4(bw, _translation);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteQuaternion(xe, nameof(_rotation), _rotation);
        xs.WriteVector4(xe, nameof(_translation), _translation);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkQTransformf other && _rotation == other._rotation && _translation == other._translation && Signature == other.Signature;
    }
    public static bool operator ==(hkQTransformf? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkQTransformf? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_rotation);
        code.Add(_translation);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
