using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkMultipleVertexBufferLockedElement : IHavokObject
{
    public virtual uint Signature => 0;
    public byte _vertexBufferIndex;
    public byte _elementIndex;
    public byte _lockedBufferIndex;
    public byte _vertexFormatIndex;
    public byte _lockFlags;
    public byte _outputBufferIndex;
    public sbyte _emulatedIndex;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _vertexBufferIndex = br.ReadByte();
        _elementIndex = br.ReadByte();
        _lockedBufferIndex = br.ReadByte();
        _vertexFormatIndex = br.ReadByte();
        _lockFlags = br.ReadByte();
        _outputBufferIndex = br.ReadByte();
        _emulatedIndex = br.ReadSByte();
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        bw.WriteByte(_vertexBufferIndex);
        bw.WriteByte(_elementIndex);
        bw.WriteByte(_lockedBufferIndex);
        bw.WriteByte(_vertexFormatIndex);
        bw.WriteByte(_lockFlags);
        bw.WriteByte(_outputBufferIndex);
        bw.WriteSByte(_emulatedIndex);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumber(xe, nameof(_vertexBufferIndex), _vertexBufferIndex);
        xs.WriteNumber(xe, nameof(_elementIndex), _elementIndex);
        xs.WriteNumber(xe, nameof(_lockedBufferIndex), _lockedBufferIndex);
        xs.WriteNumber(xe, nameof(_vertexFormatIndex), _vertexFormatIndex);
        xs.WriteNumber(xe, nameof(_lockFlags), _lockFlags);
        xs.WriteNumber(xe, nameof(_outputBufferIndex), _outputBufferIndex);
        xs.WriteNumber(xe, nameof(_emulatedIndex), _emulatedIndex);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkMultipleVertexBufferLockedElement other && _vertexBufferIndex == other._vertexBufferIndex && _elementIndex == other._elementIndex && _lockedBufferIndex == other._lockedBufferIndex && _vertexFormatIndex == other._vertexFormatIndex && _lockFlags == other._lockFlags && _outputBufferIndex == other._outputBufferIndex && _emulatedIndex == other._emulatedIndex && Signature == other.Signature;
    }
    public static bool operator ==(hkMultipleVertexBufferLockedElement? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkMultipleVertexBufferLockedElement? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_vertexBufferIndex);
        code.Add(_elementIndex);
        code.Add(_lockedBufferIndex);
        code.Add(_vertexFormatIndex);
        code.Add(_lockFlags);
        code.Add(_outputBufferIndex);
        code.Add(_emulatedIndex);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
