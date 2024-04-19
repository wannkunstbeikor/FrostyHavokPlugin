using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpPhysicsSystemData : hkReferencedObject
{
    public override uint Signature => 0;
    public List<hknpMaterial?> _materials = new();
    public List<hknpMotionProperties?> _motionProperties = new();
    public List<hknpMotionCinfo?> _motionCinfos = new();
    public List<hknpBodyCinfo?> _bodyCinfos = new();
    public List<hknpConstraintCinfo?> _constraintCinfos = new();
    public List<hkReferencedObject?> _referencedObjects = new();
    public string _name = string.Empty;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _materials = des.ReadClassArray<hknpMaterial>(br);
        _motionProperties = des.ReadClassArray<hknpMotionProperties>(br);
        _motionCinfos = des.ReadClassArray<hknpMotionCinfo>(br);
        _bodyCinfos = des.ReadClassArray<hknpBodyCinfo>(br);
        _constraintCinfos = des.ReadClassArray<hknpConstraintCinfo>(br);
        _referencedObjects = des.ReadClassPointerArray<hkReferencedObject>(br);
        _name = des.ReadStringPointer(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteClassArray<hknpMaterial>(bw, _materials);
        s.WriteClassArray<hknpMotionProperties>(bw, _motionProperties);
        s.WriteClassArray<hknpMotionCinfo>(bw, _motionCinfos);
        s.WriteClassArray<hknpBodyCinfo>(bw, _bodyCinfos);
        s.WriteClassArray<hknpConstraintCinfo>(bw, _constraintCinfos);
        s.WriteClassPointerArray<hkReferencedObject>(bw, _referencedObjects);
        s.WriteStringPointer(bw, _name);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClassArray<hknpMaterial>(xe, nameof(_materials), _materials);
        xs.WriteClassArray<hknpMotionProperties>(xe, nameof(_motionProperties), _motionProperties);
        xs.WriteClassArray<hknpMotionCinfo>(xe, nameof(_motionCinfos), _motionCinfos);
        xs.WriteClassArray<hknpBodyCinfo>(xe, nameof(_bodyCinfos), _bodyCinfos);
        xs.WriteClassArray<hknpConstraintCinfo>(xe, nameof(_constraintCinfos), _constraintCinfos);
        xs.WriteClassPointerArray<hkReferencedObject>(xe, nameof(_referencedObjects), _referencedObjects);
        xs.WriteString(xe, nameof(_name), _name);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpPhysicsSystemData other && base.Equals(other) && _materials.SequenceEqual(other._materials) && _motionProperties.SequenceEqual(other._motionProperties) && _motionCinfos.SequenceEqual(other._motionCinfos) && _bodyCinfos.SequenceEqual(other._bodyCinfos) && _constraintCinfos.SequenceEqual(other._constraintCinfos) && _referencedObjects.SequenceEqual(other._referencedObjects) && _name == other._name && Signature == other.Signature;
    }
    public static bool operator ==(hknpPhysicsSystemData? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpPhysicsSystemData? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_materials);
        code.Add(_motionProperties);
        code.Add(_motionCinfos);
        code.Add(_bodyCinfos);
        code.Add(_constraintCinfos);
        code.Add(_referencedObjects);
        code.Add(_name);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
