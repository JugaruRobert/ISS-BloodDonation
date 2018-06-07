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
    public class PatientRepository : RepositoryObject
    {
        #region Constructors
        public PatientRepository(RepositoryContext context) : base(context) { }
        #endregion

        #region Methods
        public void Insert(Patient patient)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.Patients_Insert", patient.GenerateSqlParametersFromModel().ToArray());
        }

        public void Update(Patient patient)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.Patients_Update", patient.GenerateSqlParametersFromModel().ToArray());
        }

        public void Delete(string CNP)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.Patients_Remove", new SqlParameter("CNP", CNP));
        }

        public IEnumerable<Patient> ReadAll()
        {
            return DbOperations.ExecuteQuery<Patient>(_context.CONNECTION_STRING, "dbo.Patients_ReadAll");
        }

        public Patient ReadByID(string CNP)
        {
            return DbOperations.ExecuteQuery<Patient>(_context.CONNECTION_STRING, "dbo.Patients_ReadByID", new SqlParameter("CNP", CNP)).FirstOrDefault();
        }

        #endregion
    }
}