using DGD.HubCore.DLG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGD.Hub.DLG
{
    partial class DialogManager
    {
        void NullHandler(Message msg)
        {
            Dbg.Assert(msg.MessageCode == Message_t.Null);            
        }

        void SyncHandler(Message msg)
        {
            Dbg.Assert(msg.MessageCode == Message_t.Sync);

            PostMessage(Message_t.Null, reqID:msg.ID);
        }
    }
}
