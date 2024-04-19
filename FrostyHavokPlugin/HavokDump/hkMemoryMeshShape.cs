using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkMemoryMeshShape : hkMeshShape
{
    public override uint Signature => 0;
    public List<hkMeshSectionCinfo?> _sections = new();
    public List<ushort> _indices16 = new();
    public List<uint> _indices32 = new();
    public string _name = string.Empty;
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
        return obj is hkMemoryMeshShape other && base.Equals(other) && _sections.SequenceEqual(other._sections) && _indices16.SequenceEqual(other._indices16) && _indices32.SequenceEqual(other._indices32) && _name == other._name && Signature == other.Signature;
    }
    public static bool operator ==(hkMemoryMeshShape? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkMemoryMeshShape? a, object? b) => !(a == b);
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
