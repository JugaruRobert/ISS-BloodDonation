using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Repository.Core
{
    public class RepositoryObject : IDisposable
    {
        #region Members
        protected RepositoryContext _context;
        #endregion

        #region Constructors
        public RepositoryObject(RepositoryContext context)
        {
            _context = context;
        }
        #endregion

        #region IDisposable Implementation
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }
            if (_context != null)
            {
                _context = null;
            }
        }

        ~RepositoryObject()
        {
            Dispose(false);
        }
        #endregion
    }
}
