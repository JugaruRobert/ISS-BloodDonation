using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Controller.Core
{
    public class PatientController : ControllerObject
    {
        #region Constructors
        public PatientController(ControllerContext context) : base(context) { }
        #endregion

        #region Methods
        public void Insert(Patient patient)
        {
            _context.RepositoryContext.PatientRepository.Insert(patient);
        }

        public IEnumerable<Patient> ReadAll()
        {
            return _context.RepositoryContext.PatientRepository.ReadAll();
        }

        public Patient ReadByID(string CNP)
        {
            return _context.RepositoryContext.PatientRepository.ReadByID(CNP);
        }

        public void Update(Patient patient)
        {
            _context.RepositoryContext.PatientRepository.Update(patient);
        }

        public void Delete(string CNP)
        {
            _context.RepositoryContext.PatientRepository.Delete(CNP);
        }
        #endregion
    }
}