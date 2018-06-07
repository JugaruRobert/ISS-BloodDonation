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
    public class BloodBankRepository : RepositoryObject
    {
        #region Constructors
        public BloodBankRepository(RepositoryContext context) : base(context) { }
        #endregion

        #region Methods
        public void Insert(BloodBank bloodBank)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.BloodBanks_Insert", bloodBank.GenerateSqlParametersFromModel().ToArray());
        }

        public void Update(BloodBank bloodBank)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.BloodBanks_Update", bloodBank.GenerateSqlParametersFromModel().ToArray());
        }

        public void Delete(Guid bankID)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.BloodBanks_Remove", new SqlParameter("BankID", bankID));
        }

        public IEnumerable<BloodBank> ReadAll()
        {
            return DbOperations.ExecuteQuery<BloodBank>(_context.CONNECTION_STRING, "dbo.BloodBanks_ReadAll");
        }

        public BloodBank ReadByID(Guid bankID)
        {
            return DbOperations.ExecuteQuery<BloodBank>(_context.CONNECTION_STRING, "dbo.BloodBanks_ReadByID", new SqlParameter("BankID", bankID)).FirstOrDefault();
        }

        #endregion
    }
}
