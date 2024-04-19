using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpConvexPolytopeShapeFace : IHavokObject
{
    public virtual uint Signature => 0;
    public byte _minHalfAngle;
    public byte _numIndices;
    public ushort _firstIndex;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _minHalfAngle = br.ReadByte();
        _numIndices = br.ReadByte();
        _firstIndex = br.ReadUInt16();
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        bw.WriteByte(_minHalfAngle);
        bw.WriteByte(_numIndices);
        bw.WriteUInt16(_firstIndex);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumber(xe, nameof(_minHalfAngle), _minHalfAngle);
        xs.WriteNumber(xe, nameof(_numIndices), _numIndices);
        xs.WriteNumber(xe, nameof(_firstIndex), _firstIndex);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpConvexPolytopeShapeFace other && _minHalfAngle == other._minHalfAngle && _numIndices == other._numIndices && _firstIndex == other._firstIndex && Signature == other.Signature;
    }
    public static bool operator ==(hknpConvexPolytopeShapeFace? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpConvexPolytopeShapeFace? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_minHalfAngle);
        code.Add(_numIndices);
        code.Add(_firstIndex);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
