using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkSethkIntRealPairhkContainerHeapAllocatorhkMapOperationshkIntRealPair : IHavokObject, IEquatable<hkSethkIntRealPairhkContainerHeapAllocatorhkMapOperationshkIntRealPair?>
{
    public virtual uint Signature => 0;
    public List<hkIntRealPair> _elem;
    public int _numElems;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _elem = des.ReadClassArray<hkIntRealPair>(br);
        _numElems = br.ReadInt32();
        br.Position += 4; // padding
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteClassArray<hkIntRealPair>(bw, _elem);
        bw.WriteInt32(_numElems);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClassArray<hkIntRealPair>(xe, nameof(_elem), _elem);
        xs.WriteNumber(xe, nameof(_numElems), _numElems);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkSethkIntRealPairhkContainerHeapAllocatorhkMapOperationshkIntRealPair);
    }
    public bool Equals(hkSethkIntRealPairhkContainerHeapAllocatorhkMapOperationshkIntRealPair? other)
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
