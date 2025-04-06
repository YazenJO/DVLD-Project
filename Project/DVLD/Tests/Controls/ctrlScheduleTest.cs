using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.Filtering;
using DVLD_Buisness;
using DVLD_Business;
using DVLD.Classes;

namespace DVLD.Tests.Controls
{
    public partial class ctrlScheduleTest : UserControl
    {
        private enum enMode
        {
            AddNew=1,
            Update=2
        }
        private enMode _Mode;
        private enum enTestAttemptType
        {
            FirstTime=1,
            Retake=2
        }

        private float TotalFeeds = 0;
        private enTestAttemptType _TestAttemptType=enTestAttemptType.FirstTime;
        private clsTestType.enTestType _testType=clsTestType.enTestType.VisionTest;
        private clsLocalDrivingLicenseApplication _ldlApplication;

        private clsTestAppointment _testAppointment;

        public ctrlScheduleTest()
        {
            InitializeComponent();
        }

        private void _LabelsValue()
        {
          
            lblLocalDrivingLicenseAppID.Text = _ldlApplication.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = clsLicenseClass.Find(_ldlApplication.LicenseClassID).ClassName;
            dtpTestDate.Value = DateTime.Today;
           
            lblRetakeAppFees.Text = clsApplicationType.Find(8).Fees.ToString();
            gbRetakeTestInfo.Enabled = false;
            lblUserMessage.Visible = false;
        }
        private void _FillData()
        {
            if (_Mode == enMode.Update || _TestAttemptType == enTestAttemptType.Retake)
            {
                lblTrial.Text = _ldlApplication.TotalTrialsPerTest(_testType).ToString();
                dtpTestDate.Value = _testAppointment.AppointmentDate;
            }

            if (_TestAttemptType != enTestAttemptType.Retake)
            {
                lblTotalFees.Text = TotalFeeds.ToString();
            }

            if (_testType == clsTestType.enTestType.WrittenTest && _ldlApplication.GetPassedTestCount() == 0)
            {
                DisplayUserMessage("Cannot Schedule, Vision Test Should be Passed First.");
            }
            else if (_testType == clsTestType.enTestType.StreetTest && _ldlApplication.GetPassedTestCount() == 1)
            {
                DisplayUserMessage("Cannot Schedule, Street Test Should be Passed First.");
            }

            if (_TestAttemptType == enTestAttemptType.Retake)
            {
                lblTitle.Text = "Schedule Retake Test";
                gbRetakeTestInfo.Enabled = true;
                TotalFeeds+= clsApplicationType.Find(8).Fees;
                lblTotalFees.Text = TotalFeeds.ToString();
            }

            UpdateTestTypeDisplay();
        }

        private void DisplayUserMessage(string message)
        {
            lblUserMessage.Visible = true;
            lblUserMessage.Text = message;
        }

        private void UpdateTestTypeDisplay()
        {
            switch (_testType)
            {
                case clsTestType.enTestType.VisionTest:
                    pbTestTypeImage.Image = Properties.Resources.Vision_512;
                    gbTestType.Text = "Schedule Vision Test";
                    break;
                case clsTestType.enTestType.WrittenTest:
                    pbTestTypeImage.Image = Properties.Resources.Written_Test_512;
                    gbTestType.Text = "Schedule Written Test";
                    break;
                case clsTestType.enTestType.StreetTest:
                    pbTestTypeImage.Image = Properties.Resources.driving_test_512;
                    gbTestType.Text = "Schedule Street Test";
                    break;
            }
        }

        private void _GetTestType()
        {
            int NumberOFPAsseddTests=_ldlApplication.GetPassedTestCount();
            switch (NumberOFPAsseddTests)
            {
                case 0:
                    _testType = clsTestType.enTestType.VisionTest;
                    pbTestTypeImage.Image= Properties.Resources.Vision_512;
                    break;
                case 1:
                    _testType= clsTestType.enTestType.WrittenTest;
                    pbTestTypeImage.Image = Properties.Resources.Written_Test_512;
                    break;
                case 2:
                    _testType = clsTestType.enTestType.StreetTest;
                    pbTestTypeImage.Image = Properties.Resources.Street_Test_32;
                    
                    break;

            }
            
        }

        private void _CheckTestAttemptType()
        {

            if (_ldlApplication.DoesAttendTestType(_testType))
                _TestAttemptType = enTestAttemptType.Retake;
            else
                _TestAttemptType = enTestAttemptType.FirstTime;
        }
        public void Load(int LDLAppID, int AppointmentID = -1)
        {

            _ldlApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LDLAppID);
            lblFullName.Text = _ldlApplication.PersonFullName;
            _LabelsValue();

            if (AppointmentID == -1)
            {
                InitializeNewAppointment();
                return;
            }
            else
            {
                LoadExistingAppointment(AppointmentID);
            }

            _CheckTestAttemptType();
            _GetTestType();
            _FillData();





        }
        private void InitializeNewAppointment()
        {
            _Mode = enMode.AddNew;
            _testAppointment = new clsTestAppointment();
            _GetTestType();
            _CheckTestAttemptType();
            lblTrial.Text = _ldlApplication.TotalTrialsPerTest(_testType).ToString();
            TotalFeeds = clsTestType.Find(_testType).Fees;
            lblFees.Text = TotalFeeds.ToString();
            _FillData();
        }

        private void LoadExistingAppointment(int AppointmentID)
        {
            _Mode = enMode.Update;
            _testAppointment = clsTestAppointment.Find(AppointmentID);
            TotalFeeds = clsTestType.Find(_testType).Fees;
            _testType = (clsTestType.enTestType)_testAppointment.TestTypeID;
        }


        private void gbTestType_Enter(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _testAppointment.LocalDrivingLicenseApplicationID = _ldlApplication.LocalDrivingLicenseApplicationID;
            _testAppointment.TestTypeID = (int)_testType;
            _testAppointment.AppointmentDate = dtpTestDate.Value;
            _testAppointment.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _testAppointment.IsLocked = false;
            _testAppointment.PaidFees = (decimal)TotalFeeds;
            if (_TestAttemptType==enTestAttemptType.Retake)
            {
                _testAppointment.RetakeTestApplicationID =_ldlApplication.GetLastTestAppointmentFailedTestType(_testType);
            }
            if (_testAppointment.Save())
            {
                lblRetakeTestAppID.Text = _testAppointment.RetakeTestApplicationID.ToString();
                MessageBox.Show("Data Saved Succesfully!");
               


            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.","Eror",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}
