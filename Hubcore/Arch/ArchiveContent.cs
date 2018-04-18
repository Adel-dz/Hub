using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGD.HubCore.Arch
{
    public interface IArchiveContent
    {
        byte[] ArchiveHeader { get; }
        DateTime CreationTime { get; }
        IEnumerable<string> Files { get; }
    }
}
