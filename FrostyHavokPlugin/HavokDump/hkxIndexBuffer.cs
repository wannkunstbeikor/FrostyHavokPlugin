using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxIndexBuffer : hkReferencedObject
{
    public override uint Signature => 0;
    public hkxIndexBuffer_IndexType _indexType;
    public List<ushort> _indices16 = new();
    public List<uint> _indices32 = new();
    public uint _vertexBaseOffset;
    public uint _length;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _indexType = (hkxIndexBuffer_IndexType)br.ReadSByte();
        br.Position += 7; // padding
        _indices16 = des.ReadUInt16Array(br);
        _indices32 = des.ReadUInt32Array(br);
        _vertexBaseOffset = br.ReadUInt32();
        _length = br.ReadUInt32();
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteSByte((sbyte)_indexType);
        for (int i = 0; i < 7; i++) bw.WriteByte(0); // padding
        s.WriteUInt16Array(bw, _indices16);
        s.WriteUInt32Array(bw, _indices32);
        bw.WriteUInt32(_vertexBaseOffset);
        bw.WriteUInt32(_length);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteEnum<hkxIndexBuffer_IndexType, sbyte>(xe, nameof(_indexType), (sbyte)_indexType);
        xs.WriteNumberArray(xe, nameof(_indices16), _indices16);
        xs.WriteNumberArray(xe, nameof(_indices32), _indices32);
        xs.WriteNumber(xe, nameof(_vertexBaseOffset), _vertexBaseOffset);
        xs.WriteNumber(xe, nameof(_length), _length);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkxIndexBuffer other && base.Equals(other) && _indexType == other._indexType && _indices16.SequenceEqual(other._indices16) && _indices32.SequenceEqual(other._indices32) && _vertexBaseOffset == other._vertexBaseOffset && _length == other._length && Signature == other.Signature;
    }
    public static bool operator ==(hkxIndexBuffer? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkxIndexBuffer? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_indexType);
        code.Add(_indices16);
        code.Add(_indices32);
        code.Add(_vertexBaseOffset);
        code.Add(_length);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
