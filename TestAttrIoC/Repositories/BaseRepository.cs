using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace TestAttrIoC
{
    public class BaseRepository : IDisposable
    {
        bool _disposed;
        protected DbContext _dbContext;

        public bool DisposeContext
        {
            get;
            set;
        }

        public BaseRepository(DbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            _dbContext = dbContext;

            Trace.TraceInformation(this.GetType().FullName + " #" + this.GetHashCode()+" created ...");
        }

        protected void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (DisposeContext && disposing && _dbContext != null)
            {
                _dbContext.Dispose();
            }
            _disposed = true;
            _dbContext = null;
            Trace.TraceInformation(this.GetType().FullName + " #" + this.GetHashCode() + " disposed ...");
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
