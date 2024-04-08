using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkFloat16Transform : IHavokObject, IEquatable<hkFloat16Transform?>
{
    public virtual uint Signature => 0;
    public hkFloat16[] _elements = new hkFloat16[12];
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _elements = des.ReadStructCStyleArray<hkFloat16>(br, 12);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteStructCStyleArray<hkFloat16>(bw, _elements);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClassArray(xe, nameof(_elements), _elements);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkFloat16Transform);
    }
    public bool Equals(hkFloat16Transform? other)
    {
        return other is not null && _elements.Equals(other._elements) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_elements);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
