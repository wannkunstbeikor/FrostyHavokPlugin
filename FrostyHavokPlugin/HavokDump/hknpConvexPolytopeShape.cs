using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpConvexPolytopeShape : hknpConvexShape, IEquatable<hknpConvexPolytopeShape?>
{
    public override uint Signature => 0;
    public List<Vector4> _planes;
    public List<hknpConvexPolytopeShapeFace> _faces;
    public List<byte> _indices;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _planes = des.ReadVector4RelArray(br);
        _faces = des.ReadClassRelArray<hknpConvexPolytopeShapeFace>(br);
        _indices = des.ReadByteRelArray(br);
        br.Position += 4; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteVector4RelArray(bw, _planes);
        s.WriteClassRelArray<hknpConvexPolytopeShapeFace>(bw, _faces);
        s.WriteByteRelArray(bw, _indices);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteVector4Array(xe, nameof(_planes), _planes);
        xs.WriteClassArray<hknpConvexPolytopeShapeFace>(xe, nameof(_faces), _faces);
        xs.WriteNumberArray(xe, nameof(_indices), _indices);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpConvexPolytopeShape);
    }
    public bool Equals(hknpConvexPolytopeShape? other)
    {
        return other is not null && _planes.Equals(other._planes) && _faces.Equals(other._faces) && _indices.Equals(other._indices) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_planes);
        code.Add(_faces);
        code.Add(_indices);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
