namespace easyLib.DB
{
    partial class FuzzyTable<T>
    {
        class DatumHolder: IStorable
        {
            public DatumHolder(T datum)
            {
                Datum = datum;
            }

            public T Datum { get; set; }
            public byte Tag { get; set; }

            public void Read(IReader reader)
            {
                Tag = reader.ReadByte();
                Datum.Read(reader);
            }

            public void Write(IWriter writer)
            {
                writer.Write(Tag);
                Datum.Write(writer);
            }
        }
    }
}
