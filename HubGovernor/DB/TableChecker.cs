using System;

namespace DGD.HubGovernor.DB
{
    interface ITableChecker: IDisposable
    {
        uint TableID { get; }

        bool Check();
    }
}
