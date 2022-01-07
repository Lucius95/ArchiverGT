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
    //class StartClass
    //{
    //    public delegate void HandlerMess(string message);
    //    public event HandlerMess Event_LogMess;

    //    private Archiving A = null;

    //    public void Starter()
    //    {
    //        int flag_finish = 0;
    //        Flag_Start = 1;

    //        CompressedFile2 = SourceFile.Split(new string[] { Path.GetExtension(Obmen.Source_FilePath) }, StringSplitOptions.RemoveEmptyEntries);
    //        CompressedFile = CompressedFile2[0] + ".gz";

    //        try
    //        {
    //            source_stream = new FileStream(SourceFile, FileMode.Open, FileAccess.Read);
    //        }
    //        catch (Exception ex)
    //        {
    //            Event_LogMess?.Invoke(this.GetType() + " : " + new StackTrace(false).GetFrame(0).GetMethod().Name + " : " + ex.Message);
    //        }
    //        Obmen.Lenght_Source = source_stream.Length;
    //        SumPackages = (int)(Obmen.Lenght_Source / 1000000);
    //        SumPackages = ((int)(Obmen.Lenght_Source % 1000000)) > 0 ? (SumPackages + 1) : SumPackages;

    //        target_stream = new FileStream(CompressedFile, FileMode.Create, FileAccess.Write);
    //        target_stream.Position = 4 * 1000000 + 4 + 10;

    //        var sw = new Stopwatch();
    //        Obmen.Sw_Obmen = sw;
    //        sw.Start();

    //        var VarThread = new Thread(this.LockField_FileStream_Compressed);
    //        VarThread.IsBackground = true;
    //        VarThread.Start();

    //        Event_LogMess?.Invoke("Архивирование началось");

    //        VarThread = new Thread(this.LockField_Compressed);
    //        VarThread.IsBackground = true;
    //        VarThread.Start();

    //        VarThread = new Thread(this.LockField_Compressed);
    //        VarThread.IsBackground = true;
    //        VarThread.Start();

    //        VarThread = new Thread(this.LockField_Compressed);
    //        VarThread.IsBackground = true;
    //        VarThread.Start();

    //        VarThread = new Thread(this.LockField_Compressed);
    //        VarThread.IsBackground = true;
    //        VarThread.Start();

    //        VarThread = new Thread(this.LockField_Compressed);
    //        VarThread.IsBackground = true;
    //        VarThread.Start();

    //        while (flag_finish == 0)
    //        {
    //            if (SumPackages + 1 == Byte_Priority_Compression)
    //            {
    //                target_stream.Position = 0;

    //                byte[] Format_Bytes = Encoding.UTF8.GetBytes(Path.GetExtension(Obmen.Source_FilePath));
    //                byte[] Format_Bytes_Size = new byte[1];
    //                Format_Bytes_Size[0] = (byte)Format_Bytes.Length;

    //                target_stream.Write(Format_Bytes_Size, 0, 1);
    //                target_stream.Write(Format_Bytes, 0, Format_Bytes.Length);

    //                byte[] CountFile_Bytes = BitConverter.GetBytes(CountFile);
    //                target_stream.Write(CountFile_Bytes, 0, CountFile_Bytes.Length);

    //                byte[][] SizeFile_Bytes = new byte[1000000][];
    //                for (int i = 0; i < CountFile; i++)
    //                {
    //                    SizeFile_Bytes[i] = BitConverter.GetBytes(SizeFile[i]);
    //                }
    //                for (int i = 0; i < CountFile; i++)
    //                {
    //                    target_stream.Write(SizeFile_Bytes[i], 0, SizeFile_Bytes[i].Length);
    //                }

    //                sw.Stop();
    //                Obmen.Time1 = Convert.ToString(sw.ElapsedMilliseconds);
    //                Event_LogMess?.Invoke("Архивирование закончено");
    //                Event_LogMess?.Invoke("Время " + Obmen.Time1);
    //                FlagShowTime = 0;
    //                flag_finish = 1;
    //                Flag_Stop = 1;

    //                source_stream.Close();
    //                target_stream.Close();
    //            }
    //        }
    //    }
    //}
}
