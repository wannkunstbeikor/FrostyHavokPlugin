using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxNode : hkxAttributeHolder, IEquatable<hkxNode?>
{
    public override uint Signature => 0;
    public string _name;
    public hkReferencedObject _object;
    public List<Matrix4> _keyFrames;
    public List<hkxNode> _children;
    public List<hkxNodeAnnotationData> _annotations;
    public List<float> _linearKeyFrameHints;
    public string _userProperties;
    public bool _selected;
    public bool _bone;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _name = des.ReadStringPointer(br);
        _object = des.ReadClassPointer<hkReferencedObject>(br);
        _keyFrames = des.ReadMatrix4Array(br);
        _children = des.ReadClassPointerArray<hkxNode>(br);
        _annotations = des.ReadClassArray<hkxNodeAnnotationData>(br);
        _linearKeyFrameHints = des.ReadSingleArray(br);
        _userProperties = des.ReadStringPointer(br);
        _selected = br.ReadBoolean();
        _bone = br.ReadBoolean();
        br.Position += 6; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteStringPointer(bw, _name);
        s.WriteClassPointer<hkReferencedObject>(bw, _object);
        s.WriteMatrix4Array(bw, _keyFrames);
        s.WriteClassPointerArray<hkxNode>(bw, _children);
        s.WriteClassArray<hkxNodeAnnotationData>(bw, _annotations);
        s.WriteSingleArray(bw, _linearKeyFrameHints);
        s.WriteStringPointer(bw, _userProperties);
        bw.WriteBoolean(_selected);
        bw.WriteBoolean(_bone);
        for (int i = 0; i < 6; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteString(xe, nameof(_name), _name);
        xs.WriteClassPointer(xe, nameof(_object), _object);
        xs.WriteMatrix4Array(xe, nameof(_keyFrames), _keyFrames);
        xs.WriteClassPointerArray<hkxNode>(xe, nameof(_children), _children);
        xs.WriteClassArray<hkxNodeAnnotationData>(xe, nameof(_annotations), _annotations);
        xs.WriteFloatArray(xe, nameof(_linearKeyFrameHints), _linearKeyFrameHints);
        xs.WriteString(xe, nameof(_userProperties), _userProperties);
        xs.WriteBoolean(xe, nameof(_selected), _selected);
        xs.WriteBoolean(xe, nameof(_bone), _bone);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkxNode);
    }
    public bool Equals(hkxNode? other)
    {
        return other is not null && _name.Equals(other._name) && _object.Equals(other._object) && _keyFrames.Equals(other._keyFrames) && _children.Equals(other._children) && _annotations.Equals(other._annotations) && _linearKeyFrameHints.Equals(other._linearKeyFrameHints) && _userProperties.Equals(other._userProperties) && _selected.Equals(other._selected) && _bone.Equals(other._bone) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_name);
        code.Add(_object);
        code.Add(_keyFrames);
        code.Add(_children);
        code.Add(_annotations);
        code.Add(_linearKeyFrameHints);
        code.Add(_userProperties);
        code.Add(_selected);
        code.Add(_bone);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
