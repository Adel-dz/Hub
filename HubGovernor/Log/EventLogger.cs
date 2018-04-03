using System;
using System.Collections;
using System.Collections.Generic;

namespace DGD.HubGovernor.Log
{
    

    interface IEventLogger: IDisposable
    {
        bool IsDisposed { get; }

        IEventLog LogEvent(string txt, DateTime tm, EventType_t evType);        
    }
}
