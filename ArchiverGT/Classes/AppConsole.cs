using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.IO.Compression;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Numerics;


namespace ArchiverGT.Classes
{
    class AppConsole
    {
        public static void Print_Console(long Lenght_Source, int status, Stopwatch sw)
        {
            string str = "";

            str += "Размер файла:" + "\r" + "\n" + (Lenght_Source / 1048576) + " MB" + "\r" + "\n";
            str += "Пройдено времени:" + "\r" + "\n" + (sw.ElapsedMilliseconds) / 1000 + " Секунд" + "\r" + "\n";
            str += "Объем занимаемой памяти:" + "\r" + "\n" + (Process.GetProcessesByName("ArchiverGT")[0].WorkingSet64 / 1024 / 1024).ToString() + " MB" + "\r" + "\n";
            //str += "Объем занимаемой памяти:" + "\r" + "\n" + (Process.GetProcessesByName("ArchiverGT")[0].PeakPagedMemorySize64 / 1024 / 1024).ToString() + " MB" + "\r" + "\n";
            str += "Статус:" + "\r" + "\n" + (string)(status == 0 ? "Indefinitely" : (status == 1 ? "Подготовка" : "Архивирование")) + "\r" + "\n";

            StartForm.Console.Text = str;
        }
    }
}
