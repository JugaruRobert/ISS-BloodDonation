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
    public class BloodTypeRepository : RepositoryObject
    {
        #region Constructors
        public BloodTypeRepository(RepositoryContext context) : base(context) { }
        #endregion

        #region Methods
        public void Insert(BloodType bloodType)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.BloodTypes_Insert", bloodType.GenerateSqlParametersFromModel().ToArray());
        }

        public void Update(BloodType bloodType)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.BloodTypes_Update", bloodType.GenerateSqlParametersFromModel().ToArray());
        }

        public void Delete(Guid bloodTypeID)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.BloodTypes_Remove", new SqlParameter("BloodTypeID", bloodTypeID));
        }

        public IEnumerable<BloodType> ReadAll()
        {
            return DbOperations.ExecuteQuery<BloodType>(_context.CONNECTION_STRING, "dbo.BloodTypes_ReadAll");
        }

        public BloodType ReadByID(Guid bloodTypeID)
        {
            return DbOperations.ExecuteQuery<BloodType>(_context.CONNECTION_STRING, "dbo.BloodTypes_ReadByID", new SqlParameter("BloodTypeID", bloodTypeID)).FirstOrDefault();
        }

        #endregion
    }
}
