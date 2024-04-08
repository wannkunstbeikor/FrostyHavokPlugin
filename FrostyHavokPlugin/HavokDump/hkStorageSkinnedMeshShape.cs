using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkStorageSkinnedMeshShape : hkSkinnedMeshShape, IEquatable<hkStorageSkinnedMeshShape?>
{
    public override uint Signature => 0;
    public List<short> _bonesBuffer;
    public List<hkSkinnedMeshShapeBoneSet> _boneSets;
    public List<hkSkinnedMeshShapeBoneSection> _boneSections;
    public List<hkSkinnedMeshShapePart> _parts;
    public string _name;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _bonesBuffer = des.ReadInt16Array(br);
        _boneSets = des.ReadClassArray<hkSkinnedMeshShapeBoneSet>(br);
        _boneSections = des.ReadClassArray<hkSkinnedMeshShapeBoneSection>(br);
        _parts = des.ReadClassArray<hkSkinnedMeshShapePart>(br);
        _name = des.ReadStringPointer(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteInt16Array(bw, _bonesBuffer);
        s.WriteClassArray<hkSkinnedMeshShapeBoneSet>(bw, _boneSets);
        s.WriteClassArray<hkSkinnedMeshShapeBoneSection>(bw, _boneSections);
        s.WriteClassArray<hkSkinnedMeshShapePart>(bw, _parts);
        s.WriteStringPointer(bw, _name);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteNumberArray(xe, nameof(_bonesBuffer), _bonesBuffer);
        xs.WriteClassArray<hkSkinnedMeshShapeBoneSet>(xe, nameof(_boneSets), _boneSets);
        xs.WriteClassArray<hkSkinnedMeshShapeBoneSection>(xe, nameof(_boneSections), _boneSections);
        xs.WriteClassArray<hkSkinnedMeshShapePart>(xe, nameof(_parts), _parts);
        xs.WriteString(xe, nameof(_name), _name);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkStorageSkinnedMeshShape);
    }
    public bool Equals(hkStorageSkinnedMeshShape? other)
    {
        return other is not null && _bonesBuffer.Equals(other._bonesBuffer) && _boneSets.Equals(other._boneSets) && _boneSections.Equals(other._boneSections) && _parts.Equals(other._parts) && _name.Equals(other._name) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_bonesBuffer);
        code.Add(_boneSets);
        code.Add(_boneSections);
        code.Add(_parts);
        code.Add(_name);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
