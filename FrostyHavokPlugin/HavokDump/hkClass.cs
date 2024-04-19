using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkClass : IHavokObject
{
    public virtual uint Signature => 0;
    public string _name = string.Empty;
    public hkClass? _parent;
    public int _objectSize;
    public int _numImplementedInterfaces;
    public List<hkClassEnum?> _declaredEnums = new();
    public List<hkClassMember?> _declaredMembers = new();
    // TYPE_POINTER TYPE_VOID _defaults
    // TYPE_POINTER TYPE_STRUCT _attributes
    public hkClass_FlagValues _flags;
    public int _describedVersion;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _name = des.ReadStringPointer(br);
        _parent = des.ReadClassPointer<hkClass>(br);
        _objectSize = br.ReadInt32();
        _numImplementedInterfaces = br.ReadInt32();
        // Read TYPE_SIMPLEARRAY
        // Read TYPE_SIMPLEARRAY
        br.Position += 16; // padding
        _flags = (hkClass_FlagValues)br.ReadUInt32();
        _describedVersion = br.ReadInt32();
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteStringPointer(bw, _name);
        s.WriteClassPointer<hkClass>(bw, _parent);
        bw.WriteInt32(_objectSize);
        bw.WriteInt32(_numImplementedInterfaces);
        // Write TYPE_SIMPLEARRAY
        // Write TYPE_SIMPLEARRAY
        for (int i = 0; i < 16; i++) bw.WriteByte(0); // padding
        bw.WriteUInt32((uint)_flags);
        bw.WriteInt32(_describedVersion);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteString(xe, nameof(_name), _name);
        xs.WriteClassPointer(xe, nameof(_parent), _parent);
        xs.WriteNumber(xe, nameof(_objectSize), _objectSize);
        xs.WriteNumber(xe, nameof(_numImplementedInterfaces), _numImplementedInterfaces);
        // Write TYPE_SIMPLEARRAY
        // Write TYPE_SIMPLEARRAY
        xs.WriteFlag<hkClass_FlagValues, uint>(xe, nameof(_flags), (uint)_flags);
        xs.WriteNumber(xe, nameof(_describedVersion), _describedVersion);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkClass other && _name == other._name && _parent == other._parent && _objectSize == other._objectSize && _numImplementedInterfaces == other._numImplementedInterfaces && _declaredEnums.SequenceEqual(other._declaredEnums) && _declaredMembers.SequenceEqual(other._declaredMembers) && _flags == other._flags && _describedVersion == other._describedVersion && Signature == other.Signature;
    }
    public static bool operator ==(hkClass? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkClass? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_name);
        code.Add(_parent);
        code.Add(_objectSize);
        code.Add(_numImplementedInterfaces);
        code.Add(_declaredEnums);
        code.Add(_declaredMembers);
        code.Add(_flags);
        code.Add(_describedVersion);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
