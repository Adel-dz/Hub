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
        Message DefaultProcessing(Message msg)
        {
            Dbg.Log($"Default processing for {msg.MessageCode.ToString()} msg.");

            return null;
        }

    }
}
