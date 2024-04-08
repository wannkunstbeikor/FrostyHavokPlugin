using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkVertexFormat : IHavokObject, IEquatable<hkVertexFormat?>
{
    public virtual uint Signature => 0;
    public hkVertexFormatElement[] _elements = new hkVertexFormatElement[32];
    public int _numElements;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _elements = des.ReadStructCStyleArray<hkVertexFormatElement>(br, 32);
        _numElements = br.ReadInt32();
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteStructCStyleArray<hkVertexFormatElement>(bw, _elements);
        bw.WriteInt32(_numElements);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClassArray(xe, nameof(_elements), _elements);
        xs.WriteNumber(xe, nameof(_numElements), _numElements);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkVertexFormat);
    }
    public bool Equals(hkVertexFormat? other)
    {
        return other is not null && _elements.Equals(other._elements) && _numElements.Equals(other._numElements) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_elements);
        code.Add(_numElements);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
