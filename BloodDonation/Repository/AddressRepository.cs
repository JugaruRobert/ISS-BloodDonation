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
    public class AddressRepository : RepositoryObject
    {
        #region Constructors
        public AddressRepository(RepositoryContext context) : base(context) { }
        #endregion

        #region Methods
        public void Insert(Address address)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.Addresses_Insert", address.GenerateSqlParametersFromModel().ToArray());
        }

        public void Update(Address address)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.Addresses_Update", address.GenerateSqlParametersFromModel().ToArray());
        }

        public void Delete(Guid addressID)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.Addresses_Remove", new SqlParameter("AddressID", addressID));
        }

        public IEnumerable<Address> ReadAll()
        {
            return DbOperations.ExecuteQuery<Address>(_context.CONNECTION_STRING, "dbo.Addresses_ReadAll");
        }

        public Address ReadByID(Guid addressID)
        {
            return DbOperations.ExecuteQuery<Address>(_context.CONNECTION_STRING, "dbo.Addresses_ReadByID", new SqlParameter("AddressID", addressID)).FirstOrDefault();
        }

        #endregion
    }
}
