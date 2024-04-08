using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpMotionCinfo : IHavokObject, IEquatable<hknpMotionCinfo?>
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
        return Equals(obj as hknpMotionCinfo);
    }
    public bool Equals(hknpMotionCinfo? other)
    {
        return other is not null && _motionPropertiesId.Equals(other._motionPropertiesId) && _enableDeactivation.Equals(other._enableDeactivation) && _inverseMass.Equals(other._inverseMass) && _massFactor.Equals(other._massFactor) && _maxLinearAccelerationDistancePerStep.Equals(other._maxLinearAccelerationDistancePerStep) && _maxRotationToPreventTunneling.Equals(other._maxRotationToPreventTunneling) && _inverseInertiaLocal.Equals(other._inverseInertiaLocal) && _centerOfMassWorld.Equals(other._centerOfMassWorld) && _orientation.Equals(other._orientation) && _linearVelocity.Equals(other._linearVelocity) && _angularVelocity.Equals(other._angularVelocity) && Signature == other.Signature;
    }
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
