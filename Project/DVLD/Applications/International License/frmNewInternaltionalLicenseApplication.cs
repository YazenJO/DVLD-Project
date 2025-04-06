using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Buisness;
using DVLD_Business;
using DVLD.Classes;
using DVLD.Licenses;
using DVLD.Licenses.Controls;
using DVLD.Licenses.International_Licenses;
using Microsoft.VisualBasic.ApplicationServices;

namespace DVLD.Applications.International_License
{
    public partial class frmNewInternaltionalLicenseApplication : Form
    {
        private int _InternationalLicenseID;
        private clsInternationalLicense _NewInternationalLicense;
        private int LicenseID;
        private clsApplication _NewApplication;
        public frmNewInternaltionalLicenseApplication()
        {
            InitializeComponent();
        }
       


        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowInternationalLicenseInfo frm =
                new frmShowInternationalLicenseInfo((int)_NewInternationalLicense.InternationalLicenseID);
            frm.ShowDialog();
        }

        private void _FillDefaultData()
        {
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = lblApplicationDate.Text;
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(1));//add one year.
            lblFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.NewInternationalLicense).Fees.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;



        }

        private void frmNewInternaltionalLicenseApplication_Load(object sender, EventArgs e)
        {
            _FillDefaultData();
        }

        private void ctrlDriverLicenseInfoWithFilter1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
             LicenseID = obj;
            lblLocalLicenseID.Text= clsLocalDrivingLicenseApplication.FindByApplicationID(clsLicense.Find(LicenseID).ApplicationID).LocalDrivingLicenseApplicationID.ToString();
            if (clsLicense.Find(LicenseID).LicenseClass !=
                clsLicenseClass.Find("Class 3 - Ordinary driving license").LicenseClassID)
            {
                MessageBox.Show("License Class Should Be [Class 3 - Ordinary driving license] Choose Another License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;


            }
            llShowLicenseHistory.Enabled= true;
            btnIssueLicense.Enabled = true;


        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDriverLicenseHistory frm = new frmDriverLicenseHistory(clsDriver.Find(_NewInternationalLicense.DriverID).PersonID);
            frm.ShowDialog();
        }

        private bool _CreateNewApplication()
        {
            _NewApplication = new clsApplication();
            _NewApplication.ApplicantPersonID = clsLocalDrivingLicenseApplication.FindByApplicationID(clsLicense.Find(LicenseID).ApplicationID).ApplicantPersonID;
            _NewApplication.ApplicationDate = DateTime.Now;
            _NewApplication.ApplicationStatus = clsApplication.enApplicationStatus.New;
            _NewApplication.ApplicationTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;
            _NewApplication.LastStatusDate = DateTime.Now;
            _NewApplication.PaidFees = clsApplicationType.Find(6).Fees;
            _NewApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            return _NewApplication.Save();
           
        }

        private bool _CreateNewInternationalLincense()
        {
            _NewInternationalLicense = new clsInternationalLicense();
            
            
            _NewInternationalLicense.ApplicationID = _NewApplication.ApplicationID;
            _NewInternationalLicense.DriverID = clsLicense.Find(LicenseID).DriverID;
            _NewInternationalLicense.IssuedUsingLocalLicenseID = (int)clsLicense.Find(LicenseID).LicenseID;
            _NewInternationalLicense.IssueDate = _NewApplication.ApplicationDate;
            _NewInternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);
            _NewInternationalLicense.IsActive = true;
            _NewInternationalLicense.CreatedByUserID = _NewApplication.CreatedByUserID;
            return _NewInternationalLicense.Save();

        }
        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            if (clsInternationalLicense.GetActiveInternationalLicenseIDByDriverID(clsLicense.Find(LicenseID).DriverID)!=null)
            {
                MessageBox.Show("Driver Already Have A Active International License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
          

            if (_CreateNewApplication())
            {
                if (_CreateNewInternationalLincense())
                {
                    MessageBox.Show("International License Issued Successfully with ID=" +
                                    _NewInternationalLicense.InternationalLicenseID);
                    lblApplicationID.Text= _NewInternationalLicense.ApplicationID.ToString();
                    lblInternationalLicenseID.Text = _NewInternationalLicense.InternationalLicenseID.ToString();
                    llShowLicenseInfo.Enabled = true;
                    btnIssueLicense.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Can't Make This Application", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           
            

        }
    }
}
