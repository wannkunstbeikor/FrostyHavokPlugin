using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkcdStaticTreeTreehkcdStaticTreeDynamicStorage32 : hkcdStaticTreeDynamicStorage32, IEquatable<hkcdStaticTreeTreehkcdStaticTreeDynamicStorage32?>
{
    public override uint Signature => 0;
    public hkAabb _domain;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _domain = new hkAabb();
        _domain.Read(des, br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        _domain.Write(s, bw);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClass(xe, nameof(_domain), _domain);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkcdStaticTreeTreehkcdStaticTreeDynamicStorage32);
    }
    public bool Equals(hkcdStaticTreeTreehkcdStaticTreeDynamicStorage32? other)
    {
        return other is not null && _domain.Equals(other._domain) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_domain);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
