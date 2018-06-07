using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Controller.Core
{
    public class CountryController : ControllerObject
    {
        #region Constructors
        public CountryController(ControllerContext context) : base(context) { }
        #endregion

        #region Methods
        public void Insert(Country country)
        {
            _context.RepositoryContext.CountryRepository.Insert(country);
        }

        public IEnumerable<Country> ReadAll()
        {
            return _context.RepositoryContext.CountryRepository.ReadAll();
        }

        public Country ReadByID(Guid countryID)
        {
            return _context.RepositoryContext.CountryRepository.ReadByID(countryID);
        }

        public void Update(Country country)
        {
            _context.RepositoryContext.CountryRepository.Update(country);
        }

        public void Delete(Guid countryID)
        {
            _context.RepositoryContext.CountryRepository.Delete(countryID);
        }

        public Country GetCountryByName(string Name)
        {
            return _context.RepositoryContext.CountryRepository.GetCountryByName(Name);
        }
        #endregion
    }
}
