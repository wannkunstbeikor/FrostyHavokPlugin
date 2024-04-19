using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkClassMember : IHavokObject
{
    public virtual uint Signature => 0;
    public string _name = string.Empty;
    public hkClass? _class;
    public hkClassEnum? _enum;
    public hkClassMember_Type _type;
    public hkClassMember_Type _subtype;
    public short _cArraySize;
    public hkClassMember_FlagValues _flags;
    public ushort _offset;
    // TYPE_POINTER TYPE_STRUCT _attributes
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _name = des.ReadStringPointer(br);
        _class = des.ReadClassPointer<hkClass>(br);
        _enum = des.ReadClassPointer<hkClassEnum>(br);
        _type = (hkClassMember_Type)br.ReadByte();
        _subtype = (hkClassMember_Type)br.ReadByte();
        _cArraySize = br.ReadInt16();
        _flags = (hkClassMember_FlagValues)br.ReadUInt16();
        _offset = br.ReadUInt16();
        br.Position += 8; // padding
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteStringPointer(bw, _name);
        s.WriteClassPointer<hkClass>(bw, _class);
        s.WriteClassPointer<hkClassEnum>(bw, _enum);
        bw.WriteByte((byte)_type);
        bw.WriteByte((byte)_subtype);
        bw.WriteInt16(_cArraySize);
        bw.WriteUInt16((ushort)_flags);
        bw.WriteUInt16(_offset);
        for (int i = 0; i < 8; i++) bw.WriteByte(0); // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteString(xe, nameof(_name), _name);
        xs.WriteClassPointer(xe, nameof(_class), _class);
        xs.WriteClassPointer(xe, nameof(_enum), _enum);
        xs.WriteEnum<hkClassMember_Type, byte>(xe, nameof(_type), (byte)_type);
        xs.WriteEnum<hkClassMember_Type, byte>(xe, nameof(_subtype), (byte)_subtype);
        xs.WriteNumber(xe, nameof(_cArraySize), _cArraySize);
        xs.WriteFlag<hkClassMember_FlagValues, ushort>(xe, nameof(_flags), (ushort)_flags);
        xs.WriteNumber(xe, nameof(_offset), _offset);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkClassMember other && _name == other._name && _class == other._class && _enum == other._enum && _type == other._type && _subtype == other._subtype && _cArraySize == other._cArraySize && _flags == other._flags && _offset == other._offset && Signature == other.Signature;
    }
    public static bool operator ==(hkClassMember? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkClassMember? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_name);
        code.Add(_class);
        code.Add(_enum);
        code.Add(_type);
        code.Add(_subtype);
        code.Add(_cArraySize);
        code.Add(_flags);
        code.Add(_offset);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
