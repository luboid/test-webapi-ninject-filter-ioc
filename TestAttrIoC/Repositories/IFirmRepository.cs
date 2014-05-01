using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAttrIoC
{
    public interface IFirmRepository : IDisposable
    {
        bool Exists(string userId, string id);
    }
}