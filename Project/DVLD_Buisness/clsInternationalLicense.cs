using System;
using System.Data;
using DVLD_Buisness;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsInternationalLicense
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int? InternationalLicenseID { set; get; }
        public int ApplicationID { set; get; }
        public int DriverID { set; get; }
        public int IssuedUsingLocalLicenseID { set; get; }
        public DateTime IssueDate { set; get; }
        public DateTime ExpirationDate { set; get; }
        public bool IsActive { set; get; }
        public int CreatedByUserID { set; get; }

        public clsInternationalLicense()
        {
            this.InternationalLicenseID = null;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.IssuedUsingLocalLicenseID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.IsActive = false;
            this.CreatedByUserID = -1;
            Mode = enMode.AddNew;
        }
        private clsInternationalLicense(int? InternationalLicenseID, int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            this.InternationalLicenseID = InternationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;
            Mode = enMode.Update;
        }
        private bool _AddNewInternationalLicense()
        {
            this.InternationalLicenseID = (int?)clsInternationalLicenseData.AddNewInternationalLicense(this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID, this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);
            return (this.InternationalLicenseID != -1);
        }
        private bool _UpdateInternationalLicense()
            => clsInternationalLicenseData.UpdateInternationalLicense(this.InternationalLicenseID, this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID, this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);
        public static clsInternationalLicense Find(int? InternationalLicenseID)
        {
            int ApplicationID = -1;
            int DriverID = -1;
            int IssuedUsingLocalLicenseID = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            bool IsActive = false;
            int CreatedByUserID = -1;

            bool IsFound = clsInternationalLicenseData.GetInternationalLicenseByID(InternationalLicenseID, ref ApplicationID, ref DriverID, ref IssuedUsingLocalLicenseID, ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID);

            if (IsFound)
                return new clsInternationalLicense(InternationalLicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);
            else
                return null;
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewInternationalLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateInternationalLicense();
            }
            return false;
        }
        public static bool DeleteInternationalLicense(int? InternationalLicenseID)
            => clsInternationalLicenseData.DeleteInternationalLicense(InternationalLicenseID);
        public static bool DoesInternationalLicenseExist(int? InternationalLicenseID)
            => clsInternationalLicenseData.DoesInternationalLicenseExist(InternationalLicenseID);
        public static DataTable GetInternationalLicenses()
            => clsInternationalLicenseData.GetAllInternationalLicenses();
        public static DataTable GetDriverInternationalLicenses(int DriverID)
        {
            return clsInternationalLicenseData.GetDriverInternationalLicenses(DriverID);
        }
        public static clsInternationalLicense GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {
            int ApplicationID = -1;
            int InternationalLicenseID = -1;
            int IssuedUsingLocalLicenseID = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            bool IsActive = true;
            int CreatedByUserID = -1;

            bool IsFound = clsInternationalLicenseData.GetActiveInternationalLicenseIDByDriverID(ref DriverID,
                ref InternationalLicenseID,ref ApplicationID, ref IssuedUsingLocalLicenseID, ref IssueDate, ref ExpirationDate,
                ref CreatedByUserID);

            if (IsFound)
            {
                return new clsInternationalLicense(InternationalLicenseID, ApplicationID, DriverID,
                    IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);
            }
            else
            {
                return null;
            }
        }
        public static DataTable GetAllInternationalLicenses()
        {
            return clsInternationalLicenseData.GetAllInternationalLicenses();

        }

    }
}
