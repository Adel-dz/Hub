using DGD.HubCore.Updating;
using easyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace DGD.HubCore.Arch
{
    public sealed class ArchiveEngin
    {
        struct ArchiveContent: IArchiveContent
        {
            public byte[] ArchiveHeader { get; set; }
            public DateTime CreationTime { get; set; }
            public IEnumerable<string> Files { get; set; }
        }


        const string FILE_SIGNATURE = "GSS1";

        public event Action Initializing;
        public event Action Compressing;
        public event Action Restoring;
        public event Action Done;


        public void Backup(string filePath , string srcFolder , byte[] archHeader)
        {
            Initializing?.Invoke();

            FilesBag bag = CreateBag(srcFolder);

            FileStream fsDest = null;

            try
            {
                fsDest = File.Create(filePath);
                var writer = new RawDataWriter(fsDest , Encoding.UTF8);
                writer.Write(Signature);
                writer.Write(DateTime.Now);
                writer.Write(archHeader.Length);
                writer.Write(archHeader);

                Compressing?.Invoke();
                bag.Compress(fsDest);

                Done?.Invoke();
            }
            catch
            {
                if (fsDest != null)
                {
                    fsDest.Dispose();
                    File.Delete(filePath);
                }

                throw;
            }
        }

        public IArchiveContent GetArchiveContent(string filePath)
        {
            var archData = new ArchiveContent();

            using (FileStream fs = File.OpenRead(filePath))
            {
                var reader = new RawDataReader(fs , Encoding.UTF8);
                byte[] sign = Signature;

                foreach (byte b in sign)
                    if (reader.ReadByte() != b)
                        throw new CorruptedFileException(filePath);

                archData.CreationTime = reader.ReadTime();
                int len = reader.ReadInt();
                archData.ArchiveHeader = reader.ReadBytes(len);
                archData.Files = FilesBag.GetContent(fs);

                return archData;
            }
        }

        public void Restore(string filePath , string destFolder)
        {
            Initializing?.Invoke();

            using (FileStream fs = File.OpenRead(filePath))
            {
                var reader = new RawDataReader(fs , Encoding.UTF8);
                byte[] sign = Signature;

                foreach (byte b in sign)
                    if (reader.ReadByte() != b)
                        throw new CorruptedFileException(filePath);

                reader.ReadTime();  //ignore creation time
                int headerLen = reader.ReadInt();
                reader.ReadBytes(headerLen);    //ignore header

                Restoring?.Invoke();
                var bag = new FilesBag();

                Clean(destFolder);

                bag.Decompress(fs , destFolder);

                Done?.Invoke();
            }
        }

        void Clean(string folder)
        {
            string[] folders = Directory.GetDirectories(folder);
            string[] files = Directory.GetFiles(folder);

            foreach (string file in files)
                File.Delete(file);

            foreach (string dir in folders)
                Directory.Delete(dir , true);
        }

        //private:
        static byte[] Signature => Encoding.UTF8.GetBytes(FILE_SIGNATURE);

        FilesBag CreateBag(string srcFolder)
        {
            Initializing?.Invoke();

            var bag = new FilesBag();

            string[] files = Directory.GetFiles(srcFolder);

            foreach (string file in files)
                bag.Add(file);

            string[] folders = Directory.GetDirectories(srcFolder);

            foreach (string folder in folders)
                AddFiles(bag , folder , srcFolder);

            return bag;
        }

        static void AddFiles(FilesBag bag , string subFolder , string rootFolder)
        {
            string[] files = Directory.GetFiles(subFolder);
            string relDir = subFolder.Remove(0 , rootFolder.Length + 1);

            foreach (string file in files)
                bag.Add(file , relDir);

            string[] folders = Directory.GetDirectories(subFolder);

            foreach (string folder in folders)
                AddFiles(bag , folder , rootFolder);
        }
    }
}
