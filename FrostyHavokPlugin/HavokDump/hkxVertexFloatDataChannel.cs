using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxVertexFloatDataChannel : hkReferencedObject, IEquatable<hkxVertexFloatDataChannel?>
{
    public override uint Signature => 0;
    public List<float> _perVertexFloats;
    public hkxVertexFloatDataChannel_VertexFloatDimensions _dimensions;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _perVertexFloats = des.ReadSingleArray(br);
        _dimensions = (hkxVertexFloatDataChannel_VertexFloatDimensions)br.ReadByte();
        br.Position += 7; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteSingleArray(bw, _perVertexFloats);
        bw.WriteByte((byte)_dimensions);
        for (int i = 0; i < 7; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteFloatArray(xe, nameof(_perVertexFloats), _perVertexFloats);
        xs.WriteEnum<hkxVertexFloatDataChannel_VertexFloatDimensions, byte>(xe, nameof(_dimensions), (byte)_dimensions);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkxVertexFloatDataChannel);
    }
    public bool Equals(hkxVertexFloatDataChannel? other)
    {
        return other is not null && _perVertexFloats.Equals(other._perVertexFloats) && _dimensions.Equals(other._dimensions) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_perVertexFloats);
        code.Add(_dimensions);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
