using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;
using System.Numerics;

namespace ArchiverGT.Classes
{
    class TestClass
    {
        private ConcurrentQueue<byte[]> Dict_Que = new ConcurrentQueue<byte[]>();
        private byte[] byteArr = new byte[10000000];
        public Stopwatch SW_Gl = new Stopwatch();

        public TestClass()
        {
            for (int i = 0; i < 50; i++)
            {
                byteArr = new byte[10000000];
                Dict_Que.Enqueue(byteArr);
            }
        }
        ~TestClass()
        {
            while(true)
            {

            }
        }
    }
}
