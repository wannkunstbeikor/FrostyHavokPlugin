using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxMaterial : hkxAttributeHolder
{
    public override uint Signature => 0;
    public string _name = string.Empty;
    public List<hkxMaterialTextureStage?> _stages = new();
    public Vector4 _diffuseColor;
    public Vector4 _ambientColor;
    public Vector4 _specularColor;
    public Vector4 _emissiveColor;
    public List<hkxMaterial?> _subMaterials = new();
    public hkReferencedObject? _extraData;
    public float[] _uvMapScale = new float[2];
    public float[] _uvMapOffset = new float[2];
    public float _uvMapRotation;
    public hkxMaterial_UVMappingAlgorithm _uvMapAlgorithm;
    public float _specularMultiplier;
    public float _specularExponent;
    public hkxMaterial_Transparency _transparency;
    public ulong _userData;
    public List<hkxMaterialProperty?> _properties = new();
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _name = des.ReadStringPointer(br);
        _stages = des.ReadClassArray<hkxMaterialTextureStage>(br);
        br.Position += 8; // padding
        _diffuseColor = des.ReadVector4(br);
        _ambientColor = des.ReadVector4(br);
        _specularColor = des.ReadVector4(br);
        _emissiveColor = des.ReadVector4(br);
        _subMaterials = des.ReadClassPointerArray<hkxMaterial>(br);
        _extraData = des.ReadClassPointer<hkReferencedObject>(br);
        _uvMapScale = des.ReadSingleCStyleArray(br, 2);
        _uvMapOffset = des.ReadSingleCStyleArray(br, 2);
        _uvMapRotation = br.ReadSingle();
        _uvMapAlgorithm = (hkxMaterial_UVMappingAlgorithm)br.ReadUInt32();
        _specularMultiplier = br.ReadSingle();
        _specularExponent = br.ReadSingle();
        _transparency = (hkxMaterial_Transparency)br.ReadByte();
        br.Position += 7; // padding
        _userData = br.ReadUInt64();
        _properties = des.ReadClassArray<hkxMaterialProperty>(br);
        br.Position += 8; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteStringPointer(bw, _name);
        s.WriteClassArray<hkxMaterialTextureStage>(bw, _stages);
        for (int i = 0; i < 8; i++) bw.WriteByte(0); // padding
        s.WriteVector4(bw, _diffuseColor);
        s.WriteVector4(bw, _ambientColor);
        s.WriteVector4(bw, _specularColor);
        s.WriteVector4(bw, _emissiveColor);
        s.WriteClassPointerArray<hkxMaterial>(bw, _subMaterials);
        s.WriteClassPointer<hkReferencedObject>(bw, _extraData);
        s.WriteSingleCStyleArray(bw, _uvMapScale);
        s.WriteSingleCStyleArray(bw, _uvMapOffset);
        bw.WriteSingle(_uvMapRotation);
        bw.WriteUInt32((uint)_uvMapAlgorithm);
        bw.WriteSingle(_specularMultiplier);
        bw.WriteSingle(_specularExponent);
        bw.WriteByte((byte)_transparency);
        for (int i = 0; i < 7; i++) bw.WriteByte(0); // padding
        bw.WriteUInt64(_userData);
        s.WriteClassArray<hkxMaterialProperty>(bw, _properties);
        for (int i = 0; i < 8; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteString(xe, nameof(_name), _name);
        xs.WriteClassArray<hkxMaterialTextureStage>(xe, nameof(_stages), _stages);
        xs.WriteVector4(xe, nameof(_diffuseColor), _diffuseColor);
        xs.WriteVector4(xe, nameof(_ambientColor), _ambientColor);
        xs.WriteVector4(xe, nameof(_specularColor), _specularColor);
        xs.WriteVector4(xe, nameof(_emissiveColor), _emissiveColor);
        xs.WriteClassPointerArray<hkxMaterial>(xe, nameof(_subMaterials), _subMaterials);
        xs.WriteClassPointer(xe, nameof(_extraData), _extraData);
        xs.WriteFloatArray(xe, nameof(_uvMapScale), _uvMapScale);
        xs.WriteFloatArray(xe, nameof(_uvMapOffset), _uvMapOffset);
        xs.WriteFloat(xe, nameof(_uvMapRotation), _uvMapRotation);
        xs.WriteEnum<hkxMaterial_UVMappingAlgorithm, uint>(xe, nameof(_uvMapAlgorithm), (uint)_uvMapAlgorithm);
        xs.WriteFloat(xe, nameof(_specularMultiplier), _specularMultiplier);
        xs.WriteFloat(xe, nameof(_specularExponent), _specularExponent);
        xs.WriteEnum<hkxMaterial_Transparency, byte>(xe, nameof(_transparency), (byte)_transparency);
        xs.WriteNumber(xe, nameof(_userData), _userData);
        xs.WriteClassArray<hkxMaterialProperty>(xe, nameof(_properties), _properties);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkxMaterial other && base.Equals(other) && _name == other._name && _stages.SequenceEqual(other._stages) && _diffuseColor == other._diffuseColor && _ambientColor == other._ambientColor && _specularColor == other._specularColor && _emissiveColor == other._emissiveColor && _subMaterials.SequenceEqual(other._subMaterials) && _extraData == other._extraData && _uvMapScale == other._uvMapScale && _uvMapOffset == other._uvMapOffset && _uvMapRotation == other._uvMapRotation && _uvMapAlgorithm == other._uvMapAlgorithm && _specularMultiplier == other._specularMultiplier && _specularExponent == other._specularExponent && _transparency == other._transparency && _userData == other._userData && _properties.SequenceEqual(other._properties) && Signature == other.Signature;
    }
    public static bool operator ==(hkxMaterial? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkxMaterial? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_name);
        code.Add(_stages);
        code.Add(_diffuseColor);
        code.Add(_ambientColor);
        code.Add(_specularColor);
        code.Add(_emissiveColor);
        code.Add(_subMaterials);
        code.Add(_extraData);
        code.Add(_uvMapScale);
        code.Add(_uvMapOffset);
        code.Add(_uvMapRotation);
        code.Add(_uvMapAlgorithm);
        code.Add(_specularMultiplier);
        code.Add(_specularExponent);
        code.Add(_transparency);
        code.Add(_userData);
        code.Add(_properties);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
