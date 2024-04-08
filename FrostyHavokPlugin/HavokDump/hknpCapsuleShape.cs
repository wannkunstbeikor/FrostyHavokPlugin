using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpCapsuleShape : hknpConvexPolytopeShape, IEquatable<hknpCapsuleShape?>
{
    public override uint Signature => 0;
    public Vector4 _a;
    public Vector4 _b;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _a = des.ReadVector4(br);
        _b = des.ReadVector4(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteVector4(bw, _a);
        s.WriteVector4(bw, _b);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteVector4(xe, nameof(_a), _a);
        xs.WriteVector4(xe, nameof(_b), _b);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpCapsuleShape);
    }
    public bool Equals(hknpCapsuleShape? other)
    {
        return other is not null && _a.Equals(other._a) && _b.Equals(other._b) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_a);
        code.Add(_b);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
