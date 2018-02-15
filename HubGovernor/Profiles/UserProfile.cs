using DGD.HubCore.DB;

namespace DGD.HubGovernor.Profiles
{
    sealed class UserProfile : ProfileRow
    {
        public UserProfile(uint id, string name, HubCore.DLG.ProfilePrivilege_t privilege = 
                HubCore.DLG.ProfilePrivilege_t.Default):
            base(id, name, privilege)
        { }

        public UserProfile()
        { }


        public override string ToString() => 
            $"ID: {ID}, Name: {Name}, Privilège: {HubCore.DLG.ProfilePrivileges.GetPrivilegeName(Privilege)}";
    }
}
