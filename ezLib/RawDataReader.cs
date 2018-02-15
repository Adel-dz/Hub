using System;
using System.IO;
using System.Text;
using static System.Diagnostics.Debug;



namespace easyLib
{
    public class RawDataReader: IReader
    {
        readonly BinaryReader m_reader;


        //public:
        public RawDataReader(Stream input, Encoding encoding)
        {
            Assert(input != null);
            Assert(input.CanRead);
            Assert(encoding != null);

            m_reader = new BinaryReader(input , encoding , true);
        }

        
        public int Read(byte[] buffer , int bufferOffset , int count)
        {
            Assert(buffer != null);
            Assert(bufferOffset + count <= buffer.Length);

            return m_reader.Read(buffer , bufferOffset , count);
        }

        public bool ReadBoolean()
        {            
            return m_reader.ReadBoolean();
        }

        public byte ReadByte()
        {
            return m_reader.ReadByte();
        }

        public byte[] ReadBytes(int byteCount)
        {
            Assert(byteCount >= 0);

            return m_reader.ReadBytes(byteCount);
        }

        public char ReadChar()
        {
            return m_reader.ReadChar();
        }

        public decimal ReadDecimal()
        {
            return m_reader.ReadDecimal();
        }

        public double ReadDouble()
        {
            return m_reader.ReadDouble();
        }

        public float ReadFloat()
        {
            return m_reader.ReadSingle();
        }

        public short ReadShort()
        {
            return m_reader.ReadInt16();
        }

        public int ReadInt()
        {
            return m_reader.ReadInt32();
        }

        public long ReadLong()
        {
            return m_reader.ReadInt64();
        }

        public sbyte ReadSByte()
        {
            return m_reader.ReadSByte();
        }

        public string ReadString()
        {
            return m_reader.ReadString();
        }

        public DateTime ReadTime()
        {
            return new DateTime(m_reader.ReadInt64());
        }

        public ushort ReadUShort()
        {
            return m_reader.ReadUInt16();
        }

        public uint ReadUInt()
        {
            return m_reader.ReadUInt32();
        }

        public ulong ReadULong()
        {
            return m_reader.ReadUInt64();
        }

        public void Skip(int byteCount)
        {
            Assert(byteCount >= 0);

            ReadBytes(byteCount);
        }


        //protected:
        protected BinaryReader Reader
        {
            get { return m_reader; }
        }        
    }
}
