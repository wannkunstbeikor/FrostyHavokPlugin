using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxBlobMeshShape : hkMeshShape, IEquatable<hkxBlobMeshShape?>
{
    public override uint Signature => 0;
    public hkxBlob _blob;
    public string _name;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _blob = new hkxBlob();
        _blob.Read(des, br);
        _name = des.ReadStringPointer(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        _blob.Write(s, bw);
        s.WriteStringPointer(bw, _name);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClass(xe, nameof(_blob), _blob);
        xs.WriteString(xe, nameof(_name), _name);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkxBlobMeshShape);
    }
    public bool Equals(hkxBlobMeshShape? other)
    {
        return other is not null && _blob.Equals(other._blob) && _name.Equals(other._name) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_blob);
        code.Add(_name);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
