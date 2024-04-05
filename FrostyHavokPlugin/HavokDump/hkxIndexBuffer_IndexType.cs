using System;
namespace hk;
public enum hkxIndexBuffer_IndexType : uint
{
    INDEX_TYPE_INVALID = 0,
    INDEX_TYPE_TRI_LIST = 1,
    INDEX_TYPE_TRI_STRIP = 2,
    INDEX_TYPE_TRI_FAN = 3,
    INDEX_TYPE_MAX_ID = 4,
}
