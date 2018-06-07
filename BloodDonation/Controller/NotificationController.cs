using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BloodDonation.Controller.Core
{
    public class NotificationController : ControllerObject
    {
        #region Constructors
        public NotificationController(ControllerContext context) : base(context) { }
        #endregion

        #region Methods
        public void Insert(Notification notification)
        {
            _context.RepositoryContext.NotificationRepository.Insert(notification);
        }

        public IEnumerable<Notification> ReadAll()
        {
            return _context.RepositoryContext.NotificationRepository.ReadAll();
        }

        public Notification ReadByID(Guid notificationID)
        {
            return _context.RepositoryContext.NotificationRepository.ReadByID(notificationID);
        }

        public void Update(Notification notification)
        {
            _context.RepositoryContext.NotificationRepository.Update(notification);
        }

        public void Delete(Guid notificationID)
        {
            _context.RepositoryContext.NotificationRepository.Delete(notificationID);
        }
        #endregion
    }
}
