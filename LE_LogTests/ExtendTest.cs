using LE_Log;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LE_LogTests
{
    [TestClass]
    public class ExtendTest
    {

        [TestMethod]
        public void ExtendOut()
        {
            Log.AddAction(LogType.Debug, DebugOut);
            Log.Debug("2333");
            Log.Debug("Debug", "2333");
            Log.Debug("2333:{0}", 233);
            Log.Debug("Debug", "2333:{0}", 233);
        }

        public void DebugOut(string message, string stack)
        {
            if (message.Equals("2333"))
            {
                Console.WriteLine("Debug catch");
            }
        }

        [TestMethod]
        public void StackTest()
        {
            Log.Info("2333");
            Log.Info("Info", "2333");
            Log.Info("2333:{0}", 233);
            Log.Info("Info", "2333:{0}", 233);
            Log.StackTraceMark |= LogType.Info;
            Log.Info("2333");
            Log.Info("Info", "2333");
            Log.Info("2333:{0}", 233);
            Log.Info("Info", "2333:{0}", 233);
        }
    }
}
