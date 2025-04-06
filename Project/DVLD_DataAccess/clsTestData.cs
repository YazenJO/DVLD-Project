using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsTestData
    {
        public static bool GetTestByID(int? TestID, ref int TestAppointmentID, ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM Tests WHERE TestID = @TestID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TestID", (object)TestID ?? DBNull.Value);

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;


                                TestAppointmentID = (int)reader["TestAppointmentID"];
                                TestResult = (bool)reader["TestResult"];
                                Notes = (reader["Notes"] != DBNull.Value) ? (string)reader["Notes"] : null;
                                CreatedByUserID = (int)reader["CreatedByUserID"];
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
        public static int AddNewTest(int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            int TestID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"INSERT INTO Tests (TestAppointmentID, TestResult, Notes, CreatedByUserID)
                            VALUES (@TestAppointmentID, @TestResult, @Notes, @CreatedByUserID)
                            SELECT SCOPE_IDENTITY();";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                        command.Parameters.AddWithValue("@TestResult", TestResult);
                        command.Parameters.AddWithValue("@Notes", (object)Notes ?? DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                            TestID = insertedID;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return TestID;
        }
        public static bool UpdateTest(int? TestID, int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"UPDATE Tests  
                            SET 
                            TestAppointmentID = @TestAppointmentID, 
                            TestResult = @TestResult, 
                            Notes = @Notes, 
                            CreatedByUserID = @CreatedByUserID
                            WHERE TestID = @TestID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@TestID", TestID);
                        command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                        command.Parameters.AddWithValue("@TestResult", TestResult);
                        command.Parameters.AddWithValue("@Notes", (object)Notes ?? DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

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
        public static bool DeleteTest(int? TestID)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"Delete Tests 
                                where TestID = @TestID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TestID", (object)TestID ?? DBNull.Value);

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
        public static bool DoesTestExist(int? TestID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT Found = 1 FROM Tests WHERE TestID = @TestID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TestID", (object)TestID ?? DBNull.Value);

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
        public static DataTable GetAllTests()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM Tests";

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
        public static bool GetTestByTestAppointmentID(int TestAppointmentID, ref int? TestID,  ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM Tests WHERE TestID = @TestAppointmentID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TestAppointmentID",TestAppointmentID);

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                TestID = (reader["TestID"] != DBNull.Value) ? (int?)reader["TestID"] : null;
                                TestResult = (bool)reader["TestResult"];
                                Notes = (reader["Notes"] != DBNull.Value) ? (string)reader["Notes"] : null;
                                CreatedByUserID = (int)reader["CreatedByUserID"];
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
        
    }
}
