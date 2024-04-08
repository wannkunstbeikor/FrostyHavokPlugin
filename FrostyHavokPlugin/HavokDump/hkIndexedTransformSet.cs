using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkIndexedTransformSet : hkReferencedObject, IEquatable<hkIndexedTransformSet?>
{
    public override uint Signature => 0;
    public List<Matrix4> _matrices;
    public List<Matrix4> _inverseMatrices;
    public List<short> _matricesOrder;
    public List<string> _matricesNames;
    public List<hkMeshBoneIndexMapping> _indexMappings;
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
        return Equals(obj as hkIndexedTransformSet);
    }
    public bool Equals(hkIndexedTransformSet? other)
    {
        return other is not null && _matrices.Equals(other._matrices) && _inverseMatrices.Equals(other._inverseMatrices) && _matricesOrder.Equals(other._matricesOrder) && _matricesNames.Equals(other._matricesNames) && _indexMappings.Equals(other._indexMappings) && _allMatricesAreAffine.Equals(other._allMatricesAreAffine) && Signature == other.Signature;
    }
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
