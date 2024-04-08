using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkDocumentationAttribute : IHavokObject, IEquatable<hkDocumentationAttribute?>
{
    public virtual uint Signature => 0;
    public string _docsSectionTag;
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
        return Equals(obj as hkDocumentationAttribute);
    }
    public bool Equals(hkDocumentationAttribute? other)
    {
        return other is not null && _docsSectionTag.Equals(other._docsSectionTag) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_docsSectionTag);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
