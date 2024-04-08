using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hknpCompoundShape : hknpCompositeShape, IEquatable<hknpCompoundShape?>
{
    public override uint Signature => 0;
    public hkFreeListArrayhknpShapeInstancehkHandleshort32767hknpShapeInstanceIdDiscriminant8hknpShapeInstance _instances;
    public hkAabb _aabb;
    public bool _isMutable;
    // TYPE_STRUCT TYPE_VOID _mutationSignals
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _instances = new hkFreeListArrayhknpShapeInstancehkHandleshort32767hknpShapeInstanceIdDiscriminant8hknpShapeInstance();
        _instances.Read(des, br);
        br.Position += 8; // padding
        _aabb = new hkAabb();
        _aabb.Read(des, br);
        _isMutable = br.ReadBoolean();
        br.Position += 31; // padding
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        _instances.Write(s, bw);
        for (int i = 0; i < 8; i++) bw.WriteByte(0); // padding
        _aabb.Write(s, bw);
        bw.WriteBoolean(_isMutable);
        for (int i = 0; i < 31; i++) bw.WriteByte(0); // padding
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteClass(xe, nameof(_instances), _instances);
        xs.WriteClass(xe, nameof(_aabb), _aabb);
        xs.WriteBoolean(xe, nameof(_isMutable), _isMutable);
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as hknpCompoundShape);
    }
    public bool Equals(hknpCompoundShape? other)
    {
        return other is not null && _instances.Equals(other._instances) && _aabb.Equals(other._aabb) && _isMutable.Equals(other._isMutable) && Signature == other.Signature;
    }
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_instances);
        code.Add(_aabb);
        code.Add(_isMutable);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
