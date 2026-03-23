using DVLD.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.Business
{
    public class clsUser
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int UserID { set; get; }
        public int PersonID { set; get; }
        public clsPerson PersonInfo;
        public string UserName { set; get; }
        public string HashPassword { set; get; }
        public void SetPassword(string password)
        {
            this.HashPassword = clsSecuritycs.ComputeHash(password);
        }
        public bool IsActive { set; get; }

        public clsUser()

        {
            this.UserID = -1;
            this.UserName = "";
            this.HashPassword = "";
            this.IsActive = true;
            Mode = enMode.AddNew;
        }

        private clsUser(int UserId, int PersonId, string username, string Hashpassword,
            bool isActive)

        {
            this.UserID = UserId;
            this.PersonID = PersonId;
            this.PersonInfo = clsPerson.Find(PersonId);
            this.UserName = username;
            this.HashPassword = Hashpassword;
            this.IsActive = isActive;

            Mode = enMode.Update;
        }
        private clsUser(int UserId, int PersonId, string username, bool isActive)

        {
            this.UserID = UserId;
            this.PersonID = PersonId;
            this.PersonInfo = clsPerson.Find(PersonId);
            this.UserName = username;
            this.IsActive = isActive;

            Mode = enMode.Update;
        }
        private bool _AddNewUser()
        {
            
            this.UserID = clsUserData.AddUser(this.PersonID, this.UserName, HashPassword, this.IsActive);

            return (this.UserID != -1);
        }
        private bool _UpdateUser()
        {
            return clsUserData.UpdateUserInfo(this.UserID, this.PersonID, this.UserName,this.IsActive);
        }
        public static bool ChangePassword(string userName, string NewPasswordHash)
        {
            return clsUserData.ChangePassword( userName, NewPasswordHash);
        }
        public static bool DeleteUser(int UserId)
        {
            return clsUserData.DeleteUser(UserId);
        }
        public static clsUser FindUserInfoByUserID(int UserId)
        {
            int PersonId = -1;
            string userName = "";
            bool isActive = false;

            bool IsFound = clsUserData.FindUserInfoByUserID
                                (UserId, ref PersonId, ref userName, ref isActive);

            if (IsFound)
                return new clsUser(UserId, PersonId, userName, isActive);
            else
                return null;
        }
        public static clsUser FindUserInfoByPersonID(int PersonId)
        {
            int UserId = -1;
            string userName = "";
            bool isActive = false;

            bool IsFound = clsUserData.FindUserInfoByPersonID
                                (PersonId, ref UserId, ref userName,  ref isActive);

            if (IsFound)
                return new clsUser(UserId, UserId, userName,  isActive);
            else
                return null;
        }
        public static clsUser FindAllUserInfoByUserID(int UserId)
        {
            int PersonId = -1;
            string userName = "", HashPassword = "";
            bool isActive = false;

            bool IsFound = clsUserData.FindAllUserInfoByUserID
                                (UserId, ref PersonId, ref userName, ref HashPassword, ref isActive);

            if (IsFound)
                return new clsUser(UserId, PersonId, userName, HashPassword, isActive);
            else
                return null;
        }
        public static clsUser FindAllUserInfoByUserName(string userName)
        {
            int UserId=-1 , PersonId = -1;
            string HashPassword = "";
            bool isActive = false;
            bool IsFound = clsUserData.FindAllUserInfoByUserName
                                (userName, ref UserId, ref PersonId, ref HashPassword, ref isActive);

            if (IsFound)
                return new clsUser(UserId, PersonId, userName, HashPassword, isActive);
            else
                return null;
        }
        public static clsUser FindByUsernameAndPassword(string userName, string password)
        {
            int UserId = -1;
            int PersonId = -1;
            string HashPassword = clsSecuritycs.ComputeHash(password);
            bool isActive = false;

            bool IsFound = clsUserData.FindAllUserInfoByUserNameAndPasswordHash
                                (userName, HashPassword, ref UserId, ref PersonId, ref isActive);

            if (IsFound)
                return new clsUser(UserId, PersonId, userName, HashPassword, isActive);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateUser();

            }

            return false;
        }

        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }       
        public static bool isUserExist(int UserId)
        {
            return clsUserData.IsUserExist(UserId);
        }

        public static bool isUserExist(string userName)
        {
            return clsUserData.IsUserExist(userName);
        }

        public static bool isUserExistForPersonID(int PersonId)
        {
            return clsUserData.IsUserExistForPersonID(PersonId);
        }
       
    }
}
