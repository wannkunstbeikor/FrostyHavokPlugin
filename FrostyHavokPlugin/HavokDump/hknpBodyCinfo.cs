using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpBodyCinfo : IHavokObject, IEquatable<hknpBodyCinfo?>
{
    public virtual uint Signature => 0;
    public hknpShape _shape;
    public uint _reservedBodyId;
    public uint _motionId;
    public byte _qualityId;
    public ushort _materialId;
    public uint _collisionFilterInfo;
    public hknpBody_FlagsEnum _flags;
    public float _collisionLookAheadDistance;
    public string _name;
    public Vector4 _position;
    public Quaternion _orientation;
    public hknpBody_SpuFlagsEnum _spuFlags;
    public hkLocalFrame _localFrame;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _shape = des.ReadClassPointer<hknpShape>(br);
        _reservedBodyId = br.ReadUInt32();
        _motionId = br.ReadUInt32();
        _qualityId = br.ReadByte();
        br.Position += 1; // padding
        _materialId = br.ReadUInt16();
        _collisionFilterInfo = br.ReadUInt32();
        _flags = (hknpBody_FlagsEnum)br.ReadUInt32();
        _collisionLookAheadDistance = br.ReadSingle();
        _name = des.ReadStringPointer(br);
        br.Position += 8; // padding
        _position = des.ReadVector4(br);
        _orientation = des.ReadQuaternion(br);
        _spuFlags = (hknpBody_SpuFlagsEnum)br.ReadByte();
        br.Position += 7; // padding
        _localFrame = des.ReadClassPointer<hkLocalFrame>(br);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClassPointer(xe, nameof(_shape), _shape);
        xs.WriteNumber(xe, nameof(_reservedBodyId), _reservedBodyId);
        xs.WriteNumber(xe, nameof(_motionId), _motionId);
        xs.WriteNumber(xe, nameof(_qualityId), _qualityId);
        xs.WriteNumber(xe, nameof(_materialId), _materialId);
        xs.WriteNumber(xe, nameof(_collisionFilterInfo), _collisionFilterInfo);
        xs.WriteFlag<hknpBody_FlagsEnum, uint>(xe, nameof(_flags), (uint)_flags);
        xs.WriteFloat(xe, nameof(_collisionLookAheadDistance), _collisionLookAheadDistance);
        xs.WriteString(xe, nameof(_name), _name);
        xs.WriteVector4(xe, nameof(_position), _position);
        xs.WriteQuaternion(xe, nameof(_orientation), _orientation);
        xs.WriteFlag<hknpBody_SpuFlagsEnum, byte>(xe, nameof(_spuFlags), (byte)_spuFlags);
        xs.WriteClassPointer(xe, nameof(_localFrame), _localFrame);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpBodyCinfo);
    }
    public bool Equals(hknpBodyCinfo? other)
    {
        return other is not null && _shape.Equals(other._shape) && _reservedBodyId.Equals(other._reservedBodyId) && _motionId.Equals(other._motionId) && _qualityId.Equals(other._qualityId) && _materialId.Equals(other._materialId) && _collisionFilterInfo.Equals(other._collisionFilterInfo) && _flags.Equals(other._flags) && _collisionLookAheadDistance.Equals(other._collisionLookAheadDistance) && _name.Equals(other._name) && _position.Equals(other._position) && _orientation.Equals(other._orientation) && _spuFlags.Equals(other._spuFlags) && _localFrame.Equals(other._localFrame) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_shape);
        code.Add(_reservedBodyId);
        code.Add(_motionId);
        code.Add(_qualityId);
        code.Add(_materialId);
        code.Add(_collisionFilterInfo);
        code.Add(_flags);
        code.Add(_collisionLookAheadDistance);
        code.Add(_name);
        code.Add(_position);
        code.Add(_orientation);
        code.Add(_spuFlags);
        code.Add(_localFrame);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
