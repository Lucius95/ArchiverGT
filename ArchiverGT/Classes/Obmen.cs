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
    class Obmen
    {
        public static object Locker = new object();
        public static int Send_Flag = 0;
        public static int Flag_Log2 = 0;
        public static int Flag_Dec_Cus = 0;
        public static string Time1;
        public static string Source_FilePath = null;

        //Для вывода//
        public static long Lenght_Source = 0;
        public static long Position_Source = 0;
        public static int ProgressPercent = 0;
        public static Stopwatch Sw_Obmen = new Stopwatch();
    }
}
