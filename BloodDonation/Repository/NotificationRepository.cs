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
    public class NotificationRepository : RepositoryObject
    {
        #region Constructors
        public NotificationRepository(RepositoryContext context) : base(context) { }
        #endregion

        #region Methods
        public void Insert(Notification notification)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.Notifications_Insert", notification.GenerateSqlParametersFromModel().ToArray());
        }

        public void Update(Notification notification)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.Notifications_Update", notification.GenerateSqlParametersFromModel().ToArray());
        }

        public void Delete(Guid notificationID)
        {
            DbOperations.ExecuteCommand(_context.CONNECTION_STRING, "dbo.Notifications_Remove", new SqlParameter("NotificationID", notificationID));
        }

        public IEnumerable<Notification> ReadAll()
        {
            return DbOperations.ExecuteQuery<Notification>(_context.CONNECTION_STRING, "dbo.Notifications_ReadAll");
        }

        public Notification ReadByID(Guid notificationID)
        {
            return DbOperations.ExecuteQuery<Notification>(_context.CONNECTION_STRING, "dbo.Notifications_ReadByID", new SqlParameter("NotificationID", notificationID)).FirstOrDefault();
        }

        #endregion
    }
}
