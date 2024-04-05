using System;
namespace hk;
public enum hkMeshSection_PrimitiveType : uint
{
    PRIMITIVE_TYPE_UNKNOWN = 0,
    PRIMITIVE_TYPE_POINT_LIST = 1,
    PRIMITIVE_TYPE_LINE_LIST = 2,
    PRIMITIVE_TYPE_TRIANGLE_LIST = 3,
    PRIMITIVE_TYPE_TRIANGLE_STRIP = 4,
}
