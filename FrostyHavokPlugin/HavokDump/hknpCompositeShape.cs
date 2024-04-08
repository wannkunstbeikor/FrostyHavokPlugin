using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpCompositeShape : hknpShape, IEquatable<hknpCompositeShape?>
{
    public override uint Signature => 0;
    public hknpSparseCompactMapunsignedshort _edgeWeldingMap;
    public uint _shapeTagCodecInfo;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _edgeWeldingMap = new hknpSparseCompactMapunsignedshort();
        _edgeWeldingMap.Read(des, br);
        _shapeTagCodecInfo = br.ReadUInt32();
        br.Position += 4; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        _edgeWeldingMap.Write(s, bw);
        bw.WriteUInt32(_shapeTagCodecInfo);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClass(xe, nameof(_edgeWeldingMap), _edgeWeldingMap);
        xs.WriteNumber(xe, nameof(_shapeTagCodecInfo), _shapeTagCodecInfo);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpCompositeShape);
    }
    public bool Equals(hknpCompositeShape? other)
    {
        return other is not null && _edgeWeldingMap.Equals(other._edgeWeldingMap) && _shapeTagCodecInfo.Equals(other._shapeTagCodecInfo) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_edgeWeldingMap);
        code.Add(_shapeTagCodecInfo);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
