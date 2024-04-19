using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxMaterialShader : hkReferencedObject
{
    public override uint Signature => 0;
    public string _name = string.Empty;
    public hkxMaterialShader_ShaderType _type;
    public string _vertexEntryName = string.Empty;
    public string _geomEntryName = string.Empty;
    public string _pixelEntryName = string.Empty;
    public List<byte> _data = new();
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _name = des.ReadStringPointer(br);
        _type = (hkxMaterialShader_ShaderType)br.ReadByte();
        br.Position += 7; // padding
        _vertexEntryName = des.ReadStringPointer(br);
        _geomEntryName = des.ReadStringPointer(br);
        _pixelEntryName = des.ReadStringPointer(br);
        _data = des.ReadByteArray(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteStringPointer(bw, _name);
        bw.WriteByte((byte)_type);
        for (int i = 0; i < 7; i++) bw.WriteByte(0); // padding
        s.WriteStringPointer(bw, _vertexEntryName);
        s.WriteStringPointer(bw, _geomEntryName);
        s.WriteStringPointer(bw, _pixelEntryName);
        s.WriteByteArray(bw, _data);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteString(xe, nameof(_name), _name);
        xs.WriteEnum<hkxMaterialShader_ShaderType, byte>(xe, nameof(_type), (byte)_type);
        xs.WriteString(xe, nameof(_vertexEntryName), _vertexEntryName);
        xs.WriteString(xe, nameof(_geomEntryName), _geomEntryName);
        xs.WriteString(xe, nameof(_pixelEntryName), _pixelEntryName);
        xs.WriteNumberArray(xe, nameof(_data), _data);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkxMaterialShader other && base.Equals(other) && _name == other._name && _type == other._type && _vertexEntryName == other._vertexEntryName && _geomEntryName == other._geomEntryName && _pixelEntryName == other._pixelEntryName && _data.SequenceEqual(other._data) && Signature == other.Signature;
    }
    public static bool operator ==(hkxMaterialShader? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkxMaterialShader? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_name);
        code.Add(_type);
        code.Add(_vertexEntryName);
        code.Add(_geomEntryName);
        code.Add(_pixelEntryName);
        code.Add(_data);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
