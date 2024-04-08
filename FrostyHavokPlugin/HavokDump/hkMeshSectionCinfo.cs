using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkMeshSectionCinfo : IHavokObject, IEquatable<hkMeshSectionCinfo?>
{
    public virtual uint Signature => 0;
    public hkMeshVertexBuffer _vertexBuffer;
    public hkMeshMaterial _material;
    public hkMeshSection_PrimitiveType _primitiveType;
    public int _numPrimitives;
    public hkMeshSection_MeshSectionIndexType _indexType;
    // TYPE_POINTER TYPE_VOID _indices
    public int _vertexStartIndex;
    public int _transformIndex;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _vertexBuffer = des.ReadClassPointer<hkMeshVertexBuffer>(br);
        _material = des.ReadClassPointer<hkMeshMaterial>(br);
        _primitiveType = (hkMeshSection_PrimitiveType)br.ReadByte();
        br.Position += 3; // padding
        _numPrimitives = br.ReadInt32();
        _indexType = (hkMeshSection_MeshSectionIndexType)br.ReadByte();
        br.Position += 15; // padding
        _vertexStartIndex = br.ReadInt32();
        _transformIndex = br.ReadInt32();
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteClassPointer<hkMeshVertexBuffer>(bw, _vertexBuffer);
        s.WriteClassPointer<hkMeshMaterial>(bw, _material);
        bw.WriteByte((byte)_primitiveType);
        for (int i = 0; i < 3; i++) bw.WriteByte(0); // padding
        bw.WriteInt32(_numPrimitives);
        bw.WriteByte((byte)_indexType);
        for (int i = 0; i < 15; i++) bw.WriteByte(0); // padding
        bw.WriteInt32(_vertexStartIndex);
        bw.WriteInt32(_transformIndex);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClassPointer(xe, nameof(_vertexBuffer), _vertexBuffer);
        xs.WriteClassPointer(xe, nameof(_material), _material);
        xs.WriteEnum<hkMeshSection_PrimitiveType, byte>(xe, nameof(_primitiveType), (byte)_primitiveType);
        xs.WriteNumber(xe, nameof(_numPrimitives), _numPrimitives);
        xs.WriteEnum<hkMeshSection_MeshSectionIndexType, byte>(xe, nameof(_indexType), (byte)_indexType);
        xs.WriteNumber(xe, nameof(_vertexStartIndex), _vertexStartIndex);
        xs.WriteNumber(xe, nameof(_transformIndex), _transformIndex);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkMeshSectionCinfo);
    }
    public bool Equals(hkMeshSectionCinfo? other)
    {
        return other is not null && _vertexBuffer.Equals(other._vertexBuffer) && _material.Equals(other._material) && _primitiveType.Equals(other._primitiveType) && _numPrimitives.Equals(other._numPrimitives) && _indexType.Equals(other._indexType) && _vertexStartIndex.Equals(other._vertexStartIndex) && _transformIndex.Equals(other._transformIndex) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_vertexBuffer);
        code.Add(_material);
        code.Add(_primitiveType);
        code.Add(_numPrimitives);
        code.Add(_indexType);
        code.Add(_vertexStartIndex);
        code.Add(_transformIndex);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
