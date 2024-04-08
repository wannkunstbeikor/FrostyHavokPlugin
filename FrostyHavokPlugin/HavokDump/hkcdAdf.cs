using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkcdAdf : IHavokObject, IEquatable<hkcdAdf?>
{
    public virtual uint Signature => 0;
    public float _accuracy;
    public hkAabb _domain;
    public Vector4 _origin;
    public Vector4 _scale;
    public float[] _range = new float[2];
    public List<uint> _nodes;
    public List<ushort> _voxels;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _accuracy = br.ReadSingle();
        br.Position += 12; // padding
        _domain = new hkAabb();
        _domain.Read(des, br);
        _origin = des.ReadVector4(br);
        _scale = des.ReadVector4(br);
        _range = des.ReadSingleCStyleArray(br, 2);
        _nodes = des.ReadUInt32Array(br);
        _voxels = des.ReadUInt16Array(br);
        br.Position += 8; // padding
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        bw.WriteSingle(_accuracy);
        for (int i = 0; i < 12; i++) bw.WriteByte(0); // padding
        _domain.Write(s, bw);
        s.WriteVector4(bw, _origin);
        s.WriteVector4(bw, _scale);
        s.WriteSingleCStyleArray(bw, _range);
        s.WriteUInt32Array(bw, _nodes);
        s.WriteUInt16Array(bw, _voxels);
        for (int i = 0; i < 8; i++) bw.WriteByte(0); // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteFloat(xe, nameof(_accuracy), _accuracy);
        xs.WriteClass(xe, nameof(_domain), _domain);
        xs.WriteVector4(xe, nameof(_origin), _origin);
        xs.WriteVector4(xe, nameof(_scale), _scale);
        xs.WriteFloatArray(xe, nameof(_range), _range);
        xs.WriteNumberArray(xe, nameof(_nodes), _nodes);
        xs.WriteNumberArray(xe, nameof(_voxels), _voxels);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkcdAdf);
    }
    public bool Equals(hkcdAdf? other)
    {
        return other is not null && _accuracy.Equals(other._accuracy) && _domain.Equals(other._domain) && _origin.Equals(other._origin) && _scale.Equals(other._scale) && _range.Equals(other._range) && _nodes.Equals(other._nodes) && _voxels.Equals(other._voxels) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_accuracy);
        code.Add(_domain);
        code.Add(_origin);
        code.Add(_scale);
        code.Add(_range);
        code.Add(_nodes);
        code.Add(_voxels);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
