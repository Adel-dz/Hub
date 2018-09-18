using DGD.HubCore.RunOnce;
using easyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGD.HubGovernor.RunOnce
{
    partial class RunOnceManager
    {
        public sealed class Entry: IStorable
        {
            Entry()
            { }
            
            public Entry(uint idClient, IRunOnceAction action)
            {
                ClientID = idClient;
                Action = action;
                CreationTime = DateTime.Now;
            }


            public uint ClientID { get; private set; }            
            public IRunOnceAction Action { get; private set; }
            public DateTime CreationTime { get; set; }

            public void Read(IReader reader)
            {
                ClientID = reader.ReadUInt();

            }

            public void Write(IWriter writer)
            {
                throw new NotImplementedException();
            }
        }
    }
}
