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

namespace DVLD.Licenses.Local_Licenses
{
    public partial class frmShowLicenseInfo : Form
    {
        
        private clsLicense License;

        
        public frmShowLicenseInfo(int LicenseID)
        {
            InitializeComponent();
            ctrlDriverLicenseInfo1.LocalDriverLicenseInfoByLocalDAppID(LicenseID);
        }

        private void frmShowLicenseInfo_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
