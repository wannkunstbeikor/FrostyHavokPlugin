using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpWrappedConstraintData : hkpConstraintData
{
    public override uint Signature => 0;
    public hkpConstraintData? _constraintData;
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
        return obj is hkpWrappedConstraintData other && base.Equals(other) && _constraintData == other._constraintData && Signature == other.Signature;
    }
    public static bool operator ==(hkpWrappedConstraintData? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkpWrappedConstraintData? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_constraintData);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
