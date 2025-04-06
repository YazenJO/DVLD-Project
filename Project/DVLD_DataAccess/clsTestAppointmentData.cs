using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsTestAppointmentData
    {
        public static bool GetTestAppointmentByID(int? TestAppointmentID, ref int TestTypeID, ref int LocalDrivingLicenseApplicationID, ref DateTime AppointmentDate, ref decimal PaidFees, ref int CreatedByUserID, ref bool IsLocked, ref int? RetakeTestApplicationID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TestAppointmentID", (object)TestAppointmentID ?? DBNull.Value);

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;


                                TestTypeID = (int)reader["TestTypeID"];
                                LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                                AppointmentDate = (DateTime)reader["AppointmentDate"];
                                PaidFees = (decimal)reader["PaidFees"];
                                CreatedByUserID = (int)reader["CreatedByUserID"];
                                IsLocked = (bool)reader["IsLocked"];
                                RetakeTestApplicationID = (reader["RetakeTestApplicationID"] != DBNull.Value) ? (int?)reader["RetakeTestApplicationID"] : null;
                            }
                            else
                                isFound = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                isFound = false;
            }

            return isFound;
        }
        public static int AddNewTestAppointment(int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID, bool IsLocked, int? RetakeTestApplicationID)
        {
            int TestAppointmentID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"INSERT INTO TestAppointments (TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID)
                            VALUES (@TestTypeID, @LocalDrivingLicenseApplicationID, @AppointmentDate, @PaidFees, @CreatedByUserID, @IsLocked, @RetakeTestApplicationID)
                            SELECT SCOPE_IDENTITY();";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                        command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                        command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
                        command.Parameters.AddWithValue("@PaidFees", PaidFees);
                        command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                        command.Parameters.AddWithValue("@IsLocked", IsLocked);
                        command.Parameters.AddWithValue("@RetakeTestApplicationID", (object)RetakeTestApplicationID ?? DBNull.Value);

                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                            TestAppointmentID = insertedID;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return TestAppointmentID;
        }
        public static bool UpdateTestAppointment(int? TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID, bool IsLocked, int? RetakeTestApplicationID)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"UPDATE TestAppointments  
                            SET 
                            TestTypeID = @TestTypeID, 
                            LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID, 
                            AppointmentDate = @AppointmentDate, 
                            PaidFees = @PaidFees, 
                            CreatedByUserID = @CreatedByUserID, 
                            IsLocked = @IsLocked, 
                            RetakeTestApplicationID = @RetakeTestApplicationID
                            WHERE TestAppointmentID = @TestAppointmentID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                        command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                        command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                        command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
                        command.Parameters.AddWithValue("@PaidFees", PaidFees);
                        command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                        command.Parameters.AddWithValue("@IsLocked", IsLocked);
                        command.Parameters.AddWithValue("@RetakeTestApplicationID", (object)RetakeTestApplicationID ?? DBNull.Value);

                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return (rowsAffected > 0);
        }
        public static bool DeleteTestAppointment(int? TestAppointmentID)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"Delete TestAppointments 
                                where TestAppointmentID = @TestAppointmentID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TestAppointmentID", (object)TestAppointmentID ?? DBNull.Value);

                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return (rowsAffected > 0);
        }
        public static bool DoesTestAppointmentExist(int? TestAppointmentID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT Found = 1 FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TestAppointmentID", (object)TestAppointmentID ?? DBNull.Value);

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        isFound = reader.HasRows;
                    }
                }
            }
            catch (Exception ex)
            {
                isFound = false;
            }

            return isFound;
        }
        public static DataTable GetAllTestAppointments()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM TestAppointments";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                            dt.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return dt;
        }

        public static DataTable GetTestAppointmentByLDLID(int LDLID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM TestAppointments_View Where LocalDrivingLicenseApplicationID=@LDLID";
                    

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LDLID", LDLID);
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                            dt.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return dt;
        }

        public static bool DoesUserHasActiveTestAppointment(int LDL_ID, int TestTypeID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT Found=1 FROM TestAppointments ta  WHERE ta.LocalDrivingLicenseApplicationID=@LDL_ID AND ta.TestTypeID=@TestTypeID AND ta.IsLocked=0";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LDL_ID", LDL_ID);
                        command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        isFound = reader.HasRows;
                    }
                }
            }
            catch (Exception ex)
            {
                isFound = false;
            }

            return isFound;
        }
        
    }
}
