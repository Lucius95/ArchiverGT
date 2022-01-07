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
    class LogQueue
    {
        private string _str;
        private int result = 0;
        private object obj_sync = new object();
        private ConcurrentQueue<string> Queue_Str = new ConcurrentQueue<string>();
        private Task<int> tt;

        public LogQueue()
        {
            //Создание нового лог файла
            using (FileStream fs = new FileStream("Log.txt", FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs)) { }
            }

            //Пуск потока записи логов в файл
            var VarThread = new Thread(this.Task_Method)
            {
                IsBackground = true,
                Name = $"Log Thread"
            };
            VarThread.Start();
        }

        //Запись  в лог файл
        public void WriteLog(string str)
        {            
            Queue_Str.Enqueue(str);
            //lock (obj_sync)
            //{
            //    Monitor.Pulse(obj_sync);
            //}
            //tt = new Task<int>(() => this.Task_Method(5));
        }
            
        public void Task_Method()
        {
            string str;
            int result = 0;

            while (true)
            {
                try
                {
                    if (Queue_Str.Count != 0)
                    {
                        using (StreamWriter sw = new StreamWriter("Log.txt", true, System.Text.Encoding.Default))
                        {
                            Queue_Str.TryDequeue(out str);
                            sw.WriteLine(str);
                        }
                    }
                }
                catch (Exception ex)
                {
                    using (StreamWriter sw = new StreamWriter("Log.txt", true, System.Text.Encoding.Default))
                    {
                        sw.WriteLine(ex.Message);
                    }
                }
            }

            //return result;
        }
    }
}
