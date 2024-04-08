using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkMemoryMeshTexture : hkMeshTexture, IEquatable<hkMemoryMeshTexture?>
{
    public override uint Signature => 0;
    public string _filename;
    public List<byte> _data;
    public hkMeshTexture_Format _format;
    public bool _hasMipMaps;
    public hkMeshTexture_FilterMode _filterMode;
    public hkMeshTexture_TextureUsageType _usageHint;
    public int _textureCoordChannel;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _filename = des.ReadStringPointer(br);
        _data = des.ReadByteArray(br);
        _format = (hkMeshTexture_Format)br.ReadSByte();
        _hasMipMaps = br.ReadBoolean();
        _filterMode = (hkMeshTexture_FilterMode)br.ReadSByte();
        _usageHint = (hkMeshTexture_TextureUsageType)br.ReadSByte();
        _textureCoordChannel = br.ReadInt32();
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteStringPointer(bw, _filename);
        s.WriteByteArray(bw, _data);
        bw.WriteSByte((sbyte)_format);
        bw.WriteBoolean(_hasMipMaps);
        bw.WriteSByte((sbyte)_filterMode);
        bw.WriteSByte((sbyte)_usageHint);
        bw.WriteInt32(_textureCoordChannel);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteString(xe, nameof(_filename), _filename);
        xs.WriteNumberArray(xe, nameof(_data), _data);
        xs.WriteEnum<hkMeshTexture_Format, sbyte>(xe, nameof(_format), (sbyte)_format);
        xs.WriteBoolean(xe, nameof(_hasMipMaps), _hasMipMaps);
        xs.WriteEnum<hkMeshTexture_FilterMode, sbyte>(xe, nameof(_filterMode), (sbyte)_filterMode);
        xs.WriteEnum<hkMeshTexture_TextureUsageType, sbyte>(xe, nameof(_usageHint), (sbyte)_usageHint);
        xs.WriteNumber(xe, nameof(_textureCoordChannel), _textureCoordChannel);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkMemoryMeshTexture);
    }
    public bool Equals(hkMemoryMeshTexture? other)
    {
        return other is not null && _filename.Equals(other._filename) && _data.Equals(other._data) && _format.Equals(other._format) && _hasMipMaps.Equals(other._hasMipMaps) && _filterMode.Equals(other._filterMode) && _usageHint.Equals(other._usageHint) && _textureCoordChannel.Equals(other._textureCoordChannel) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_filename);
        code.Add(_data);
        code.Add(_format);
        code.Add(_hasMipMaps);
        code.Add(_filterMode);
        code.Add(_usageHint);
        code.Add(_textureCoordChannel);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
