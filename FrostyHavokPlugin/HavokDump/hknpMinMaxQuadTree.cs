using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpMinMaxQuadTree : IHavokObject
{
    public virtual uint Signature => 0;
    public List<hknpMinMaxQuadTreeMinMaxLevel?> _coarseTreeData = new();
    public Vector4 _offset;
    public float _multiplier;
    public float _invMultiplier;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _coarseTreeData = des.ReadClassArray<hknpMinMaxQuadTreeMinMaxLevel>(br);
        _offset = des.ReadVector4(br);
        _multiplier = br.ReadSingle();
        _invMultiplier = br.ReadSingle();
        br.Position += 8; // padding
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteClassArray<hknpMinMaxQuadTreeMinMaxLevel>(bw, _coarseTreeData);
        s.WriteVector4(bw, _offset);
        bw.WriteSingle(_multiplier);
        bw.WriteSingle(_invMultiplier);
        for (int i = 0; i < 8; i++) bw.WriteByte(0); // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClassArray<hknpMinMaxQuadTreeMinMaxLevel>(xe, nameof(_coarseTreeData), _coarseTreeData);
        xs.WriteVector4(xe, nameof(_offset), _offset);
        xs.WriteFloat(xe, nameof(_multiplier), _multiplier);
        xs.WriteFloat(xe, nameof(_invMultiplier), _invMultiplier);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpMinMaxQuadTree other && _coarseTreeData.SequenceEqual(other._coarseTreeData) && _offset == other._offset && _multiplier == other._multiplier && _invMultiplier == other._invMultiplier && Signature == other.Signature;
    }
    public static bool operator ==(hknpMinMaxQuadTree? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpMinMaxQuadTree? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_coarseTreeData);
        code.Add(_offset);
        code.Add(_multiplier);
        code.Add(_invMultiplier);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
