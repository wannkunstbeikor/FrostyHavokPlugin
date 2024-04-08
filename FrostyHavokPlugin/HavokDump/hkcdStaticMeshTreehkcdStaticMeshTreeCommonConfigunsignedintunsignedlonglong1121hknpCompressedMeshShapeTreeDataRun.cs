using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkcdStaticMeshTreehkcdStaticMeshTreeCommonConfigunsignedintunsignedlonglong1121hknpCompressedMeshShapeTreeDataRun : hkcdStaticMeshTreeBase, IEquatable<hkcdStaticMeshTreehkcdStaticMeshTreeCommonConfigunsignedintunsignedlonglong1121hknpCompressedMeshShapeTreeDataRun?>
{
    public override uint Signature => 0;
    public List<uint> _packedVertices;
    public List<ulong> _sharedVertices;
    public List<hknpCompressedMeshShapeTreeDataRun> _primitiveDataRuns;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _packedVertices = des.ReadUInt32Array(br);
        _sharedVertices = des.ReadUInt64Array(br);
        _primitiveDataRuns = des.ReadClassArray<hknpCompressedMeshShapeTreeDataRun>(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteUInt32Array(bw, _packedVertices);
        s.WriteUInt64Array(bw, _sharedVertices);
        s.WriteClassArray<hknpCompressedMeshShapeTreeDataRun>(bw, _primitiveDataRuns);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteNumberArray(xe, nameof(_packedVertices), _packedVertices);
        xs.WriteNumberArray(xe, nameof(_sharedVertices), _sharedVertices);
        xs.WriteClassArray<hknpCompressedMeshShapeTreeDataRun>(xe, nameof(_primitiveDataRuns), _primitiveDataRuns);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkcdStaticMeshTreehkcdStaticMeshTreeCommonConfigunsignedintunsignedlonglong1121hknpCompressedMeshShapeTreeDataRun);
    }
    public bool Equals(hkcdStaticMeshTreehkcdStaticMeshTreeCommonConfigunsignedintunsignedlonglong1121hknpCompressedMeshShapeTreeDataRun? other)
    {
        return other is not null && _packedVertices.Equals(other._packedVertices) && _sharedVertices.Equals(other._sharedVertices) && _primitiveDataRuns.Equals(other._primitiveDataRuns) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_packedVertices);
        code.Add(_sharedVertices);
        code.Add(_primitiveDataRuns);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
