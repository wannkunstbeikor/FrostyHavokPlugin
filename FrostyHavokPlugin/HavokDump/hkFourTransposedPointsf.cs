using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkFourTransposedPointsf : IHavokObject
{
    public virtual uint Signature => 0;
    public Vector4[] _vertices = new Vector4[3];
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _vertices = des.ReadVector4CStyleArray(br, 3);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteVector4CStyleArray(bw, _vertices);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteVector4Array(xe, nameof(_vertices), _vertices);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkFourTransposedPointsf other && _vertices == other._vertices && Signature == other.Signature;
    }
    public static bool operator ==(hkFourTransposedPointsf? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkFourTransposedPointsf? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_vertices);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
