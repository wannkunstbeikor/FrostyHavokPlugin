using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpBreakableConstraintData : hkpWrappedConstraintData, IEquatable<hknpBreakableConstraintData?>
{
    public override uint Signature => 0;
    public float _threshold;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _threshold = br.ReadSingle();
        br.Position += 4; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteSingle(_threshold);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteFloat(xe, nameof(_threshold), _threshold);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpBreakableConstraintData);
    }
    public bool Equals(hknpBreakableConstraintData? other)
    {
        return other is not null && _threshold.Equals(other._threshold) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_threshold);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
