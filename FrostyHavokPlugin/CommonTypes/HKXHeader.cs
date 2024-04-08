using Frosty.Sdk.IO;
using FrostyHavokPlugin.Utils;

namespace FrostyHavokPlugin.CommonTypes;

public class HKXHeader
{
    public byte BaseClass;
    public int ContentsClassNameSectionIndex;
    public int ContentsClassNameSectionOffset;
    public int ContentsSectionIndex;
    public int ContentsSectionOffset;
    public string ContentsVersionString;
    public byte Endian;
    public int FileVersion;
    public int Flags;
    public uint Magic0;
    public uint Magic1;
    public short MaxPredicate;
    public byte PaddingOption;
    public byte PointerSize;
    public int SectionCount;
    public short SectionOffset;
    public short Unk40;
    public short Unk42;
    public uint Unk44;
    public uint Unk48;
    public uint Unk4C;
    public int UserTag;

    private HKXHeader()
    {
    }

    internal HKXHeader(DataStream br)
    {
        Magic0 = br.AssertUInt32(0x57E0E057);
        Magic1 = br.AssertUInt32(0x10C0C010);
        UserTag = br.ReadInt32();
        FileVersion = br.AssertInt32(0x0B);
        PointerSize = br.AssertByte(4, 8);
        Endian = br.AssertByte(0, 1);
        PaddingOption = br.AssertByte(0, 1);
        BaseClass = br.AssertByte(1);
        SectionCount = br.AssertInt32(3);
        ContentsSectionIndex = br.ReadInt32();
        ContentsSectionOffset = br.ReadInt32();
        ContentsClassNameSectionIndex = br.ReadInt32();
        ContentsClassNameSectionOffset = br.ReadInt32();
        ContentsVersionString = br.ReadFixedSizedString(15);
        br.AssertByte(0xFF);
        Flags = br.ReadInt32();
        MaxPredicate = br.ReadInt16();
        SectionOffset = br.ReadInt16();
        if (SectionOffset != 16)
        {
            return;
        }

        Unk40 = br.ReadInt16();
        Unk42 = br.ReadInt16();
        Unk44 = br.ReadUInt32();
        Unk48 = br.ReadUInt32();
        Unk4C = br.ReadUInt32();
    }

    internal void Write(DataStream bw)
    {
        bw.WriteUInt32(Magic0);
        bw.WriteUInt32(Magic1);
        bw.WriteInt32(UserTag);
        bw.WriteInt32(FileVersion);
        bw.WriteByte(PointerSize);
        bw.WriteByte(Endian);
        bw.WriteByte(PaddingOption);
        bw.WriteByte(BaseClass);
        bw.WriteInt32(SectionCount);

        bw.WriteInt32(ContentsSectionIndex);
        bw.WriteInt32(ContentsSectionOffset);
        bw.WriteInt32(ContentsClassNameSectionIndex);
        bw.WriteInt32(ContentsClassNameSectionOffset);
        bw.WriteFixedSizedString(ContentsVersionString, 15);
        bw.WriteByte(0xFF);
        bw.WriteInt32(Flags);
        bw.WriteInt16(MaxPredicate);
        bw.WriteInt16(SectionOffset);

        if (SectionOffset != 16)
        {
            return;
        }

        bw.WriteInt16(Unk40);
        bw.WriteInt16(Unk42);
        bw.WriteUInt32(Unk44);
        bw.WriteUInt32(Unk48);
        bw.WriteUInt32(Unk4C);
    }
}