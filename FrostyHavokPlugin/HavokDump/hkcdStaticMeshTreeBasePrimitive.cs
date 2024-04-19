using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkcdStaticMeshTreeBasePrimitive : IHavokObject
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
        return obj is hkcdStaticMeshTreeBasePrimitive other && _indices == other._indices && Signature == other.Signature;
    }
    public static bool operator ==(hkcdStaticMeshTreeBasePrimitive? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkcdStaticMeshTreeBasePrimitive? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_indices);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
