using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DVLD_Buisness;
using DVLD_Business;
using static DevExpress.Skins.SolidColorHelper;

namespace DVLD.Licenses.International_Licenses.Controls
{
    public partial class ctrlInternationalLicenseInfo : UserControl
    {
        private int _InternationalLicenseID;
        private clsInternationalLicense internationalLicense;
        public ctrlInternationalLicenseInfo()
        {
            InitializeComponent();
        }

        private void _LoadPersonImage()
        {
            

        }

        private void _SetData()
        {
            clsPerson Person = clsPerson.Find(clsDriver.Find(internationalLicense.DriverID).PersonID);
            lblFullName.Text = Person.FullName ;  // Assuming there's a FullName property
            lblInternationalLicenseID.Text = internationalLicense.InternationalLicenseID?.ToString() ?? "N/A";
            lblApplicationID.Text = internationalLicense.ApplicationID.ToString();
            lblDateOfBirth.Text = Person.DateOfBirth.ToString(); // Assuming a DateTime field
            lblDriverID.Text = internationalLicense.DriverID.ToString();
            lblExpirationDate.Text = internationalLicense.ExpirationDate.ToShortDateString();
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
            lblIsActive.Text = internationalLicense.IsActive ? "Active" : "Inactive";
            lblIssueDate.Text = internationalLicense.IssueDate.ToShortDateString();
            lblLocalLicenseID.Text = internationalLicense.IssuedUsingLocalLicenseID.ToString();
            lblNationalNo.Text = Person.NationalNo;
            if (Person.ImagePath != "")
                if (File.Exists(Person.ImagePath))
                    pbPersonImage.Load(Person.ImagePath);
                else
                    MessageBox.Show("Could not find this image: = " + Person.ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        public void LoadInteernationalLicenseInfo(int InternationalLicenseID)
        {
            _InternationalLicenseID=InternationalLicenseID;
            internationalLicense = clsInternationalLicense.Find(InternationalLicenseID);
            _SetData();

        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
