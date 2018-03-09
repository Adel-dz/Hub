using DGD.HubCore.DB;
using easyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static System.Diagnostics.Debug;



namespace DGD.HubCore.Updating
{
    public static class UpdateEngin
    {
        const string TABLES_UPDATE_SIGNATURE = "HGTBLUPDATE1";
        const string GLOBAL_MANIFEST_SIGNATURE = "HGUM1";
        const string DATA_MANIFEST_SIGNATURE = "HGDUM1";
        const string APP_MANIFEST_SIGNATURE = "HAUM1";


        public static void WriteUpdateManifest(IUpdateManifest manifset , string filePath)
        {
            Assert(manifset != null);

            using (FileStream fs = File.Create(filePath))
            {
                var bw = new BinaryWriter(fs);
                bw.Write(Encoding.UTF8.GetBytes(GLOBAL_MANIFEST_SIGNATURE));

                bw.Write(manifset.UpdateKey);
                bw.Write(manifset.DataGeneration);
                bw.Write(manifset.Versions.Keys.Count());

                foreach (AppArchitecture_t arch in manifset.Versions.Keys)
                {
                    Version ver = manifset.Versions[arch];

                    bw.Write((byte)arch);
                    bw.Write(ver.Major);
                    bw.Write(ver.Minor);
                    bw.Write(ver.Build);
                    bw.Write(ver.Revision);
                }
            }
        }

        public static UpdateManifest ReadUpdateManifest(string filePath)
        {
            using (FileStream fs = File.OpenRead(filePath))
            {
                var br = new BinaryReader(fs);

                byte[] sign = Encoding.UTF8.GetBytes(GLOBAL_MANIFEST_SIGNATURE);

                foreach (byte b in sign)
                    if (b != br.ReadByte())
                        throw new CorruptedStreamException();

                uint updateKey = br.ReadUInt32();
                uint dataGen = br.ReadUInt32();
                int archCount = br.ReadInt32();

                var dict = new Dictionary<AppArchitecture_t , Version>(archCount);

                for (int i = 0; i < archCount; ++i)
                {
                    byte arch = br.ReadByte();

                    if (!Enum.IsDefined(typeof(AppArchitecture_t) , arch))
                        throw new CorruptedFileException(filePath);

                    int maj = br.ReadInt32();
                    int min = br.ReadInt32();
                    int build = br.ReadInt32();
                    int rev = br.ReadInt32();

                    Version ver;

                    if (build == -1)
                        ver = new Version(maj , min);
                    else if (rev == -1)
                        ver = new Version(maj , min , build);
                    else
                        ver = new Version(maj , min , build , rev);

                    dict[(AppArchitecture_t)arch] = ver;
                }

                return new UpdateManifest(updateKey , dataGen , dict);
            }
        }

        public static void UpdateDataManifest(string filePath , UpdateURI updateURI)
        {
            Assert(updateURI != null);

            using (FileStream fs = File.Open(filePath , FileMode.OpenOrCreate , FileAccess.ReadWrite))
            {
                var writer = new RawDataWriter(fs , Encoding.UTF8);
                byte[] sign = Encoding.UTF8.GetBytes(DATA_MANIFEST_SIGNATURE);

                if (fs.Length == 0)
                {
                    writer.Write(sign);
                    writer.Write(1);
                }
                else
                {
                    var reader = new RawDataReader(fs , Encoding.UTF8);

                    for (int i = 0; i < sign.Length; ++i)
                        if (reader.ReadByte() != sign[i])
                            throw new CorruptedFileException(filePath);

                    int uriCount = reader.ReadInt();
                    fs.Position -= sizeof(int);
                    writer.Write(uriCount + 1);
                    fs.Seek(0 , SeekOrigin.End);
                }

                writer.Write(updateURI.DataPreGeneration);
                writer.Write(updateURI.DataPostGeneration);
                writer.Write(updateURI.FileURI);
            }
        }

        public static void WriteAppManifest(string filePath , IReadOnlyDictionary<AppArchitecture_t , string> updates)
        {
            Assert(updates != null);

            using (FileStream fs = File.Open(filePath , FileMode.OpenOrCreate , FileAccess.ReadWrite))
            {
                var writer = new RawDataWriter(fs , Encoding.UTF8);
                byte[] sign = Encoding.UTF8.GetBytes(APP_MANIFEST_SIGNATURE);

                writer.Write(sign);
                writer.Write(updates.Count);

                foreach (KeyValuePair<AppArchitecture_t , string> kv in updates)
                {
                    writer.Write((byte)kv.Key);
                    writer.Write(kv.Value);
                }

            }
        }

        public static Dictionary<AppArchitecture_t , string> ReadAppManifest(string filePath)
        {
            using (FileStream fs = File.OpenRead(filePath))
            {
                var reader = new RawDataReader(fs , Encoding.UTF8);
                byte[] sign = Encoding.UTF8.GetBytes(APP_MANIFEST_SIGNATURE);

                foreach (byte b in sign)
                    if (b != reader.ReadByte())
                        throw new CorruptedFileException(filePath);

                int nbKey = reader.ReadInt();
                var dict = new Dictionary<AppArchitecture_t , string>(nbKey);

                for (int i = 0; i < nbKey; ++i)
                {
                    byte arch = reader.ReadByte();

                    if (!Enum.IsDefined(typeof(AppArchitecture_t) , arch))
                        throw new CorruptedFileException(filePath);

                    string fileName = reader.ReadString();
                    dict[(AppArchitecture_t)arch] = fileName;
                }

                return dict;
            }
        }

        public static IEnumerable<UpdateURI> ReadDataManifest(string filePath , uint startGeneration = 0)
        {
            using (FileStream fs = File.OpenRead(filePath))
            {
                var reader = new RawDataReader(fs , Encoding.UTF8);

                byte[] sign = Encoding.UTF8.GetBytes(DATA_MANIFEST_SIGNATURE);

                for (int i = 0; i < sign.Length; ++i)
                    if (reader.ReadByte() != sign[i])
                        throw new CorruptedFileException(filePath);

                int uriCount = reader.ReadInt();
                var lst = new List<UpdateURI>();

                for (int i = 0; i < uriCount; ++i)
                {
                    uint preGen = reader.ReadUInt();
                    uint postGen = reader.ReadUInt();
                    string uri = reader.ReadString();

                    if (preGen >= startGeneration)
                        lst.Add(new UpdateURI(uri , preGen , postGen));
                }


                return lst;
            }
        }

        public static void SaveTablesUpdate(List<TableUpdate> updates , string filePath)
        {
            Assert(updates != null);


            using (FileStream fs = File.Create(filePath))
            {
                var writer = new RawDataWriter(fs , Encoding.UTF8);

                writer.Write(Encoding.UTF8.GetBytes(TABLES_UPDATE_SIGNATURE));
                writer.Write(updates.Count);

                foreach (TableUpdate update in updates)
                {
                    PushTableUpdate(writer , update);
                }
            }
        }

        public static IEnumerable<TableUpdate> LoadTablesUpdate(string filePath , IDatumFactory dataFactory)
        {

            using (FileStream fs = File.OpenRead(filePath))
            {
                var reader = new RawDataReader(fs , Encoding.UTF8);
                byte[] sign = Encoding.UTF8.GetBytes(TABLES_UPDATE_SIGNATURE);

                for (int i = 0; i < sign.Length; ++i)
                    if (reader.ReadByte() != sign[i])
                        throw new CorruptedFileException(filePath);

                int tableCount = reader.ReadInt();
                var lst = new List<TableUpdate>(tableCount);

                for (int i = 0; i < tableCount; ++i)
                    lst.Add(LoadTableUpdate(reader , dataFactory));

                return lst;
            }
        }


        //private:
        static TableUpdate LoadTableUpdate(IReader reader , IDatumFactory dataFactory)
        {
            uint idTable = reader.ReadUInt();
            uint preGen = reader.ReadUInt();
            uint postGen = reader.ReadUInt();
            int szDatum = reader.ReadInt();
            int actionCount = reader.ReadInt();

            var lst = new List<IUpdateAction>(actionCount);

            for (int i = 0; i < actionCount; ++i)
            {
                var code = (ActionCode_t)reader.ReadByte();

                switch (code)
                {
                    case ActionCode_t.ResetTable:
                    lst.Add(new ResetTable());
                    break;

                    case ActionCode_t.DeleteRow:
                    {
                        var delAction = new DeleteRow(0);
                        delAction.Read(reader);
                        lst.Add(delAction);
                    }
                    break;

                    case ActionCode_t.ReplaceRow:
                    {
                        var repAction = new ReplaceRow(dataFactory.CreateDatum(idTable));
                        repAction.Read(reader);
                        lst.Add(repAction);
                    }
                    break;

                    case ActionCode_t.AddRow:
                    {
                        var addAction = new AddRow(dataFactory.CreateDatum(idTable));
                        addAction.Read(reader);
                        lst.Add(addAction);
                    }
                    break;

                    default:
                    Assert(false);
                    break;
                }
            }

            return new TableUpdate(idTable , lst , szDatum , preGen , postGen);
        }

        static void PushTableUpdate(IWriter writer , TableUpdate update)
        {
            writer.Write(update.TableID);
            writer.Write(update.PreGeneration);
            writer.Write(update.PostGeneration);
            writer.Write(update.DatumMaxSize);
            writer.Write(update.Actions.Count());

            foreach (IUpdateAction action in update.Actions)
            {
                writer.Write((byte)action.Code);
                action.Write(writer);
            }
        }
    }
}
