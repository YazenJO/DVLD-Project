using System;
using System.Data;
using System.Runtime.CompilerServices;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsDriver
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int? DriverID { set; get; }
        public int PersonID { set; get; }
        public int CreatedByUserID { set; get; }
        public DateTime CreatedDate { set; get; }

        public clsDriver()
        {
            this.DriverID = null;
            this.PersonID = -1;
            this.CreatedByUserID = -1;
            this.CreatedDate = DateTime.Now;
            Mode = enMode.AddNew;
        }
        private clsDriver(int? DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = CreatedDate;
            Mode = enMode.Update;
        }
        private bool _AddNewDriver()
        {
            this.DriverID = (int?)clsDriverData.AddNewDriver(this.PersonID, this.CreatedByUserID, this.CreatedDate);
            return (this.DriverID != -1);
        }
        private bool _UpdateDriver()
            => clsDriverData.UpdateDriver(this.DriverID, this.PersonID, this.CreatedByUserID, this.CreatedDate);
        public static clsDriver Find(int? DriverID)
        {
            int PersonID = -1;
            int CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;

            bool IsFound = clsDriverData.GetDriverByID(DriverID, ref PersonID, ref CreatedByUserID, ref CreatedDate);

            if (IsFound)
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            else
                return null;
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDriver())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateDriver();
            }
            return false;
        }
        public static bool DeleteDriver(int? DriverID)
            => clsDriverData.DeleteDriver(DriverID);
        public static bool DoesDriverExist(int? DriverID)
            => clsDriverData.DoesDriverExist(DriverID);
        public static DataTable GetDrivers()
            => clsDriverData.GetAllDrivers();

        public static clsDriver FindByPersonID(int PersonID)
        {
            int? DriverId=-1;
            int CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;

            bool IsFound = clsDriverData.GetDriverInfoByPersonID(PersonID,ref DriverId, ref CreatedByUserID, ref CreatedDate);

            if (IsFound)
                return new clsDriver(DriverId, PersonID, CreatedByUserID, CreatedDate);
            else
                return null;
        }

        public  static DataTable GetLocalDriverLicenses(int DriverID)
            => clsDriverData.GetLocalDriverLicenses(DriverID);

        public static DataTable GetLocalDriverLicensesByPersonID(int PersonID)
            => clsDriverData.GetLocalDriverLicensesByPersonID(PersonID);

        public static DataTable GetAllDriversFormDriversView()
            => clsDriverData.GetAllDriversFormDriversView();
        public static DataTable GetInternationalLicenses(int DriverID)
        {
            return clsInternationalLicense.GetDriverInternationalLicenses(DriverID);
        }
    }
}
