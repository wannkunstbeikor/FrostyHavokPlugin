using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxNodeSelectionSet : hkxAttributeHolder, IEquatable<hkxNodeSelectionSet?>
{
    public override uint Signature => 0;
    public List<hkxNode> _selectedNodes;
    public string _name;
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
        return Equals(obj as hkxNodeSelectionSet);
    }
    public bool Equals(hkxNodeSelectionSet? other)
    {
        return other is not null && _selectedNodes.Equals(other._selectedNodes) && _name.Equals(other._name) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_selectedNodes);
        code.Add(_name);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
