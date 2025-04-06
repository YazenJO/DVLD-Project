using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.Controls.ApplicationControls;
using DVLD_Buisness;
using DVLD_Business;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD.Tests.Schdule_Test
{
    public partial class frmTestAppointments : Form
    {
        private int _LDlAppID;
        private clsTestType.enTestType _testType;

        public frmTestAppointments(int LDLAppID,clsTestType.enTestType TestType)
        {
            InitializeComponent();
           _LDlAppID = LDLAppID ;
            _testType = TestType;

        }

        void _FrmDesignByTestType()
        {
            lblRecord.Text = drgAppointments.RowCount.ToString();
            switch (_testType)
            {
                case clsTestType.enTestType.VisionTest:
                    this.Text = "Vision Test Appointmetns";
                    lblTitle.Text = "Vision Test Appointmetns";
                    picTest.Image = Properties.Resources.Vision_512;
                    break;
                case clsTestType.enTestType.WrittenTest:
                    this.Text = "Written Test Appointmetns";
                    lblTitle.Text = "Written Test Appointmetns";
                    picTest.Image = Properties.Resources.Written_Test_512;
                    break;
                case clsTestType.enTestType.StreetTest:
                    this.Text = "Street Test Appointmetns";
                    lblTitle.Text = "Street Test Appointmetns";
                    picTest.Image = Properties.Resources.driving_test_512;
                    break;

            }
            _HandleTestTypeChange();
        }
        private void Schdule_Test_Load(object sender, EventArgs e)
        {
           
        }

        private void frmTestAppointments_Load(object sender, EventArgs e)
        {
            ctrlDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(_LDlAppID);
            drgAppointments.DataSource = clsTestAppointment.GetTestAppointmentByLDLID(_LDlAppID);
            _FrmDesignByTestType();
     

        }

        void _HandleTestTypeChange()
        {
            switch (clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LDlAppID).GetPassedTestCount())
            {
                case 0:
                    _testType = clsTestType.enTestType.VisionTest;
                    break;
                case 1:
                    _testType = clsTestType.enTestType.WrittenTest;
                    break;
                case 2:
                    _testType = clsTestType.enTestType.StreetTest;
                    break;
                default:
                    btnAddNewAppointment.Enabled = false;
                    break;


            }
        }
        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {
           
            if (clsTestAppointment.DoesUserHasActiveTestAppointment(_LDlAppID,_testType))
            {
                MessageBox.Show(
                    "Person Already have an active appointment for this test, You\ncannot add new appointment",
                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            frmScheduleTest frm = new frmScheduleTest(_LDlAppID);
            frm.ShowDialog();
            frmTestAppointments_Load(null,null);
           
        }

        private void contextMenuStrip1_Opened(object sender, EventArgs e)
        {
            if (clsTestAppointment.Find((int)drgAppointments.CurrentRow.Cells[0].Value).IsLocked)
            {
                editToolStripMenuItem.Enabled=false;
                takeTestToolStripMenuItem.Enabled=false;
            }
            else
            {
                editToolStripMenuItem.Enabled = true;
                takeTestToolStripMenuItem.Enabled = true;
            }
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTakeTest frm = new frmTakeTest((int)drgAppointments.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmTestAppointments_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmScheduleTest frm = new frmScheduleTest(_LDlAppID, (int)drgAppointments.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmTestAppointments_Load(null, null);
        }
    }
}
