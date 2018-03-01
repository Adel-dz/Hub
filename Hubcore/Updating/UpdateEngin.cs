using DGD.HubCore.DB;
using easyLib;
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


        public static void WriteUpdateManifest(UpdateManifest manifset,string filePath)
        {
            Assert(manifset != null);

            using (FileStream fs = File.Create(filePath))
            {
                var bw = new BinaryWriter(fs);
                bw.Write(Encoding.UTF8.GetBytes(GLOBAL_MANIFEST_SIGNATURE));

                bw.Write(manifset.UpdateKey);
                bw.Write(manifset.DataGeneration);
                bw.Write(manifset.AppGeneration);
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

                return new UpdateManifest(br.ReadUInt32(), br.ReadUInt32() , br.ReadUInt32());
            }
        }

        public static void UpdateDataManifest(string filePath, UpdateURI updateURI)
        {
            Assert(updateURI != null);


            using (FileStream fs = File.Open(filePath , FileMode.OpenOrCreate, FileAccess.ReadWrite))
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

        public static IEnumerable<UpdateURI> ReadDataManifest(string filePath, uint startGeneration = 0)
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

                for(int i = 0; i < uriCount; ++i)
                {
                    uint preGen = reader.ReadUInt();
                    uint postGen = reader.ReadUInt();
                    string uri = reader.ReadString();

                    if (preGen >= startGeneration)
                        lst.Add(new UpdateURI(uri , preGen, postGen));
                }


                return lst;
            }
        }

        public static void SaveTablesUpdate(List<TableUpdate> updates, string filePath)
        {
            Assert(updates != null);
            

            using (FileStream fs = File.Create(filePath))
            {
                var writer = new RawDataWriter(fs , Encoding.UTF8);

                writer.Write(Encoding.UTF8.GetBytes(TABLES_UPDATE_SIGNATURE));
                writer.Write(updates.Count);

                foreach (TableUpdate update in updates)
                {
                    PushTableUpdate(writer, update);
                }
            }
        }

        public static IEnumerable<TableUpdate> LoadTablesUpdate(string filePath, IDatumFactory dataFactory)
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
                    lst.Add(LoadTableUpdate(reader, dataFactory));

                return lst;
            }
        }


        //private:
        static TableUpdate LoadTableUpdate(IReader reader, IDatumFactory dataFactory)
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

            return new TableUpdate(idTable , lst , szDatum, preGen , postGen);
        }

        static void PushTableUpdate(IWriter writer, TableUpdate update)
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
