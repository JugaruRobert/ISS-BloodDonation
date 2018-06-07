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
    public class HospitalRepository : RepositoryObject
    {
        #region Constructors
        public HospitalRepository(RepositoryContext context) : base(context) { }
        #endregion

        #region Methods
        public void Insert(Hospital hospital)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.Hospitals_Insert", hospital.GenerateSqlParametersFromModel().ToArray());
        }

        public void Update(Hospital hospital)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.Hospitals_Update", hospital.GenerateSqlParametersFromModel().ToArray());
        }

        public void Delete(string HospitalID)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.Hospitals_Remove", new SqlParameter("HospitalID", HospitalID));
        }

        public IEnumerable<Hospital> ReadAll()
        {
            return DbOperations.ExecuteQuery<Hospital>(_context.CONNECTION_STRING, "dbo.Hospitals_ReadAll");
        }

        public Hospital ReadByID(string HospitalID)
        {
            return DbOperations.ExecuteQuery<Hospital>(_context.CONNECTION_STRING, "dbo.Hospitals_ReadByID", new SqlParameter("HospitalID", HospitalID)).FirstOrDefault();
        }

        #endregion
    }
}