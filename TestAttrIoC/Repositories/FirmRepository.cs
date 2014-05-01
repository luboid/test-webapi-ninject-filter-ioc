using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAttrIoC
{
    public class FirmRepository : BaseRepository, IFirmRepository
    {
        public FirmRepository(DbContext dbContext) :
            base(dbContext)
        { }

        public bool Exists(string userId, string id)
        {
            ThrowIfDisposed();
            //do something...
            return true;
        }
    }
}
