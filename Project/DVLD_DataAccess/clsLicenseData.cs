using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsLicenseData
    {
        public static bool GetLicenseByID(int? LicenseID, ref int ApplicationID, ref int DriverID, ref int LicenseClass, ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref decimal PaidFees, ref bool IsActive, ref byte IssueReason, ref int CreatedByUserID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM Licenses WHERE LicenseID = @LicenseID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LicenseID", (object)LicenseID ?? DBNull.Value);

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;


                                ApplicationID = (int)reader["ApplicationID"];
                                DriverID = (int)reader["DriverID"];
                                LicenseClass = (int)reader["LicenseClass"];
                                IssueDate = (DateTime)reader["IssueDate"];
                                ExpirationDate = (DateTime)reader["ExpirationDate"];
                                Notes = (reader["Notes"] != DBNull.Value) ? (string)reader["Notes"] : null;
                                PaidFees = (decimal)reader["PaidFees"];
                                IsActive = (bool)reader["IsActive"];
                                IssueReason = (byte)reader["IssueReason"];
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
        public static int AddNewLicense(int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate, DateTime ExpirationDate, string Notes, decimal PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            int LicenseID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"INSERT INTO Licenses (ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID)
                            VALUES (@ApplicationID, @DriverID, @LicenseClass, @IssueDate, @ExpirationDate, @Notes, @PaidFees, @IsActive, @IssueReason, @CreatedByUserID)
                            SELECT SCOPE_IDENTITY();";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                        command.Parameters.AddWithValue("@DriverID", DriverID);
                        command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
                        command.Parameters.AddWithValue("@IssueDate", IssueDate);
                        command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
                        command.Parameters.AddWithValue("@Notes", (object)Notes ?? DBNull.Value);
                        command.Parameters.AddWithValue("@PaidFees", PaidFees);
                        command.Parameters.AddWithValue("@IsActive", IsActive);
                        command.Parameters.AddWithValue("@IssueReason", IssueReason);
                        command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                            LicenseID = insertedID;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return LicenseID;
        }
        public static bool UpdateLicense(int? LicenseID, int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate, DateTime ExpirationDate, string Notes, decimal PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"UPDATE Licenses  
                            SET 
                            ApplicationID = @ApplicationID, 
                            DriverID = @DriverID, 
                            LicenseClass = @LicenseClass, 
                            IssueDate = @IssueDate, 
                            ExpirationDate = @ExpirationDate, 
                            Notes = @Notes, 
                            PaidFees = @PaidFees, 
                            IsActive = @IsActive, 
                            IssueReason = @IssueReason, 
                            CreatedByUserID = @CreatedByUserID
                            WHERE LicenseID = @LicenseID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@LicenseID", LicenseID);
                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                        command.Parameters.AddWithValue("@DriverID", DriverID);
                        command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
                        command.Parameters.AddWithValue("@IssueDate", IssueDate);
                        command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
                        command.Parameters.AddWithValue("@Notes", (object)Notes ?? DBNull.Value);
                        command.Parameters.AddWithValue("@PaidFees", PaidFees);
                        command.Parameters.AddWithValue("@IsActive", IsActive);
                        command.Parameters.AddWithValue("@IssueReason", IssueReason);
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
        public static bool DeleteLicense(int? LicenseID)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"Delete Licenses 
                                where LicenseID = @LicenseID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LicenseID", (object)LicenseID ?? DBNull.Value);

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
        public static bool DoesLicenseExist(int? LicenseID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT Found = 1 FROM Licenses WHERE LicenseID = @LicenseID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LicenseID", (object)LicenseID ?? DBNull.Value);

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
        public static DataTable GetAllLicenses()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM Licenses";

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
        public static bool GetLicenseByApplicationID(int ApplicationID, ref int LicenseID, ref int DriverID, ref int LicenseClass, ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref decimal PaidFees, ref bool IsActive, ref byte IssueReason, ref int CreatedByUserID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM Licenses WHERE ApplicationID = @ApplicationID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;


                                LicenseID = (int)reader["LicenseID"];
                                DriverID = (int)reader["DriverID"];
                                LicenseClass = (int)reader["LicenseClass"];
                                IssueDate = (DateTime)reader["IssueDate"];
                                ExpirationDate = (DateTime)reader["ExpirationDate"];
                                Notes = (reader["Notes"] != DBNull.Value) ? (string)reader["Notes"] : null;
                                PaidFees = (decimal)reader["PaidFees"];
                                IsActive = (bool)reader["IsActive"];
                                IssueReason = (byte)reader["IssueReason"];
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
        public static int GetActiveLicenseID(int ApplicationID,int LicenseClass)
        {
            int LicenseID=-1;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query =
                        "SELECT LicenseID FROM Licenses WHERE ApplicationID=@ApplicationID AND LicenseClass=@LicenseClass AND IsActive=1;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                        command.Parameters.AddWithValue("@LicenseClass", LicenseClass);

                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int activeLicenseID))
                            LicenseID = activeLicenseID;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            

            return LicenseID;
        }

        public static bool HasIssuedLicenseForSameClass(int PersonId , int LicenessClass)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"SELECT Found = 1 FROM  Licenses INNER JOIN Drivers ON Licenses.DriverID = Drivers.DriverID INNER JOIN
                                   People ON Drivers.PersonID = People.PersonID  WHERE  Drivers.PersonID =@PersonId AND LicenseClass =@LicenessClass AND IsActive=1";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PersonId", PersonId);
                        command.Parameters.AddWithValue("@LicenessClass", LicenessClass);

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
