using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Controller.Core
{
    public class CityController : ControllerObject
    {
        #region Constructors
        public CityController(ControllerContext context) : base(context) { }
        #endregion

        #region Methods
        public void Insert(City city)
        {
            _context.RepositoryContext.CityRepository.Insert(city);
        }

        public IEnumerable<City> ReadAll()
        {
            return _context.RepositoryContext.CityRepository.ReadAll();
        }

        public City ReadByID(Guid cityID)
        {
            return _context.RepositoryContext.CityRepository.ReadByID(cityID);
        }

        public void Update(City city)
        {
            _context.RepositoryContext.CityRepository.Update(city);
        }

        public void Delete(Guid cityID)
        {
            _context.RepositoryContext.CityRepository.Delete(cityID);
        }

        public City GetCityByName(string Name, Guid countryID)
        {
            return _context.RepositoryContext.CityRepository.GetCityByName(Name, countryID);
        }

        public IEnumerable<City> GetCitiesByCountry(Guid countryID)
        {
            return _context.RepositoryContext.CityRepository.GetCitiesByCountry(countryID);
        }
        #endregion
    }
}
