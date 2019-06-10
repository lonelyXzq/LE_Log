using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace LE_Log
{
    internal class LogSaver
    {
        private readonly string filePath;

        private readonly string fileName;

        public static int WaitTime = 1000;

        public static int MaxLogSaveCache = 32;



        private readonly Queue<string> logQueue;

        private StreamWriter streamWriter;

        private Thread thread;

        private AutoResetEvent exitEvent;

        private bool stackTrace;

        private bool state;

        public string FullFilePath => Path.GetFullPath(filePath + fileName);

        public bool StackTrace { get => stackTrace; set => stackTrace = value; }

        public string FilePath => filePath;

        public string FileName => fileName;

        public LogSaver(string filePath)
        {
            logQueue = new Queue<string>();
            this.filePath = filePath;
            fileName = CreateFileName();
            state = false;
        }

        ~LogSaver()
        {
            Stop();
        }

        public void Start()
        {
            state = true;
            thread = new Thread(KeepSave);
            exitEvent = new AutoResetEvent(false);
            try
            {
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                streamWriter = new StreamWriter(filePath + fileName, true);
            }
            catch (Exception e)
            {
                Log.Error("file error", e.Message);
            }
            thread.Start();
            Console.WriteLine("logSaver start");
        }

        public void Stop()
        {
            Console.WriteLine("logSaver stop");
            Save();
            if (state)
            {
                exitEvent.Set();
                thread.Join();
                streamWriter.Close();
                state = false;
            }
        }

        public void KeepSave()
        {
            while (true)
            {
                if (exitEvent.WaitOne(WaitTime))
                {
                    break;
                }
                if (logQueue.Count > MaxLogSaveCache)
                {
                    Save();
                }
            }
        }

        public void Add(LogType logType, string message, string stack)
        {
            lock (logQueue)
            {
                logQueue.Enqueue(string.Format("[{0}]{1}:{2}\n{3}", logType, Thread.CurrentThread.Name, message, stack).TrimEnd());
            }
        }

        public void Save()
        {
            if (streamWriter != null)
            {
                try
                {
                    int n = logQueue.Count;
                    for (int i = 0; i < n; i++)
                    {
                        streamWriter.WriteLine(logQueue.Dequeue());
                    }
                    streamWriter.Flush();
                }
                catch (Exception e)
                {
                    Log.Error("file error", e.Message);
                }
            }
        }

        private string CreateFileName()
        {
            return string.Format("{0:yyyy_MM_dd_HH_mm_ss}.log", DateTime.Now);
        }
    }
}
