using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpCompressedMeshShape : hknpCompositeShape
{
    public override uint Signature => 0;
    public hknpCompressedMeshShapeData? _data;
    public hkBitField? _quadIsFlat;
    public hkBitField? _triangleIsInternal;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _data = des.ReadClassPointer<hknpCompressedMeshShapeData>(br);
        _quadIsFlat = new hkBitField();
        _quadIsFlat.Read(des, br);
        _triangleIsInternal = new hkBitField();
        _triangleIsInternal.Read(des, br);
        br.Position += 8; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteClassPointer<hknpCompressedMeshShapeData>(bw, _data);
        _quadIsFlat.Write(s, bw);
        _triangleIsInternal.Write(s, bw);
        for (int i = 0; i < 8; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClassPointer(xe, nameof(_data), _data);
        xs.WriteClass(xe, nameof(_quadIsFlat), _quadIsFlat);
        xs.WriteClass(xe, nameof(_triangleIsInternal), _triangleIsInternal);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpCompressedMeshShape other && base.Equals(other) && _data == other._data && _quadIsFlat == other._quadIsFlat && _triangleIsInternal == other._triangleIsInternal && Signature == other.Signature;
    }
    public static bool operator ==(hknpCompressedMeshShape? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpCompressedMeshShape? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_data);
        code.Add(_quadIsFlat);
        code.Add(_triangleIsInternal);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
