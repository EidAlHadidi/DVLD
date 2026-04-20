using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Data;
using System.Net;
using System.Security.Policy;
using System.IO;

namespace Business
{
    public class clsPerson
    {
        public enum enMode { AddNew=0,Update=1};
        private enMode _Mode = enMode.AddNew;
        public int ID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return (ThirdName != null) ?
                   (FirstName + " " + SecondName + " " + ThirdName + " " + LastName) :
                   (FirstName + " " + SecondName + " " + LastName);
            }
        }

        public DateTime DateOfBirth { get; set; }
        public int Gendor {  get; set; }
        public string Address { get; set; }
        public string Phone {  get; set; }
        public string Email { get; set; }
        public int NationalityCountryID { get; set; }

        public clsCountry CountryInfo
        {
            get
            {
                return clsCountry.Find(NationalityCountryID);
            }
            set
            {
                
            }
        }
        public string ImagePath { get; set; }

        public clsPerson()
        {
            _Mode = enMode.AddNew;
            this.ID = -1;
            this.NationalNo = "";
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.DateOfBirth = DateTime.Now;
            this.Gendor = 0;
            this.Address = "";
            this.Phone = "";
            this.Email = "";
            this.NationalityCountryID = -1;
            this.ImagePath = "";
            this.CountryInfo = null;
        }

        private clsPerson(int iD, string nationalNo, string firstName, string secondName, string thirdName, string lastName, DateTime dateOfBirth,
            int gendor, string address, string phone,string email, int nationalityCountryID, string imagePath)
        {
            this._Mode = enMode.Update;
            this.ID = iD;
            this.NationalNo = nationalNo;
            this.FirstName = firstName;
            this.SecondName = secondName;
            this.ThirdName = thirdName;
            this.LastName = lastName;
            this.DateOfBirth = dateOfBirth;
            this.Gendor = gendor;
            this.Address = address;
            this.Phone = phone;
            this.Email = email;
            this.NationalityCountryID = nationalityCountryID;
            this.ImagePath = imagePath;
            this.CountryInfo = clsCountry.Find(nationalityCountryID);
        }

        public static DataTable GetAllPeople()
        {
            return DAL_People.GetAllPeople();
        }

        public static clsPerson Find(int ID)
        {
            string NationalNo = "", FirstName = "", SecondName = "", ThridName = "",
                LastName = "", Address = "", Phone = "", Email = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            int Gendor = -1, NationalityCountryID = -1;

            bool isFound = DAL_People.Find(ID, ref NationalNo, ref FirstName, ref SecondName, ref ThridName, ref LastName, ref DateOfBirth,
                ref Gendor, ref Address, ref Phone, ref Email, ref NationalityCountryID, ref ImagePath);
            if (isFound)
            {
                return new clsPerson(ID, NationalNo, FirstName, SecondName, ThridName, LastName, DateOfBirth,
                    Gendor, Address, Phone, Email, NationalityCountryID, ImagePath);
            }
            else
            {
                return null;
            }

        }

        public static clsPerson Find(string NationalNo)
        {
            string FirstName = "", SecondName = "", ThridName = "",
                LastName = "", Address = "", Phone = "", Email = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            int Gendor = -1, NationalityCountryID = -1,ID = -1;

            bool isFound = DAL_People.Find(NationalNo,ref ID, ref FirstName, ref SecondName, ref ThridName, ref LastName, ref DateOfBirth,
                ref Gendor, ref Address, ref Phone, ref Email, ref NationalityCountryID, ref ImagePath);
            if (isFound)
            {
                return new clsPerson(ID, NationalNo, FirstName, SecondName, ThridName, LastName, DateOfBirth,
                    Gendor, Address, Phone, Email, NationalityCountryID, ImagePath);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewPerson()
        {
            this.ID = DAL_People.AddNewPerson(NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth,
                Gendor, Address, Phone, Email, NationalityCountryID, ImagePath);

            return (this.ID != -1);
        }

        private bool _UpdatePerson()
        {
            return DAL_People.UpdatePerson(ID, NationalNo, FirstName, SecondName, ThirdName, LastName
                , DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath);
        }

        public bool Save()
        {
            switch(_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;
                case enMode.Update:
                    return _UpdatePerson();
            }
            return false;
        }

        public static bool DeletePerson(int ID)
        {

            return DAL_People.DeletePerson(ID);
        }

        public static bool isPersonExist(int PersonID)
        {
            return DAL_People.IsPersonExist(PersonID);
        }

        public static bool isPersonExist(string NationalNo)
        {
            return DAL_People.IsPersonExist(NationalNo);
        }

    }
}
