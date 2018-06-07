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
    public class DonorRepository : RepositoryObject
    {
        #region Constructors
        public DonorRepository(RepositoryContext context) : base(context) { }
        #endregion

        #region Methods
        public void Insert(Donor donor)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.Donors_Insert", donor.GenerateSqlParametersFromModel().ToArray());
        }

        public void Update(Donor donor)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.Donors_Update", donor.GenerateSqlParametersFromModel().ToArray());
        }

        public void Delete(string CNP)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.Donors_Remove", new SqlParameter("CNP", CNP));
        }

        public IEnumerable<Donor> ReadAll()
        {
            return DbOperations.ExecuteQuery<Donor>(_context.CONNECTION_STRING, "dbo.Donors_ReadAll");
        }

        public Donor ReadByID(string CNP)
        {
            return DbOperations.ExecuteQuery<Donor>(_context.CONNECTION_STRING, "dbo.Donors_ReadByID", new SqlParameter("CNP", CNP)).FirstOrDefault();
        }

        //public int GetCanDonate(string CNP)
        //{
        //    return DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.GetCanDonate", new SqlParameter("CNP", CNP));
        //}

        #endregion
    }
}
