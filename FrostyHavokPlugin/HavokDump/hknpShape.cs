using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpShape : hkReferencedObject
{
    public override uint Signature => 0;
    public hknpShape_FlagsEnum _flags;
    public byte _numShapeKeyBits;
    public hknpCollisionDispatchType_Enum _dispatchType;
    public float _convexRadius;
    public ulong _userData;
    public hkRefCountedProperties? _properties;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _flags = (hknpShape_FlagsEnum)br.ReadUInt16();
        _numShapeKeyBits = br.ReadByte();
        _dispatchType = (hknpCollisionDispatchType_Enum)br.ReadByte();
        _convexRadius = br.ReadSingle();
        _userData = br.ReadUInt64();
        _properties = des.ReadClassPointer<hkRefCountedProperties>(br);
        br.Position += 8; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteUInt16((ushort)_flags);
        bw.WriteByte(_numShapeKeyBits);
        bw.WriteByte((byte)_dispatchType);
        bw.WriteSingle(_convexRadius);
        bw.WriteUInt64(_userData);
        s.WriteClassPointer<hkRefCountedProperties>(bw, _properties);
        for (int i = 0; i < 8; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteFlag<hknpShape_FlagsEnum, ushort>(xe, nameof(_flags), (ushort)_flags);
        xs.WriteNumber(xe, nameof(_numShapeKeyBits), _numShapeKeyBits);
        xs.WriteEnum<hknpCollisionDispatchType_Enum, byte>(xe, nameof(_dispatchType), (byte)_dispatchType);
        xs.WriteFloat(xe, nameof(_convexRadius), _convexRadius);
        xs.WriteNumber(xe, nameof(_userData), _userData);
        xs.WriteClassPointer(xe, nameof(_properties), _properties);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpShape other && base.Equals(other) && _flags == other._flags && _numShapeKeyBits == other._numShapeKeyBits && _dispatchType == other._dispatchType && _convexRadius == other._convexRadius && _userData == other._userData && _properties == other._properties && Signature == other.Signature;
    }
    public static bool operator ==(hknpShape? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpShape? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_flags);
        code.Add(_numShapeKeyBits);
        code.Add(_dispatchType);
        code.Add(_convexRadius);
        code.Add(_userData);
        code.Add(_properties);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
