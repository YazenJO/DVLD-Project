using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.Classes;
using DVLD.Licenses.Local_Licenses;
using DVLD_Buisness;
using DVLD_Business;

namespace DVLD.Licenses.Detain_License
{
    public partial class frmDetainLicense : Form
    {
        private int _LicenseID;
       
        private clsDetain _NewDetain;
        public frmDetainLicense()
        {
            InitializeComponent();
        }
        private void _FillDefaultData()
        {
            lblDetainDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;

        }
        private void _SetData()
        {

            lblLicenseID.Text = _LicenseID.ToString();




        }
        private bool _CreateNewDetain()
        {
            _NewDetain = new clsDetain();
            _NewDetain.LicenseID = _LicenseID;
            _NewDetain.FineFees = int.Parse(txtFineFees.Text.Trim());
            _NewDetain.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _NewDetain.DetainDate = DateTime.Today;
            return (_NewDetain.Save());
        }
        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _LicenseID = obj;
            _SetData();
            if (clsDetain.DoesLicenseDetained(_LicenseID))
            {
                MessageBox.Show("The license you selected is Already Detained. Please select an active license to continue.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            llShowLicenseHistory.Enabled = true;
            btnDetain.Enabled = true;
        }

        private void frmDetainLicense_Load(object sender, EventArgs e)
        {
            _FillDefaultData();
        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFineFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "Fees cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtFineFees, null);

            };


            if (!clsValidatoin.IsNumber(txtFineFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(txtFineFees, null);
            };
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                return;
            }
            if (MessageBox.Show("Are you sure  want to Detain this license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                return;
            }
           

            if (!_CreateNewDetain())
            {
                MessageBox.Show("Error While Creating A New Detain", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


           
            MessageBox.Show("License Renewed Successfully with ID = " + _NewDetain.DetainID, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lblDetainID.Text = _NewDetain.DetainID.ToString();
            btnDetain.Enabled = false;
            llShowLicenseInfo.Enabled = true;
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDriverLicenseHistory frm = new frmDriverLicenseHistory(clsDriver.Find(clsLicense.Find(_NewDetain.LicenseID).DriverID).PersonID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtFineFees_TextChanged(object sender, EventArgs e)
        {
            string input = txtFineFees.Text;

            // Remove non-numeric characters
            string numericText = new string(input.Where(char.IsDigit).ToArray());

            // Update the textbox only if the content changed
            if (txtFineFees.Text != numericText)
            {
                txtFineFees.Text = numericText;
                txtFineFees.SelectionStart = txtFineFees.Text.Length; // Keep cursor at the end
            }
        }

        private void llShowLicenseInfo_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo((int)_NewDetain.LicenseID);
            frm.ShowDialog();
        }
    }
}
