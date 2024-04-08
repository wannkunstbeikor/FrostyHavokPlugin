using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpPhysicsSceneData : hkReferencedObject, IEquatable<hknpPhysicsSceneData?>
{
    public override uint Signature => 0;
    public List<hknpPhysicsSystemData> _systemDatas;
    public hknpRefWorldCinfo _worldCinfo;
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
        return Equals(obj as hknpPhysicsSceneData);
    }
    public bool Equals(hknpPhysicsSceneData? other)
    {
        return other is not null && _systemDatas.Equals(other._systemDatas) && _worldCinfo.Equals(other._worldCinfo) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_systemDatas);
        code.Add(_worldCinfo);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
