using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkcdStaticMeshTreeBasePrimitive : IHavokObject, IEquatable<hkcdStaticMeshTreeBasePrimitive?>
{
    public virtual uint Signature => 0;
    public byte[] _indices = new byte[4];
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _indices = des.ReadByteCStyleArray(br, 4);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteByteCStyleArray(bw, _indices);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumberArray(xe, nameof(_indices), _indices);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkcdStaticMeshTreeBasePrimitive);
    }
    public bool Equals(hkcdStaticMeshTreeBasePrimitive? other)
    {
        return other is not null && _indices.Equals(other._indices) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_indices);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
