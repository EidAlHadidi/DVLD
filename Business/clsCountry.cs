using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Data;

namespace Business
{
    public class clsCountry
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        
        //We dont need a public constructor because we dont need to make an instance of clsCountry
        /*
            public clsCountry()
            {
                CountryID = -1;
                CountryName = "";
            }
        */

        private clsCountry(int countryID, string countryName)
        {
            this.CountryID = countryID;
            this.CountryName = countryName;
        }

        public static DataTable GetAllCountries()
        {
            return DAL_Countries.GetAllCountries();
        }

        public static clsCountry Find(int ID)
        {
            string CountryName = "";
            bool isFound = DAL_Countries.Find(ID, ref CountryName);
            if (isFound)
            {
                return new clsCountry(ID, CountryName);
            }
            else
                return null;
        }

        public static clsCountry Find(string CountryName)
        {
            int ID = -1;
            bool isFound = DAL_Countries.Find(CountryName,ref ID);
            if (isFound)
            {
                return new clsCountry(ID, CountryName);
            }
            else
                return null;
        }

        public static bool IsCountryExist(int ID)
        {
            return DAL_Countries.IsCountryExist(ID);
        }

        public static bool IsCountryExist(string CountryName)
        {
            return DAL_Countries.IsCountryExist(CountryName);
        }

    }
}
