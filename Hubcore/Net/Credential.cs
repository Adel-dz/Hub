using System.Net;
using static System.Diagnostics.Debug;


namespace DGD.HubCore.Net
{
    public interface ICredential
    {
        string UserName { get; }
        string Password { get; }
    }


    public class Credential: ICredential
    {
        public Credential(string userName, string pw)
        {
            Assert(!string.IsNullOrWhiteSpace(userName));
            Assert(!string.IsNullOrWhiteSpace(pw));

            UserName = userName;
            Password = pw;
        }


        public string UserName { get; }
        public string Password { get; }
    }
}
