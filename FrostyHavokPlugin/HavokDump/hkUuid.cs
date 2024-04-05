using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkUuid : IHavokObject, IEquatable<hkUuid?>
{
    public virtual uint Signature => 0;
    public uint[] _data = new uint[4];
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _data = des.ReadUInt32CStyleArray(br, 4);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumberArray(xe, nameof(_data), _data);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkUuid);
    }
    public bool Equals(hkUuid? other)
    {
        return other is not null && _data.Equals(other._data) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_data);
        code.Add(Signature);
        return code.ToHashCode();
    }
}