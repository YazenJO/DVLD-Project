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

namespace DVLD.Licenses.Controls
{
    public partial class ctrlLicensesHistory : UserControl
    {
        private int _PesronID;
        
        
        public ctrlLicensesHistory()
        {
            InitializeComponent();
        }

        void FillData()
        {
            
         dgvLocalLicensesHistory.DataSource = clsDriver.GetLocalDriverLicensesByPersonID(_PesronID);
         lblLocalLicensesRecords.Text = dgvLocalLicensesHistory.RowCount.ToString();
         dgvInternationalLicensesHistory.DataSource = clsDriver.GetInternationalLicenses((int)clsDriver.FindByPersonID(_PesronID).DriverID);
         lblLocalLicensesRecords.Text = dgvInternationalLicensesHistory.RowCount.ToString();

        }
        public void LoadDriverLicensesHistory(int PesronID)
        {
            _PesronID = PesronID;
            FillData();
        }
    }
}
