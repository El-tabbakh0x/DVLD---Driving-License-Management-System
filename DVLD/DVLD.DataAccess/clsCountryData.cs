 using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.DataAccess
{
    public class clsCountryData
    {
        static public bool GetCountryByCountryName(string CountryName, ref short CountryID)
        {
            bool IsFound = false;
            string query = "SELECT Countries.CountryID, Countries.CountryName FROM Countries" +
                "  WHERE CountryName = @CountryName";
            using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand(query, Connection))
                {
                    Command.Parameters.Add("CountryName", SqlDbType.NVarChar,50).Value = CountryName;

                    try
                    {
                        Connection.Open();
                        SqlDataReader Reader = Command.ExecuteReader();
                        if (Reader.Read())
                        {
                            IsFound = true;
                            CountryID = Convert.ToInt16(Reader["CountryID"]);
                        }
                        else
                        {
                            IsFound = false;
                        }
                        Reader.Close();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("DAL Get Country By CountryName Error: " + ex.Message);
                    }
                }
            }
            return IsFound;
        }
        static public bool GetCountryByID(short CountryID, ref string CountryName )
        {
            bool IsFound = false;
            string query = "SELECT Countries.CountryID, Countries.CountryName FROM Countries  WHERE CountryID = @CountryID";
            using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand(query, Connection))
                {
                    Command.Parameters.Add("CountryID", SqlDbType.Int).Value = CountryID;

                    try
                    {
                        Connection.Open();
                        SqlDataReader Reader = Command.ExecuteReader();
                        if (Reader.Read())
                        {
                            IsFound = true;
                            CountryName = Convert.ToString(Reader["CountryName"]);
                        }
                        else
                        {
                            IsFound = false;
                        }
                        Reader.Close();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("DAL Get Country By ID Error: " + ex.Message);
                    }
                }
            }
            return IsFound;
        }
        static public DataTable GetAllCountries()
        {
            DataTable dtAllCountries = new DataTable();
            string query = "SELECT Countries.CountryID, Countries.CountryName FROM Countries ";
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
                            dtAllCountries.Load(Reader);
                        }
                        /*
                        else
                        {
                            dtAllCountries=null;
                        }
                        */
                        Reader.Close();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("DAL Get All Countries Error: " + ex.Message);
                    }
                }
            }
            return dtAllCountries;
        }
    }
}
