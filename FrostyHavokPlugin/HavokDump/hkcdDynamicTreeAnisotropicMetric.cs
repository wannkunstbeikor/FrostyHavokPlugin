using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkcdDynamicTreeAnisotropicMetric : IHavokObject, IEquatable<hkcdDynamicTreeAnisotropicMetric?>
{
    public virtual uint Signature => 0;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        br.Position += 1; // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkcdDynamicTreeAnisotropicMetric);
    }
    public bool Equals(hkcdDynamicTreeAnisotropicMetric? other)
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
