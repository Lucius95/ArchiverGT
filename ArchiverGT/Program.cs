using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Concurrent;
using System.Threading;
using System.IO;
using System.IO.Compression;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Numerics;
using ArchivingBoost;

namespace ArchiverGT
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new StartForm());
            //Archiving A = null;
            //Classes.Log objLog;
            //string[] str = new string[1];

            //str[0] = @"D:\C#\TestVid.avi";
            //A = new Archiving(str, "Finish_File");
            //objLog = new Classes.Log();
            //A.Event_LogMess += objLog.WriteLog;
        }
    }
}
