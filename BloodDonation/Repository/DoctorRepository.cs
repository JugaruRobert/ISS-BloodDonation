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
    public class DoctorRepository:RepositoryObject
    {
        #region Constructors
        public DoctorRepository(RepositoryContext context) : base(context) { }
        #endregion

        #region Methods
        public void Insert(Doctor doctor)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.Doctors_Insert", doctor.GenerateSqlParametersFromModel().ToArray());
        }

        public void Update(Doctor doctor)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.Doctors_Update", doctor.GenerateSqlParametersFromModel().ToArray());
        }

        public void Delete(string CNP)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.Doctors_Remove", new SqlParameter("DoctorCNP", CNP));
        }

        public IEnumerable<Doctor> ReadAll()
        {
            return DbOperations.ExecuteQuery<Doctor>(_context.CONNECTION_STRING, "dbo.Doctors_ReadAll");
        }

        public Doctor ReadByID(string doctorCNP)
        {
            return DbOperations.ExecuteQuery<Doctor>(_context.CONNECTION_STRING, "dbo.Doctors_ReadByID", new SqlParameter("DoctorCNP", doctorCNP)).FirstOrDefault();
        }

        #endregion
    }
}
