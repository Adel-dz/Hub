using System;
using System.Collections.Generic;

namespace DGD.HubCore.DB
{
    public interface IDatumFactory
    {
        IDataRow CreateDatum(uint tableID);
    }
    
}
