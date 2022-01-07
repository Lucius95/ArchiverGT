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
    public partial class StartForm : Form
    {
        Archiving A = null;
        Classes.ZipArch ZipObj = null;
        Classes.LogQueue objLog;
        Classes.TestClass TT;
        Point LastPoint;

        public StartForm()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.Icon;
            Console = textBox2;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                this.Left += e.X - LastPoint.X;
                this.Top += e.Y - LastPoint.Y;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            LastPoint = new Point(e.X, e.Y);
        }

        private void OpenButton_MouseEnter(object sender, EventArgs e)
        {
            OpenButton.Image = Properties.Resources.path3;
        }

        private void OpenButton_MouseLeave(object sender, EventArgs e)
        {
            OpenButton.Image = Properties.Resources.path2;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            labelPercent.Text = "" + A.ProgressPercent + " %";
            //ProgressArchiving.Value = A.ProgressPercent;

            if (A.Flag_Stop == 1)
            {
                textBox1.Text = "";
                ProgressArchiving.Value = 0;
                A.ProgressPercent = 0; 
                ArchBotton.Enabled = true;
                UnzipBotton.Enabled = true;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Classes.AppConsole.Print_Console(A.Lenght_Source, A.status, A.SW_Gl);
            //Classes.AppConsole.Print_Console(1000, 0, ZipLibObj.SW_Gl);
            //Classes.AppConsole.Print_Console(1000, 0, ZipObj.SW_Gl);
            //Classes.AppConsole.Print_Console(1000, 0, TT.SW_Gl);

        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            string str = "";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            //textBox1.Text = openFileDialog1.FileNames;
            A = new Archiving(openFileDialog1.FileNames, "Finish_File");
            objLog = new Classes.LogQueue();
            A.Event_LogMess += objLog.WriteLog;           
        }

        private void ArchBotton_Click(object sender, EventArgs e)
        {
            if (A != null)
            {
                if (A.Flag_Start == 0)
                {
                    var VarThread = new Thread(A.Gain_Thread);
                    VarThread.IsBackground = true;
                    VarThread.Start();
                    timer1.Start();
                    timer2.Start();
                    ArchBotton.Enabled = false;
                }
            }
        }

        private void UnzipBotton_Click(object sender, EventArgs e)
        {
            if (A != null)
            {
                if (A.Flag_Start == 0)
                {
                    var VarThread = new Thread(A.ArcMethod_Decompress_GZip_Custom);
                    VarThread.IsBackground = true;
                    VarThread.Start();
                    timer1.Start();
                    timer2.Start();
                    UnzipBotton.Enabled = true;
                }
            }   
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            A.Dispose();
            timer2.Stop();
            timer1.Stop();
            A = null;
        }

        private void ZipButton_Click(object sender, EventArgs e)
        {
            ZipObj = new Classes.ZipArch(@"D:\фильмы\Документы\part");
            var VarThread = new Thread(ZipObj.ZipMet);
            VarThread.IsBackground = true;
            VarThread.Start();
            timer2.Start();
        }
    }
}
