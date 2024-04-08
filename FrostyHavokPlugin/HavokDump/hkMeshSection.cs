using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkMeshSection : IHavokObject, IEquatable<hkMeshSection?>
{
    public virtual uint Signature => 0;
    public hkMeshSection_PrimitiveType _primitiveType;
    public int _numPrimitives;
    public int _numIndices;
    public int _vertexStartIndex;
    public int _transformIndex;
    public hkMeshSection_MeshSectionIndexType _indexType;
    // TYPE_POINTER TYPE_VOID _indices
    public hkMeshVertexBuffer _vertexBuffer;
    public hkMeshMaterial _material;
    public int _sectionIndex;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _primitiveType = (hkMeshSection_PrimitiveType)br.ReadByte();
        br.Position += 3; // padding
        _numPrimitives = br.ReadInt32();
        _numIndices = br.ReadInt32();
        _vertexStartIndex = br.ReadInt32();
        _transformIndex = br.ReadInt32();
        _indexType = (hkMeshSection_MeshSectionIndexType)br.ReadByte();
        br.Position += 11; // padding
        _vertexBuffer = des.ReadClassPointer<hkMeshVertexBuffer>(br);
        _material = des.ReadClassPointer<hkMeshMaterial>(br);
        _sectionIndex = br.ReadInt32();
        br.Position += 4; // padding
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        bw.WriteByte((byte)_primitiveType);
        for (int i = 0; i < 3; i++) bw.WriteByte(0); // padding
        bw.WriteInt32(_numPrimitives);
        bw.WriteInt32(_numIndices);
        bw.WriteInt32(_vertexStartIndex);
        bw.WriteInt32(_transformIndex);
        bw.WriteByte((byte)_indexType);
        for (int i = 0; i < 11; i++) bw.WriteByte(0); // padding
        s.WriteClassPointer<hkMeshVertexBuffer>(bw, _vertexBuffer);
        s.WriteClassPointer<hkMeshMaterial>(bw, _material);
        bw.WriteInt32(_sectionIndex);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteEnum<hkMeshSection_PrimitiveType, byte>(xe, nameof(_primitiveType), (byte)_primitiveType);
        xs.WriteNumber(xe, nameof(_numPrimitives), _numPrimitives);
        xs.WriteNumber(xe, nameof(_numIndices), _numIndices);
        xs.WriteNumber(xe, nameof(_vertexStartIndex), _vertexStartIndex);
        xs.WriteNumber(xe, nameof(_transformIndex), _transformIndex);
        xs.WriteEnum<hkMeshSection_MeshSectionIndexType, byte>(xe, nameof(_indexType), (byte)_indexType);
        xs.WriteClassPointer(xe, nameof(_vertexBuffer), _vertexBuffer);
        xs.WriteClassPointer(xe, nameof(_material), _material);
        xs.WriteNumber(xe, nameof(_sectionIndex), _sectionIndex);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkMeshSection);
    }
    public bool Equals(hkMeshSection? other)
    {
        return other is not null && _primitiveType.Equals(other._primitiveType) && _numPrimitives.Equals(other._numPrimitives) && _numIndices.Equals(other._numIndices) && _vertexStartIndex.Equals(other._vertexStartIndex) && _transformIndex.Equals(other._transformIndex) && _indexType.Equals(other._indexType) && _vertexBuffer.Equals(other._vertexBuffer) && _material.Equals(other._material) && _sectionIndex.Equals(other._sectionIndex) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_primitiveType);
        code.Add(_numPrimitives);
        code.Add(_numIndices);
        code.Add(_vertexStartIndex);
        code.Add(_transformIndex);
        code.Add(_indexType);
        code.Add(_vertexBuffer);
        code.Add(_material);
        code.Add(_sectionIndex);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
