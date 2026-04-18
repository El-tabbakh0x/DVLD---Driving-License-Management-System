using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DVLD.DataAccess
{
    public class clsTestData
    {
        public static bool GetTestInfoByID(int TestID,
            ref int TestAppointmentID, ref bool TestResult,
            ref string Notes, ref int CreatedByUserID)
        {
            bool isFound = false;
            string query = "SELECT * FROM Tests WHERE TestID = @TestID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@TestID", SqlDbType.Int).Value = TestID;
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            isFound = true;
                            TestAppointmentID = Convert.ToInt32(reader["TestAppointmentID"]);
                            TestResult = Convert.ToBoolean(reader["TestResult"]);
                            if (reader["Notes"] == DBNull.Value)

                                Notes = "";
                            else
                                Notes = Convert.ToString(reader["Notes"]);

                            CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
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
        public static bool GetLastTestByPersonAndTestTypeAndLicenseClass
            (int PersonID, int LicenseClassID, int TestTypeID, ref int TestID,
              ref int TestAppointmentID, ref bool TestResult,
              ref string Notes, ref int CreatedByUserID)
        {
            bool isFound = false;
            string query = @"SELECT  top 1 Tests.TestID, 
                Tests.TestAppointmentID, Tests.TestResult, 
			    Tests.Notes, Tests.CreatedByUserID, Applications.PersonID
                FROM    LocalDrivingLicenseApplications INNER JOIN
                                 Tests INNER JOIN
                                 TestAppointments ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID
                                ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID
                                INNER JOIN
                                 Applications ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
                WHERE        (Applications.PersonID = @PersonID) 
                        AND (LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID)
                        AND ( TestAppointments.TestTypeID=@TestTypeID)
                ORDER BY Tests.TestAppointmentID DESC";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@PersonID", SqlDbType.Int).Value = PersonID;
                    command.Parameters.Add("@LicenseClassID", SqlDbType.Int).Value = LicenseClassID;
                    command.Parameters.Add("@TestTypeID", SqlDbType.Int).Value = TestTypeID;
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            isFound = true;
                            TestID = Convert.ToInt32(reader["TestID"]);
                            TestAppointmentID = Convert.ToInt32(reader["TestAppointmentID"]);
                            TestResult = Convert.ToBoolean(reader["TestResult"]);
                            if (reader["Notes"] == DBNull.Value)

                                Notes = "";
                            else
                                Notes = Convert.ToString(reader["Notes"]);

                            CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
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
        public static DataTable GetAllTests()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM Tests order by TestID";

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

        public static int AddNewTest(int TestAppointmentID, bool TestResult,
             string Notes, int CreatedByUserID)
        {
            int TestID = -1;
            string query = @"Insert Into Tests (TestAppointmentID,TestResult,
                                                Notes,   CreatedByUserID)
                            Values (@TestAppointmentID,@TestResult,
                                                @Notes,   @CreatedByUserID);
                            
                                UPDATE TestAppointments 
                                SET IsLocked=1 where TestAppointmentID = @TestAppointmentID;

                                SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@TestAppointmentID", SqlDbType.Int).Value = TestAppointmentID;
                    command.Parameters.Add("@TestResult", SqlDbType.Bit).Value = TestResult;

                    if (Notes != "" && Notes != null)
                        command.Parameters.Add("@Notes", SqlDbType.NVarChar,500).Value = Notes;
                    else
                        command.Parameters.Add("@Notes", SqlDbType.NVarChar, 500).Value = System.DBNull.Value;

                    command.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = CreatedByUserID;

                    try
                    {
                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            TestID = insertedID;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error: " + ex.Message);
                    }
                }
            }
            return TestID;
        }
        public static bool UpdateTest(int TestID, int TestAppointmentID, bool TestResult,
             string Notes, int CreatedByUserID)
        {
            int rowsAffected = 0;
            string query = @"Update  Tests  
                            set TestAppointmentID = @TestAppointmentID,
                                TestResult=@TestResult,
                                Notes = @Notes,
                                CreatedByUserID=@CreatedByUserID
                                where TestID = @TestID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@TestID", SqlDbType.Int).Value = TestID;
                            command.Parameters.Add("@TestAppointmentID", SqlDbType.Int).Value = TestAppointmentID;
                    command.Parameters.Add("@TestResult", SqlDbType.Bit).Value = TestResult;
                    command.Parameters.Add("@Notes", SqlDbType.NVarChar,500).Value = Notes;
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

        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            byte PassedTestCount = 0;
            string query = @"SELECT PassedTestCount = count(TestTypeID)
                         FROM Tests INNER JOIN
                         TestAppointments ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID
						 where LocalDrivingLicenseApplicationID =@LocalDrivingLicenseApplicationID and TestResult=1";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@LocalDrivingLicenseApplicationID", SqlDbType.Int).Value = LocalDrivingLicenseApplicationID ;
                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && byte.TryParse(result.ToString(), out byte ptCount))
                        {
                            PassedTestCount = ptCount;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error: " + ex.Message);
                    }
                }
            }
            return PassedTestCount;
        }
    }
}
