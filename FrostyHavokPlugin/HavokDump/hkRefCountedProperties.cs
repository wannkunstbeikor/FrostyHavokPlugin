using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkRefCountedProperties : IHavokObject
{
    public virtual uint Signature => 0;
    public List<hkRefCountedPropertiesEntry?> _entries = new();
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _entries = des.ReadClassArray<hkRefCountedPropertiesEntry>(br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteClassArray<hkRefCountedPropertiesEntry>(bw, _entries);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClassArray<hkRefCountedPropertiesEntry>(xe, nameof(_entries), _entries);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkRefCountedProperties other && _entries.SequenceEqual(other._entries) && Signature == other.Signature;
    }
    public static bool operator ==(hkRefCountedProperties? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkRefCountedProperties? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_entries);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
