using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkcdStaticMeshTreeBaseSection : hkcdStaticTreeTreehkcdStaticTreeDynamicStorage4, IEquatable<hkcdStaticMeshTreeBaseSection?>
{
    public override uint Signature => 0;
    public float[] _codecParms = new float[6];
    public uint _firstPackedVertex;
    public hkcdStaticMeshTreeBaseSectionSharedVertices _sharedVertices;
    public hkcdStaticMeshTreeBaseSectionPrimitives _primitives;
    public hkcdStaticMeshTreeBaseSectionDataRuns _dataRuns;
    public byte _numPackedVertices;
    public byte _numSharedIndices;
    public ushort _leafIndex;
    public byte _page;
    public byte _flags;
    public byte _layerData;
    public byte _unusedData;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _codecParms = des.ReadSingleCStyleArray(br, 6);
        _firstPackedVertex = br.ReadUInt32();
        _sharedVertices = new hkcdStaticMeshTreeBaseSectionSharedVertices();
        _sharedVertices.Read(des, br);
        _primitives = new hkcdStaticMeshTreeBaseSectionPrimitives();
        _primitives.Read(des, br);
        _dataRuns = new hkcdStaticMeshTreeBaseSectionDataRuns();
        _dataRuns.Read(des, br);
        _numPackedVertices = br.ReadByte();
        _numSharedIndices = br.ReadByte();
        _leafIndex = br.ReadUInt16();
        _page = br.ReadByte();
        _flags = br.ReadByte();
        _layerData = br.ReadByte();
        _unusedData = br.ReadByte();
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteSingleCStyleArray(bw, _codecParms);
        bw.WriteUInt32(_firstPackedVertex);
        _sharedVertices.Write(s, bw);
        _primitives.Write(s, bw);
        _dataRuns.Write(s, bw);
        bw.WriteByte(_numPackedVertices);
        bw.WriteByte(_numSharedIndices);
        bw.WriteUInt16(_leafIndex);
        bw.WriteByte(_page);
        bw.WriteByte(_flags);
        bw.WriteByte(_layerData);
        bw.WriteByte(_unusedData);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteFloatArray(xe, nameof(_codecParms), _codecParms);
        xs.WriteNumber(xe, nameof(_firstPackedVertex), _firstPackedVertex);
        xs.WriteClass(xe, nameof(_sharedVertices), _sharedVertices);
        xs.WriteClass(xe, nameof(_primitives), _primitives);
        xs.WriteClass(xe, nameof(_dataRuns), _dataRuns);
        xs.WriteNumber(xe, nameof(_numPackedVertices), _numPackedVertices);
        xs.WriteNumber(xe, nameof(_numSharedIndices), _numSharedIndices);
        xs.WriteNumber(xe, nameof(_leafIndex), _leafIndex);
        xs.WriteNumber(xe, nameof(_page), _page);
        xs.WriteNumber(xe, nameof(_flags), _flags);
        xs.WriteNumber(xe, nameof(_layerData), _layerData);
        xs.WriteNumber(xe, nameof(_unusedData), _unusedData);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkcdStaticMeshTreeBaseSection);
    }
    public bool Equals(hkcdStaticMeshTreeBaseSection? other)
    {
        return other is not null && _codecParms.Equals(other._codecParms) && _firstPackedVertex.Equals(other._firstPackedVertex) && _sharedVertices.Equals(other._sharedVertices) && _primitives.Equals(other._primitives) && _dataRuns.Equals(other._dataRuns) && _numPackedVertices.Equals(other._numPackedVertices) && _numSharedIndices.Equals(other._numSharedIndices) && _leafIndex.Equals(other._leafIndex) && _page.Equals(other._page) && _flags.Equals(other._flags) && _layerData.Equals(other._layerData) && _unusedData.Equals(other._unusedData) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_codecParms);
        code.Add(_firstPackedVertex);
        code.Add(_sharedVertices);
        code.Add(_primitives);
        code.Add(_dataRuns);
        code.Add(_numPackedVertices);
        code.Add(_numSharedIndices);
        code.Add(_leafIndex);
        code.Add(_page);
        code.Add(_flags);
        code.Add(_layerData);
        code.Add(_unusedData);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
