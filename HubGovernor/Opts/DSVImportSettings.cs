using DGD.HubGovernor.TR;
using DGD.HubGovernor.TR.Imp;
using System;
using System.Collections.Generic;
using System.IO;

namespace DGD.HubGovernor.Opts
{

    sealed class DSVImportSettings
    {
        Dictionary<ColumnKey_t , int> m_colMapping = new Dictionary<ColumnKey_t, int>();

        public DSVImportSettings()
        {
            ColumnsSeparator = '|';
            DisplayCount = 16;
        }

        public char ColumnsSeparator { get; set; }
        public ushort DisplayCount { get; set; }
        public ushort LineOffset { get; set; }
        public IDictionary<ColumnKey_t , int> ColumnsMapping => m_colMapping;

        public void Reset()
        {
            ColumnsSeparator = '|';
            DisplayCount = 16;
            LineOffset = 0;
            m_colMapping.Clear();
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write(ColumnsSeparator);
            writer.Write(DisplayCount);
            writer.Write(LineOffset);

            writer.Write(ColumnsMapping.Keys.Count);
            foreach(ColumnKey_t col in m_colMapping.Keys)
            {
                writer.Write((int)col);
                writer.Write(ColumnsMapping[col]);
            }
        }

        public void Load(BinaryReader reader)
        {
            ColumnsSeparator = reader.ReadChar();
            DisplayCount = reader.ReadUInt16();
            LineOffset = reader.ReadUInt16();

            int count = reader.ReadInt32();

            if (count < 0)
                count = 0;

            var dict = new Dictionary<ColumnKey_t, int>(count);

            for(int i = 0; i < count;++i)
            {
                var col = (ColumnKey_t)reader.ReadInt32();
                int ndx = reader.ReadInt32();

                dict.Add(col , ndx);
            }

            m_colMapping = dict;
        }
    }
}
