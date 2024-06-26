using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkcdStaticPvs : IHavokObject
{
    public virtual uint Signature => 0;
    public hkcdStaticTreeTreehkcdStaticTreeDynamicStorage6? _cells;
    public int _bytesPerCells;
    public int _cellsPerBlock;
    public List<byte> _pvs = new();
    public List<ushort> _map = new();
    public List<hkcdStaticPvsBlockHeader?> _blocks = new();
    public virtual void Read(PackFileDeserializer des, DataStream br)
    {
        _cells = new hkcdStaticTreeTreehkcdStaticTreeDynamicStorage6();
        _cells.Read(des, br);
        _bytesPerCells = br.ReadInt32();
        _cellsPerBlock = br.ReadInt32();
        _pvs = des.ReadByteArray(br);
        _map = des.ReadUInt16Array(br);
        _blocks = des.ReadClassArray<hkcdStaticPvsBlockHeader>(br);
        br.Position += 8; // padding
    }
    public virtual void Write(PackFileSerializer s, DataStream bw)
    {
        _cells.Write(s, bw);
        bw.WriteInt32(_bytesPerCells);
        bw.WriteInt32(_cellsPerBlock);
        s.WriteByteArray(bw, _pvs);
        s.WriteUInt16Array(bw, _map);
        s.WriteClassArray<hkcdStaticPvsBlockHeader>(bw, _blocks);
        for (int i = 0; i < 8; i++) bw.WriteByte(0); // padding
    }
    public virtual void WriteXml(XmlSerializer xs, XElement xe)
    {
        xs.WriteClass(xe, nameof(_cells), _cells);
        xs.WriteNumber(xe, nameof(_bytesPerCells), _bytesPerCells);
        xs.WriteNumber(xe, nameof(_cellsPerBlock), _cellsPerBlock);
        xs.WriteNumberArray(xe, nameof(_pvs), _pvs);
        xs.WriteNumberArray(xe, nameof(_map), _map);
        xs.WriteClassArray<hkcdStaticPvsBlockHeader>(xe, nameof(_blocks), _blocks);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkcdStaticPvs other && _cells == other._cells && _bytesPerCells == other._bytesPerCells && _cellsPerBlock == other._cellsPerBlock && _pvs.SequenceEqual(other._pvs) && _map.SequenceEqual(other._map) && _blocks.SequenceEqual(other._blocks) && Signature == other.Signature;
    }
    public static bool operator ==(hkcdStaticPvs? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkcdStaticPvs? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_cells);
        code.Add(_bytesPerCells);
        code.Add(_cellsPerBlock);
        code.Add(_pvs);
        code.Add(_map);
        code.Add(_blocks);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
