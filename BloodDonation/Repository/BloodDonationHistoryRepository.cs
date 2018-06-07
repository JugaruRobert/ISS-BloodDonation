using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using BloodDonation.Library;
using BloodDonation.Repository.Core;

namespace BloodDonation.Repository
{
    public class BloodDonationHistoryRepository : RepositoryObject
    {
        #region Constructors
        public BloodDonationHistoryRepository(RepositoryContext context) : base(context) { }
        #endregion

        #region Methods
        public void Insert(BloodDonationHistory bloodDonationHistory)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.BloodDonationHistories_Insert", bloodDonationHistory.GenerateSqlParametersFromModel().ToArray());
        }

        public void InsertEmpty(BloodDonationHistory bloodDonationHistory)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.BloodDonationHistories_InsertEmpty", bloodDonationHistory.GenerateSqlParametersFromModel().ToArray());
        }

        public void InsertEmptySpecific(BloodDonationHistory bloodDonationHistory)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.BloodDonationHistories_InsertEmptySpecific", bloodDonationHistory.GenerateSqlParametersFromModel().ToArray());
        }

        public void Update(BloodDonationHistory bloodDonationHistory)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.BloodDonationHistories_Update", bloodDonationHistory.GenerateSqlParametersFromModel().ToArray());
        }

        public void Delete(Guid donationID)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.BloodDonationHistories_Remove", new SqlParameter("DonationID", donationID));
        }

        public IEnumerable<BloodDonationHistory> ReadAll()
        {
            return DbOperations.ExecuteQuery<BloodDonationHistory>(_context.CONNECTION_STRING, "dbo.BloodDonationHistories_ReadAll");
        }

        public BloodDonationHistory ReadByID(Guid donationID)
        {
            return DbOperations.ExecuteQuery<BloodDonationHistory>(_context.CONNECTION_STRING, "dbo.BloodDonationHistories_ReadByID", new SqlParameter("DonationID", donationID)).FirstOrDefault();
        }

        public IEnumerable<BloodDonationHistory> GetBloodHistoryForDonor(string CNP)
        {
            return DbOperations.ExecuteQuery<BloodDonationHistory>(_context.CONNECTION_STRING, "dbo.GetBloodHistoryForDonor", new SqlParameter("CNP", CNP));
        }

        public BloodDonationHistory GetHasDiseases(string CNP)
        {
            return DbOperations.ExecuteQuery<BloodDonationHistory>(_context.CONNECTION_STRING, "dbo.GetHasDiseases", new SqlParameter("@DonorCNP", CNP)).FirstOrDefault(); ;
        }

        public BloodDonationHistory GetLatestDonation(string CNP)
        {
            return DbOperations.ExecuteQuery<BloodDonationHistory>(_context.CONNECTION_STRING, "dbo.GetLatestDonation", new SqlParameter("@DonorCNP", CNP)).FirstOrDefault(); ;
        }
        #endregion
    }
}
