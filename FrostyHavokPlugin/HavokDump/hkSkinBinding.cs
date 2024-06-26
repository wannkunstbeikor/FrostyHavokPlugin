using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkSkinBinding : hkMeshShape
{
    public override uint Signature => 0;
    public hkMeshShape? _skin;
    public List<Matrix4> _worldFromBoneTransforms = new();
    public List<string> _boneNames = new();
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _skin = des.ReadClassPointer<hkMeshShape>(br);
        _worldFromBoneTransforms = des.ReadMatrix4Array(br);
        _boneNames = des.ReadStringPointerArray(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteClassPointer<hkMeshShape>(bw, _skin);
        s.WriteMatrix4Array(bw, _worldFromBoneTransforms);
        s.WriteStringPointerArray(bw, _boneNames);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClassPointer(xe, nameof(_skin), _skin);
        xs.WriteMatrix4Array(xe, nameof(_worldFromBoneTransforms), _worldFromBoneTransforms);
        xs.WriteStringArray(xe, nameof(_boneNames), _boneNames);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkSkinBinding other && base.Equals(other) && _skin == other._skin && _worldFromBoneTransforms.SequenceEqual(other._worldFromBoneTransforms) && _boneNames.SequenceEqual(other._boneNames) && Signature == other.Signature;
    }
    public static bool operator ==(hkSkinBinding? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkSkinBinding? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_skin);
        code.Add(_worldFromBoneTransforms);
        code.Add(_boneNames);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
