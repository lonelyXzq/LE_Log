using System;
using System.Diagnostics;

namespace LE_Log
{
    public static class Log
    {

        /// <summary>
        /// 是否开启时间记录.default:false
        /// </summary>
        public static bool IsLogTime;

        /// <summary>
        /// StackTrace的标签
        /// </summary>
        public static LogType StackTraceMark;

        private static bool isLogSave;

        private static bool isLogConsole;


        /// <summary>
        /// 是否保存记录.default:false
        /// </summary>
        public static bool IsLogSave
        {
            get
            {
                return isLogSave;
            }
            set
            {
                if (value == isLogSave)
                {
                    return;
                }
                isLogSave = value;
                if (value==true)
                {
                    logSaver.Start();
                    AddLogHandler(logSaver.Add);
                }
                else
                {
                    logSaver.Stop();
                    DeleteLogHandler(logSaver.Add);
                }
            }
        }

        /// <summary>
        /// 是否开启控制台输出.default:true
        /// </summary>
        public static bool IsLogConsole
        {
            get
            {
                return isLogConsole;
            }
            set
            {
                if (value == isLogConsole)
                {
                    return;
                }
                isLogConsole = value;
                if (value == true)
                {
                    AddLogHandler(LogConsole);
                }
                else
                {
                    DeleteLogHandler(LogConsole);
                }
            }
        }



        public static string Prefix = ">>";

        public static string StackPrefix = "    >>";
        /// <summary>
        /// log保存路径
        /// </summary>
        public static string LogSavePath;

        private static LogHandler logHandler;

        private static readonly LogSaver logSaver;

        static Log()
        {
            logSaver = new LogSaver("/Log/");
            StackTraceMark = LogType.Error | LogType.Exception |LogType.FatalError;
            IsLogConsole = true;
            IsLogSave = false;
        }

        public static void AddLogHandler(LogHandler log)
        {
            if (logHandler == null)
            {
                logHandler = log;
            }
            else
            {
                logHandler += log;
            }
        }

        public static void DeleteLogHandler(LogHandler log)
        {
            if (logHandler != null)
            {
                logHandler -= log;
                return;
            }
            Log.Error("logHandler error", "logHandler does not exist!");
        }

        public static void ClearLogHandler()
        {
            logHandler = null;
        }

        private static void LogConsole(LogType logType, string message, string stack)
        {
            Console.WriteLine(string.Format("[{0}]:{1}\n{2}", logType, message, stack).TrimEnd());
        }

        private static string GetTime()
        {
            if (!IsLogTime)
            {
                return Prefix;
            }
            return string.Format("{0:HH:mm:ss.fff} {1}", DateTime.Now, Prefix);
        }

        private static string GetText(string tag, string message)
        {
            return string.Format("{0}{1} :: {2}", GetTime(), tag, message);
        }

        private static string FormatStack(StackTrace st)
        {
            string s = string.Empty;
            for (int i = 2; i < st.FrameCount; i++)
            {
                StackFrame sf = st.GetFrame(i);
                string _t = sf.GetFileName();
                if (_t != null)
                {
                    s += string.Format("{0}{1}:{2}(at{3}:{4})\n", StackPrefix, sf.GetMethod().ReflectedType.Name, sf.GetMethod(), _t, st.GetFrame(i).GetFileLineNumber());
                }
            }
            return s;
        }

        private static void Operator(LogType logType, string messsge)
        {
            logHandler?.Invoke(logType, messsge, (StackTraceMark & logType) == logType ? FormatStack(new StackTrace(true)) : string.Empty);
        }

        [Conditional("DEBUG")]
        public static void Debug(string message)
        {
            Debug(string.Empty, message);
        }

        [Conditional("DEBUG")]
        public static void Debug(string message, params object[] args)
        {
            Debug(string.Empty, message, args);
        }

        [Conditional("DEBUG")]
        public static void Debug(string tag, string message, params object[] args)
        {
            Debug(tag, string.Format(message, args));
        }

        [Conditional("DEBUG")]
        public static void Debug(string tag, string message)
        {
            Operator(LogType.Debug, GetText(tag, message));
        }


        public static void Info(string message)
        {
            Info(string.Empty, message);
        }

        public static void Info(string message, params object[] args)
        {
            Info(string.Empty, string.Format(message, args));
        }

        public static void Info(string tag, string message, params object[] args)
        {
            Info(tag, string.Format(message, args));
        }

        public static void Info(string tag, string message)
        {
            Operator(LogType.Info, GetText(tag, message));
        }

        public static void Warning(string message)
        {
            Warning(string.Empty, message);
        }

        public static void Warning(string message, params object[] args)
        {
            Warning(string.Empty, string.Format(message, args));
        }

        public static void Warning(string tag, string message, params object[] args)
        {
            Warning(tag, string.Format(message, args));
        }

        public static void Warning(string tag, string message)
        {
            Operator(LogType.Warning, GetText(tag, message));
        }

        public static void Error(string message)
        {
            Error(string.Empty, message);
        }

        public static void Error(string message, params object[] args)
        {
            Error(string.Empty, string.Format(message, args));
        }

        public static void Error(string tag, string message, params object[] args)
        {
            Error(tag, string.Format(message, args));
        }

        public static void Error(string tag, string message)
        {
            Operator(LogType.Error, GetText(tag, message));
        }

        public static void Exception(Exception exception, string message)
        {
            Exception(exception,string.Empty, message);
        }

        public static void Exception(Exception exception, string message, params object[] args)
        {
            Exception(exception, string.Empty, string.Format(message, args));
        }

        public static void Exception(Exception exception, string tag, string message, params object[] args)
        {
            Exception(exception, tag, string.Format(message, args));
        }

        public static void Exception(Exception exception, string tag, string message)
        {
            Operator(LogType.Exception, GetText(tag, message));
            throw exception;
        }

        public static void FatalError(string message)
        {
            FatalError(string.Empty, message);
        }

        public static void FatalError(string message, params object[] args)
        {
            FatalError(string.Empty, string.Format(message, args));
        }

        public static void FatalError(string tag, string message, params object[] args)
        {
            FatalError(tag, string.Format(message, args));
        }

        public static void FatalError(string tag, string message)
        {
            Operator(LogType.FatalError, GetText(tag, message));
        }
    }
}
