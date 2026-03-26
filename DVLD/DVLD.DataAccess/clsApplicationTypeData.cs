using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.DataAccess
{
    public class clsApplicationTypeData
    {
        public static bool GetApplicationTypeInfoByID(int ApplicationTypeID,
           ref string ApplicationTypeTitle, ref float ApplicationFees)
        {
            bool isFound = false;
            string query = "SELECT * FROM ApplicationTypes WHERE ApplicationTypeID = @ApplicationTypeID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                { 
                   command.Parameters.Add("@ApplicationTypeID", SqlDbType.Int).Value = ApplicationTypeID;
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            isFound = true;
                            ApplicationTypeTitle = (string)reader["ApplicationTypeTitle"];
                            ApplicationFees = Convert.ToSingle(reader["ApplicationFees"]);
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
                        throw new Exception("Get Application Type Info By ID Error: " + ex.Message);
                    }
                }
            }
            return isFound;
        }

        public static DataTable GetAllApplicationTypes()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM ApplicationTypes order by ApplicationTypeTitle";
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
                        throw new Exception("GetAllApplicationTypes Error: " + ex.Message);
                    }
                }
            }
            return dt;
        }

        public static int AddNewApplicationType(string Title, float Fees)
        {
            int ApplicationTypeID = -1;
            string query = @"Insert Into ApplicationTypes (ApplicationTypeTitle,ApplicationFees)
                            Values (@Title,@Fees)
                            
                            SELECT SCOPE_IDENTITY();";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                command.Parameters.Add("@ApplicationTypeTitle", SqlDbType.NVarChar,50).Value= Title;
                command.Parameters.Add("@ApplicationFees", SqlDbType.SmallMoney).Value = Fees;
                    try
                    {
                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            ApplicationTypeID = insertedID;
                        }
                    }

                    catch (Exception ex)
                    {
                        throw new Exception("Add New Application Type Error: " + ex.Message);
                    }
                }
            }
            return ApplicationTypeID;
        }

        public static bool UpdateApplicationType(int ApplicationTypeID, string Title, float Fees)
        {

            int rowsAffected = 0;
            string query = @"Update  ApplicationTypes  
                            set ApplicationTypeTitle = @Title,
                                ApplicationFees = @Fees
                                where ApplicationTypeID = @ApplicationTypeID";
           using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@ApplicationTypeID", SqlDbType.Int).Value = ApplicationTypeID;
                    command.Parameters.Add("@Title", SqlDbType.NVarChar, 50).Value = Title;
                    command.Parameters.Add("@Fees", SqlDbType.SmallMoney).Value = Fees;

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        return false;
                        throw new Exception("Update Application Type Error: " + ex.Message);

                    }
                }
            }

            return (rowsAffected > 0);
        }

    }
}

