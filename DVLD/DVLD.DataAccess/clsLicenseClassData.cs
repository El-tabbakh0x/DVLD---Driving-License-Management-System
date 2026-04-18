using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.DataAccess
{
    public class clsLicenseClassData
    {
        public static bool GetLicenseClassInfoByID(int LicenseClassID,
            ref string LicenseClassName , ref string ClassDescription, ref byte MinimumeAllowedAge,
            ref byte DefaultValidityLength, ref float ClassFees)
        {
            bool isFound = false;
            string query = "SELECT * FROM LicenseClasses WHERE LicenseClassID = @LicenseClassID";
            using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@LicenseClassID", SqlDbType.Int).Value = LicenseClassID;
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            isFound = true;

                            LicenseClassName  = Convert.ToString(reader["LicenseClassName"]);
                            ClassDescription = Convert.ToString(reader["ClassDescription"]);
                            MinimumeAllowedAge = Convert.ToByte(reader["MinimumeAllowedAge"]);
                            DefaultValidityLength = Convert.ToByte(reader["DefaultValidityLength"]);
                            ClassFees = Convert.ToSingle(reader["ClassFees"]);
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
        public static bool GetLicenseClassInfoByLicenseClassName (string LicenseClassName , ref int LicenseClassID,
            ref string ClassDescription, ref byte MinimumeAllowedAge,
           ref byte DefaultValidityLength, ref float ClassFees)
        {
            bool isFound = false;
            string query = "SELECT * FROM LicenseClasses WHERE LicenseClassName  = @LicenseClassName ";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@LicenseClassName ",SqlDbType.NVarChar,50 ).Value = LicenseClassName ;

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            isFound = true;
                            LicenseClassID = Convert.ToByte(reader["LicenseClassID"]);
                            ClassDescription = Convert.ToString(reader["ClassDescription"]);
                            MinimumeAllowedAge = Convert.ToByte(reader["MinimumeAllowedAge"]);
                            DefaultValidityLength = Convert.ToByte(reader["DefaultValidityLength"]);
                            ClassFees = Convert.ToSingle(reader["ClassFees"]);
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
        public static DataTable GetAllLicenseClasses()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM LicenseClasses order by LicenseClassName ";
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

        public static int AddNewLicenseClass(string LicenseClassName , string ClassDescription,
            byte MinimumeAllowedAge, byte DefaultValidityLength, float ClassFees)
        {
            int LicenseClassID = -1;
            string query = @"Insert Into LicenseClasses 
           (
            LicenseClassName ,ClassDescription,MinimumeAllowedAge, 
            DefaultValidityLength,ClassFees)
                            Values ( 
            @LicenseClassName ,@ClassDescription,@MinimumeAllowedAge, 
            @DefaultValidityLength,@ClassFees)
                            where LicenseClassID = @LicenseClassID;
                            SELECT SCOPE_IDENTITY();";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@LicenseClassName ", SqlDbType.NVarChar, 50).Value = LicenseClassName ;
                    command.Parameters.Add("@ClassDescription", SqlDbType.NVarChar, 500).Value = ClassDescription;
                    command.Parameters.Add("@MinimumeAllowedAge", SqlDbType.TinyInt).Value = MinimumeAllowedAge;
                    command.Parameters.Add("@DefaultValidityLength", SqlDbType.TinyInt).Value = DefaultValidityLength;
                    command.Parameters.Add("@ClassFees", SqlDbType.SmallMoney).Value = ClassFees;
                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            LicenseClassID = insertedID;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error: " + ex.Message);

                    }
                }
            }
            return LicenseClassID;
        }

        public static bool UpdateLicenseClass(int LicenseClassID, string LicenseClassName ,
            string ClassDescription,
            byte MinimumeAllowedAge, byte DefaultValidityLength, float ClassFees)
        {
            int rowsAffected = 0;
            string query = @"Update  LicenseClasses  
                            set LicenseClassName  = @LicenseClassName ,
                                ClassDescription = @ClassDescription,
                                MinimumeAllowedAge = @MinimumeAllowedAge,
                                DefaultValidityLength = @DefaultValidityLength,
                                ClassFees = @ClassFees
                                where LicenseClassID = @LicenseClassID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LicenseClassID", SqlDbType.Int).Value = LicenseClassID;
                    command.Parameters.Add("@LicenseClassName ", SqlDbType.NVarChar, 50).Value = LicenseClassName ;
                    command.Parameters.Add("@ClassDescription", SqlDbType.NVarChar, 500).Value = ClassDescription;
                    command.Parameters.Add("@MinimumeAllowedAge", SqlDbType.TinyInt).Value = MinimumeAllowedAge;
                    command.Parameters.Add("@DefaultValidityLength", SqlDbType.TinyInt).Value = DefaultValidityLength;
                    command.Parameters.Add("@ClassFees", SqlDbType.SmallMoney).Value = ClassFees;
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
