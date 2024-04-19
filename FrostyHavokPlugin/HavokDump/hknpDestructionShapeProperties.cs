using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpDestructionShapeProperties : hkReferencedObject
{
    public override uint Signature => 0;
    public Matrix4 _worldFromShape;
    public bool _isHierarchicalCompound;
    public bool _hasDestructionShapes;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _worldFromShape = des.ReadTransform(br);
        _isHierarchicalCompound = br.ReadBoolean();
        _hasDestructionShapes = br.ReadBoolean();
        br.Position += 14; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        s.WriteTransform(bw, _worldFromShape);
        bw.WriteBoolean(_isHierarchicalCompound);
        bw.WriteBoolean(_hasDestructionShapes);
        for (int i = 0; i < 14; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteTransform(xe, nameof(_worldFromShape), _worldFromShape);
        xs.WriteBoolean(xe, nameof(_isHierarchicalCompound), _isHierarchicalCompound);
        xs.WriteBoolean(xe, nameof(_hasDestructionShapes), _hasDestructionShapes);
    }
    public override bool Equals(object? obj)
    {
        return obj is hknpDestructionShapeProperties other && base.Equals(other) && _worldFromShape == other._worldFromShape && _isHierarchicalCompound == other._isHierarchicalCompound && _hasDestructionShapes == other._hasDestructionShapes && Signature == other.Signature;
    }
    public static bool operator ==(hknpDestructionShapeProperties? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hknpDestructionShapeProperties? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_worldFromShape);
        code.Add(_isHierarchicalCompound);
        code.Add(_hasDestructionShapes);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
