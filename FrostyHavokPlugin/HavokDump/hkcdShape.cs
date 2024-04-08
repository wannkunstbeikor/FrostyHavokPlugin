using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkcdShape : hkReferencedObject, IEquatable<hkcdShape?>
{
    public override uint Signature => 0;
    // TYPE_ENUM TYPE_UINT8 _type
    public hkcdShapeDispatchType_ShapeDispatchTypeEnum _dispatchType;
    public byte _bitsPerKey;
    public hkcdShapeInfoCodecType_ShapeInfoCodecTypeEnum _shapeInfoCodecType;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        br.Position += 1; // padding
        _dispatchType = (hkcdShapeDispatchType_ShapeDispatchTypeEnum)br.ReadByte();
        _bitsPerKey = br.ReadByte();
        _shapeInfoCodecType = (hkcdShapeInfoCodecType_ShapeInfoCodecTypeEnum)br.ReadByte();
        br.Position += 4; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        for (int i = 0; i < 1; i++) bw.WriteByte(0); // padding
        bw.WriteByte((byte)_dispatchType);
        bw.WriteByte(_bitsPerKey);
        bw.WriteByte((byte)_shapeInfoCodecType);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteEnum<hkcdShapeDispatchType_ShapeDispatchTypeEnum, byte>(xe, nameof(_dispatchType), (byte)_dispatchType);
        xs.WriteNumber(xe, nameof(_bitsPerKey), _bitsPerKey);
        xs.WriteEnum<hkcdShapeInfoCodecType_ShapeInfoCodecTypeEnum, byte>(xe, nameof(_shapeInfoCodecType), (byte)_shapeInfoCodecType);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkcdShape);
    }
    public bool Equals(hkcdShape? other)
    {
        return other is not null && _dispatchType.Equals(other._dispatchType) && _bitsPerKey.Equals(other._bitsPerKey) && _shapeInfoCodecType.Equals(other._shapeInfoCodecType) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_dispatchType);
        code.Add(_bitsPerKey);
        code.Add(_shapeInfoCodecType);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
