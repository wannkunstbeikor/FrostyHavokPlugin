using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpConvexShape : hknpShape, IEquatable<hknpConvexShape?>
{
    public override uint Signature => 0;
    public List<Vector4> _vertices;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _vertices = des.ReadVector4RelArray(br);
        br.Position += 12; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteVector4RelArray(bw, _vertices);
        for (int i = 0; i < 12; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteVector4Array(xe, nameof(_vertices), _vertices);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpConvexShape);
    }
    public bool Equals(hknpConvexShape? other)
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
