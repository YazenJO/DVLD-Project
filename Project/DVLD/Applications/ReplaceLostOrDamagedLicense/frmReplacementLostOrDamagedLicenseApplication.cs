using DVLD.Classes;
using DVLD.Licenses;
using DVLD.Licenses.Local_Licenses;
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

namespace DVLD.Applications.ReplaceLostOrDamagedLicense
{
    public partial class frmReplacementLostOrDamagedLicenseApplication : Form
    {
        private int _LicenseID;
        private clsApplication _NewApplication;

        private clsLicense _newLicense;
        public frmReplacementLostOrDamagedLicenseApplication()
        {
            InitializeComponent();
        }
        private void _SetData()
        {

            lblOldLicenseID.Text = _LicenseID.ToString();




        }
        private void _FillDefaultData()
        {
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.RenewDrivingLicense).Fees.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;

        }
        private bool _CreateNewApplication()
        {
            _NewApplication = new clsApplication();
            _NewApplication.ApplicantPersonID = clsLocalDrivingLicenseApplication.FindByApplicationID(clsLicense.Find(_LicenseID).ApplicationID).ApplicantPersonID;
            _NewApplication.ApplicationDate = DateTime.Now;
            _NewApplication.ApplicationStatus = clsApplication.enApplicationStatus.New;
            if (rbDamagedLicense.Checked)
            {
                _NewApplication.ApplicationTypeID = (int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense;
            }
            else
            {
                _NewApplication.ApplicationTypeID = (int)clsApplication.enApplicationType.ReplaceLostDrivingLicense;
            }
            
            _NewApplication.LastStatusDate = DateTime.Now;
            _NewApplication.PaidFees = clsApplicationType.Find(6).Fees;
            _NewApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            return _NewApplication.Save();

        }
        private bool _CreateNewLicence()
        {
            clsLicense oldLicense = clsLicense.Find(_LicenseID);
            _newLicense = new clsLicense();
            _newLicense.LicenseClass = oldLicense.LicenseClass;
            _newLicense.ApplicationID = oldLicense.ApplicationID;
            _newLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _newLicense.DriverID = clsLicense.Find(_LicenseID).DriverID;
            _newLicense.IssueDate = DateTime.Today;
            _newLicense.ExpirationDate = DateTime.Today.AddYears(clsLicenseClass.Find(oldLicense.LicenseClass).DefaultValidityLength);
            _newLicense.IsActive = true;
            _newLicense.Notes = "";
            //1-FirstTime, 2-Renew, 3-Replacement for Damaged, 4- Replacement for Lost.
            if (rbDamagedLicense.Checked)
            {
                _newLicense.IssueReason = 3;
            }
            else
            {
                _newLicense.IssueReason = 4;
            }
            _newLicense.PaidFees = (decimal)Convert.ToSingle(lblApplicationFees.Text);
                  
            return (_newLicense.Save());
        }
        private void ctrlDriverLicenseInfoWithFilter2_OnLicenseSelected(int obj)
        {
            _LicenseID = obj;
            _SetData();
            if (!clsLicense.Find(_LicenseID).IsActive)
            {
                MessageBox.Show("The license you selected is inactive. Please select an active license to continue.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            llShowLicenseHistory.Enabled = true;
            btnIssueReplacement.Enabled = true;

        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDamagedLicense.Checked)
            {
                lblTitle.Text = "License Replacement for Damaged License";
                lblApplicationFees.Text = clsApplicationType.Find(3).Fees.ToString();
            }
            else
            {
                lblTitle.Text = "License Replacement for Lost License";
                lblApplicationFees.Text = clsApplicationType.Find(4).Fees.ToString();
            }

        }

        private void frmReplacementLostOrDamagedLicenseApplication_Load(object sender, EventArgs e)
        {
            _FillDefaultData();
        }

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure  want to issue Replacement for the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
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
            MessageBox.Show("License Renewed Successfully with ID = " + _newLicense.LicenseID, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lblRreplacedLicenseID.Text = _newLicense.LicenseID.ToString();
            lblApplicationID.Text = _newLicense.ApplicationID.ToString();

            btnIssueReplacement.Enabled = false;
            llShowLicenseInfo.Enabled = true;
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo((int)_newLicense.LicenseID);
            frm.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDriverLicenseHistory frm = new frmDriverLicenseHistory(clsDriver.Find(_newLicense.DriverID).PersonID);
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
