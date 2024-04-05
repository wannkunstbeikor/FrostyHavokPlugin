using System;
namespace hk;
public enum hkClassMember_FlagValues : uint
{
    FLAGS_NONE = 0,
    ALIGN_8 = 128,
    ALIGN_16 = 256,
    NOT_OWNED = 512,
    SERIALIZE_IGNORED = 1024,
    ALIGN_32 = 2048,
    ALIGN_REAL = 256,
}
