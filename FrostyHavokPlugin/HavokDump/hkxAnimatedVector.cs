using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxAnimatedVector : hkReferencedObject, IEquatable<hkxAnimatedVector?>
{
    public override uint Signature => 0;
    public List<float> _vectors;
    public hkxAttribute_Hint _hint;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _vectors = des.ReadSingleArray(br);
        _hint = (hkxAttribute_Hint)br.ReadByte();
        br.Position += 7; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteSingleArray(bw, _vectors);
        bw.WriteByte((byte)_hint);
        for (int i = 0; i < 7; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteFloatArray(xe, nameof(_vectors), _vectors);
        xs.WriteEnum<hkxAttribute_Hint, byte>(xe, nameof(_hint), (byte)_hint);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkxAnimatedVector);
    }
    public bool Equals(hkxAnimatedVector? other)
    {
        return other is not null && _vectors.Equals(other._vectors) && _hint.Equals(other._hint) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_vectors);
        code.Add(_hint);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
