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
        class FileData: IEquatable<FileData>
        {
            readonly static string m_curDirPrefix = $".{Path.DirectorySeparatorChar}";
            readonly static string m_altCurDirPrefix = $".{Path.AltDirectorySeparatorChar}";


            public FileData(string fileName , string destFolder = null)
            {
                FileName = fileName;

                if (!string.IsNullOrWhiteSpace(destFolder))
                {
                    if (destFolder.StartsWith(m_curDirPrefix) || destFolder.StartsWith(m_altCurDirPrefix))
                        destFolder = destFolder.Remove(0 , m_curDirPrefix.Length);

                    int len = destFolder.Length;

                    if (len > 0 && destFolder[len - 1] != Path.DirectorySeparatorChar &&
                            destFolder[len - 1] != Path.AltDirectorySeparatorChar)
                        destFolder += Path.DirectorySeparatorChar;
                }


                if (!string.IsNullOrWhiteSpace(destFolder))
                    DestFolder = destFolder;
            }


            public string FileName { get; }
            public string DestFolder { get; }

            public bool Equals(FileData other)
            {
                if (other == null)
                    return false;

                return string.Compare(FileName , other.FileName , true) == 0 &&
                    string.Compare(DestFolder , other.DestFolder , true) == 0;
            }


            public static FileData Create(string filePath) => new FileData(Path.GetFileName(filePath));
            public static FileData Create(string filePath , string destFolder) => new FileData(Path.GetFileName(filePath) , destFolder);
        }



        const string SIGNATURE = "FBAG1";

        readonly List<FileData> m_files = new List<FileData>();


        public void Add(string file)
        {
            Assert(!string.IsNullOrWhiteSpace(file));
            Assert(!Contains(file) == false);

            m_files.Add(FileData.Create(file));
        }

        public void Add(string file , string relativeDestDir)
        {
            Assert(!string.IsNullOrWhiteSpace(file));
            Assert(!string.IsNullOrWhiteSpace(relativeDestDir));
            Assert(!Contains(file , relativeDestDir) == false);

            m_files.Add(FileData.Create(file , relativeDestDir));
        }

        public void Remove(string file)
        {
            if (string.IsNullOrWhiteSpace(file))
                return;

            var fileData = new FileData(file);

            for (int i = 0; i < m_files.Count; ++i)
                if (fileData.Equals(fileData))
                {
                    m_files.RemoveAt(i);
                    break;
                }
        }

        public bool Contains(string file) => m_files.Contains(FileData.Create(file));

        public bool Contains(string file , string destFolder) => m_files.Contains(FileData.Create(file , destFolder));

        public void Compress(string filePath)
        {
            /*
             * signature
             * folders count
             * list of folder
             * files count
             * list of:
             * * file name
             * * ndx dest folder. ndx == -1 => cur dir
             * * file len
             * * file data */

            //build folders list
            var folders = new List<string>();

            var seq = from fd in m_files
                      where fd.DestFolder != null 
                      select fd.DestFolder;


            folders.AddRange(seq.Distinct(StringComparer.OrdinalIgnoreCase));


            using (FileStream fs = File.Create(filePath))
            using (var gzs = new GZipStream(fs , CompressionMode.Compress))
            {
                var writer = new RawDataWriter(gzs , Encoding.UTF8);

                writer.Write(Signature);
                writer.Write(m_files.Count);
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

                for (int i = 0; i < fileCount; ++i)
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
