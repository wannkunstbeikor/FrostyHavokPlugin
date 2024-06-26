using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkIndexedTransformSet : hkReferencedObject
{
    public override uint Signature => 0;
    public List<Matrix4> _matrices = new();
    public List<Matrix4> _inverseMatrices = new();
    public List<short> _matricesOrder = new();
    public List<string> _matricesNames = new();
    public List<hkMeshBoneIndexMapping?> _indexMappings = new();
    public bool _allMatricesAreAffine;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _matrices = des.ReadMatrix4Array(br);
        _inverseMatrices = des.ReadMatrix4Array(br);
        _matricesOrder = des.ReadInt16Array(br);
        _matricesNames = des.ReadStringPointerArray(br);
        _indexMappings = des.ReadClassArray<hkMeshBoneIndexMapping>(br);
        _allMatricesAreAffine = br.ReadBoolean();
        br.Position += 7; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteMatrix4Array(bw, _matrices);
        s.WriteMatrix4Array(bw, _inverseMatrices);
        s.WriteInt16Array(bw, _matricesOrder);
        s.WriteStringPointerArray(bw, _matricesNames);
        s.WriteClassArray<hkMeshBoneIndexMapping>(bw, _indexMappings);
        bw.WriteBoolean(_allMatricesAreAffine);
        for (int i = 0; i < 7; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteMatrix4Array(xe, nameof(_matrices), _matrices);
        xs.WriteMatrix4Array(xe, nameof(_inverseMatrices), _inverseMatrices);
        xs.WriteNumberArray(xe, nameof(_matricesOrder), _matricesOrder);
        xs.WriteStringArray(xe, nameof(_matricesNames), _matricesNames);
        xs.WriteClassArray<hkMeshBoneIndexMapping>(xe, nameof(_indexMappings), _indexMappings);
        xs.WriteBoolean(xe, nameof(_allMatricesAreAffine), _allMatricesAreAffine);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkIndexedTransformSet other && base.Equals(other) && _matrices.SequenceEqual(other._matrices) && _inverseMatrices.SequenceEqual(other._inverseMatrices) && _matricesOrder.SequenceEqual(other._matricesOrder) && _matricesNames.SequenceEqual(other._matricesNames) && _indexMappings.SequenceEqual(other._indexMappings) && _allMatricesAreAffine == other._allMatricesAreAffine && Signature == other.Signature;
    }
    public static bool operator ==(hkIndexedTransformSet? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkIndexedTransformSet? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_matrices);
        code.Add(_inverseMatrices);
        code.Add(_matricesOrder);
        code.Add(_matricesNames);
        code.Add(_indexMappings);
        code.Add(_allMatricesAreAffine);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
