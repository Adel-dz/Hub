using DGD.HubCore.DB;
using System;
using easyLib;


namespace DGD.HubGovernor.Profiles
{
    enum ManagementMode_t: byte
    {
        Auto,
        Manual
    }

    interface IProfileManagementMode: IDataRow
    {
        uint ProfileID { get; }
        ManagementMode_t ManagementMode { get; }
    }


    sealed class ProfileManagementMode: DataRow, IProfileManagementMode
    {
        static readonly string[] m_modesName =
        {
            "Automatique",
            "Manuel"
        };

        public ProfileManagementMode()
        { }

        public ProfileManagementMode(uint idProfile, ManagementMode_t mode = ManagementMode_t.Auto):
            base(idProfile)
        {
            ManagementMode = mode;
        }


        public ManagementMode_t ManagementMode { get; set; }
        public uint ProfileID => ID;

        public static int Size => sizeof(uint) + sizeof(byte);

        public static string GetManagementModeName(ManagementMode_t mode) => m_modesName[(byte)mode];


        //protected:
        protected override void DoRead(IReader reader)
        {
            byte mode = reader.ReadByte();

            if (!Enum.IsDefined(typeof(ManagementMode_t) , mode))
                throw new CorruptedStreamException();

            ManagementMode = (ManagementMode_t)mode;
        }

        protected override void DoWrite(IWriter writer) => writer.Write((byte)ManagementMode);

        protected override string[] GetContent()
        {
            return new[]
            {
                ID.ToString(),
                GetManagementModeName(ManagementMode)
            };
        }
    }
}
