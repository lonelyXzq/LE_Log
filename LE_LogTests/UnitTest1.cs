using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LE_Log;

namespace LE_LogTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Log.Error("error","2333");
        }
    }
}
