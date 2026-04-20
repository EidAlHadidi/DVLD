using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using System.Data;

namespace Business
{
    public class clsApplicationType
    {
        public int ApplicationTypeID {  get; set; }
        public string ApplicationTypeTitle { get; set; }
        public float ApplicationFees { get; set; }

        public clsApplicationType()
        {
            ApplicationTypeID = -1;
            ApplicationTypeTitle = "";
            ApplicationFees = 0;
        }

        private clsApplicationType(int applicationTypeID, string applicationTypeTitle, float applicationFees)
        {
            this.ApplicationTypeID = applicationTypeID;
            this.ApplicationTypeTitle = applicationTypeTitle;
            this.ApplicationFees = applicationFees;
        }

        public static DataTable GetAllApplicationTypes()
        {
            return DAL_ApplicationTypes.GetApplicationTypes();
        }

        public static clsApplicationType Find(int ID)
        {
            string Title = "";
            float Fees = 0;
            if(DAL_ApplicationTypes.Find(ID,ref Title,ref Fees))
            {
                return new clsApplicationType(ID,Title,Fees);
            }
            else
                return null;
        }

        private bool _UpdateApplicationType()
        {
            return DAL_ApplicationTypes.UpdateApplicationType(ApplicationTypeID, ApplicationTypeTitle, ApplicationFees);
        }

        public bool Save()
        {
            return _UpdateApplicationType();
        }

    }
}
