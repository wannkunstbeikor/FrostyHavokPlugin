using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpConstraint : IHavokObject
{
    public virtual uint Signature => 0;
    public uint _bodyIdA;
    public uint _bodyIdB;
    public hkpConstraintData? _data;
    public uint _id;
    // TYPE_UINT16 TYPE_VOID _immediateId
    public hknpConstraint_FlagsEnum _flags;
    public hkpConstraintData_ConstraintType _type;
    // TYPE_POINTER TYPE_VOID _atoms
    public ushort _sizeOfAtoms;
    public ushort _sizeOfSchemas;
    public byte _numSolverResults;
    public byte _numSolverElemTemps;
    public ushort _runtimeSize;
    // TYPE_POINTER TYPE_VOID _runtime
    public ulong _userData;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _bodyIdA = br.ReadUInt32();
        _bodyIdB = br.ReadUInt32();
        _data = des.ReadClassPointer<hkpConstraintData>(br);
        _id = br.ReadUInt32();
        br.Position += 2; // padding
        _flags = (hknpConstraint_FlagsEnum)br.ReadByte();
        _type = (hkpConstraintData_ConstraintType)br.ReadByte();
        br.Position += 8; // padding
        _sizeOfAtoms = br.ReadUInt16();
        _sizeOfSchemas = br.ReadUInt16();
        _numSolverResults = br.ReadByte();
        _numSolverElemTemps = br.ReadByte();
        _runtimeSize = br.ReadUInt16();
        br.Position += 8; // padding
        _userData = br.ReadUInt64();
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        bw.WriteUInt32(_bodyIdA);
        bw.WriteUInt32(_bodyIdB);
        s.WriteClassPointer<hkpConstraintData>(bw, _data);
        bw.WriteUInt32(_id);
        for (int i = 0; i < 2; i++) bw.WriteByte(0); // padding
        bw.WriteByte((byte)_flags);
        bw.WriteByte((byte)_type);
        for (int i = 0; i < 8; i++) bw.WriteByte(0); // padding
        bw.WriteUInt16(_sizeOfAtoms);
        bw.WriteUInt16(_sizeOfSchemas);
        bw.WriteByte(_numSolverResults);
        bw.WriteByte(_numSolverElemTemps);
        bw.WriteUInt16(_runtimeSize);
        for (int i = 0; i < 8; i++) bw.WriteByte(0); // padding
        bw.WriteUInt64(_userData);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumber(xe, nameof(_bodyIdA), _bodyIdA);
        xs.WriteNumber(xe, nameof(_bodyIdB), _bodyIdB);
        xs.WriteClassPointer(xe, nameof(_data), _data);
        xs.WriteNumber(xe, nameof(_id), _id);
        xs.WriteFlag<hknpConstraint_FlagsEnum, byte>(xe, nameof(_flags), (byte)_flags);
        xs.WriteEnum<hkpConstraintData_ConstraintType, byte>(xe, nameof(_type), (byte)_type);
        xs.WriteNumber(xe, nameof(_sizeOfAtoms), _sizeOfAtoms);
        xs.WriteNumber(xe, nameof(_sizeOfSchemas), _sizeOfSchemas);
        xs.WriteNumber(xe, nameof(_numSolverResults), _numSolverResults);
        xs.WriteNumber(xe, nameof(_numSolverElemTemps), _numSolverElemTemps);
        xs.WriteNumber(xe, nameof(_runtimeSize), _runtimeSize);
        xs.WriteNumber(xe, nameof(_userData), _userData);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpConstraint other && _bodyIdA == other._bodyIdA && _bodyIdB == other._bodyIdB && _data == other._data && _id == other._id && _flags == other._flags && _type == other._type && _sizeOfAtoms == other._sizeOfAtoms && _sizeOfSchemas == other._sizeOfSchemas && _numSolverResults == other._numSolverResults && _numSolverElemTemps == other._numSolverElemTemps && _runtimeSize == other._runtimeSize && _userData == other._userData && Signature == other.Signature;
    }
    public static bool operator ==(hknpConstraint? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpConstraint? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_bodyIdA);
        code.Add(_bodyIdB);
        code.Add(_data);
        code.Add(_id);
        code.Add(_flags);
        code.Add(_type);
        code.Add(_sizeOfAtoms);
        code.Add(_sizeOfSchemas);
        code.Add(_numSolverResults);
        code.Add(_numSolverElemTemps);
        code.Add(_runtimeSize);
        code.Add(_userData);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
