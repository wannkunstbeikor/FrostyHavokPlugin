using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpRefWorldCinfo : hkReferencedObject, IEquatable<hknpRefWorldCinfo?>
{
    public override uint Signature => 0;
    public hknpWorldCinfo _info;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _info = new hknpWorldCinfo();
        _info.Read(des, br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        _info.Write(s, bw);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClass(xe, nameof(_info), _info);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpRefWorldCinfo);
    }
    public bool Equals(hknpRefWorldCinfo? other)
    {
        return other is not null && _info.Equals(other._info) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_info);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
