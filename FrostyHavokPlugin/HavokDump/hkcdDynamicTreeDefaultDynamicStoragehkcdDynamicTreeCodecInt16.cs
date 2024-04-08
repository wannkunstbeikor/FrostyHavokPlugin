using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkcdDynamicTreeDefaultDynamicStoragehkcdDynamicTreeCodecInt16 : hkcdDynamicTreeDynamicStorage0hkcdDynamicTreeAnisotropicMetrichkcdDynamicTreeCodecInt16, IEquatable<hkcdDynamicTreeDefaultDynamicStoragehkcdDynamicTreeCodecInt16?>
{
    public override uint Signature => 0;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkcdDynamicTreeDefaultDynamicStoragehkcdDynamicTreeCodecInt16);
    }
    public bool Equals(hkcdDynamicTreeDefaultDynamicStoragehkcdDynamicTreeCodecInt16? other)
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
