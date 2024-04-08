using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpVelocityConstraintMotor : hkpLimitedForceConstraintMotor, IEquatable<hkpVelocityConstraintMotor?>
{
    public override uint Signature => 0;
    public float _tau;
    public float _velocityTarget;
    public bool _useVelocityTargetFromConstraintTargets;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _tau = br.ReadSingle();
        _velocityTarget = br.ReadSingle();
        _useVelocityTargetFromConstraintTargets = br.ReadBoolean();
        br.Position += 7; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteSingle(_tau);
        bw.WriteSingle(_velocityTarget);
        bw.WriteBoolean(_useVelocityTargetFromConstraintTargets);
        for (int i = 0; i < 7; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteFloat(xe, nameof(_tau), _tau);
        xs.WriteFloat(xe, nameof(_velocityTarget), _velocityTarget);
        xs.WriteBoolean(xe, nameof(_useVelocityTargetFromConstraintTargets), _useVelocityTargetFromConstraintTargets);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkpVelocityConstraintMotor);
    }
    public bool Equals(hkpVelocityConstraintMotor? other)
    {
        return other is not null && _tau.Equals(other._tau) && _velocityTarget.Equals(other._velocityTarget) && _useVelocityTargetFromConstraintTargets.Equals(other._useVelocityTargetFromConstraintTargets) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_tau);
        code.Add(_velocityTarget);
        code.Add(_useVelocityTargetFromConstraintTargets);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
