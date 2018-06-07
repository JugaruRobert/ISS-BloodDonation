using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Controller.Core
{
    public class BloodDonationHistoryController : ControllerObject
    {
        #region Constructors
        public BloodDonationHistoryController(ControllerContext context) : base(context) { }
        #endregion

        #region Methods
        public void Insert(BloodDonationHistory bloodDonationHistory)
        {
            _context.RepositoryContext.BloodDonationHistoryRepository.Insert(bloodDonationHistory);
        }

        public void InsertEmpty(BloodDonationHistory bloodDonationHistory)
        {
            _context.RepositoryContext.BloodDonationHistoryRepository.InsertEmpty(bloodDonationHistory);
        }

        public void InsertEmptySpecific(BloodDonationHistory bloodDonationHistory)
        {
            _context.RepositoryContext.BloodDonationHistoryRepository.InsertEmptySpecific(bloodDonationHistory);
        }

        public IEnumerable<BloodDonationHistory> ReadAll()
        {
            return _context.RepositoryContext.BloodDonationHistoryRepository.ReadAll();
        }

        public BloodDonationHistory ReadByID(Guid donationID)
        {
            return _context.RepositoryContext.BloodDonationHistoryRepository.ReadByID(donationID);
        }

        public void Update(BloodDonationHistory bloodDonationHistory)
        {
            _context.RepositoryContext.BloodDonationHistoryRepository.Update(bloodDonationHistory);
        }

        public void Delete(Guid donationID)
        {
            _context.RepositoryContext.BloodDonationHistoryRepository.Delete(donationID);
        }

        public IEnumerable<BloodDonationHistory> GetBloodHistoryForDonor(string CNP)
        {
            return _context.RepositoryContext.BloodDonationHistoryRepository.GetBloodHistoryForDonor(CNP);
        }

        public BloodDonationHistory GetHasDeseases(string CNP)
        {
            return _context.RepositoryContext.BloodDonationHistoryRepository.GetHasDiseases(CNP);
        }

        public BloodDonationHistory GetLatestDonation(string CNP)
        {
            return _context.RepositoryContext.BloodDonationHistoryRepository.GetLatestDonation(CNP);
        }
        #endregion
    }
}
