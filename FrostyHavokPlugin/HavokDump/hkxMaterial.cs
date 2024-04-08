using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxMaterial : hkxAttributeHolder, IEquatable<hkxMaterial?>
{
    public override uint Signature => 0;
    public string _name;
    public List<hkxMaterialTextureStage> _stages;
    public Vector4 _diffuseColor;
    public Vector4 _ambientColor;
    public Vector4 _specularColor;
    public Vector4 _emissiveColor;
    public List<hkxMaterial> _subMaterials;
    public hkReferencedObject _extraData;
    public float[] _uvMapScale = new float[2];
    public float[] _uvMapOffset = new float[2];
    public float _uvMapRotation;
    public hkxMaterial_UVMappingAlgorithm _uvMapAlgorithm;
    public float _specularMultiplier;
    public float _specularExponent;
    public hkxMaterial_Transparency _transparency;
    public ulong _userData;
    public List<hkxMaterialProperty> _properties;
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
        return Equals(obj as hkxMaterial);
    }
    public bool Equals(hkxMaterial? other)
    {
        return other is not null && _name.Equals(other._name) && _stages.Equals(other._stages) && _diffuseColor.Equals(other._diffuseColor) && _ambientColor.Equals(other._ambientColor) && _specularColor.Equals(other._specularColor) && _emissiveColor.Equals(other._emissiveColor) && _subMaterials.Equals(other._subMaterials) && _extraData.Equals(other._extraData) && _uvMapScale.Equals(other._uvMapScale) && _uvMapOffset.Equals(other._uvMapOffset) && _uvMapRotation.Equals(other._uvMapRotation) && _uvMapAlgorithm.Equals(other._uvMapAlgorithm) && _specularMultiplier.Equals(other._specularMultiplier) && _specularExponent.Equals(other._specularExponent) && _transparency.Equals(other._transparency) && _userData.Equals(other._userData) && _properties.Equals(other._properties) && Signature == other.Signature;
    }
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
