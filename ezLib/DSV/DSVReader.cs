using System;
using System.Collections.Generic;
using static System.Diagnostics.Debug;
using System.IO;


namespace easyLib.DSV
{
    public sealed partial class DSVReader
    {        
        readonly DSVReadEngin m_readEngin;
        readonly TextReader m_reader;
        readonly char m_strDelimiter;
        readonly string m_recordSep;
        readonly char m_fieldSep;
        
        

        public DSVReader(TextReader reader, string rowSeparator, char columnSeparator = ',', char stringDelimiter = '"')
        {
            Assert(reader != null);
            Assert(String.IsNullOrEmpty(rowSeparator) == false);
            Assert(columnSeparator != stringDelimiter);
            Assert(rowSeparator.IndexOf(columnSeparator) == -1);
            Assert(rowSeparator.IndexOf(stringDelimiter) == -1);

            
            m_readEngin = new DSVReadEngin(this);
            m_reader = reader;
            m_strDelimiter = stringDelimiter;
            m_recordSep = rowSeparator;
            m_fieldSep = columnSeparator;
        }

        public DSVReader(TextReader reader, char columnSeparator = ',', char stringDelimiter = '"') :
            this(reader, Environment.NewLine, columnSeparator, stringDelimiter)
        { }


        public char StringDelimiter { get { return m_strDelimiter; } }
        public char ColumnSeparator { get { return m_fieldSep; } }
        public string RowSeparator { get { return m_recordSep; } }

        public IList<string> ReadNextRow()
        {
            return m_readEngin.ReadNextRow();                       
        }

        public IEnumerable< IList<string> > ReadAllRows()
        {
            IList<string> rec = m_readEngin.ReadNextRow();

            if (rec != null)
            {
                var fieldCount = rec.Count;
                yield return rec;

                rec = m_readEngin.ReadNextRow(fieldCount);

                while(rec != null)
                {
                    if (rec.Count != fieldCount)
                        throw new InvalidDataException();

                    yield return rec;

                    rec = m_readEngin.ReadNextRow(fieldCount);
                }
            }
        }        

        public string ReadNextValue()
        {
            return m_readEngin.ReadNextValue();
        }        
    }
}
 