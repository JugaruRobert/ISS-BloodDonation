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
    public class UserRepository : RepositoryObject
    {
        #region Constructors
        public UserRepository(RepositoryContext context) : base(context) { }
        #endregion

        #region Methods
        public void Insert(User user)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.Users_Insert", user.GenerateSqlParametersFromModel().ToArray());
        }

        public void Update(User user)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.Users_Update", user.GenerateSqlParametersFromModel().ToArray());
        }

        public void Delete(string username)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.Users_Remove", new SqlParameter("Username", username));
        }

        public IEnumerable<User> ReadAll()
        {
            return DbOperations.ExecuteQuery<User>(_context.CONNECTION_STRING, "dbo.Users_ReadAll");
        }

        public User ReadByID(string username)
        {
            return DbOperations.ExecuteQuery<User>(_context.CONNECTION_STRING, "dbo.Users_ReadByID", new SqlParameter("Username", username)).FirstOrDefault();
        }

        #endregion
    }
}
