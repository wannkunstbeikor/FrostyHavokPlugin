using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkGeometry : hkReferencedObject
{
    public override uint Signature => 0;
    public List<Vector4> _vertices = new();
    public List<hkGeometryTriangle?> _triangles = new();
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _vertices = des.ReadVector4Array(br);
        _triangles = des.ReadClassArray<hkGeometryTriangle>(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteVector4Array(bw, _vertices);
        s.WriteClassArray<hkGeometryTriangle>(bw, _triangles);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteVector4Array(xe, nameof(_vertices), _vertices);
        xs.WriteClassArray<hkGeometryTriangle>(xe, nameof(_triangles), _triangles);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkGeometry other && base.Equals(other) && _vertices.SequenceEqual(other._vertices) && _triangles.SequenceEqual(other._triangles) && Signature == other.Signature;
    }
    public static bool operator ==(hkGeometry? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkGeometry? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_vertices);
        code.Add(_triangles);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
