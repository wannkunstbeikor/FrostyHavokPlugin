using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxTextureFile : hkReferencedObject
{
    public override uint Signature => 0;
    public string _filename = string.Empty;
    public string _name = string.Empty;
    public string _originalFilename = string.Empty;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _filename = des.ReadStringPointer(br);
        _name = des.ReadStringPointer(br);
        _originalFilename = des.ReadStringPointer(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteStringPointer(bw, _filename);
        s.WriteStringPointer(bw, _name);
        s.WriteStringPointer(bw, _originalFilename);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteString(xe, nameof(_filename), _filename);
        xs.WriteString(xe, nameof(_name), _name);
        xs.WriteString(xe, nameof(_originalFilename), _originalFilename);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkxTextureFile other && base.Equals(other) && _filename == other._filename && _name == other._name && _originalFilename == other._originalFilename && Signature == other.Signature;
    }
    public static bool operator ==(hkxTextureFile? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkxTextureFile? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_filename);
        code.Add(_name);
        code.Add(_originalFilename);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
