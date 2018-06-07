using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Controller.Core
{
    public class DiseaseController : ControllerObject
    {
        #region Constructors
        public DiseaseController(ControllerContext context) : base(context) { }
        #endregion

        #region Methods
        public void Insert(Disease disease)
        {
            _context.RepositoryContext.DiseaseRepository.Insert(disease);
        }

        public IEnumerable<Disease> ReadAll()
        {
            return _context.RepositoryContext.DiseaseRepository.ReadAll();
        }

        public Disease ReadByID(Guid diseaseID)
        {
            return _context.RepositoryContext.DiseaseRepository.ReadByID(diseaseID);
        }

        public void Update(Disease disease)
        {
            _context.RepositoryContext.DiseaseRepository.Update(disease);
        }

        public void Delete(Guid diseaseID)
        {
            _context.RepositoryContext.DiseaseRepository.Delete(diseaseID);
        }
        #endregion
    }
}
