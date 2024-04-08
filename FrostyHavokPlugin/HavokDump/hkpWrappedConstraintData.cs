using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpWrappedConstraintData : hkpConstraintData, IEquatable<hkpWrappedConstraintData?>
{
    public override uint Signature => 0;
    public hkpConstraintData _constraintData;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _constraintData = des.ReadClassPointer<hkpConstraintData>(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteClassPointer<hkpConstraintData>(bw, _constraintData);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClassPointer(xe, nameof(_constraintData), _constraintData);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkpWrappedConstraintData);
    }
    public bool Equals(hkpWrappedConstraintData? other)
    {
        return other is not null && _constraintData.Equals(other._constraintData) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_constraintData);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
