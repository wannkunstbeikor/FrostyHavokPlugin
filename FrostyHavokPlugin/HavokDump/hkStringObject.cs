using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkStringObject : hkReferencedObject, IEquatable<hkStringObject?>
{
    public override uint Signature => 0;
    public string _string;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _string = des.ReadStringPointer(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteStringPointer(bw, _string);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteString(xe, nameof(_string), _string);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkStringObject);
    }
    public bool Equals(hkStringObject? other)
    {
        return other is not null && _string.Equals(other._string) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_string);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
