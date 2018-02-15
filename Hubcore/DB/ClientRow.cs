using System;
using easyLib;
using static System.Diagnostics.Debug;


namespace DGD.HubCore.DB
{

    public interface IClientRow: IDataRow
    {
        uint ProfileID { get; }
        string MachineName { get; }
        string ContactName { get; }
        string ContaclEMail { get; }
        string ContactPhone { get; }
        DateTime CreationTime { get; }
    }


    public class ClientRow: DataRow, IClientRow
    {
        string m_machineName;
        string m_contactName;
        string m_contaclEMail;
        string m_contactPhone;

        public ClientRow()
        { }

        public ClientRow(uint id , uint idProfile) :
            base(id)
        {
            ProfileID = idProfile;
            CreationTime = DateTime.Now;
        }

        public uint ProfileID { get; private set; }

        public string MachineName
        {
            get { return m_machineName ?? ""; }
            set { m_machineName = value; }
        }

        public string ContactName
        {
            get { return m_contactName ?? ""; }
            set { m_contactName = value; }
        }

        public string ContaclEMail
        {
            get { return m_contaclEMail ?? ""; }
            set { m_contaclEMail = value; }
        }

        public string ContactPhone
        {
            get { return m_contactPhone ?? ""; }
            set { m_contactPhone = value; }
        }

        public DateTime CreationTime { get; set; }


        //protected:
        protected override void DoRead(IReader reader)
        {
            ProfileID = reader.ReadUInt();

            if (ProfileID == 0)
                throw new CorruptedStreamException();

            MachineName = reader.ReadString();
            ContactName = reader.ReadString();
            ContaclEMail = reader.ReadString();
            ContactPhone = reader.ReadString();
            CreationTime = reader.ReadTime();
        }

        protected override void DoWrite(IWriter writer)
        {
            Assert(ProfileID > 0);

            writer.Write(ProfileID);            
            writer.Write(MachineName);
            writer.Write(ContactName);
            writer.Write(ContaclEMail);
            writer.Write(ContactPhone);
            writer.Write(CreationTime);
        }

        protected override string[] GetContent()
        {
            return new[]
            {
                ID.ToString(),
                ProfileID.ToString(),
                CreationTime.ToString(),
                MachineName,
                ContactName,
                ContactPhone,
                ContaclEMail
            };
        }
    }

}
