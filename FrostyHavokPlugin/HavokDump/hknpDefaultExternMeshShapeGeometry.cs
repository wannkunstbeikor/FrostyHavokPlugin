using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpDefaultExternMeshShapeGeometry : hknpExternMeshShapeGeometry
{
    public override uint Signature => 0;
    public hkGeometry? _geometry;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _geometry = des.ReadClassPointer<hkGeometry>(br);
        br.Position += 8; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteClassPointer<hkGeometry>(bw, _geometry);
        for (int i = 0; i < 8; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClassPointer(xe, nameof(_geometry), _geometry);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpDefaultExternMeshShapeGeometry other && base.Equals(other) && _geometry == other._geometry && Signature == other.Signature;
    }
    public static bool operator ==(hknpDefaultExternMeshShapeGeometry? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpDefaultExternMeshShapeGeometry? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_geometry);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
