using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpCharacterProxyCinfo : hkReferencedObject, IEquatable<hknpCharacterProxyCinfo?>
{
    public override uint Signature => 0;
    public Vector4 _position;
    public Quaternion _orientation;
    public Vector4 _velocity;
    public float _dynamicFriction;
    public float _staticFriction;
    public float _keepContactTolerance;
    public Vector4 _up;
    public hknpShape _shape;
    // TYPE_POINTER TYPE_VOID _world
    public uint _collisionFilterInfo;
    public float _keepDistance;
    public float _contactAngleSensitivity;
    public uint _userPlanes;
    public float _maxCharacterSpeedForSolver;
    public float _characterStrength;
    public float _characterMass;
    public float _maxSlope;
    public float _penetrationRecoverySpeed;
    public int _maxCastIterations;
    public bool _refreshManifoldInCheckSupport;
    public bool _presenceInWorld;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _position = des.ReadVector4(br);
        _orientation = des.ReadQuaternion(br);
        _velocity = des.ReadVector4(br);
        _dynamicFriction = br.ReadSingle();
        _staticFriction = br.ReadSingle();
        _keepContactTolerance = br.ReadSingle();
        br.Position += 4; // padding
        _up = des.ReadVector4(br);
        _shape = des.ReadClassPointer<hknpShape>(br);
        br.Position += 8; // padding
        _collisionFilterInfo = br.ReadUInt32();
        _keepDistance = br.ReadSingle();
        _contactAngleSensitivity = br.ReadSingle();
        _userPlanes = br.ReadUInt32();
        _maxCharacterSpeedForSolver = br.ReadSingle();
        _characterStrength = br.ReadSingle();
        _characterMass = br.ReadSingle();
        _maxSlope = br.ReadSingle();
        _penetrationRecoverySpeed = br.ReadSingle();
        _maxCastIterations = br.ReadInt32();
        _refreshManifoldInCheckSupport = br.ReadBoolean();
        _presenceInWorld = br.ReadBoolean();
        br.Position += 6; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteVector4(bw, _position);
        s.WriteQuaternion(bw, _orientation);
        s.WriteVector4(bw, _velocity);
        bw.WriteSingle(_dynamicFriction);
        bw.WriteSingle(_staticFriction);
        bw.WriteSingle(_keepContactTolerance);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
        s.WriteVector4(bw, _up);
        s.WriteClassPointer<hknpShape>(bw, _shape);
        for (int i = 0; i < 8; i++) bw.WriteByte(0); // padding
        bw.WriteUInt32(_collisionFilterInfo);
        bw.WriteSingle(_keepDistance);
        bw.WriteSingle(_contactAngleSensitivity);
        bw.WriteUInt32(_userPlanes);
        bw.WriteSingle(_maxCharacterSpeedForSolver);
        bw.WriteSingle(_characterStrength);
        bw.WriteSingle(_characterMass);
        bw.WriteSingle(_maxSlope);
        bw.WriteSingle(_penetrationRecoverySpeed);
        bw.WriteInt32(_maxCastIterations);
        bw.WriteBoolean(_refreshManifoldInCheckSupport);
        bw.WriteBoolean(_presenceInWorld);
        for (int i = 0; i < 6; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteVector4(xe, nameof(_position), _position);
        xs.WriteQuaternion(xe, nameof(_orientation), _orientation);
        xs.WriteVector4(xe, nameof(_velocity), _velocity);
        xs.WriteFloat(xe, nameof(_dynamicFriction), _dynamicFriction);
        xs.WriteFloat(xe, nameof(_staticFriction), _staticFriction);
        xs.WriteFloat(xe, nameof(_keepContactTolerance), _keepContactTolerance);
        xs.WriteVector4(xe, nameof(_up), _up);
        xs.WriteClassPointer(xe, nameof(_shape), _shape);
        xs.WriteNumber(xe, nameof(_collisionFilterInfo), _collisionFilterInfo);
        xs.WriteFloat(xe, nameof(_keepDistance), _keepDistance);
        xs.WriteFloat(xe, nameof(_contactAngleSensitivity), _contactAngleSensitivity);
        xs.WriteNumber(xe, nameof(_userPlanes), _userPlanes);
        xs.WriteFloat(xe, nameof(_maxCharacterSpeedForSolver), _maxCharacterSpeedForSolver);
        xs.WriteFloat(xe, nameof(_characterStrength), _characterStrength);
        xs.WriteFloat(xe, nameof(_characterMass), _characterMass);
        xs.WriteFloat(xe, nameof(_maxSlope), _maxSlope);
        xs.WriteFloat(xe, nameof(_penetrationRecoverySpeed), _penetrationRecoverySpeed);
        xs.WriteNumber(xe, nameof(_maxCastIterations), _maxCastIterations);
        xs.WriteBoolean(xe, nameof(_refreshManifoldInCheckSupport), _refreshManifoldInCheckSupport);
        xs.WriteBoolean(xe, nameof(_presenceInWorld), _presenceInWorld);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpCharacterProxyCinfo);
    }
    public bool Equals(hknpCharacterProxyCinfo? other)
    {
        return other is not null && _position.Equals(other._position) && _orientation.Equals(other._orientation) && _velocity.Equals(other._velocity) && _dynamicFriction.Equals(other._dynamicFriction) && _staticFriction.Equals(other._staticFriction) && _keepContactTolerance.Equals(other._keepContactTolerance) && _up.Equals(other._up) && _shape.Equals(other._shape) && _collisionFilterInfo.Equals(other._collisionFilterInfo) && _keepDistance.Equals(other._keepDistance) && _contactAngleSensitivity.Equals(other._contactAngleSensitivity) && _userPlanes.Equals(other._userPlanes) && _maxCharacterSpeedForSolver.Equals(other._maxCharacterSpeedForSolver) && _characterStrength.Equals(other._characterStrength) && _characterMass.Equals(other._characterMass) && _maxSlope.Equals(other._maxSlope) && _penetrationRecoverySpeed.Equals(other._penetrationRecoverySpeed) && _maxCastIterations.Equals(other._maxCastIterations) && _refreshManifoldInCheckSupport.Equals(other._refreshManifoldInCheckSupport) && _presenceInWorld.Equals(other._presenceInWorld) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_position);
        code.Add(_orientation);
        code.Add(_velocity);
        code.Add(_dynamicFriction);
        code.Add(_staticFriction);
        code.Add(_keepContactTolerance);
        code.Add(_up);
        code.Add(_shape);
        code.Add(_collisionFilterInfo);
        code.Add(_keepDistance);
        code.Add(_contactAngleSensitivity);
        code.Add(_userPlanes);
        code.Add(_maxCharacterSpeedForSolver);
        code.Add(_characterStrength);
        code.Add(_characterMass);
        code.Add(_maxSlope);
        code.Add(_penetrationRecoverySpeed);
        code.Add(_maxCastIterations);
        code.Add(_refreshManifoldInCheckSupport);
        code.Add(_presenceInWorld);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
