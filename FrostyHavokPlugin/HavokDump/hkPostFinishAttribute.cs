using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkPostFinishAttribute : IHavokObject, IEquatable<hkPostFinishAttribute?>
{
    public virtual uint Signature => 0;
    // TYPE_POINTER TYPE_VOID _postFinishFunction
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        br.Position += 8; // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkPostFinishAttribute);
    }
    public bool Equals(hkPostFinishAttribute? other)
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