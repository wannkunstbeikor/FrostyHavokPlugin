using System;
namespace hk;
public enum hkVertexFormat_ComponentType : uint
{
    TYPE_NONE = 0,
    TYPE_INT8 = 1,
    TYPE_UINT8 = 2,
    TYPE_INT16 = 3,
    TYPE_UINT16 = 4,
    TYPE_INT32 = 5,
    TYPE_UINT32 = 6,
    TYPE_UINT8_DWORD = 7,
    TYPE_ARGB32 = 8,
    TYPE_FLOAT16 = 9,
    TYPE_FLOAT32 = 10,
    TYPE_VECTOR4 = 11,
    TYPE_LAST = 12,
}
