using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkMultipleVertexBufferElementInfo : IHavokObject, IEquatable<hkMultipleVertexBufferElementInfo?>
{
    public virtual uint Signature => 0;
    public byte _vertexBufferIndex;
    public byte _elementIndex;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _vertexBufferIndex = br.ReadByte();
        _elementIndex = br.ReadByte();
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        bw.WriteByte(_vertexBufferIndex);
        bw.WriteByte(_elementIndex);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumber(xe, nameof(_vertexBufferIndex), _vertexBufferIndex);
        xs.WriteNumber(xe, nameof(_elementIndex), _elementIndex);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkMultipleVertexBufferElementInfo);
    }
    public bool Equals(hkMultipleVertexBufferElementInfo? other)
    {
        return other is not null && _vertexBufferIndex.Equals(other._vertexBufferIndex) && _elementIndex.Equals(other._elementIndex) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_vertexBufferIndex);
        code.Add(_elementIndex);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
