using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace DVLD.DataAccess
{
   
        public class clsPersonData
    {

            public static int AddPerson(string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName,
                 DateTime DateOfBirth, string Phone, string Email, string Address, string ImagePath, short CountryID, short GenderID)
            {
                string query = @"insert into People " +
                    "( NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Phone, Email, Address, ImagePath, GenderID, CountryID  )" +
                      "Values" +
                      "( @NationalNo, @FirstName, @SecondName, @ThirdName, @LastName, @DateOfBirth,  @Phone, @Email, @Address,  @ImagePath, @GenderID, @CountryID);" +
                      "select SCOPE_IDENTITY(); ";
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 
                {
                    using (SqlCommand Command = new SqlCommand(query, Connection))
                    {
                    Command.Parameters.Add("@NationalNo", SqlDbType.NVarChar, 20).Value = NationalNo;
                    Command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 20).Value = FirstName;
                    Command.Parameters.Add("@SecondName", SqlDbType.NVarChar, 20).Value = SecondName;
                    Command.Parameters.Add("@ThirdName", SqlDbType.NVarChar, 20).Value = ThirdName;
                    Command.Parameters.Add("@LastName", SqlDbType.NVarChar, 20).Value = LastName;
                    Command.Parameters.Add("@DateOfBirth", SqlDbType.DateTime).Value = DateOfBirth;
                    Command.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = Phone;
                    Command.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = Email;
                    Command.Parameters.Add("@Address", SqlDbType.NVarChar, 500).Value = Address;
                    Command.Parameters.Add("@CountryID", SqlDbType.TinyInt).Value = CountryID;
                    Command.Parameters.Add("@GenderID", SqlDbType.TinyInt).Value = GenderID;
                    Command.Parameters.Add("@ImagePath", SqlDbType.NVarChar, 250).Value =
                        string.IsNullOrEmpty(ImagePath) ? (object)DBNull.Value : ImagePath;
                   
                        try
                        {
                            Connection.Open();
                            object result = Command.ExecuteScalar();
                            if (result != null )
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
                            throw new Exception("DAL AddPerson Error: " + ex.Message);
                        }
                       
                    }
                }
            }
            public static bool UpdatePersonInfo(int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName,
                   DateTime DateOfBirth, string Phone, string Email, string Address, string ImagePath, short CountryID, short GenderID)
            {
                int RowsAffected = 0;
                string query = @"UPDATE People
                                SET 
                                    NationalNo = @NationalNo,
                                    FirstName = @FirstName,
                                    SecondName = @SecondName,
                                    ThirdName = @ThirdName,
                                    LastName = @LastName,
                                    DateOfBirth = @DateOfBirth,
                                    Address = @Address,
                                    Phone = @Phone,
                                    Email = @Email,
                                    ImagePath = @ImagePath,
                                    CountryID = @CountryID,
                                    GenderID = @GenderID
                                WHERE PersonID = @PersonID";
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand Command = new SqlCommand(query, Connection))
                    {
                        Command.Parameters.Add("@PersonID", SqlDbType.Int ).Value = PersonID;
                        Command.Parameters.Add("@NationalNo", SqlDbType.NVarChar, 20).Value = NationalNo;
                        Command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 20).Value = FirstName;
                        Command.Parameters.Add("@SecondName", SqlDbType.NVarChar, 20).Value = SecondName;
                        Command.Parameters.Add("@ThirdName", SqlDbType.NVarChar, 20).Value = ThirdName;
                        Command.Parameters.Add("@LastName", SqlDbType.NVarChar, 20).Value = LastName;
                        Command.Parameters.Add("@DateOfBirth", SqlDbType.DateTime).Value = DateOfBirth;
                        Command.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = Phone;
                        Command.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = Email;
                        Command.Parameters.Add("@Address", SqlDbType.NVarChar, 500).Value = Address;
                        Command.Parameters.Add("@CountryID", SqlDbType.TinyInt).Value = CountryID;
                        Command.Parameters.Add("@GenderID", SqlDbType.TinyInt).Value = GenderID;
                        Command.Parameters.Add("@ImagePath", SqlDbType.NVarChar, 250).Value =
                            string.IsNullOrEmpty(ImagePath) ? (object)DBNull.Value : ImagePath;
                        try
                        {
                            Connection.Open();
                            RowsAffected = Command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("DAL Update Person Info Error: " + ex.Message);
                        }
                        
                    }
                }
                return (RowsAffected > 0);
            }
            public static bool DeletePerson(int PersonID)
            {
                int RowsAffected = 0;
                string query = @"Delete From People Where PersonID =@PersonID";
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand Command = new SqlCommand(query, Connection))
                    {
                        Command.Parameters.Add("@PersonID", SqlDbType.Int).Value = PersonID;
                        try
                        {
                            Connection.Open();
                            RowsAffected = Command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("DAL Delete Person Erorr : " + ex.Message);
                        }                   
                    }
                }
                return (RowsAffected > 0);
            }
            public static bool FindPersoneByID(int PersonID, ref string NationalNo, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName,
                    ref DateTime DateOfBirth, ref string Address, ref string Phone, ref string Email, ref string ImagePath, ref short CountryID, ref short GenderID)
                {
                    bool IsFound = false;
                    string query = "Select People.PersonID, People.FirstName, People.SecondName, People.ThirdName, People.LastName," +
                                 "People.DateOfBirth, People.Phone, People.Email, People.Address,People.ImagePath, People.CountryID," +
                                 " People.GenderID From People" +
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
                                    FirstName = Convert.ToString(Reader["FirstName"]);
                                    SecondName = Convert.ToString(Reader["SecondName"]);
                                    ThirdName = Convert.ToString(Reader["ThirdName"]);
                                    LastName = Convert.ToString(Reader["LastName"]);
                                    NationalNo = Convert.ToString(Reader["NationalNo"]);
                                    DateOfBirth = Convert.ToDateTime(Reader["DateOfBirth"]);
                                    GenderID = Convert.ToInt16(Reader["GenderID"]);
                                    Address = Convert.ToString(Reader["Address"]);
                                    Phone = Convert.ToString(Reader["Phone"]);
                                    Email = Convert.ToString(Reader["Email"]);
                                    CountryID = Convert.ToInt16(Reader["CountryID"]);
                                    ImagePath =
                                        Reader["ImagePath"] != DBNull.Value ? Convert.ToString(Reader["ImagePath"]) : "";
                                }
                                else
                                {
                                    IsFound = false;

                                }
                                Reader.Close();
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("DAL Find Person By ID Error : " + ex.Message);
                            }

                        }
                    }
                        return IsFound;
                }
            public static bool FindPersoneByNationalNo(string NationalNo, ref int PersonID, ref string FirstName, ref string SecondName,
                ref string ThirdName, ref string LastName,ref DateTime DateOfBirth, ref string Phone,ref string Email,
                ref string Address, ref string ImagePath, ref short CountryID, ref short GenderID)
            {
                bool IsFound = false;
                string query = "Select People.PersonID, People.FirstName, People.SecondName, People.ThirdName, People.LastName," +
                                 "People.DateOfBirth, People.Phone, People.Email, People.Address,People.ImagePath, People.CountryID," +
                                 " People.GenderID From People" +
                                " Where NationalNo = @NationalNo ;";
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand Command = new SqlCommand(query, Connection))
                    {
                        Command.Parameters.Add("NationalNo", SqlDbType.NChar,20).Value = NationalNo;
                        try
                        {
                            Connection.Open();
                            SqlDataReader Reader = Command.ExecuteReader();
                            if (Reader.Read())
                            {
                                IsFound = true;
                                PersonID = Convert.ToInt32(Reader["PersonID"]);
                                FirstName = Convert.ToString(Reader["FirstName"]);
                                SecondName = Convert.ToString(Reader["SecondName"]);
                                ThirdName = Convert.ToString(Reader["ThirdName"]);
                                LastName = Convert.ToString(Reader["LastName"]);
                                DateOfBirth = Convert.ToDateTime(Reader["DateOfBirth"]);
                                GenderID = Convert.ToInt16(Reader["GenderID"]);
                                Address = Convert.ToString(Reader["Address"]);
                                Phone = Convert.ToString(Reader["Phone"]);
                                Email = Convert.ToString(Reader["Email"]);
                                CountryID = Convert.ToInt16(Reader["CountryID"]);
                                ImagePath =
                                    Reader["ImagePath"] != DBNull.Value ? Convert.ToString(Reader["ImagePath"]) : "";
                            }
                            else
                            {
                                IsFound = false;

                            }
                            Reader.Close();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("DAL Find Person By NationalNo Error : " + ex.Message);
                        }

                    }
                }
                return IsFound;
            }
                public static DataTable GetAllPeople()
                {
                    DataTable dtPersons = new DataTable();

                    string query = "Select P.PersonID, P.NationalNo, P.FirstName, P.SecondName, " +
                                 "     P.ThirdName, P.LastName, P.DateOfBirth, G.GenderName As Gender, P.Phone, " +
                                 " P.Email, P.Address, " +
                                 " C.CountryName As Country, P.ImagePath " +
                                 "From people P " +
                                 "   Inner join Genders G " +
                                        "On G.GenderID = P.GenderID " +
                                " Inner join Countries C " +
                                        " On C.CountryID = P.CountryID " +
                                        "   ORDER BY P.FirstName";
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
                                throw new Exception("DAL Get All Persons Error:" + ex.Message);
                            }                   
                        }
                    }
                    return dtPersons;
                }

            
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
  
        }
}


