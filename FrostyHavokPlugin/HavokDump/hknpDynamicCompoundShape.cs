using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpDynamicCompoundShape : hknpCompoundShape
{
    public override uint Signature => 0;
    public hknpDynamicCompoundShapeData? _boundingVolumeData;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _boundingVolumeData = des.ReadClassPointer<hknpDynamicCompoundShapeData>(br);
        br.Position += 8; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteClassPointer<hknpDynamicCompoundShapeData>(bw, _boundingVolumeData);
        for (int i = 0; i < 8; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClassPointer(xe, nameof(_boundingVolumeData), _boundingVolumeData);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpDynamicCompoundShape other && base.Equals(other) && _boundingVolumeData == other._boundingVolumeData && Signature == other.Signature;
    }
    public static bool operator ==(hknpDynamicCompoundShape? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpDynamicCompoundShape? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_boundingVolumeData);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
