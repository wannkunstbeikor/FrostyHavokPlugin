using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpCharacterRigidBodyCinfo : hkReferencedObject, IEquatable<hknpCharacterRigidBodyCinfo?>
{
    public override uint Signature => 0;
    public uint _collisionFilterInfo;
    public hknpShape _shape;
    // TYPE_POINTER TYPE_VOID _world
    public Vector4 _position;
    public Quaternion _orientation;
    public float _mass;
    public float _dynamicFriction;
    public float _staticFriction;
    public float _weldingTolerance;
    public uint _reservedBodyId;
    public byte _additionFlags;
    public Vector4 _up;
    public float _maxSlope;
    public float _maxForce;
    public float _maxSpeedForSimplexSolver;
    public float _supportDistance;
    public float _hardSupportDistance;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _collisionFilterInfo = br.ReadUInt32();
        br.Position += 4; // padding
        _shape = des.ReadClassPointer<hknpShape>(br);
        br.Position += 16; // padding
        _position = des.ReadVector4(br);
        _orientation = des.ReadQuaternion(br);
        _mass = br.ReadSingle();
        _dynamicFriction = br.ReadSingle();
        _staticFriction = br.ReadSingle();
        _weldingTolerance = br.ReadSingle();
        _reservedBodyId = br.ReadUInt32();
        _additionFlags = br.ReadByte();
        br.Position += 11; // padding
        _up = des.ReadVector4(br);
        _maxSlope = br.ReadSingle();
        _maxForce = br.ReadSingle();
        _maxSpeedForSimplexSolver = br.ReadSingle();
        _supportDistance = br.ReadSingle();
        _hardSupportDistance = br.ReadSingle();
        br.Position += 12; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteUInt32(_collisionFilterInfo);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
        s.WriteClassPointer<hknpShape>(bw, _shape);
        for (int i = 0; i < 16; i++) bw.WriteByte(0); // padding
        s.WriteVector4(bw, _position);
        s.WriteQuaternion(bw, _orientation);
        bw.WriteSingle(_mass);
        bw.WriteSingle(_dynamicFriction);
        bw.WriteSingle(_staticFriction);
        bw.WriteSingle(_weldingTolerance);
        bw.WriteUInt32(_reservedBodyId);
        bw.WriteByte(_additionFlags);
        for (int i = 0; i < 11; i++) bw.WriteByte(0); // padding
        s.WriteVector4(bw, _up);
        bw.WriteSingle(_maxSlope);
        bw.WriteSingle(_maxForce);
        bw.WriteSingle(_maxSpeedForSimplexSolver);
        bw.WriteSingle(_supportDistance);
        bw.WriteSingle(_hardSupportDistance);
        for (int i = 0; i < 12; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteNumber(xe, nameof(_collisionFilterInfo), _collisionFilterInfo);
        xs.WriteClassPointer(xe, nameof(_shape), _shape);
        xs.WriteVector4(xe, nameof(_position), _position);
        xs.WriteQuaternion(xe, nameof(_orientation), _orientation);
        xs.WriteFloat(xe, nameof(_mass), _mass);
        xs.WriteFloat(xe, nameof(_dynamicFriction), _dynamicFriction);
        xs.WriteFloat(xe, nameof(_staticFriction), _staticFriction);
        xs.WriteFloat(xe, nameof(_weldingTolerance), _weldingTolerance);
        xs.WriteNumber(xe, nameof(_reservedBodyId), _reservedBodyId);
        xs.WriteNumber(xe, nameof(_additionFlags), _additionFlags);
        xs.WriteVector4(xe, nameof(_up), _up);
        xs.WriteFloat(xe, nameof(_maxSlope), _maxSlope);
        xs.WriteFloat(xe, nameof(_maxForce), _maxForce);
        xs.WriteFloat(xe, nameof(_maxSpeedForSimplexSolver), _maxSpeedForSimplexSolver);
        xs.WriteFloat(xe, nameof(_supportDistance), _supportDistance);
        xs.WriteFloat(xe, nameof(_hardSupportDistance), _hardSupportDistance);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpCharacterRigidBodyCinfo);
    }
    public bool Equals(hknpCharacterRigidBodyCinfo? other)
    {
        return other is not null && _collisionFilterInfo.Equals(other._collisionFilterInfo) && _shape.Equals(other._shape) && _position.Equals(other._position) && _orientation.Equals(other._orientation) && _mass.Equals(other._mass) && _dynamicFriction.Equals(other._dynamicFriction) && _staticFriction.Equals(other._staticFriction) && _weldingTolerance.Equals(other._weldingTolerance) && _reservedBodyId.Equals(other._reservedBodyId) && _additionFlags.Equals(other._additionFlags) && _up.Equals(other._up) && _maxSlope.Equals(other._maxSlope) && _maxForce.Equals(other._maxForce) && _maxSpeedForSimplexSolver.Equals(other._maxSpeedForSimplexSolver) && _supportDistance.Equals(other._supportDistance) && _hardSupportDistance.Equals(other._hardSupportDistance) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_collisionFilterInfo);
        code.Add(_shape);
        code.Add(_position);
        code.Add(_orientation);
        code.Add(_mass);
        code.Add(_dynamicFriction);
        code.Add(_staticFriction);
        code.Add(_weldingTolerance);
        code.Add(_reservedBodyId);
        code.Add(_additionFlags);
        code.Add(_up);
        code.Add(_maxSlope);
        code.Add(_maxForce);
        code.Add(_maxSpeedForSimplexSolver);
        code.Add(_supportDistance);
        code.Add(_hardSupportDistance);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
