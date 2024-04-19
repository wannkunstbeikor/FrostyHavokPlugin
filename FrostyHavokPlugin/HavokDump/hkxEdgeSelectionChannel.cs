using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxEdgeSelectionChannel : hkReferencedObject
{
    public override uint Signature => 0;
    public List<int> _selectedEdges = new();
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _selectedEdges = des.ReadInt32Array(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteInt32Array(bw, _selectedEdges);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteNumberArray(xe, nameof(_selectedEdges), _selectedEdges);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkxEdgeSelectionChannel other && base.Equals(other) && _selectedEdges.SequenceEqual(other._selectedEdges) && Signature == other.Signature;
    }
    public static bool operator ==(hkxEdgeSelectionChannel? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkxEdgeSelectionChannel? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_selectedEdges);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
