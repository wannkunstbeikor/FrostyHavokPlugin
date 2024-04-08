using System.Xml.Linq;
using Frosty.Sdk.IO;

namespace FrostyHavokPlugin.Interfaces;

public interface IHavokObject
{
    public uint Signature { get; }

    public void Read(PackFileDeserializer des, DataStream br);

    public void Write(PackFileSerializer s, DataStream bw);
    public void WriteXml(XmlSerializer xs, XElement xe);
    //public void ReadXml(XmlDeserializer xd, XElement xe);
}