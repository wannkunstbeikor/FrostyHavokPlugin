using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkRootLevelContainerNamedVariant : IHavokObject, IEquatable<hkRootLevelContainerNamedVariant?>
{
    public virtual uint Signature => 0;
    public string _name;
    public string _className;
    public hkReferencedObject _variant;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _name = des.ReadStringPointer(br);
        _className = des.ReadStringPointer(br);
        _variant = des.ReadClassPointer<hkReferencedObject>(br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteStringPointer(bw, _name);
        s.WriteStringPointer(bw, _className);
        s.WriteClassPointer<hkReferencedObject>(bw, _variant);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteString(xe, nameof(_name), _name);
        xs.WriteString(xe, nameof(_className), _className);
        xs.WriteClassPointer(xe, nameof(_variant), _variant);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkRootLevelContainerNamedVariant);
    }
    public bool Equals(hkRootLevelContainerNamedVariant? other)
    {
        return other is not null && _name.Equals(other._name) && _className.Equals(other._className) && _variant.Equals(other._variant) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_name);
        code.Add(_className);
        code.Add(_variant);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
