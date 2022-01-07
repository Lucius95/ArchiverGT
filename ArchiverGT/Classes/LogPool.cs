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
    class LogPool
    { 
        private MyThreadPool pool = new Classes.MyThreadPool();
        private string _str;
        private object suncObj = new object();

        //Конструктор
        public LogPool()
        {
            //Создание нового лог файла
            using (FileStream fs = new FileStream("Log.txt", FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs)) { }
            }

            //Создание пула потоков для операций логирования
            pool = new Classes.MyThreadPool();
        }

        //Запись  в лог файл
        public void WriteLog(string str)
        {
            _str = str;
            this.pool.Queue(this.LogOperation);
        }

        //Постановка в очередь
        private void LogOperation()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("Log.txt", true, System.Text.Encoding.Default))
                {
                    sw.WriteLine(_str);
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
