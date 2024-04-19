using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpConvexShape : hknpShape
{
    public override uint Signature => 0;
    public List<Vector4> _vertices = new();
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
        return obj is hknpConvexShape other && base.Equals(other) && _vertices.SequenceEqual(other._vertices) && Signature == other.Signature;
    }
    public static bool operator ==(hknpConvexShape? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpConvexShape? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_vertices);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
