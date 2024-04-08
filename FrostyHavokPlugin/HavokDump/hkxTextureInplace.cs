using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkxTextureInplace : hkReferencedObject, IEquatable<hkxTextureInplace?>
{
    public override uint Signature => 0;
    public sbyte[] _fileType = new sbyte[4];
    public List<byte> _data;
    public string _name;
    public string _originalFilename;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _fileType = des.ReadSByteCStyleArray(br, 4);
        br.Position += 4; // padding
        _data = des.ReadByteArray(br);
        _name = des.ReadStringPointer(br);
        _originalFilename = des.ReadStringPointer(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteSByteCStyleArray(bw, _fileType);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
        s.WriteByteArray(bw, _data);
        s.WriteStringPointer(bw, _name);
        s.WriteStringPointer(bw, _originalFilename);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteNumberArray(xe, nameof(_fileType), _fileType);
        xs.WriteNumberArray(xe, nameof(_data), _data);
        xs.WriteString(xe, nameof(_name), _name);
        xs.WriteString(xe, nameof(_originalFilename), _originalFilename);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkxTextureInplace);
    }
    public bool Equals(hkxTextureInplace? other)
    {
        return other is not null && _fileType.Equals(other._fileType) && _data.Equals(other._data) && _name.Equals(other._name) && _originalFilename.Equals(other._originalFilename) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_fileType);
        code.Add(_data);
        code.Add(_name);
        code.Add(_originalFilename);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
