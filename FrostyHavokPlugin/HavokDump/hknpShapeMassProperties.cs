using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpShapeMassProperties : hkReferencedObject, IEquatable<hknpShapeMassProperties?>
{
    public override uint Signature => 0;
    public hkCompressedMassProperties _compressedMassProperties;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _compressedMassProperties = new hkCompressedMassProperties();
        _compressedMassProperties.Read(des, br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        _compressedMassProperties.Write(s, bw);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClass(xe, nameof(_compressedMassProperties), _compressedMassProperties);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpShapeMassProperties);
    }
    public bool Equals(hknpShapeMassProperties? other)
    {
        return other is not null && _compressedMassProperties.Equals(other._compressedMassProperties) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_compressedMassProperties);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
