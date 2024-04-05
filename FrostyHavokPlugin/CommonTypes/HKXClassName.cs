using Frosty.Sdk.IO;
using FrostyHavokPlugin.Utils;

namespace FrostyHavokPlugin.CommonTypes;

public class HKXClassName
{
    public string ClassName;
    public uint Signature;

    internal HKXClassName()
    {
    }

    internal HKXClassName(DataStream br)
    {
        Signature = br.ReadUInt32();
        br.AssertByte(0x09); // Seems random but ok
        ClassName = br.ReadNullTerminatedString();
    }

    public void Write(DataStream bw)
    {
        bw.WriteUInt32(Signature);
        bw.WriteByte(0x09);
        bw.WriteNullTerminatedString(ClassName);
    }
}