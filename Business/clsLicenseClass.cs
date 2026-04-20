using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Data;

namespace Business
{
    public class clsLicenseClass
    {

        public int LicenseClassID {  get; set; }
        public string ClassName {  get; set; }
        public string ClassDescription {  get; set; }
        public int MinimumAllowedAge {  get; set; }
        public int DefaultValidityLength {  get; set; }
        public float ClassFees {  get; set; }

        public clsLicenseClass()
        {
            LicenseClassID = -1;
            ClassName = string.Empty;
            ClassDescription = string.Empty;
            MinimumAllowedAge = -1;
            DefaultValidityLength = -1;
            ClassFees = -1;
        }

        private clsLicenseClass(int LicenseClassID,string ClassName,string ClassDescription,int MinimumAllowedAge,
            int DefaultValidityLength,float ClassFees)
        {
            this.LicenseClassID = LicenseClassID;
            this.ClassName = ClassName;
            this.ClassDescription = ClassDescription;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.DefaultValidityLength = DefaultValidityLength;
            this.ClassFees = ClassFees;
        }

        public static DataTable GetAllLicenseClasses()
        {
            return DAL_LicenseClasses.GetAllLicenseClasses();
        }

        public static clsLicenseClass Find(int LicenseClassID)
        {
            string ClassName = "", ClassDescription = "";
            int MinimumAllowedAge = -1, DefaultValidityLength = -1;
            float ClassFees = -1;
            if (DAL_LicenseClasses.Find(LicenseClassID, ref ClassName, ref ClassDescription, ref MinimumAllowedAge,
                ref DefaultValidityLength, ref ClassFees))
            {
                return new clsLicenseClass(LicenseClassID,ClassName,ClassDescription, 
                    MinimumAllowedAge, DefaultValidityLength, ClassFees);

            }
            else
                return null;
        }

    }
}
