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
    public class DiseaseRepository : RepositoryObject
    {
        #region Constructors
        public DiseaseRepository(RepositoryContext context) : base(context) { }
        #endregion

        #region Methods
        public void Insert(Disease disease)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.Diseases_Insert", disease.GenerateSqlParametersFromModel().ToArray());
        }

        public void Update(Disease disease)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.Diseases_Update", disease.GenerateSqlParametersFromModel().ToArray());
        }

        public void Delete(Guid diseaseID)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.Diseases_Remove", new SqlParameter("DiseaseID", diseaseID));
        }

        public IEnumerable<Disease> ReadAll()
        {
            return DbOperations.ExecuteQuery<Disease>(_context.CONNECTION_STRING, "dbo.Diseases_ReadAll");
        }

        public Disease ReadByID(Guid diseaseID)
        {
            return DbOperations.ExecuteQuery<Disease>(_context.CONNECTION_STRING, "dbo.Diseases_ReadByID", new SqlParameter("DiseaseID", diseaseID)).FirstOrDefault();
        }
        #endregion
    }
    
}
