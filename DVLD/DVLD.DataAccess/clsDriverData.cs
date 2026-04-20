using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.DataAccess
{
    public class clsDriverData
    {
        public static bool GetDriverInfoByDriverID(int DriverID,
           ref int PersonID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool isFound = false;
            string query = "SELECT * FROM Drivers WHERE DriverID = @DriverID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@DriverID", SqlDbType.Int).Value = DriverID;

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            isFound = true;
                            PersonID = Convert.ToInt32(reader["PersonID"]);
                            CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                            CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
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

        public static bool GetDriverInfoByPersonID(int PersonID, ref int DriverID,
            ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool isFound = false;
            string query = "SELECT * FROM Drivers WHERE PersonID = @PersonID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@PersonID", SqlDbType.Int).Value = PersonID;
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            isFound = true;

                            DriverID = Convert.ToInt32(reader["DriverID"]);
                            CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                            CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
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

        public static DataTable GetAllDrivers()
        {

            DataTable dt = new DataTable();
            string query = "SELECT * FROM vwDriver order by FullName";

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

        public static int AddNewDriver(int PersonID, int CreatedByUserID)
        {
            int DriverID = -1;
            string query = @"Insert Into Drivers (PersonID,CreatedByUserID,CreatedDate)
                            Values (@PersonID,@CreatedByUserID,@CreatedDate);
                          
                            SELECT SCOPE_IDENTITY();";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", SqlDbType.Int).Value = PersonID;
                    command.Parameters.AddWithValue("@CreatedByUserID", SqlDbType.Int).Value = CreatedByUserID;
                    command.Parameters.AddWithValue("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            DriverID = insertedID;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error: " + ex.Message);
                    }
                }
            }
                    return DriverID;
        }

        public static bool UpdateDriver(int DriverID, int PersonID, int CreatedByUserID)
        {
            int rowsAffected = 0;
            string query = @"Update  Drivers  
                            set PersonID = @PersonID,
                                CreatedByUserID = @CreatedByUserID
                                where DriverID = @DriverID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DriverID", SqlDbType.Int).Value = DriverID;
                    command.Parameters.AddWithValue("@PersonID", SqlDbType.Int).Value = PersonID;
                    command.Parameters.AddWithValue("@CreatedByUserID", SqlDbType.Int).Value = CreatedByUserID;
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
    }
}
