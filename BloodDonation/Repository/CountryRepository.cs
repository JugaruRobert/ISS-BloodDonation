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
    public class CountryRepository : RepositoryObject
    {
        #region Constructors
        public CountryRepository(RepositoryContext context) : base(context) { }
        #endregion

        #region Methods
        public void Insert(Country country)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.Countries_Insert", country.GenerateSqlParametersFromModel().ToArray());
        }

        public void Update(Country country)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.Countries_Update", country.GenerateSqlParametersFromModel().ToArray());
        }

        public void Delete(Guid countryID)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.Countries_Delete", new SqlParameter("CountryID", countryID));
        }

        public IEnumerable<Country> ReadAll()
        {
            return DbOperations.ExecuteQuery<Country>(_context.CONNECTION_STRING, "dbo.Countries_ReadAll");
        }

        public Country ReadByID(Guid countryID)
        {
            return DbOperations.ExecuteQuery<Country>(_context.CONNECTION_STRING, "dbo.Countries_ReadByID", new SqlParameter("CountryID", countryID)).FirstOrDefault();
        }

        public Country GetCountryByName(string Name)
        {
            return DbOperations.ExecuteQuery<Country>(_context.CONNECTION_STRING, "dbo.GetCountryByName", new SqlParameter("Name", Name)).FirstOrDefault();
        }
        #endregion
    }
}