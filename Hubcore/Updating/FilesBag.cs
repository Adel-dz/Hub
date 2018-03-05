using easyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using static System.Diagnostics.Debug;

namespace DGD.HubCore.Updating
{
    public sealed class FilesBag
    {
        class FileData: IEquatable<FileData>
        {
            readonly static string m_curDirPrefix = $".{Path.DirectorySeparatorChar}";
            readonly static string m_altCurDirPrefix = $".{Path.AltDirectorySeparatorChar}";


            public FileData(string filePath , string destFolder = null)
            {
                FilePath = filePath;

                if (!string.IsNullOrWhiteSpace(destFolder))
                {
                    if (destFolder.StartsWith(m_curDirPrefix) || destFolder.StartsWith(m_altCurDirPrefix))
                        destFolder = destFolder.Remove(0 , m_curDirPrefix.Length);
                    else if (destFolder[0] == Path.DirectorySeparatorChar || destFolder[1] == Path.AltDirectorySeparatorChar)
                        destFolder = destFolder.Remove(0 , 1);

                    int len = destFolder.Length;

                    if (len > 0 && destFolder[len - 1] != Path.DirectorySeparatorChar &&
                            destFolder[len - 1] != Path.AltDirectorySeparatorChar)
                        destFolder += Path.DirectorySeparatorChar;
                }


                if (!string.IsNullOrWhiteSpace(destFolder))
                    DestFolder = destFolder;
            }


            public string FileName => Path.GetFileName(FilePath);
            public string FilePath { get; }
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
        const int NDX_CUR_FOLDER = -1;

        readonly List<FileData> m_files = new List<FileData>();


        public void Add(string filePath)
        {
            Assert(!string.IsNullOrWhiteSpace(filePath));
            Assert(!Contains(filePath) == false);

            m_files.Add(FileData.Create(filePath));
        }

        public void Add(string filePath , string relativeDestDir)
        {
            Assert(!string.IsNullOrWhiteSpace(filePath));
            Assert(!string.IsNullOrWhiteSpace(relativeDestDir));
            Assert(!Contains(filePath , relativeDestDir) == false);

            m_files.Add(FileData.Create(filePath , relativeDestDir));
        }

        public void Remove(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return;

            var fileData = new FileData(filePath);

            for (int i = 0; i < m_files.Count; ++i)
                if (fileData.Equals(fileData))
                {
                    m_files.RemoveAt(i);
                    break;
                }
        }

        public void Remove(string filePath , string relativeDestDir)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return;

            var fileData = new FileData(filePath , relativeDestDir);

            for (int i = 0; i < m_files.Count; ++i)
                if (fileData.Equals(fileData))
                {
                    m_files.RemoveAt(i);
                    break;
                }
        }


        public bool Contains(string filePath) => m_files.Contains(FileData.Create(filePath));

        public bool Contains(string filePath , string destFolder) => m_files.Contains(FileData.Create(filePath , destFolder));

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
                writer.Write(folders.Count);

                foreach (string folder in folders)
                    writer.Write(folder);

                writer.Write(m_files.Count);

                foreach (FileData fd in m_files)
                {
                    writer.Write(fd.FileName);

                    if (fd.DestFolder == null)
                        writer.Write(NDX_CUR_FOLDER);
                    else
                    {
                        string dir = fd.FileName;
                        int ndx = folders.FindIndex(x => string.Compare(dir , x , true) == 0);

                        Assert(ndx >= 0);

                        writer.Write(ndx);
                    }

                    using (FileStream fStream = File.OpenRead(fd.FilePath))
                    {
                        writer.Write(fStream.Length);
                        fStream.CopyTo(gzs);
                    }
                }
            }
        }

        public void Decompress(string filePath , string destFolder)
        {
            using (FileStream fs = File.OpenRead(filePath))
            using (var gzs = new GZipStream(fs , CompressionMode.Decompress))
            {
                var reader = new RawDataReader(gzs , Encoding.UTF8);

                foreach (byte b in Signature)
                    if (b != reader.ReadByte())
                        throw new CorruptedFileException(filePath);

                int nbDir = reader.ReadInt();
                var folders = new List<string>(nbDir);

                for (int i = 0; i < nbDir; ++i)
                {
                    string dir = Path.Combine(destFolder , reader.ReadString());
                    folders.Add(dir);
                    Directory.CreateDirectory(dir);
                }


                int nbFile = reader.ReadInt();

                for (int i = 0; i < nbFile; ++i)
                {
                    string file = reader.ReadString();
                    int ndxDir = reader.ReadInt();

                    if (ndxDir == NDX_CUR_FOLDER)
                        file = Path.Combine(destFolder , file);
                    else
                        file = Path.Combine(folders[ndxDir] , file);

                    int fileLen = reader.ReadInt();

                    CreateFile(gzs, file, fileLen);
                }
            }
        }


        //private:
        static byte[] Signature => Encoding.UTF8.GetBytes(SIGNATURE);


        static void CreateFile(Stream input , string filePath , int fileLen)
        {
            const int SZ_BUFFER = 1024;

            using (FileStream fs = File.Create(filePath))
            {
                var reader = new BinaryReader(input , Encoding.UTF8 , true);
                var writer = new BinaryWriter(fs , Encoding.UTF8);                

                while(fileLen > 0)
                {
                    byte[] buffer = reader.ReadBytes(Math.Min(SZ_BUFFER , fileLen));
                    writer.Write(buffer);
                    fileLen -= buffer.Length;
                }
            }
        }
    }
}
