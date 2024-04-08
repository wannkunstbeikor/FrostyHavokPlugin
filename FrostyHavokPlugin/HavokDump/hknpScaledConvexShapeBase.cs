using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpScaledConvexShapeBase : hknpShape, IEquatable<hknpScaledConvexShapeBase?>
{
    public override uint Signature => 0;
    public hknpConvexShape _childShape;
    public int _childShapeSize;
    public Vector4 _scale;
    public Vector4 _translation;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _childShape = des.ReadClassPointer<hknpConvexShape>(br);
        _childShapeSize = br.ReadInt32();
        br.Position += 4; // padding
        _scale = des.ReadVector4(br);
        _translation = des.ReadVector4(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteClassPointer<hknpConvexShape>(bw, _childShape);
        bw.WriteInt32(_childShapeSize);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
        s.WriteVector4(bw, _scale);
        s.WriteVector4(bw, _translation);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClassPointer(xe, nameof(_childShape), _childShape);
        xs.WriteNumber(xe, nameof(_childShapeSize), _childShapeSize);
        xs.WriteVector4(xe, nameof(_scale), _scale);
        xs.WriteVector4(xe, nameof(_translation), _translation);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpScaledConvexShapeBase);
    }
    public bool Equals(hknpScaledConvexShapeBase? other)
    {
        return other is not null && _childShape.Equals(other._childShape) && _childShapeSize.Equals(other._childShapeSize) && _scale.Equals(other._scale) && _translation.Equals(other._translation) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_childShape);
        code.Add(_childShapeSize);
        code.Add(_scale);
        code.Add(_translation);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
