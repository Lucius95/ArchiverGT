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

namespace ArchivingBoost
{
    class MyStream
    {
        private FileStream[] source_streamArr;
        private long[] range_stream;
        public long Length = 0;
        public long[] LengthStreams;
        public long Position = 0;
        public int NumberStream = 0;

        public MyStream(string[] path)
        {
            try
            {
                source_streamArr = new FileStream[path.Length];
                range_stream = new long[path.Length];
                LengthStreams = new long[path.Length];
                NumberStream = path.Length;
                for (int i = 0; i < path.Length; i++)
                {
                    source_streamArr[i] = new FileStream(path[i], FileMode.Open, FileAccess.Read);
                    LengthStreams[i] = source_streamArr[i].Length;
                    Length += source_streamArr[i].Length;
                    range_stream[i] = Length;
                }
            }
            catch (Exception ex)
            {

            }
        }

        public int Read(byte[] arr, int offset, int count)
        {
            int ReadCount = 0;
            int i = 0;

            i = Numberstream(Position);
            ReadCount = source_streamArr[i].Read(arr, offset, count);
            Position += ReadCount;

            return ReadCount;
        }

        public int Numberstream(long Position)
        {
            for (int i = 0; i < source_streamArr.Length; i++)
            {
                if (Position < range_stream[i])
                {
                    return i;
                }
            }

            return 0;
        }

        public void Close()
        {
            for (int i = 0; i < source_streamArr.Length; i++)
            {
                source_streamArr[i].Close();
            }
        }
    }
    public class Archiving : IDisposable
    {
        public delegate void HandlerMess(string message);
        public event HandlerMess Event_LogMess;

        public string SourceFile = "";
        public string FileName_Finish = "";
        public string[] SourceFileArr;
        private string CompressedFile = "";
        private string TargetFile = "";
        private object Locker_Compressed = new object();
        private object Locker_Withdrawal = new object();
        private FileStream target_stream;
        private MyStream my_stream;
        private int Byte_Priority_Withdrawal = 0;
        private int Byte_Priority_Compression = 1;
        private int CountFile = 0;
        private int[] SizeFile = new int[1000000];
        private int[] Number_Packages = new int[10000];
        private int NumberFile = 0;
        private ConcurrentDictionary<long, byte[]> Dict_Gl = new ConcurrentDictionary<long, byte[]>();
        private ConcurrentQueue<byte[]> Dict_Que = new ConcurrentQueue<byte[]>(); 
        private int SumPackages = 0;
        private int PartFile_Lenght = 1000000;

        /////////////////////////
        public int status = 0;
        public int Flag_Start = 0;
        public int Flag_Stop = 0;
        public int Flag_Finish_GainThread = 0;
        public int Flag_Finish_Monitor = 0;
        private int Flag_Finish_GL = 0;

        //Для вывода//
        public long Lenght_Source = 0;
        public long Position_Source = 0;
        public int ProgressPercent = 0;
        public Stopwatch SW_Gl = new Stopwatch();
        public string Time1;

        //Конструктор
        public Archiving(string[] _SourceFileArr, string _FileName_Finish)
        {
            SourceFileArr = _SourceFileArr;
            FileName_Finish = _FileName_Finish;
            //MessageBox.Show("Количество файлов " + SourceFileArr.Length);
        }

        private void LockField_FileStream_Compressed()
        {
            byte[] byteArr;
            while (Flag_Stop != 1)
            {
                try
                {
                    if (Dict_Gl.ContainsKey(Byte_Priority_Compression) == true)
                    {
                        //target_stream.Write(Dict_Gl[Byte_Priority_Compression], 0, Dict_Gl[Byte_Priority_Compression].Length);
                        //Dict_Gl[Byte_Priority_Compression] = null;
                        //Event_LogMess?.Invoke("Количество пакетов " + Dict_Gl.Count);
                        Dict_Gl.TryRemove(Byte_Priority_Compression, out byteArr);
                        target_stream.Write(byteArr, 0, byteArr.Length);
                        ProgressPercent = (int)((100 * Byte_Priority_Compression) / SumPackages);
                        Byte_Priority_Compression++;                        
                    }

                }
                catch (Exception ex)
                {
                    Event_LogMess?.Invoke(this.GetType() + " : " + new StackTrace(false).GetFrame(0).GetMethod().Name + " : " + ex.Message);
                }
            }
        }

        private void LockField_Compressed()
        {
            byte[] ByteArr = new byte[PartFile_Lenght];
            byte[] ByteArr2;
            MemoryStream StreamPart;
            GZipStream compression_stream;
            int Priority = 0;
            int Read = 0;

            try
            {
                while (Flag_Finish_GL == 0)
                {
                    LockField_Withdrawal(ByteArr, ref Priority, ref Read);

                    if (Flag_Finish_GL == 0)
                    {
                        StreamPart = new MemoryStream();
                        compression_stream = new GZipStream(StreamPart, CompressionMode.Compress);
                        compression_stream.Write(ByteArr, 0, Read);
                        compression_stream.Close();
                        ByteArr2 = StreamPart.ToArray();
                        StreamPart = null;
                        SizeFile[Priority - 1] = ByteArr2.Length;
                        Dict_Gl.TryAdd(Priority, ByteArr2);
                        Event_LogMess?.Invoke("Пакет - " + Priority);
                    }
                }
            }
            catch (Exception ex)
            {
                Event_LogMess?.Invoke(this.GetType() + " : " + new StackTrace(false).GetFrame(0).GetMethod().Name + " : " + ex.Message);
            }
        }

        private void LockField_Withdrawal(byte[] ByteArr, ref int Priority, ref int Read)
        {
            int RAM_Buff = 0;
            int number_stream = 0;
            lock (Locker_Withdrawal)
            {
                try
                {
                    //RAM_Buff = (int)Process.GetProcessesByName("ArchiverGT")[0].WorkingSet64 / 1024 / 1024;
                    //if (RAM_Buff > 300)
                    //{
                    //    Monitor.Wait(Locker_Withdrawal);
                    //}
                    if (Flag_Finish_GL == 0)
                    {
                        if ((my_stream.Position != my_stream.Length))
                        {
                            Byte_Priority_Withdrawal++;
                            Priority = Byte_Priority_Withdrawal;
                            number_stream = my_stream.Numberstream(my_stream.Position);
                            Number_Packages[number_stream] = Priority;
                            Read = my_stream.Read(ByteArr, 0, PartFile_Lenght);
                        }
                        else
                        {
                            Flag_Finish_GL = 1;
                            //Monitor.Wait(Locker_Withdrawal);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Event_LogMess?.Invoke(this.GetType() + " : " + new StackTrace(false).GetFrame(0).GetMethod().Name + " : " + ex.Message);
                }
            }
        }

        public void ArcMethod_Decompress_GZip_Custom()
        {
            byte[] ByteArr = new byte[1000000];
            byte[] ByteArr2 = new byte[1000000];
            int Read = 0;
            int Read2 = 0;
            MemoryStream Part2;

            CompressedFile = SourceFileArr[0];
            TargetFile = Path.GetDirectoryName(SourceFileArr[0]);

            var sw = new Stopwatch();
            SW_Gl = sw;
            sw.Start();

            try
            {
                using (FileStream source_stream = new FileStream(CompressedFile, FileMode.Open, FileAccess.Read))
                {
                    int size = 0;
                    byte[] bytes_size = new byte[4];                //
                    byte[] bytes_format = new byte[10];             //
                    byte[] bytes_names = new byte[100];              //
                    byte[] bytes_NumberFile = new byte[4];          //
                    byte[] bytes_Number_Packages = new byte[7];     //
                    byte[] bytes_CountFile = new byte[4];           //
                    byte[] bytes_SizeFile = new byte[7];            //                   

                    source_stream.Position = 0;

                    //Количество файлов
                    source_stream.Read(bytes_NumberFile, 0, 4);
                    NumberFile = BitConverter.ToInt32(bytes_NumberFile, 0);
                    MessageBox.Show("Количество файлов " + NumberFile);

                    //Количество пакетов в файлах
                    for (int i = 0; i < NumberFile; i++)
                    {
                        source_stream.Read(bytes_Number_Packages, 0, 4);
                        Number_Packages[i] = BitConverter.ToInt32(bytes_Number_Packages, 0);
                        //MessageBox.Show("Количество пакетов файла " + i + " " + Number_Packages[i]);
                    }

                    /////Имена файлов\\\\\
                    //////////////////////
                    string[] Names = new string[NumberFile];
                    for (int i = 0; i < NumberFile; i++)
                    {
                        //
                        source_stream.Read(bytes_size, 0, 1);
                        size = (int)bytes_size[0];
                        //MessageBox.Show("size " + size);
                        //
                        source_stream.Read(bytes_names, 0, size);
                        Names[i] = Encoding.UTF8.GetString(bytes_names, 0, size);
                        //MessageBox.Show("Name " + Names);
                    }

                    //Общее количество пакетов
                    source_stream.Read(bytes_CountFile, 0, 4);
                    CountFile = BitConverter.ToInt32(bytes_CountFile, 0);
                    MessageBox.Show("CountFile " + CountFile);

                    //Количество байтов в пакетах
                    for (int i = 0; i < CountFile; i++)
                    {
                        source_stream.Read(bytes_SizeFile, 0, 4);
                        SizeFile[i] = BitConverter.ToInt32(bytes_SizeFile, 0);
                    }

                    source_stream.Position = 4 * 1000000 + 4 * 10000 + 4 + 4 + 200000;

                    Event_LogMess?.Invoke("Разархивирование GZip");

                    int loc_count = 0;
                    for (int i = 0; i < NumberFile; i++)
                    {
                        //Поток для записи востановленного файла
                        using (FileStream target_stream = new FileStream(TargetFile + @"\" + Names[i], FileMode.Create, FileAccess.Write))
                        {
                            for (int j = loc_count; j < Number_Packages[i]; j++)
                            {
                                ByteArr = new byte[1200000];
                                ByteArr2 = new byte[1200000];
                                Part2 = new MemoryStream();
                                using (MemoryStream Part = new MemoryStream())
                                {
                                    Read = source_stream.Read(ByteArr, 0, SizeFile[loc_count]);
                                    Part.Write(ByteArr, 0, Read);
                                    Part.Position = 0;

                                    using (GZipStream decompression_stream = new GZipStream(Part, CompressionMode.Decompress))
                                    {
                                        decompression_stream.CopyTo(Part2);
                                        Part2.Position = 0;
                                        Read2 = Part2.Read(ByteArr2, 0, (int)Part2.Length);
                                        target_stream.Write(ByteArr2, 0, Read2);
                                        ProgressPercent = (100 * loc_count) / (CountFile - 1);
                                    }
                                }
                                loc_count++;
                            }
                        }
                    }
                    //Завершение разархивирования
                    Flag_Stop = 1;

                }
                Event_LogMess?.Invoke("Файл разархивирован");
            }
            catch (Exception ex)
            {
                Event_LogMess?.Invoke(this.GetType() + " : " + new StackTrace(false).GetFrame(0).GetMethod().Name + " : " + ex.Message);
            }

            sw.Stop();
            Time1 = Convert.ToString(sw.ElapsedMilliseconds);
            Event_LogMess?.Invoke("Время " + Time1);
        }

        public void Gain_Thread()
        {
            var sw = new Stopwatch();
            SW_Gl = sw;
            sw.Start();

            Event_LogMess?.Invoke("Подготовка к архивированию");
            Flag_Start = 1;
            status = 1;
            CompressedFile = Path.GetDirectoryName(SourceFileArr[0]) + @"\" + FileName_Finish + ".gz";

            try
            {
                my_stream = new MyStream(SourceFileArr);

                Lenght_Source = my_stream.Length;
                for (int i = 0; i < my_stream.NumberStream; i++)
                {
                    SumPackages += (int)(my_stream.LengthStreams[i] / PartFile_Lenght);
                    SumPackages = ((int)(my_stream.LengthStreams[i] % PartFile_Lenght)) > 0 ? (SumPackages + 1) : SumPackages;
                }

                target_stream = new FileStream(CompressedFile, FileMode.Create, FileAccess.Write);
                target_stream.Position = 4 * 1000000 + 4 * 10000 + 4 + 4 + 200000;
            }
            catch (Exception ex)
            {
                Event_LogMess?.Invoke(this.GetType() + " : " + new StackTrace(false).GetFrame(0).GetMethod().Name + " : " + ex.Message);
            }

            //Пуск потока, который записывает сжатые куски в файл
            var VarThread = new Thread(this.LockField_FileStream_Compressed)
            {
                IsBackground = true,
                Name = $"Write file thread"
            };
            VarThread.Start();

            Event_LogMess?.Invoke("Архивирование началось");
            status = 2;

            //Пуск потоков которые читают и сжимают куски файлов
            for (int i = 0; i < 10; i++)
            {
                VarThread = new Thread(this.LockField_Compressed)
                {
                    IsBackground = true,
                    Name = $"Compressed Thread {i}"
                };
                VarThread.Start();
            }

            while (Flag_Finish_GainThread == 0)
            {
                if (SumPackages + 1 == Byte_Priority_Compression)
                {
                    CountFile = Byte_Priority_Withdrawal; //

                    if (target_stream.CanSeek == true)
                    {
                    }

                    target_stream.Position = 0;

                    //Передаем количество файлов
                    byte[] NumberFile_Bytes = BitConverter.GetBytes(my_stream.NumberStream);
                    target_stream.Write(NumberFile_Bytes, 0, 4);

                    //Передаем количество пакетов в файлах
                    byte[][] Number_Packages_Bytes = new byte[10000][];
                    for (int i = 0; i < my_stream.NumberStream; i++)
                    {
                        Number_Packages_Bytes[i] = BitConverter.GetBytes(Number_Packages[i]);
                    }
                    for (int i = 0; i < my_stream.NumberStream; i++)
                    {
                        target_stream.Write(Number_Packages_Bytes[i], 0, 4);
                    }

                    /////Имена файлов\\\\\
                    //////////////////////
                    for (int i = 0; i < my_stream.NumberStream; i++)
                    {
                        byte[] Name_Bytes = Encoding.UTF8.GetBytes(Path.GetFileName(SourceFileArr[i]));
                        byte[] Name_Bytes_Size = new byte[1];
                        Name_Bytes_Size[0] = (byte)(Name_Bytes.Length);

                        //Передача формата
                        target_stream.Write(Name_Bytes_Size, 0, 1);
                        target_stream.Write(Name_Bytes, 0, Name_Bytes.Length);
                    }

                    //Общее количество пакетов
                    byte[] CountFile_Bytes = BitConverter.GetBytes(CountFile);
                    target_stream.Write(CountFile_Bytes, 0, CountFile_Bytes.Length);

                    //Количество байтов в пакетах
                    byte[][] SizeFile_Bytes = new byte[1000000][];
                    for (int i = 0; i < CountFile; i++)
                    {
                        SizeFile_Bytes[i] = BitConverter.GetBytes(SizeFile[i]);
                    }
                    for (int i = 0; i < CountFile; i++)
                    {
                        target_stream.Write(SizeFile_Bytes[i], 0, 4);
                    }

                    /////Флаги\\\\\
                    ///////////////
                    Flag_Stop = 1;
                    Flag_Finish_GainThread = 1;
                    status = 3;

                    sw.Stop();
                    Time1 = Convert.ToString(sw.ElapsedMilliseconds);
                    Event_LogMess?.Invoke("Архивирование закончено");
                    Event_LogMess?.Invoke("Время " + Time1);
                    my_stream.Close();
                    target_stream.Close();
                    this.Dispose();
                }
            }
        }

        public void CancelThreads()
        {
            Flag_Finish_GainThread = 1;
            Flag_Finish_GL = 1;
            Flag_Stop = 1;
            Flag_Finish_Monitor = 1;
        }

        public void Dispose()
        {
            my_stream.Close();
            target_stream.Close();
            CancelThreads();
        }
    }
}
