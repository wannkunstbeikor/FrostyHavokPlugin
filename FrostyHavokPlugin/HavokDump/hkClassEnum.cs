using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkClassEnum : IHavokObject
{
    public virtual uint Signature => 0;
    public string _name = string.Empty;
    public List<hkClassEnumItem?> _items = new();
    // TYPE_POINTER TYPE_STRUCT _attributes
    public hkClassEnum_FlagValues _flags;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _name = des.ReadStringPointer(br);
        // Read TYPE_SIMPLEARRAY
        br.Position += 8; // padding
        _flags = (hkClassEnum_FlagValues)br.ReadUInt32();
        br.Position += 4; // padding
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteStringPointer(bw, _name);
        // Write TYPE_SIMPLEARRAY
        for (int i = 0; i < 8; i++) bw.WriteByte(0); // padding
        bw.WriteUInt32((uint)_flags);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteString(xe, nameof(_name), _name);
        // Write TYPE_SIMPLEARRAY
        xs.WriteFlag<hkClassEnum_FlagValues, uint>(xe, nameof(_flags), (uint)_flags);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkClassEnum other && _name == other._name && _items.SequenceEqual(other._items) && _flags == other._flags && Signature == other.Signature;
    }
    public static bool operator ==(hkClassEnum? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkClassEnum? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_name);
        code.Add(_items);
        code.Add(_flags);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
