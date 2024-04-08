using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpBinaryAction : hknpAction, IEquatable<hknpBinaryAction?>
{
    public override uint Signature => 0;
    public uint _bodyA;
    public uint _bodyB;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _bodyA = br.ReadUInt32();
        _bodyB = br.ReadUInt32();
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteUInt32(_bodyA);
        bw.WriteUInt32(_bodyB);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteNumber(xe, nameof(_bodyA), _bodyA);
        xs.WriteNumber(xe, nameof(_bodyB), _bodyB);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpBinaryAction);
    }
    public bool Equals(hknpBinaryAction? other)
    {
        return other is not null && _bodyA.Equals(other._bodyA) && _bodyB.Equals(other._bodyB) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_bodyA);
        code.Add(_bodyB);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
