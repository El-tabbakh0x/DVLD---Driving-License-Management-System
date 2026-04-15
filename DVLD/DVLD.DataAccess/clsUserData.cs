using DVLD.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.DataAccess
{
    public class clsUserData
    {
        public static int AddUser(int PersonID, string UserName, string PasswordHash, bool IsActive)
        {
            string query = @"insert into Users  " +
                "( PersonID, UserName, PasswordHash, IsActive)" +
                  "Values" +
                  "( @PersonID, @UserName, @PasswordHash, @IsActive);" +
                  "select SCOPE_IDENTITY(); ";
            using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand(query, Connection))
                {
                    Command.Parameters.Add("@PersonID", SqlDbType.Int).Value = PersonID;
                    Command.Parameters.Add("@UserName", SqlDbType.NVarChar, 50).Value = UserName;
                    Command.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 256).Value = PasswordHash;
                    Command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = IsActive;
                    try
                    {
                        Connection.Open();
                        object result = Command.ExecuteScalar();
                        if (result != null)
                        {
                            return (Convert.ToInt32(result));
                        }
                        else
                        {
                            return -1;

                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("DAL Add User Error: " + ex.Message);
                    }

                }
            }
        }
        public static bool UpdateUserInfo(int UserID, int PersonID, string UserName, bool IsActive)
        {
            int RowsAffected = 0;
            string query = @"UPDATE Users
                                SET 
                                    PersonID = @PersonID,
                                    UserName = @UserName,
                                    IsActive = @IsActive,
                                WHERE UserID = @UserID";
            using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand(query, Connection))
                {
                    Command.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
                    Command.Parameters.Add("@PersonID", SqlDbType.Int).Value = PersonID;
                    Command.Parameters.Add("@UserName", SqlDbType.NVarChar, 50).Value = UserName;
                    Command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = IsActive;
                   
                    try
                    {
                        Connection.Open();
                        RowsAffected = Command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("DAL Update User Info Error: " + ex.Message);
                    }
                }
            }
            return (RowsAffected > 0);
        }
        public static bool ChangePassword(string UserName, string NewPasswordHash)
        {
            int RowsAffected = 0;
            string query = @"UPDATE Users
                                SET 
                                    PasswordHash = @NewPasswordHash,
                                WHERE UserName = @UserName";
            using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand(query, Connection))
                {                   
                    Command.Parameters.Add("@UserName", SqlDbType.NVarChar, 50).Value = UserName;
                    Command.Parameters.Add("@NewPasswordHash", SqlDbType.NVarChar, 256).Value = NewPasswordHash;

                    try
                    {
                        Connection.Open();
                        RowsAffected = Command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("DALChange Password Error: " + ex.Message);
                    }
                }
            }
            return (RowsAffected > 0);
        }
        public static bool DeleteUser(int UserID)
        {
            int RowsAffected = 0;
            string query = @"Delete From Users Where UserID =@UserID";
            using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand(query, Connection))
                {
                    Command.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
                    try
                    {
                        Connection.Open();
                        RowsAffected = Command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("DAL Delete User Erorr : " + ex.Message);
                    }
                }
            }
            return (RowsAffected > 0);
        }
        public static bool FindUserInfoByUserID
            (int UserID, ref int PersonID, ref string UserName, ref bool IsActive)
        {
            bool IsFound = false;
            string query = "Select Users.PersonID, Users.UserName, Users.IsActive " +
                         "  From Users " +
                        " Where UserID = @UserID ;";
            using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand(query, Connection))
                {
                    Command.Parameters.Add("UserID", SqlDbType.Int).Value = UserID;
                    try
                    {
                        Connection.Open();
                        SqlDataReader Reader = Command.ExecuteReader();
                        if (Reader.Read())
                        {
                            IsFound = true;
                            PersonID = Convert.ToInt32(Reader["PersonID"]);
                            UserName = Convert.ToString(Reader["UserName"]);
                            IsActive = Convert.ToBoolean(Reader["IsActive"]);
                        }
                        else
                        {
                            IsFound = false;
                        }
                        Reader.Close();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("DAL Find User Info By UserID Error : " + ex.Message);
                    }
                }
            }
            return IsFound;
        }
        public static bool FindUserInfoByPersonID
           (int PersonID, ref int UserID, ref string UserName, ref bool IsActive)
        {
            bool IsFound = false;
            string query = "Select Users.UserID, Users.UserName, Users.IsActive " +
                         "  From Users " +
                        " Where PersonID = @PersonID ;";
            using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand(query, Connection))
                {
                    Command.Parameters.Add("PersonID", SqlDbType.Int).Value = PersonID;
                    try
                    {
                        Connection.Open();
                        SqlDataReader Reader = Command.ExecuteReader();
                        if (Reader.Read())
                        {
                            IsFound = true;
                            UserID = Convert.ToInt32(Reader["UserID"]);
                            UserName = Convert.ToString(Reader["UserName"]);
                            IsActive = Convert.ToBoolean(Reader["IsActive"]);
                        }
                        else
                        {
                            IsFound = false;
                        }
                        Reader.Close();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("DAL Find All User Info By PersonID Error : " + ex.Message);
                    }
                }
            }
            return IsFound;
        }
        public static bool FindAllUserInfoByUserID
            (int UserID, ref int PersonID, ref string UserName, ref string PasswordHash, ref bool IsActive)
        {
            bool IsFound = false;
            string query = "Select Users.PersonID, Users.UserName, Users.PasswordHash, Users.IsActive " +
                         "  From Users " +
                        " Where UserID = @UserID ;";
            using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand(query, Connection))
                {
                    Command.Parameters.Add("UserID", SqlDbType.Int).Value = UserID;
                    try
                    {
                        Connection.Open();
                        SqlDataReader Reader = Command.ExecuteReader();
                        if (Reader.Read())
                        {
                            IsFound = true;
                            PersonID = Convert.ToInt32(Reader["PersonID"]);
                            UserName = Convert.ToString(Reader["UserName"]);
                            PasswordHash = Convert.ToString(Reader["PasswordHash"]);
                            IsActive = Convert.ToBoolean(Reader["IsActive"]);                            
                        }
                        else
                        {
                            IsFound = false;
                        }
                        Reader.Close();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("DAL Find All User Info By UserID Error : " + ex.Message);
                    }
                }
            }
            return IsFound;
        }
public static bool FindAllUserInfoByUserName
           ( string UserName, ref int UserID, ref int PersonID,  ref string PasswordHash, ref bool IsActive)
{
    bool IsFound = false;
    string query = "Select Users.UserID, Users.PersonID, Users.UserName, Users.PasswordHash, Users.IsActive " +
                 "  From Users " +
                " Where UserName = @UserName ;";
    using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
    {
        using (SqlCommand Command = new SqlCommand(query, Connection))
        {
            Command.Parameters.Add("UserName", SqlDbType.NVarChar,50).Value = UserName;
            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();
                if (Reader.Read())
                {
                    IsFound = true;
                    UserID = Convert.ToInt32(Reader["UserID"]);
                    PersonID = Convert.ToInt32(Reader["PersonID"]);
                    PasswordHash = Convert.ToString(Reader["PasswordHash"]);
                    IsActive = Convert.ToBoolean(Reader["IsActive"]);
                }
                else
                {
                    IsFound = false;
                }
                Reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("DAL Find All User Info By UserName Error : " + ex.Message);
            }
        }
    }
    return IsFound;
}
        public static bool FindAllUserInfoByUserNameAndPasswordHash
           (string UserName, string PasswordHash, ref int UserID, ref int PersonID, ref bool IsActive)
        {
            bool IsFound = false;
            string query = "Select Users.UserID, Users.PersonID, Users.UserName, Users.IsActive " +
                         "  From Users " +
                        " Where UserName = @UserName And PasswordHash = @PasswordHash ;";
            using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand(query, Connection))
                {
                    Command.Parameters.Add("UserName", SqlDbType.NVarChar, 50).Value = UserName;
                    Command.Parameters.Add("PasswordHash", SqlDbType.NVarChar, 256).Value = PasswordHash;

                    try
                    {
                        Connection.Open();
                        SqlDataReader Reader = Command.ExecuteReader();
                        if (Reader.Read())
                        {
                            IsFound = true;
                            UserID = Convert.ToInt32(Reader["UserID"]);
                            PersonID = Convert.ToInt32(Reader["PersonID"]);
                            IsActive = Convert.ToBoolean(Reader["IsActive"]);
                        }
                        else
                        {
                            IsFound = false;
                        }
                        Reader.Close();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("DAL Find All User Info By UserName AndPassword Hash Error : " + ex.Message);
                    }
                }
            }
            return IsFound;
        }
        public static DataTable GetAllUsers()
        {
            DataTable dtPersons = new DataTable();

            string query = @"SELECT  Users.UserID, Users.PersonID,
                            FullName = People.FirstName + ' ' + People.SecondName + ' ' + ISNULL( People.ThirdName,'') +' ' + People.LastName,
                             Users.UserName, Users.IsActive
                             FROM  Users INNER JOIN
                                    People ON Users.PersonID = People.PersonID";

            using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand(query, Connection))
                {
                    try
                    {
                        Connection.Open();
                        SqlDataReader Reader = Command.ExecuteReader();
                        if (Reader.HasRows)
                        {
                            dtPersons.Load(Reader);
                        }
                        else
                        {
                            return null;
                        }
                        Reader.Close();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("DAL Get All Users Error:" + ex.Message);
                    }
                }
            }
            return dtPersons;
        }
        public static bool IsUserExist(int UserID)
        {
            bool IsExist = false;
            string query = "SELECT Found=1 FROM Users WHERE UserID = @UserID";
            using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand(query, Connection))
                {
                    Command.Parameters.Add("UserID", SqlDbType.Int).Value = UserID;
                    try
                    {
                        Connection.Open();
                        SqlDataReader Reader = Command.ExecuteReader();
                        IsExist = Reader.HasRows;
                        Reader.Close();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("DAL Is User Exist By UserID Error : " + ex.Message);
                    }
                }
            }
            return IsExist;
        }
        public static bool IsUserExist(string UserName)
        {
            bool IsExist = false;
            string query = "SELECT Found=1 FROM Users WHERE UserName = @UserName";
            using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand(query, Connection))
                {
                    Command.Parameters.Add("UserName", SqlDbType.NChar, 50).Value = UserName;
                    try
                    {
                        Connection.Open();
                        SqlDataReader Reader = Command.ExecuteReader();
                        IsExist = Reader.HasRows;
                        Reader.Close();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("DAL Is User Exist By UserName Error : " + ex.Message);
                    }
                }
            }
            return IsExist;
        }
        public static bool IsUserExistForPersonID(int PersonID)
        {
            bool IsExist = false;
            string query = "SELECT Found=1 FROM Users WHERE PersonID = @PersonID";
            using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand(query, Connection))
                {
                    Command.Parameters.Add("PersonID", SqlDbType.Int).Value = PersonID;
                    try
                    {
                        Connection.Open();
                        SqlDataReader Reader = Command.ExecuteReader();
                        IsExist = Reader.HasRows;
                        Reader.Close();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("DAL Is User Exist For Person By PersonID Error : " + ex.Message);
                    }
                }
            }
            return IsExist;
        }
        public static bool DoesPersonHaveUser(int PersonID)
        {
            bool IsExist = false;
            string query = "SELECT Found=1 FROM Users WHERE PersonID = @PersonID";
            using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand(query, Connection))
                {
                    Command.Parameters.Add("PersonID", SqlDbType.Int).Value = PersonID;
                    try
                    {
                        Connection.Open();
                        SqlDataReader Reader = Command.ExecuteReader();
                        IsExist = Reader.HasRows;
                        Reader.Close();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("DAL Does Person Have User Error : " + ex.Message);
                    }
                }
            }
            return IsExist;
        }
    }
}

/*
 *  
           
                

            
            public static bool IsPersonExist(int PersonID)
            {
                bool IsExist = false;
                string query = "Select SELECT Exist=1 FROM People WHERE PersonID = @PersonID ;";
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand Command = new SqlCommand(query, Connection))
                    {
                        Command.Parameters.Add("PersonID", SqlDbType.Int).Value = PersonID;
                        try
                        {
                            Connection.Open();
                            SqlDataReader Reader = Command.ExecuteReader();
                            IsExist = Reader.HasRows;
                            Reader.Close();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("DAL Is Person Exist By PersonID Error : " + ex.Message);
                        }
                    }
                }
                return IsExist;
            }
            public static bool IsPersonExist(string NationalNo)
            {
                bool IsExist = false;
                string query = "Select SELECT Exist=1 FROM People WHERE NationalNo = @NationalNo ;";
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand Command = new SqlCommand(query, Connection))
                    {
                        Command.Parameters.Add("NationalNo", SqlDbType.NChar, 20).Value = NationalNo;
                        try
                        {
                            Connection.Open();
                            SqlDataReader Reader = Command.ExecuteReader();
                            IsExist = Reader.HasRows;
                            Reader.Close();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("DAL Is Person Exist By NationalNo Error : " + ex.Message);
                        }
                    }
                }
                return IsExist;
            }
            public static bool IsNationalNoExist(string NationalNo, int PersonID)
            {
                bool IsExist = false;
                string query = "Select SELECT Exist=1 FROM People WHERE NationalNo = @NationalNo and PersonID <> @PersonID;";
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand Command = new SqlCommand(query, Connection))
                    {
                        Command.Parameters.Add("PersonID", SqlDbType.Int).Value = PersonID;
                        Command.Parameters.Add("NationalNo", SqlDbType.NChar, 20).Value = NationalNo;
                        try
                        {
                            Connection.Open();
                            SqlDataReader Reader = Command.ExecuteReader();
                            IsExist = Reader.HasRows;
                            Reader.Close();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("DAL Is Person Exist By NationalNo Error : " + ex.Message);
                        }
                    }
                }
            return IsExist;
            }
 * */