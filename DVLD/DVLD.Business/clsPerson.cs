using DVLD.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.Business
{
    public class clsPerson
    {
        public enum enMode { AddPerson = 1, UpdatePerson = 2 };
        public enMode Mode = enMode.AddPerson;
        public enum enGender { Male = 1, Female = 2 };
        public enGender Gender;

        public int PersonID { set; get; }
        public string FirstName { set; get; }
        public string SecondName { set; get; }
        public string ThirdName { set; get; }
        public string LastName { set; get; }       
        public string NationalNo { set; get; }
        public DateTime DateOfBirth { set; get; }
        public short GenderID { set; get; }
        public string Address { set; get; }
        public string Phone { set; get; }
        public string Email { set; get; }
        public short CountryID { set; get; }
        public clsCountry Country;

        private string _ImagePath;

        public string ImagePath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }
        public string FullName
        {
            get { return FirstName + " " + SecondName + " " + ThirdName + " " + LastName; }

        }
   

        public clsPerson()

        {
            this.PersonID = -1;
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.DateOfBirth = DateTime.Now;
            this.GenderID = -1;
            this.Address = "";
            this.Phone = "";
            this.Email = "";
            this.CountryID = -1;
            this.ImagePath = "";

            Mode = enMode.AddPerson;
        }

        private clsPerson(int PersonId, string nationalNo, string firstName, string secondName, string thirdName,
            string lastName,  DateTime dateOfBirth,string phone, string email, string address, string imagePath,
            short countryId, short genderId)

        {
            this.PersonID = PersonId;
            this.FirstName = firstName;
            this.SecondName = secondName;
            this.ThirdName = thirdName;
            this.LastName = lastName;
            this.NationalNo = nationalNo;
            this.DateOfBirth = dateOfBirth;
            this.GenderID = genderId;
            this.Address = address;
            this.Phone = phone;
            this.Email = email;
            this.CountryID = countryId;
            this.ImagePath = imagePath;
            this.Country = clsCountry.GetCountry(countryId);
            Mode = enMode.UpdatePerson;
        }

        private bool _AddPerson()
        {
            this.PersonID = clsPersonData.AddPerson(this.NationalNo,this.FirstName, this.SecondName, this.ThirdName,
                this.LastName,this.DateOfBirth, this.Phone, this.Email, this.Address, this.ImagePath,
                this.CountryID, this.GenderID );

            return (this.PersonID != -1);
        }

        private bool _UpdatePerson()
        {
            return clsPersonData.UpdatePersonInfo(this.PersonID, this.NationalNo, this.FirstName, this.SecondName,
                this.ThirdName, this.LastName, this.DateOfBirth, this.Phone, this.Email, this.Address, this.ImagePath,
                this.CountryID, this.GenderID);
        }

        public static clsPerson Find(int PersonId)
        {

            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", NationalNo = "", Email = "",
                Phone = "", Address = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            short CountryId = -1;
            short GenderId = 0;

            bool IsFound = clsPersonData.FindPersoneByID(
                            PersonId, ref NationalNo, ref FirstName, ref SecondName,ref ThirdName, ref LastName,
                            ref DateOfBirth, ref Phone,ref Email, ref Address, ref ImagePath,ref CountryId, ref GenderId);

            if (IsFound)
            {
                return new clsPerson(PersonId, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Phone,
                                      Email, Address, ImagePath, CountryId, GenderId);
            }
            else
                return null;
        }

        public static clsPerson Find(string nationalNo)
        {
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", Email = "", Phone = "", Address = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            int PersonId = -1;
            short CountryId = -1;
            short GenderId = 0;

            bool IsFound = clsPersonData.FindPersoneByNationalNo(
                        nationalNo, ref PersonId, ref FirstName, ref SecondName,ref ThirdName, ref LastName, ref DateOfBirth,
                        ref Phone, ref Email, ref Address, ref ImagePath,ref CountryId, ref GenderId);


            if (IsFound)
            {
                return new clsPerson(PersonId, nationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Phone,
                                      Email, Address, ImagePath, CountryId, GenderId);
            }
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddPerson:
                    if (_AddPerson())
                    {

                        Mode = enMode.UpdatePerson;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.UpdatePerson:

                    return _UpdatePerson();

            }

            return false;
        }

        public static DataTable GetAllPeople()
        {
            return clsPersonData.GetAllPeople();
        }

        public static bool DeletePerson(int ID)
        {
            return clsPersonData.DeletePerson(ID);
        }

        public static bool isPersonExist(int ID)
        {
            return clsPersonData.IsPersonExist(ID);
        }

        public static bool isPersonExist(string NationlNo)
        {
            return clsPersonData.IsPersonExist(NationlNo);
        }

    }
}
