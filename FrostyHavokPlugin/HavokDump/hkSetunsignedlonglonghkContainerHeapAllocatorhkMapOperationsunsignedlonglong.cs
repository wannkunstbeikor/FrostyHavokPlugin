using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkSetunsignedlonglonghkContainerHeapAllocatorhkMapOperationsunsignedlonglong : IHavokObject
{
    public virtual uint Signature => 0;
    public List<ulong> _elem = new();
    public int _numElems;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _elem = des.ReadUInt64Array(br);
        _numElems = br.ReadInt32();
        br.Position += 4; // padding
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteUInt64Array(bw, _elem);
        bw.WriteInt32(_numElems);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumberArray(xe, nameof(_elem), _elem);
        xs.WriteNumber(xe, nameof(_numElems), _numElems);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkSetunsignedlonglonghkContainerHeapAllocatorhkMapOperationsunsignedlonglong other && _elem.SequenceEqual(other._elem) && _numElems == other._numElems && Signature == other.Signature;
    }
    public static bool operator ==(hkSetunsignedlonglonghkContainerHeapAllocatorhkMapOperationsunsignedlonglong? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkSetunsignedlonglonghkContainerHeapAllocatorhkMapOperationsunsignedlonglong? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_elem);
        code.Add(_numElems);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
