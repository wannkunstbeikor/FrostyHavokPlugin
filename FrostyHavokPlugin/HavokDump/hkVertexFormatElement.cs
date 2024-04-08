using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkVertexFormatElement : IHavokObject, IEquatable<hkVertexFormatElement?>
{
    public virtual uint Signature => 0;
    public hkVertexFormat_ComponentType _dataType;
    public byte _numValues;
    public hkVertexFormat_ComponentUsage _usage;
    public byte _subUsage;
    public hkVertexFormat_HintFlags _flags;
    public byte[] _pad = new byte[3];
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _dataType = (hkVertexFormat_ComponentType)br.ReadByte();
        _numValues = br.ReadByte();
        _usage = (hkVertexFormat_ComponentUsage)br.ReadByte();
        _subUsage = br.ReadByte();
        _flags = (hkVertexFormat_HintFlags)br.ReadByte();
        _pad = des.ReadByteCStyleArray(br, 3);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        bw.WriteByte((byte)_dataType);
        bw.WriteByte(_numValues);
        bw.WriteByte((byte)_usage);
        bw.WriteByte(_subUsage);
        bw.WriteByte((byte)_flags);
        s.WriteByteCStyleArray(bw, _pad);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteEnum<hkVertexFormat_ComponentType, byte>(xe, nameof(_dataType), (byte)_dataType);
        xs.WriteNumber(xe, nameof(_numValues), _numValues);
        xs.WriteEnum<hkVertexFormat_ComponentUsage, byte>(xe, nameof(_usage), (byte)_usage);
        xs.WriteNumber(xe, nameof(_subUsage), _subUsage);
        xs.WriteFlag<hkVertexFormat_HintFlags, byte>(xe, nameof(_flags), (byte)_flags);
        xs.WriteNumberArray(xe, nameof(_pad), _pad);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkVertexFormatElement);
    }
    public bool Equals(hkVertexFormatElement? other)
    {
        return other is not null && _dataType.Equals(other._dataType) && _numValues.Equals(other._numValues) && _usage.Equals(other._usage) && _subUsage.Equals(other._subUsage) && _flags.Equals(other._flags) && _pad.Equals(other._pad) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_dataType);
        code.Add(_numValues);
        code.Add(_usage);
        code.Add(_subUsage);
        code.Add(_flags);
        code.Add(_pad);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
