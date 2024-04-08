using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxMesh : hkReferencedObject, IEquatable<hkxMesh?>
{
    public override uint Signature => 0;
    public List<hkxMeshSection> _sections;
    public List<hkxMeshUserChannelInfo> _userChannelInfos;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _sections = des.ReadClassPointerArray<hkxMeshSection>(br);
        _userChannelInfos = des.ReadClassPointerArray<hkxMeshUserChannelInfo>(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteClassPointerArray<hkxMeshSection>(bw, _sections);
        s.WriteClassPointerArray<hkxMeshUserChannelInfo>(bw, _userChannelInfos);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClassPointerArray<hkxMeshSection>(xe, nameof(_sections), _sections);
        xs.WriteClassPointerArray<hkxMeshUserChannelInfo>(xe, nameof(_userChannelInfos), _userChannelInfos);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkxMesh);
    }
    public bool Equals(hkxMesh? other)
    {
        return other is not null && _sections.Equals(other._sections) && _userChannelInfos.Equals(other._userChannelInfos) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_sections);
        code.Add(_userChannelInfos);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
