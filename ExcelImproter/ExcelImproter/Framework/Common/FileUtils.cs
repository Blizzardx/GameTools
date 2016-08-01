using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Communication;
using Thrift.Protocol;

namespace ExcelImproter.Configs
{
    class FileUtils
    {
        #region write
        public static void WriteStringFile(string path, string content, bool isEncrypt = false)
        {
            EnsureFolder(path);
            FileStream fs = File.OpenWrite(path);
            fs.SetLength(0);
            var sw = new StreamWriter(fs);
            sw.Write(content);
            sw.Dispose();
            fs.Dispose();
        }
        public static void WriteStringFile(string path, List<string> contentList, bool isEncrypt = false)
        {
            EnsureFolder(path);
            FileStream fs = File.OpenWrite(path);
            fs.Seek(fs.Length, 0);
            var sw = new StreamWriter(fs);
            foreach (string line in contentList)
            {
                sw.WriteLine(line);
            }
            sw.Dispose();
            fs.Dispose();
        }
        public static void WriteByteFile(string path, byte[] bytes)
        {
            EnsureFolder(path);
            FileStream fs = File.OpenWrite(path);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
            fs.Dispose();
        }
        #endregion

        #region file option
        public static void DeleteFile(string filePath)
        {
            Console.WriteLine("########   " + filePath);
            if (!File.Exists(filePath))
            {
                return;
            }
            File.Delete(filePath);
        }
        public static void EnsureFolder(string path)
        {
            string folder = Path.GetDirectoryName(path);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
        }
        #endregion

        #region read
        public static string ReadStringFile(string path, bool isEncrypt = false)
        {
            string content = "";
            if (File.Exists(path))
            {
                try
                {
                    StreamReader sr = File.OpenText(path);
                    content = sr.ReadToEnd();
                    sr.Dispose();
                }
                catch (Exception e)
                {
                    content = "";
                }
            }
            else
            {
            }

            return content;
        }
        public static byte[] ReadByteFile(string path)
        {
            byte[] res = null;
            if (File.Exists(path))
            {
                try
                {
                    FileStream sr = File.OpenRead(path);
                    res = new byte[sr.Length];
                    sr.Read(res, 0, res.Length);
                    sr.Dispose();
                }
                catch (Exception e)
                {
                    res = null;
                }
            }
            else
            {
            }

            return res;
        }
        #endregion
    }
}
