using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkFreeListArrayhknpMotionPropertieshknpMotionPropertiesId8hknpMotionPropertiesFreeListArrayOperations : IHavokObject, IEquatable<hkFreeListArrayhknpMotionPropertieshknpMotionPropertiesId8hknpMotionPropertiesFreeListArrayOperations?>
{
    public virtual uint Signature => 0;
    public List<hknpMotionProperties> _elements;
    public int _firstFree;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _elements = des.ReadClassArray<hknpMotionProperties>(br);
        _firstFree = br.ReadInt32();
        br.Position += 4; // padding
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteClassArray<hknpMotionProperties>(bw, _elements);
        bw.WriteInt32(_firstFree);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClassArray<hknpMotionProperties>(xe, nameof(_elements), _elements);
        xs.WriteNumber(xe, nameof(_firstFree), _firstFree);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkFreeListArrayhknpMotionPropertieshknpMotionPropertiesId8hknpMotionPropertiesFreeListArrayOperations);
    }
    public bool Equals(hkFreeListArrayhknpMotionPropertieshknpMotionPropertiesId8hknpMotionPropertiesFreeListArrayOperations? other)
    {
        return other is not null && _elements.Equals(other._elements) && _firstFree.Equals(other._firstFree) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_elements);
        code.Add(_firstFree);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
