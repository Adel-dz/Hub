using System;
using System.Collections;
using System.Collections.Generic;

namespace DGD.HubGovernor.Log
{



    interface IEventLogger: IDisposable
    {
        bool IsDisposed { get; }

        void LogEvent(string txt, DateTime tm, EventType_t evType, EventSource_t src , uint clientID = 0);
        IEnumerable<IEventLog> EnumerateLog(uint clID = 0);        
    }
}
