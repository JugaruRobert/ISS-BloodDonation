using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Controller.Core
{
    public class BloodBankController : ControllerObject
    {
        #region Constructors
        public BloodBankController(ControllerContext context) : base(context) { }
        #endregion

        #region Methods
        public void Insert(BloodBank bloodBank)
        {
            _context.RepositoryContext.BloodBankRepository.Insert(bloodBank);
        }

        public IEnumerable<BloodBank> ReadAll()
        {
            return _context.RepositoryContext.BloodBankRepository.ReadAll();
        }

        public BloodBank ReadByID(Guid bankID)
        {
            return _context.RepositoryContext.BloodBankRepository.ReadByID(bankID);
        }

        public void Update(BloodBank bloodBank)
        {
            _context.RepositoryContext.BloodBankRepository.Update(bloodBank);
        }

        public void Delete(Guid bankID)
        {
            _context.RepositoryContext.BloodBankRepository.Delete(bankID);
        }
        #endregion
    }
}
