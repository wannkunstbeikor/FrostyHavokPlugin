using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpTyremarksWheel : hkReferencedObject
{
    public override uint Signature => 0;
    public int _currentPosition;
    public int _numPoints;
    public List<hknpTyremarkPoint?> _tyremarkPoints = new();
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _currentPosition = br.ReadInt32();
        _numPoints = br.ReadInt32();
        _tyremarkPoints = des.ReadClassArray<hknpTyremarkPoint>(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteInt32(_currentPosition);
        bw.WriteInt32(_numPoints);
        s.WriteClassArray<hknpTyremarkPoint>(bw, _tyremarkPoints);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteNumber(xe, nameof(_currentPosition), _currentPosition);
        xs.WriteNumber(xe, nameof(_numPoints), _numPoints);
        xs.WriteClassArray<hknpTyremarkPoint>(xe, nameof(_tyremarkPoints), _tyremarkPoints);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpTyremarksWheel other && base.Equals(other) && _currentPosition == other._currentPosition && _numPoints == other._numPoints && _tyremarkPoints.SequenceEqual(other._tyremarkPoints) && Signature == other.Signature;
    }
    public static bool operator ==(hknpTyremarksWheel? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpTyremarksWheel? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_currentPosition);
        code.Add(_numPoints);
        code.Add(_tyremarkPoints);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
