using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGD.HubCore.DB
{
    public interface ITablesCollection: IEnumerable<IDBTable>
    {
        IDBTable this[uint tableID] { get; }
    }
}
