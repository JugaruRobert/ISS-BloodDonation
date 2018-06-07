using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Controller.Core
{
    public class TrombocitesController : ControllerObject
    {
        #region Constructors
        public TrombocitesController(ControllerContext context) : base(context) { }
        #endregion

        #region Methods
        public void Insert(Trombocite Trombocites)
        {
            _context.RepositoryContext.TrombocitesRepository.Insert(Trombocites);
        }

        public IEnumerable<Trombocite> ReadAll()
        {
            return _context.RepositoryContext.TrombocitesRepository.ReadAll();
        }

        public Trombocite ReadByID(Guid TrombocitesID)
        {
            return _context.RepositoryContext.TrombocitesRepository.ReadByID(TrombocitesID);
        }

        public void Update(Trombocite Trombocites)
        {
            _context.RepositoryContext.TrombocitesRepository.Update(Trombocites);
        }

        public void Delete(Guid TrombocitesID)
        {
            _context.RepositoryContext.TrombocitesRepository.Delete(TrombocitesID);
        }

        public void RefreshBloodStock()
        {
            _context.RepositoryContext.TrombocitesRepository.RefreshBloodStock();
        }
        #endregion
    }
}
