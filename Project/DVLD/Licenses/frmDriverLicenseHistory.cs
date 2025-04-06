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

namespace DVLD.Licenses
{
    public partial class frmDriverLicenseHistory : Form
    {
        private int _PersonID;
        
        
        public frmDriverLicenseHistory(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
            
            
        }

        private void frmDriverLicenseHistory_Load(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.LoadPersonInfo(_PersonID);
            ctrlLicensesHistory1.LoadDriverLicensesHistory(_PersonID);
        }
    }
}
