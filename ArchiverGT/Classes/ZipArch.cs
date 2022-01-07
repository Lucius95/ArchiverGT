using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Diagnostics;

namespace ArchiverGT.Classes
{
    class ZipArch
    {
        public string _path;
        public Stopwatch SW_Gl = new Stopwatch();
        public string Time1;

        public ZipArch(string path)
        {
            _path = path;
        }
        public void ZipMet()
        {
            var sw = new Stopwatch();
            SW_Gl = sw;
            sw.Start();

            string sourceFolder = _path; // исходная папка
            string zipFile = _path + ".zip"; // сжатый файл
            ZipFile.CreateFromDirectory(sourceFolder, zipFile);

            sw.Stop();
            Time1 = Convert.ToString(sw.ElapsedMilliseconds);
        }
    }
}
