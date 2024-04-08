using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkcdSimdTree : IHavokObject, IEquatable<hkcdSimdTree?>
{
    public virtual uint Signature => 0;
    public List<hkcdSimdTreeNode> _nodes;
    public hkAabb _domain;
    public uint _root;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _nodes = des.ReadClassArray<hkcdSimdTreeNode>(br);
        _domain = new hkAabb();
        _domain.Read(des, br);
        _root = br.ReadUInt32();
        br.Position += 12; // padding
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteClassArray<hkcdSimdTreeNode>(bw, _nodes);
        _domain.Write(s, bw);
        bw.WriteUInt32(_root);
        for (int i = 0; i < 12; i++) bw.WriteByte(0); // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClassArray<hkcdSimdTreeNode>(xe, nameof(_nodes), _nodes);
        xs.WriteClass(xe, nameof(_domain), _domain);
        xs.WriteNumber(xe, nameof(_root), _root);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkcdSimdTree);
    }
    public bool Equals(hkcdSimdTree? other)
    {
        return other is not null && _nodes.Equals(other._nodes) && _domain.Equals(other._domain) && _root.Equals(other._root) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_nodes);
        code.Add(_domain);
        code.Add(_root);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
