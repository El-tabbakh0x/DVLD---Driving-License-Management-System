using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.CodeDom;

namespace DVLD.DataAccess
{
    public class clsTestTypeData
    {
        public static bool GetTestTypeInfoByID(int TestTypeID,
            ref string TestTypeTitle, ref string TestDescription, ref float TestFees)
        {
            bool isFound = false;
            string query = "SELECT * FROM TestTypes WHERE TestTypeID = @TestTypeID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@TestTypeID", SqlDbType.Int ).Value=TestTypeID;

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            isFound = true;

                            TestTypeTitle = (string)reader["TestTypeTitle"];
                            TestDescription = (string)reader["TestTypeDescription"];
                            TestFees = Convert.ToSingle(reader["TestTypeFees"]);
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
                        throw new Exception("Get Test Type Info By ID Error: " + ex.Message);
                    }

                }
            }
            return isFound;
        }

        public static DataTable GetAllTestTypes()
        {

            DataTable dt = new DataTable();
            string query = "SELECT * FROM TestTypes order by TestTypeID";
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
                        throw new Exception("Get All Test Types Error: " + ex.Message);
                    }
                }
            }
        
            return dt;
        }

        public static int AddNewTestType(string Title, string Description, float Fees)
        {
            int TestTypeID = -1;
            string query = @"Insert Into TestTypes (TestTypeTitle,TestTypeTitle,TestTypeFees)
                            Values (@TestTypeTitle,@TestTypeDescription,@ApplicationFees)
                            where TestTypeID = @TestTypeID;
                            SELECT SCOPE_IDENTITY();";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@TestTypeTitle", SqlDbType.NVarChar,50).Value = Title;
                    command.Parameters.Add("@TestTypeDescription", SqlDbType.NVarChar,500).Value = Description;
                    command.Parameters.Add("@ApplicationFees", SqlDbType.SmallMoney).Value = Fees;
                    try
                    {
                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            TestTypeID = insertedID;
                        }
                    }

                    catch (Exception ex)
                    {
                        throw new Exception("Add New Test Type Error: " + ex.Message);

                    }

                }
            }


            return TestTypeID;

        }

        public static bool UpdateTestType(int TestTypeID, string Title, string Description, float Fees)
        {
            int rowsAffected = 0;
            string query = @"Update  TestTypes  
                            set TestTypeTitle = @TestTypeTitle,
                                TestTypeDescription=@TestTypeDescription,
                                TestTypeFees = @TestTypeFees
                                where TestTypeID = @TestTypeID";

           using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TestTypeID", SqlDbType.Int).Value = TestTypeID;
                    command.Parameters.Add("@TestTypeTitle", SqlDbType.NVarChar, 50).Value = Title;
                    command.Parameters.Add("@TestTypeDescription", SqlDbType.NVarChar, 500).Value = Description;
                    command.Parameters.Add("@ApplicationFees", SqlDbType.SmallMoney).Value = Fees;

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        return false;
                        throw new Exception("Update Test Type Error: " + ex.Message);
                    }
                }
            }

            return (rowsAffected > 0);
        }

    }
}
