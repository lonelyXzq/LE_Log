using System;
using System.Collections.Generic;
using System.Text;

namespace LE_Log
{
    /// <summary>
    /// Log输出委托
    /// </summary>
    /// <param name="logType"></param>
    /// <param name="message"></param>
    /// <param name="stack"></param>
    public delegate void LogHandler(LogType logType, string message, string stack);


    /// <summary>
    /// 
    /// </summary>
    /// <param name="messsage"></param>
    /// <param name="stack"></param>
    public delegate void LogAction(string messsage,string stack);
}
