using easyLib;
using static System.Diagnostics.Debug;
using DGD.HubCore.DLG;
using System;

namespace DGD.HubCore.DB
{
    public interface IProfileRow: IDataRow
    {
        string Name { get; }
        ProfilePrivilege_t Privilege { get; }
    }


    public class ProfileRow: DataRow, IProfileRow
    {
        public ProfileRow(uint id, string name, ProfilePrivilege_t privilege = ProfilePrivilege_t.Default):
            base(id)
        {
            Name = name ?? string.Empty;
        }

        public ProfileRow():
            this(0,null)
        { }


        public string Name { get; private set; }
        public ProfilePrivilege_t Privilege { get; private set; }
      

        //protected:
        protected override void DoRead(IReader reader)
        {
            Name = reader.ReadString();
            byte privilege = reader.ReadByte();

            if (string.IsNullOrWhiteSpace(Name) || !Enum.IsDefined(typeof(ProfilePrivilege_t) , privilege))
                throw new CorruptedStreamException();
        }

        protected override void DoWrite(IWriter writer)
        {
            Assert(!string.IsNullOrWhiteSpace(Name));
            writer.Write(Name);
            writer.Write((byte)Privilege);
        }

        protected override string[] GetContent()
        {
            return new[]
            {
                ID.ToString(),
                Name,
                ProfilePrivileges.GetPrivilegeName(Privilege)
            };
        }
    }
}
