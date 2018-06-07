using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Controller.Core
{
    public class AddressController : ControllerObject
    {
        #region Constructors
        public AddressController(ControllerContext context) : base(context) { }
        #endregion

        #region Methods
        public void Insert(Address address)
        {
            _context.RepositoryContext.AddressRepository.Insert(address);
        }

        public IEnumerable<Address> ReadAll()
        {
            return _context.RepositoryContext.AddressRepository.ReadAll();
        }

        public Address ReadByID(Guid addressID)
        {
            return _context.RepositoryContext.AddressRepository.ReadByID(addressID);
        }

        public void Update(Address address)
        {
            _context.RepositoryContext.AddressRepository.Update(address);
        }

        public void Delete(Guid addressID)
        {
            _context.RepositoryContext.AddressRepository.Delete(addressID);
        }
        #endregion
    }
}
