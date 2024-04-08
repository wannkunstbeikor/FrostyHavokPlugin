using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpLinearParametricCurve : hkpParametricCurve, IEquatable<hkpLinearParametricCurve?>
{
    public override uint Signature => 0;
    public float _smoothingFactor;
    public bool _closedLoop;
    public Vector4 _dirNotParallelToTangentAlongWholePath;
    public List<Vector4> _points;
    public List<float> _distance;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _smoothingFactor = br.ReadSingle();
        _closedLoop = br.ReadBoolean();
        br.Position += 11; // padding
        _dirNotParallelToTangentAlongWholePath = des.ReadVector4(br);
        _points = des.ReadVector4Array(br);
        _distance = des.ReadSingleArray(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteSingle(_smoothingFactor);
        bw.WriteBoolean(_closedLoop);
        for (int i = 0; i < 11; i++) bw.WriteByte(0); // padding
        s.WriteVector4(bw, _dirNotParallelToTangentAlongWholePath);
        s.WriteVector4Array(bw, _points);
        s.WriteSingleArray(bw, _distance);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteFloat(xe, nameof(_smoothingFactor), _smoothingFactor);
        xs.WriteBoolean(xe, nameof(_closedLoop), _closedLoop);
        xs.WriteVector4(xe, nameof(_dirNotParallelToTangentAlongWholePath), _dirNotParallelToTangentAlongWholePath);
        xs.WriteVector4Array(xe, nameof(_points), _points);
        xs.WriteFloatArray(xe, nameof(_distance), _distance);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkpLinearParametricCurve);
    }
    public bool Equals(hkpLinearParametricCurve? other)
    {
        return other is not null && _smoothingFactor.Equals(other._smoothingFactor) && _closedLoop.Equals(other._closedLoop) && _dirNotParallelToTangentAlongWholePath.Equals(other._dirNotParallelToTangentAlongWholePath) && _points.Equals(other._points) && _distance.Equals(other._distance) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_smoothingFactor);
        code.Add(_closedLoop);
        code.Add(_dirNotParallelToTangentAlongWholePath);
        code.Add(_points);
        code.Add(_distance);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
