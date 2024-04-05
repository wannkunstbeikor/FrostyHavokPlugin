using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpDynamicCompoundShape : hknpCompoundShape, IEquatable<hknpDynamicCompoundShape?>
{
    public override uint Signature => 0;
    public hknpDynamicCompoundShapeData _boundingVolumeData;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _boundingVolumeData = des.ReadClassPointer<hknpDynamicCompoundShapeData>(br);
        br.Position += 8; // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClassPointer(xe, nameof(_boundingVolumeData), _boundingVolumeData);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpDynamicCompoundShape);
    }
    public bool Equals(hknpDynamicCompoundShape? other)
    {
        return other is not null && _boundingVolumeData.Equals(other._boundingVolumeData) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_boundingVolumeData);
        code.Add(Signature);
        return code.ToHashCode();
    }
}