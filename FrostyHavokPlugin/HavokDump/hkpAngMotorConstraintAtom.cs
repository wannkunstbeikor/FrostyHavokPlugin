using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpAngMotorConstraintAtom : hkpConstraintAtom, IEquatable<hkpAngMotorConstraintAtom?>
{
    public override uint Signature => 0;
    public bool _isEnabled;
    public byte _motorAxis;
    // TYPE_INT16 TYPE_VOID _initializedOffset
    // TYPE_INT16 TYPE_VOID _previousTargetAngleOffset
    // TYPE_INT16 TYPE_VOID _correspondingAngLimitSolverResultOffset
    public float _targetAngle;
    public hkpConstraintMotor _motor;
    // TYPE_UINT8 TYPE_VOID _padding
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _isEnabled = br.ReadBoolean();
        _motorAxis = br.ReadByte();
        br.Position += 8; // padding
        _targetAngle = br.ReadSingle();
        _motor = des.ReadClassPointer<hkpConstraintMotor>(br);
        br.Position += 16; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteBoolean(_isEnabled);
        bw.WriteByte(_motorAxis);
        for (int i = 0; i < 8; i++) bw.WriteByte(0); // padding
        bw.WriteSingle(_targetAngle);
        s.WriteClassPointer<hkpConstraintMotor>(bw, _motor);
        for (int i = 0; i < 16; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteBoolean(xe, nameof(_isEnabled), _isEnabled);
        xs.WriteNumber(xe, nameof(_motorAxis), _motorAxis);
        xs.WriteFloat(xe, nameof(_targetAngle), _targetAngle);
        xs.WriteClassPointer(xe, nameof(_motor), _motor);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkpAngMotorConstraintAtom);
    }
    public bool Equals(hkpAngMotorConstraintAtom? other)
    {
        return other is not null && _isEnabled.Equals(other._isEnabled) && _motorAxis.Equals(other._motorAxis) && _targetAngle.Equals(other._targetAngle) && _motor.Equals(other._motor) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_isEnabled);
        code.Add(_motorAxis);
        code.Add(_targetAngle);
        code.Add(_motor);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
