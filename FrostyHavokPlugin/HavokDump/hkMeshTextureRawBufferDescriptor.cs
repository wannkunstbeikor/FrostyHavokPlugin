using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkMeshTextureRawBufferDescriptor : IHavokObject, IEquatable<hkMeshTextureRawBufferDescriptor?>
{
    public virtual uint Signature => 0;
    public long _offset;
    public uint _stride;
    public uint _numElements;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _offset = br.ReadInt64();
        _stride = br.ReadUInt32();
        _numElements = br.ReadUInt32();
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        bw.WriteInt64(_offset);
        bw.WriteUInt32(_stride);
        bw.WriteUInt32(_numElements);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumber(xe, nameof(_offset), _offset);
        xs.WriteNumber(xe, nameof(_stride), _stride);
        xs.WriteNumber(xe, nameof(_numElements), _numElements);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkMeshTextureRawBufferDescriptor);
    }
    public bool Equals(hkMeshTextureRawBufferDescriptor? other)
    {
        return other is not null && _offset.Equals(other._offset) && _stride.Equals(other._stride) && _numElements.Equals(other._numElements) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_offset);
        code.Add(_stride);
        code.Add(_numElements);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
