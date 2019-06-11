using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LE_Log;
using System.Threading;

namespace LE_LogTests
{
    [TestClass]
    public class ThreadTest
    {
        private Thread[] threads;

        [TestInitialize]
        public void Init()
        {
            threads = new Thread[8];
            for (int i = 0; i < 8; i++)
            {
                threads[i] = new Thread(Out)
                {
                    Name = "thread" + i
                };
            }
        }

        [TestMethod]
        public void ThredOperatorTest()
        {
            for (int i = 0; i < 8; i++)
            {
                threads[i].Start();
            }
        }

        [TestMethod]
        public void SaveTest()
        {
            Log.LogSavePath = ".\\Log";
            Log.IsLogSave = true;
            for (int i = 0; i < 8; i++)
            {
                threads[i].Start();
            }
        }

        public void Out()
        {
            for (int i = 0; i < 100; i++)
            {
                Log.Info("2333");
            }
        }
    }


}
