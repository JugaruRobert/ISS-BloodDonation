using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Controller.Core
{
    public class DiseaseResultController : ControllerObject
    {
        #region Constructors
        public DiseaseResultController(ControllerContext context) : base(context) { }
        #endregion

        #region Methods
        public void Insert(DiseasesResult diseasesResult)
        {
            _context.RepositoryContext.DiseaseResultRepository.Insert(diseasesResult);
        }

        public IEnumerable<DiseasesResult> ReadAll()
        {
            return _context.RepositoryContext.DiseaseResultRepository.ReadAll();
        }

        public DiseasesResult ReadByID(Guid diseaseID, Guid donationID)
        {
            return _context.RepositoryContext.DiseaseResultRepository.ReadByID(diseaseID, donationID);
        }

        public void Update(DiseasesResult disease)
        {
            _context.RepositoryContext.DiseaseResultRepository.Update(disease);
        }

        public void Delete(Guid diseaseID, Guid donationID)
        {
            _context.RepositoryContext.DiseaseResultRepository.Delete(diseaseID, donationID);
        }
        #endregion
    }


}
