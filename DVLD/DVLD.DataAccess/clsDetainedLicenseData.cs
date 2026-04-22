using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.DataAccess
{
    public class clsDetainedLicenseData
    {
        public static int AddNewDetainedLicense(int LicenseID, DateTime DetainedDate,
           float FineFees, int CreatedByUserID)
        {
            int DetainedID = -1;
            string query = @"INSERT INTO dbo.DetainedLicenses
                               (LicenseID, DetainedDate, FineFees, CreatedByUserID, IsReleased)
                            VALUES
                               (@LicenseID, @DetainedDate, @FineFees, @CreatedByUserID,0);
                            SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@LicenseID", SqlDbType.Int).Value = LicenseID;
                    command.Parameters.Add("@DetainedDate", SqlDbType.DateTime).Value = DetainedDate;
                    command.Parameters.Add("@FineFees", SqlDbType.SmallMoney).Value = FineFees;
                    command.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = CreatedByUserID;
                    try
                    {
                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            DetainedID = insertedID;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error: " + ex.Message);
                    }
                }
            }
            return DetainedID;
        }
        public static bool UpdateDetainedLicense(int DetainedID,int LicenseID, DateTime DetainedDate,
            float FineFees, int CreatedByUserID)
        {
            int rowsAffected = 0;
            string query = @"UPDATE dbo.DetainedLicenses
                              SET LicenseID = @LicenseID, 
                              DetainedDate  = @DetainedDate, 
                              FineFees = @FineFees,
                              CreatedByUserID = @CreatedByUserID,   
                              WHERE DetainedID =@DetainedID ;";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@DetainedLicenseID", SqlDbType.Int).Value = DetainedID;
                    command.Parameters.Add("@LicenseID", SqlDbType.Int).Value = LicenseID;
                    command.Parameters.Add("@DetainedDate", SqlDbType.DateTime).Value = DetainedDate;
                    command.Parameters.Add("@FineFees", SqlDbType.SmallMoney).Value = FineFees;
                    command.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = CreatedByUserID;
                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        return false;
                        throw new Exception("Error: " + ex.Message);
                    }
                }
            }
            return (rowsAffected > 0);
        }

        public static bool GetDetainedLicenseInfoByID(int DetainedID ,
           ref int LicenseID, ref DateTime DetainedDate ,
           ref float FineFees, ref int CreatedByUserID,
           ref bool IsReleased, ref DateTime ReleasedDate ,
           ref int ReleasedByUserID, ref int ReleasedApplicationID)
        {
            bool isFound = false;
            string query = "SELECT * FROM DetainedLicenses WHERE DetainedID  = @DetainedID ";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@DetainedID ", SqlDbType.Int).Value= DetainedID ;

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            isFound = true;

                            LicenseID = Convert.ToInt32(reader["LicenseID"]);
                            DetainedDate  = Convert.ToDateTime(reader["DetainedDate"]);
                            FineFees = Convert.ToSingle(reader["FineFees"]);
                            CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);

                            IsReleased = Convert.ToBoolean(reader["IsReleased"]);

                            if (reader["ReleasedDate "] == DBNull.Value)

                                ReleasedDate  = DateTime.MaxValue;
                            else
                                ReleasedDate  = (DateTime)reader["ReleasedDate"];


                            if (reader["ReleasedByUserID"] == DBNull.Value)

                                ReleasedByUserID = -1;
                            else
                                ReleasedByUserID = (int)reader["ReleasedByUserID"];

                            if (reader["ReleasedApplicationID"] == DBNull.Value)

                                ReleasedApplicationID = -1;
                            else
                                ReleasedApplicationID = (int)reader["ReleasedApplicationID"];

                        }
                        else
                        {
                            isFound = false;
                        }

                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        isFound = false;
                        throw new Exception("Error: " + ex.Message);
                    }
                }
            }

            return isFound;
        }


        public static bool GetDetainedLicenseInfoByLicenseID(int LicenseID,ref int DetainedID , 
            ref DateTime DetainedDate ,ref float FineFees, ref int CreatedByUserID,ref bool IsReleased, 
            ref DateTime ReleasedDate ,ref int ReleasedByUserID, ref int ReleasedApplicationID)
        {
            bool isFound = false;
            string query = "SELECT top 1 * FROM DetainedLicenses WHERE LicenseID = @LicenseID order by DetainedID  desc";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@LicenseID", SqlDbType.Int).Value = LicenseID;

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            isFound = true;

                            DetainedID  = Convert.ToInt32(reader["DetainedID"]);
                            DetainedDate  = Convert.ToDateTime(reader["DetainedDate"]);
                            FineFees = Convert.ToSingle(reader["FineFees"]);
                            CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);

                            IsReleased = Convert.ToBoolean(reader["IsReleased"]);

                            if (reader["ReleasedDate"] == DBNull.Value)

                                ReleasedDate = DateTime.MaxValue;
                            else
                                ReleasedDate = (DateTime)reader["ReleasedDate"];


                            if (reader["ReleasedByUserID"] == DBNull.Value)

                                ReleasedByUserID = -1;
                            else
                                ReleasedByUserID = (int)reader["ReleasedByUserID"];

                            if (reader["ReleasedApplicationID"] == DBNull.Value)

                                ReleasedApplicationID = -1;
                            else
                                ReleasedApplicationID = (int)reader["ReleasedApplicationID"];

                        }
                        else
                        {
                            isFound = false;
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        isFound = false;
                        throw new Exception("Error: " + ex.Message);
                    }
                }
            }

            return isFound;
        }

        public static DataTable GetAllDetainedLicenses()
        {

            DataTable dt = new DataTable();
            string query = "select * from vwDetainedLicense order by IsReleased ,DetainedID ;";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            dt.Load(reader);
                        }
                        reader.Close();
                    }

                    catch (Exception ex)
                    {
                        throw new Exception("Error: " + ex.Message);
                    }
                }
            }

            return dt;

        }

       

        public static bool ReleaseDetainedLicense(int DetainedID ,
                 int ReleasedByUserID, int ReleasedApplicationID)
        {

            int rowsAffected = 0;
            string query = @"UPDATE dbo.DetainedLicenses
                              SET IsReleased = 1, 
                              ReleasedDate = @ReleasedDate, 
                              ReleasedApplicationID = @ReleasedApplicationID   
                              WHERE DetainedID =@DetainedID ;";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@DetainedID", SqlDbType.Int).Value = DetainedID ;
                    command.Parameters.Add("@ReleasedByUserID", SqlDbType.Int).Value = ReleasedByUserID;
                    command.Parameters.Add("@ReleasedApplicationID", SqlDbType.Int).Value = ReleasedApplicationID;
                    command.Parameters.AddWithValue("@ReleasedDate", SqlDbType.Int).Value = DateTime.Now;
                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        return false;
                        throw new Exception("Error: " + ex.Message);
                    }
                }
            }
            return (rowsAffected > 0);
        }

        public static bool IsLicenseDetained(int LicenseID)
        {
            bool IsDetained = false;
            string query = @"select IsDetained=1 
                            from detainedLicenses 
                            where 
                            LicenseID=@LicenseID 
                            and IsReleased=0;";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@LicenseID", SqlDbType.Int).Value = LicenseID;

                    try
                    {
                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            IsDetained = Convert.ToBoolean(result);
                        }
                    }

                    catch (Exception ex)
                    {
                        throw new Exception("Error: " + ex.Message);

                    }
                }
            }
            return IsDetained;
            ;

        }

    }
}
