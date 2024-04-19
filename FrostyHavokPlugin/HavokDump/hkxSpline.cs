using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxSpline : hkReferencedObject
{
    public override uint Signature => 0;
    public List<hkxSplineControlPoint?> _controlPoints = new();
    public bool _isClosed;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _controlPoints = des.ReadClassArray<hkxSplineControlPoint>(br);
        _isClosed = br.ReadBoolean();
        br.Position += 7; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteClassArray<hkxSplineControlPoint>(bw, _controlPoints);
        bw.WriteBoolean(_isClosed);
        for (int i = 0; i < 7; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClassArray<hkxSplineControlPoint>(xe, nameof(_controlPoints), _controlPoints);
        xs.WriteBoolean(xe, nameof(_isClosed), _isClosed);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkxSpline other && base.Equals(other) && _controlPoints.SequenceEqual(other._controlPoints) && _isClosed == other._isClosed && Signature == other.Signature;
    }
    public static bool operator ==(hkxSpline? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkxSpline? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_controlPoints);
        code.Add(_isClosed);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
