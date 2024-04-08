using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpConstraintCinfo : IHavokObject, IEquatable<hknpConstraintCinfo?>
{
    public virtual uint Signature => 0;
    public hkpConstraintData _constraintData;
    public uint _bodyA;
    public uint _bodyB;
    public hknpConstraint_FlagsEnum _flags;
    public int _additionalRuntimeSize;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _constraintData = des.ReadClassPointer<hkpConstraintData>(br);
        _bodyA = br.ReadUInt32();
        _bodyB = br.ReadUInt32();
        _flags = (hknpConstraint_FlagsEnum)br.ReadByte();
        br.Position += 3; // padding
        _additionalRuntimeSize = br.ReadInt32();
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteClassPointer<hkpConstraintData>(bw, _constraintData);
        bw.WriteUInt32(_bodyA);
        bw.WriteUInt32(_bodyB);
        bw.WriteByte((byte)_flags);
        for (int i = 0; i < 3; i++) bw.WriteByte(0); // padding
        bw.WriteInt32(_additionalRuntimeSize);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClassPointer(xe, nameof(_constraintData), _constraintData);
        xs.WriteNumber(xe, nameof(_bodyA), _bodyA);
        xs.WriteNumber(xe, nameof(_bodyB), _bodyB);
        xs.WriteFlag<hknpConstraint_FlagsEnum, byte>(xe, nameof(_flags), (byte)_flags);
        xs.WriteNumber(xe, nameof(_additionalRuntimeSize), _additionalRuntimeSize);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpConstraintCinfo);
    }
    public bool Equals(hknpConstraintCinfo? other)
    {
        return other is not null && _constraintData.Equals(other._constraintData) && _bodyA.Equals(other._bodyA) && _bodyB.Equals(other._bodyB) && _flags.Equals(other._flags) && _additionalRuntimeSize.Equals(other._additionalRuntimeSize) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_constraintData);
        code.Add(_bodyA);
        code.Add(_bodyB);
        code.Add(_flags);
        code.Add(_additionalRuntimeSize);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
