using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpExternMeshShape : hknpCompositeShape
{
    public override uint Signature => 0;
    public hknpExternMeshShapeGeometry? _geometry;
    public hknpExternMeshShapeData? _boundingVolumeData;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _geometry = des.ReadClassPointer<hknpExternMeshShapeGeometry>(br);
        _boundingVolumeData = des.ReadClassPointer<hknpExternMeshShapeData>(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteClassPointer<hknpExternMeshShapeGeometry>(bw, _geometry);
        s.WriteClassPointer<hknpExternMeshShapeData>(bw, _boundingVolumeData);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClassPointer(xe, nameof(_geometry), _geometry);
        xs.WriteClassPointer(xe, nameof(_boundingVolumeData), _boundingVolumeData);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpExternMeshShape other && base.Equals(other) && _geometry == other._geometry && _boundingVolumeData == other._boundingVolumeData && Signature == other.Signature;
    }
    public static bool operator ==(hknpExternMeshShape? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpExternMeshShape? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_geometry);
        code.Add(_boundingVolumeData);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
