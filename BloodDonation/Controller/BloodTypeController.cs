using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Controller.Core
{
    public class BloodTypeController : ControllerObject
    {
        #region Constructors
        public BloodTypeController(ControllerContext context) : base(context) { }
        #endregion

        #region Methods
        public void Insert(BloodType bloodType)
        {
            _context.RepositoryContext.BloodTypeRepository.Insert(bloodType);
        }

        public IEnumerable<BloodType> ReadAll()
        {
            return _context.RepositoryContext.BloodTypeRepository.ReadAll();
        }

        public BloodType ReadByID(Guid bloodTypeID)
        {
            return _context.RepositoryContext.BloodTypeRepository.ReadByID(bloodTypeID);
        }

        public void Update(BloodType bloodType)
        {
            _context.RepositoryContext.BloodTypeRepository.Update(bloodType);
        }

        public void Delete(Guid bloodTypeID)
        {
            _context.RepositoryContext.BloodTypeRepository.Delete(bloodTypeID);
        }
        #endregion
    }
}
