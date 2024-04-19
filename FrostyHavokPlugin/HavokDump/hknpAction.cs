using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpAction : hkReferencedObject
{
    public override uint Signature => 0;
    public ulong _userData;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _userData = br.ReadUInt64();
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteUInt64(_userData);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteNumber(xe, nameof(_userData), _userData);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpAction other && base.Equals(other) && _userData == other._userData && Signature == other.Signature;
    }
    public static bool operator ==(hknpAction? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpAction? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_userData);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
