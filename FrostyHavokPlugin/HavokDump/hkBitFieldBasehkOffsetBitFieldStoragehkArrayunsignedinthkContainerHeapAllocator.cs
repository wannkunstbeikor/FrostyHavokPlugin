using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkBitFieldBasehkOffsetBitFieldStoragehkArrayunsignedinthkContainerHeapAllocator : IHavokObject, IEquatable<hkBitFieldBasehkOffsetBitFieldStoragehkArrayunsignedinthkContainerHeapAllocator?>
{
    public virtual uint Signature => 0;
    public hkOffsetBitFieldStoragehkArrayunsignedinthkContainerHeapAllocator _storage;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _storage = new hkOffsetBitFieldStoragehkArrayunsignedinthkContainerHeapAllocator();
        _storage.Read(des, br);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClass(xe, nameof(_storage), _storage);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkBitFieldBasehkOffsetBitFieldStoragehkArrayunsignedinthkContainerHeapAllocator);
    }
    public bool Equals(hkBitFieldBasehkOffsetBitFieldStoragehkArrayunsignedinthkContainerHeapAllocator? other)
    {
        return other is not null && _storage.Equals(other._storage) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_storage);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
