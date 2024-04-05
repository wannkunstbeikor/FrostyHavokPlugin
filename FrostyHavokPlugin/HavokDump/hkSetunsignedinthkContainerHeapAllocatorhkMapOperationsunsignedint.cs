using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkSetunsignedinthkContainerHeapAllocatorhkMapOperationsunsignedint : IHavokObject, IEquatable<hkSetunsignedinthkContainerHeapAllocatorhkMapOperationsunsignedint?>
{
    public virtual uint Signature => 0;
    public List<uint> _elem;
    public int _numElems;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _elem = des.ReadUInt32Array(br);
        _numElems = br.ReadInt32();
        br.Position += 4; // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumberArray(xe, nameof(_elem), _elem);
        xs.WriteNumber(xe, nameof(_numElems), _numElems);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkSetunsignedinthkContainerHeapAllocatorhkMapOperationsunsignedint);
    }
    public bool Equals(hkSetunsignedinthkContainerHeapAllocatorhkMapOperationsunsignedint? other)
    {
        return other is not null && _elem.Equals(other._elem) && _numElems.Equals(other._numElems) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_elem);
        code.Add(_numElems);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
