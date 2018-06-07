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
    public class RequestRepository:RepositoryObject
    {
        #region Constructors
        public RequestRepository(RepositoryContext context) : base(context) { }
        #endregion

        #region Methods
        public void Insert(Request request)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.Requests_Insert", request.GenerateSqlParametersFromModel().ToArray());
        }

        public void Update(Request request)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.Requests_Update", request.GenerateSqlParametersFromModel().ToArray());
        }

        public void Delete(string patientCNP)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.Requests_Remove", new SqlParameter("PatientCNP", patientCNP));
        }

        public IEnumerable<Request> ReadAll()
        {
            return DbOperations.ExecuteQuery<Request>(_context.CONNECTION_STRING, "dbo.Requests_ReadAll");
        }

        public Request ReadByID(string patientCNP)
        {
            return DbOperations.ExecuteQuery<Request>(_context.CONNECTION_STRING, "dbo.Requests_ReadByID", new SqlParameter("PatientCNP", patientCNP)).FirstOrDefault();
        }

        public IEnumerable<Request> GetAllRequestsByDoctorCNP(string doctorCNP)
        {
            return DbOperations.ExecuteQuery<Request>(_context.CONNECTION_STRING, "dbo.GetAllRequestsByDoctorCNP", new SqlParameter("DoctorCNP", doctorCNP));
        }

        #endregion
    }
}
