using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpVehicleData : hkReferencedObject, IEquatable<hknpVehicleData?>
{
    public override uint Signature => 0;
    public Vector4 _gravity;
    public sbyte _numWheels;
    public Matrix3x4 _chassisOrientation;
    public float _torqueRollFactor;
    public float _torquePitchFactor;
    public float _torqueYawFactor;
    public float _extraTorqueFactor;
    public float _maxVelocityForPositionalFriction;
    public float _chassisUnitInertiaYaw;
    public float _chassisUnitInertiaRoll;
    public float _chassisUnitInertiaPitch;
    public float _frictionEqualizer;
    public float _normalClippingAngleCos;
    public float _maxFrictionSolverMassRatio;
    public List<hknpVehicleDataWheelComponentParams> _wheelParams;
    public List<sbyte> _numWheelsPerAxle;
    public hkpVehicleFrictionDescription _frictionDescription;
    public Vector4 _chassisFrictionInertiaInvDiag;
    public bool _alreadyInitialised;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _gravity = des.ReadVector4(br);
        _numWheels = br.ReadSByte();
        br.Position += 15; // padding
        _chassisOrientation = des.ReadMatrix3(br);
        _torqueRollFactor = br.ReadSingle();
        _torquePitchFactor = br.ReadSingle();
        _torqueYawFactor = br.ReadSingle();
        _extraTorqueFactor = br.ReadSingle();
        _maxVelocityForPositionalFriction = br.ReadSingle();
        _chassisUnitInertiaYaw = br.ReadSingle();
        _chassisUnitInertiaRoll = br.ReadSingle();
        _chassisUnitInertiaPitch = br.ReadSingle();
        _frictionEqualizer = br.ReadSingle();
        _normalClippingAngleCos = br.ReadSingle();
        _maxFrictionSolverMassRatio = br.ReadSingle();
        br.Position += 4; // padding
        _wheelParams = des.ReadClassArray<hknpVehicleDataWheelComponentParams>(br);
        _numWheelsPerAxle = des.ReadSByteArray(br);
        _frictionDescription = new hkpVehicleFrictionDescription();
        _frictionDescription.Read(des, br);
        _chassisFrictionInertiaInvDiag = des.ReadVector4(br);
        _alreadyInitialised = br.ReadBoolean();
        br.Position += 15; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteVector4(bw, _gravity);
        bw.WriteSByte(_numWheels);
        for (int i = 0; i < 15; i++) bw.WriteByte(0); // padding
        s.WriteMatrix3(bw, _chassisOrientation);
        bw.WriteSingle(_torqueRollFactor);
        bw.WriteSingle(_torquePitchFactor);
        bw.WriteSingle(_torqueYawFactor);
        bw.WriteSingle(_extraTorqueFactor);
        bw.WriteSingle(_maxVelocityForPositionalFriction);
        bw.WriteSingle(_chassisUnitInertiaYaw);
        bw.WriteSingle(_chassisUnitInertiaRoll);
        bw.WriteSingle(_chassisUnitInertiaPitch);
        bw.WriteSingle(_frictionEqualizer);
        bw.WriteSingle(_normalClippingAngleCos);
        bw.WriteSingle(_maxFrictionSolverMassRatio);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
        s.WriteClassArray<hknpVehicleDataWheelComponentParams>(bw, _wheelParams);
        s.WriteSByteArray(bw, _numWheelsPerAxle);
        _frictionDescription.Write(s, bw);
        s.WriteVector4(bw, _chassisFrictionInertiaInvDiag);
        bw.WriteBoolean(_alreadyInitialised);
        for (int i = 0; i < 15; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteVector4(xe, nameof(_gravity), _gravity);
        xs.WriteNumber(xe, nameof(_numWheels), _numWheels);
        xs.WriteMatrix3(xe, nameof(_chassisOrientation), _chassisOrientation);
        xs.WriteFloat(xe, nameof(_torqueRollFactor), _torqueRollFactor);
        xs.WriteFloat(xe, nameof(_torquePitchFactor), _torquePitchFactor);
        xs.WriteFloat(xe, nameof(_torqueYawFactor), _torqueYawFactor);
        xs.WriteFloat(xe, nameof(_extraTorqueFactor), _extraTorqueFactor);
        xs.WriteFloat(xe, nameof(_maxVelocityForPositionalFriction), _maxVelocityForPositionalFriction);
        xs.WriteFloat(xe, nameof(_chassisUnitInertiaYaw), _chassisUnitInertiaYaw);
        xs.WriteFloat(xe, nameof(_chassisUnitInertiaRoll), _chassisUnitInertiaRoll);
        xs.WriteFloat(xe, nameof(_chassisUnitInertiaPitch), _chassisUnitInertiaPitch);
        xs.WriteFloat(xe, nameof(_frictionEqualizer), _frictionEqualizer);
        xs.WriteFloat(xe, nameof(_normalClippingAngleCos), _normalClippingAngleCos);
        xs.WriteFloat(xe, nameof(_maxFrictionSolverMassRatio), _maxFrictionSolverMassRatio);
        xs.WriteClassArray<hknpVehicleDataWheelComponentParams>(xe, nameof(_wheelParams), _wheelParams);
        xs.WriteNumberArray(xe, nameof(_numWheelsPerAxle), _numWheelsPerAxle);
        xs.WriteClass(xe, nameof(_frictionDescription), _frictionDescription);
        xs.WriteVector4(xe, nameof(_chassisFrictionInertiaInvDiag), _chassisFrictionInertiaInvDiag);
        xs.WriteBoolean(xe, nameof(_alreadyInitialised), _alreadyInitialised);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpVehicleData);
    }
    public bool Equals(hknpVehicleData? other)
    {
        return other is not null && _gravity.Equals(other._gravity) && _numWheels.Equals(other._numWheels) && _chassisOrientation.Equals(other._chassisOrientation) && _torqueRollFactor.Equals(other._torqueRollFactor) && _torquePitchFactor.Equals(other._torquePitchFactor) && _torqueYawFactor.Equals(other._torqueYawFactor) && _extraTorqueFactor.Equals(other._extraTorqueFactor) && _maxVelocityForPositionalFriction.Equals(other._maxVelocityForPositionalFriction) && _chassisUnitInertiaYaw.Equals(other._chassisUnitInertiaYaw) && _chassisUnitInertiaRoll.Equals(other._chassisUnitInertiaRoll) && _chassisUnitInertiaPitch.Equals(other._chassisUnitInertiaPitch) && _frictionEqualizer.Equals(other._frictionEqualizer) && _normalClippingAngleCos.Equals(other._normalClippingAngleCos) && _maxFrictionSolverMassRatio.Equals(other._maxFrictionSolverMassRatio) && _wheelParams.Equals(other._wheelParams) && _numWheelsPerAxle.Equals(other._numWheelsPerAxle) && _frictionDescription.Equals(other._frictionDescription) && _chassisFrictionInertiaInvDiag.Equals(other._chassisFrictionInertiaInvDiag) && _alreadyInitialised.Equals(other._alreadyInitialised) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_gravity);
        code.Add(_numWheels);
        code.Add(_chassisOrientation);
        code.Add(_torqueRollFactor);
        code.Add(_torquePitchFactor);
        code.Add(_torqueYawFactor);
        code.Add(_extraTorqueFactor);
        code.Add(_maxVelocityForPositionalFriction);
        code.Add(_chassisUnitInertiaYaw);
        code.Add(_chassisUnitInertiaRoll);
        code.Add(_chassisUnitInertiaPitch);
        code.Add(_frictionEqualizer);
        code.Add(_normalClippingAngleCos);
        code.Add(_maxFrictionSolverMassRatio);
        code.Add(_wheelParams);
        code.Add(_numWheelsPerAxle);
        code.Add(_frictionDescription);
        code.Add(_chassisFrictionInertiaInvDiag);
        code.Add(_alreadyInitialised);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
