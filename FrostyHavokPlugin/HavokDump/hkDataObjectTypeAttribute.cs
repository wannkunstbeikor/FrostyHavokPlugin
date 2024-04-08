using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkDataObjectTypeAttribute : IHavokObject, IEquatable<hkDataObjectTypeAttribute?>
{
    public virtual uint Signature => 0;
    public string _typeName;
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
        return Equals(obj as hkDataObjectTypeAttribute);
    }
    public bool Equals(hkDataObjectTypeAttribute? other)
    {
        return other is not null && _typeName.Equals(other._typeName) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_typeName);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
