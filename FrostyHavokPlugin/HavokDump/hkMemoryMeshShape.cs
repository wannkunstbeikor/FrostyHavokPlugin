using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkMemoryMeshShape : hkMeshShape, IEquatable<hkMemoryMeshShape?>
{
    public override uint Signature => 0;
    public List<hkMeshSectionCinfo> _sections;
    public List<ushort> _indices16;
    public List<uint> _indices32;
    public string _name;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _sections = des.ReadClassArray<hkMeshSectionCinfo>(br);
        _indices16 = des.ReadUInt16Array(br);
        _indices32 = des.ReadUInt32Array(br);
        _name = des.ReadStringPointer(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteClassArray<hkMeshSectionCinfo>(bw, _sections);
        s.WriteUInt16Array(bw, _indices16);
        s.WriteUInt32Array(bw, _indices32);
        s.WriteStringPointer(bw, _name);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClassArray<hkMeshSectionCinfo>(xe, nameof(_sections), _sections);
        xs.WriteNumberArray(xe, nameof(_indices16), _indices16);
        xs.WriteNumberArray(xe, nameof(_indices32), _indices32);
        xs.WriteString(xe, nameof(_name), _name);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkMemoryMeshShape);
    }
    public bool Equals(hkMemoryMeshShape? other)
    {
        return other is not null && _sections.Equals(other._sections) && _indices16.Equals(other._indices16) && _indices32.Equals(other._indices32) && _name.Equals(other._name) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_sections);
        code.Add(_indices16);
        code.Add(_indices32);
        code.Add(_name);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
