using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpCompressedHeightFieldShape : hknpHeightFieldShape, IEquatable<hknpCompressedHeightFieldShape?>
{
    public override uint Signature => 0;
    public List<ushort> _storage;
    public List<ushort> _shapeTags;
    public bool _triangleFlip;
    public float _offset;
    public float _scale;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _storage = des.ReadUInt16Array(br);
        _shapeTags = des.ReadUInt16Array(br);
        _triangleFlip = br.ReadBoolean();
        br.Position += 3; // padding
        _offset = br.ReadSingle();
        _scale = br.ReadSingle();
        br.Position += 4; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteUInt16Array(bw, _storage);
        s.WriteUInt16Array(bw, _shapeTags);
        bw.WriteBoolean(_triangleFlip);
        for (int i = 0; i < 3; i++) bw.WriteByte(0); // padding
        bw.WriteSingle(_offset);
        bw.WriteSingle(_scale);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteNumberArray(xe, nameof(_storage), _storage);
        xs.WriteNumberArray(xe, nameof(_shapeTags), _shapeTags);
        xs.WriteBoolean(xe, nameof(_triangleFlip), _triangleFlip);
        xs.WriteFloat(xe, nameof(_offset), _offset);
        xs.WriteFloat(xe, nameof(_scale), _scale);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpCompressedHeightFieldShape);
    }
    public bool Equals(hknpCompressedHeightFieldShape? other)
    {
        return other is not null && _storage.Equals(other._storage) && _shapeTags.Equals(other._shapeTags) && _triangleFlip.Equals(other._triangleFlip) && _offset.Equals(other._offset) && _scale.Equals(other._scale) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_storage);
        code.Add(_shapeTags);
        code.Add(_triangleFlip);
        code.Add(_offset);
        code.Add(_scale);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
