using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkAabbUint32 : IHavokObject
{
    public virtual uint Signature => 0;
    public uint[] _min = new uint[3];
    public byte[] _expansionMin = new byte[3];
    public byte _expansionShift;
    public uint[] _max = new uint[3];
    public byte[] _expansionMax = new byte[3];
    public byte _shapeKeyByte;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _min = des.ReadUInt32CStyleArray(br, 3);
        _expansionMin = des.ReadByteCStyleArray(br, 3);
        _expansionShift = br.ReadByte();
        _max = des.ReadUInt32CStyleArray(br, 3);
        _expansionMax = des.ReadByteCStyleArray(br, 3);
        _shapeKeyByte = br.ReadByte();
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteUInt32CStyleArray(bw, _min);
        s.WriteByteCStyleArray(bw, _expansionMin);
        bw.WriteByte(_expansionShift);
        s.WriteUInt32CStyleArray(bw, _max);
        s.WriteByteCStyleArray(bw, _expansionMax);
        bw.WriteByte(_shapeKeyByte);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumberArray(xe, nameof(_min), _min);
        xs.WriteNumberArray(xe, nameof(_expansionMin), _expansionMin);
        xs.WriteNumber(xe, nameof(_expansionShift), _expansionShift);
        xs.WriteNumberArray(xe, nameof(_max), _max);
        xs.WriteNumberArray(xe, nameof(_expansionMax), _expansionMax);
        xs.WriteNumber(xe, nameof(_shapeKeyByte), _shapeKeyByte);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkAabbUint32 other && _min == other._min && _expansionMin == other._expansionMin && _expansionShift == other._expansionShift && _max == other._max && _expansionMax == other._expansionMax && _shapeKeyByte == other._shapeKeyByte && Signature == other.Signature;
    }
    public static bool operator ==(hkAabbUint32? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkAabbUint32? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_min);
        code.Add(_expansionMin);
        code.Add(_expansionShift);
        code.Add(_max);
        code.Add(_expansionMax);
        code.Add(_shapeKeyByte);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
