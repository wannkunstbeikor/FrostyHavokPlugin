using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkCompressedMassProperties : IHavokObject, IEquatable<hkCompressedMassProperties?>
{
    public virtual uint Signature => 0;
    public short[] _centerOfMass = new short[4];
    public short[] _inertia = new short[4];
    public short[] _majorAxisSpace = new short[4];
    public float _mass;
    public float _volume;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _centerOfMass = des.ReadInt16CStyleArray(br, 4);
        _inertia = des.ReadInt16CStyleArray(br, 4);
        _majorAxisSpace = des.ReadInt16CStyleArray(br, 4);
        _mass = br.ReadSingle();
        _volume = br.ReadSingle();
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteInt16CStyleArray(bw, _centerOfMass);
        s.WriteInt16CStyleArray(bw, _inertia);
        s.WriteInt16CStyleArray(bw, _majorAxisSpace);
        bw.WriteSingle(_mass);
        bw.WriteSingle(_volume);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumberArray(xe, nameof(_centerOfMass), _centerOfMass);
        xs.WriteNumberArray(xe, nameof(_inertia), _inertia);
        xs.WriteNumberArray(xe, nameof(_majorAxisSpace), _majorAxisSpace);
        xs.WriteFloat(xe, nameof(_mass), _mass);
        xs.WriteFloat(xe, nameof(_volume), _volume);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkCompressedMassProperties);
    }
    public bool Equals(hkCompressedMassProperties? other)
    {
        return other is not null && _centerOfMass.Equals(other._centerOfMass) && _inertia.Equals(other._inertia) && _majorAxisSpace.Equals(other._majorAxisSpace) && _mass.Equals(other._mass) && _volume.Equals(other._volume) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_centerOfMass);
        code.Add(_inertia);
        code.Add(_majorAxisSpace);
        code.Add(_mass);
        code.Add(_volume);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
