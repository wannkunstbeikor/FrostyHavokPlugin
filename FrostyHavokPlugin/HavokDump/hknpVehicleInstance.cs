using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpVehicleInstance : hknpUnaryAction, IEquatable<hknpVehicleInstance?>
{
    public override uint Signature => 0;
    public hknpVehicleData _data;
    public hknpVehicleDriverInput _driverInput;
    public hknpVehicleSteering _steering;
    public hknpVehicleEngine _engine;
    public hknpVehicleTransmission _transmission;
    public hknpVehicleBrake _brake;
    public hknpVehicleSuspension _suspension;
    public hknpVehicleAerodynamics _aerodynamics;
    public hknpVehicleWheelCollide _wheelCollide;
    public hknpTyremarksInfo _tyreMarks;
    public hknpVehicleVelocityDamper _velocityDamper;
    public List<hknpVehicleInstanceWheelInfo> _wheelsInfo;
    public hkpVehicleFrictionStatus _frictionStatus;
    public hknpVehicleDriverInputStatus _deviceStatus;
    public List<bool> _isFixed;
    public float _wheelsTimeSinceMaxPedalInput;
    public bool _tryingToReverse;
    public float _torque;
    public float _rpm;
    public float _mainSteeringAngle;
    public float _mainSteeringAngleAssumingNoReduction;
    public List<float> _wheelsSteeringAngle;
    public bool _isReversing;
    public sbyte _currentGear;
    public bool _delayed;
    public float _clutchDelayCountdown;
    // TYPE_POINTER TYPE_VOID _world
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _data = des.ReadClassPointer<hknpVehicleData>(br);
        _driverInput = des.ReadClassPointer<hknpVehicleDriverInput>(br);
        _steering = des.ReadClassPointer<hknpVehicleSteering>(br);
        _engine = des.ReadClassPointer<hknpVehicleEngine>(br);
        _transmission = des.ReadClassPointer<hknpVehicleTransmission>(br);
        _brake = des.ReadClassPointer<hknpVehicleBrake>(br);
        _suspension = des.ReadClassPointer<hknpVehicleSuspension>(br);
        _aerodynamics = des.ReadClassPointer<hknpVehicleAerodynamics>(br);
        _wheelCollide = des.ReadClassPointer<hknpVehicleWheelCollide>(br);
        _tyreMarks = des.ReadClassPointer<hknpTyremarksInfo>(br);
        _velocityDamper = des.ReadClassPointer<hknpVehicleVelocityDamper>(br);
        _wheelsInfo = des.ReadClassArray<hknpVehicleInstanceWheelInfo>(br);
        _frictionStatus = new hkpVehicleFrictionStatus();
        _frictionStatus.Read(des, br);
        _deviceStatus = des.ReadClassPointer<hknpVehicleDriverInputStatus>(br);
        _isFixed = des.ReadBooleanArray(br);
        _wheelsTimeSinceMaxPedalInput = br.ReadSingle();
        _tryingToReverse = br.ReadBoolean();
        br.Position += 3; // padding
        _torque = br.ReadSingle();
        _rpm = br.ReadSingle();
        _mainSteeringAngle = br.ReadSingle();
        _mainSteeringAngleAssumingNoReduction = br.ReadSingle();
        _wheelsSteeringAngle = des.ReadSingleArray(br);
        _isReversing = br.ReadBoolean();
        _currentGear = br.ReadSByte();
        _delayed = br.ReadBoolean();
        br.Position += 1; // padding
        _clutchDelayCountdown = br.ReadSingle();
        br.Position += 8; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteClassPointer<hknpVehicleData>(bw, _data);
        s.WriteClassPointer<hknpVehicleDriverInput>(bw, _driverInput);
        s.WriteClassPointer<hknpVehicleSteering>(bw, _steering);
        s.WriteClassPointer<hknpVehicleEngine>(bw, _engine);
        s.WriteClassPointer<hknpVehicleTransmission>(bw, _transmission);
        s.WriteClassPointer<hknpVehicleBrake>(bw, _brake);
        s.WriteClassPointer<hknpVehicleSuspension>(bw, _suspension);
        s.WriteClassPointer<hknpVehicleAerodynamics>(bw, _aerodynamics);
        s.WriteClassPointer<hknpVehicleWheelCollide>(bw, _wheelCollide);
        s.WriteClassPointer<hknpTyremarksInfo>(bw, _tyreMarks);
        s.WriteClassPointer<hknpVehicleVelocityDamper>(bw, _velocityDamper);
        s.WriteClassArray<hknpVehicleInstanceWheelInfo>(bw, _wheelsInfo);
        _frictionStatus.Write(s, bw);
        s.WriteClassPointer<hknpVehicleDriverInputStatus>(bw, _deviceStatus);
        s.WriteBooleanArray(bw, _isFixed);
        bw.WriteSingle(_wheelsTimeSinceMaxPedalInput);
        bw.WriteBoolean(_tryingToReverse);
        for (int i = 0; i < 3; i++) bw.WriteByte(0); // padding
        bw.WriteSingle(_torque);
        bw.WriteSingle(_rpm);
        bw.WriteSingle(_mainSteeringAngle);
        bw.WriteSingle(_mainSteeringAngleAssumingNoReduction);
        s.WriteSingleArray(bw, _wheelsSteeringAngle);
        bw.WriteBoolean(_isReversing);
        bw.WriteSByte(_currentGear);
        bw.WriteBoolean(_delayed);
        for (int i = 0; i < 1; i++) bw.WriteByte(0); // padding
        bw.WriteSingle(_clutchDelayCountdown);
        for (int i = 0; i < 8; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClassPointer(xe, nameof(_data), _data);
        xs.WriteClassPointer(xe, nameof(_driverInput), _driverInput);
        xs.WriteClassPointer(xe, nameof(_steering), _steering);
        xs.WriteClassPointer(xe, nameof(_engine), _engine);
        xs.WriteClassPointer(xe, nameof(_transmission), _transmission);
        xs.WriteClassPointer(xe, nameof(_brake), _brake);
        xs.WriteClassPointer(xe, nameof(_suspension), _suspension);
        xs.WriteClassPointer(xe, nameof(_aerodynamics), _aerodynamics);
        xs.WriteClassPointer(xe, nameof(_wheelCollide), _wheelCollide);
        xs.WriteClassPointer(xe, nameof(_tyreMarks), _tyreMarks);
        xs.WriteClassPointer(xe, nameof(_velocityDamper), _velocityDamper);
        xs.WriteClassArray<hknpVehicleInstanceWheelInfo>(xe, nameof(_wheelsInfo), _wheelsInfo);
        xs.WriteClass(xe, nameof(_frictionStatus), _frictionStatus);
        xs.WriteClassPointer(xe, nameof(_deviceStatus), _deviceStatus);
        xs.WriteBooleanArray(xe, nameof(_isFixed), _isFixed);
        xs.WriteFloat(xe, nameof(_wheelsTimeSinceMaxPedalInput), _wheelsTimeSinceMaxPedalInput);
        xs.WriteBoolean(xe, nameof(_tryingToReverse), _tryingToReverse);
        xs.WriteFloat(xe, nameof(_torque), _torque);
        xs.WriteFloat(xe, nameof(_rpm), _rpm);
        xs.WriteFloat(xe, nameof(_mainSteeringAngle), _mainSteeringAngle);
        xs.WriteFloat(xe, nameof(_mainSteeringAngleAssumingNoReduction), _mainSteeringAngleAssumingNoReduction);
        xs.WriteFloatArray(xe, nameof(_wheelsSteeringAngle), _wheelsSteeringAngle);
        xs.WriteBoolean(xe, nameof(_isReversing), _isReversing);
        xs.WriteNumber(xe, nameof(_currentGear), _currentGear);
        xs.WriteBoolean(xe, nameof(_delayed), _delayed);
        xs.WriteFloat(xe, nameof(_clutchDelayCountdown), _clutchDelayCountdown);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpVehicleInstance);
    }
    public bool Equals(hknpVehicleInstance? other)
    {
        return other is not null && _data.Equals(other._data) && _driverInput.Equals(other._driverInput) && _steering.Equals(other._steering) && _engine.Equals(other._engine) && _transmission.Equals(other._transmission) && _brake.Equals(other._brake) && _suspension.Equals(other._suspension) && _aerodynamics.Equals(other._aerodynamics) && _wheelCollide.Equals(other._wheelCollide) && _tyreMarks.Equals(other._tyreMarks) && _velocityDamper.Equals(other._velocityDamper) && _wheelsInfo.Equals(other._wheelsInfo) && _frictionStatus.Equals(other._frictionStatus) && _deviceStatus.Equals(other._deviceStatus) && _isFixed.Equals(other._isFixed) && _wheelsTimeSinceMaxPedalInput.Equals(other._wheelsTimeSinceMaxPedalInput) && _tryingToReverse.Equals(other._tryingToReverse) && _torque.Equals(other._torque) && _rpm.Equals(other._rpm) && _mainSteeringAngle.Equals(other._mainSteeringAngle) && _mainSteeringAngleAssumingNoReduction.Equals(other._mainSteeringAngleAssumingNoReduction) && _wheelsSteeringAngle.Equals(other._wheelsSteeringAngle) && _isReversing.Equals(other._isReversing) && _currentGear.Equals(other._currentGear) && _delayed.Equals(other._delayed) && _clutchDelayCountdown.Equals(other._clutchDelayCountdown) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_data);
        code.Add(_driverInput);
        code.Add(_steering);
        code.Add(_engine);
        code.Add(_transmission);
        code.Add(_brake);
        code.Add(_suspension);
        code.Add(_aerodynamics);
        code.Add(_wheelCollide);
        code.Add(_tyreMarks);
        code.Add(_velocityDamper);
        code.Add(_wheelsInfo);
        code.Add(_frictionStatus);
        code.Add(_deviceStatus);
        code.Add(_isFixed);
        code.Add(_wheelsTimeSinceMaxPedalInput);
        code.Add(_tryingToReverse);
        code.Add(_torque);
        code.Add(_rpm);
        code.Add(_mainSteeringAngle);
        code.Add(_mainSteeringAngleAssumingNoReduction);
        code.Add(_wheelsSteeringAngle);
        code.Add(_isReversing);
        code.Add(_currentGear);
        code.Add(_delayed);
        code.Add(_clutchDelayCountdown);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
