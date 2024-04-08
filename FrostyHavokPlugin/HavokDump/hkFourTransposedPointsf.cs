using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkFourTransposedPointsf : IHavokObject, IEquatable<hkFourTransposedPointsf?>
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
        return Equals(obj as hkFourTransposedPointsf);
    }
    public bool Equals(hkFourTransposedPointsf? other)
    {
        return other is not null && _vertices.Equals(other._vertices) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_vertices);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
