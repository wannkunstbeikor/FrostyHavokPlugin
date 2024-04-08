using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpStaticCompoundShape : hknpCompoundShape, IEquatable<hknpStaticCompoundShape?>
{
    public override uint Signature => 0;
    public hknpStaticCompoundShapeData _boundingVolumeData;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _boundingVolumeData = des.ReadClassPointer<hknpStaticCompoundShapeData>(br);
        br.Position += 8; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteClassPointer<hknpStaticCompoundShapeData>(bw, _boundingVolumeData);
        for (int i = 0; i < 8; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClassPointer(xe, nameof(_boundingVolumeData), _boundingVolumeData);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpStaticCompoundShape);
    }
    public bool Equals(hknpStaticCompoundShape? other)
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
