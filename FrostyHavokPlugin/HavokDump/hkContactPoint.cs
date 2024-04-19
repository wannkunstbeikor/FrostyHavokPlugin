using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkContactPoint : IHavokObject
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
        return obj is hkContactPoint other && _position == other._position && _separatingNormal == other._separatingNormal && Signature == other.Signature;
    }
    public static bool operator ==(hkContactPoint? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkContactPoint? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_position);
        code.Add(_separatingNormal);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
