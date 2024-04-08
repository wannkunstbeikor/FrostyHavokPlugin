using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkSkinnedMeshShapePart : IHavokObject, IEquatable<hkSkinnedMeshShapePart?>
{
    public virtual uint Signature => 0;
    public int _startVertex;
    public int _numVertices;
    public int _startIndex;
    public int _numIndices;
    public ushort _boneSetId;
    public ushort _meshSectionIndex;
    public Vector4 _boundingSphere;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _startVertex = br.ReadInt32();
        _numVertices = br.ReadInt32();
        _startIndex = br.ReadInt32();
        _numIndices = br.ReadInt32();
        _boneSetId = br.ReadUInt16();
        _meshSectionIndex = br.ReadUInt16();
        br.Position += 12; // padding
        _boundingSphere = des.ReadVector4(br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        bw.WriteInt32(_startVertex);
        bw.WriteInt32(_numVertices);
        bw.WriteInt32(_startIndex);
        bw.WriteInt32(_numIndices);
        bw.WriteUInt16(_boneSetId);
        bw.WriteUInt16(_meshSectionIndex);
        for (int i = 0; i < 12; i++) bw.WriteByte(0); // padding
        s.WriteVector4(bw, _boundingSphere);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumber(xe, nameof(_startVertex), _startVertex);
        xs.WriteNumber(xe, nameof(_numVertices), _numVertices);
        xs.WriteNumber(xe, nameof(_startIndex), _startIndex);
        xs.WriteNumber(xe, nameof(_numIndices), _numIndices);
        xs.WriteNumber(xe, nameof(_boneSetId), _boneSetId);
        xs.WriteNumber(xe, nameof(_meshSectionIndex), _meshSectionIndex);
        xs.WriteVector4(xe, nameof(_boundingSphere), _boundingSphere);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkSkinnedMeshShapePart);
    }
    public bool Equals(hkSkinnedMeshShapePart? other)
    {
        return other is not null && _startVertex.Equals(other._startVertex) && _numVertices.Equals(other._numVertices) && _startIndex.Equals(other._startIndex) && _numIndices.Equals(other._numIndices) && _boneSetId.Equals(other._boneSetId) && _meshSectionIndex.Equals(other._meshSectionIndex) && _boundingSphere.Equals(other._boundingSphere) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_startVertex);
        code.Add(_numVertices);
        code.Add(_startIndex);
        code.Add(_numIndices);
        code.Add(_boneSetId);
        code.Add(_meshSectionIndex);
        code.Add(_boundingSphere);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
