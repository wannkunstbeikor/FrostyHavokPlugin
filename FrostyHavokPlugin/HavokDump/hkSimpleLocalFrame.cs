using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkSimpleLocalFrame : hkLocalFrame
{
    public override uint Signature => 0;
    public Matrix4 _transform;
    public List<hkLocalFrame?> _children = new();
    public hkLocalFrame? _parentFrame;
    public hkLocalFrameGroup? _group;
    public string _name = string.Empty;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _transform = des.ReadTransform(br);
        _children = des.ReadClassPointerArray<hkLocalFrame>(br);
        _parentFrame = des.ReadClassPointer<hkLocalFrame>(br);
        _group = des.ReadClassPointer<hkLocalFrameGroup>(br);
        _name = des.ReadStringPointer(br);
        br.Position += 8; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteTransform(bw, _transform);
        s.WriteClassPointerArray<hkLocalFrame>(bw, _children);
        s.WriteClassPointer<hkLocalFrame>(bw, _parentFrame);
        s.WriteClassPointer<hkLocalFrameGroup>(bw, _group);
        s.WriteStringPointer(bw, _name);
        for (int i = 0; i < 8; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteTransform(xe, nameof(_transform), _transform);
        xs.WriteClassPointerArray<hkLocalFrame>(xe, nameof(_children), _children);
        xs.WriteClassPointer(xe, nameof(_parentFrame), _parentFrame);
        xs.WriteClassPointer(xe, nameof(_group), _group);
        xs.WriteString(xe, nameof(_name), _name);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkSimpleLocalFrame other && base.Equals(other) && _transform == other._transform && _children.SequenceEqual(other._children) && _parentFrame == other._parentFrame && _group == other._group && _name == other._name && Signature == other.Signature;
    }
    public static bool operator ==(hkSimpleLocalFrame? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkSimpleLocalFrame? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_transform);
        code.Add(_children);
        code.Add(_parentFrame);
        code.Add(_group);
        code.Add(_name);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
