using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpExternMeshShape : hknpCompositeShape, IEquatable<hknpExternMeshShape?>
{
    public override uint Signature => 0;
    public hknpExternMeshShapeGeometry _geometry;
    public hknpExternMeshShapeData _boundingVolumeData;
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
        return Equals(obj as hknpExternMeshShape);
    }
    public bool Equals(hknpExternMeshShape? other)
    {
        return other is not null && _geometry.Equals(other._geometry) && _boundingVolumeData.Equals(other._boundingVolumeData) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_geometry);
        code.Add(_boundingVolumeData);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
