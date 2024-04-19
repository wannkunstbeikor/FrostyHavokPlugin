using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpMaterialDescriptor : IHavokObject
{
    public virtual uint Signature => 0;
    public string _name = string.Empty;
    public hknpRefMaterial? _material;
    public ushort _materialId;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _name = des.ReadStringPointer(br);
        _material = des.ReadClassPointer<hknpRefMaterial>(br);
        _materialId = br.ReadUInt16();
        br.Position += 6; // padding
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteStringPointer(bw, _name);
        s.WriteClassPointer<hknpRefMaterial>(bw, _material);
        bw.WriteUInt16(_materialId);
        for (int i = 0; i < 6; i++) bw.WriteByte(0); // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteString(xe, nameof(_name), _name);
        xs.WriteClassPointer(xe, nameof(_material), _material);
        xs.WriteNumber(xe, nameof(_materialId), _materialId);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpMaterialDescriptor other && _name == other._name && _material == other._material && _materialId == other._materialId && Signature == other.Signature;
    }
    public static bool operator ==(hknpMaterialDescriptor? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpMaterialDescriptor? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_name);
        code.Add(_material);
        code.Add(_materialId);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
