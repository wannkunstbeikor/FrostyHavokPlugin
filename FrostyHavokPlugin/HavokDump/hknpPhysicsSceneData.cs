using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpPhysicsSceneData : hkReferencedObject
{
    public override uint Signature => 0;
    public List<hknpPhysicsSystemData?> _systemDatas = new();
    public hknpRefWorldCinfo? _worldCinfo;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _systemDatas = des.ReadClassPointerArray<hknpPhysicsSystemData>(br);
        _worldCinfo = des.ReadClassPointer<hknpRefWorldCinfo>(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteClassPointerArray<hknpPhysicsSystemData>(bw, _systemDatas);
        s.WriteClassPointer<hknpRefWorldCinfo>(bw, _worldCinfo);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClassPointerArray<hknpPhysicsSystemData>(xe, nameof(_systemDatas), _systemDatas);
        xs.WriteClassPointer(xe, nameof(_worldCinfo), _worldCinfo);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpPhysicsSceneData other && base.Equals(other) && _systemDatas.SequenceEqual(other._systemDatas) && _worldCinfo == other._worldCinfo && Signature == other.Signature;
    }
    public static bool operator ==(hknpPhysicsSceneData? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpPhysicsSceneData? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_systemDatas);
        code.Add(_worldCinfo);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
