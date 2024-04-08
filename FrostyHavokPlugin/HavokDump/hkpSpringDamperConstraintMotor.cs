using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpSpringDamperConstraintMotor : hkpLimitedForceConstraintMotor, IEquatable<hkpSpringDamperConstraintMotor?>
{
    public override uint Signature => 0;
    public float _springConstant;
    public float _springDamping;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _springConstant = br.ReadSingle();
        _springDamping = br.ReadSingle();
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteSingle(_springConstant);
        bw.WriteSingle(_springDamping);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteFloat(xe, nameof(_springConstant), _springConstant);
        xs.WriteFloat(xe, nameof(_springDamping), _springDamping);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkpSpringDamperConstraintMotor);
    }
    public bool Equals(hkpSpringDamperConstraintMotor? other)
    {
        return other is not null && _springConstant.Equals(other._springConstant) && _springDamping.Equals(other._springDamping) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_springConstant);
        code.Add(_springDamping);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
