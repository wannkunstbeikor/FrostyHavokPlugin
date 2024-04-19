using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkMemoryMeshVertexBuffer : hkMeshVertexBuffer
{
    public override uint Signature => 0;
    public hkVertexFormat? _format;
    public int[] _elementOffsets = new int[32];
    public List<byte> _memory = new();
    public int _vertexStride;
    public bool _locked;
    public int _numVertices;
    public bool _isBigEndian;
    public bool _isSharable;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _format = new hkVertexFormat();
        _format.Read(des, br);
        _elementOffsets = des.ReadInt32CStyleArray(br, 32);
        br.Position += 4; // padding
        _memory = des.ReadByteArray(br);
        _vertexStride = br.ReadInt32();
        _locked = br.ReadBoolean();
        br.Position += 3; // padding
        _numVertices = br.ReadInt32();
        _isBigEndian = br.ReadBoolean();
        _isSharable = br.ReadBoolean();
        br.Position += 2; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        _format.Write(s, bw);
        s.WriteInt32CStyleArray(bw, _elementOffsets);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
        s.WriteByteArray(bw, _memory);
        bw.WriteInt32(_vertexStride);
        bw.WriteBoolean(_locked);
        for (int i = 0; i < 3; i++) bw.WriteByte(0); // padding
        bw.WriteInt32(_numVertices);
        bw.WriteBoolean(_isBigEndian);
        bw.WriteBoolean(_isSharable);
        for (int i = 0; i < 2; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClass(xe, nameof(_format), _format);
        xs.WriteNumberArray(xe, nameof(_elementOffsets), _elementOffsets);
        xs.WriteNumberArray(xe, nameof(_memory), _memory);
        xs.WriteNumber(xe, nameof(_vertexStride), _vertexStride);
        xs.WriteBoolean(xe, nameof(_locked), _locked);
        xs.WriteNumber(xe, nameof(_numVertices), _numVertices);
        xs.WriteBoolean(xe, nameof(_isBigEndian), _isBigEndian);
        xs.WriteBoolean(xe, nameof(_isSharable), _isSharable);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkMemoryMeshVertexBuffer other && base.Equals(other) && _format == other._format && _elementOffsets == other._elementOffsets && _memory.SequenceEqual(other._memory) && _vertexStride == other._vertexStride && _locked == other._locked && _numVertices == other._numVertices && _isBigEndian == other._isBigEndian && _isSharable == other._isSharable && Signature == other.Signature;
    }
    public static bool operator ==(hkMemoryMeshVertexBuffer? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkMemoryMeshVertexBuffer? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_format);
        code.Add(_elementOffsets);
        code.Add(_memory);
        code.Add(_vertexStride);
        code.Add(_locked);
        code.Add(_numVertices);
        code.Add(_isBigEndian);
        code.Add(_isSharable);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
