using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpMinMaxQuadTreeMinMaxLevel : IHavokObject, IEquatable<hknpMinMaxQuadTreeMinMaxLevel?>
{
    public virtual uint Signature => 0;
    public List<uint> _minMaxData;
    public ushort _xRes;
    public ushort _zRes;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _minMaxData = des.ReadUInt32Array(br);
        _xRes = br.ReadUInt16();
        _zRes = br.ReadUInt16();
        br.Position += 4; // padding
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteUInt32Array(bw, _minMaxData);
        bw.WriteUInt16(_xRes);
        bw.WriteUInt16(_zRes);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumberArray(xe, nameof(_minMaxData), _minMaxData);
        xs.WriteNumber(xe, nameof(_xRes), _xRes);
        xs.WriteNumber(xe, nameof(_zRes), _zRes);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpMinMaxQuadTreeMinMaxLevel);
    }
    public bool Equals(hknpMinMaxQuadTreeMinMaxLevel? other)
    {
        return other is not null && _minMaxData.Equals(other._minMaxData) && _xRes.Equals(other._xRes) && _zRes.Equals(other._zRes) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_minMaxData);
        code.Add(_xRes);
        code.Add(_zRes);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
