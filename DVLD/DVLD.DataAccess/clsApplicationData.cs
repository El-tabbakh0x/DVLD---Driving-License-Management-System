using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD.DataAccess
{
    public class clsApplicationData
    {
        public static int AddNewApplication(int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID,
     byte ApplicationStatus, DateTime LastStatusDate,
     float PaidFees, int CreatedByUserID)
        {
            int ApplicationID = -1;
            string query = @"INSERT INTO Applications ( 
                            PersonID ,ApplicationDate,ApplicationTypeID,
                            ApplicationStatusID, LastStatusDate,
                            PaidFees,CreatedByUserID)
                             VALUES (@PersonID,@ApplicationDate,@ApplicationTypeID,
                                      @ApplicationStatusID, @LastStatusDate,
                                      @PaidFees, @CreatedByUserID);
                             SELECT SCOPE_IDENTITY();";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@PersonID ", SqlDbType.Int).Value = ApplicantPersonID;
                    command.Parameters.Add("@ApplicationDate", SqlDbType.DateTime).Value = ApplicationDate;
                    command.Parameters.Add("@ApplicationTypeID", SqlDbType.Int).Value = ApplicationTypeID;
                    command.Parameters.Add("@ApplicationStatusID ", SqlDbType.Int).Value = ApplicationStatus;
                    command.Parameters.Add("@LastStatusDate", SqlDbType.DateTime).Value = @LastStatusDate;
                    command.Parameters.Add("@PaidFees", SqlDbType.SmallMoney).Value = @PaidFees;
                    command.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = @CreatedByUserID;
                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null )
                        {
                            ApplicationID =Convert.ToInt32(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("AddNewApplication Error: " + ex.Message);
                    }
                }
            }
            return ApplicationID;
        }
        public static bool UpdateApplication(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID,
             byte ApplicationStatus, DateTime LastStatusDate,
             float PaidFees, int CreatedByUserID)
        {
            int rowsAffected = 0;
            string query = @"Update  Applications  
                            set ApplicantPersonID = @ApplicantPersonID,
                                ApplicationDate = @ApplicationDate,
                                ApplicationTypeID = @ApplicationTypeID,
                                ApplicationStatus = @ApplicationStatus, 
                                LastStatusDate = @LastStatusDate,
                                PaidFees = @PaidFees,
                                CreatedByUserID=@CreatedByUserID
                            where ApplicationID=@ApplicationID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@ApplicationID", SqlDbType.Int).Value = ApplicationID;
                    command.Parameters.Add("@ApplicantPersonID", SqlDbType.Int).Value = ApplicantPersonID;
                    command.Parameters.Add("@ApplicationDate", SqlDbType.DateTime).Value = ApplicationDate;
                    command.Parameters.Add("@ApplicationTypeID", SqlDbType.Int).Value = ApplicationTypeID;
                    command.Parameters.Add("@ApplicationStatus", SqlDbType.Int).Value = ApplicationStatus;
                    command.Parameters.Add("@@LastStatusDate", SqlDbType.DateTime).Value = @LastStatusDate;
                    command.Parameters.Add("@@PaidFees", SqlDbType.SmallMoney).Value = @PaidFees;
                    command.Parameters.Add("@@CreatedByUserID", SqlDbType.Int).Value = @CreatedByUserID;
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
        public static bool DeleteApplication(int ApplicationID)
        {
            int rowsAffected = 0;
            string query = @"Delete Applications 
                                where ApplicationID = @ApplicationID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@ApplicationID", SqlDbType.Int).Value = ApplicationID;
                    try
                    {
                        connection.Open();

                        rowsAffected = command.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error: " + ex.Message);
                    }
                }
            }
            return (rowsAffected > 0);
        }
        public static bool GetApplicationInfoByID(int ApplicationID,
          ref int ApplicantPersonID, ref DateTime ApplicationDate, ref int ApplicationTypeID,
          ref byte ApplicationStatus, ref DateTime LastStatusDate,
          ref float PaidFees, ref int CreatedByUserID)
        {
            bool isFound = false;
            string query = "SELECT * FROM Applications WHERE ApplicationID = @ApplicationID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@ApplicationID",SqlDbType.Int ).Value = ApplicationID;
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            isFound = true;
                            ApplicantPersonID = Convert.ToInt32(reader["ApplicantPersonID"]);
                            ApplicationDate = Convert.ToDateTime(reader["ApplicationDate"]);
                            ApplicationTypeID = Convert.ToInt32(reader["ApplicationTypeID"]);
                            ApplicationStatus = Convert.ToByte(reader["ApplicationStatus"]);
                            LastStatusDate = Convert.ToDateTime(reader["LastStatusDate"]);
                            PaidFees = Convert.ToSingle(reader["PaidFees"]);
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
                        throw new Exception("Get Application Info By ID Error: " + ex.Message);
                    }
                }
            }
            return isFound;
        }
        public static DataTable GetAllApplications()
        {
            DataTable dt = new DataTable();
            string query = "select * from ApplicationsList_View order by ApplicationDate desc";
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
        public static bool IsApplicationExist(int ApplicationID)
        {
            bool isFound = false;
            string query = "SELECT Found=1 FROM Applications WHERE ApplicationID = @ApplicationID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@ApplicationID", SqlDbType.Int).Value = ApplicationID;
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        isFound = reader.HasRows;
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
        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return (GetActiveApplicationID(PersonID, ApplicationTypeID) != -1);
        }
        public static int GetActiveApplicationID(int PersonID, int ApplicationTypeID)
        {
            int ActiveApplicationID = -1;
            string query = "SELECT ActiveApplicationID=ApplicationID FROM Applications WHERE ApplicantPersonID = @ApplicantPersonID and ApplicationTypeID=@ApplicationTypeID and ApplicationStatus=1";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@ApplicantPersonID", SqlDbType.Int).Value = PersonID;
                    command.Parameters.Add("@ApplicationTypeID", SqlDbType.Int).Value = ApplicationTypeID;
                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            ActiveApplicationID = Convert.ToInt32(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        return ActiveApplicationID;
                        throw new Exception("Error: " + ex.Message);
                    }
                }
            }
                    return ActiveApplicationID;
        }
        public static int GetActiveApplicationIDForLicenseClass(int PersonID, int ApplicationTypeID, int LicenseClassID)
        {
            int ActiveApplicationID = -1;
            string query = @"SELECT ActiveApplicationID=Applications.ApplicationID  
                            From
                            Applications INNER JOIN
                            LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                            WHERE ApplicantPersonID = @ApplicantPersonID 
                            and ApplicationTypeID=@ApplicationTypeID 
							and LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID
                            and ApplicationStatus=1";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@ApplicantPersonID", SqlDbType.Int).Value = PersonID;
                    command.Parameters.Add("@ApplicationTypeID", SqlDbType.Int).Value = ApplicationTypeID;
                    command.Parameters.Add("@LicenseClassID", SqlDbType.Int).Value = LicenseClassID;
                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            ActiveApplicationID = Convert.ToInt32(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        return ActiveApplicationID;
                        throw new Exception("Error: " + ex.Message);
                    }
                }
            }
                return ActiveApplicationID;
        }
        public static bool UpdateStatus(int ApplicationID, short NewStatus)
        {
            int rowsAffected = 0;
            string query = @"Update  Applications  
                            set 
                                ApplicationStatus = @NewStatus, 
                                LastStatusDate = @LastStatusDate
                            where ApplicationID=@ApplicationID;";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@ApplicationID", SqlDbType.Int).Value = ApplicationID;
                    command.Parameters.Add("@NewStatus", SqlDbType.Int).Value = NewStatus;
                    command.Parameters.Add("@LastStatusDate", SqlDbType.DateTime).Value = DateTime.Now;
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
