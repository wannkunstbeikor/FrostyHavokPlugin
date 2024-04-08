using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpMotion : IHavokObject, IEquatable<hknpMotion?>
{
    public virtual uint Signature => 0;
    public Vector4 _centerOfMassAndMassFactor;
    public Quaternion _orientation;
    public Half[] _inverseInertia = new Half[4];
    public uint _firstAttachedBodyId;
    // TYPE_UINT32 TYPE_VOID _solverId
    public Half[] _linearVelocityCage = new Half[3];
    public ushort _motionPropertiesId;
    public Half _maxLinearAccelerationDistancePerStep;
    public Half _maxRotationToPreventTunneling;
    public Half _integrationFactor;
    public byte _cellIndex;
    public byte _spaceSplitterWeight;
    public Vector4 _linearVelocity;
    public Vector4 _angularVelocity;
    public Vector4 _previousStepLinearVelocity;
    public Vector4 _previousStepAngularVelocity;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _centerOfMassAndMassFactor = des.ReadVector4(br);
        _orientation = des.ReadQuaternion(br);
        _inverseInertia = des.ReadHalfCStyleArray(br, 4);
        _firstAttachedBodyId = br.ReadUInt32();
        br.Position += 4; // padding
        _linearVelocityCage = des.ReadHalfCStyleArray(br, 3);
        _motionPropertiesId = br.ReadUInt16();
        _maxLinearAccelerationDistancePerStep = des.ReadHalf(br);
        _maxRotationToPreventTunneling = des.ReadHalf(br);
        _integrationFactor = des.ReadHalf(br);
        _cellIndex = br.ReadByte();
        _spaceSplitterWeight = br.ReadByte();
        _linearVelocity = des.ReadVector4(br);
        _angularVelocity = des.ReadVector4(br);
        _previousStepLinearVelocity = des.ReadVector4(br);
        _previousStepAngularVelocity = des.ReadVector4(br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteVector4(bw, _centerOfMassAndMassFactor);
        s.WriteQuaternion(bw, _orientation);
        s.WriteHalfCStyleArray(bw, _inverseInertia);
        bw.WriteUInt32(_firstAttachedBodyId);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
        s.WriteHalfCStyleArray(bw, _linearVelocityCage);
        bw.WriteUInt16(_motionPropertiesId);
        s.WriteHalf(bw, _maxLinearAccelerationDistancePerStep);
        s.WriteHalf(bw, _maxRotationToPreventTunneling);
        s.WriteHalf(bw, _integrationFactor);
        bw.WriteByte(_cellIndex);
        bw.WriteByte(_spaceSplitterWeight);
        s.WriteVector4(bw, _linearVelocity);
        s.WriteVector4(bw, _angularVelocity);
        s.WriteVector4(bw, _previousStepLinearVelocity);
        s.WriteVector4(bw, _previousStepAngularVelocity);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteVector4(xe, nameof(_centerOfMassAndMassFactor), _centerOfMassAndMassFactor);
        xs.WriteQuaternion(xe, nameof(_orientation), _orientation);
        xs.WriteFloatArray(xe, nameof(_inverseInertia), _inverseInertia);
        xs.WriteNumber(xe, nameof(_firstAttachedBodyId), _firstAttachedBodyId);
        xs.WriteFloatArray(xe, nameof(_linearVelocityCage), _linearVelocityCage);
        xs.WriteNumber(xe, nameof(_motionPropertiesId), _motionPropertiesId);
        xs.WriteFloat(xe, nameof(_maxLinearAccelerationDistancePerStep), _maxLinearAccelerationDistancePerStep);
        xs.WriteFloat(xe, nameof(_maxRotationToPreventTunneling), _maxRotationToPreventTunneling);
        xs.WriteFloat(xe, nameof(_integrationFactor), _integrationFactor);
        xs.WriteNumber(xe, nameof(_cellIndex), _cellIndex);
        xs.WriteNumber(xe, nameof(_spaceSplitterWeight), _spaceSplitterWeight);
        xs.WriteVector4(xe, nameof(_linearVelocity), _linearVelocity);
        xs.WriteVector4(xe, nameof(_angularVelocity), _angularVelocity);
        xs.WriteVector4(xe, nameof(_previousStepLinearVelocity), _previousStepLinearVelocity);
        xs.WriteVector4(xe, nameof(_previousStepAngularVelocity), _previousStepAngularVelocity);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpMotion);
    }
    public bool Equals(hknpMotion? other)
    {
        return other is not null && _centerOfMassAndMassFactor.Equals(other._centerOfMassAndMassFactor) && _orientation.Equals(other._orientation) && _inverseInertia.Equals(other._inverseInertia) && _firstAttachedBodyId.Equals(other._firstAttachedBodyId) && _linearVelocityCage.Equals(other._linearVelocityCage) && _motionPropertiesId.Equals(other._motionPropertiesId) && _maxLinearAccelerationDistancePerStep.Equals(other._maxLinearAccelerationDistancePerStep) && _maxRotationToPreventTunneling.Equals(other._maxRotationToPreventTunneling) && _integrationFactor.Equals(other._integrationFactor) && _cellIndex.Equals(other._cellIndex) && _spaceSplitterWeight.Equals(other._spaceSplitterWeight) && _linearVelocity.Equals(other._linearVelocity) && _angularVelocity.Equals(other._angularVelocity) && _previousStepLinearVelocity.Equals(other._previousStepLinearVelocity) && _previousStepAngularVelocity.Equals(other._previousStepAngularVelocity) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_centerOfMassAndMassFactor);
        code.Add(_orientation);
        code.Add(_inverseInertia);
        code.Add(_firstAttachedBodyId);
        code.Add(_linearVelocityCage);
        code.Add(_motionPropertiesId);
        code.Add(_maxLinearAccelerationDistancePerStep);
        code.Add(_maxRotationToPreventTunneling);
        code.Add(_integrationFactor);
        code.Add(_cellIndex);
        code.Add(_spaceSplitterWeight);
        code.Add(_linearVelocity);
        code.Add(_angularVelocity);
        code.Add(_previousStepLinearVelocity);
        code.Add(_previousStepAngularVelocity);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
