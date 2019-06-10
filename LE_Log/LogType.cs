using System;
using System.Collections.Generic;
using System.Text;

namespace LE_Log
{
    [Flags]
    public enum LogType
    {
        /// <summary>
        /// 调试
        /// </summary>
        Debug = 1,
        /// <summary>
        /// 信息
        /// </summary>
        Info = 2,
        /// <summary>
        /// 警告
        /// </summary>
        Warning = 4,
        /// <summary>
        /// 错误
        /// </summary>
        Error = 8,
        /// <summary>
        /// 异常
        /// </summary>
        Exception = 16,
        /// <summary>
        /// 严重错误
        /// </summary>
        FatalError=32
    }
}
