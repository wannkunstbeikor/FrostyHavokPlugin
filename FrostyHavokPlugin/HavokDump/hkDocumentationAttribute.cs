using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkDocumentationAttribute : IHavokObject
{
    public virtual uint Signature => 0;
    public string _docsSectionTag = string.Empty;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _docsSectionTag = des.ReadStringPointer(br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteStringPointer(bw, _docsSectionTag);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteString(xe, nameof(_docsSectionTag), _docsSectionTag);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkDocumentationAttribute other && _docsSectionTag == other._docsSectionTag && Signature == other.Signature;
    }
    public static bool operator ==(hkDocumentationAttribute? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkDocumentationAttribute? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_docsSectionTag);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
