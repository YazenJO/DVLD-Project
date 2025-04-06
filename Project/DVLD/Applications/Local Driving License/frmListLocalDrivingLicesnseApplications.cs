using DVLD.Applications;
using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using DVLD_Buisness;
using System.Security.Cryptography;
using DVLD_Business;
using DVLD.Controls.ApplicationControls;
using DVLD.Licenses;
using DVLD.Licenses.Local_Licenses;
using DVLD.Tests.Schdule_Test;
using static DVLD_Buisness.clsApplication;

namespace DVLD.Tests
{
    public partial class frmListLocalDrivingLicesnseApplications : Form
    {
        private DataTable _dtAllLocalDrivingLicenseApplications;
        public frmListLocalDrivingLicesnseApplications()
        {
            InitializeComponent();
        }

        private void frmListLocalDrivingLicesnseApplications_Load(object sender, EventArgs e)
        {
            _dtAllLocalDrivingLicenseApplications = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            dgvLocalDrivingLicenseApplications.DataSource = _dtAllLocalDrivingLicenseApplications;
            
            lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
            if (dgvLocalDrivingLicenseApplications.Rows.Count>0)
            {

                dgvLocalDrivingLicenseApplications.Columns[0].HeaderText = "L.D.L.AppID";
                dgvLocalDrivingLicenseApplications.Columns[0].Width = 120;

                dgvLocalDrivingLicenseApplications.Columns[1].HeaderText = "Driving Class";
                dgvLocalDrivingLicenseApplications.Columns[1].Width = 300;

                dgvLocalDrivingLicenseApplications.Columns[2].HeaderText = "National No.";
                dgvLocalDrivingLicenseApplications.Columns[2].Width = 150;

                dgvLocalDrivingLicenseApplications.Columns[3].HeaderText = "Full Name";
                dgvLocalDrivingLicenseApplications.Columns[3].Width = 350;

                dgvLocalDrivingLicenseApplications.Columns[4].HeaderText = "Application Date";
                dgvLocalDrivingLicenseApplications.Columns[4].Width = 170;

                dgvLocalDrivingLicenseApplications.Columns[5].HeaderText = "Passed Tests";
                dgvLocalDrivingLicenseApplications.Columns[5].Width = 150;
            }

            cbFilterBy.SelectedIndex = 0;


        }


        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }

            _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = "";
            lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {

            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {

                case "L.D.L.AppID":
                    FilterColumn = "L.D.LAppID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "Status":
                    FilterColumn = "Application Status";
                    break;


                default:
                    FilterColumn = "None";
                    break;

            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
                return;
            }


            if (FilterColumn == "L.D.LAppID")
                //in this case we deal with integer not string.
                
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter =
                    string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            
            else
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
        }


        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow number incase L.D.L.AppID id is selected.
            if (cbFilterBy.Text == "L.D.L.AppID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }




      



        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicesnseApplication frm = new frmAddUpdateLocalDrivingLicesnseApplication();
            frm.ShowDialog();
            //refresh
            frmListLocalDrivingLicesnseApplications_Load(null, null);
        }

        private void scheToolStripMenuItem_Click(object sender, EventArgs e)
        {
          frmTestAppointments appointments= new frmTestAppointments((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value,clsTestType.enTestType.VisionTest);
          appointments.ShowDialog();
          frmListLocalDrivingLicesnseApplications_Load(null, null);
        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            frmLocalDrivingLicenseApplicationInfo frm= new frmLocalDrivingLicenseApplicationInfo((int)dgvLocalDrivingLicenseApplications.CurrentRow
                .Cells[0].Value);
            frm.ShowDialog();
        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicesnseApplication frm = new frmAddUpdateLocalDrivingLicesnseApplication((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are U Sure Wanna To Delete This Rcord","Delete Confirmation",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.No)
            {
                return;
            }
            clsLocalDrivingLicenseApplication Application =
                clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID((int)dgvLocalDrivingLicenseApplications.CurrentRow
                    .Cells[0].Value);
            ;
            if (Application.Delete())
            {
                MessageBox.Show("Deleted Successfully");
                //refresh
                frmListLocalDrivingLicesnseApplications_Load(null, null);
            }
            else
            {
                MessageBox.Show(
                    "Unable to delete this record because it is linked to other data. Please remove related records first or check dependencies before trying again.",
                    "Cant Delete This Record !", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void caancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are U Sure Wanna To Cancel This Rcord", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            clsLocalDrivingLicenseApplication Application =
                clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID((int)dgvLocalDrivingLicenseApplications.CurrentRow
                    .Cells[0].Value);
            ;
            if (Application.Cancel())
            {
                MessageBox.Show("Canceled Successfully");
                //refresh
                frmListLocalDrivingLicesnseApplications_Load(null, null);
            }
        }

        private void _HandleTestRecordsInMenu()
        {
            var applicationId = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            var application = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(applicationId);
            int passedTests = application.GetPassedTestCount();

            // Mapping test statuses to enabled/disabled states
            var testStates = new Dictionary<int, (bool issueLicense, bool showLicense, bool showHistory, bool scheduleVision, bool scheduleWritten, bool scheduleStreet)>
            {
                { 0, (false, false, false, true, false, false) },
                { 1, (false, false, false, false, true, false) },
                { 2, (false, false, false, false, false, true) },
                { 3, (true, true, true, false, false, false) }
            };

            if (testStates.TryGetValue(passedTests, out var states))
            {
                issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = states.issueLicense;
                showLicenseToolStripMenuItem.Enabled = states.showLicense;
                showPersonLicenseHistoryToolStripMenuItem.Enabled = states.showHistory;
                scheToolStripMenuItem.Enabled = states.scheduleVision;
                schduleWrittenTestToolStripMenuItem.Enabled = states.scheduleWritten;
                schduleStreetTestToolStripMenuItem.Enabled = states.scheduleStreet;
            }

            var testApplicationStatus = new Dictionary<clsApplication.enApplicationStatus, (bool showLicense, bool showPersonLicenseHistory,bool ScheduleTest)>
            {
                { clsApplication.enApplicationStatus.Completed, (true,  true,false) },
                {  clsApplication.enApplicationStatus.New, (false, true,true) },
                { clsApplication.enApplicationStatus.Cancelled, (false,  true ,false) },
               
            };
            if (testApplicationStatus.TryGetValue(application.ApplicationStatus, out var Appstates))
            {
                showLicenseToolStripMenuItem.Enabled = Appstates.showLicense;
                
                schduleTestsToolStripMenuItem.Enabled = Appstates.ScheduleTest;
                showPersonLicenseHistoryToolStripMenuItem.Enabled = Appstates.showPersonLicenseHistory;
            }

            if (clsLicense.FindByAppId(application.ApplicationID) != null)
            {
                issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
            }
          

           



        }

        private void CheckApplicationStatus()
        {
            var applicationId = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            var applicationStatus = clsApplication.FindBaseApplication(clsLocalDrivingLicenseApplication
                .FindByLocalDrivingAppLicenseID(applicationId).ApplicationID).ApplicationStatus;

            bool isEditable = applicationStatus == clsApplication.enApplicationStatus.New;

            editApplicationToolStripMenuItem.Enabled = isEditable;
            deleteApplicationToolStripMenuItem.Enabled = isEditable;
            caancelApplicationToolStripMenuItem.Enabled = isEditable;
            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = isEditable;
            schduleTestsToolStripMenuItem.Enabled = isEditable;

            _HandleTestRecordsInMenu();
        }

        private void LDLApplicationsMenuStrip_Opened(object sender, EventArgs e)
        {
            CheckApplicationStatus();
        }

        private void schduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTestAppointments appointments = new frmTestAppointments((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value, clsTestType.enTestType.WrittenTest);
            appointments.ShowDialog();
            frmListLocalDrivingLicesnseApplications_Load(null, null);
        }

        private void schduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTestAppointments appointments = new frmTestAppointments((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value, clsTestType.enTestType.StreetTest);
            appointments.ShowDialog();
            frmListLocalDrivingLicesnseApplications_Load(null, null);
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowLicenseInfo frm= new frmShowLicenseInfo((int)clsLicense.FindByAppId(clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value).ApplicationID).LicenseID);
            frm.ShowDialog();
        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIssueDriverLicenseForTheFirstTime frm = new frmIssueDriverLicenseForTheFirstTime(
                (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);

            frm.ShowDialog();
            frmListLocalDrivingLicesnseApplications_Load(null, null);

        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDriverLicenseHistory frm= new frmDriverLicenseHistory(clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value).ApplicantPersonID);
            frm.ShowDialog();
        }
    }
}
