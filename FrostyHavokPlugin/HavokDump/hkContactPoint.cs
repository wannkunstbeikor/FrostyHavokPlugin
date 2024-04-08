using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkContactPoint : IHavokObject, IEquatable<hkContactPoint?>
{
    public virtual uint Signature => 0;
    public Vector4 _position;
    public Vector4 _separatingNormal;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _position = des.ReadVector4(br);
        _separatingNormal = des.ReadVector4(br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteVector4(bw, _position);
        s.WriteVector4(bw, _separatingNormal);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteVector4(xe, nameof(_position), _position);
        xs.WriteVector4(xe, nameof(_separatingNormal), _separatingNormal);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkContactPoint);
    }
    public bool Equals(hkContactPoint? other)
    {
        return other is not null && _position.Equals(other._position) && _separatingNormal.Equals(other._separatingNormal) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_position);
        code.Add(_separatingNormal);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
