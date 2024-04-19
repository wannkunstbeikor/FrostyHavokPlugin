using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkSphere : IHavokObject
{
    public virtual uint Signature => 0;
    public Vector4 _pos;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _pos = des.ReadVector4(br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteVector4(bw, _pos);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteVector4(xe, nameof(_pos), _pos);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkSphere other && _pos == other._pos && Signature == other.Signature;
    }
    public static bool operator ==(hkSphere? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkSphere? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_pos);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
