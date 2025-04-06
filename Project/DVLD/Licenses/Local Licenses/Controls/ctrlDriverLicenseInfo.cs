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

namespace DVLD.Licenses.Local_Licenses.Controls
{
    public partial class ctrlDriverLicenseInfo : UserControl
    {
      
        private clsLicense _DriveLicense;
        public clsLicense SelectedLicenseInfo
        { get { return _DriveLicense; } }

        public int PersonID { get; set; }
        

        public ctrlDriverLicenseInfo()
        {
            InitializeComponent();
        }

            private void SetLicenseData(clsLicense _DriveLicense)
            {
                clsLocalDrivingLicenseApplication localApp=clsLocalDrivingLicenseApplication.FindByApplicationID(_DriveLicense.ApplicationID);
                clsPerson Person=clsPerson.Find(localApp.ApplicantPersonID);
                lblFullName.Text = localApp.PersonFullName; // Assuming FullName exists in _DriveLicense
                lblClass.Text = clsLicenseClass.Find(_DriveLicense.LicenseClass).ClassName;
                lblDateOfBirth.Text = Person.DateOfBirth.ToShortDateString(); // Assuming DateOfBirth exists
                lblDriverID.Text = _DriveLicense.DriverID.ToString();
                lblExpirationDate.Text = _DriveLicense.ExpirationDate.ToShortDateString();
                lblNationalNo.Text = Person.NationalNo;
                switch (Person.Gendor)
                {
                case 0:
                    lblGendor.Text = "Male";
                    pbGendor.Image = Properties.Resources.Man_32;
                    break;
                case 1:
                    lblGendor.Text = "Female";
                    pbGendor.Image = Properties.Resources.Woman_32;
                    break;
                }
                lblIsActive.Text = _DriveLicense.IsActive ? "Active" : "Inactive";
                lblIsDetained.Text = clsDetain.DoesLicenseDetained((int)this._DriveLicense.LicenseID) ? "Yes" : "No"; // Assuming IsDetained exists
                lblIssueDate.Text = _DriveLicense.IssueDate.ToShortDateString();
                lblIssueReason.Text = clsApplicationType.Find(_DriveLicense.IssueReason).Title;
                lblLicenseID.Text = _DriveLicense.LicenseID.HasValue ? _DriveLicense.LicenseID.Value.ToString() : "N/A";
                if (Person.ImagePath!=null|| Person.ImagePath !="")
                {
                    try
                    {
                        pbPersonImage.Image = Image.FromFile(Person.ImagePath);
                }
                    catch (Exception e)
                    {
                        pbPersonImage.Image = Properties.Resources.Male_512;
                    }
                 
                }

                if (this._DriveLicense.Notes=="")
                {
                    lblNotes.Text = "No Notes";
                }
                else
                {
                    lblNotes.Text = this._DriveLicense.Notes;
                }

                PersonID = Person.PersonID;
            }
        public void LoadLocalDriverLicenseInfo(int DriveLinceseID)
        {
        
            _DriveLicense=clsLicense.Find(DriveLinceseID);
            if (_DriveLicense!=null)
            {
                SetLicenseData(_DriveLicense);
            }
            else
            {
                MessageBox.Show("There is no License With ID =" + DriveLinceseID, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            
        }
        public void LocalDriverLicenseInfoByLocalDAppID(int LicenseID)
        {
            
            _DriveLicense = clsLicense.Find(LicenseID);
            SetLicenseData(_DriveLicense);
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
