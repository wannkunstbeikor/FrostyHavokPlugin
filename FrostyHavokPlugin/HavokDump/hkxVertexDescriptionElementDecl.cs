using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxVertexDescriptionElementDecl : IHavokObject
{
    public virtual uint Signature => 0;
    public uint _byteOffset;
    public hkxVertexDescription_DataType _type;
    public hkxVertexDescription_DataUsage _usage;
    public uint _byteStride;
    public byte _numElements;
    public hkxVertexDescription_DataHint _hint;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _byteOffset = br.ReadUInt32();
        _type = (hkxVertexDescription_DataType)br.ReadUInt16();
        _usage = (hkxVertexDescription_DataUsage)br.ReadUInt16();
        _byteStride = br.ReadUInt32();
        _numElements = br.ReadByte();
        br.Position += 1; // padding
        _hint = (hkxVertexDescription_DataHint)br.ReadUInt16();
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        bw.WriteUInt32(_byteOffset);
        bw.WriteUInt16((ushort)_type);
        bw.WriteUInt16((ushort)_usage);
        bw.WriteUInt32(_byteStride);
        bw.WriteByte(_numElements);
        for (int i = 0; i < 1; i++) bw.WriteByte(0); // padding
        bw.WriteUInt16((ushort)_hint);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumber(xe, nameof(_byteOffset), _byteOffset);
        xs.WriteEnum<hkxVertexDescription_DataType, ushort>(xe, nameof(_type), (ushort)_type);
        xs.WriteEnum<hkxVertexDescription_DataUsage, ushort>(xe, nameof(_usage), (ushort)_usage);
        xs.WriteNumber(xe, nameof(_byteStride), _byteStride);
        xs.WriteNumber(xe, nameof(_numElements), _numElements);
        xs.WriteEnum<hkxVertexDescription_DataHint, ushort>(xe, nameof(_hint), (ushort)_hint);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkxVertexDescriptionElementDecl other && _byteOffset == other._byteOffset && _type == other._type && _usage == other._usage && _byteStride == other._byteStride && _numElements == other._numElements && _hint == other._hint && Signature == other.Signature;
    }
    public static bool operator ==(hkxVertexDescriptionElementDecl? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkxVertexDescriptionElementDecl? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_byteOffset);
        code.Add(_type);
        code.Add(_usage);
        code.Add(_byteStride);
        code.Add(_numElements);
        code.Add(_hint);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
