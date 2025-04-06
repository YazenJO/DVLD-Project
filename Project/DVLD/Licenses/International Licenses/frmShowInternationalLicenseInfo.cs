using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses.International_Licenses
{
    public partial class frmShowInternationalLicenseInfo : Form
    {
        
        public frmShowInternationalLicenseInfo(int InternationalLicenseID)
        {
            InitializeComponent();
            ctrlInternationalLicenseInfo1.LoadInteernationalLicenseInfo(InternationalLicenseID);
        }

        private void frmShowInternationalLicenseInfo_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
