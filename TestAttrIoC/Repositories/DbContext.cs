using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAttrIoC
{
    public class DbContext:IDisposable
    {
        public DbContext()
        {

        }

        public void Dispose()
        {
        }
    }
}