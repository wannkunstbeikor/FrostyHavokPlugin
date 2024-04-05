using System.Collections.Generic;
using Frosty.Sdk.IO;
using FrostyHavokPlugin.Utils;

namespace FrostyHavokPlugin.CommonTypes;

public class HKXSection
{
    public readonly Dictionary<uint, GlobalFixup> _globalMap = new();

    public readonly Dictionary<uint, LocalFixup> _localMap = new();
    public readonly Dictionary<uint, VirtualFixup> _virtualMap = new();
    public List<GlobalFixup> GlobalFixups = new();

    public List<LocalFixup> LocalFixups = new();

    public DataStream SectionData;
    public int SectionID;

    public string SectionTag;
    public List<VirtualFixup> VirtualFixups = new();

    public string ContentsVersionString;

    internal HKXSection()
    {
    }

    internal HKXSection(DataStream br, string contentsVersionString, long startOffset, uint fixupTableOffset)
    {
        SectionTag = br.ReadFixedSizedString(19);
        br.AssertByte(0xFF);
        var absoluteDataStart = br.ReadUInt32();
        var fixupSize = br.ReadUInt32();
        var localFixupsSize = br.ReadUInt32();
        var virtualFixupsOffset = br.ReadUInt32();
        var exportsOffset = br.ReadUInt32();
        var importsOffset = br.ReadUInt32();
        var endOffset = br.ReadInt32();

        // Read Data
        SectionData = br.CreateSubStream(startOffset + absoluteDataStart, endOffset);

        // Local fixups
        LocalFixups = new List<LocalFixup>();
        if (localFixupsSize > 0)
        {
            br.StepIn(fixupTableOffset);
            while (br.Position < fixupTableOffset + localFixupsSize)
            {
                if (br.ReadUInt32() == 0xFFFFFFFF)
                {
                    break;
                }

                br.Position -= 4;
                var f = new LocalFixup(br);
                _localMap.Add(f.Src, f);
                LocalFixups.Add(f);
            }

            br.StepOut();
        }


        // Global fixups
        GlobalFixups = new List<GlobalFixup>();
        if (fixupSize - localFixupsSize > 0)
        {
            br.StepIn(fixupTableOffset + localFixupsSize);
            while (br.Position < fixupTableOffset + fixupSize)
            {
                if (br.ReadUInt32() == 0xFFFFFFFF)
                {
                    break;
                }

                br.Position -= 4;
                var f = new GlobalFixup(br);
                _globalMap.Add(f.Src, f);
                GlobalFixups.Add(f);
            }
            br.StepOut();
        }

        // Virtual fixups
        VirtualFixups = new List<VirtualFixup>();
        if (exportsOffset - virtualFixupsOffset > 0)
        {
            br.StepIn(startOffset + absoluteDataStart + virtualFixupsOffset);
            while (br.Position - startOffset < absoluteDataStart + exportsOffset)
            {
                if (br.ReadUInt32() == 0xFFFFFFFF)
                {
                    break;
                }

                br.Position -= 4;
                var f = new VirtualFixup(br);
                _virtualMap.Add(f.Src, f);
                VirtualFixups.Add(f);
            }

            br.StepOut();
        }

        br.AssertUInt32(0xFFFFFFFF);
        br.AssertUInt32(0xFFFFFFFF);
        br.AssertUInt32(0xFFFFFFFF);
        br.AssertUInt32(0xFFFFFFFF);
    }

    // public void WriteHeader(DataStream bw)
    // {
    //     bw.WriteFixStr(SectionTag, 19);
    //     bw.WriteByte(0xFF);
    //     bw.ReserveUInt32("absoffset" + SectionID);
    //     bw.ReserveUInt32("locoffset" + SectionID);
    //     bw.ReserveUInt32("globoffset" + SectionID);
    //     bw.ReserveUInt32("virtoffset" + SectionID);
    //     bw.ReserveUInt32("expoffset" + SectionID);
    //     bw.ReserveUInt32("impoffset" + SectionID);
    //     bw.ReserveUInt32("endoffset" + SectionID);
    //
    //     bw.WriteUInt32(0xFFFFFFFF);
    //     bw.WriteUInt32(0xFFFFFFFF);
    //     bw.WriteUInt32(0xFFFFFFFF);
    //     bw.WriteUInt32(0xFFFFFFFF);
    // }
    //
    // public void WriteData(DataStream bw)
    // {
    //     var absoluteOffset = (uint)bw.Position;
    //     bw.FillUInt32("absoffset" + SectionID, absoluteOffset);
    //     bw.WriteBytes(SectionData);
    //     while (bw.Position % 16 != 0) bw.WriteByte(0xFF); // 16 byte align
    //
    //     // Local fixups
    //     bw.FillUInt32("locoffset" + SectionID, (uint)bw.Position - absoluteOffset);
    //     foreach (var loc in LocalFixups) loc.Write(bw);
    //     while (bw.Position % 16 != 0) bw.WriteByte(0xFF); // 16 byte align
    //
    //     // Global fixups
    //     bw.FillUInt32("globoffset" + SectionID, (uint)bw.Position - absoluteOffset);
    //     foreach (var glob in GlobalFixups) glob.Write(bw);
    //     while (bw.Position % 16 != 0) bw.WriteByte(0xFF); // 16 byte align
    //
    //     // Virtual fixups
    //     bw.FillUInt32("virtoffset" + SectionID, (uint)bw.Position - absoluteOffset);
    //     foreach (var virt in VirtualFixups) virt.Write(bw);
    //     while (bw.Position % 16 != 0) bw.WriteByte(0xFF); // 16 byte align
    //
    //     bw.FillUInt32("expoffset" + SectionID, (uint)bw.Position - absoluteOffset);
    //     bw.FillUInt32("impoffset" + SectionID, (uint)bw.Position - absoluteOffset);
    //     bw.FillUInt32("endoffset" + SectionID, (uint)bw.Position - absoluteOffset);
    // }

    // Only use for a classnames structure after preliminary deserialization
    internal HKXClassNames ReadClassnames()
    {
        var classnames = new HKXClassNames();
        classnames.Read(SectionData);
        return classnames;
    }
}