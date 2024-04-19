using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkcdStaticMeshTreeBasePrimitiveDataRunBasehknpCompressedMeshShapeTreeDataRunData : IHavokObject
{
    public virtual uint Signature => 0;
    public hknpCompressedMeshShapeTreeDataRunData? _value;
    public byte _index;
    public byte _count;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _value = new hknpCompressedMeshShapeTreeDataRunData();
        _value.Read(des, br);
        _index = br.ReadByte();
        _count = br.ReadByte();
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        _value.Write(s, bw);
        bw.WriteByte(_index);
        bw.WriteByte(_count);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClass(xe, nameof(_value), _value);
        xs.WriteNumber(xe, nameof(_index), _index);
        xs.WriteNumber(xe, nameof(_count), _count);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkcdStaticMeshTreeBasePrimitiveDataRunBasehknpCompressedMeshShapeTreeDataRunData other && _value == other._value && _index == other._index && _count == other._count && Signature == other.Signature;
    }
    public static bool operator ==(hkcdStaticMeshTreeBasePrimitiveDataRunBasehknpCompressedMeshShapeTreeDataRunData? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkcdStaticMeshTreeBasePrimitiveDataRunBasehknpCompressedMeshShapeTreeDataRunData? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_value);
        code.Add(_index);
        code.Add(_count);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
