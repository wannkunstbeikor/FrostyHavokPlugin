using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpVehicleInstanceWheelInfo : IHavokObject, IEquatable<hknpVehicleInstanceWheelInfo?>
{
    public virtual uint Signature => 0;
    public hkContactPoint _contactPoint;
    public float _contactFriction;
    // TYPE_UINT32 TYPE_VOID _contactBodyId
    public uint _contactShapeKey;
    public Vector4 _hardPointWs;
    public Vector4 _rayEndPointWs;
    public float _currentSuspensionLength;
    public Vector4 _suspensionDirectionWs;
    public Vector4 _spinAxisChassisSpace;
    public Vector4 _spinAxisWs;
    public Quaternion _steeringOrientationChassisSpace;
    public float _spinVelocity;
    public float _noSlipIdealSpinVelocity;
    public float _spinAngle;
    public float _skidEnergyDensity;
    public float _sideForce;
    public float _forwardSlipVelocity;
    public float _sideSlipVelocity;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _contactPoint = new hkContactPoint();
        _contactPoint.Read(des, br);
        _contactFriction = br.ReadSingle();
        br.Position += 4; // padding
        _contactShapeKey = br.ReadUInt32();
        br.Position += 4; // padding
        _hardPointWs = des.ReadVector4(br);
        _rayEndPointWs = des.ReadVector4(br);
        _currentSuspensionLength = br.ReadSingle();
        br.Position += 12; // padding
        _suspensionDirectionWs = des.ReadVector4(br);
        _spinAxisChassisSpace = des.ReadVector4(br);
        _spinAxisWs = des.ReadVector4(br);
        _steeringOrientationChassisSpace = des.ReadQuaternion(br);
        _spinVelocity = br.ReadSingle();
        _noSlipIdealSpinVelocity = br.ReadSingle();
        _spinAngle = br.ReadSingle();
        _skidEnergyDensity = br.ReadSingle();
        _sideForce = br.ReadSingle();
        _forwardSlipVelocity = br.ReadSingle();
        _sideSlipVelocity = br.ReadSingle();
        br.Position += 4; // padding
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        _contactPoint.Write(s, bw);
        bw.WriteSingle(_contactFriction);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
        bw.WriteUInt32(_contactShapeKey);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
        s.WriteVector4(bw, _hardPointWs);
        s.WriteVector4(bw, _rayEndPointWs);
        bw.WriteSingle(_currentSuspensionLength);
        for (int i = 0; i < 12; i++) bw.WriteByte(0); // padding
        s.WriteVector4(bw, _suspensionDirectionWs);
        s.WriteVector4(bw, _spinAxisChassisSpace);
        s.WriteVector4(bw, _spinAxisWs);
        s.WriteQuaternion(bw, _steeringOrientationChassisSpace);
        bw.WriteSingle(_spinVelocity);
        bw.WriteSingle(_noSlipIdealSpinVelocity);
        bw.WriteSingle(_spinAngle);
        bw.WriteSingle(_skidEnergyDensity);
        bw.WriteSingle(_sideForce);
        bw.WriteSingle(_forwardSlipVelocity);
        bw.WriteSingle(_sideSlipVelocity);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClass(xe, nameof(_contactPoint), _contactPoint);
        xs.WriteFloat(xe, nameof(_contactFriction), _contactFriction);
        xs.WriteNumber(xe, nameof(_contactShapeKey), _contactShapeKey);
        xs.WriteVector4(xe, nameof(_hardPointWs), _hardPointWs);
        xs.WriteVector4(xe, nameof(_rayEndPointWs), _rayEndPointWs);
        xs.WriteFloat(xe, nameof(_currentSuspensionLength), _currentSuspensionLength);
        xs.WriteVector4(xe, nameof(_suspensionDirectionWs), _suspensionDirectionWs);
        xs.WriteVector4(xe, nameof(_spinAxisChassisSpace), _spinAxisChassisSpace);
        xs.WriteVector4(xe, nameof(_spinAxisWs), _spinAxisWs);
        xs.WriteQuaternion(xe, nameof(_steeringOrientationChassisSpace), _steeringOrientationChassisSpace);
        xs.WriteFloat(xe, nameof(_spinVelocity), _spinVelocity);
        xs.WriteFloat(xe, nameof(_noSlipIdealSpinVelocity), _noSlipIdealSpinVelocity);
        xs.WriteFloat(xe, nameof(_spinAngle), _spinAngle);
        xs.WriteFloat(xe, nameof(_skidEnergyDensity), _skidEnergyDensity);
        xs.WriteFloat(xe, nameof(_sideForce), _sideForce);
        xs.WriteFloat(xe, nameof(_forwardSlipVelocity), _forwardSlipVelocity);
        xs.WriteFloat(xe, nameof(_sideSlipVelocity), _sideSlipVelocity);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpVehicleInstanceWheelInfo);
    }
    public bool Equals(hknpVehicleInstanceWheelInfo? other)
    {
        return other is not null && _contactPoint.Equals(other._contactPoint) && _contactFriction.Equals(other._contactFriction) && _contactShapeKey.Equals(other._contactShapeKey) && _hardPointWs.Equals(other._hardPointWs) && _rayEndPointWs.Equals(other._rayEndPointWs) && _currentSuspensionLength.Equals(other._currentSuspensionLength) && _suspensionDirectionWs.Equals(other._suspensionDirectionWs) && _spinAxisChassisSpace.Equals(other._spinAxisChassisSpace) && _spinAxisWs.Equals(other._spinAxisWs) && _steeringOrientationChassisSpace.Equals(other._steeringOrientationChassisSpace) && _spinVelocity.Equals(other._spinVelocity) && _noSlipIdealSpinVelocity.Equals(other._noSlipIdealSpinVelocity) && _spinAngle.Equals(other._spinAngle) && _skidEnergyDensity.Equals(other._skidEnergyDensity) && _sideForce.Equals(other._sideForce) && _forwardSlipVelocity.Equals(other._forwardSlipVelocity) && _sideSlipVelocity.Equals(other._sideSlipVelocity) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_contactPoint);
        code.Add(_contactFriction);
        code.Add(_contactShapeKey);
        code.Add(_hardPointWs);
        code.Add(_rayEndPointWs);
        code.Add(_currentSuspensionLength);
        code.Add(_suspensionDirectionWs);
        code.Add(_spinAxisChassisSpace);
        code.Add(_spinAxisWs);
        code.Add(_steeringOrientationChassisSpace);
        code.Add(_spinVelocity);
        code.Add(_noSlipIdealSpinVelocity);
        code.Add(_spinAngle);
        code.Add(_skidEnergyDensity);
        code.Add(_sideForce);
        code.Add(_forwardSlipVelocity);
        code.Add(_sideSlipVelocity);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
