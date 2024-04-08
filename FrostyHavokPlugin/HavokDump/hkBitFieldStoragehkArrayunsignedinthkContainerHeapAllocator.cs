using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkBitFieldStoragehkArrayunsignedinthkContainerHeapAllocator : IHavokObject, IEquatable<hkBitFieldStoragehkArrayunsignedinthkContainerHeapAllocator?>
{
    public virtual uint Signature => 0;
    public List<uint> _words;
    public int _numBits;
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _words = des.ReadUInt32Array(br);
        _numBits = br.ReadInt32();
        br.Position += 4; // padding
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        s.WriteUInt32Array(bw, _words);
        bw.WriteInt32(_numBits);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteNumberArray(xe, nameof(_words), _words);
        xs.WriteNumber(xe, nameof(_numBits), _numBits);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hkBitFieldStoragehkArrayunsignedinthkContainerHeapAllocator);
    }
    public bool Equals(hkBitFieldStoragehkArrayunsignedinthkContainerHeapAllocator? other)
    {
        return other is not null && _words.Equals(other._words) && _numBits.Equals(other._numBits) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_words);
        code.Add(_numBits);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
