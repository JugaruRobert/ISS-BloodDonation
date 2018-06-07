using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodDonation.Controller.Core;

namespace BloodDonation.Controller
{
    public class RequestController : ControllerObject
    {
        #region Constructors
        public RequestController(ControllerContext context) : base(context) { }
        #endregion

        #region Methods
        public void Insert(Request request)
        {
            _context.RepositoryContext.RequestRepository.Insert(request);
        }

        public IEnumerable<Request> ReadAll()
        {
            return _context.RepositoryContext.RequestRepository.ReadAll();
        }

        public Request ReadByID(string patientCNP)
        {
            return _context.RepositoryContext.RequestRepository.ReadByID(patientCNP);
        }

        public void Update(Request request)
        {
            _context.RepositoryContext.RequestRepository.Update(request);
        }

        public void Delete(string patientCNP)
        {
            _context.RepositoryContext.RequestRepository.Delete(patientCNP);
        }
        public IEnumerable<Request> GetAllRequestsByDoctorCNP(string doctorCNP)
        {
           return  _context.RepositoryContext.RequestRepository.GetAllRequestsByDoctorCNP(doctorCNP);
        }
        #endregion
    }
}
