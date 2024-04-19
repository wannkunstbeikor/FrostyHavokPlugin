using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpPositionConstraintMotor : hkpLimitedForceConstraintMotor
{
    public override uint Signature => 0;
    public float _tau;
    public float _damping;
    public float _proportionalRecoveryVelocity;
    public float _constantRecoveryVelocity;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _tau = br.ReadSingle();
        _damping = br.ReadSingle();
        _proportionalRecoveryVelocity = br.ReadSingle();
        _constantRecoveryVelocity = br.ReadSingle();
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteSingle(_tau);
        bw.WriteSingle(_damping);
        bw.WriteSingle(_proportionalRecoveryVelocity);
        bw.WriteSingle(_constantRecoveryVelocity);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteFloat(xe, nameof(_tau), _tau);
        xs.WriteFloat(xe, nameof(_damping), _damping);
        xs.WriteFloat(xe, nameof(_proportionalRecoveryVelocity), _proportionalRecoveryVelocity);
        xs.WriteFloat(xe, nameof(_constantRecoveryVelocity), _constantRecoveryVelocity);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkpPositionConstraintMotor other && base.Equals(other) && _tau == other._tau && _damping == other._damping && _proportionalRecoveryVelocity == other._proportionalRecoveryVelocity && _constantRecoveryVelocity == other._constantRecoveryVelocity && Signature == other.Signature;
    }
    public static bool operator ==(hkpPositionConstraintMotor? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkpPositionConstraintMotor? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_tau);
        code.Add(_damping);
        code.Add(_proportionalRecoveryVelocity);
        code.Add(_constantRecoveryVelocity);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
