using BloodDonation.Library;
using BloodDonation.Repository.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Repository
{
    public class RedBloodCellRepository : RepositoryObject
    {
        #region Constructors
        public RedBloodCellRepository(RepositoryContext context) : base(context) { }
        #endregion

        #region Methods
        public void Insert(RedBloodCell redBloodCell)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.RedBloodCells_Insert", redBloodCell.GenerateSqlParametersFromModel().ToArray());
        }

        public void Update(RedBloodCell redBloodCell)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.RedBloodCells_Update", redBloodCell.GenerateSqlParametersFromModel().ToArray());
        }

        public void Delete(Guid redBloodCellsID)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.RedBloodCells_Remove", new SqlParameter("RedBloodCellsID", redBloodCellsID));
        }

        public IEnumerable<RedBloodCell> ReadAll()
        {
            return DbOperations.ExecuteQuery<RedBloodCell>(_context.CONNECTION_STRING, "dbo.RedBloodCells_ReadAll");
        }

        public RedBloodCell ReadByID(Guid redBloodCellsID)
        {
            return DbOperations.ExecuteQuery<RedBloodCell>(_context.CONNECTION_STRING, "dbo.RedBloodCells_ReadByID", new SqlParameter("RedBloodCellsID", redBloodCellsID)).FirstOrDefault();
        }
        #endregion
    }
}
