using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkDataObjectTypeAttribute : IHavokObject
{
    public virtual uint Signature => 0;
    public string _typeName = string.Empty;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _typeName = des.ReadStringPointer(br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteStringPointer(bw, _typeName);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteString(xe, nameof(_typeName), _typeName);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkDataObjectTypeAttribute other && _typeName == other._typeName && Signature == other.Signature;
    }
    public static bool operator ==(hkDataObjectTypeAttribute? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkDataObjectTypeAttribute? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_typeName);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
