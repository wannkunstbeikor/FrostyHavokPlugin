using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxEnvironment : hkReferencedObject, IEquatable<hkxEnvironment?>
{
    public override uint Signature => 0;
    public List<hkxEnvironmentVariable> _variables;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _variables = des.ReadClassArray<hkxEnvironmentVariable>(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteClassArray<hkxEnvironmentVariable>(bw, _variables);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClassArray<hkxEnvironmentVariable>(xe, nameof(_variables), _variables);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkxEnvironment);
    }
    public bool Equals(hkxEnvironment? other)
    {
        return other is not null && _variables.Equals(other._variables) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_variables);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
