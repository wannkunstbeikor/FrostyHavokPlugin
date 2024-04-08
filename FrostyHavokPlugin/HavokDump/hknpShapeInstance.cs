using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpShapeInstance : IHavokObject, IEquatable<hknpShapeInstance?>
{
    public virtual uint Signature => 0;
    public Matrix4 _transform;
    public Vector4 _scale;
    public hknpShape _shape;
    public ushort _shapeTag;
    public ushort _destructionTag;
    public byte[] _padding = new byte[30];
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _transform = des.ReadTransform(br);
        _scale = des.ReadVector4(br);
        _shape = des.ReadClassPointer<hknpShape>(br);
        _shapeTag = br.ReadUInt16();
        _destructionTag = br.ReadUInt16();
        _padding = des.ReadByteCStyleArray(br, 30);
        br.Position += 6; // padding
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteTransform(bw, _transform);
        s.WriteVector4(bw, _scale);
        s.WriteClassPointer<hknpShape>(bw, _shape);
        bw.WriteUInt16(_shapeTag);
        bw.WriteUInt16(_destructionTag);
        s.WriteByteCStyleArray(bw, _padding);
        for (int i = 0; i < 6; i++) bw.WriteByte(0); // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteTransform(xe, nameof(_transform), _transform);
        xs.WriteVector4(xe, nameof(_scale), _scale);
        xs.WriteClassPointer(xe, nameof(_shape), _shape);
        xs.WriteNumber(xe, nameof(_shapeTag), _shapeTag);
        xs.WriteNumber(xe, nameof(_destructionTag), _destructionTag);
        xs.WriteNumberArray(xe, nameof(_padding), _padding);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpShapeInstance);
    }
    public bool Equals(hknpShapeInstance? other)
    {
        return other is not null && _transform.Equals(other._transform) && _scale.Equals(other._scale) && _shape.Equals(other._shape) && _shapeTag.Equals(other._shapeTag) && _destructionTag.Equals(other._destructionTag) && _padding.Equals(other._padding) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_transform);
        code.Add(_scale);
        code.Add(_shape);
        code.Add(_shapeTag);
        code.Add(_destructionTag);
        code.Add(_padding);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
