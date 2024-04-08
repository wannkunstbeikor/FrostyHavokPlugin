using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxVertexDescription : IHavokObject, IEquatable<hkxVertexDescription?>
{
    public virtual uint Signature => 0;
    public List<hkxVertexDescriptionElementDecl> _decls;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _decls = des.ReadClassArray<hkxVertexDescriptionElementDecl>(br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteClassArray<hkxVertexDescriptionElementDecl>(bw, _decls);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClassArray<hkxVertexDescriptionElementDecl>(xe, nameof(_decls), _decls);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkxVertexDescription);
    }
    public bool Equals(hkxVertexDescription? other)
    {
        return other is not null && _decls.Equals(other._decls) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_decls);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
