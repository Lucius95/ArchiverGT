using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;
using System.Numerics;

namespace ArchiverGT.Classes
{
    class Log
    {
        private string _str;
        private object suncObj = new object();
        private ConcurrentDictionary<long, string> Dict_Gl = new ConcurrentDictionary<long, string>();
        private object sync_obj = new object();

        //Конструктор
        public Log()
        {
            //Создание нового лог файла
            using (FileStream fs = new FileStream("Log.txt", FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs)) { }
            }
        }

        //Запись  в лог файл
        public void WriteLog(string str)
        {
            try
            {
                lock (sync_obj)
                {
                    using (StreamWriter sw = new StreamWriter("Log.txt", true, System.Text.Encoding.Default))
                    {
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
    }
}
