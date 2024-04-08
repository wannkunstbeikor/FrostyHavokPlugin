using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpHeightFieldShape : hknpCompositeShape, IEquatable<hknpHeightFieldShape?>
{
    public override uint Signature => 0;
    public hkAabb _aabb;
    public Vector4 _floatToIntScale;
    public Vector4 _intToFloatScale;
    public int _intSizeX;
    public int _intSizeZ;
    public int _numBitsX;
    public int _numBitsZ;
    public hknpMinMaxQuadTree _minMaxTree;
    public int _minMaxTreeCoarseness;
    public bool _includeShapeKeyInSdfContacts;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _aabb = new hkAabb();
        _aabb.Read(des, br);
        _floatToIntScale = des.ReadVector4(br);
        _intToFloatScale = des.ReadVector4(br);
        _intSizeX = br.ReadInt32();
        _intSizeZ = br.ReadInt32();
        _numBitsX = br.ReadInt32();
        _numBitsZ = br.ReadInt32();
        _minMaxTree = new hknpMinMaxQuadTree();
        _minMaxTree.Read(des, br);
        _minMaxTreeCoarseness = br.ReadInt32();
        _includeShapeKeyInSdfContacts = br.ReadBoolean();
        br.Position += 11; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        _aabb.Write(s, bw);
        s.WriteVector4(bw, _floatToIntScale);
        s.WriteVector4(bw, _intToFloatScale);
        bw.WriteInt32(_intSizeX);
        bw.WriteInt32(_intSizeZ);
        bw.WriteInt32(_numBitsX);
        bw.WriteInt32(_numBitsZ);
        _minMaxTree.Write(s, bw);
        bw.WriteInt32(_minMaxTreeCoarseness);
        bw.WriteBoolean(_includeShapeKeyInSdfContacts);
        for (int i = 0; i < 11; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClass(xe, nameof(_aabb), _aabb);
        xs.WriteVector4(xe, nameof(_floatToIntScale), _floatToIntScale);
        xs.WriteVector4(xe, nameof(_intToFloatScale), _intToFloatScale);
        xs.WriteNumber(xe, nameof(_intSizeX), _intSizeX);
        xs.WriteNumber(xe, nameof(_intSizeZ), _intSizeZ);
        xs.WriteNumber(xe, nameof(_numBitsX), _numBitsX);
        xs.WriteNumber(xe, nameof(_numBitsZ), _numBitsZ);
        xs.WriteClass(xe, nameof(_minMaxTree), _minMaxTree);
        xs.WriteNumber(xe, nameof(_minMaxTreeCoarseness), _minMaxTreeCoarseness);
        xs.WriteBoolean(xe, nameof(_includeShapeKeyInSdfContacts), _includeShapeKeyInSdfContacts);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpHeightFieldShape);
    }
    public bool Equals(hknpHeightFieldShape? other)
    {
        return other is not null && _aabb.Equals(other._aabb) && _floatToIntScale.Equals(other._floatToIntScale) && _intToFloatScale.Equals(other._intToFloatScale) && _intSizeX.Equals(other._intSizeX) && _intSizeZ.Equals(other._intSizeZ) && _numBitsX.Equals(other._numBitsX) && _numBitsZ.Equals(other._numBitsZ) && _minMaxTree.Equals(other._minMaxTree) && _minMaxTreeCoarseness.Equals(other._minMaxTreeCoarseness) && _includeShapeKeyInSdfContacts.Equals(other._includeShapeKeyInSdfContacts) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_aabb);
        code.Add(_floatToIntScale);
        code.Add(_intToFloatScale);
        code.Add(_intSizeX);
        code.Add(_intSizeZ);
        code.Add(_numBitsX);
        code.Add(_numBitsZ);
        code.Add(_minMaxTree);
        code.Add(_minMaxTreeCoarseness);
        code.Add(_includeShapeKeyInSdfContacts);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
