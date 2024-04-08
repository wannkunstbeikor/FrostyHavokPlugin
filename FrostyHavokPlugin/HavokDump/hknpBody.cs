using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpBody : IHavokObject, IEquatable<hknpBody?>
{
    public virtual uint Signature => 0;
    public Matrix4 _transform;
    public hkAabb16 _aabb;
    public hknpShape _shape;
    public hknpBody_FlagsEnum _flags;
    public uint _collisionFilterInfo;
    public uint _id;
    public uint _nextAttachedBodyId;
    public uint _motionId;
    public ushort _materialId;
    public byte _qualityId;
    public byte _shapeSizeDiv16;
    public uint _broadPhaseId;
    public uint _indexIntoActiveListOrDeactivatedIslandId;
    public Half _radiusOfComCenteredBoundingSphere;
    public ushort _maxContactDistance;
    public ushort _maxTimDistance;
    public byte _timAngle;
    public hknpBody_SpuFlagsEnum _spuFlags;
    public short[] _motionToBodyRotation = new short[4];
    public ulong _userData;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _transform = des.ReadTransform(br);
        _aabb = new hkAabb16();
        _aabb.Read(des, br);
        _shape = des.ReadClassPointer<hknpShape>(br);
        _flags = (hknpBody_FlagsEnum)br.ReadUInt32();
        _collisionFilterInfo = br.ReadUInt32();
        _id = br.ReadUInt32();
        _nextAttachedBodyId = br.ReadUInt32();
        _motionId = br.ReadUInt32();
        _materialId = br.ReadUInt16();
        _qualityId = br.ReadByte();
        _shapeSizeDiv16 = br.ReadByte();
        _broadPhaseId = br.ReadUInt32();
        _indexIntoActiveListOrDeactivatedIslandId = br.ReadUInt32();
        _radiusOfComCenteredBoundingSphere = des.ReadHalf(br);
        _maxContactDistance = br.ReadUInt16();
        _maxTimDistance = br.ReadUInt16();
        _timAngle = br.ReadByte();
        _spuFlags = (hknpBody_SpuFlagsEnum)br.ReadByte();
        _motionToBodyRotation = des.ReadInt16CStyleArray(br, 4);
        _userData = br.ReadUInt64();
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteTransform(bw, _transform);
        _aabb.Write(s, bw);
        s.WriteClassPointer<hknpShape>(bw, _shape);
        bw.WriteUInt32((uint)_flags);
        bw.WriteUInt32(_collisionFilterInfo);
        bw.WriteUInt32(_id);
        bw.WriteUInt32(_nextAttachedBodyId);
        bw.WriteUInt32(_motionId);
        bw.WriteUInt16(_materialId);
        bw.WriteByte(_qualityId);
        bw.WriteByte(_shapeSizeDiv16);
        bw.WriteUInt32(_broadPhaseId);
        bw.WriteUInt32(_indexIntoActiveListOrDeactivatedIslandId);
        s.WriteHalf(bw, _radiusOfComCenteredBoundingSphere);
        bw.WriteUInt16(_maxContactDistance);
        bw.WriteUInt16(_maxTimDistance);
        bw.WriteByte(_timAngle);
        bw.WriteByte((byte)_spuFlags);
        s.WriteInt16CStyleArray(bw, _motionToBodyRotation);
        bw.WriteUInt64(_userData);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteTransform(xe, nameof(_transform), _transform);
        xs.WriteClass(xe, nameof(_aabb), _aabb);
        xs.WriteClassPointer(xe, nameof(_shape), _shape);
        xs.WriteFlag<hknpBody_FlagsEnum, uint>(xe, nameof(_flags), (uint)_flags);
        xs.WriteNumber(xe, nameof(_collisionFilterInfo), _collisionFilterInfo);
        xs.WriteNumber(xe, nameof(_id), _id);
        xs.WriteNumber(xe, nameof(_nextAttachedBodyId), _nextAttachedBodyId);
        xs.WriteNumber(xe, nameof(_motionId), _motionId);
        xs.WriteNumber(xe, nameof(_materialId), _materialId);
        xs.WriteNumber(xe, nameof(_qualityId), _qualityId);
        xs.WriteNumber(xe, nameof(_shapeSizeDiv16), _shapeSizeDiv16);
        xs.WriteNumber(xe, nameof(_broadPhaseId), _broadPhaseId);
        xs.WriteNumber(xe, nameof(_indexIntoActiveListOrDeactivatedIslandId), _indexIntoActiveListOrDeactivatedIslandId);
        xs.WriteFloat(xe, nameof(_radiusOfComCenteredBoundingSphere), _radiusOfComCenteredBoundingSphere);
        xs.WriteNumber(xe, nameof(_maxContactDistance), _maxContactDistance);
        xs.WriteNumber(xe, nameof(_maxTimDistance), _maxTimDistance);
        xs.WriteNumber(xe, nameof(_timAngle), _timAngle);
        xs.WriteFlag<hknpBody_SpuFlagsEnum, byte>(xe, nameof(_spuFlags), (byte)_spuFlags);
        xs.WriteNumberArray(xe, nameof(_motionToBodyRotation), _motionToBodyRotation);
        xs.WriteNumber(xe, nameof(_userData), _userData);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpBody);
    }
    public bool Equals(hknpBody? other)
    {
        return other is not null && _transform.Equals(other._transform) && _aabb.Equals(other._aabb) && _shape.Equals(other._shape) && _flags.Equals(other._flags) && _collisionFilterInfo.Equals(other._collisionFilterInfo) && _id.Equals(other._id) && _nextAttachedBodyId.Equals(other._nextAttachedBodyId) && _motionId.Equals(other._motionId) && _materialId.Equals(other._materialId) && _qualityId.Equals(other._qualityId) && _shapeSizeDiv16.Equals(other._shapeSizeDiv16) && _broadPhaseId.Equals(other._broadPhaseId) && _indexIntoActiveListOrDeactivatedIslandId.Equals(other._indexIntoActiveListOrDeactivatedIslandId) && _radiusOfComCenteredBoundingSphere.Equals(other._radiusOfComCenteredBoundingSphere) && _maxContactDistance.Equals(other._maxContactDistance) && _maxTimDistance.Equals(other._maxTimDistance) && _timAngle.Equals(other._timAngle) && _spuFlags.Equals(other._spuFlags) && _motionToBodyRotation.Equals(other._motionToBodyRotation) && _userData.Equals(other._userData) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_transform);
        code.Add(_aabb);
        code.Add(_shape);
        code.Add(_flags);
        code.Add(_collisionFilterInfo);
        code.Add(_id);
        code.Add(_nextAttachedBodyId);
        code.Add(_motionId);
        code.Add(_materialId);
        code.Add(_qualityId);
        code.Add(_shapeSizeDiv16);
        code.Add(_broadPhaseId);
        code.Add(_indexIntoActiveListOrDeactivatedIslandId);
        code.Add(_radiusOfComCenteredBoundingSphere);
        code.Add(_maxContactDistance);
        code.Add(_maxTimDistance);
        code.Add(_timAngle);
        code.Add(_spuFlags);
        code.Add(_motionToBodyRotation);
        code.Add(_userData);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
