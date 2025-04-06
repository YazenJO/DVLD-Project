using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsDetainData
    {
        public static bool GetDetainByID(int? DetainID, ref int LicenseID, ref DateTime DetainDate, ref decimal FineFees, ref int CreatedByUserID, ref bool IsReleased, ref DateTime? ReleaseDate, ref int? ReleasedByUserID, ref int? ReleaseApplicationID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM DetainedLicenses WHERE DetainID = @DetainID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DetainID", (object)DetainID ?? DBNull.Value);

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;


                                LicenseID = (int)reader["LicenseID"];
                                DetainDate = (DateTime)reader["DetainDate"];
                                FineFees = (decimal)reader["FineFees"];
                                CreatedByUserID = (int)reader["CreatedByUserID"];
                                IsReleased = (bool)reader["IsReleased"];
                                ReleaseDate = (reader["ReleaseDate"] != DBNull.Value) ? (DateTime?)reader["ReleaseDate"] : null;
                                ReleasedByUserID = (reader["ReleasedByUserID"] != DBNull.Value) ? (int?)reader["ReleasedByUserID"] : null;
                                ReleaseApplicationID = (reader["ReleaseApplicationID"] != DBNull.Value) ? (int?)reader["ReleaseApplicationID"] : null;
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
        public static int AddNewDetain(int LicenseID, DateTime DetainDate, decimal FineFees, int CreatedByUserID, bool IsReleased, DateTime? ReleaseDate, int? ReleasedByUserID, int? ReleaseApplicationID)
        {
            int DetainID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"INSERT INTO DetainedLicenses (LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID)
                            VALUES (@LicenseID, @DetainDate, @FineFees, @CreatedByUserID, @IsReleased, @ReleaseDate, @ReleasedByUserID, @ReleaseApplicationID)
                            SELECT SCOPE_IDENTITY();";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@LicenseID", LicenseID);
                        command.Parameters.AddWithValue("@DetainDate", DetainDate);
                        command.Parameters.AddWithValue("@FineFees", FineFees);
                        command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                        command.Parameters.AddWithValue("@IsReleased", IsReleased);
                        command.Parameters.AddWithValue("@ReleaseDate", (object)ReleaseDate ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ReleasedByUserID", (object)ReleasedByUserID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ReleaseApplicationID", (object)ReleaseApplicationID ?? DBNull.Value);

                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                            DetainID = insertedID;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return DetainID;
        }
        public static bool UpdateDetain(int? DetainID, int LicenseID, DateTime DetainDate, decimal FineFees, int CreatedByUserID, bool IsReleased, DateTime? ReleaseDate, int? ReleasedByUserID, int? ReleaseApplicationID)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"UPDATE DetainedLicenses  
                            SET 
                            LicenseID = @LicenseID, 
                            DetainDate = @DetainDate, 
                            FineFees = @FineFees, 
                            CreatedByUserID = @CreatedByUserID, 
                            IsReleased = @IsReleased, 
                            ReleaseDate = @ReleaseDate, 
                            ReleasedByUserID = @ReleasedByUserID, 
                            ReleaseApplicationID = @ReleaseApplicationID
                            WHERE DetainID = @DetainID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@DetainID", DetainID);
                        command.Parameters.AddWithValue("@LicenseID", LicenseID);
                        command.Parameters.AddWithValue("@DetainDate", DetainDate);
                        command.Parameters.AddWithValue("@FineFees", FineFees);
                        command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                        command.Parameters.AddWithValue("@IsReleased", IsReleased);
                        command.Parameters.AddWithValue("@ReleaseDate", (object)ReleaseDate ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ReleasedByUserID", (object)ReleasedByUserID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ReleaseApplicationID", (object)ReleaseApplicationID ?? DBNull.Value);

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
        public static bool DeleteDetain(int? DetainID)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"Delete DetainedLicenses 
                                where DetainID = @DetainID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DetainID", (object)DetainID ?? DBNull.Value);

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
        public static bool DoesLicenseDetained(int LicenseID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT Found = 1 FROM DetainedLicenses WHERE LicenseID = @LicenseID AND IsReleased=0";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LicenseID", LicenseID);

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
        public static bool DoesDetainExist(int? DetainID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT Found = 1 FROM DetainedLicenses WHERE DetainID = @DetainID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DetainID", (object)DetainID ?? DBNull.Value);

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
        public static DataTable GetAllDetainedLicenses()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "select * from detainedLicenses_View order by IsReleased ,DetainID";

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
        public static bool GetDetainByLicenseID( int LicenseID,ref int? DetainID,  ref DateTime DetainDate, ref decimal FineFees, ref int CreatedByUserID, ref bool IsReleased, ref DateTime? ReleaseDate, ref int? ReleasedByUserID, ref int? ReleaseApplicationID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM DetainedLicenses WHERE LicenseID = @LicenseID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LicenseID", LicenseID);
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;


                                DetainID = (int)reader["DetainID"];
                                DetainDate = (DateTime)reader["DetainDate"];
                                FineFees = (decimal)reader["FineFees"];
                                CreatedByUserID = (int)reader["CreatedByUserID"];
                                IsReleased = (bool)reader["IsReleased"];
                                ReleaseDate = (reader["ReleaseDate"] != DBNull.Value) ? (DateTime?)reader["ReleaseDate"] : null;
                                ReleasedByUserID = (reader["ReleasedByUserID"] != DBNull.Value) ? (int?)reader["ReleasedByUserID"] : null;
                                ReleaseApplicationID = (reader["ReleaseApplicationID"] != DBNull.Value) ? (int?)reader["ReleaseApplicationID"] : null;
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
