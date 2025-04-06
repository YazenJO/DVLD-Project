using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Buisness;
using DVLD_Business;
using DVLD.Classes;

namespace DVLD.Licenses.Local_Licenses
{
    public partial class frmIssueDriverLicenseForTheFirstTime : Form
    {
        private clsLicense _newLicense;
        private clsDriver _Driver; 
       
        private int _LocalApplicationId;


        private clsLocalDrivingLicenseApplication Localapp;
        public frmIssueDriverLicenseForTheFirstTime(int LocalApplicationId)
        {
            InitializeComponent();

            _LocalApplicationId = LocalApplicationId;
            ctrlDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(LocalApplicationId);
             Localapp = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalApplicationId);
        }

        private void frmIssueDriverLicenseForTheFirstTime_Load(object sender, EventArgs e)
        {

        }

        private bool CreateNewLicence()
        {
            clsLocalDrivingLicenseApplication localApp = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalApplicationId);
            clsLicenseClass localAppLicenseClass = clsLicenseClass.Find(Localapp.LicenseClassID);
            _newLicense = new clsLicense();
            _newLicense.LicenseClass = localAppLicenseClass.LicenseClassID;
            _newLicense.ApplicationID = localApp.ApplicationID;
            _newLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _newLicense.DriverID = (int)_Driver.DriverID;
            _newLicense.IssueDate = DateTime.Today;
            _newLicense.ExpirationDate = DateTime.Today.AddYears(localAppLicenseClass.DefaultValidityLength);
            _newLicense.IsActive = true;
            _newLicense.Notes = txtNotes.Text.Trim();
            _newLicense.PaidFees = (decimal)localAppLicenseClass.ClassFees;
            _newLicense.IssueReason = 1;//1-FirstTime, 2-Renew, 3-Replacement for Damaged, 4- Replacement for Lost.
            return (_newLicense.Save());
        }

        private bool CreateNewDriver()
        {
            _Driver = new clsDriver();
            _Driver.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _Driver.CreatedDate = DateTime.Today;
            _Driver.PersonID = Localapp.ApplicantPersonID;
            return (_Driver.Save());

        }

        private void ctrlDrivingLicenseApplicationInfo1_Load(object sender, EventArgs e)
        {






        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (clsDriver.FindByPersonID(Localapp.ApplicantPersonID)==null)
            {
                CreateNewDriver();
            }
            else
            {
                _Driver = clsDriver.FindByPersonID(Localapp.ApplicantPersonID);
            }
           
                if (CreateNewLicence())
                {
                    clsApplication application=clsApplication.FindBaseApplication(Localapp.ApplicationID);
                    if (application.SetComplete())
                    {
                        MessageBox.Show("License Issued Successfully with License ID = " + _newLicense.LicenseID,
                            "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error While Finding A Application", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                   
                   
                }
                else
                {
                    MessageBox.Show("Error While Creating A Licence","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            
        
        private void btnClose_Click(object sender, EventArgs e)
        {
           Close();
        }
    }
}

