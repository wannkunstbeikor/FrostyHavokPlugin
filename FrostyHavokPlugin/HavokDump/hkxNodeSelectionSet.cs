using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxNodeSelectionSet : hkxAttributeHolder
{
    public override uint Signature => 0;
    public List<hkxNode?> _selectedNodes = new();
    public string _name = string.Empty;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _selectedNodes = des.ReadClassPointerArray<hkxNode>(br);
        _name = des.ReadStringPointer(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteClassPointerArray<hkxNode>(bw, _selectedNodes);
        s.WriteStringPointer(bw, _name);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClassPointerArray<hkxNode>(xe, nameof(_selectedNodes), _selectedNodes);
        xs.WriteString(xe, nameof(_name), _name);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkxNodeSelectionSet other && base.Equals(other) && _selectedNodes.SequenceEqual(other._selectedNodes) && _name == other._name && Signature == other.Signature;
    }
    public static bool operator ==(hkxNodeSelectionSet? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkxNodeSelectionSet? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_selectedNodes);
        code.Add(_name);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
