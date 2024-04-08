using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkpBridgeAtoms : IHavokObject, IEquatable<hkpBridgeAtoms?>
{
    public virtual uint Signature => 0;
    public hkpBridgeConstraintAtom _bridgeAtom;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _bridgeAtom = new hkpBridgeConstraintAtom();
        _bridgeAtom.Read(des, br);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        _bridgeAtom.Write(s, bw);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClass(xe, nameof(_bridgeAtom), _bridgeAtom);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkpBridgeAtoms);
    }
    public bool Equals(hkpBridgeAtoms? other)
    {
        return other is not null && _bridgeAtom.Equals(other._bridgeAtom) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_bridgeAtom);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
