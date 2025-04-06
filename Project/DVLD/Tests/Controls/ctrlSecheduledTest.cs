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
using static DVLD_Buisness.clsTestType;

namespace DVLD.Tests.Controls
{
    public partial class ctrlSecheduledTest : UserControl
    {
        
          private clsTestType.enTestType _testType = clsTestType.enTestType.VisionTest;
       
        private clsTestAppointment _testAppointment;

        public ctrlSecheduledTest()
        {
            InitializeComponent();
        }

        private void _FillData()
        {
            _testType = (clsTestType.enTestType)_testAppointment.TestTypeID;

            lblLocalDrivingLicenseAppID.Text = _testAppointment.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = clsLicenseClass.Find(clsLocalDrivingLicenseApplication
                .FindByLocalDrivingAppLicenseID(_testAppointment.LocalDrivingLicenseApplicationID)
                .LicenseClassID).ClassName;
            lblFullName.Text = clsLocalDrivingLicenseApplication
                .FindByLocalDrivingAppLicenseID(_testAppointment.LocalDrivingLicenseApplicationID).PersonFullName;

            lblTrial.Text = clsLocalDrivingLicenseApplication
                .FindByLocalDrivingAppLicenseID(_testAppointment.LocalDrivingLicenseApplicationID)
                .TotalTrialsPerTest(_testType).ToString();

                lblDate.Text = _testAppointment.AppointmentDate.ToString();
            lblFees.Text = _testAppointment.PaidFees.ToString();
            clsTest Test = clsTest.FindByTestAppointmentID((int)_testAppointment.TestAppointmentID);
            if (Test == null)
                lblTestID.Text = "Not Taken Yet";
            else

                lblTestID.Text = Test.TestID.ToString();

            switch (_testType)
            {
                case clsTestType.enTestType.VisionTest:
                    pbTestTypeImage.Image = Properties.Resources.Vision_512;
                    break;
                case clsTestType.enTestType.WrittenTest:
                    pbTestTypeImage.Image = Properties.Resources.Written_Test_512;
                    break;
                case clsTestType.enTestType.StreetTest:
                    pbTestTypeImage.Image = Properties.Resources.driving_test_512;
                    break;

            }

        }


        

        public void Load(int AppointmentID)
            {
                _testAppointment=clsTestAppointment.Find(AppointmentID);
                _FillData();

            }

            private void ctrlSecheduledTest_Load(object sender, EventArgs e)
            {
               
            }

        
    }
}

