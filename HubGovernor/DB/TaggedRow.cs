using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGD.HubGovernor.DB
{
    interface ITaggedRow
    {
        bool Disabled { get; set; }
    }
}
