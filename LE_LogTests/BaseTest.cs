using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LE_Log;

namespace LE_LogTests
{
    [TestClass]
    public class BaseTest
    {
        [TestMethod]
        public void TimeSet()
        {
            Log.IsLogTime = true;
            Log.Debug("2333");
            Log.Debug("FatalDebug", "2333");
            Log.Debug("2333:{0}", 233);
            Log.Debug("FatalDebug", "2333:{0}", 233);
            Log.IsLogTime = false;
        }

        [TestMethod]
        public void TestDebug()
        {
            Log.Debug("2333");
            Log.Debug("FatalDebug", "2333");
            Log.Debug("2333:{0}", 233);
            Log.Debug("FatalDebug", "2333:{0}", 233);
        }

        [TestMethod]
        public void TestInfo()
        {
            Log.Info("2333");
            Log.Info("FatalInfo", "2333");
            Log.Info("2333:{0}", 233);
            Log.Info("FatalInfo", "2333:{0}", 233);
        }

        [TestMethod]
        public void TestWarning()
        {
            Log.Warning("2333");
            Log.Warning("FatalWarning", "2333");
            Log.Warning("2333:{0}", 233);
            Log.Warning("FatalWarning", "2333:{0}", 233);
        }

        [TestMethod]
        public void TestFatalError()
        {
            Log.FatalError("2333");
            Log.FatalError("FatalError", "2333");
            Log.FatalError("2333:{0}", 233);
            Log.FatalError("FatalError", "2333:{0}", 233);
        }

        [TestMethod]
        public void TestError()
        {
            Log.Error("2333");
            Log.Error("error","2333");
            Log.Error("2333:{0}",233);
            Log.Error("error", "2333:{0}", 233);
        }

        [TestMethod]
        public void TestException()
        {
            try
            {
                Log.Exception(new Exception(), "2333");
            }
            catch (Exception)
            {
                Console.WriteLine("exception test");
            }
            try
            {
                Log.Exception(new Exception(), "Exception", "2333");
            }
            catch (Exception)
            {
                Console.WriteLine("exception test");
            }
            try
            {
                Log.Exception(new Exception(), "2333:{0}", 233);
            }
            catch (Exception)
            {
                Console.WriteLine("exception test");
            }
            try
            {
                Log.Exception(new Exception(), "Exception", "2333:{0}", 233);
            }
            catch (Exception)
            {
                Console.WriteLine("exception test");
            }
        }
    }
}
