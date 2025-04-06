using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsTest
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int? TestID { set; get; }
        public int TestAppointmentID { set; get; }
        public bool TestResult { set; get; }
        public string Notes { set; get; }
        public int CreatedByUserID { set; get; }

        public clsTest()
        {
            this.TestID = null;
            this.TestAppointmentID = -1;
            this.TestResult = false;
            this.Notes = null;
            this.CreatedByUserID = -1;
            Mode = enMode.AddNew;
        }
        private clsTest(int? TestID, int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.CreatedByUserID = CreatedByUserID;
            Mode = enMode.Update;
        }
        private bool _AddNewTest()
        {
            this.TestID = (int?)clsTestData.AddNewTest(this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);
            return (this.TestID != -1);
        }
        private bool _UpdateTest()
            => clsTestData.UpdateTest(this.TestID, this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);
        public static clsTest Find(int? TestID)
        {
            int TestAppointmentID = -1;
            bool TestResult = false;
            string Notes = null;
            int CreatedByUserID = -1;

            bool IsFound = clsTestData.GetTestByID(TestID, ref TestAppointmentID, ref TestResult, ref Notes, ref CreatedByUserID);

            if (IsFound)
                return new clsTest(TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID);
            else
                return null;
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTest())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateTest();
            }
            return false;
        }
        public static bool DeleteTest(int? TestID)
            => clsTestData.DeleteTest(TestID);
        public static bool DoesTestExist(int? TestID)
            => clsTestData.DoesTestExist(TestID);
        public static DataTable GetTests()
            => clsTestData.GetAllTests();

      
        public static clsTest FindByTestAppointmentID(int TestAppointmentID)
        {
            int? TestID = -1;
            bool TestResult = false;
            string Notes = null;
            int CreatedByUserID = -1;

            bool IsFound = clsTestData.GetTestByTestAppointmentID(TestAppointmentID, ref TestID,   ref TestResult, ref Notes, ref CreatedByUserID);

            if (IsFound)
                return new clsTest(TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID);
            else
                return null;
        }
    }
}
