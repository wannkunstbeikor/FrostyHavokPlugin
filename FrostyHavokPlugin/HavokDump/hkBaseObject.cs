using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkBaseObject : IHavokObject, IEquatable<hkBaseObject?>
{
    public virtual uint Signature => 0;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        des.ReadUSize(br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteUSize(bw, 0);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkBaseObject);
    }
    public bool Equals(hkBaseObject? other)
    {
        return other is not null && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(Signature);
        return code.ToHashCode();
    }
}
