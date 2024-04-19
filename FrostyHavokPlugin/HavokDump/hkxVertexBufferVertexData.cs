using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxVertexBufferVertexData : IHavokObject
{
    public virtual uint Signature => 0;
    public List<float> _vectorData = new();
    public List<float> _floatData = new();
    public List<uint> _uint32Data = new();
    public List<ushort> _uint16Data = new();
    public List<byte> _uint8Data = new();
    public uint _numVerts;
    public uint _vectorStride;
    public uint _floatStride;
    public uint _uint32Stride;
    public uint _uint16Stride;
    public uint _uint8Stride;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _vectorData = des.ReadSingleArray(br);
        _floatData = des.ReadSingleArray(br);
        _uint32Data = des.ReadUInt32Array(br);
        _uint16Data = des.ReadUInt16Array(br);
        _uint8Data = des.ReadByteArray(br);
        _numVerts = br.ReadUInt32();
        _vectorStride = br.ReadUInt32();
        _floatStride = br.ReadUInt32();
        _uint32Stride = br.ReadUInt32();
        _uint16Stride = br.ReadUInt32();
        _uint8Stride = br.ReadUInt32();
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteSingleArray(bw, _vectorData);
        s.WriteSingleArray(bw, _floatData);
        s.WriteUInt32Array(bw, _uint32Data);
        s.WriteUInt16Array(bw, _uint16Data);
        s.WriteByteArray(bw, _uint8Data);
        bw.WriteUInt32(_numVerts);
        bw.WriteUInt32(_vectorStride);
        bw.WriteUInt32(_floatStride);
        bw.WriteUInt32(_uint32Stride);
        bw.WriteUInt32(_uint16Stride);
        bw.WriteUInt32(_uint8Stride);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteFloatArray(xe, nameof(_vectorData), _vectorData);
        xs.WriteFloatArray(xe, nameof(_floatData), _floatData);
        xs.WriteNumberArray(xe, nameof(_uint32Data), _uint32Data);
        xs.WriteNumberArray(xe, nameof(_uint16Data), _uint16Data);
        xs.WriteNumberArray(xe, nameof(_uint8Data), _uint8Data);
        xs.WriteNumber(xe, nameof(_numVerts), _numVerts);
        xs.WriteNumber(xe, nameof(_vectorStride), _vectorStride);
        xs.WriteNumber(xe, nameof(_floatStride), _floatStride);
        xs.WriteNumber(xe, nameof(_uint32Stride), _uint32Stride);
        xs.WriteNumber(xe, nameof(_uint16Stride), _uint16Stride);
        xs.WriteNumber(xe, nameof(_uint8Stride), _uint8Stride);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkxVertexBufferVertexData other && _vectorData.SequenceEqual(other._vectorData) && _floatData.SequenceEqual(other._floatData) && _uint32Data.SequenceEqual(other._uint32Data) && _uint16Data.SequenceEqual(other._uint16Data) && _uint8Data.SequenceEqual(other._uint8Data) && _numVerts == other._numVerts && _vectorStride == other._vectorStride && _floatStride == other._floatStride && _uint32Stride == other._uint32Stride && _uint16Stride == other._uint16Stride && _uint8Stride == other._uint8Stride && Signature == other.Signature;
    }
    public static bool operator ==(hkxVertexBufferVertexData? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkxVertexBufferVertexData? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_vectorData);
        code.Add(_floatData);
        code.Add(_uint32Data);
        code.Add(_uint16Data);
        code.Add(_uint8Data);
        code.Add(_numVerts);
        code.Add(_vectorStride);
        code.Add(_floatStride);
        code.Add(_uint32Stride);
        code.Add(_uint16Stride);
        code.Add(_uint8Stride);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
