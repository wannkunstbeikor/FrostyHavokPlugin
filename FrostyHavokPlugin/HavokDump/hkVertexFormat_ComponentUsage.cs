using System;
namespace hk;
public enum hkVertexFormat_ComponentUsage : uint
{
    USAGE_NONE = 0,
    USAGE_POSITION = 1,
    USAGE_NORMAL = 2,
    USAGE_COLOR = 3,
    USAGE_TANGENT = 4,
    USAGE_BINORMAL = 5,
    USAGE_BLEND_MATRIX_INDEX = 6,
    USAGE_BLEND_WEIGHTS = 7,
    USAGE_BLEND_WEIGHTS_LAST_IMPLIED = 8,
    USAGE_TEX_COORD = 9,
    USAGE_POINT_SIZE = 10,
    USAGE_USER = 11,
    USAGE_LAST = 12,
}
