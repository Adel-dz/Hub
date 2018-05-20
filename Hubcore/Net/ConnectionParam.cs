using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Diagnostics.Debug;


namespace DGD.HubCore.Net
{
    public interface IConnectionParam
    {
        string Host { get; }
        IProxy Proxy { get; }        
        ICredential Credential { get; }
    }


    public sealed class ConnectionParam: IConnectionParam
    {
        public ConnectionParam(string host, ICredential credential = null, IProxy proxy = null )
        {
            Assert(Uri.IsWellFormedUriString(host , UriKind.Absolute));

            Host = host;
            Proxy = proxy;
            Credential = credential;
        }

        public string Host { get; }
        public IProxy Proxy { get; }
        public ICredential Credential { get; }
    }
}
