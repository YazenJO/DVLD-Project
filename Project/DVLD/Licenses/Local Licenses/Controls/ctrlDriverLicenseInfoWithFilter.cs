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
using DVLD.Classes;

namespace DVLD.Licenses.Local_Licenses.Controls
{
    public partial class ctrlDriverLicenseInfoWithFilter : UserControl
    {
        private int _LicenseID=-1;
        public event Action<int> OnLicenseSelected;
        public int PersonID { get; set; }

        protected virtual void DriverSelected(int LicenseID)
        {
            Action<int> handler = OnLicenseSelected;
            if (handler != null)
            {
                handler(LicenseID);
            }
        }
        public ctrlDriverLicenseInfoWithFilter()
        {
            InitializeComponent();
        }
      

        public void LoadByLicenseID(int LicenseID)
        {
            _LicenseID = LicenseID;
            txtLicenseID.Text = _LicenseID.ToString();
            txtLicenseID.Enabled = false;
            btnFind_Click((this), null);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLicenseID.Focus();
                return;

            }

                int.Parse(txtLicenseID.Text);
                _LicenseID = int.Parse(txtLicenseID.Text.Trim());
            
            
            ctrlDriverLicenseInfo1.LoadLocalDriverLicenseInfo(_LicenseID);
            if (ctrlDriverLicenseInfo1.SelectedLicenseInfo !=null)
            {
                if (OnLicenseSelected!=null)
                {
                    DriverSelected((int)ctrlDriverLicenseInfo1.SelectedLicenseInfo.LicenseID);
                    PersonID = ctrlDriverLicenseInfo1.PersonID;
                }
            }
        }

        private void txtLicenseID_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtLicenseID.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtLicenseID, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(txtLicenseID, null);
            }
        }

        private void txtLicenseID_TextChanged(object sender, EventArgs e)
        {
            string input = txtLicenseID.Text;

            // Remove non-numeric characters
            string numericText = new string(input.Where(char.IsDigit).ToArray());

            // Update the textbox only if the content changed
            if (txtLicenseID.Text != numericText)
            {
                txtLicenseID.Text = numericText;
                txtLicenseID.SelectionStart = txtLicenseID.Text.Length; // Keep cursor at the end
            }
        }

        private void ctrlDriverLicenseInfoWithFilter_Load(object sender, EventArgs e)
        {

        }
    }
}
