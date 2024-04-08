using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Frosty.Sdk.IO;

namespace FrostyHavokPlugin;

public class HavokTypeDumper
{
    public const int POINTER_SIZE = 8;

    public enum VTYPE
    {
        TYPE_VOID = 0,
        TYPE_BOOL = 1,
        TYPE_CHAR = 2,
        TYPE_INT8 = 3,
        TYPE_UINT8 = 4,
        TYPE_INT16 = 5,
        TYPE_UINT16 = 6,
        TYPE_INT32 = 7,
        TYPE_UINT32 = 8,
        TYPE_INT64 = 9,
        TYPE_UINT64 = 10,
        TYPE_REAL = 11,
        TYPE_VECTOR4 = 12,
        TYPE_QUATERNION = 13,
        TYPE_MATRIX3 = 14,
        TYPE_ROTATION = 15,
        TYPE_QSTRANSFORM = 16,
        TYPE_MATRIX4 = 17,
        TYPE_TRANSFORM = 18,
        TYPE_ZERO = 19,
        TYPE_POINTER = 20,
        TYPE_FUNCTIONPOINTER = 21,
        TYPE_ARRAY = 22,
        TYPE_INPLACEARRAY = 23,
        TYPE_ENUM = 24,
        TYPE_STRUCT = 25,
        TYPE_SIMPLEARRAY = 26,
        TYPE_HOMOGENEOUSARRAY = 27,
        TYPE_VARIANT = 28,
        TYPE_CSTRING = 29,
        TYPE_ULONG = 30,
        TYPE_FLAGS = 31,
        TYPE_HALF = 32,
        TYPE_STRINGPTR = 33,
        TYPE_RELARRAY = 34,
        TYPE_MAX = 35
    }

    [Flags]
    public enum FlagValues
    {
        FLAGS_NONE = 0,
        ALIGN_8 = 128,
        ALIGN_16 = 256,
        NOT_OWNED = 512,
        SERIALIZE_IGNORED = 1024,
        ALIGN_32 = 2048,
        ALIGN_REAL = 256
    }

    public class hkClass
    {
        public string Name { get; set; }

        public string Parent { get; set; }

        public long pParent;

        public int ObjectSize { get; set; }
        public int NumImplementedInterfaces;
        public long DeclaredEnums;
        public int NumDeclaredEnums;
        public long DeclaredMembers;
        public int NumDeclaredMembers;
        public long Defaults;
        public long Attributes;
        public uint Signature; // not sure where they get the signature from

        public uint Flags { get; set; }

        public uint DescribedVersion { get; set; }

        public List<hkClassEnum> Enums { get; set; } = new();

        public List<hkClassMember> Members { get; set; } = new();

        public static Dictionary<long, hkClass> Classes = new();

        public static hkClass Serialize(MemoryReader inStream)
        {
            hkClass retVal;
            Classes.Add(inStream.Position, retVal = new hkClass
            {
                Name = inStream.ReadNullTerminatedString(),
                pParent = inStream.ReadLong(),
                ObjectSize = inStream.ReadInt(),
                NumImplementedInterfaces = inStream.ReadInt(),
                DeclaredEnums = inStream.ReadLong(),
                NumDeclaredEnums = inStream.ReadInt(),
                DeclaredMembers = inStream.ReadLong(),
                NumDeclaredMembers = inStream.ReadInt(),
                Defaults = inStream.ReadLong(),
                Attributes = inStream.ReadLong(),
                Flags = inStream.ReadUInt(),
                DescribedVersion = inStream.ReadUInt(),
            });


            inStream.Position = retVal.DeclaredEnums;
            retVal.Enums = new List<hkClassEnum>(retVal.NumDeclaredEnums);
            for (int i = 0; i < retVal.NumDeclaredEnums; i++)
            {
                retVal.Enums.Add(hkClassEnum.Serialize(inStream, retVal.Name));
            }

            return retVal;
        }

        public void Fixup(MemoryReader inReader)
        {
            if (pParent != 0)
            {
                Debug.Assert(Classes.TryGetValue(pParent, out hkClass? parent));
                Parent = parent.Name;
            }

            if (NumDeclaredMembers != 0)
            {
                inReader.Position = DeclaredMembers;
                Members = new(NumDeclaredMembers);
                for (int i = 0; i < NumDeclaredMembers; i++)
                {
                    Members.Add(hkClassMember.Serialize(inReader));
                }

                foreach (hkClassMember member in Members)
                {
                    member.Fixup(Name);
                }
            }
        }

        public void Create()
        {
            CurrentFile.Clear();
            WriteLine("using System;");
            WriteLine("using System.Collections.Generic;");
            WriteLine("using System.Xml.Linq;");
            WriteLine("using Frosty.Sdk.IO;");
            WriteLine("using FrostyHavokPlugin;");
            WriteLine("using FrostyHavokPlugin.Interfaces;");
            WriteLine("using OpenTK.Mathematics;");
            WriteLine("using Half = System.Half;");


            WriteLine("namespace hk;");

            bool isBase = string.IsNullOrEmpty(Parent);
            if (!isBase)
            {
                WriteLine($"public class {Name} : {Parent}, IEquatable<{Name}?>");
            }
            else
            {
                WriteLine($"public class {Name} : IHavokObject, IEquatable<{Name}?>");
            }

            WriteLine("{");

            PushIndent();

            WriteLine($"public {(isBase ? "virtual" : "override")} uint Signature => {Signature};");

            foreach (hkClassMember member in Members)
            {
                member.Create();
            }

            WriteLine(
                $"public {(isBase ? "virtual" : "override")} void Read(PackFileDeserializer des, DataStream br)");
            WriteLine("{");
            PushIndent();

            if (!isBase)
            {
                WriteLine("base.Read(des, br);");
            }

            int offset = pParent != 0 ? Classes[pParent].ObjectSize : 0;
            foreach (hkClassMember member in Members)
            {
                member.CreateRead(ref offset);
            }

            if (ObjectSize - offset > 0)
            {
                WriteLine($"br.Position += {ObjectSize - offset}; // padding");
            }

            PopIndent();
            WriteLine("}");

            WriteLine(
                $"public {(isBase ? "virtual" : "override")} void Write(PackFileSerializer s, DataStream bw)");
            WriteLine("{");
            PushIndent();

            if (!isBase)
            {
                WriteLine("base.Write(s, bw);");
            }

            offset = pParent != 0 ? Classes[pParent].ObjectSize : 0;
            foreach (hkClassMember member in Members)
            {
                member.CreateWrite(ref offset);
            }

            if (ObjectSize - offset > 0)
            {
                WriteLine($"for (int i = 0; i < {ObjectSize - offset}; i++) bw.WriteByte(0); // padding");
            }

            PopIndent();
            WriteLine("}");

            WriteLine(
                $"public {(isBase ? "virtual" : "override")} void WriteXml(XmlSerializer xs, XElement xe)");
            WriteLine("{");
            PushIndent();

            if (!isBase)
            {
                WriteLine("base.WriteXml(xs, xe);");
            }

            foreach (hkClassMember member in Members)
            {
                member.CreateXmlWrite();
            }

            PopIndent();
            WriteLine("}");

            WriteLine("public override bool Equals(object? obj)");
            WriteLine("{");
            PushIndent();
            WriteLine($"return Equals(obj as {Name});");
            PopIndent();
            WriteLine("}");

            WriteLine($"public bool Equals({Name}? other)");
            WriteLine("{");
            PushIndent();
            string ret = "return other is not null";
            foreach (hkClassMember member in Members)
            {
                if (((FlagValues)member.Flags).HasFlag(FlagValues.SERIALIZE_IGNORED))
                {
                    continue;
                }
                ret += $" && {member.Name}.Equals(other.{member.Name})";
            }
            ret += " && Signature == other.Signature;";
            WriteLine(ret);
            PopIndent();
            WriteLine("}");

            WriteLine($"public override int GetHashCode()");
            WriteLine("{");
            PushIndent();
            WriteLine("HashCode code = new();");
            foreach (hkClassMember member in Members)
            {
                if (((FlagValues)member.Flags).HasFlag(FlagValues.SERIALIZE_IGNORED))
                {
                    continue;
                }
                WriteLine($"code.Add({member.Name});");
            }
            WriteLine("code.Add(Signature);");
            WriteLine("return code.ToHashCode();");
            PopIndent();
            WriteLine("}");

            PopIndent();
            WriteLine("}");

            File.WriteAllText($"/home/jona/RiderProjects/FrostyToolsuite/FrostyCli/Havok/HavokDump/{Name}.cs", CurrentFile.ToString());
        }
    }

    public class hkClassMember
    {
        public string Name { get; set; }

        public string? Class { get; set; }

        public long pClass;

        public string? Enum { get; set; }
        public long pEnum;

        public byte Type { get; set; }

        public byte SubType { get; set; }

        public short ArraySize { get; set; }

        public ushort Flags { get; set; }

        public ushort Offset { get; set; }

        public long Attributes;

        public int Padding;

        public static hkClassMember Serialize(MemoryReader inReader)
        {
            return new hkClassMember()
            {
                Name = inReader.ReadNullTerminatedString(),
                pClass = inReader.ReadLong(),
                pEnum = inReader.ReadLong(),
                Type = inReader.ReadByte(),
                SubType = inReader.ReadByte(),
                ArraySize = inReader.ReadShort(),
                Flags = inReader.ReadUShort(),
                Offset = inReader.ReadUShort(),
                Attributes = inReader.ReadLong()
            };
        }

        public void Fixup(string className)
        {
            Name = "_" + Name;
            if (pClass != 0)
            {
                Debug.Assert(hkClass.Classes.TryGetValue(pClass, out hkClass? c));
                Class = c.Name;
            }

            if (pEnum != 0)
            {
                Debug.Assert(hkClassEnum.Enums.TryGetValue(pEnum, out hkClassEnum? e));
                Enum = e.Name;
            }

            if (((VTYPE)Type == VTYPE.TYPE_ENUM || (VTYPE)Type == VTYPE.TYPE_FLAGS) && pEnum == 0)
            {
                if (className == "hknpVehicleWheelCollide")
                {
                    Enum = "WheelCollideType";
                }
                else if (className == "hkcdShape")
                {
                    Enum = "ShapeTypeEnum";
                }
            }
        }

        public void Create()
        {
            if (((FlagValues)Flags).HasFlag(FlagValues.SERIALIZE_IGNORED))
            {
                WriteLine($"// {(VTYPE)Type} {(VTYPE)SubType} {Name}");
                return;
            }
            string type = ReduceType(this, (VTYPE)Type);
            if ((VTYPE)Type != VTYPE.TYPE_ARRAY && (VTYPE)Type != VTYPE.TYPE_RELARRAY && (VTYPE)Type != VTYPE.TYPE_SIMPLEARRAY && ArraySize > 0)
            {
                WriteLine($"public {type}[] {Name} = new {type}[{ArraySize}];");
            }
            else
            {
                WriteLine($"public {type} {Name};");
            }
        }

        public void CreateRead(ref int offset)
        {
            if (((FlagValues)Flags).HasFlag(FlagValues.SERIALIZE_IGNORED))
            {
                return;
            }

            if ((Padding = Offset - offset) > 0)
            {
                WriteLine($"br.Position += {Padding}; // padding");
                offset += Padding;
            }

            string? primitive;

            switch ((VTYPE)Type)
            {
                case VTYPE.TYPE_ARRAY:
                {
                    primitive = GetSimpleType((VTYPE)SubType);
                    if (primitive is null)
                    {
                        primitive = GetComplexType((VTYPE)SubType);
                    }
                    if (primitive is not null)
                    {
                        WriteLine($"{Name} = des.Read{primitive}Array(br);");
                        break;
                    }

                    if ((VTYPE)SubType == VTYPE.TYPE_STRUCT)
                    {
                        WriteLine($"{Name} = des.ReadClassArray<{Class}>(br);");
                        break;
                    }
                    if ((VTYPE)SubType == VTYPE.TYPE_POINTER)
                    {
                        WriteLine($"{Name} = des.ReadClassPointerArray<{Class}>(br);");
                        break;
                    }

                    WriteLine($"// Read {(VTYPE)SubType} array");
                    break;
                }
                case VTYPE.TYPE_RELARRAY:
                {
                    primitive = GetSimpleType((VTYPE)SubType) ?? GetComplexType((VTYPE)SubType);
                    if (primitive is not null)
                    {
                        WriteLine($"{Name} = des.Read{primitive}RelArray(br);");
                        break;
                    }

                    if ((VTYPE)SubType == VTYPE.TYPE_STRUCT)
                    {
                        WriteLine($"{Name} = des.ReadClassRelArray<{Class}>(br);");
                        break;
                    }
                    if ((VTYPE)SubType == VTYPE.TYPE_POINTER)
                    {
                        WriteLine($"{Name} = des.ReadClassPointerRelArray<{Class}>(br);");
                        break;
                    }

                    WriteLine($"// Read {(VTYPE)SubType} rel array");
                    break;
                }
                case VTYPE.TYPE_ENUM:
                case VTYPE.TYPE_FLAGS:
                {
                    if (string.IsNullOrEmpty(Enum))
                    {
                        throw new Exception();
                    }
                    primitive = GetEnumType((VTYPE)SubType, Enum);
                    WriteLine($"{Name} = ({Enum})br.Read{primitive}();");
                    break;
                }
                // Read inline class
                case VTYPE.TYPE_STRUCT when ArraySize > 0:
                {
                    WriteLine($"{Name} = des.ReadStructCStyleArray<{Class}>(br, {ArraySize});");
                    break;
                }
                case VTYPE.TYPE_STRUCT:
                    WriteLine($"{Name} = new {Class}();");
                    WriteLine($"{Name}.Read(des, br);");
                    break;
                // Read class pointer
                case VTYPE.TYPE_POINTER when (VTYPE)SubType != VTYPE.TYPE_STRUCT:
                    throw new Exception("bruh");
                case VTYPE.TYPE_POINTER when ArraySize > 0:
                {
                    WriteLine($"{Name} = des.ReadClassPointerCStyleArray<{Class}>(br, {ArraySize});");
                    break;
                }
                case VTYPE.TYPE_POINTER:
                    WriteLine($"{Name} = des.ReadClassPointer<{Class}>(br);");
                    break;
                default:
                {
                    primitive = GetSimpleType((VTYPE)Type);
                    if (primitive is not null)
                    {
                        if (ArraySize > 0)
                        {
                            WriteLine($"{Name} = des.Read{primitive}CStyleArray(br, {ArraySize});");
                        }
                        else
                        {
                            WriteLine($"{Name} = br.Read{primitive}();");
                        }

                        break;
                    }

                    primitive = GetComplexType((VTYPE)Type);
                    if (primitive is not null)
                    {
                        if (ArraySize > 0)
                        {
                            WriteLine($"{Name} = des.Read{primitive}CStyleArray(br, {ArraySize});");
                        }
                        else
                        {
                            WriteLine($"{Name} = des.Read{primitive}(br);");
                        }

                        break;
                    }

                    WriteLine($"// Read {(VTYPE)Type}");
                    break;
                }
            }


            offset += MemberSize(this, (VTYPE)Type);
        }

        public void CreateWrite(ref int offset)
        {
            if (((FlagValues)Flags).HasFlag(FlagValues.SERIALIZE_IGNORED))
            {
                return;
            }

            if ((Padding = Offset - offset) > 0)
            {
                WriteLine($"for (int i = 0; i < {Padding}; i++) bw.WriteByte(0); // padding");
                offset += Padding;
            }

            string? primitive;

            switch ((VTYPE)Type)
            {
                case VTYPE.TYPE_ARRAY:
                {
                    primitive = GetSimpleType((VTYPE)SubType);
                    if (primitive is null)
                    {
                        primitive = GetComplexType((VTYPE)SubType);
                    }
                    if (primitive is not null)
                    {
                        WriteLine($"s.Write{primitive}Array(bw, {Name});");
                        break;
                    }

                    if ((VTYPE)SubType == VTYPE.TYPE_STRUCT)
                    {
                        WriteLine($"s.WriteClassArray<{Class}>(bw, {Name});");
                        break;
                    }
                    if ((VTYPE)SubType == VTYPE.TYPE_POINTER)
                    {
                        WriteLine($"s.WriteClassPointerArray<{Class}>(bw, {Name});");
                        break;
                    }

                    WriteLine($"// Write {(VTYPE)SubType} array");
                    break;
                }
                case VTYPE.TYPE_RELARRAY:
                {
                    primitive = GetSimpleType((VTYPE)SubType) ?? GetComplexType((VTYPE)SubType);
                    if (primitive is not null)
                    {
                        WriteLine($"s.Write{primitive}RelArray(bw, {Name});");
                        break;
                    }

                    if ((VTYPE)SubType == VTYPE.TYPE_STRUCT)
                    {
                        WriteLine($"s.WriteClassRelArray<{Class}>(bw, {Name});");
                        break;
                    }
                    if ((VTYPE)SubType == VTYPE.TYPE_POINTER)
                    {
                        WriteLine($"s.WriteClassPointerRelArray<{Class}>(bw, {Name});");
                        break;
                    }

                    WriteLine($"// Write {(VTYPE)SubType} rel array");
                    break;
                }
                case VTYPE.TYPE_ENUM:
                case VTYPE.TYPE_FLAGS:
                {
                    if (string.IsNullOrEmpty(Enum))
                    {
                        throw new Exception();
                    }
                    primitive = GetEnumType((VTYPE)SubType, Enum);

                    WriteLine($"bw.Write{primitive}(({ReduceType(this, (VTYPE)SubType)}){Name});");
                    break;
                }
                // Read inline class
                case VTYPE.TYPE_STRUCT when ArraySize > 0:
                {
                    WriteLine($"s.WriteStructCStyleArray<{Class}>(bw, {Name});");
                    break;
                }
                case VTYPE.TYPE_STRUCT:
                    WriteLine($"{Name}.Write(s, bw);");
                    break;
                // Read class pointer
                case VTYPE.TYPE_POINTER when (VTYPE)SubType != VTYPE.TYPE_STRUCT:
                    throw new Exception("bruh");
                case VTYPE.TYPE_POINTER when ArraySize > 0:
                {
                    WriteLine($"s.WriteClassPointerCStyleArray<{Class}>(bw, {Name});");
                    break;
                }
                case VTYPE.TYPE_POINTER:
                    WriteLine($"s.WriteClassPointer<{Class}>(bw, {Name});");
                    break;
                default:
                {
                    primitive = GetSimpleType((VTYPE)Type);
                    if (primitive is not null)
                    {
                        if (ArraySize > 0)
                        {
                            WriteLine($"s.Write{primitive}CStyleArray(bw, {Name});");
                        }
                        else
                        {
                            WriteLine($"bw.Write{primitive}({Name});");
                        }

                        break;
                    }

                    primitive = GetComplexType((VTYPE)Type);
                    if (primitive is not null)
                    {
                        if (ArraySize > 0)
                        {
                            WriteLine($"s.Write{primitive}CStyleArray(bw, {Name});");
                        }
                        else
                        {
                            WriteLine($"s.Write{primitive}(bw, {Name});");
                        }

                        break;
                    }

                    WriteLine($"// Write {(VTYPE)Type}");
                    break;
                }
            }


            offset += MemberSize(this, (VTYPE)Type);
        }

        public void CreateXmlWrite()
        {
            if (((FlagValues)Flags).HasFlag(FlagValues.SERIALIZE_IGNORED))
            {
                return;
            }

            string? primitive;

            switch ((VTYPE)Type)
            {
                case VTYPE.TYPE_ARRAY:
                case VTYPE.TYPE_RELARRAY:
                {
                    primitive = GetSimpleTypeXml((VTYPE)SubType) ?? GetComplexType((VTYPE)SubType);
                    if (primitive is not null)
                    {
                        WriteLine($"xs.Write{primitive}Array(xe, nameof({Name}), {Name});");
                        break;
                    }

                    if ((VTYPE)SubType == VTYPE.TYPE_STRUCT)
                    {
                        WriteLine($"xs.WriteClassArray<{Class}>(xe, nameof({Name}), {Name});");
                        break;
                    }

                    if ((VTYPE)SubType == VTYPE.TYPE_POINTER)
                    {
                        WriteLine($"xs.WriteClassPointerArray<{Class}>(xe, nameof({Name}), {Name});");
                        break;
                    }

                    WriteLine($"// Write {(VTYPE)SubType} array");
                    break;
                }
                case VTYPE.TYPE_ENUM:
                {
                    if (string.IsNullOrEmpty(Enum))
                    {
                        throw new Exception();
                    }

                    primitive = ReduceType(this, (VTYPE)SubType);

                    WriteLine($"xs.WriteEnum<{Enum}, {primitive}>(xe, nameof({Name}), ({primitive}){Name});");
                    break;
                }
                case VTYPE.TYPE_FLAGS:
                {
                    if (string.IsNullOrEmpty(Enum))
                    {
                        throw new Exception();
                    }

                    primitive = ReduceType(this, (VTYPE)SubType);

                    WriteLine($"xs.WriteFlag<{Enum}, {primitive}>(xe, nameof({Name}), ({primitive}){Name});");
                    break;
                }
                // Read inline class
                case VTYPE.TYPE_STRUCT when ArraySize > 0:
                {
                    WriteLine($"xs.WriteClassArray(xe, nameof({Name}), {Name});");
                    break;
                }
                case VTYPE.TYPE_STRUCT:
                    WriteLine($"xs.WriteClass(xe, nameof({Name}), {Name});");
                    break;
                // Read class pointer
                case VTYPE.TYPE_POINTER when (VTYPE)SubType != VTYPE.TYPE_STRUCT:
                    throw new Exception("bruh");
                case VTYPE.TYPE_POINTER when ArraySize > 0:
                {
                    WriteLine($"xs.WriteClassPointerArray(xe, nameof({Name}), {Name});");

                    break;
                }
                case VTYPE.TYPE_POINTER:
                    WriteLine($"xs.WriteClassPointer(xe, nameof({Name}), {Name});");
                    break;
                default:
                {
                    primitive = GetSimpleTypeXml((VTYPE)Type) ?? GetComplexType((VTYPE)Type);

                    if (primitive is not null)
                    {
                        if (ArraySize > 0)
                        {
                            WriteLine($"xs.Write{primitive}Array(xe, nameof({Name}), {Name});");
                        }
                        else
                        {
                            WriteLine($"xs.Write{primitive}(xe, nameof({Name}), {Name});");
                        }

                        break;
                    }

                    WriteLine($"// Write {(VTYPE)Type}");
                    if ((VTYPE)Type == VTYPE.TYPE_RELARRAY)
                    {

                    }
                    break;
                }
            }
        }
    }

    public class hkClassEnum
    {
        public class Item
        {
            public uint Value { get; set; }

            public string Name { get; set; }

            public static Item Serialize(MemoryReader inReader)
            {
                return new Item()
                {
                    Value = inReader.ReadUInt(),
                    Name = inReader.ReadNullTerminatedString()
                };
            }

            public void Create()
            {
                WriteLine($"{Name} = {Value},");
            }
        }
        public string Name { get; set; }

        public long pItems;

        public List<Item> Items { get; set; } = new();
        public int NumItems;
        public long Attributes;
        public uint Flags { get; set; }

        public static Dictionary<long, hkClassEnum> Enums = new();

        public static hkClassEnum Serialize(MemoryReader inReader, string inClassName)
        {
            hkClassEnum retVal;
            Enums.Add(inReader.Position, retVal = new()
            {
                Name = inReader.ReadNullTerminatedString(),
                pItems = inReader.ReadLong(),
                NumItems = inReader.ReadInt(),
                Attributes = inReader.ReadLong(),
                Flags = (uint)inReader.ReadLong()// pad
            });

            retVal.Name = inClassName + "_" + retVal.Name;

            long curPos = inReader.Position;
            inReader.Position = retVal.pItems;
            retVal.Items = new List<Item>(retVal.NumItems);
            for (int i = 0; i < retVal.NumItems; i++)
            {
                retVal.Items.Add(Item.Serialize(inReader));
            }

            inReader.Position = curPos;

            return retVal;
        }

        public void Create()
        {
            CurrentFile.Clear();

            WriteLine("using System;");

            WriteLine("namespace hk;");

            if (Name.Contains("Flags", StringComparison.OrdinalIgnoreCase))
            {
                WriteLine("[Flags]");
            }

            WriteLine($"public enum {Name} : uint");
            WriteLine("{");
            PushIndent();

            foreach (Item item in Items)
            {
                item.Create();
            }

            PopIndent();
            WriteLine("}");

            File.WriteAllText($"/home/jona/RiderProjects/FrostyToolsuite/FrostyCli/Havok/HavokDump/{Name}.cs", CurrentFile.ToString());
        }
    }

    public static int MemberSize(hkClassMember m, VTYPE t, bool v = false)
    {
        short adjarrsize = m.ArraySize;
        if (v || adjarrsize == 0)
        {
            adjarrsize = 1;
        }
        switch (t)
        {
            case VTYPE.TYPE_ENUM:
            case VTYPE.TYPE_FLAGS:
                return MemberSize(m, (VTYPE)m.SubType, true);
            case VTYPE.TYPE_CSTRING:
            case VTYPE.TYPE_STRINGPTR:
                return POINTER_SIZE;
            case VTYPE.TYPE_UINT8:
                return 1 * adjarrsize;
            case VTYPE.TYPE_INT8:
                return 1 * adjarrsize;
            case VTYPE.TYPE_CHAR:
                return 1 * adjarrsize;
            case VTYPE.TYPE_UINT16:
                return 2 * adjarrsize;
            case VTYPE.TYPE_INT16:
                return 2 * adjarrsize;
            case VTYPE.TYPE_HALF:
                return 2 * adjarrsize;
            case VTYPE.TYPE_UINT32:
                return 4 * adjarrsize;
            case VTYPE.TYPE_INT32:
                return 4 * adjarrsize;
            case VTYPE.TYPE_ULONG:
                return 8 * adjarrsize;
            case VTYPE.TYPE_UINT64:
                return 8 * adjarrsize;
            case VTYPE.TYPE_INT64:
                return 8 * adjarrsize;
            case VTYPE.TYPE_BOOL:
                return 1 * adjarrsize;
            case VTYPE.TYPE_REAL:
                return 4 * adjarrsize;
            case VTYPE.TYPE_QUATERNION:
                return 16 * adjarrsize;
            case VTYPE.TYPE_ROTATION:
                return 48 * adjarrsize;
            case VTYPE.TYPE_VECTOR4:
                return 16 * adjarrsize;
            case VTYPE.TYPE_MATRIX4:
                return 64 * adjarrsize;
            case VTYPE.TYPE_MATRIX3:
                return 48 * adjarrsize;
            case VTYPE.TYPE_TRANSFORM:
                return 64 * adjarrsize;
            case VTYPE.TYPE_QSTRANSFORM:
                return 64 * adjarrsize;
            case VTYPE.TYPE_POINTER:
                return POINTER_SIZE * adjarrsize;
            case VTYPE.TYPE_ARRAY:
                return 16;
            case VTYPE.TYPE_RELARRAY:
                return 4;
            case VTYPE.TYPE_SIMPLEARRAY:
                return 16; // ?
            case VTYPE.TYPE_STRUCT:
                return hkClass.Classes[m.pClass].ObjectSize * adjarrsize;
            case VTYPE.TYPE_VARIANT:
                return 8; // Don't really care for this
            default:
                throw new Exception("Unknown type");
        }
    }

    public static string ReduceType(hkClassMember m, VTYPE t, bool v = false)
    {
        string r;
        switch (t)
        {
            case VTYPE.TYPE_ENUM:
            case VTYPE.TYPE_FLAGS:
                r = m.Enum!;
                break;
            case VTYPE.TYPE_CSTRING:
            case VTYPE.TYPE_STRINGPTR:
                r = "string";
                break;
            case VTYPE.TYPE_UINT8:
                r = "byte";
                break;
            case VTYPE.TYPE_INT8:
            case VTYPE.TYPE_CHAR:
                r = "sbyte";
                break;
            case VTYPE.TYPE_UINT16:
                r = "ushort";
                break;
            case VTYPE.TYPE_INT16:
                r = "short";
                break;
            case VTYPE.TYPE_HALF:
                r = "Half";
                break;
            case VTYPE.TYPE_UINT32:
                r = "uint";
                break;
            case VTYPE.TYPE_INT32:
                r = "int";
                break;
            case VTYPE.TYPE_ULONG:
            case VTYPE.TYPE_UINT64:
                r = "ulong";
                break;
            case VTYPE.TYPE_INT64:
                r = "long";
                break;
            case VTYPE.TYPE_BOOL:
                r = "bool";
                break;
            case VTYPE.TYPE_REAL:
                r = "float";
                break;
            case VTYPE.TYPE_QUATERNION:
                r = "Quaternion";
                break;
            case VTYPE.TYPE_ROTATION:
                r = "Matrix4";
                break;
            case VTYPE.TYPE_VECTOR4:
                r = "Vector4";
                break;
            case VTYPE.TYPE_MATRIX3:
            case VTYPE.TYPE_QSTRANSFORM:
                r = "Matrix3x4";
                break;
            case VTYPE.TYPE_MATRIX4:
            case VTYPE.TYPE_TRANSFORM:
                r = "Matrix4";
                break;
            case VTYPE.TYPE_POINTER when v:
            {
                if (!string.IsNullOrEmpty(m.Class))
                {
                    r = m.Class;
                }
                else
                {
                    r = "void*";
                }
                return r;
            }
            case VTYPE.TYPE_POINTER:
                r = ReduceType(m, (VTYPE)m.SubType, true);
                break;
            case VTYPE.TYPE_ARRAY:
            case VTYPE.TYPE_RELARRAY:
            case VTYPE.TYPE_SIMPLEARRAY:
                r = "List<" + ReduceType(m, (VTYPE)m.SubType, true) + ">";
                break;
            case VTYPE.TYPE_STRUCT:
                r = m.Class;
                break;
            case VTYPE.TYPE_VARIANT:
                r = "ulong";
                break;
            default:
                throw new Exception("Unknown type");
        }
        return r;
    }

    public static string? GetSimpleType(VTYPE t)
    {
        switch (t)
        {
            case VTYPE.TYPE_BOOL:
                return "Boolean";
            case VTYPE.TYPE_CHAR:
            case VTYPE.TYPE_INT8:
                return "SByte";
            case VTYPE.TYPE_UINT8:
                return "Byte";
            case VTYPE.TYPE_INT16:
                return "Int16";
            case VTYPE.TYPE_UINT16:
                return "UInt16";
            case VTYPE.TYPE_INT32:
                return "Int32";
            case VTYPE.TYPE_UINT32:
                return "UInt32";
            case VTYPE.TYPE_INT64:
                return "Int64";
            case VTYPE.TYPE_UINT64:
            case VTYPE.TYPE_ULONG:
                return "UInt64";
            case VTYPE.TYPE_REAL:
                return "Single";
            default:
                return null;
        }
    }

    public static string? GetSimpleTypeXml(VTYPE t)
    {
        switch (t)
        {
            case VTYPE.TYPE_BOOL:
                return "Boolean";
            case VTYPE.TYPE_CHAR:
            case VTYPE.TYPE_INT8:
            case VTYPE.TYPE_UINT8:
            case VTYPE.TYPE_INT16:
            case VTYPE.TYPE_UINT16:
            case VTYPE.TYPE_INT32:
            case VTYPE.TYPE_UINT32:
            case VTYPE.TYPE_INT64:
            case VTYPE.TYPE_UINT64:
            case VTYPE.TYPE_ULONG:
                return "Number";
            case VTYPE.TYPE_HALF:
            case VTYPE.TYPE_REAL:
                return "Float";
            case VTYPE.TYPE_CSTRING:
            case VTYPE.TYPE_STRINGPTR:
                return "String";
            default:
                return null;
        }
    }

    public static string? GetComplexType(VTYPE t)
    {
        switch (t)
        {
            case VTYPE.TYPE_HALF:
                return "Half";
            case VTYPE.TYPE_QUATERNION:
                return "Quaternion";
            case VTYPE.TYPE_ROTATION:
                return "Matrix3";
            case VTYPE.TYPE_VECTOR4:
                return "Vector4";
            case VTYPE.TYPE_MATRIX4:
                return "Matrix4";
            case VTYPE.TYPE_MATRIX3:
                return "Matrix3";
            case VTYPE.TYPE_TRANSFORM:
                return "Transform";
            case VTYPE.TYPE_QSTRANSFORM:
                return "QSTransform";
            case VTYPE.TYPE_CSTRING:
            case VTYPE.TYPE_STRINGPTR:
                return "StringPointer";
            default:
                return null;
        }
    }

    public static string GetEnumType(VTYPE t, string ename)
    {
        switch (t)
        {
            case VTYPE.TYPE_INT8:
                return "SByte";
            case VTYPE.TYPE_UINT8:
                return "Byte";
            case VTYPE.TYPE_INT16:
                return "Int16";
            case VTYPE.TYPE_UINT16:
                return "UInt16";
            case VTYPE.TYPE_INT32:
                return "Int32";
            case VTYPE.TYPE_UINT32:
                return "UInt32";
            default:
                throw new Exception("Unimplemented type");
        }
    }

    public static int CurrentIndent = 0;
    public static StringBuilder CurrentFile = new();

    public static void PushIndent()
    {
        CurrentIndent++;
    }

    public static void PopIndent()
    {
        CurrentIndent--;
    }

    public static void WriteLine(string s)
    {
        string indent = "";
        for (int i = 0; i < CurrentIndent; i++)
        {
            indent += "    ";
        }
        CurrentFile.AppendLine(indent + s);
    }

    public static void Dump()
    {
        MemoryReader reader = new(Process.GetProcessById(23628)) { Position = 0x1430dec60 };

        long offset = reader.ReadLong();
        do
        {
            long curPos = reader.Position;
            reader.Position = offset;
            hkClass.Serialize(reader);
            reader.Position = curPos;
            offset = reader.ReadLong();
        } while (offset != 0);

        foreach (hkClass value in hkClass.Classes.Values)
        {
            value.Fixup(reader);
            value.Create();
        }

        foreach (hkClassEnum value in hkClassEnum.Enums.Values)
        {
            value.Create();
        }
    }
}