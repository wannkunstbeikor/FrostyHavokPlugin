using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpDefaultBroadPhaseConfig : hknpBroadPhaseConfig, IEquatable<hknpDefaultBroadPhaseConfig?>
{
    public override uint Signature => 0;
    public hknpBroadPhaseConfigLayer[] _layers = new hknpBroadPhaseConfigLayer[4];
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _layers = des.ReadStructCStyleArray<hknpBroadPhaseConfigLayer>(br, 4);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteStructCStyleArray<hknpBroadPhaseConfigLayer>(bw, _layers);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClassArray(xe, nameof(_layers), _layers);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpDefaultBroadPhaseConfig);
    }
    public bool Equals(hknpDefaultBroadPhaseConfig? other)
    {
        return other is not null && _layers.Equals(other._layers) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_layers);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
