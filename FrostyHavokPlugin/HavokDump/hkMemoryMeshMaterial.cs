using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkMemoryMeshMaterial : hkMeshMaterial
{
    public override uint Signature => 0;
    public string _materialName = string.Empty;
    public List<hkMeshTexture?> _textures = new();
    public Vector4 _diffuseColor;
    public Vector4 _ambientColor;
    public Vector4 _specularColor;
    public Vector4 _emissiveColor;
    public ulong _userData;
    public float _tesselationFactor;
    public float _displacementAmount;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _materialName = des.ReadStringPointer(br);
        _textures = des.ReadClassPointerArray<hkMeshTexture>(br);
        br.Position += 8; // padding
        _diffuseColor = des.ReadVector4(br);
        _ambientColor = des.ReadVector4(br);
        _specularColor = des.ReadVector4(br);
        _emissiveColor = des.ReadVector4(br);
        _userData = br.ReadUInt64();
        _tesselationFactor = br.ReadSingle();
        _displacementAmount = br.ReadSingle();
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteStringPointer(bw, _materialName);
        s.WriteClassPointerArray<hkMeshTexture>(bw, _textures);
        for (int i = 0; i < 8; i++) bw.WriteByte(0); // padding
        s.WriteVector4(bw, _diffuseColor);
        s.WriteVector4(bw, _ambientColor);
        s.WriteVector4(bw, _specularColor);
        s.WriteVector4(bw, _emissiveColor);
        bw.WriteUInt64(_userData);
        bw.WriteSingle(_tesselationFactor);
        bw.WriteSingle(_displacementAmount);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteString(xe, nameof(_materialName), _materialName);
        xs.WriteClassPointerArray<hkMeshTexture>(xe, nameof(_textures), _textures);
        xs.WriteVector4(xe, nameof(_diffuseColor), _diffuseColor);
        xs.WriteVector4(xe, nameof(_ambientColor), _ambientColor);
        xs.WriteVector4(xe, nameof(_specularColor), _specularColor);
        xs.WriteVector4(xe, nameof(_emissiveColor), _emissiveColor);
        xs.WriteNumber(xe, nameof(_userData), _userData);
        xs.WriteFloat(xe, nameof(_tesselationFactor), _tesselationFactor);
        xs.WriteFloat(xe, nameof(_displacementAmount), _displacementAmount);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkMemoryMeshMaterial other && base.Equals(other) && _materialName == other._materialName && _textures.SequenceEqual(other._textures) && _diffuseColor == other._diffuseColor && _ambientColor == other._ambientColor && _specularColor == other._specularColor && _emissiveColor == other._emissiveColor && _userData == other._userData && _tesselationFactor == other._tesselationFactor && _displacementAmount == other._displacementAmount && Signature == other.Signature;
    }
    public static bool operator ==(hkMemoryMeshMaterial? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkMemoryMeshMaterial? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_materialName);
        code.Add(_textures);
        code.Add(_diffuseColor);
        code.Add(_ambientColor);
        code.Add(_specularColor);
        code.Add(_emissiveColor);
        code.Add(_userData);
        code.Add(_tesselationFactor);
        code.Add(_displacementAmount);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
