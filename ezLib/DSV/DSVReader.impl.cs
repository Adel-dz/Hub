using System.Collections.Generic;
using static System.Diagnostics.Debug;
using System.IO;
using System.Linq;
using System.Text;

namespace easyLib.DSV
{
    partial class DSVReader
    {
        struct DSVReadEngin
        {
            enum BuildState
            {
                None,
                BuildingRecord,                             
                BuildingField,
                BuildingQuotedField,
                PostBuildingQuotedField,
                End
            }

            readonly DSVReader m_owner;         
            readonly StringBuilder m_strBuilder;
            readonly List<string> m_record;
            BuildState m_state;
            int m_maxFieldCount;            


            public DSVReadEngin(DSVReader owner)
            {
                m_owner = owner;
                m_strBuilder = new StringBuilder();
                m_record = new List<string>();
                m_state = BuildState.None;
                m_maxFieldCount = 0;
            }


            public List<string> ReadNextRow(int maxFieldCount = int.MaxValue)
            {
                Assert(m_owner != null);
                Assert(m_strBuilder != null && m_strBuilder.Length == 0);

                if (m_owner.m_reader.Peek() == -1)
                    return null;

                m_maxFieldCount = maxFieldCount;
                m_state = BuildState.BuildingRecord;
                m_record.Clear();

                do
                    switch (m_state)
                    {
                        case BuildState.BuildingRecord:
                            BuildRecord();
                            break;
                        case BuildState.BuildingField:
                            BuildField();
                            break;
                        case BuildState.BuildingQuotedField:
                            BuildQuotedField();
                            break;
                        case BuildState.PostBuildingQuotedField:
                            PostBuildQuotedField();
                            break;                           
                    }
                while (m_state != BuildState.End);

                return m_record.ToList();
            }

            public string ReadNextValue()
            {
                Assert(m_owner != null);
                Assert(m_strBuilder != null && m_strBuilder.Length == 0);

                int ccode = m_owner.m_reader.Read();
                if (ccode == -1)
                    return null;

                char c = (char)ccode;

                if (c == m_owner.m_strDelimiter)
                    m_state = BuildState.BuildingQuotedField;
                else if (c == m_owner.m_recordSep[0])
                    if (ConsumeRS())
                        return string.Empty;
                    else
                        m_state = BuildState.BuildingField;
                else if (c == m_owner.m_fieldSep)
                    return string.Empty;
                else
                {
                    m_strBuilder.Append(c);
                    m_state = BuildState.BuildingField;
                }


                m_record.Clear();
                m_maxFieldCount = 1;

                while (m_state != BuildState.End)
                    switch (m_state)
                    {
                        case BuildState.BuildingRecord:
                            m_state = BuildState.End;
                            break;
                        case BuildState.BuildingField:
                            BuildField();
                            break;
                        case BuildState.BuildingQuotedField:
                            BuildQuotedField();
                            break;
                        case BuildState.PostBuildingQuotedField:
                            PostBuildQuotedField();
                            break;
                        default:
                            break;
                    }

                Assert(m_record.Count == 1);
                return m_record[0];
            }



            //private:
            void BuildRecord()
            {
                Assert(m_strBuilder.Length == 0);

                do
                {
                    int ccode = m_owner.m_reader.Read();

                    if (ccode == -1)
                    {
                        AddField(string.Empty);
                        m_state = BuildState.End;
                    }
                    else
                    {
                        char c = (char)ccode;

                        if (c == m_owner.m_recordSep[0])
                            if (ConsumeRS())
                            {
                                AddField(string.Empty);
                                m_state = BuildState.End;
                            }
                            else
                                m_state = BuildState.BuildingField;
                        else if (c == m_owner.m_fieldSep)
                        {
                            AddField(string.Empty);
                            m_state = BuildState.BuildingRecord;
                        }                        
                        else if (c == m_owner.m_strDelimiter)
                            m_state = BuildState.BuildingQuotedField;
                        else
                        {
                            m_strBuilder.Append(c);
                            m_state = BuildState.BuildingField;
                        }
                    }
                } while (m_state == BuildState.BuildingRecord);
            }

            void BuildField()
            {
                Assert(m_strBuilder.Length >= 1);

                do
                {
                    int ccode = m_owner.m_reader.Read();

                    if (ccode == -1)
                    {
                        AddField();
                        m_state = BuildState.End;
                    }
                    else
                    {
                        char c = (char)ccode;

                        if (c == m_owner.m_fieldSep)
                        {
                            AddField();
                            m_state = BuildState.BuildingRecord;
                        }
                        else if (c == m_owner.m_strDelimiter)
                            throw new InvalidDataException();
                        else if (c == m_owner.m_recordSep[0] && ConsumeRS())
                            {
                                AddField();
                                m_state = BuildState.End;
                            }
                        else
                            m_strBuilder.Append(c);
                    }

                } while (m_state == BuildState.BuildingField);
            }

            void BuildQuotedField()
            {
                do
                {
                    int ccode = m_owner.m_reader.Read();

                    if (ccode == -1)
                        throw new InvalidDataException();

                    var c = (char)ccode;

                    if (c == m_owner.m_strDelimiter)
                        m_state = BuildState.PostBuildingQuotedField;
                    else
                        m_strBuilder.Append(c);
                } while (m_state == BuildState.BuildingQuotedField);
            }

            void PostBuildQuotedField()
            {
                int ccode = m_owner.m_reader.Read();

                if (ccode == -1)
                    throw new InvalidDataException();

                var c = (char)ccode;

                if (c == m_owner.m_strDelimiter)
                {
                    m_strBuilder.Append(c);
                    m_state = BuildState.BuildingQuotedField;
                }
                else if (c == m_owner.m_fieldSep)
                {
                    AddField();
                    m_state = BuildState.BuildingRecord;
                }
                else if (c == m_owner.m_recordSep[0] && ConsumeRS())
                {
                    AddField();
                    m_state = BuildState.End;
                }
                else
                    throw new InvalidDataException();
            }

            void AddField()
            {
                if (m_record.Count < m_maxFieldCount)
                {
                    m_record.Add(m_strBuilder.ToString());
                    m_strBuilder.Clear();
                }
                else
                    throw new InvalidDataException();
            }

            void AddField(string str)
            {
                if (m_record.Count < m_maxFieldCount)
                    m_record.Add(str);
                else
                    throw new InvalidDataException();
            }

            bool ConsumeRS()
            {
                int ndx = 1;

                while(ndx < m_owner.m_recordSep.Length)
                {
                    int ccode = m_owner.m_reader.Peek();

                    if (ccode == -1 || (char)ccode != m_owner.m_recordSep[ndx])
                    {
                        m_strBuilder.Append(m_owner.m_recordSep, 0, ndx);
                        return false;
                    }

                    m_owner.m_reader.Read();
                    ++ndx;
                }

                return true;
            }
        }
    }
}
