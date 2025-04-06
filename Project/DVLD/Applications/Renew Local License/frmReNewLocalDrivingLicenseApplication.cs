using DevExpress.Internal;
using DevExpress.Utils.About;
using DVLD.Classes;
using DVLD.Controls;
using DVLD_Buisness;
using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using DVLD.Licenses;
using DVLD.Licenses.Local_Licenses;

namespace DVLD.Applications.Renew_Local_License
{
    public partial class frmReNewLocalDrivingLicenseApplication : Form
    {
        private int _LicenseID;
        private clsApplication _NewApplication;
       
        private clsLicense _newLicense;
        public frmReNewLocalDrivingLicenseApplication()
        {
            InitializeComponent();
        }

        private void _FillDefaultData()
        {
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = lblApplicationDate.Text;
            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.RenewDrivingLicense).Fees.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            
        }

        private void _SetData()
        {
            float LicenseFees = clsLicenseClass.Find(clsLicense.Find(_LicenseID).LicenseClass)
                .ClassFees;
            float ApplicationFees = clsApplicationType.Find(2).Fees;
            lblOldLicenseID.Text=_LicenseID.ToString();
            lblExpirationDate.Text = clsFormat.DateToShort(clsLicense.Find(_LicenseID).ExpirationDate);
            lblLicenseFees.Text = LicenseFees.ToString();
            lblTotalFees.Text = (LicenseFees + ApplicationFees).ToString();
            
        }
        private bool _CreateNewApplication()
        {
            _NewApplication = new clsApplication();
            _NewApplication.ApplicantPersonID = clsLocalDrivingLicenseApplication.FindByApplicationID(clsLicense.Find(_LicenseID).ApplicationID).ApplicantPersonID;
            _NewApplication.ApplicationDate = DateTime.Now;
            _NewApplication.ApplicationStatus = clsApplication.enApplicationStatus.New;
            _NewApplication.ApplicationTypeID = (int)clsApplication.enApplicationType.RenewDrivingLicense;
            _NewApplication.LastStatusDate = DateTime.Now;
            _NewApplication.PaidFees = clsApplicationType.Find(6).Fees;
            _NewApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            return _NewApplication.Save();

        }

        private bool _CreateNewLicence()
        {
            clsLicense oldLicense=clsLicense.Find(_LicenseID);
            _newLicense=new clsLicense();
            _newLicense.LicenseClass = oldLicense.LicenseClass;
            _newLicense.ApplicationID = oldLicense.ApplicationID;
            _newLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _newLicense.DriverID = clsLicense.Find(_LicenseID).DriverID;
            _newLicense.IssueDate = DateTime.Today;
            _newLicense.ExpirationDate = DateTime.Today.AddYears(clsLicenseClass.Find(oldLicense.LicenseClass).DefaultValidityLength);
            _newLicense.IsActive = true;
            _newLicense.Notes = txtNotes.Text.Trim();
            _newLicense.PaidFees = (decimal)Convert.ToSingle(lblTotalFees.Text);
            _newLicense.IssueReason = 2;//1-FirstTime, 2-Renew, 3-Replacement for Damaged, 4- Replacement for Lost.
            return (_newLicense.Save());
        }
        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _LicenseID = obj;
            _SetData();
            if (!clsLicense.Find(_LicenseID).IsActive)
            {
                MessageBox.Show("The license you selected is inactive. Please select an active license to continue.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            if (clsLicense.Find(_LicenseID).ExpirationDate >
               DateTime.Today)
            {
                MessageBox.Show("Selected License is not yet expiared, it will expire on:"+ clsLicense.Find(_LicenseID).ExpirationDate, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;


            }

           
            llShowLicenseHistory.Enabled = true;
            btnRenewLicense.Enabled=true;

        }

        private void frmReNewLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _FillDefaultData();
        }

        private void ctrlDriverLicenseInfoWithFilter1_Load(object sender, EventArgs e)
        {

        }

        private void btnRenewLicense_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure  want to Renew the license?","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Information)==DialogResult.No)
            {
                return;
            }
            if (!_CreateNewApplication())
            {
                MessageBox.Show("Error While Creating A New Application", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           
            if (!_CreateNewLicence())
            {
                MessageBox.Show("Error While Creating A New Licence", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            clsLicense OldLicense = clsLicense.Find(_LicenseID);
            OldLicense.IsActive = false;
            if (!OldLicense.Save())
            {
                MessageBox.Show("Error While Upadting Old Licence Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("License Renewed Successfully with ID = " +_newLicense.LicenseID, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lblRenewedLicenseID.Text = _newLicense.LicenseID.ToString();
            lblApplicationID.Text = _newLicense.ApplicationID.ToString();
            
            btnRenewLicense.Enabled= false;
            llShowLicenseInfo.Enabled= true;
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDriverLicenseHistory frm = new frmDriverLicenseHistory(clsDriver.Find(_newLicense.DriverID).PersonID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo((int)_newLicense.LicenseID);
            frm.ShowDialog();
        }
    }
}
