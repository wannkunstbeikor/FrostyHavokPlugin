using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkDescriptionAttribute : IHavokObject, IEquatable<hkDescriptionAttribute?>
{
    public virtual uint Signature => 0;
    public string _string;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _string = des.ReadStringPointer(br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteStringPointer(bw, _string);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteString(xe, nameof(_string), _string);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkDescriptionAttribute);
    }
    public bool Equals(hkDescriptionAttribute? other)
    {
        return other is not null && _string.Equals(other._string) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_string);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
