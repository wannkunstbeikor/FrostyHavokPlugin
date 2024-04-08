using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkClassEnum : IHavokObject, IEquatable<hkClassEnum?>
{
    public virtual uint Signature => 0;
    public string _name;
    public List<hkClassEnumItem> _items;
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
        return Equals(obj as hkClassEnum);
    }
    public bool Equals(hkClassEnum? other)
    {
        return other is not null && _name.Equals(other._name) && _items.Equals(other._items) && _flags.Equals(other._flags) && Signature == other.Signature;
    }
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
