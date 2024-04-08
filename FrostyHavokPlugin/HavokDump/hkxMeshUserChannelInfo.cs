using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxMeshUserChannelInfo : hkxAttributeHolder, IEquatable<hkxMeshUserChannelInfo?>
{
    public override uint Signature => 0;
    public string _name;
    public string _className;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _name = des.ReadStringPointer(br);
        _className = des.ReadStringPointer(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteStringPointer(bw, _name);
        s.WriteStringPointer(bw, _className);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteString(xe, nameof(_name), _name);
        xs.WriteString(xe, nameof(_className), _className);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkxMeshUserChannelInfo);
    }
    public bool Equals(hkxMeshUserChannelInfo? other)
    {
        return other is not null && _name.Equals(other._name) && _className.Equals(other._className) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_name);
        code.Add(_className);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
