using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Frosty.Sdk.IO;
using FrostyHavokPlugin;
using FrostyHavokPlugin.Interfaces;
using OpenTK.Mathematics;
using Half = System.Half;
namespace hk;
public class hkAlignSceneToNodeOptions : hkReferencedObject
{
    public override uint Signature => 0;
    public bool _invert;
    public bool _transformPositionX;
    public bool _transformPositionY;
    public bool _transformPositionZ;
    public bool _transformRotation;
    public bool _transformScale;
    public bool _transformSkew;
    public int _keyframe;
    public string _nodeName = string.Empty;
    public override void Read(PackFileDeserializer des, DataStream br)
    {
        base.Read(des, br);
        _invert = br.ReadBoolean();
        _transformPositionX = br.ReadBoolean();
        _transformPositionY = br.ReadBoolean();
        _transformPositionZ = br.ReadBoolean();
        _transformRotation = br.ReadBoolean();
        _transformScale = br.ReadBoolean();
        _transformSkew = br.ReadBoolean();
        br.Position += 1; // padding
        _keyframe = br.ReadInt32();
        br.Position += 4; // padding
        _nodeName = des.ReadStringPointer(br);
    }
    public override void Write(PackFileSerializer s, DataStream bw)
    {
        base.Write(s, bw);
        bw.WriteBoolean(_invert);
        bw.WriteBoolean(_transformPositionX);
        bw.WriteBoolean(_transformPositionY);
        bw.WriteBoolean(_transformPositionZ);
        bw.WriteBoolean(_transformRotation);
        bw.WriteBoolean(_transformScale);
        bw.WriteBoolean(_transformSkew);
        for (int i = 0; i < 1; i++) bw.WriteByte(0); // padding
        bw.WriteInt32(_keyframe);
        for (int i = 0; i < 4; i++) bw.WriteByte(0); // padding
        s.WriteStringPointer(bw, _nodeName);
    }
    public override void WriteXml(XmlSerializer xs, XElement xe)
    {
        base.WriteXml(xs, xe);
        xs.WriteBoolean(xe, nameof(_invert), _invert);
        xs.WriteBoolean(xe, nameof(_transformPositionX), _transformPositionX);
        xs.WriteBoolean(xe, nameof(_transformPositionY), _transformPositionY);
        xs.WriteBoolean(xe, nameof(_transformPositionZ), _transformPositionZ);
        xs.WriteBoolean(xe, nameof(_transformRotation), _transformRotation);
        xs.WriteBoolean(xe, nameof(_transformScale), _transformScale);
        xs.WriteBoolean(xe, nameof(_transformSkew), _transformSkew);
        xs.WriteNumber(xe, nameof(_keyframe), _keyframe);
        xs.WriteString(xe, nameof(_nodeName), _nodeName);
    }
    public override bool Equals(object? obj)
    {
        return obj is hkAlignSceneToNodeOptions other && base.Equals(other) && _invert == other._invert && _transformPositionX == other._transformPositionX && _transformPositionY == other._transformPositionY && _transformPositionZ == other._transformPositionZ && _transformRotation == other._transformRotation && _transformScale == other._transformScale && _transformSkew == other._transformSkew && _keyframe == other._keyframe && _nodeName == other._nodeName && Signature == other.Signature;
    }
    public static bool operator ==(hkAlignSceneToNodeOptions? a, object? b) => a?.Equals(b) ?? b is null;
    public static bool operator !=(hkAlignSceneToNodeOptions? a, object? b) => !(a == b);
    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(_invert);
        code.Add(_transformPositionX);
        code.Add(_transformPositionY);
        code.Add(_transformPositionZ);
        code.Add(_transformRotation);
        code.Add(_transformScale);
        code.Add(_transformSkew);
        code.Add(_keyframe);
        code.Add(_nodeName);
        code.Add(Signature);
        return code.ToHashCode();
    }
}
