using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpMotionCinfo : IHavokObject
{
    public virtual uint Signature => 0;
    public ushort _motionPropertiesId;
    public bool _enableDeactivation;
    public float _inverseMass;
    public float _massFactor;
    public float _maxLinearAccelerationDistancePerStep;
    public float _maxRotationToPreventTunneling;
    public Vector4 _inverseInertiaLocal;
    public Vector4 _centerOfMassWorld;
    public Quaternion _orientation;
    public Vector4 _linearVelocity;
    public Vector4 _angularVelocity;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _motionPropertiesId = br.ReadUInt16();
        _enableDeactivation = br.ReadBoolean();
        br.Position += 1; // padding
        _inverseMass = br.ReadSingle();
        _massFactor = br.ReadSingle();
        _maxLinearAccelerationDistancePerStep = br.ReadSingle();
        _maxRotationToPreventTunneling = br.ReadSingle();
        br.Position += 12; // padding
        _inverseInertiaLocal = des.ReadVector4(br);
        _centerOfMassWorld = des.ReadVector4(br);
        _orientation = des.ReadQuaternion(br);
        _linearVelocity = des.ReadVector4(br);
        _angularVelocity = des.ReadVector4(br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        bw.WriteUInt16(_motionPropertiesId);
        bw.WriteBoolean(_enableDeactivation);
        for (int i = 0; i < 1; i++) bw.WriteByte(0); // padding
        bw.WriteSingle(_inverseMass);
        bw.WriteSingle(_massFactor);
        bw.WriteSingle(_maxLinearAccelerationDistancePerStep);
        bw.WriteSingle(_maxRotationToPreventTunneling);
        for (int i = 0; i < 12; i++) bw.WriteByte(0); // padding
        s.WriteVector4(bw, _inverseInertiaLocal);
        s.WriteVector4(bw, _centerOfMassWorld);
        s.WriteQuaternion(bw, _orientation);
        s.WriteVector4(bw, _linearVelocity);
        s.WriteVector4(bw, _angularVelocity);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumber(xe, nameof(_motionPropertiesId), _motionPropertiesId);
        xs.WriteBoolean(xe, nameof(_enableDeactivation), _enableDeactivation);
        xs.WriteFloat(xe, nameof(_inverseMass), _inverseMass);
        xs.WriteFloat(xe, nameof(_massFactor), _massFactor);
        xs.WriteFloat(xe, nameof(_maxLinearAccelerationDistancePerStep), _maxLinearAccelerationDistancePerStep);
        xs.WriteFloat(xe, nameof(_maxRotationToPreventTunneling), _maxRotationToPreventTunneling);
        xs.WriteVector4(xe, nameof(_inverseInertiaLocal), _inverseInertiaLocal);
        xs.WriteVector4(xe, nameof(_centerOfMassWorld), _centerOfMassWorld);
        xs.WriteQuaternion(xe, nameof(_orientation), _orientation);
        xs.WriteVector4(xe, nameof(_linearVelocity), _linearVelocity);
        xs.WriteVector4(xe, nameof(_angularVelocity), _angularVelocity);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpMotionCinfo other && _motionPropertiesId == other._motionPropertiesId && _enableDeactivation == other._enableDeactivation && _inverseMass == other._inverseMass && _massFactor == other._massFactor && _maxLinearAccelerationDistancePerStep == other._maxLinearAccelerationDistancePerStep && _maxRotationToPreventTunneling == other._maxRotationToPreventTunneling && _inverseInertiaLocal == other._inverseInertiaLocal && _centerOfMassWorld == other._centerOfMassWorld && _orientation == other._orientation && _linearVelocity == other._linearVelocity && _angularVelocity == other._angularVelocity && Signature == other.Signature;
    }
    public static bool operator ==(hknpMotionCinfo? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpMotionCinfo? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_motionPropertiesId);
        code.Add(_enableDeactivation);
        code.Add(_inverseMass);
        code.Add(_massFactor);
        code.Add(_maxLinearAccelerationDistancePerStep);
        code.Add(_maxRotationToPreventTunneling);
        code.Add(_inverseInertiaLocal);
        code.Add(_centerOfMassWorld);
        code.Add(_orientation);
        code.Add(_linearVelocity);
        code.Add(_angularVelocity);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
