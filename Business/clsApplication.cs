using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Data;

namespace Business
{
    public class clsApplication
    {
        public enum enMode { AddNew,Update}
        private enMode _Mode = enMode.AddNew;


        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }
        public clsPerson Person
        {
            get
            {
                return clsPerson.Find(ApplicantPersonID);
            }
        }
        public DateTime ApplicationDate { get; set; }
        public int ApplicationTypeID {  get; set; }
        public clsApplicationType ApplicationType
        {
            get
            {
                return clsApplicationType.Find(ApplicationTypeID);
            }
        }
        public int ApplicationStatus { get; set; }
        public string ApplicationStatusCaption
        {
            get
            {
                switch(ApplicationStatus)
                {
                    case 1:
                        return "New";
                    case 2:
                        return "Cancelled";
                    case 3:
                        return "Completed";
                    default:
                        return "New";
                }
            }
        }

        public DateTime LastStatusDate {  get; set; }
        public float PaidFees {  get; set; }
        public int CreatedByUserID {  get; set; }
        public clsUser CreatedUser
        {
            get
            {
                return clsUser.Find(CreatedByUserID);
            }
        }

        public clsApplication()
        {
            _Mode = enMode.AddNew;
            ApplicationID = -1;
            ApplicantPersonID = -1;
            ApplicationDate = DateTime.Now;
            ApplicationTypeID = -1;
            ApplicationStatus = -1;
            LastStatusDate = DateTime.Now;
            PaidFees = -1;
            CreatedByUserID = -1;

        }

        private clsApplication(int applicationID, int applicantPersonID, DateTime applicationDate, int applicationTypeID, int applicationStatus, DateTime lastStatusDate, float paidFees, int createdByUserID)
        {
            _Mode = enMode.Update;
            ApplicationID = applicationID;
            ApplicantPersonID = applicantPersonID;
            ApplicationDate = applicationDate;
            ApplicationTypeID = applicationTypeID;
            ApplicationStatus = applicationStatus;
            LastStatusDate = lastStatusDate;
            PaidFees = paidFees;
            CreatedByUserID = createdByUserID;
        }

        public static DataTable GetAllApplications()
        {
            return DAL_Applications.GetAllApplications();
        }
        
        public static clsApplication Find(int ApplicationID)
        {
            int applicantPersonID = -1, ApplicationTypeID = -1, ApplicationStatus = -1, CreatedByUserID = -1;
            float Fees = -1;
            DateTime ApplicationDate = DateTime.Now,LastStatusDate = DateTime.Now;
            if (DAL_Applications.Find(ApplicationID, ref applicantPersonID, ref ApplicationDate,
                ref ApplicationTypeID, ref ApplicationStatus, ref LastStatusDate, ref Fees, ref CreatedByUserID))
            {
                return new clsApplication(ApplicationID, applicantPersonID, ApplicationDate,
                    ApplicationTypeID, ApplicationStatus, LastStatusDate, Fees, CreatedByUserID);
            }
            else
                return null;
        }

        public static clsApplication Find(int ApplicantPersonID,int ApplicationTypeID,int ApplicationStatusID)
        {
            int ApplicationID = -1, CreatedByUserID = -1;
            float Fees = -1;
            DateTime ApplicationDate = DateTime.Now, LastStatusDate = DateTime.Now;
            if (DAL_Applications.Find(ApplicantPersonID, ApplicationTypeID, ApplicationStatusID, ref ApplicationID,
                ref ApplicationDate, ref LastStatusDate, ref Fees, ref CreatedByUserID))
            {
                return new clsApplication(ApplicationID, ApplicantPersonID, ApplicationDate,
                    ApplicationTypeID, ApplicationStatusID, LastStatusDate, Fees, CreatedByUserID);
            }
            else
                return null;
        }

        private bool _AddNewApplication()
        {
            this.ApplicationID = DAL_Applications.AddNewApplication(ApplicantPersonID, ApplicationDate,
                ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);

            return (this.ApplicationID != -1);
        }

        private bool _UpdateApplication()
        {
            return DAL_Applications.UpdateApplication(ApplicationID, ApplicantPersonID, ApplicationDate,
                ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);
        }

        public bool Save()
        {
            switch(_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplication())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;
                case enMode.Update:
                    return _UpdateApplication();
            }
            return false;
        }

        public static bool IsApplicationExist(int ID)
        {
            return DAL_Applications.isApplicationExist(ID);
        }

        public static bool IsApplicationExist(int PersonID,int ApplicationTypeID,int ApplicationStatus)
        {
            return DAL_Applications.isApplicationExist(PersonID, ApplicationTypeID, ApplicationStatus);
        }


        public static bool DeleteApplication(int ID)
        {
            return DAL_Applications.DeleteApplication(ID);
        }
    }
}
