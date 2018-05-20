using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Diagnostics.Debug;


namespace DGD.HubCore.Net
{
    public interface IProxy
    {
        string Host { get; }
        ushort Port { get; }
    }


    public class Proxy: IProxy
    {
        public Proxy(string host, ushort port)
        {
            Assert(host != null);

            Host = host;
            Port = port;

        }

        public string Host { get; }
        public ushort Port { get; }

        public static IProxy DetectProxy()
        {
            IWebProxy proxy = WebRequest.GetSystemWebProxy();
            Uri uri = proxy.GetProxy(new Uri("http://test.com"));

            if (uri.Authority != uri.Host)
                return new Proxy(uri.Host , (ushort)uri.Port);

            return null;
        }
    }
}
