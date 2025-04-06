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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace DVLD.Applications.Rlease_Detained_License
{
    public partial class frmReleaseDetainedLicenseApplication : Form
    {
        private int _LicenseID;
        private clsApplication _NewApplication;
        private clsDetain Detain;
        public frmReleaseDetainedLicenseApplication()
        {
            InitializeComponent();
        }
        public frmReleaseDetainedLicenseApplication(int LicenseID)
        {
            InitializeComponent();
             _LicenseID= LicenseID ;
             ctrlDriverLicenseInfoWithFilter1.LoadByLicenseID(LicenseID);
            _SetData();
            llShowLicenseHistory.Enabled = true;
            btnRelease.Enabled = true;
        }
        private void _SetData()
        {
            Detain = clsDetain.FindByLicenseID(_LicenseID);
            decimal FineFees = Detain.FineFees;
            float ApplicationFees = clsApplicationType.Find(5).Fees;
            lblDetainID.Text = Detain.DetainID.ToString();
            lblLicenseID.Text = _LicenseID.ToString();
            lblDetainDate.Text = clsFormat.DateToShort(Detain.DetainDate);
            lblFineFees.Text = FineFees.ToString();
            lblTotalFees.Text = (FineFees + (decimal)ApplicationFees).ToString();
            lblCreatedByUser.Text = clsUser.FindByUserID(Detain.CreatedByUserID).UserName;
            lblApplicationFees.Text = ApplicationFees.ToString();

        }
        private bool _CreateNewApplication()
        {
            _NewApplication = new clsApplication();
            _NewApplication.ApplicantPersonID = clsLocalDrivingLicenseApplication.FindByApplicationID(clsLicense.Find(_LicenseID).ApplicationID).ApplicantPersonID;
            _NewApplication.ApplicationDate = DateTime.Now;
            _NewApplication.ApplicationStatus = clsApplication.enApplicationStatus.New;
            _NewApplication.ApplicationTypeID = (int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense;
            _NewApplication.LastStatusDate = DateTime.Now;
            _NewApplication.PaidFees = clsApplicationType.Find(5).Fees;
            _NewApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            return _NewApplication.Save();

        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {

            _LicenseID = obj;
            
            if (!clsDetain.DoesLicenseDetained(_LicenseID))
            {
                MessageBox.Show("The license you selected is Not Detained. Please select an Detained license to continue.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            _SetData();
            llShowLicenseHistory.Enabled = true;
            btnRelease.Enabled = true;
        }

        private bool _UpdateDetainData()
        {
            Detain.IsReleased = true;
            Detain.ReleaseDate = DateTime.Today;
            Detain.ReleasedByUserID=clsGlobal.CurrentUser.UserID;
            Detain.ReleaseApplicationID = _NewApplication.ApplicationID;
            return Detain.Save();
        }
        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure  want to Release the Detained license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                return;
            }
            if (!_CreateNewApplication())
            {
                MessageBox.Show("Error While Creating A New Application", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            


            
           
            if (!_UpdateDetainData())
            {
                MessageBox.Show("Error While Upadting Detain Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("License Released Successfully with ID = " + _NewApplication.ApplicationID, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lblApplicationID.Text = _NewApplication.ApplicationID.ToString();

            btnRelease.Enabled = false;
            llShowLicenseInfo.Enabled = true;
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDriverLicenseHistory frm = new frmDriverLicenseHistory(clsDriver.Find(clsLicense.Find(_LicenseID).DriverID).PersonID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo((int)_LicenseID);
            frm.ShowDialog();
        }
    }
}
