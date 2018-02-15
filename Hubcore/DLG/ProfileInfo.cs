using easyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Diagnostics.Debug;


namespace DGD.HubCore.DLG
{
    public sealed class ProfileInfo: IStorable
    {
        ProfileInfo()
        { }

        public ProfileInfo(uint id, string name, ProfilePrivilege_t privilege = ProfilePrivilege_t.Default)
        {
            ProfileID = id;
            ProfileName = name ?? "";
            ProfilePrivilege = privilege;
        }

        public uint ProfileID { get; private set; }
        public string ProfileName { get; private set; }
        public ProfilePrivilege_t ProfilePrivilege { get; private set; }

        public void Read(IReader reader)
        {
            ProfileID = reader.ReadUInt();
            ProfileName = reader.ReadString();
            byte privlege = reader.ReadByte();

            if (ProfileID == 0 || string.IsNullOrWhiteSpace(ProfileName) || 
                    !Enum.IsDefined(typeof(ProfilePrivilege_t), privlege))
                throw new CorruptedStreamException();
        }

        public void Write(IWriter writer)
        {
            Assert(ProfileID > 0);
            Assert(!string.IsNullOrWhiteSpace(ProfileName));            

            writer.Write(ProfileID);
            writer.Write(ProfileName);
            writer.Write((byte)ProfilePrivilege);
        }

        public static ProfileInfo LoadProfileInfo(IReader reader)
        {
            var prInfo = new ProfileInfo();
            prInfo.Read(reader);

            return prInfo;
        }
    }
}
