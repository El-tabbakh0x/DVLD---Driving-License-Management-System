using DVLD.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.Business
{
    public class clsCountry
    {
        public short CountryID { set; get; }
        public string CountryName { set; get; }

        public clsCountry()

        {
            this.CountryID = -1;
            this.CountryName = "";

        }

        private clsCountry(short countryId, string countryName)

        {
            this.CountryID = countryId;
            this.CountryName = countryName;
        }

        public static clsCountry GetCountry(short countryId)
        {
            string countryName = "";

            if (clsCountryData.GetCountryByID(countryId, ref countryName))

                return new clsCountry(countryId, countryName);
            else
                return null;

        }

        public static clsCountry GetCountry(string countryName)
        {

            short countryId = -1;

            if (clsCountryData.GetCountryByCountryName(countryName, ref countryId))

                return new clsCountry(countryId, countryName);
            else
                return null;

        }

        public static DataTable GetAllCountries()
        {
            return clsCountryData.GetAllCountries();

        }
    }
}
