using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkRootLevelContainer : IHavokObject, IEquatable<hkRootLevelContainer?>
{
    public virtual uint Signature => 0;
    public List<hkRootLevelContainerNamedVariant> _namedVariants;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _namedVariants = des.ReadClassArray<hkRootLevelContainerNamedVariant>(br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteClassArray<hkRootLevelContainerNamedVariant>(bw, _namedVariants);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClassArray<hkRootLevelContainerNamedVariant>(xe, nameof(_namedVariants), _namedVariants);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkRootLevelContainer);
    }
    public bool Equals(hkRootLevelContainer? other)
    {
        return other is not null && _namedVariants.Equals(other._namedVariants) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_namedVariants);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
