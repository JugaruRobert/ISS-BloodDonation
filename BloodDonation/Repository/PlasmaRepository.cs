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
    public class PlasmaRepository : RepositoryObject
    {
        #region Constructors
        public PlasmaRepository(RepositoryContext context) : base(context) { }
        #endregion

        #region Methods
        public void Insert(Plasma Plasma)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.Plasma_Insert", Plasma.GenerateSqlParametersFromModel().ToArray());
        }

        public void Update(Plasma Plasma)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.Plasma_Update", Plasma.GenerateSqlParametersFromModel().ToArray());
        }

        public void Delete(Guid PlasmaID)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.Plasma_Delete", new SqlParameter("PlasmaID", PlasmaID));
        }

        public IEnumerable<Plasma> ReadAll()
        {
            return DbOperations.ExecuteQuery<Plasma>(_context.CONNECTION_STRING, "dbo.Plasma_ReadAll");
        }

        public Plasma ReadByID(Guid PlasmaID)
        {
            return DbOperations.ExecuteQuery<Plasma>(_context.CONNECTION_STRING, "dbo.Plasma_ReadByID", new SqlParameter("PlasmaID", PlasmaID)).FirstOrDefault();
        }
        #endregion
    }
}