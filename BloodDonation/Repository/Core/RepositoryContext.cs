using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Repository.Core
{
    public class RepositoryContext : IDisposable
    {
        #region Constants
        internal string CONNECTION_STRING = "Data Source=MADALINA\\SQLEXPRESS01;Initial Catalog=BloodDonation; Integrated Security=True";
        #endregion

        #region Members
        private BloodBankRepository bloodBankRepository;
        private BloodDonationHistoryRepository bloodDonationHistoryRepository;
        private DiseaseRepository diseaseRepository;
        private AddressRepository addressRepository;
        private DonorRepository donorRepository;
        private RedBloodCellRepository redBloodCellRepository;
        private DiseaseResultRepository diseaseResultRepository;
        private CityRepository cityRepository;
        private CountryRepository countryRepository;
        private DoctorRepository doctorRepository;
        private RequestRepository requestRepository;
        private PlasmaRepository plasmaRepository;
        private TrombocitesRepository trombocitesRepository;
        private UserRepository userRepository;
        private PatientRepository patientRepository;
        private HospitalRepository hospitalRepository;
        private NotificationRepository notificationRepository;
        private BloodTypeRepository bloodTypeRepository;
        #endregion

        #region Properties
        public BloodBankRepository BloodBankRepository
        {
            get
            {
                if (bloodBankRepository == null)
                {
                    bloodBankRepository = new BloodBankRepository(this);
                }
                return bloodBankRepository;
            }
        }

        public BloodDonationHistoryRepository BloodDonationHistoryRepository
        {
            get
            {
                if (bloodDonationHistoryRepository == null)
                {
                    bloodDonationHistoryRepository = new BloodDonationHistoryRepository(this);
                }
                return bloodDonationHistoryRepository;
            }
        }
        public DiseaseRepository DiseaseRepository
        {
            get
            {
                if (diseaseRepository == null)
                {
                    diseaseRepository = new DiseaseRepository(this);
                }
                return diseaseRepository;
            }
        }

        public AddressRepository AddressRepository
        {
            get
            {
                if (addressRepository == null)
                {
                    addressRepository = new AddressRepository(this);
                }
                return addressRepository;
            }
        }

        public DonorRepository DonorRepository
        {
            get
            {
                if (donorRepository == null)
                {
                    donorRepository = new DonorRepository(this);
                }
                return donorRepository;
            }
        }

        public RedBloodCellRepository RedBloodCellRepository
        {
            get
            {
                if (redBloodCellRepository == null)
                {
                    redBloodCellRepository = new RedBloodCellRepository(this);
                }
                return redBloodCellRepository;
            }
        }

        public DiseaseResultRepository DiseaseResultRepository
        {
            get
            {
                if (diseaseResultRepository == null)
                {
                    diseaseResultRepository = new DiseaseResultRepository(this);
                }
                return diseaseResultRepository;
            }
        }

        public CityRepository CityRepository
        {
            get
            {
                if (cityRepository == null)
                {
                    cityRepository = new CityRepository(this);
                }
                return cityRepository;
            }
        }

        public CountryRepository CountryRepository
        {
            get
            {
                if (countryRepository == null)
                {
                    countryRepository = new CountryRepository(this);
                }
                return countryRepository;
            }
        }

        public PlasmaRepository PlasmaRepository
        {
            get
            {
                if (plasmaRepository == null)
                {
                    plasmaRepository = new PlasmaRepository(this);
                }
                return plasmaRepository;
            }
        }

        public TrombocitesRepository TrombocitesRepository
        {
            get
            {
                if (trombocitesRepository == null)
                {
                    trombocitesRepository = new TrombocitesRepository(this);
                }
                return trombocitesRepository;
            }
        }

        public UserRepository UserRepository
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(this);
                }
                return userRepository;
            }
        }

        public DoctorRepository DoctorRepository
        {
            get
            {
                if (doctorRepository == null)
                {
                    doctorRepository = new DoctorRepository(this);
                }
                return doctorRepository;
            }
        }

        public RequestRepository RequestRepository
        {
            get
            {
                if (requestRepository == null)
                {
                    requestRepository = new RequestRepository(this);
                }
                return requestRepository;
            }
        }

        public PatientRepository PatientRepository
        {
            get
            {
                if (patientRepository == null)
                {
                    patientRepository = new PatientRepository(this);
                }
                return patientRepository;
            }
        }

        public HospitalRepository HospitalRepository
        {
            get
            {
                if (hospitalRepository == null)
                {
                    hospitalRepository = new HospitalRepository(this);
                }

                return hospitalRepository;
            }

        }

        public NotificationRepository NotificationRepository
        {
            get
            {
                if (notificationRepository == null)
                {
                    notificationRepository = new NotificationRepository(this);
                }
                return notificationRepository;
            }
        }

        public BloodTypeRepository BloodTypeRepository
        {
            get
            {
                if (bloodTypeRepository == null)
                {
                    bloodTypeRepository = new BloodTypeRepository(this);
                }
                return bloodTypeRepository;
            }
        }

        #endregion

        #region IDisposable Implementation
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing)
                return;

            DisposeRepositoryObject(bloodBankRepository);
            DisposeRepositoryObject(bloodDonationHistoryRepository);
            DisposeRepositoryObject(diseaseRepository);
            DisposeRepositoryObject(addressRepository);
            DisposeRepositoryObject(donorRepository);
            DisposeRepositoryObject(redBloodCellRepository);
            DisposeRepositoryObject(diseaseResultRepository);
            DisposeRepositoryObject(cityRepository);
            DisposeRepositoryObject(countryRepository);
            DisposeRepositoryObject(doctorRepository);
            DisposeRepositoryObject(requestRepository);
            DisposeRepositoryObject(plasmaRepository);
            DisposeRepositoryObject(trombocitesRepository);
            DisposeRepositoryObject(doctorRepository);
            DisposeRepositoryObject(requestRepository);
            DisposeRepositoryObject(notificationRepository);
            DisposeRepositoryObject(bloodTypeRepository);
        }

        private void DisposeRepositoryObject(RepositoryObject repositoryObject)
        {
            if (repositoryObject != null)
            {
                repositoryObject.Dispose();
                repositoryObject = null;
            }
        }

        ~RepositoryContext()
        {
            Dispose(false);
        }
        #endregion
    }
}
