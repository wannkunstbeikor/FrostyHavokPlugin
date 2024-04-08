using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkSkinnedRefMeshShape : hkMeshShape, IEquatable<hkSkinnedRefMeshShape?>
{
    public override uint Signature => 0;
    public hkSkinnedMeshShape _skinnedMeshShape;
    public List<short> _bones;
    public List<Vector4> _localFromRootTransforms;
    public string _name;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _skinnedMeshShape = des.ReadClassPointer<hkSkinnedMeshShape>(br);
        _bones = des.ReadInt16Array(br);
        _localFromRootTransforms = des.ReadVector4Array(br);
        _name = des.ReadStringPointer(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteClassPointer<hkSkinnedMeshShape>(bw, _skinnedMeshShape);
        s.WriteInt16Array(bw, _bones);
        s.WriteVector4Array(bw, _localFromRootTransforms);
        s.WriteStringPointer(bw, _name);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClassPointer(xe, nameof(_skinnedMeshShape), _skinnedMeshShape);
        xs.WriteNumberArray(xe, nameof(_bones), _bones);
        xs.WriteVector4Array(xe, nameof(_localFromRootTransforms), _localFromRootTransforms);
        xs.WriteString(xe, nameof(_name), _name);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkSkinnedRefMeshShape);
    }
    public bool Equals(hkSkinnedRefMeshShape? other)
    {
        return other is not null && _skinnedMeshShape.Equals(other._skinnedMeshShape) && _bones.Equals(other._bones) && _localFromRootTransforms.Equals(other._localFromRootTransforms) && _name.Equals(other._name) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_skinnedMeshShape);
        code.Add(_bones);
        code.Add(_localFromRootTransforms);
        code.Add(_name);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
