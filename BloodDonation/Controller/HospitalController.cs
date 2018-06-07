using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Controller.Core
{
    public class HospitalController : ControllerObject
    {
        #region Constructors
        public HospitalController(ControllerContext context) : base(context) { }
        #endregion

        #region Methods
        public void Insert(Hospital hospital)
        {
            _context.RepositoryContext.HospitalRepository.Insert(hospital);
        }

        public IEnumerable<Hospital> ReadAll()
        {
            return _context.RepositoryContext.HospitalRepository.ReadAll();
        }

        public Hospital ReadByID(string HospitalID)
        {
            return _context.RepositoryContext.HospitalRepository.ReadByID(HospitalID);
        }

        public void Update(Hospital hospital)
        {
            _context.RepositoryContext.HospitalRepository.Update(hospital);
        }

        public void Delete(string HospitalID)
        {
            _context.RepositoryContext.HospitalRepository.Delete(HospitalID);
        }
        #endregion
    }
}