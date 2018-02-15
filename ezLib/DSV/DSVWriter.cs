using System;
using System.Collections.Generic;
using static System.Diagnostics.Debug;
using System.IO;
using System.Text;

namespace easyLib.DSV
{
    public sealed class DSVWriter
    {
        readonly TextWriter m_writer;
        readonly string m_rowSep;
        readonly int m_colCount;
        int m_szCurRow;
        readonly char m_colSep;
        readonly char m_strDelimiter;


        public DSVWriter(TextWriter writer, int columnCount, char columnSeparator = ';', char strDelimiter = '"') :
            this(writer, columnCount, Environment.NewLine, columnSeparator, strDelimiter)
        { }

        public DSVWriter(TextWriter writer, int columnCount, string rowSeparator,
            char columnSeparator = ';', char strDelimiter = '"')
        {
            Assert(writer != null);
            Assert(string.IsNullOrEmpty(rowSeparator) == false);
            Assert(columnCount > 0);
            Assert(columnSeparator != strDelimiter);
            Assert(rowSeparator.IndexOf(columnSeparator) == -1);
            Assert(rowSeparator.IndexOf(strDelimiter) == -1);

            m_writer = writer;
            m_rowSep = rowSeparator;
            m_colCount = columnCount;
            m_colSep = columnSeparator;
            m_strDelimiter = strDelimiter;
        }


        public string RoWSeparator { get { return m_rowSep; } }
        public char ColumnSeparator { get { return m_colSep; } }
        public char StringDelimiter { get { return m_strDelimiter; } }
        public int ColumnCount { get { return m_colCount; } }

        public DSVWriter Write(bool data)
        {
            m_writer.Write(data);
            AdjustRow();
            return this;
        }
        public DSVWriter Write(char data)
        {
            Write(new string(data, 1));            
            return this;
        }
        public DSVWriter Write(long data)
        {
            m_writer.Write(data);
            AdjustRow();
            return this;
        }
        public DSVWriter Write(ulong data)
        {
            m_writer.Write(data);
            AdjustRow();
            return this;
        }
        public DSVWriter Write(double data)
        {
            m_writer.Write(data);
            AdjustRow();
            return this;
        }
        public DSVWriter Write(decimal data)
        {
            m_writer.Write(data);
            AdjustRow();
            return this;
        }
        public DSVWriter Write(string data)
        {
            m_writer.Write( NormalizeData(data) );
            AdjustRow();
            return this;
        }
        public DSVWriter Write(object data)
        {
            m_writer.Write(data);
            AdjustRow();
            return this;
        }
        public DSVWriter Write<T>(IEnumerable<T> items)
        {
            Assert(m_szCurRow < m_colCount);

            foreach (T data in items)            
                Write(data.ToString());
            
            return this;
        }        
        void AdjustRow()
        {
            Assert(m_szCurRow < m_colCount);

            if (++m_szCurRow == m_colCount)
            {
                m_writer.Write(m_rowSep);
                m_szCurRow = 0;
            }
            else
                m_writer.Write(m_colSep);
        }
        string NormalizeData(string data)
        {
            //TODO: a optimser!!!
            if (data == null)
                return string.Empty;

            int len = data.Length;
            bool normalized = true;
            int ndx = 0;

            for (;ndx < len && normalized;++ndx)            
                if (data[ndx] == m_rowSep[0] && ndx + m_rowSep.Length <= len)
                {
                    normalized = false;
                    for (int i = 1; i < m_rowSep.Length; ++i)
                        if (data[ndx + i] != m_rowSep[i])
                        {
                            normalized = true;
                            break;
                        }
                }
                else if (data[ndx] == m_colSep || data[ndx] == m_strDelimiter)
                    normalized = false;

            if (normalized)
                return data;

            var sb = new StringBuilder();
            sb.Append(data);

            for (int i = len - 1; i >= 0; --i)
                if (sb[i] == m_strDelimiter)                
                    sb.Insert(i, m_strDelimiter);

            sb.Insert(0, m_strDelimiter).Append(m_strDelimiter);
                

            return sb.ToString();
        }

    }
}