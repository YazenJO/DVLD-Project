using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Tests.Schdule_Test
{
    public partial class frmScheduleTest : Form
    {
        private int _LocalicenseDriverID;
        private int _AppointmentID;
        public frmScheduleTest(int LocalDLID,int AppointmentID=-1)
        {
            InitializeComponent();
            _LocalicenseDriverID = LocalDLID;
            _AppointmentID = AppointmentID;
        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            if (_AppointmentID==-1)
            {
                ctrlScheduleTest1.Load(_LocalicenseDriverID);
                return;
            }
            ctrlScheduleTest1.Load(_LocalicenseDriverID, _AppointmentID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void ctrlScheduleTest1_Load(object sender, EventArgs e)
        {

        }
    }
}
