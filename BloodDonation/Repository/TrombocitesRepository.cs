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
    public class TrombocitesRepository : RepositoryObject
    {
        #region Constructors
        public TrombocitesRepository(RepositoryContext context) : base(context) { }
        #endregion

        #region Methods
        public void Insert(Trombocite Trombocites)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.Trombocites_Insert", Trombocites.GenerateSqlParametersFromModel().ToArray());
        }

        public void Update(Trombocite Trombocites)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.Trombocites_Update", Trombocites.GenerateSqlParametersFromModel().ToArray());
        }

        public void Delete(Guid TrombocitesID)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.Trombocites_Delete", new SqlParameter("TrombocitesID", TrombocitesID));
        }

        public IEnumerable<Trombocite> ReadAll()
        {
            return DbOperations.ExecuteQuery<Trombocite>(_context.CONNECTION_STRING, "dbo.Trombocites_ReadAll");
        }

        public Trombocite ReadByID(Guid TrombocitesID)
        {
            return DbOperations.ExecuteQuery<Trombocite>(_context.CONNECTION_STRING, "dbo.Trombocites_ReadByID", new SqlParameter("TrombocitesID", TrombocitesID)).FirstOrDefault();
        }

        public void RefreshBloodStock()
        {
            DbOperations.ExecuteQuery<Trombocite>(_context.CONNECTION_STRING, "dbo.RefreshBloodStock");
        }
        #endregion
    }
}