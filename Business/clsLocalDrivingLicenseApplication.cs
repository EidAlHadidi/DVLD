using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Business
{
    public class clsLocalDrivingLicenseApplication
    {
        public enum enMode { AddNew,Update};
        private enMode _Mode = enMode.AddNew;
        public int LocalDrivingLicenseApplicationID {  get; set; }
        public int ApplicationID { get; set; }
        public int LicenseClassID { get; set; }
        
        public clsApplication Application {  get; set; }
        
        public clsLicenseClass LicenseClass
        {
            get
            {
                return clsLicenseClass.Find(LicenseClassID);
            }
        }

        public clsLocalDrivingLicenseApplication()
        {
            LocalDrivingLicenseApplicationID = -1;
            ApplicationID = -1;
            LicenseClassID = -1;
            Application = clsApplication.Find(ApplicationID);
            _Mode = enMode.AddNew;
        }

        public int PassedTestsCount
        {
            get
            {
                return GetPassedTests(LocalDrivingLicenseApplicationID);
            }
        }

        private clsLocalDrivingLicenseApplication(int localDrivingLicenseApplicationID, 
            int applicationID, int licenseClassID)
        {
            LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            ApplicationID = applicationID;
            LicenseClassID = licenseClassID;
            Application = clsApplication.Find(ApplicationID);
            _Mode = enMode.Update;
        }

        private bool _AddNewDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID =
                DAL_LocalDrivingLicenseApplications.AddNewLocalApplication(this.ApplicationID, this.LicenseClassID);
            return (this.LocalDrivingLicenseApplicationID != -1);
        }

        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return DAL_LocalDrivingLicenseApplications.GetAllApplications();
        }

        public static DataTable GetAllLocalDrivingLicenseFullApplications_View()
        {
            return DAL_LocalDrivingLicenseApplications.GetAllApplications_FullView();
        }

        public static DataTable GetLocalDrivingApplications_View()
        {
            return DAL_LocalDrivingLicenseApplications.GetAllApplications_View();
        }

        public static clsLocalDrivingLicenseApplication Find(int ID)
        {
            int ApplicationID = -1,LicenseClassID = -1;
            if (DAL_LocalDrivingLicenseApplications.Find(ID, ref ApplicationID, ref LicenseClassID))
            {
                return new clsLocalDrivingLicenseApplication(ID, ApplicationID, LicenseClassID);
            }
            else
                return null;
        }

        public static clsLocalDrivingLicenseApplication Find(int PersonID,int Status,int LicenseClassID)
        {
            int ApplicationID = -1, LocalApplicationID = -1;
            bool isFound = DAL_LocalDrivingLicenseApplications.Find(PersonID, Status, LicenseClassID
                , ref ApplicationID, ref LocalApplicationID);
            if (isFound)
            {
                return new clsLocalDrivingLicenseApplication(LocalApplicationID,
                    ApplicationID, LicenseClassID);
            }
            else
                return null;
        }

        public static bool IsLocalDrivingLicenseApplicationExist(int ID)
        {
            return DAL_LocalDrivingLicenseApplications.IsLocalDrivingLicenseApplicationExist(ID);
        }

        public bool Save()
        {
            switch(_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDrivingLicenseApplication())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    return false;
                case enMode.Update:
                    return _UpdateLocalDrivingLicenseApplication();

            }
            return false;
        }

        public static bool DeleteLDL_Application(int ID,bool DeleteBaseApplication = false)
        {
            clsLocalDrivingLicenseApplication local = clsLocalDrivingLicenseApplication.Find(ID);
            if (local == null)
                return false;

            if (DeleteBaseApplication)
            {
                int ApplicationID = local.ApplicationID;
                return DAL_LocalDrivingLicenseApplications.DeleteLocalApplication(ID) && DAL_Applications.DeleteApplication(ApplicationID);
            }
            else
                return DAL_LocalDrivingLicenseApplications.DeleteLocalApplication(ID);
        }
        
        public static bool IsLocalDrivingLicenseApplicationExist(int PersonID,int ApplicationType,int Status)
        {
            return DAL_LocalDrivingLicenseApplications
                .IsLocalDrivingLicenseApplicationExist(PersonID, ApplicationType, Status);
        }

        private bool _UpdateLocalDrivingLicenseApplication()
        {
            return DAL_LocalDrivingLicenseApplications.UpdateLocalDrivingLicenseApplication
                (ApplicationID, Application.ApplicantPersonID, Application.ApplicationDate, Application.ApplicationTypeID,
                Application.ApplicationStatus, Application.LastStatusDate, Application.PaidFees, Application.CreatedByUserID,
                LicenseClassID, LocalDrivingLicenseApplicationID);
        }

        //Another

        public static int GetPassedTests(int ID)
        {
            return DAL_LocalDrivingLicenseApplications.GetPassedTests(ID);
        }

        /* This is a stupid code:) 
        //Custom attributes for the view
        public string ClassName { get; set; }
        public string NationalNo { get; set; }
        public string FullName { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int PassedTestCount { get; set; }
        public string Status { get; set; }

        private clsLocalDrivingLicenseApplication(int localDrivingLicenseApplicationID,
            string ClassName, string NationalNo, string FullName, DateTime ApplicationDate,
            int PassedTestCount, string Status)
        {
            this.LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            this.ClassName = ClassName;
            this.NationalNo = NationalNo;
            this.FullName = FullName;
            this.ApplicationDate = ApplicationDate;
            this.PassedTestCount = PassedTestCount;
            this.Status = Status;
        }

        public static clsLocalDrivingLicenseApplication Find_View(int ID)
        {
            string ClassName = "", NationalNo = "", FullName = "", Status = "";
            DateTime ApplicationDate = DateTime.MinValue;
            int PassedTestCount = -1;
            bool isFound = DAL_LocalDrivingLicenseApplications.Find(ID, ref ClassName, ref NationalNo,
                ref FullName, ref ApplicationDate, ref PassedTestCount, ref Status);
            if (isFound)
            {
                return new clsLocalDrivingLicenseApplication(ID, ClassName, NationalNo,
                    FullName, ApplicationDate, PassedTestCount, Status);
            }
            else
                return null;
        }
        */

        public static bool DeleteLocalWithBase(int ID)
        {
            clsLocalDrivingLicenseApplication local = clsLocalDrivingLicenseApplication.Find(ID);
            if (local != null)
            {
                int ApplicationID = local.ApplicationID;
                return DAL_LocalDrivingLicenseApplications.DeleteLocalApplication(ID) && DAL_Applications.DeleteApplication(ApplicationID);

            }
            else
                return DAL_LocalDrivingLicenseApplications.DeleteLocalApplication(ID);
        }

    }
}
