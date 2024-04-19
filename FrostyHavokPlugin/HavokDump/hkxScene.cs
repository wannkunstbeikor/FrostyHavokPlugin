using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxScene : hkReferencedObject
{
    public override uint Signature => 0;
    public string _modeller = string.Empty;
    public string _asset = string.Empty;
    public float _sceneLength;
    public uint _numFrames;
    public hkxNode? _rootNode;
    public List<hkxNodeSelectionSet?> _selectionSets = new();
    public List<hkxCamera?> _cameras = new();
    public List<hkxLight?> _lights = new();
    public List<hkxMesh?> _meshes = new();
    public List<hkxMaterial?> _materials = new();
    public List<hkxTextureInplace?> _inplaceTextures = new();
    public List<hkxTextureFile?> _externalTextures = new();
    public List<hkxSkinBinding?> _skinBindings = new();
    public List<hkxSpline?> _splines = new();
    public Matrix3x4 _appliedTransform;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _modeller = des.ReadStringPointer(br);
        _asset = des.ReadStringPointer(br);
        _sceneLength = br.ReadSingle();
        _numFrames = br.ReadUInt32();
        _rootNode = des.ReadClassPointer<hkxNode>(br);
        _selectionSets = des.ReadClassPointerArray<hkxNodeSelectionSet>(br);
        _cameras = des.ReadClassPointerArray<hkxCamera>(br);
        _lights = des.ReadClassPointerArray<hkxLight>(br);
        _meshes = des.ReadClassPointerArray<hkxMesh>(br);
        _materials = des.ReadClassPointerArray<hkxMaterial>(br);
        _inplaceTextures = des.ReadClassPointerArray<hkxTextureInplace>(br);
        _externalTextures = des.ReadClassPointerArray<hkxTextureFile>(br);
        _skinBindings = des.ReadClassPointerArray<hkxSkinBinding>(br);
        _splines = des.ReadClassPointerArray<hkxSpline>(br);
        _appliedTransform = des.ReadMatrix3(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteStringPointer(bw, _modeller);
        s.WriteStringPointer(bw, _asset);
        bw.WriteSingle(_sceneLength);
        bw.WriteUInt32(_numFrames);
        s.WriteClassPointer<hkxNode>(bw, _rootNode);
        s.WriteClassPointerArray<hkxNodeSelectionSet>(bw, _selectionSets);
        s.WriteClassPointerArray<hkxCamera>(bw, _cameras);
        s.WriteClassPointerArray<hkxLight>(bw, _lights);
        s.WriteClassPointerArray<hkxMesh>(bw, _meshes);
        s.WriteClassPointerArray<hkxMaterial>(bw, _materials);
        s.WriteClassPointerArray<hkxTextureInplace>(bw, _inplaceTextures);
        s.WriteClassPointerArray<hkxTextureFile>(bw, _externalTextures);
        s.WriteClassPointerArray<hkxSkinBinding>(bw, _skinBindings);
        s.WriteClassPointerArray<hkxSpline>(bw, _splines);
        s.WriteMatrix3(bw, _appliedTransform);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteString(xe, nameof(_modeller), _modeller);
        xs.WriteString(xe, nameof(_asset), _asset);
        xs.WriteFloat(xe, nameof(_sceneLength), _sceneLength);
        xs.WriteNumber(xe, nameof(_numFrames), _numFrames);
        xs.WriteClassPointer(xe, nameof(_rootNode), _rootNode);
        xs.WriteClassPointerArray<hkxNodeSelectionSet>(xe, nameof(_selectionSets), _selectionSets);
        xs.WriteClassPointerArray<hkxCamera>(xe, nameof(_cameras), _cameras);
        xs.WriteClassPointerArray<hkxLight>(xe, nameof(_lights), _lights);
        xs.WriteClassPointerArray<hkxMesh>(xe, nameof(_meshes), _meshes);
        xs.WriteClassPointerArray<hkxMaterial>(xe, nameof(_materials), _materials);
        xs.WriteClassPointerArray<hkxTextureInplace>(xe, nameof(_inplaceTextures), _inplaceTextures);
        xs.WriteClassPointerArray<hkxTextureFile>(xe, nameof(_externalTextures), _externalTextures);
        xs.WriteClassPointerArray<hkxSkinBinding>(xe, nameof(_skinBindings), _skinBindings);
        xs.WriteClassPointerArray<hkxSpline>(xe, nameof(_splines), _splines);
        xs.WriteMatrix3(xe, nameof(_appliedTransform), _appliedTransform);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkxScene other && base.Equals(other) && _modeller == other._modeller && _asset == other._asset && _sceneLength == other._sceneLength && _numFrames == other._numFrames && _rootNode == other._rootNode && _selectionSets.SequenceEqual(other._selectionSets) && _cameras.SequenceEqual(other._cameras) && _lights.SequenceEqual(other._lights) && _meshes.SequenceEqual(other._meshes) && _materials.SequenceEqual(other._materials) && _inplaceTextures.SequenceEqual(other._inplaceTextures) && _externalTextures.SequenceEqual(other._externalTextures) && _skinBindings.SequenceEqual(other._skinBindings) && _splines.SequenceEqual(other._splines) && _appliedTransform == other._appliedTransform && Signature == other.Signature;
    }
    public static bool operator ==(hkxScene? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkxScene? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_modeller);
        code.Add(_asset);
        code.Add(_sceneLength);
        code.Add(_numFrames);
        code.Add(_rootNode);
        code.Add(_selectionSets);
        code.Add(_cameras);
        code.Add(_lights);
        code.Add(_meshes);
        code.Add(_materials);
        code.Add(_inplaceTextures);
        code.Add(_externalTextures);
        code.Add(_skinBindings);
        code.Add(_splines);
        code.Add(_appliedTransform);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
