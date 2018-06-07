using BloodDonation.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Controller.Core
{
    public class ControllerContext : IDisposable
    {
        #region Members
        private RepositoryContext repositoryContext;

        private BloodBankController bloodBankController;
        private RedBloodCellController redBloodCellController;
        private BloodDonationHistoryController bloodDonationHistoryController;
        private DiseaseController diseaseController;
        private DiseaseResultController diseaseResultController;
        private CityController cityController;
        private CountryController countryController;
        private DonorController donorController;
        private AddressController addressController;
        private DoctorController doctorController;
        private RequestController requestController;
        private UserController userController;
        private PatientController patientController;
        private NotificationController notificationController;
        private TrombocitesController trombocitesController;
        private PlasmaController plasmaController;
        private BloodTypeController bloodTypeController;
        #endregion

        #region Constructor
        public ControllerContext() { }
        #endregion

        #region Properties
        public RepositoryContext RepositoryContext
        {
            get
            {
                if (repositoryContext == null)
                {
                    repositoryContext = new RepositoryContext();
                }
                return repositoryContext;
            }
        }

        public BloodBankController BloodBankController
        {
            get
            {
                if (bloodBankController == null)
                {
                    bloodBankController = new BloodBankController(this);
                }
                return bloodBankController;
            }
        }

        public RedBloodCellController RedBloodCellController
        {
            get
            {
                if (redBloodCellController == null)
                {
                    redBloodCellController = new RedBloodCellController(this);
                }
                return redBloodCellController;
            }
        }

        public BloodDonationHistoryController BloodDonationHistoryController
        {
            get
            {
                if (bloodDonationHistoryController == null)
                {
                    bloodDonationHistoryController = new BloodDonationHistoryController(this);
                }
                return bloodDonationHistoryController;
            }
        }

        public DiseaseController DiseaseController
        {
            get
            {
                if (diseaseController == null)
                {
                    diseaseController = new DiseaseController(this);
                }
                return diseaseController;
            }
        }
        public DiseaseResultController DiseaseResultController
        {
            get
            {
                if (diseaseResultController == null)
                {
                    diseaseResultController = new DiseaseResultController(this);
                }
                return diseaseResultController;
            }
        }

        public DoctorController DoctorController
        {
            get
            {
                if (doctorController == null)
                {
                    doctorController = new DoctorController(this);
                }
                return doctorController;
            }
        }

        public RequestController RequestController
        {
            get
            {
                if (requestController == null)
                {
                    requestController = new RequestController(this);
                }
                return requestController;
            }
        }
        
        public CityController CityController
        {
            get
            {
                if (cityController == null)
                {
                    cityController = new CityController(this);
                }
                return cityController;
            }
        }

        public CountryController CountryController
        {
            get 
            {
                if (countryController == null)
                {
                    countryController = new CountryController(this);
                }
                return countryController;
            }
        }

        public DonorController DonorController
        {
            get
            {
                if (donorController == null)
                {
                    donorController = new DonorController(this);
                }
                return donorController;
            }
        }

        public AddressController AddressController
        {
            get
            {
                if (addressController == null)
                {
                    addressController = new AddressController(this);
                }
                return addressController;
            }
        }

        public UserController UserController
        {
            get
            {
                if (userController == null)
                {
                    userController = new UserController(this);
                }
                return userController;
            }
        }
        
        public PatientController PatientController
        {
            get
            {
                if (patientController == null)
                {
                    patientController = new PatientController(this);
                }
                return patientController;
            }
        }

        public NotificationController NotificationController
        {
            get
            {
                if (notificationController == null)
                {
                    notificationController = new NotificationController(this);
                }
                return notificationController;
            }
        }

        public TrombocitesController TrombocitesController
        {
            get
            {
                if (trombocitesController == null)
                {
                    trombocitesController = new TrombocitesController(this);
                }
                return trombocitesController;
            }
        }

        public PlasmaController PlasmaController
        {
            get
            {
                if (plasmaController == null)
                {
                    plasmaController = new PlasmaController(this);
                }
                return plasmaController;
            }
        }

        public BloodTypeController BloodTypeController
        {
            get
            {
                if (bloodTypeController == null)
                {
                    bloodTypeController = new BloodTypeController(this);
                }
                return bloodTypeController;
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

            DisposeControllerObject(bloodBankController);
            DisposeControllerObject(redBloodCellController);
            DisposeControllerObject(bloodDonationHistoryController);
            DisposeControllerObject(diseaseController);
            DisposeControllerObject(diseaseResultController);
            DisposeControllerObject(cityController);
            DisposeControllerObject(countryController);
            DisposeControllerObject(donorController);
            DisposeControllerObject(addressController);
            DisposeControllerObject(doctorController);
            DisposeControllerObject(requestController);
            DisposeControllerObject(notificationController);
            DisposeControllerObject(trombocitesController);
            DisposeControllerObject(plasmaController);
            DisposeControllerObject(bloodTypeController);

            if (repositoryContext != null)
            {
                repositoryContext.Dispose();
                repositoryContext = null;
            }
        }

        private void DisposeControllerObject(ControllerObject controllerObject)
        {
            if (controllerObject != null)
            {
                controllerObject.Dispose();
                controllerObject = null;
            }
        }

        ~ControllerContext()
        {
            Dispose(false);
        }
        #endregion
    }
}
