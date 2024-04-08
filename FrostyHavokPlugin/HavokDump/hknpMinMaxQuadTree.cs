using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpMinMaxQuadTree : IHavokObject, IEquatable<hknpMinMaxQuadTree?>
{
    public virtual uint Signature => 0;
    public List<hknpMinMaxQuadTreeMinMaxLevel> _coarseTreeData;
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
        return Equals(obj as hknpMinMaxQuadTree);
    }
    public bool Equals(hknpMinMaxQuadTree? other)
    {
        return other is not null && _coarseTreeData.Equals(other._coarseTreeData) && _offset.Equals(other._offset) && _multiplier.Equals(other._multiplier) && _invMultiplier.Equals(other._invMultiplier) && Signature == other.Signature;
    }
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
