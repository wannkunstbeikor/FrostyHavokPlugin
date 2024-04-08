using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpLinMotorConstraintAtom : hkpConstraintAtom, IEquatable<hkpLinMotorConstraintAtom?>
{
    public override uint Signature => 0;
    public bool _isEnabled;
    public byte _motorAxis;
    // TYPE_INT16 TYPE_VOID _initializedOffset
    // TYPE_INT16 TYPE_VOID _previousTargetPositionOffset
    public float _targetPosition;
    public hkpConstraintMotor _motor;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _isEnabled = br.ReadBoolean();
        _motorAxis = br.ReadByte();
        br.Position += 4; // padding
        _targetPosition = br.ReadSingle();
        br.Position += 4; // padding
        _motor = des.ReadClassPointer<hkpConstraintMotor>(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteBoolean(_isEnabled);
        bw.WriteByte(_motorAxis);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
        bw.WriteSingle(_targetPosition);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
        s.WriteClassPointer<hkpConstraintMotor>(bw, _motor);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteBoolean(xe, nameof(_isEnabled), _isEnabled);
        xs.WriteNumber(xe, nameof(_motorAxis), _motorAxis);
        xs.WriteFloat(xe, nameof(_targetPosition), _targetPosition);
        xs.WriteClassPointer(xe, nameof(_motor), _motor);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkpLinMotorConstraintAtom);
    }
    public bool Equals(hkpLinMotorConstraintAtom? other)
    {
        return other is not null && _isEnabled.Equals(other._isEnabled) && _motorAxis.Equals(other._motorAxis) && _targetPosition.Equals(other._targetPosition) && _motor.Equals(other._motor) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_isEnabled);
        code.Add(_motorAxis);
        code.Add(_targetPosition);
        code.Add(_motor);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
