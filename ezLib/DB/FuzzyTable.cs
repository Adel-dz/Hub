using System.Collections.Generic;


namespace easyLib.DB
{
    public abstract partial class FuzzyTable<T>: DataTable<T>
        where T: IStorable, IDatum, new()
    {
        const byte DELETED_DATUM_TAG = 0XFF;
        readonly FileHeader m_header = new FileHeader();
        readonly List<long> m_positions = new List<long>();
        long m_newItemPos;
        
        
        protected FuzzyTable(uint id, string name , string filePath) :
            base(id, name , filePath)
        { }


        //protected:        
        protected override TableHeader Header => m_header;

        protected override int DoInsert(T item)
        {
            Writer.Position = m_newItemPos;

            var dh = new DatumHolder(item);
            dh.Write(Writer);

            m_positions.Add(m_newItemPos);
            m_newItemPos = Writer.Position;
            ++m_header.DataCount;

            return m_positions.Count - 1;
        }

        protected override void DoDelete(int ndxItem)
        {
            Writer.Position = m_positions[ndxItem];
            Writer.Write(DELETED_DATUM_TAG);
            m_positions.RemoveAt(ndxItem);
        }

        protected override void DoDispose()
        {
            m_positions.Clear();
        }

        protected override T DoRead(int ndxItem)
        {
            Reader.Position = m_positions[ndxItem] + sizeof(byte);

            T item = CreateEmptyDatum();
            item.Read(Reader);

            return item;
        }

        protected override int DoReplace(int ndxItem , T item)
        {
            if (ndxItem == m_positions.Count - 1)
            {
                Writer.Position = m_positions[ndxItem];

                var dh = new DatumHolder(item);
                dh.Write(Writer);
                m_newItemPos = Writer.Position;
            }
            else
            {
                Writer.Position = m_positions[ndxItem];
                Writer.Write(DELETED_DATUM_TAG);
                m_positions.RemoveAt(ndxItem);

                Writer.Position = m_newItemPos;
                var dh = new DatumHolder(item);
                dh.Write(Writer);
                m_positions.Add(m_newItemPos);
                m_newItemPos = Writer.Position;
                ++m_header.DataCount;
            }


            return m_positions.Count - 1;
        }

        protected override int GetDataCount()
        {
            return m_positions.Count;
        }

        protected override void Init()
        {
            Reader.Position = m_header.DataPosition;

            var dh = new DatumHolder(CreateEmptyDatum());

            for (int i = 0; i < m_header.DataCount; ++i)
            {
                long pos = Reader.Position;

                dh.Read(Reader);

                if (dh.Tag != DELETED_DATUM_TAG)
                    m_positions.Add(pos);
            }

            m_newItemPos = Reader.Position;
        }


        //private:
        T CreateEmptyDatum()
        {
            return new T();
        }
    }
}
