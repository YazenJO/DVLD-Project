using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVLD_DataAccess.clsCountryData;
using System.Net;
using System.Security.Policy;

namespace DVLD_DataAccess
{
    public class clsDriverData
    {


        public static bool GetDriverByID(int? DriverID, ref int PersonID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM Drivers WHERE DriverID = @DriverID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DriverID", (object)DriverID ?? DBNull.Value);

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;


                                PersonID = (int)reader["PersonID"];
                                CreatedByUserID = (int)reader["CreatedByUserID"];
                                CreatedDate = (DateTime)reader["CreatedDate"];
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
        public static int AddNewDriver(int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            int DriverID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"INSERT INTO Drivers (PersonID, CreatedByUserID, CreatedDate)
                            VALUES (@PersonID, @CreatedByUserID, @CreatedDate)
                            SELECT SCOPE_IDENTITY();";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                        command.Parameters.AddWithValue("@CreatedDate", CreatedDate);

                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                            DriverID = insertedID;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return DriverID;
        }
        public static bool UpdateDriver(int? DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"UPDATE Drivers  
                            SET 
                            PersonID = @PersonID, 
                            CreatedByUserID = @CreatedByUserID, 
                            CreatedDate = @CreatedDate
                            WHERE DriverID = @DriverID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@DriverID", DriverID);
                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                        command.Parameters.AddWithValue("@CreatedDate", CreatedDate);

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
        public static bool DeleteDriver(int? DriverID)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"Delete Drivers 
                                where DriverID = @DriverID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DriverID", (object)DriverID ?? DBNull.Value);

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
        public static bool DoesDriverExist(int? DriverID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT Found = 1 FROM Drivers WHERE DriverID = @DriverID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DriverID", (object)DriverID ?? DBNull.Value);

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
        public static DataTable GetAllDrivers()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM Drivers";

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

        public static DataTable GetAllDriversFormDriversView()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM DriversList_View";

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


        public static DataTable GetLocalDriverLicenses(int DirverID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                        string query = @"SELECT  dbo.Licenses.LicenseID, dbo.Licenses.ApplicationID, dbo.Licenses.LicenseClass, dbo.Licenses.IssueDate, dbo.Licenses.ExpirationDate, dbo.Licenses.IsActive
                                            FROM  dbo.Licenses INNER JOIN
                                             dbo.Applications ON dbo.Licenses.ApplicationID = dbo.Applications.ApplicationID INNER JOIN
                                             dbo.LocalDrivingLicenseApplications ON dbo.Applications.ApplicationID = dbo.LocalDrivingLicenseApplications.ApplicationID 
						                     WHERE DriverID=@DriverID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DriverID", DirverID);
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
        public static DataTable GetLocalDriverLicensesByPersonID(int PersonID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"SELECT Licenses.LicenseID, Licenses.ApplicationID, LicenseClasses.ClassName, Licenses.IssueDate, Licenses.ExpirationDate, Licenses.IsActive
                                     FROM Licenses INNER JOIN
                                     Applications ON Licenses.ApplicationID = Applications.ApplicationID INNER JOIN
                                     LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID INNER JOIN
                                     LicenseClasses ON Licenses.LicenseClass = LicenseClasses.LicenseClassID AND LocalDrivingLicenseApplications.LicenseClassID = LicenseClasses.LicenseClassID
                                     WHERE (ApplicantPersonID = @PersonID)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PersonID", PersonID);
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
        public static bool GetDriverInfoByPersonID(int PersonID,ref int? DriverID,
            ref int CreatedByUserID,ref DateTime CreatedDate)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Drivers WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;

                    DriverID = (int)reader["DriverID"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    CreatedDate = (DateTime)reader["CreatedDate"];

                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

       

    }
}
