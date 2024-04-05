using System;
namespace hk;
public enum hknpShapeType_Enum : uint
{
    CONVEX = 0,
    CONVEX_POLYTOPE = 1,
    SPHERE = 2,
    CAPSULE = 3,
    TRIANGLE = 4,
    COMPRESSED_MESH = 5,
    EXTERN_MESH = 6,
    STATIC_COMPOUND = 7,
    DYNAMIC_COMPOUND = 8,
    HEIGHT_FIELD = 9,
    COMPRESSED_HEIGHT_FIELD = 10,
    SCALED_CONVEX = 11,
    MASKED_COMPOSITE = 12,
    DUMMY = 13,
    USER_0 = 14,
    USER_1 = 15,
    USER_2 = 16,
    USER_3 = 17,
    NUM_SHAPE_TYPES = 18,
    INVALID = 19,
}
