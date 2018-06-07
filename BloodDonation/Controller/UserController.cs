using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Controller.Core
{
    public class UserController : ControllerObject
    {
        #region Constructors
        public UserController(ControllerContext context) : base(context) { }
        #endregion

        #region Methods
        public void Insert(User address)
        {
            _context.RepositoryContext.UserRepository.Insert(address);
        }

        public IEnumerable<User> ReadAll()
        {
            return _context.RepositoryContext.UserRepository.ReadAll();
        }

        public User ReadByID(string username)
        {
            return _context.RepositoryContext.UserRepository.ReadByID(username);
        }

        public void Update(User address)
        {
            _context.RepositoryContext.UserRepository.Update(address);
        }

        public void Delete(string username)
        {
            _context.RepositoryContext.UserRepository.Delete(username);
        }
        #endregion
    }
}
