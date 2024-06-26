using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkcdStaticMeshTreeBase : hkcdStaticTreeTreehkcdStaticTreeDynamicStorage5
{
    public override uint Signature => 0;
    public int _numPrimitiveKeys;
    public int _bitsPerKey;
    public uint _maxKeyValue;
    public List<hkcdStaticMeshTreeBaseSection?> _sections = new();
    public List<hkcdStaticMeshTreeBasePrimitive?> _primitives = new();
    public List<ushort> _sharedVerticesIndex = new();
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _numPrimitiveKeys = br.ReadInt32();
        _bitsPerKey = br.ReadInt32();
        _maxKeyValue = br.ReadUInt32();
        br.Position += 4; // padding
        _sections = des.ReadClassArray<hkcdStaticMeshTreeBaseSection>(br);
        _primitives = des.ReadClassArray<hkcdStaticMeshTreeBasePrimitive>(br);
        _sharedVerticesIndex = des.ReadUInt16Array(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteInt32(_numPrimitiveKeys);
        bw.WriteInt32(_bitsPerKey);
        bw.WriteUInt32(_maxKeyValue);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
        s.WriteClassArray<hkcdStaticMeshTreeBaseSection>(bw, _sections);
        s.WriteClassArray<hkcdStaticMeshTreeBasePrimitive>(bw, _primitives);
        s.WriteUInt16Array(bw, _sharedVerticesIndex);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteNumber(xe, nameof(_numPrimitiveKeys), _numPrimitiveKeys);
        xs.WriteNumber(xe, nameof(_bitsPerKey), _bitsPerKey);
        xs.WriteNumber(xe, nameof(_maxKeyValue), _maxKeyValue);
        xs.WriteClassArray<hkcdStaticMeshTreeBaseSection>(xe, nameof(_sections), _sections);
        xs.WriteClassArray<hkcdStaticMeshTreeBasePrimitive>(xe, nameof(_primitives), _primitives);
        xs.WriteNumberArray(xe, nameof(_sharedVerticesIndex), _sharedVerticesIndex);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkcdStaticMeshTreeBase other && base.Equals(other) && _numPrimitiveKeys == other._numPrimitiveKeys && _bitsPerKey == other._bitsPerKey && _maxKeyValue == other._maxKeyValue && _sections.SequenceEqual(other._sections) && _primitives.SequenceEqual(other._primitives) && _sharedVerticesIndex.SequenceEqual(other._sharedVerticesIndex) && Signature == other.Signature;
    }
    public static bool operator ==(hkcdStaticMeshTreeBase? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkcdStaticMeshTreeBase? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_numPrimitiveKeys);
        code.Add(_bitsPerKey);
        code.Add(_maxKeyValue);
        code.Add(_sections);
        code.Add(_primitives);
        code.Add(_sharedVerticesIndex);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
