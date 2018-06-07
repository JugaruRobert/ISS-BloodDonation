using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Controller.Core
{
    public class ControllerObject : IDisposable
    {
        #region Members
        protected ControllerContext _context;
        #endregion

        #region Constructor
        public ControllerObject(ControllerContext context)
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
                return;

            if (_context != null)
            {
                _context = null;
            }
        }

        ~ControllerObject()
        {
            Dispose(false);
        }
        #endregion
    }
}
