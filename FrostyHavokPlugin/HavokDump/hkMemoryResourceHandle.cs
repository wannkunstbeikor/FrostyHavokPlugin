using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkMemoryResourceHandle : hkResourceHandle
{
    public override uint Signature => 0;
    public hkReferencedObject? _variant;
    public string _name = string.Empty;
    public List<hkMemoryResourceHandleExternalLink?> _references = new();
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _variant = des.ReadClassPointer<hkReferencedObject>(br);
        _name = des.ReadStringPointer(br);
        _references = des.ReadClassArray<hkMemoryResourceHandleExternalLink>(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteClassPointer<hkReferencedObject>(bw, _variant);
        s.WriteStringPointer(bw, _name);
        s.WriteClassArray<hkMemoryResourceHandleExternalLink>(bw, _references);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClassPointer(xe, nameof(_variant), _variant);
        xs.WriteString(xe, nameof(_name), _name);
        xs.WriteClassArray<hkMemoryResourceHandleExternalLink>(xe, nameof(_references), _references);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkMemoryResourceHandle other && base.Equals(other) && _variant == other._variant && _name == other._name && _references.SequenceEqual(other._references) && Signature == other.Signature;
    }
    public static bool operator ==(hkMemoryResourceHandle? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkMemoryResourceHandle? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_variant);
        code.Add(_name);
        code.Add(_references);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
