using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Controller.Core
{
    public class PlasmaController : ControllerObject
    {
        #region Constructors
        public PlasmaController(ControllerContext context) : base(context) { }
        #endregion

        #region Methods
        public void Insert(Plasma Plasma)
        {
            _context.RepositoryContext.PlasmaRepository.Insert(Plasma);
        }

        public IEnumerable<Plasma> ReadAll()
        {
            return _context.RepositoryContext.PlasmaRepository.ReadAll();
        }

        public Plasma ReadByID(Guid PlasmaID)
        {
            return _context.RepositoryContext.PlasmaRepository.ReadByID(PlasmaID);
        }

        public void Update(Plasma Plasma)
        {
            _context.RepositoryContext.PlasmaRepository.Update(Plasma);
        }

        public void Delete(Guid PlasmaID)
        {
            _context.RepositoryContext.PlasmaRepository.Delete(PlasmaID);
        }
        #endregion
    }
}
