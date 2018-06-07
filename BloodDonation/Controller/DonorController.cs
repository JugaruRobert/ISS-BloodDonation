using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Controller.Core
{
    public class DonorController : ControllerObject
    {
        #region Constructors
        public DonorController(ControllerContext context) : base(context) { }
        #endregion

        #region Methods
        public void Insert(Donor donor)
        {
            _context.RepositoryContext.DonorRepository.Insert(donor);
        }

        public IEnumerable<Donor> ReadAll()
        {
            return _context.RepositoryContext.DonorRepository.ReadAll();
        }

        public Donor ReadByID(string CNP)
        {
            return _context.RepositoryContext.DonorRepository.ReadByID(CNP);
        }

        public void Update(Donor donor)
        {
            _context.RepositoryContext.DonorRepository.Update(donor);
        }

        public void Delete(string CNP)
        {
            _context.RepositoryContext.DonorRepository.Delete(CNP);
        }

        //public int getCanDonate(string CNP)
        //{
        //    return _context.RepositoryContext.DonorRepository.GetCanDonate(CNP);
        //}
        #endregion
    }
}
