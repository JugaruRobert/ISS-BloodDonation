using BloodDonation.Controller.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Controller
{
    public class RedBloodCellController : ControllerObject
    {
        #region Constructors
        public RedBloodCellController(ControllerContext context) : base(context) { }
        #endregion

        #region Methods
        public void Insert(RedBloodCell redBloodCell)
        {
            _context.RepositoryContext.RedBloodCellRepository.Insert(redBloodCell);
        }

        public IEnumerable<RedBloodCell> ReadAll()
        {
            return _context.RepositoryContext.RedBloodCellRepository.ReadAll();
        }

        public RedBloodCell ReadByID(Guid redBloodCellID)
        {
            return _context.RepositoryContext.RedBloodCellRepository.ReadByID(redBloodCellID);
        }

        public void Update(RedBloodCell redBloodCell)
        {
            _context.RepositoryContext.RedBloodCellRepository.Update(redBloodCell);
        }

        public void Delete(Guid redBloodCellID)
        {
            _context.RepositoryContext.RedBloodCellRepository.Delete(redBloodCellID);
        }
        #endregion
    }
}
