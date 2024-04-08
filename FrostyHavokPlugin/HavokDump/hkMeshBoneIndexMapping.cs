using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkMeshBoneIndexMapping : IHavokObject, IEquatable<hkMeshBoneIndexMapping?>
{
    public virtual uint Signature => 0;
    public List<short> _mapping;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _mapping = des.ReadInt16Array(br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteInt16Array(bw, _mapping);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumberArray(xe, nameof(_mapping), _mapping);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkMeshBoneIndexMapping);
    }
    public bool Equals(hkMeshBoneIndexMapping? other)
    {
        return other is not null && _mapping.Equals(other._mapping) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_mapping);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
