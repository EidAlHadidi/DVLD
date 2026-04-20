using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Data;

namespace Business
{
    public class clsUser
    {
        public enum enMode { AddNew, Update };
        private enMode _Mode = enMode.AddNew;

        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public clsPerson PersonInfo
        {
            get
            {
                return clsPerson.Find(PersonID);
            }
        }

        public clsUser()
        {
            _Mode = enMode.AddNew;
            UserID = -1;
            PersonID = -1;
            UserName = "";
            Password = "";
            IsActive = false;
        }

        private clsUser(int userID, int personID, string userName, string password, bool isActive)
        {
            _Mode = enMode.Update;
            UserID = userID;
            PersonID = personID;
            UserName = userName;
            Password = password;
            IsActive = isActive;
        }

        //Create ,Read(Find,GetAll),Update,Delete,IsExist

        public static clsUser Find(int UserID)
        {
            int PersonID = -1;
            string UserName = "", Password = "";
            bool isActive = false;
            bool isFound = DAL_Users.Find(UserID, ref PersonID, ref UserName, ref Password, ref isActive);
            if(isFound)
            {
                return new clsUser(UserID, PersonID, UserName, Password, isActive);
            }
            else
                return null;
        }

        //if you passed two parameters that means you are searching for a user by it's PersonID
        public static clsUser Find(int PersonID,bool PersonIdFlag)
        {
            int UserID = -1;
            bool isActive = false;
            string UserName = "", Password = "";
            bool isFound = DAL_Users.Find(PersonID, ref UserName, ref Password, ref isActive, ref UserID);
            if (isFound)
                return new clsUser(UserID, PersonID, UserName, Password, isActive);
            else
                return null;
        }

        public static clsUser Find(string UserName,string Password)
        {
            int PersonID = -1, UserID = -1;
            bool isActive = false;
            if (DAL_Users.Find(UserName, Password, ref UserID, ref PersonID, ref isActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, isActive);
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetAllUsers()
        {
            return DAL_Users.GetAllUsers();
        }

        private bool _AddNewUser()
        {
            this.UserID = DAL_Users.AddNewUser(PersonID,UserName,Password,IsActive);
            return (this.UserID != -1);
        }

        private bool _UpdateUser()
        {
            return DAL_Users.UpdateUser(UserID, PersonID, UserName, Password, IsActive);
        }

        public bool Save()
        {
            switch(_Mode)
            {
                case enMode.AddNew:
                    if(_AddNewUser())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    break;
                case enMode.Update:
                    return _UpdateUser();
            }
            return false;
        }

        public static bool DeleteUser(int UserID)
        {
            return DAL_Users.DeleteUser(UserID);
        }

        public static bool IsUserExist(int UserID)
        {
            return DAL_Users.IsUserExist(UserID);
        }

        public static bool IsUserExist(string UserName,string Password)
        {
            return DAL_Users.IsUserExist(UserName,Password);
        }

    }
}
