using System;
using System.Diagnostics;

namespace LE_Log
{
    public static class Log
    {
        [Conditional("DEBUG")]
        public static void Debug(string message)
        {

        }

        [Conditional("DEBUG")]
        public static void Debug(string tag, string message)
        {

        }

        [Conditional("DEBUG")]
        public static void Debug(string tag, string message, params object[] args)
        {

        }
    }
}
