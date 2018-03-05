using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using static System.Diagnostics.Debug;

namespace easyLib
{
    public sealed class FilesBag
    {
        const string SIGNATURE = "FBAG1";

        readonly List<string> m_files = new List<string>();


        public IEnumerable<string> Files => m_files;

        public void Add(string filePath)
        {
            Assert(!string.IsNullOrWhiteSpace(filePath));
            Assert(!Contains(Path.GetFileName(filePath)));

            m_files.Add(filePath);
        }

        public void Remove(string filePath) => m_files.Remove(filePath);


        public bool Contains(string filePath) => 
            m_files.Find(s => string.Compare(Path.GetFileName(filePath) , s , true) == 0) != null;

        public void Write(string filePath)
        {
            using (FileStream fs = File.Create(filePath))
            using (var gzs = new GZipStream(fs , CompressionMode.Compress))
            {
                var writer = new RawDataWriter(gzs , Encoding.UTF8);

                writer.Write(Signature);
                writer.Write(m_files.Count);

                foreach(string path in m_files)
                {
                    string fileName = Path.GetFileName(path);

                    using (FileStream file = File.OpenRead(path))
                    {
                        long len = file.Length;

                        writer.Write(fileName);
                        writer.Write(len);

                        file.CopyTo(gzs);
                    }
                }

            }
        }

        public void Read(string filePath)
        {
            using (FileStream fs = File.Create(filePath))
            using (var gzs = new GZipStream(fs , CompressionMode.Decompress))
            {
                var reader = new RawDataReader(gzs , Encoding.UTF8);

                foreach (byte b in Signature)
                    if (b != reader.ReadByte())
                        throw new CorruptedFileException(filePath);

                int fileCount = reader.ReadInt();

                m_files.Clear();

                for(int i = 0; i < fileCount; ++i)
                {
                    string fileName
                }
            }
        }

        public static FilesBag Load(string filePath)
        {
            throw null;
        }


        //private:
        byte[] Signature => Encoding.UTF8.GetBytes(SIGNATURE);
            
    }
}
