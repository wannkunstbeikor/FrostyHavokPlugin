using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpBodyQualityLibrary : hkReferencedObject
{
    public override uint Signature => 0;
    // TYPE_POINTER TYPE_VOID _qualityModifiedSignal
    public hknpBodyQuality?[] _qualities = new hknpBodyQuality?[32];
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        br.Position += 16; // padding
        _qualities = des.ReadStructCStyleArray<hknpBodyQuality>(br, 32);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        for (int i = 0; i < 16; i++) bw.WriteByte(0); // padding
        s.WriteStructCStyleArray<hknpBodyQuality>(bw, _qualities);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClassArray(xe, nameof(_qualities), _qualities);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpBodyQualityLibrary other && base.Equals(other) && _qualities == other._qualities && Signature == other.Signature;
    }
    public static bool operator ==(hknpBodyQualityLibrary? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpBodyQualityLibrary? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_qualities);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
