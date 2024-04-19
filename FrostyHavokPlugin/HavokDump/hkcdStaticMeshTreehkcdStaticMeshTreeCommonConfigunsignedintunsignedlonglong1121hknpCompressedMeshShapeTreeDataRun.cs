using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkcdStaticMeshTreehkcdStaticMeshTreeCommonConfigunsignedintunsignedlonglong1121hknpCompressedMeshShapeTreeDataRun : hkcdStaticMeshTreeBase
{
    public override uint Signature => 0;
    public List<uint> _packedVertices = new();
    public List<ulong> _sharedVertices = new();
    public List<hknpCompressedMeshShapeTreeDataRun?> _primitiveDataRuns = new();
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
        return obj is hkcdStaticMeshTreehkcdStaticMeshTreeCommonConfigunsignedintunsignedlonglong1121hknpCompressedMeshShapeTreeDataRun other && base.Equals(other) && _packedVertices.SequenceEqual(other._packedVertices) && _sharedVertices.SequenceEqual(other._sharedVertices) && _primitiveDataRuns.SequenceEqual(other._primitiveDataRuns) && Signature == other.Signature;
    }
    public static bool operator ==(hkcdStaticMeshTreehkcdStaticMeshTreeCommonConfigunsignedintunsignedlonglong1121hknpCompressedMeshShapeTreeDataRun? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkcdStaticMeshTreehkcdStaticMeshTreeCommonConfigunsignedintunsignedlonglong1121hknpCompressedMeshShapeTreeDataRun? a, object? b) => !(a == b);
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
