using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsDetain
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int? DetainID { set; get; }
        public int LicenseID { set; get; }
        public DateTime DetainDate { set; get; }
        public decimal FineFees { set; get; }
        public int CreatedByUserID { set; get; }
        public bool IsReleased { set; get; }
        public DateTime? ReleaseDate { set; get; }
        public int? ReleasedByUserID { set; get; }
        public int? ReleaseApplicationID { set; get; }

        public clsDetain()
        {
            this.DetainID = null;
            this.LicenseID = -1;
            this.DetainDate = DateTime.Now;
            this.FineFees = -1;
            this.CreatedByUserID = -1;
            this.IsReleased = false;
            this.ReleaseDate = null;
            this.ReleasedByUserID = null;
            this.ReleaseApplicationID = null;
            Mode = enMode.AddNew;
        }
        private clsDetain(int? DetainID, int LicenseID, DateTime DetainDate, decimal FineFees, int CreatedByUserID, bool IsReleased, DateTime? ReleaseDate, int? ReleasedByUserID, int? ReleaseApplicationID)
        {
            this.DetainID = DetainID;
            this.LicenseID = LicenseID;
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsReleased = IsReleased;
            this.ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleaseApplicationID = ReleaseApplicationID;
            Mode = enMode.Update;
        }
        private bool _AddNewDetain()
        {
            this.DetainID = (int?)clsDetainData.AddNewDetain(this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID, this.IsReleased, this.ReleaseDate, this.ReleasedByUserID, this.ReleaseApplicationID);
            return (this.DetainID != -1);
        }
        private bool _UpdateDetain()
            => clsDetainData.UpdateDetain(this.DetainID, this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID, this.IsReleased, this.ReleaseDate, this.ReleasedByUserID, this.ReleaseApplicationID);
        public static clsDetain Find(int? DetainID)
        {
            int LicenseID = -1;
            DateTime DetainDate = DateTime.Now;
            decimal FineFees = -1;
            int CreatedByUserID = -1;
            bool IsReleased = false;
            DateTime? ReleaseDate = null;
            int? ReleasedByUserID = null;
            int? ReleaseApplicationID = null;

            bool IsFound = clsDetainData.GetDetainByID(DetainID, ref LicenseID, ref DetainDate, ref FineFees, ref CreatedByUserID, ref IsReleased, ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID);

            if (IsFound)
                return new clsDetain(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            else
                return null;
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDetain())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateDetain();
            }
            return false;
        }
        public static bool DeleteDetain(int? DetainID)
            => clsDetainData.DeleteDetain(DetainID);
        public static bool DoesDetainExist(int? DetainID)
            => clsDetainData.DoesDetainExist(DetainID);
        public static bool DoesLicenseDetained(int LicenseID)
            => clsDetainData.DoesLicenseDetained(LicenseID);
        public static DataTable GetDetainedLicenses()
            => clsDetainData.GetAllDetainedLicenses();
        public static clsDetain FindByLicenseID(int LicenseID)
        {
            int? DetainID =null;
            DateTime DetainDate = DateTime.Now;
            decimal FineFees = -1;
            int CreatedByUserID = -1;
            bool IsReleased = false;
            DateTime? ReleaseDate = null;
            int? ReleasedByUserID = null;
            int? ReleaseApplicationID = null;

            bool IsFound = clsDetainData.GetDetainByLicenseID(LicenseID, ref DetainID,  ref DetainDate, ref FineFees, ref CreatedByUserID, ref IsReleased, ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID);

            if (IsFound)
                return new clsDetain(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            else
                return null;
        }
    }
}
