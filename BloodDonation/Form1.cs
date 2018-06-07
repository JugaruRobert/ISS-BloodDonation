using BloodDonation.Controller.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BloodDonation
{
    public partial class Form1 : Form
    {
        private ControllerContext context;
        private Donor donor;
        private Doctor doctor;
        private BloodDonationHistory globalBloodDonation;
        private int globalQuantity;
        private IDictionary<String, Guid> diseasesDict = new Dictionary<String, Guid>();
        private IDictionary<Guid, String> bloodBanks = new Dictionary<Guid, String>();
        private IDictionary<Guid, String> bloodTypes = new Dictionary<Guid, String>();
        private IDictionary<Utils, int> stock;

        public Form1()
        {
            context = new ControllerContext();
            donor = null;
            doctor = null;
            globalBloodDonation = null;
            InitializeComponent();
        }

        private void ComputeStock()
        {
            bool res;
            stock = new Dictionary<Utils, int>();
            List<Trombocite> trombocites = context.TrombocitesController.ReadAll().ToList();
            foreach(Trombocite t in trombocites)
            {
                Utils key = new BloodDonation.Utils("Trombocites", t.BloodTypeID.GetValueOrDefault());
                res = false;
                foreach(Utils k in stock.Keys)
                {
                    if(key.ID == k.ID && key.Type == k.Type)
                    {
                        stock[k] = stock[k] + t.Quantity.GetValueOrDefault();
                        res = true;
                        break;
                    }
                }

                if(!res)
                {
                    stock.Add(new KeyValuePair<Utils, int>(key, t.Quantity.GetValueOrDefault()));
                }
            }

            List<RedBloodCell> redCells = context.RedBloodCellController.ReadAll().ToList();
            foreach (RedBloodCell t in redCells)
            {
                Utils key = new BloodDonation.Utils("RedBloodCells", t.BloodTypeID.GetValueOrDefault());
                res = false;
                foreach (Utils k in stock.Keys)
                {
                    if (key.ID == k.ID && key.Type == k.Type)
                    {
                        stock[k] = stock[k] + t.Quantity.GetValueOrDefault();
                        res = true;
                        break;
                    }
                }

                if (!res)
                {
                    stock.Add(new KeyValuePair<Utils, int>(key, t.Quantity.GetValueOrDefault()));
                }
            }

            List<Plasma> plasma = context.PlasmaController.ReadAll().ToList();
            foreach (Plasma t in plasma)
            {
                Utils key = new BloodDonation.Utils("Plasma", t.BloodTypeID.GetValueOrDefault());
                res = false;
                foreach (Utils k in stock.Keys)
                {
                    if (key.ID == k.ID && key.Type == k.Type)
                    {
                        stock[k] = stock[k] + t.Quantity.GetValueOrDefault();
                        res = true;
                        break;
                    }
                }

                if (!res)
                {
                    stock.Add(new KeyValuePair<Utils, int>(key, t.Quantity.GetValueOrDefault()));
                }
            }
        }

        private void ReadBloodBanks()
        {
            List<BloodBank> banks = context.BloodBankController.ReadAll().ToList();
            foreach (BloodBank b in banks)
            {
                bloodBanks.Add(new KeyValuePair<Guid, string>(new Guid(b.BankID.ToString().ToLower()), b.Name));
            }
        }

        private void ReadBloodTypes()
        {
            List<BloodType> bloods = context.BloodTypeController.ReadAll().ToList();
            foreach(BloodType b in bloods)
            {
                bloodTypes.Add(new KeyValuePair<Guid, string>(b.BloodTypeID, b.Blood + " RH" + (b.RH.GetValueOrDefault() ? "+" : "-")));
            }
        }

        private void PopulateDonations()
        {
            List<BloodDonationHistory> history = context.BloodDonationHistoryController.GetBloodHistoryForDonor(donor.CNP).ToList();
            Dictionary<String, bool> columns = new Dictionary<string, bool>();
            columns.Add("DonationID", false);
            columns.Add("DonatedForCNP", true);
            columns.Add("Date", true);
            columns.Add("Quantity", true);
            populateGridView("Donations", columns, history, donorDonationsDGV);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.notificationsPanel.Visible = false;
            this.donationHistoryPanel.Visible = true;
            PopulateDonations();
            
            //populateListView(donationHistoryList, history);
        }

        private void notificationsButton_Click(object sender, EventArgs e)
        {
            this.donationHistoryPanel.Visible = false;
            this.notificationsPanel.Visible = true;
            List<Notification> notifications = context.NotificationController.ReadAll().ToList();
            notifications = notifications.Where(n => n.DonorCNP == donor.CNP).ToList();
            List<String> donorNotifications = new List<String>();
            Dictionary<string, bool> columns = new Dictionary<string, bool>();
            columns.Add("Description", true);
            columns.Add("Status", true);
            columns.Add("NotificationID", false);
            populateGridView("Notifications", columns, notifications, notificationsDGV);
            //populateListView(notificationsList, donorNotifications);
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            try
            {
                string username = this.usernameBox.Text;
                string password = this.passwordBox.Text;

                User user = context.UserController.ReadByID(username);

                if (user == null)
                {
                    throw new Exception("User does not exist.");
                }
                else if (user.Role == "Donor")
                {
                    if (user.Password == password)
                    {

                        this.loginPanel.Visible = false;
                        this.donorPanel.Visible = true;
                        this.notificationsPanel.Visible = false;
                        this.donationHistoryPanel.Visible = true;
                        donor = context.DonorController.ReadByID(user.CNP);
                        this.donorNameLabel.Text = donor.LastName + "\n" + donor.FirstName;
                        PopulateDonations();
                        //populateListView(this.donationHistoryList, context.BloodDonationHistoryController.GetBloodHistoryForDonor(user.CNP).ToList());
                    }
                    else
                    {
                        throw new Exception("Incorrect password.");
                    }
                }
                else if (user.Role == "Doctor")
                {
                    if (user.Password == password)
                    {
                        this.loginPanel.Visible = false;
                        this.doctorPanel.Visible = true;
                        this.doctorRequestsStatusPanel.Visible = true;
                        this.doctorBloodDonationsPanel.Visible = false;
                        this.doctorStockPanel.Visible = false;
                        doctor = context.DoctorController.ReadByID(user.CNP);
                        doctorNameLabel.Text = doctor.LastName + "\n" + doctor.FirstName;
                        ReadDiseases();
                        ReadBloodBanks();
                        ReadBloodTypes();
                        populateDoctorRequestGridView();
                    }
                    else
                    {
                        throw new Exception("Incorrect password.");
                    }
                }
                else if (user.Role == "Admin")
                {
                    if (user.Password == password)
                    {
                        this.loginPanel.Visible = false;
                        this.centerPanel.Visible = true;
                        this.centerRequestsPanel.Visible = false;
                        this.centerDonationsPanel.Visible = false;
                        this.centerStockPanel.Visible = true;
                        ReadBloodBanks();
                        ReadBloodTypes();
                    }
                    else
                    {
                        throw new Exception("Incorrect password.");
                    }
                }
                else
                {
                    throw new Exception("Invalid user.");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void ReadDiseases()
        {
            IEnumerable<Disease> diseases = this.context.DiseaseController.ReadAll();
            foreach(Disease dis in diseases)
            {
                diseasesDict[dis.Name] = dis.DiseaseID;
            }
        }

        //private void populateListView<T>(ListView listView, List<T> result)
        //{
        //    listView.Clear();
        //    foreach(T t in result)
        //    {
        //        ListViewItem item = new ListViewItem(t.ToString());
        //        listView.Items.Add(item);
        //    }
        //}

        private void logoutLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.donorPanel.Visible = false;
            this.loginPanel.Visible = true;
            donor = null;
        }

        public void populateDoctorRequestGridView()
        {

            List<Request> source = context.RequestController.ReadAll().ToList();
            source = source.Where(r => r.DoctorCNP == doctor.DoctorCNP).ToList();

            DataTable table = new DataTable();

            DataColumn col1 = new DataColumn("PatientCNP");
            table.Columns.Add(col1);
            DataColumn col2 = new DataColumn("Blood Bank");
            table.Columns.Add(col2);
            DataColumn col3 = new DataColumn("Priority");
            table.Columns.Add(col3);
            DataColumn col4 = new DataColumn("Red Blood Cells");
            table.Columns.Add(col4);
            DataColumn col5 = new DataColumn("Trombocites");
            table.Columns.Add(col5);
            DataColumn col6 = new DataColumn("Plasma");
            table.Columns.Add(col6);
            DataColumn col7 = new DataColumn("Status");
            table.Columns.Add(col7);

            foreach (Request r in source)
            {
                DataRow row = table.NewRow();

                row["PatientCNP"] = r.PatientCNP;
                row["Blood Bank"] = bloodBanks[new Guid(r.BloodBankID.GetValueOrDefault().ToString().ToLower())];
                //row["Blood Bank"] = r.BloodBankID.GetValueOrDefault();
                row["Priority"] = (r.Priority == 1) ? "Low" : ((r.Priority == 2) ? "Medium" : "High");
                row["Red Blood Cells"] = r.RedBloodCellsQuantity;
                row["Trombocites"] = r.TrombocitesQuantity;
                row["Plasma"] = r.PlasmaQuantity;
                row["Status"] = (r.IsCompleted.GetValueOrDefault()) ? "Delievered" : "In progress";

                table.Rows.Add(row);
            }

            doctorRequestsDGV.DataSource = table;
            doctorRequestsDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void requestStatusButton_Click(object sender, EventArgs e)
        {
            this.doctorBloodDonationsPanel.Visible = false;
            this.doctorStockPanel.Visible = false;
            this.doctorRequestsStatusPanel.Visible = true;
            //Dictionary<string, bool> columns = new Dictionary<string, bool>();
            //columns.Add("PatientCNP", true);
            //columns.Add("Priority", true);
            //columns.Add("RedBloodCellsQuantity", true);
            //columns.Add("PlasmaQuantity", true);
            //columns.Add("TrombocitesQuantity", true);
            //populateGridView("Requests", columns, requests, centerRequestsDGV);
            populateDoctorRequestGridView();

        }

        private void stockButton_Click(object sender, EventArgs e)
        {
            this.doctorBloodDonationsPanel.Visible = false;
            this.doctorRequestsStatusPanel.Visible = false;
            this.doctorStockPanel.Visible = true;
            populateStockGridView(doctorStockDGV);
        }

        private void doctorBloodDonationsButton_Click(object sender, EventArgs e)
        {
            this.doctorRequestsStatusPanel.Visible = false;
            this.doctorStockPanel.Visible = false;
            this.doctorBloodDonationsPanel.Visible = true;
            IEnumerable<BloodDonationHistory> donations = this.context.BloodDonationHistoryController.ReadAll();
            donations = donations.Where(donation => donation.Quantity == 0).ToArray();
            Dictionary<string, bool> columns = new Dictionary<string, bool>();
            columns.Add("DonorCNP", true);
            columns.Add("DonationID", false);
            columns.Add("DonatedForCNP", true);
            populateGridView("Donations", columns, donations.ToList(), donationsToTestDGV);
        }

        public void populateGridView<T>(String name, Dictionary<string, bool> columns, List<T> source, DataGridView dgv)
        {
            List<MethodInfo> propMethods = new List<MethodInfo>();
            foreach(String c in columns.Keys)
                propMethods.Add(typeof(T).GetProperty(c).GetGetMethod());

            DataTable table = new DataTable(name);

            foreach(String c in columns.Keys)
            {
                DataColumn col = new DataColumn(c);
                table.Columns.Add(col);
            }

            object[] obj = new List<Object>().ToArray();
            List<String> keys = columns.Keys.ToList();

            for(int j = 0; j < source.Count(); j++)
            {
                DataRow row = table.NewRow();

                for (int i = 0; i < columns.Count(); i++)
                    row[keys[i]] = propMethods[i].Invoke(source[j], null);
                
                table.Rows.Add(row);
            }

            dgv.DataSource = table;
            foreach(KeyValuePair<String, bool> p in columns)
            {
                if (!p.Value)
                    dgv.Columns[p.Key].Visible = false;
            }
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        public void populateStockGridView(DataGridView dgv)
        {
            context.TrombocitesController.RefreshBloodStock();
            ComputeStock();

            DataTable table = new DataTable();
            
            DataColumn col1 = new DataColumn("Type");
            DataColumn col2 = new DataColumn("BloodTypeID");
            DataColumn col3 = new DataColumn("BloodType");
            DataColumn col4 = new DataColumn("Quantity");

            table.Columns.Add(col1);
            table.Columns.Add(col2);
            table.Columns.Add(col3);
            table.Columns.Add(col4);

            foreach(KeyValuePair<Utils, int> p in stock)
            {
                DataRow row = table.NewRow();

                row["Type"] = p.Key.Type;
                row["BloodTypeID"] = p.Key.ID;
                row["BloodType"] = bloodTypes[p.Key.ID];
                row["Quantity"] = p.Value;

                table.Rows.Add(row);
            }

            dgv.DataSource = table;
            dgv.Columns["BloodTypeID"].Visible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //List<Trombocite> trombocites = context.TrombocitesController.ReadAll().ToList();
            //List<RedBloodCell> redCells = context.RedBloodCellController.ReadAll().ToList();
            //List<Plasma> plasma = context.PlasmaController.ReadAll().ToList();

            //DataTable table = new DataTable("Stock");

            //DataColumn col1 = new DataColumn("ElementID");
            //DataColumn col2 = new DataColumn("Type");
            //DataColumn col3 = new DataColumn("BankID");
            //DataColumn col4 = new DataColumn("BloodBank");
            //DataColumn col5 = new DataColumn("BloodTypeID");
            //DataColumn col6 = new DataColumn("BloodType");
            //DataColumn col7 = new DataColumn("ExpirationDate");
            //DataColumn col8 = new DataColumn("Quantity");

            //table.Columns.Add(col1);
            //table.Columns.Add(col2);
            //table.Columns.Add(col3);
            //table.Columns.Add(col4);
            //table.Columns.Add(col5);
            //table.Columns.Add(col6);
            //table.Columns.Add(col7);
            //table.Columns.Add(col8);

            //foreach(Trombocite t in trombocites)
            //{
            //    DataRow row = table.NewRow();

            //    row["ElementID"] = t.TrombocitesID;
            //    row["Type"] = "Trombocites";
            //    row["BankID"] = t.BankID;
            //    row["BloodBank"] = bloodBanks[t.BankID.GetValueOrDefault()];
            //    row["BloodTypeID"] = t.BloodTypeID;
            //    row["BloodType"] = bloodTypes[t.BloodTypeID.GetValueOrDefault()];
            //    row["ExpirationDate"] = t.ExpirationDate;
            //    row["Quantity"] = t.Quantity;

            //    table.Rows.Add(row);
            //}

            //foreach (RedBloodCell r in redCells)
            //{
            //    DataRow row = table.NewRow();

            //    row["ElementID"] = r.RedBloodCellsID;
            //    row["Type"] = "Red Blood Cells";
            //    row["BankID"] = r.BankID;
            //    row["BloodBank"] = bloodBanks[r.BankID.GetValueOrDefault()];
            //    row["BloodTypeID"] = r.BloodTypeID;
            //    row["BloodType"] = bloodTypes[r.BloodTypeID.GetValueOrDefault()];
            //    row["ExpirationDate"] = r.ExpirationDate;
            //    row["Quantity"] = r.Quantity;

            //    table.Rows.Add(row);
            //}

            //foreach (Plasma p in plasma)
            //{
            //    DataRow row = table.NewRow();

            //    row["ElementID"] = p.PlasmaID;
            //    row["Type"] = "Plasma";
            //    row["BankID"] = p.BankID;
            //    row["BloodBank"] = bloodBanks[p.BankID.GetValueOrDefault()];
            //    row["BloodTypeID"] = p.BloodTypeID;
            //    row["BloodType"] = bloodTypes[p.BloodTypeID.GetValueOrDefault()];
            //    row["ExpirationDate"] = p.ExpirationDate;
            //    row["Quantity"] = p.Quantity;

            //    table.Rows.Add(row);
            //}

            //dgv.DataSource = table;
            //dgv.Columns["BankID"].Visible = false;
            //dgv.Columns["BloodTypeID"].Visible = false;
            //dgv.Columns["ElementID"].Visible = false;
            //dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void populateCenterRequestDGV()
        {
            List<Request> requests = context.RequestController.ReadAll().ToList();
            requests = requests.Where(r => r.IsCompleted == false).ToList();
            requests.Sort((r1, r2) => (r1.Priority < r2.Priority) ? 1 : 0);
            context.TrombocitesController.RefreshBloodStock();
            ComputeStock();

            DataTable table = new DataTable();

            DataColumn col1 = new DataColumn("PatientCNP");
            DataColumn col2 = new DataColumn("DoctorCNP");
            DataColumn col3 = new DataColumn("Priority");
            DataColumn col4 = new DataColumn("RedBloodCellsQuantity");
            DataColumn col5 = new DataColumn("PlasmaQuantity");
            DataColumn col6 = new DataColumn("TrombocitesQuantity");
            DataColumn col7 = new DataColumn("Stock available");

            table.Columns.Add(col1);
            table.Columns.Add(col2);
            table.Columns.Add(col3);
            table.Columns.Add(col4);
            table.Columns.Add(col5);
            table.Columns.Add(col6);
            table.Columns.Add(col7);

            foreach (Request r in requests)
            {
                DataRow row = table.NewRow();

                row["PatientCNP"] = r.PatientCNP;
                row["DoctorCNP"] = r.DoctorCNP;
                row["Priority"] = (r.Priority == 1) ? "Low" : ((r.Priority == 2) ? "Medium" : "High");
                row["RedBloodCellsQuantity"] = r.RedBloodCellsQuantity;
                row["PlasmaQuantity"] = r.PlasmaQuantity;
                row["TrombocitesQuantity"] = r.TrombocitesQuantity;
                row["Stock available"] = isAvailable(r);

                table.Rows.Add(row);
            }

            centerRequestsDGV.DataSource = table;
            centerRequestsDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private string isAvailable(Request r)
        {
            Guid bloodType = context.PatientController.ReadByID(r.PatientCNP).BloodTypeID.GetValueOrDefault();
            bool enoughPlasma = false, enoughTrombocites = false, enoughRedCells = false;

            if (r.PlasmaQuantity == 0)
                enoughPlasma = true;
            if (r.RedBloodCellsQuantity == 0)
                enoughRedCells = true;
            if (r.TrombocitesQuantity == 0)
                enoughTrombocites = true;

            foreach(KeyValuePair<Utils, int> p in stock)
            {
                if(p.Key.ID == bloodType)
                {
                    if (p.Key.Type == "Trombocites")
                        if (p.Value < r.TrombocitesQuantity)
                            return "No";
                        else
                            enoughTrombocites = true;
                    else if (p.Key.Type == "RedBloodCells")
                        if (p.Value < r.RedBloodCellsQuantity)
                            return "No";
                        else
                            enoughRedCells = true;
                    else if (p.Key.Type == "Plasma")
                        if (p.Value < r.PlasmaQuantity)
                            return "No";
                        else
                            enoughPlasma = true;
                    
                }
            }

            if(enoughRedCells && enoughPlasma && enoughTrombocites)
                return "Yes";
            return "No";
        }

        private void doctorLogoutLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.doctorPanel.Visible = false;
            this.loginPanel.Visible = true;
            doctor = null;
        }

        private void donateFormButton_Click(object sender, EventArgs e)
        {
            if(specificDonation.Text == "")
            {
                BloodDonationHistory donation = new BloodDonationHistory(donor.CNP,null, Guid.NewGuid());
                this.context.BloodDonationHistoryController.InsertEmpty(donation);

                MessageBox.Show("Your request has been sent! Please go to the nearest donation center for the actual donation!", "You are a life saver!", MessageBoxButtons.OK);
                this.donatePanel.Visible = false;
                this.donorPanel.Visible = true;
            }
            else
            {
                string specificCNP = specificDonation.Text;
                Patient patient = this.context.PatientController.ReadByID(specificCNP);
                if(patient == null)
                {
                    MessageBox.Show("There is no patient with this CNP that needs blood! Please contact the nearest blood donation center if you consider this as being a mistake!", "We are sorry!", MessageBoxButtons.OK);
                }
                else
                {
                    BloodDonationHistory donation = new BloodDonationHistory(donor.CNP, patient.CNP, Guid.NewGuid());
                    this.context.BloodDonationHistoryController.InsertEmptySpecific(donation);

                    MessageBox.Show("Your request has been sent! Please go to the nearest donation center for the actual donation!", "You are a life saver!", MessageBoxButtons.OK);
                    this.donatePanel.Visible = false;
                    this.donorPanel.Visible = true;
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.donatePanel.Visible = false;
            this.donorPanel.Visible = true;
        }

        private void donateButton_Click(object sender, EventArgs e)
        {
            int result = TestCanDonate();
            switch (result)
            {
                case 0:
                    this.donorPanel.Visible = false;
                    this.donatePanel.Visible = true;
                    break;
                case 1:
                    MessageBox.Show("You must wait at least 8 weeks until you can donate again!", "Error", MessageBoxButtons.OK);
                    break;
                case 2:
                    MessageBox.Show("You already have an active donation request! Please go to the donation center or wait for the results!", "Error", MessageBoxButtons.OK);
                    break;
                case 3:
                    MessageBox.Show("You cannot donate because of a past blood analysis result!", "Error", MessageBoxButtons.OK);
                    break;
            }
        }

        //0 - if user can donate
        //1 - if user cannot donate because of the date
        //2 - if user must go to the donation center to donate
        //3 - if user cannot donate beacuse of a disease
        private int TestCanDonate()
        {
            BloodDonationHistory disease = context.BloodDonationHistoryController.GetHasDeseases(donor.CNP);
            if (disease != null)
                return 3;

            BloodDonationHistory latestDonation = context.BloodDonationHistoryController.GetLatestDonation(donor.CNP);

            if (latestDonation == null)
                return 0;

            if (latestDonation.Date != null)
            {
                DateTime donationDate = latestDonation.Date.GetValueOrDefault();
                DateTime currentDate = DateTime.Now;

                if ((currentDate - donationDate).Days < 56)
                    return 1;
                else
                    return 0;
            }
            else
                return 2;
        }

        private void editInfoButton_Click(object sender, EventArgs e)
        {
            this.donorPanel.Visible = false;
            this.editInfoPanel.Visible = true;
            this.editFirstNameBox.Text = donor.FirstName;
            this.editLastNameBox.Text = donor.LastName;


            this.editAddressBox.Text = context.AddressController.ReadByID(donor.AddressID.GetValueOrDefault()).Address1;
            this.editResidenceBox.Text = context.AddressController.ReadByID(donor.ResidenceID.GetValueOrDefault()).Address1;

            IEnumerable<Country> countries = context.CountryController.ReadAll();
            countryAddressEdit.Items.Clear();
            countryResidentEdit.Items.Clear();
            foreach (Country country in countries)
            {
                this.countryAddressEdit.Items.Add(country.Name);
                this.countryResidentEdit.Items.Add(country.Name);
            }


            Guid cityId = context.AddressController.ReadByID(donor.AddressID.GetValueOrDefault()).CityID.GetValueOrDefault();
            Guid countryId = context.CityController.ReadByID(cityId).CountryID.GetValueOrDefault();


            Guid cityId1 = context.AddressController.ReadByID(donor.ResidenceID.GetValueOrDefault()).CityID.GetValueOrDefault();
            Guid countryId1 = context.CityController.ReadByID(cityId1).CountryID.GetValueOrDefault();


            IEnumerable<City> citiesAddress = context.CityController.GetCitiesByCountry(countryId);
            registerResidenceCityBox.Items.Clear();

            if (citiesAddress.Count() == 0)
            {
                this.cityAddressEdit.Items.Add("No cities for specified country.");
            }
            else
            {
                foreach (City city in citiesAddress)
                {
                    this.cityAddressEdit.Items.Add(city.Name);
                }
            }

            IEnumerable<City> citiesResidence = context.CityController.GetCitiesByCountry(countryId1);
            registerResidenceCityBox.Items.Clear();

            if (citiesResidence.Count() == 0)
            {
                this.cityResidentEdit.Items.Add("No cities for specified country.");
            }
            else
            {
                foreach (City city in citiesResidence)
                {
                    this.cityResidentEdit.Items.Add(city.Name);
                }
            }


            this.countryAddressEdit.SelectedItem = context.CountryController.ReadByID(countryId).Name;
            this.cityAddressEdit.SelectedItem = context.CityController.ReadByID(cityId).Name;

            this.countryResidentEdit.SelectedItem = context.CountryController.ReadByID(countryId1).Name;
            this.cityResidentEdit.SelectedItem = context.CityController.ReadByID(cityId1).Name;


        }

        private void saveInfoButton_Click(object sender, EventArgs e)
        {
            List<TextBox> boxes = new List<TextBox>();
            List<ComboBox> cboxes = new List<ComboBox>();

            this.editFirstNameBox.BackColor = Color.White;
            this.editLastNameBox.BackColor = Color.White;
            this.editAddressBox.BackColor = Color.White;
            this.cityAddressEdit.BackColor = Color.White;
            this.countryAddressEdit.BackColor = Color.White;
            this.editResidenceBox.BackColor = Color.White;
            this.cityResidentEdit.BackColor = Color.White;
            this.countryResidentEdit.BackColor = Color.White;

            string firstName = this.editFirstNameBox.Text;
            string lastName = this.editLastNameBox.Text;
            string addressEdited = this.editAddressBox.Text;
            string addressCity = this.cityAddressEdit.SelectedItem.ToString();
            string addressCountry = this.countryAddressEdit.SelectedItem.ToString();
            string residenceEdited = this.editResidenceBox.Text;
            string residenceCity = this.cityResidentEdit.SelectedItem.ToString();
            string residenceCountry = this.countryResidentEdit.SelectedItem.ToString();

            if (firstName == "") boxes.Add(this.editFirstNameBox);
            if (lastName == "") boxes.Add(this.editLastNameBox);
            if (addressEdited == "") boxes.Add(this.editAddressBox);
            if (residenceEdited == "") boxes.Add(this.editResidenceBox);
            if (addressCity == "No cities for specified country.") cboxes.Add(cityAddressEdit);
            if (residenceCity == "No cities for specified country.") cboxes.Add(cityResidentEdit);

            if (boxes.Count == 0 && cboxes.Count == 0)
            {
                Guid countryID = context.CountryController.GetCountryByName(addressCountry).CountryID;
                Guid cityID = context.CityController.GetCityByName(addressCity, countryID).CityID;
                Guid residenceCountryID = context.CountryController.GetCountryByName(residenceCountry).CountryID;
                Guid residenceCityID = context.CityController.GetCityByName(residenceCity, residenceCountryID).CityID;


                Guid addressId = donor.AddressID.Value;
                Guid residentId = donor.ResidenceID.Value;

                //updatam in db pentru address
                Address address = context.AddressController.ReadByID(addressId);
                address.Address1 = addressEdited;
                address.CityID = cityID;
                context.AddressController.Update(address);

                Console.WriteLine(cityID);

                //daca adresa coicinde cu adresa de residenta atunci trebuie sa inseram o noua adresa daca vrem sa modificam una..
                if (addressId == residentId)
                {
                    //updatam in db pentru resident
                    Address resident = new Address(Guid.NewGuid(), residenceEdited, residenceCityID);
                    context.AddressController.Insert(resident);
                    residentId = resident.AddressID;
                }
                else
                {
                    Address resident = context.AddressController.ReadByID(residentId);
                    resident.Address1 = residenceEdited;
                    resident.CityID = residenceCityID;
                    context.AddressController.Update(resident);
                }


                //updatam donorul
                Donor updatedDonor = new Donor(donor.CNP, firstName, lastName, donor.DateOfBirth.GetValueOrDefault(), addressId, residentId, donor.BloodTypeID.GetValueOrDefault());
                context.DonorController.Update(updatedDonor);
                donor = updatedDonor;

                this.donorNameLabel.Text = donor.LastName + "\n" + donor.FirstName;

                DialogResult mb = MessageBox.Show("Information has been updated.", "Successful!", MessageBoxButtons.OK);
                this.editInfoPanel.Visible = false;
                this.donorPanel.Visible = true;
            }
            else
            {
                foreach (TextBox t in boxes)
                    t.BackColor = Color.IndianRed;

                foreach (ComboBox t in cboxes)
                    t.BackColor = Color.IndianRed;
            }

        }

        private void cancelButtonEdit_Click(object sender, EventArgs e)
        {
            this.editInfoPanel.Visible = false;
            this.donorPanel.Visible = true;
        }

        private void registerFormButton_Click(object sender, EventArgs e)
        {
            List<TextBox> boxes = new List<TextBox>();
            List<ComboBox> drops = new List<ComboBox>();

            registerUsernameBox.BackColor = Color.White;
            registerFirstNameBox.BackColor = Color.White;
            registerLastNameBox.BackColor = Color.White;
            registerCNPBox.BackColor = Color.White;
            registerPasswordBox.BackColor = Color.White;
            registerPassword2Box.BackColor = Color.White;
            registerAddressCountryBox.BackColor = Color.White;
            registerResidenceCountryBox.BackColor = Color.White;
            registerYearBox.BackColor = Color.White;
            registerMonthBox.BackColor = Color.White;
            registerDayBox.BackColor = Color.White;

            try
            {
                string username = registerUsernameBox.Text;

                if (username == "")
                    boxes.Add(registerUsernameBox);
                

                User user = context.UserController.ReadByID(username);
                if (user != null)
                    boxes.Add(registerUsernameBox);

                string cnp = registerCNPBox.Text;
                if (cnp == "")
                    boxes.Add(registerCNPBox);
                this.donor = context.DonorController.ReadByID(cnp);
                if (donor != null)
                    boxes.Add(registerCNPBox);

                string password = registerPasswordBox.Text;
                string password2 = registerPassword2Box.Text;
                if (password != password2 || password == "")
                {
                    boxes.Add(registerPasswordBox);
                    boxes.Add(registerPassword2Box);
                }

                string firstName = registerFirstNameBox.Text;
                if (firstName == "")
                    boxes.Add(registerFirstNameBox);

                string lastName = registerLastNameBox.Text;
                if (lastName == "")
                    boxes.Add(registerLastNameBox);

                if (registerAddressCountryBox.SelectedIndex == 0)
                    drops.Add(registerAddressCountryBox);

                if (registerResidenceCountryBox.SelectedIndex == 0)
                    drops.Add(registerResidenceCountryBox);

                if (registerAddressCityBox.SelectedIndex == -1)
                    drops.Add(registerAddressCityBox);

                if (registerResidenceCityBox.SelectedIndex == -1)
                    drops.Add(registerResidenceCityBox);

                string day = "";
                if (registerDayBox.SelectedIndex == -1)
                    drops.Add(registerDayBox);
                else
                {
                    day = registerDayBox.SelectedItem.ToString();
                    if (day.Length == 1)
                        day = "0" + day;
                }


                string month = "";
                if (registerMonthBox.SelectedIndex == -1)
                    drops.Add(registerMonthBox);
                else
                {
                    month = registerMonthBox.SelectedItem.ToString();
                    if (month.Length == 1)
                        month = "0" + month;
                }

                string year = "";
                if (registerYearBox.SelectedIndex == -1)
                    drops.Add(registerYearBox);
                else
                {
                    year = registerYearBox.SelectedItem.ToString();
                }

                DateTime birthday = new DateTime();

                if (year != "" && month != "" && day != "")
                {
                    string birthdayString = year + "-" + month + "-" + day;
                    if (!DateTime.TryParse(birthdayString, out birthday))
                    {
                        drops.Add(registerYearBox);
                        drops.Add(registerMonthBox);
                        drops.Add(registerDayBox);
                    }
                }

                if(boxes.Count() == 0 && drops.Count() == 0)
                {
                    string email = registerEmailBox.Text;
                    string address = registerAddressBox.Text;
                    string addressCity = registerAddressCityBox.SelectedItem.ToString();
                    string addressCountry = registerAddressCountryBox.SelectedItem.ToString();
                    string residence = registerResidenceBox.Text;
                    string residenceCity = registerResidenceCityBox.SelectedItem.ToString();
                    string residenceCountry = registerResidenceCountryBox.SelectedItem.ToString();

                    Guid addressCountryID = context.CountryController.GetCountryByName(addressCountry).CountryID;
                    Guid addressCityID = context.CityController.GetCityByName(addressCity, addressCountryID).CityID;
                    Guid newAddressID = Guid.NewGuid();
                    Address newAddress = new Address(newAddressID, address, addressCityID);
                    context.AddressController.Insert(newAddress);

                    Guid residenceCountryID = context.CountryController.GetCountryByName(residenceCountry).CountryID;
                    Guid residenceCityID = context.CityController.GetCityByName(residenceCity, residenceCountryID).CityID;
                    Guid newResidenceID = Guid.NewGuid();
                    Address newResidence = new Address(newResidenceID, residence, residenceCityID);
                    context.AddressController.Insert(newResidence);

                    user = new BloodDonation.User(username, password, email, "Donor", cnp);
                    context.UserController.Insert(user);
                    donor = new Donor(cnp, firstName, lastName, birthday, newAddressID, newResidenceID, new Guid());
                    context.DonorController.Insert(donor);
                    this.donorNameLabel.Text = donor.LastName + "\n" + donor.FirstName;
                    //populateListView(this.donationHistoryList, context.BloodDonationHistoryController.GetBloodHistoryForDonor(user.CNP).ToList());

                    MessageBox.Show("Registration complete");

                    this.registrationFormPanel.Visible = false;
                    this.donorPanel.Visible = true;
                }
                else
                {
                    foreach (TextBox t in boxes)
                        t.BackColor = Color.IndianRed;

                    foreach (ComboBox c in drops)
                        c.BackColor = Color.IndianRed;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Registration error");
            }
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            this.loginPanel.Visible = false;
            this.registrationFormPanel.Visible = true;

            registerYearBox.Items.Clear();
            int currentYear = DateTime.Now.Year - 18;
            int minYear = currentYear - 65;
            while(currentYear >= minYear)
            {
                registerYearBox.Items.Add(currentYear.ToString());
                currentYear--;
            }

            registerYearBox.SelectedIndex = 0;

            registerMonthBox.Items.Clear();
            int i = 1;
            while(i <= 12)
            {
                registerMonthBox.Items.Add(i.ToString());
                i++;
            }

            registerMonthBox.SelectedIndex = 0;

            IEnumerable<Country> countries = context.CountryController.ReadAll();
            registerAddressCountryBox.Items.Clear();
            registerResidenceCountryBox.Items.Clear();
            registerAddressCountryBox.Items.Add("Country");
            registerResidenceCountryBox.Items.Add("Country");
            foreach (Country country in countries)
            {
                registerAddressCountryBox.Items.Add(country.Name);
                registerResidenceCountryBox.Items.Add(country.Name);
            }

            registerAddressCountryBox.SelectedIndex = 0;
            registerResidenceCountryBox.SelectedIndex = 0;

            registerAddressCityBox.Items.Clear();
            registerAddressCityBox.Items.Add("Select country first.");
            registerAddressCityBox.SelectedIndex = 0;
            registerResidenceCityBox.Items.Clear();
            registerResidenceCityBox.Items.Add("Select country first.");
            registerResidenceCityBox.SelectedIndex = 0;
        }

        private void requestFormButton_Click(object sender, EventArgs e)
        {
            requestRadioHigh.BackColor = Color.White;
            requestRadioLow.BackColor = Color.White;
            requestRadioMedium.BackColor = Color.White;
            requestBankBox.BackColor = Color.White;
            requestPatientBox.BackColor = Color.White;
            requestRedCellsBox.BackColor = Color.White;
            requestPlasmaBox.BackColor = Color.White;
            requestTrombocitesBox.BackColor = Color.White;

            List<TextBox> boxes = new List<TextBox>();
            List<ComboBox> drops = new List<ComboBox>();
            bool radios = false;

            try
            {
                int priority = 0;
                if (requestRadioLow.Checked)
                    priority = 1;
                else if (requestRadioMedium.Checked)
                    priority = 2;
                else if (requestRadioHigh.Checked)
                    priority = 3;
                else
                {
                    radios = true;
                    requestRadioHigh.BackColor = Color.IndianRed;
                    requestRadioLow.BackColor = Color.IndianRed;
                    requestRadioMedium.BackColor = Color.IndianRed;
                }

                string doctorCNP = doctor.DoctorCNP;
                if (doctorCNP == "")
                    throw new Exception("Not logged in.");

                if (requestBankBox.SelectedItem.ToString().Length < 32)
                    drops.Add(requestBankBox);

                Guid bloodBankID = new Guid(requestBankBox.SelectedItem.ToString().Split(':')[0]);

                if (requestPatientBox.SelectedItem.ToString() == "")
                    drops.Add(requestPatientBox);

                string patientCNP = requestPatientBox.SelectedItem.ToString().Split(':')[0];
                int redBloodCellsQuantity = 0, trombocitesQuantity = 0, plasmaQuantity = 0;

                try
                {
                    redBloodCellsQuantity = Convert.ToInt16(requestRedCellsBox.Text);
                }
                catch
                {
                    boxes.Add(requestRedCellsBox);
                }

                try
                {
                    trombocitesQuantity = Convert.ToInt16(requestTrombocitesBox.Text);
                }
                catch
                {
                    boxes.Add(requestTrombocitesBox);
                }

                try
                {
                    plasmaQuantity = Convert.ToInt16(requestPlasmaBox.Text);
                }
                catch
                {
                    boxes.Add(requestPlasmaBox);
                }

                if (redBloodCellsQuantity == 0)
                    boxes.Add(requestRedCellsBox);
                if (plasmaQuantity == 0)
                    boxes.Add(requestPlasmaBox);
                if (trombocitesQuantity == 0)
                    boxes.Add(requestTrombocitesBox);

                if (radios == false && boxes.Count() == 0 && drops.Count() == 0)
                {
                    Request r = context.RequestController.ReadByID(patientCNP);
                    if (r != null && r.IsCompleted == false)
                        throw new Exception("Someone already requested blood for this patient.");

                    Request request = new Request(patientCNP, doctorCNP, bloodBankID, priority, redBloodCellsQuantity, plasmaQuantity, trombocitesQuantity, false);
                    context.RequestController.Insert(request);

                    DialogResult mb = MessageBox.Show("Your request has been sent.", "Successful!", MessageBoxButtons.OK);
                    this.doctorRequestPanel.Visible = false;
                    this.doctorPanel.Visible = true;
                }
                else
                {
                    foreach (TextBox t in boxes)
                        t.BackColor = Color.IndianRed;
                    foreach (ComboBox c in drops)
                        c.BackColor = Color.IndianRed;
                    requestRadioHigh.BackColor = Color.IndianRed;
                    requestRadioLow.BackColor = Color.IndianRed;
                    requestRadioMedium.BackColor = Color.IndianRed;
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            
        }

        private void requestFormCancelButton_Click(object sender, EventArgs e)
        {
            doctorPanel.Visible = true;
            doctorRequestPanel.Visible = false;
        }

        private void requestButton_Click(object sender, EventArgs e)
        {
            this.doctorPanel.Visible = false;
            this.doctorRequestPanel.Visible = true;
            requestRedCellsBox.Text = "0";
            requestPlasmaBox.Text = "0";
            requestTrombocitesBox.Text = "0";

            IEnumerable<Patient> patients = context.PatientController.ReadAll();
            requestPatientBox.Items.Clear();
            if (patients.Count() == 0)
            {
                requestFormSendButton.Enabled = false;
                requestPatientBox.Items.Add("No patients.");
            }
            else
            {
                requestFormSendButton.Enabled = true;
                
                foreach (Patient patient in patients)
                {
                    requestPatientBox.Items.Add(patient.CNP + ": " + patient.LastName + " " + patient.FirstName);
                }
            }
            requestPatientBox.SelectedIndex = 0;

            IEnumerable<BloodBank> banks = context.BloodBankController.ReadAll();
            requestBankBox.Items.Clear();
            if (banks.Count() == 0)
            {
                requestFormSendButton.Enabled = false;
                requestBankBox.Items.Add("No blood banks.");
            }
            else
            {
                requestFormSendButton.Enabled = true;
                

                foreach (BloodBank bank in banks)
                {
                    requestBankBox.Items.Add(bank.BankID + ": " + bank.Name);
                }
            }
            requestBankBox.SelectedIndex = 0;

        }

        private void centerDonationsButton_Click(object sender, EventArgs e)
        {
            this.centerRequestsPanel.Visible = false;
            this.centerStockPanel.Visible = false;
            this.centerDonationsPanel.Visible = true;
            List<BloodDonationHistory> donations = context.BloodDonationHistoryController.ReadAll().ToList();
            Dictionary<String, bool> columns = new Dictionary<string, bool>();
            columns.Add("DonationID", false);
            columns.Add("DonorCNP", true);
            columns.Add("DonatedForCNP", true);
            columns.Add("Date", true);
            columns.Add("Quantity", true);
            populateGridView("Donations", columns, donations, centerDonationsDGV);
        }

        private void centerStockButton_Click(object sender, EventArgs e)
        {
            this.centerRequestsPanel.Visible = false;
            this.centerStockPanel.Visible = true;
            this.centerDonationsPanel.Visible = false;
            populateStockGridView(centerStockDGV);
        }

        private void centerRequestsButton_Click(object sender, EventArgs e)
        {
            this.centerRequestsPanel.Visible = true;
            this.centerStockPanel.Visible = false;
            this.centerDonationsPanel.Visible = false;
            populateCenterRequestDGV();
        }

        private void contactFormButton_Click(object sender, EventArgs e)
        {

            String description = this.messageNotification.Text;
            String cnp = (string) this.comboBoxNotificationAdmin.SelectedItem;
            cnp = cnp.Split('-')[0];
            String status = "Not Read";

            Notification notification = new Notification(Guid.NewGuid(), cnp, description, status );
            context.NotificationController.Insert(notification);

            DialogResult mb = MessageBox.Show("Your notification has been sent.", "Successful!", MessageBoxButtons.OK);
            this.contactFormPanel.Visible = false;
            this.centerPanel.Visible = true;
        }

        private void contactDonorButton_Click(object sender, EventArgs e)
        {
            this.centerPanel.Visible = false;
            this.contactFormPanel.Visible = true;

            IEnumerable<Donor> donors = context.DonorController.ReadAll();
            this.comboBoxNotificationAdmin.Items.Clear();
            foreach (Donor donor in donors)
            {
                string s = donor.CNP + "-" + donor.FirstName + " " + donor.LastName;
                this.comboBoxNotificationAdmin.Items.Add(s);
            }

            this.comboBoxNotificationAdmin.SelectedIndex = 0;
        }

        private void cancelContactButton_Click(object sender, EventArgs e)
        {
            this.contactFormPanel.Visible = false;
            this.centerPanel.Visible = true;
        }

        private void centerLogoutLabel_Click(object sender, EventArgs e)
        {
            this.centerPanel.Visible = false;
            this.loginPanel.Visible = true;
        }

        private void passwordBox_Click(object sender, EventArgs e)
        {
            passwordBox.Text = "";
        }

        private void usernameBox_Click(object sender, EventArgs e)
        {
            usernameBox.Text = "";
        }

        private void registerResidenceCountryBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(registerResidenceCountryBox.SelectedIndex != 0)
            {
                string country = registerResidenceCountryBox.SelectedItem.ToString();
                Guid countryID = context.CountryController.GetCountryByName(country).CountryID;

                IEnumerable<City> cities = context.CityController.GetCitiesByCountry(countryID);
                registerResidenceCityBox.Items.Clear();

                if (cities.Count() == 0)
                {
                    registerResidenceCityBox.Items.Add("No cities for specified country.");
                }
                else
                {
                    foreach (City city in cities)
                    {
                        registerResidenceCityBox.Items.Add(city.Name);
                    }
                }

                registerResidenceCityBox.SelectedIndex = 0;
            }
        }

        private void registerAddressCountryBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (registerAddressCountryBox.SelectedIndex != 0)
            {
                string country = registerAddressCountryBox.SelectedItem.ToString();
                Guid countryID = context.CountryController.GetCountryByName(country).CountryID;

                IEnumerable<City> cities = context.CityController.GetCitiesByCountry(countryID);
                registerAddressCityBox.Items.Clear();

                if (cities.Count() == 0)
                {
                    registerAddressCityBox.Items.Add("No cities for specified country.");
                }
                else
                {
                    foreach (City city in cities)
                    {
                        registerAddressCityBox.Items.Add(city.Name);
                    }
                }

                registerAddressCityBox.SelectedIndex = 0;
            }
        }

        private void PopulateDayBox()
        {
            try
            {
                int year = Convert.ToInt16(registerYearBox.SelectedItem.ToString());
                int month = Convert.ToInt16(registerMonthBox.SelectedItem.ToString());
                int maxDay = DateTime.DaysInMonth(year, month);
                int i = 1;

                registerDayBox.Items.Clear();
                while (i <= maxDay)
                {
                    registerDayBox.Items.Add(i.ToString());
                    i++;
                }

                registerDayBox.SelectedIndex = 0;
            }
            catch
            {
                MessageBox.Show("hi");
            }
        }

        private void registerMonthBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateDayBox();
        }

        private void registerYearBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateDayBox();
        }

        private void registerFormCancelButton_Click(object sender, EventArgs e)
        {
            registrationFormPanel.Visible = false;
            loginPanel.Visible = true;
        }

        private void registrationFormCancelButton_Click(object sender, EventArgs e)
        {
            registrationFormPanel.Visible = false;
            loginPanel.Visible = true;
        }

        private void donationsToTestDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            doctorPanel.Visible = false;
            doctorSendResults.Visible = true;

            this.globalBloodDonation = new BloodDonationHistory(dgv.Rows[dgv.CurrentCell.RowIndex].Cells["DonorCNP"].FormattedValue.ToString(),
                                                                dgv.Rows[dgv.CurrentCell.RowIndex].Cells["DonatedForCNP"].FormattedValue.ToString(),
                                                                new Guid(dgv.Rows[dgv.CurrentCell.RowIndex].Cells["DonationID"].FormattedValue.ToString()));
            textBox1.BackColor = Color.White;
            textBox1.Text = "";

            foreach(int i in diseasesCheckBox.CheckedIndices)
            {
                diseasesCheckBox.SetItemCheckState(i, CheckState.Unchecked);
            }
        }

        private void diseasesCheckBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(diseasesCheckBox.CheckedItems.Count != 0)
            {
                button2.Text = "Send Results";
            }
            else
            {
                button2.Text = "Next";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.BackColor = Color.IndianRed;
            }
            else
            {
                int quantity = 0;
                if (diseasesCheckBox.CheckedItems.Count != 0)
                {
                    bool result = int.TryParse(textBox1.Text,out quantity);
                    if(result == true)
                    {
                        foreach(String disease in diseasesCheckBox.CheckedItems)
                        {
                            DiseasesResult disResult = new DiseasesResult(diseasesDict[disease],this.globalBloodDonation.DonationID);
                            this.context.DiseaseResultController.Insert(disResult);
                        }

                        this.globalBloodDonation.Date = DateTime.Now;
                        this.globalBloodDonation.Quantity = quantity;

                        this.context.BloodDonationHistoryController.Update(this.globalBloodDonation);

                        Notification notification = new Notification(Guid.NewGuid(), this.globalBloodDonation.DonorCNP, "Tests are completed.They were positive for some diseases!", "Not Read");
                        context.NotificationController.Insert(notification);

                        PopulateDonations();

                        doctorSendResults.Visible = false;
                        doctorPanel.Visible = true;
                    }
                    else
                    {
                        textBox1.BackColor = Color.IndianRed;
                    }
                }
                else
                {
                    bool result = int.TryParse(textBox1.Text, out quantity);
                    if (result == true)
                    {
                        globalQuantity = quantity;
                        textBox2.BackColor = Color.White;
                        textBox3.BackColor = Color.White;
                        textBox4.BackColor = Color.White;
                        doctorSendResults.Visible = false;
                        doctorAddQuantity.Visible = true;
                    }
                    else
                    {
                        textBox1.BackColor = Color.IndianRed;
                    }
                }
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            doctorSendResults.Visible = false;
            doctorPanel.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            doctorSendResults.Visible = true;
            doctorAddQuantity.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.BackColor = Color.White;
            textBox3.BackColor = Color.White;
            textBox4.BackColor = Color.White;
            bool good = true;
            if (textBox2.Text == "")
            {
                good = false;
                textBox2.BackColor = Color.IndianRed;
            }
            if (textBox3.Text == "")
            {
                good = false;
                textBox3.BackColor = Color.IndianRed;
            }
            if (textBox4.Text == "")
            {
                good = false;
                textBox4.BackColor = Color.IndianRed;
            }
            if(good == true)
            {
                this.globalBloodDonation.Date = DateTime.Now;
                this.globalBloodDonation.Quantity = globalQuantity;

                this.context.BloodDonationHistoryController.Update(this.globalBloodDonation);

                Donor donor = this.context.DonorController.ReadByID(this.globalBloodDonation.DonorCNP);

                IEnumerable<BloodBank> banks = context.BloodBankController.ReadAll();
                requestBankBox.Items.Clear();
                if (banks.Count() == 0)
                {
                    requestFormSendButton.Enabled = false;
                    requestBankBox.Items.Add("No blood banks.");
                }
                else
                {
                    requestFormSendButton.Enabled = true;
                    foreach (BloodBank bank in banks)
                    {
                        requestBankBox.Items.Add(bank.BankID + ": " + bank.Name);
                    }
                }
                requestBankBox.SelectedIndex = 0;

                if (requestBankBox.SelectedItem.ToString().Length < 32)
                    requestBankBox.BackColor = Color.IndianRed;
                else
                {
                    Guid bloodBankID = new Guid(requestBankBox.SelectedItem.ToString().Split(':')[0]);

                    bool isValid = true;

                    int trombociteQuantity = 0;
                    int redBloodCellQuantity = 0;
                    int plasmaQuantity = 0;

                    bool result = int.TryParse(textBox2.Text, out trombociteQuantity);
                    if (result == false)
                    {
                        isValid = false;
                        textBox2.BackColor = Color.IndianRed;
                    }

                    result = int.TryParse(textBox3.Text, out redBloodCellQuantity);
                    if (result == false)
                    {
                        isValid = false;
                        textBox3.BackColor = Color.IndianRed;
                    }

                    result = int.TryParse(textBox4.Text, out plasmaQuantity);
                    if (result == false)
                    {
                        isValid = false;
                        textBox4.BackColor = Color.IndianRed;
                    }

                    if(isValid == true)
                    {
                        DateTime trombociteExpiration = DateTime.Now.AddDays(5);
                        DateTime redBloodCellExpiration = DateTime.Now.AddDays(42);
                        DateTime plasmaExpiration = DateTime.Now.AddMonths(12);

                        Trombocite trombocite = new Trombocite(Guid.NewGuid(),bloodBankID, donor.BloodTypeID.GetValueOrDefault(), trombociteExpiration,trombociteQuantity);
                        RedBloodCell redBloodCell = new RedBloodCell(Guid.NewGuid(), bloodBankID, donor.BloodTypeID.GetValueOrDefault(), redBloodCellExpiration, redBloodCellQuantity);
                        Plasma plasma = new Plasma(Guid.NewGuid(), bloodBankID, donor.BloodTypeID.GetValueOrDefault(), plasmaExpiration, plasmaQuantity);

                        this.context.TrombocitesController.Insert(trombocite);
                        this.context.RedBloodCellController.Insert(redBloodCell);
                        this.context.PlasmaController.Insert(plasma);

                        PopulateDonations();

                        Notification notification = new Notification(Guid.NewGuid(), donor.CNP, "Tests are completed. You are healthy and your blood will be used to save a life!", "Not Read");
                        context.NotificationController.Insert(notification);

                        doctorSendResults.Visible = false;
                        doctorPanel.Visible = true;
                    }
                }

            }
        }

        private void donationsToTestDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DeleteFromStock(Request r)
        {
            Guid bloodType = context.PatientController.ReadByID(r.PatientCNP).BloodTypeID.GetValueOrDefault();
            int redCellsQ = r.RedBloodCellsQuantity.GetValueOrDefault();
            int plasmaQ = r.PlasmaQuantity.GetValueOrDefault();
            int trombocitesQ = r.TrombocitesQuantity.GetValueOrDefault();

            List<Trombocite> trombocites = context.TrombocitesController.ReadAll().ToList();
            trombocites.Sort((t1, t2) => (t1.ExpirationDate < t2.ExpirationDate) ? 1 : 0);
            
            while(trombocitesQ > 0)
            {
                foreach(Trombocite t in trombocites)
                {
                    if(t.BloodTypeID == bloodType)
                    {
                        if(t.Quantity <= trombocitesQ)
                        {
                            trombocitesQ = trombocitesQ - t.Quantity.GetValueOrDefault();
                            context.TrombocitesController.Delete(t.TrombocitesID);
                        }
                        else
                        {
                            t.Quantity = t.Quantity - trombocitesQ;
                            context.TrombocitesController.Update(t);
                            trombocitesQ = 0;
                        }
                    }
                }
            }

            List<RedBloodCell> redCells = context.RedBloodCellController.ReadAll().ToList();
            redCells.Sort((t1, t2) => (t1.ExpirationDate < t2.ExpirationDate) ? 1 : 0);

            while (redCellsQ > 0)
            {
                foreach (RedBloodCell t in redCells)
                {
                    if (t.BloodTypeID == bloodType)
                    {
                        if (t.Quantity <= redCellsQ)
                        {
                            redCellsQ = redCellsQ - t.Quantity.GetValueOrDefault();
                            context.RedBloodCellController.Delete(t.RedBloodCellsID);
                        }
                        else
                        {
                            t.Quantity = t.Quantity - redCellsQ;
                            context.RedBloodCellController.Update(t);
                            redCellsQ = 0;
                        }
                    }
                }
            }

            List<Plasma> plasma = context.PlasmaController.ReadAll().ToList();
            plasma.Sort((t1, t2) => (t1.ExpirationDate < t2.ExpirationDate) ? 1 : 0);

            while (plasmaQ > 0)
            {
                foreach (Plasma t in plasma)
                {
                    if (t.BloodTypeID == bloodType)
                    {
                        if (t.Quantity <= plasmaQ)
                        {
                            plasmaQ = plasmaQ - t.Quantity.GetValueOrDefault();
                            context.PlasmaController.Delete(t.PlasmaID);
                        }
                        else
                        {
                            t.Quantity = t.Quantity - plasmaQ;
                            context.PlasmaController.Update(t);
                            plasmaQ = 0;
                        }
                    }
                }
            }
        }

        private void centerRequestsDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (centerRequestsDGV.SelectedCells.Count > 0)
            {
                int index = centerRequestsDGV.SelectedCells[0].RowIndex;
                if (centerRequestsDGV.Rows[index].Cells[6].Value.ToString() == "Yes")
                {
                    DialogResult answer = MessageBox.Show("Are you sure you want to send this blood?", "Confirm", MessageBoxButtons.YesNo);
                    if (answer == DialogResult.Yes)
                    {
                        string patientCNP = centerRequestsDGV.Rows[index].Cells[0].Value.ToString();
                        Request request = context.RequestController.ReadByID(patientCNP);
                        DeleteFromStock(request);
                        request.IsCompleted = true;
                        context.RequestController.Update(request);
                        populateCenterRequestDGV();
                    }
                }
                else
                {
                    MessageBox.Show("Not enough blood.", "Error");
                }

            }
        }

        private void countryAddressEdit_SelectedIndexChanged(object sender, EventArgs e)
        {

            string country = countryAddressEdit.SelectedItem.ToString();
            Guid countryID = context.CountryController.GetCountryByName(country).CountryID;

            IEnumerable<City> cities = context.CityController.GetCitiesByCountry(countryID);
            cityAddressEdit.Items.Clear();

            if (cities.Count() == 0)
            {
                this.cityAddressEdit.Items.Add("No cities for specified country.");
            }
            else
            {
                foreach (City city in cities)
                {
                    this.cityAddressEdit.Items.Add(city.Name);
                }
            }


            this.cityAddressEdit.SelectedIndex = 0;


        }

        private void countryResidentEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            string country = countryResidentEdit.SelectedItem.ToString();
            Guid countryID = context.CountryController.GetCountryByName(country).CountryID;

            IEnumerable<City> cities = context.CityController.GetCitiesByCountry(countryID);
            cityResidentEdit.Items.Clear();

            if (cities.Count() == 0)
            {
                this.cityResidentEdit.Items.Add("No cities for specified country.");
            }
            else
            {
                foreach (City city in cities)
                {
                    this.cityResidentEdit.Items.Add(city.Name);
                }
            }


            this.cityResidentEdit.SelectedIndex = 0;

        }
    }
}
