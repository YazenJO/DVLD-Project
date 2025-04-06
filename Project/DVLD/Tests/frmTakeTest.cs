using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Business;
using DVLD.Classes;

namespace DVLD.Tests
{
    public partial class frmTakeTest : Form
    {
        private int _testAppointmentId;
        private clsTest _Test;

       

        public frmTakeTest(int testAppointmentId)
        {
            InitializeComponent();
            _testAppointmentId = testAppointmentId;
            _Test = new clsTest();
        }

        private bool Result()
        {
            if (rdPass.Checked)
                return true;
            return false;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            _Test.TestAppointmentID=_testAppointmentId;
            _Test.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _Test.Notes =txtNotes.Text ;
            _Test.TestResult = Result();
            clsTestAppointment ta=clsTestAppointment.Find(_testAppointmentId);
            ta.IsLocked = true;
            if (!ta.Save())
                MessageBox.Show("Error: Test Appointment Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (_Test.Save())
            
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            ctrlSecheduledTest1.Load(_testAppointmentId);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
