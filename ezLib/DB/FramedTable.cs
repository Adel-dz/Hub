using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace easyLib.DB
{
    public abstract partial class FramedTable<T>: DataTable<T>
        where T : IStorable, IDatum, new()
    {
        const int NDX_NULL = -1;
        readonly FileHeader m_header = new FileHeader();
        readonly List<int> m_deletedFrames = new List<int>();
        readonly TableWriter m_insertWriter;
        readonly byte[] m_insertBuffer;


        //public:
        public FramedTable(uint id, string name , string filePath) :
            base(id, name , filePath)
        {
            m_insertBuffer = new byte[FrameSize];
            m_insertWriter = new TableWriter(new MemoryStream(m_insertBuffer));
        }
        

        //protected
        protected abstract int DatumSize { get; }

        protected override TableHeader Header => m_header;

        protected override int DoInsert(T item)
        {
            int ndxNew = m_header.DeletedFrameHead == NDX_NULL ?
                GetDataCount() : m_header.DeletedFrameHead;

            if (m_deletedFrames.Count > 0)
            {
                m_deletedFrames.RemoveAt(m_deletedFrames.BinarySearch(ndxNew));

                long framePos = GetFramePosition(ndxNew);

                if (m_deletedFrames.Count > 0)
                {
                    Reader.Position = framePos;
                    m_header.DeletedFrameHead = Reader.ReadInt();
                }
                else
                    m_header.DeletedFrameHead = NDX_NULL;
                
                Writer.Position = framePos;
                item.Write(Writer);
            }
            else
            {
                
                m_insertWriter.Position = 0;
                item.Write(m_insertWriter);

                Writer.Position = GetFramePosition(m_header.FrameCount);
                Writer.Write(m_insertBuffer);
                ++m_header.FrameCount;
            }

            return FrameIndexToItemIndex(ndxNew);
        }

        protected override void DoDelete(int ndxItem)
        {
            int ndxFrame = ItemIndexToFrameIndex(ndxItem);            
            Writer.Position = GetFramePosition(ndxFrame);
            Writer.Write(m_header.DeletedFrameHead);
            m_header.DeletedFrameHead = ndxFrame;

            int loc = m_deletedFrames.BinarySearch(ndxFrame);

            Debug.Assert(loc < 0);

            m_deletedFrames.Insert(~loc , ndxFrame);
        }

        protected override void DoDispose()
        {
            m_deletedFrames.Clear();
        }

        protected override T DoRead(int ndxItem)
        {
            int ndxFrame = ItemIndexToFrameIndex(ndxItem);
            Reader.Position = GetFramePosition(ndxFrame);

            T item = CreateEmptyDatum();
            item.Read(Reader);

            return item;
        }

        protected override int DoReplace(int ndxItem , T item)
        {
            int ndxFrame = ItemIndexToFrameIndex(ndxItem);
            Writer.Position = GetFramePosition(ndxFrame);
            item.Write(Writer);

            return ndxItem;
        }

        protected override int GetDataCount()
        {
            return m_header.FrameCount - m_deletedFrames.Count;
        }

        protected override void Init()
        {
            int ndxFrame = m_header.DeletedFrameHead;

            Debug.Assert(m_deletedFrames.Count == 0);

            while (ndxFrame != NDX_NULL)
            {
                m_deletedFrames.Add(ndxFrame);
                Reader.Position = GetFramePosition(ndxFrame);
                ndxFrame = Reader.ReadInt();
            }

            m_deletedFrames.Sort();
        }


        //private:
        int FrameSize => Math.Max(DatumSize , sizeof(long));
        long GetFramePosition(int ndxFrame) => Header.DataPosition + ndxFrame * FrameSize;
        T CreateEmptyDatum() => new T();

        int FrameIndexToItemIndex(int ndxFrame)
        {
            int k = m_deletedFrames.Count;

            Debug.Assert(ndxFrame < m_header.FrameCount);

            if (k == 0 || ndxFrame < m_deletedFrames[0])
                return ndxFrame;

            if (m_deletedFrames[k - 1] < ndxFrame)
                return ndxFrame - k;

            int pos = m_deletedFrames.BinarySearch(ndxFrame);

            Debug.Assert(pos < 0);
            return ndxFrame - ~pos;
        }

        int ItemIndexToFrameIndex(int ndxItem)
        {
            int k = m_deletedFrames.Count;

            if (k == 0 || ndxItem < m_deletedFrames[0])
                return ndxItem;

            if (ndxItem >= m_deletedFrames[k - 1] - k + 1)
                return ndxItem + k;

            int ndx = 1;

            while (m_deletedFrames[ndx] - ndx <= ndxItem)
                ++ndx;

            Debug.Assert(m_deletedFrames[ndx - 1] - ndx + 1 <= ndxItem);

            return ndxItem + ndx;
        }
    }
}
