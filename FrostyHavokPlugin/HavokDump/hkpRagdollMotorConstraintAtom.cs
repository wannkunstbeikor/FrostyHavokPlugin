using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpRagdollMotorConstraintAtom : hkpConstraintAtom, IEquatable<hkpRagdollMotorConstraintAtom?>
{
    public override uint Signature => 0;
    public bool _isEnabled;
    // TYPE_INT16 TYPE_VOID _initializedOffset
    // TYPE_INT16 TYPE_VOID _previousTargetAnglesOffset
    public Matrix3x4 _target_bRca;
    public hkpConstraintMotor[] _motors = new hkpConstraintMotor[3];
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _isEnabled = br.ReadBoolean();
        br.Position += 13; // padding
        _target_bRca = des.ReadMatrix3(br);
        _motors = des.ReadClassPointerCStyleArray<hkpConstraintMotor>(br, 3);
        br.Position += 8; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteBoolean(_isEnabled);
        for (int i = 0; i < 13; i++) bw.WriteByte(0); // padding
        s.WriteMatrix3(bw, _target_bRca);
        s.WriteClassPointerCStyleArray<hkpConstraintMotor>(bw, _motors);
        for (int i = 0; i < 8; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteBoolean(xe, nameof(_isEnabled), _isEnabled);
        xs.WriteMatrix3(xe, nameof(_target_bRca), _target_bRca);
        xs.WriteClassPointerArray(xe, nameof(_motors), _motors);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkpRagdollMotorConstraintAtom);
    }
    public bool Equals(hkpRagdollMotorConstraintAtom? other)
    {
        return other is not null && _isEnabled.Equals(other._isEnabled) && _target_bRca.Equals(other._target_bRca) && _motors.Equals(other._motors) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_isEnabled);
        code.Add(_target_bRca);
        code.Add(_motors);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
