using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BloodDonation.Controller.Core
{
    public class DoctorController: ControllerObject
    {
        #region Constructors
        public DoctorController(ControllerContext context) : base(context) { }
        #endregion

        #region Methods
        public void Insert(Doctor doctor)
        {
            _context.RepositoryContext.DoctorRepository.Insert(doctor);
        }

        public IEnumerable<Doctor> ReadAll()
        {
            return _context.RepositoryContext.DoctorRepository.ReadAll();
        }

        public Doctor ReadByID(string CNP)
        {
            return _context.RepositoryContext.DoctorRepository.ReadByID(CNP);
        }

        public void Update(Doctor doctor)
        {
            _context.RepositoryContext.DoctorRepository.Update(doctor);
        }

        public void Delete(string CNP)
        {
            _context.RepositoryContext.DoctorRepository.Delete(CNP);
        }
        #endregion
    }
}

