using System.Collections.Generic;
using Frosty.Sdk.IO;

namespace FrostyHavokPlugin.CommonTypes;

internal class HKXClassNames
    {
        private List<HKXClassName> ClassNames;
        public Dictionary<uint, HKXClassName> OffsetClassNamesMap;

        public void Read(DataStream br)
        {
            ClassNames = new List<HKXClassName>();
            OffsetClassNamesMap = new Dictionary<uint, HKXClassName>();
            while (true)
            {
                if (br.Position >= br.Length || br.Position + 5 >= br.Length)
                {
                    break;
                }

                br.ReadUInt32(); // signature
                var separator = br.ReadByte();
                if (separator != 0x09)
                {
                    break;
                }
                br.Position -= 5;

                var stringStart = (uint)br.Position + 5;
                var className = new HKXClassName(br);
                ClassNames.Add(className);
                OffsetClassNamesMap.Add(stringStart, className);
                if (br.Position == br.Length) break;
            }
        }
    }