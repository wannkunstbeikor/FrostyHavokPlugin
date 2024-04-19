using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpRefWorldCinfo : hkReferencedObject
{
    public override uint Signature => 0;
    public hknpWorldCinfo? _info;
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
        return obj is hknpRefWorldCinfo other && base.Equals(other) && _info == other._info && Signature == other.Signature;
    }
    public static bool operator ==(hknpRefWorldCinfo? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpRefWorldCinfo? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_info);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
