using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkGeometry : hkReferencedObject, IEquatable<hkGeometry?>
{
    public override uint Signature => 0;
    public List<Vector4> _vertices;
    public List<hkGeometryTriangle> _triangles;
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
        return Equals(obj as hkGeometry);
    }
    public bool Equals(hkGeometry? other)
    {
        return other is not null && _vertices.Equals(other._vertices) && _triangles.Equals(other._triangles) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_vertices);
        code.Add(_triangles);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
