using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkPackedVector8_3 : IHavokObject, IEquatable<hkPackedVector8_3?>
{
    public virtual uint Signature => 0;
    public sbyte[] _values = new sbyte[4];
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _values = des.ReadSByteCStyleArray(br, 4);
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteSByteCStyleArray(bw, _values);
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumberArray(xe, nameof(_values), _values);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkPackedVector8_3);
    }
    public bool Equals(hkPackedVector8_3? other)
    {
        return other is not null && _values.Equals(other._values) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_values);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
