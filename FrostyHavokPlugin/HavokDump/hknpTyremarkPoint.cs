using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpTyremarkPoint : IHavokObject, IEquatable<hknpTyremarkPoint?>
{
    public virtual uint Signature => 0;
    public Vector4 _pointLeft;
    public Vector4 _pointRight;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _pointLeft = des.ReadVector4(br);
        _pointRight = des.ReadVector4(br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteVector4(bw, _pointLeft);
        s.WriteVector4(bw, _pointRight);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteVector4(xe, nameof(_pointLeft), _pointLeft);
        xs.WriteVector4(xe, nameof(_pointRight), _pointRight);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpTyremarkPoint);
    }
    public bool Equals(hknpTyremarkPoint? other)
    {
        return other is not null && _pointLeft.Equals(other._pointLeft) && _pointRight.Equals(other._pointRight) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_pointLeft);
        code.Add(_pointRight);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
