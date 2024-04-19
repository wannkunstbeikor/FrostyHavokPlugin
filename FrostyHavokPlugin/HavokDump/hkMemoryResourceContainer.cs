using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkMemoryResourceContainer : hkResourceContainer
{
    public override uint Signature => 0;
    public string _name = string.Empty;
    // TYPE_POINTER TYPE_STRUCT _parent
    public List<hkMemoryResourceHandle?> _resourceHandles = new();
    public List<hkMemoryResourceContainer?> _children = new();
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _name = des.ReadStringPointer(br);
        br.Position += 8; // padding
        _resourceHandles = des.ReadClassPointerArray<hkMemoryResourceHandle>(br);
        _children = des.ReadClassPointerArray<hkMemoryResourceContainer>(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteStringPointer(bw, _name);
        for (int i = 0; i < 8; i++) bw.WriteByte(0); // padding
        s.WriteClassPointerArray<hkMemoryResourceHandle>(bw, _resourceHandles);
        s.WriteClassPointerArray<hkMemoryResourceContainer>(bw, _children);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteString(xe, nameof(_name), _name);
        xs.WriteClassPointerArray<hkMemoryResourceHandle>(xe, nameof(_resourceHandles), _resourceHandles);
        xs.WriteClassPointerArray<hkMemoryResourceContainer>(xe, nameof(_children), _children);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkMemoryResourceContainer other && base.Equals(other) && _name == other._name && _resourceHandles.SequenceEqual(other._resourceHandles) && _children.SequenceEqual(other._children) && Signature == other.Signature;
    }
    public static bool operator ==(hkMemoryResourceContainer? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkMemoryResourceContainer? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_name);
        code.Add(_resourceHandles);
        code.Add(_children);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
