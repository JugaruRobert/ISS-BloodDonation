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
    public class DiseaseResultRepository : RepositoryObject
    {
        #region Constructors
        public DiseaseResultRepository(RepositoryContext context) : base(context) { }
        #endregion

        #region Methods
        public void Insert(DiseasesResult diseasesResult)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.DiseasesResults_Insert", diseasesResult.GenerateSqlParametersFromModel().ToArray());
        }

        public void Update(DiseasesResult diseasesResult)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.DiseasesResults_Update", diseasesResult.GenerateSqlParametersFromModel().ToArray());
        }

        public void Delete(Guid diseaseID, Guid donationD)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.DiseasesResults_Remove", new SqlParameter("DiseaseID", diseaseID), new SqlParameter("DonationD", donationD));
        }

        public IEnumerable<DiseasesResult> ReadAll()
        {
            return DbOperations.ExecuteQuery<DiseasesResult>(_context.CONNECTION_STRING, "dbo.DiseasesResults_ReadAll");
        }

        public DiseasesResult ReadByID(Guid diseaseID, Guid donationD)
        {
            return DbOperations.ExecuteQuery<DiseasesResult>(_context.CONNECTION_STRING, "dbo.DiseasesResults_ReadByID", new SqlParameter("DiseaseID", diseaseID), new SqlParameter("DonationD", donationD)).FirstOrDefault();
        }
        #endregion
    }
}
