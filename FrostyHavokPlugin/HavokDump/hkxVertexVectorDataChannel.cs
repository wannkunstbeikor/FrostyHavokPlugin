using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxVertexVectorDataChannel : hkReferencedObject, IEquatable<hkxVertexVectorDataChannel?>
{
    public override uint Signature => 0;
    public List<float> _perVertexVectors;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _perVertexVectors = des.ReadSingleArray(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteSingleArray(bw, _perVertexVectors);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteFloatArray(xe, nameof(_perVertexVectors), _perVertexVectors);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkxVertexVectorDataChannel);
    }
    public bool Equals(hkxVertexVectorDataChannel? other)
    {
        return other is not null && _perVertexVectors.Equals(other._perVertexVectors) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_perVertexVectors);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
