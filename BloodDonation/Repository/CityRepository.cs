using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodDonation.Library;
using BloodDonation.Repository.Core;

namespace BloodDonation.Repository
{
    public class CityRepository : RepositoryObject
    {
        #region Constructors
        public CityRepository(RepositoryContext context) : base(context) { }
        #endregion

        #region Methods
        public void Insert(City city) 
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.Cities_Insert", city.GenerateSqlParametersFromModel().ToArray());
        }

        public void Update(City city)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.Cities_Update", city.GenerateSqlParametersFromModel().ToArray());
        }

        public void Delete(Guid cityID)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.Cities_Delete", new SqlParameter("CityID", cityID));
        }

        public IEnumerable<City> ReadAll()
        {
            return DbOperations.ExecuteQuery<City>(_context.CONNECTION_STRING, "dbo.Cities_ReadAll");
        }

        public City ReadByID(Guid cityID)
        {
            
            return DbOperations.ExecuteQuery<City>(_context.CONNECTION_STRING, "dbo.Cities_ReadByID", new SqlParameter("CityID", cityID)).FirstOrDefault();
        }

        public City GetCityByName(string Name, Guid countryID)
        {
            return DbOperations.ExecuteQuery<City>(_context.CONNECTION_STRING, "dbo.GetCityByName", new SqlParameter("Name", Name), new SqlParameter("CountryID", countryID)).FirstOrDefault();
        }

        public IEnumerable<City> GetCitiesByCountry(Guid countryID)
        {
            return DbOperations.ExecuteQuery<City>(_context.CONNECTION_STRING, "dbo.GetCitiesByCountry", new SqlParameter("CountryID", countryID));
        }
        #endregion
    }
}