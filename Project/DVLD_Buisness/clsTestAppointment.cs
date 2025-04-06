using System;
using System.Data;
using DVLD_Buisness;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsTestAppointment
    {
        public enum enMode
        {
            AddNew = 0,
            Update = 1
        };

        public enMode Mode = enMode.AddNew;
        public int? TestAppointmentID { set; get; }
        public int TestTypeID { set; get; }
        public int LocalDrivingLicenseApplicationID { set; get; }
        public DateTime AppointmentDate { set; get; }
        public decimal PaidFees { set; get; }
        public int CreatedByUserID { set; get; }
        public bool IsLocked { set; get; }
        public int? RetakeTestApplicationID { set; get; }

        public clsTestAppointment()
        {
            this.TestAppointmentID = null;
            this.TestTypeID = -1;
            this.LocalDrivingLicenseApplicationID = -1;
            this.AppointmentDate = DateTime.Now;
            this.PaidFees = -1;
            this.CreatedByUserID = -1;
            this.IsLocked = false;
            this.RetakeTestApplicationID = null;
            Mode = enMode.AddNew;
        }

        private clsTestAppointment(int? TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID,
            DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID, bool IsLocked,
            int? RetakeTestApplicationID)
        {
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsLocked = IsLocked;
            this.RetakeTestApplicationID = RetakeTestApplicationID;
            Mode = enMode.Update;
        }

        private bool _AddNewTestAppointment()
        {
            this.TestAppointmentID = (int?)clsTestAppointmentData.AddNewTestAppointment(this.TestTypeID,
                this.LocalDrivingLicenseApplicationID, this.AppointmentDate, this.PaidFees, this.CreatedByUserID,
                this.IsLocked, this.RetakeTestApplicationID);
            return (this.TestAppointmentID != -1);
        }

        private bool _UpdateTestAppointment()
            => clsTestAppointmentData.UpdateTestAppointment(this.TestAppointmentID, this.TestTypeID,
                this.LocalDrivingLicenseApplicationID, this.AppointmentDate, this.PaidFees, this.CreatedByUserID,
                this.IsLocked, this.RetakeTestApplicationID);

        public static clsTestAppointment Find(int? TestAppointmentID)
        {
            int TestTypeID = -1;
            int LocalDrivingLicenseApplicationID = -1;
            DateTime AppointmentDate = DateTime.Now;
            decimal PaidFees = -1;
            int CreatedByUserID = -1;
            bool IsLocked = false;
            int? RetakeTestApplicationID = null;

            bool IsFound = clsTestAppointmentData.GetTestAppointmentByID(TestAppointmentID, ref TestTypeID,
                ref LocalDrivingLicenseApplicationID, ref AppointmentDate, ref PaidFees, ref CreatedByUserID,
                ref IsLocked, ref RetakeTestApplicationID);

            if (IsFound)
                return new clsTestAppointment(TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID,
                    AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestAppointment())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateTestAppointment();
            }

            return false;
        }

        public static bool DeleteTestAppointment(int? TestAppointmentID)
            => clsTestAppointmentData.DeleteTestAppointment(TestAppointmentID);

        public static bool DoesTestAppointmentExist(int? TestAppointmentID)
            => clsTestAppointmentData.DoesTestAppointmentExist(TestAppointmentID);

        public static DataTable GetTestAppointments()
            => clsTestAppointmentData.GetAllTestAppointments();

        public static DataTable GetTestAppointmentByLDLID(int LDLID)
            => clsTestAppointmentData.GetTestAppointmentByLDLID(LDLID);

        public static bool DoesUserHasActiveTestAppointment(int LDL_ID, clsTestType.enTestType Type)
            => clsTestAppointmentData.DoesUserHasActiveTestAppointment(LDL_ID, (int)Type);


    }
}
