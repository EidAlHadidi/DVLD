//Tested Class

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Data;

namespace Business
{
    public class clsTestAppointment
    {
        private enum enMode { AddNew,Update};

        private enMode _Mode = enMode.AddNew;

        public int TestAppointmentID { get; set; }
        public int TestTypeID {  get; set; }
        
        public clsTestType TestType
        {
            get
            {
                return clsTestType.Find(TestTypeID);
            }
            set
            {

            }
        }

        public int LocalDrivingLicenseApplicationID {  get; set; }

        public clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication
        {
            get
            {
                return clsLocalDrivingLicenseApplication.Find(LocalDrivingLicenseApplicationID);
            }
            set
            {

            }
        }

        public DateTime AppointmentDate { get; set; }
        public float PaidFees {  get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUser
        {
            get
            {
                return clsUser.Find(CreatedByUserID);
            }
            set
            {

            }
        }

        public bool IsLocked {  get; set; }

        public int RetakeTestApplicationID { get; set; }

        public clsApplication RetakeTestApplication
        {
            get
            {
                return clsApplication.Find(RetakeTestApplicationID);
            }
        }

        public clsTestAppointment()
        {
            _Mode = enMode.AddNew;
            TestAppointmentID = -1;
            TestTypeID = -1;
            LocalDrivingLicenseApplicationID = -1;
            AppointmentDate = DateTime.Now;
            PaidFees = -1;
            CreatedByUserID = -1;
            IsLocked = false;
            RetakeTestApplicationID = -1;
        }

        private clsTestAppointment(int testAppointmentID, int testTypeID,
            int localDrivingLicenseApplicationID, DateTime appointmentDate, float paidFees,
            int createdByUserID, bool isLocked, int RetakeTestApplicationID)
        {
            TestAppointmentID = testAppointmentID;
            TestTypeID = testTypeID;
            LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            AppointmentDate = appointmentDate;
            PaidFees = paidFees;
            CreatedByUserID = createdByUserID;
            IsLocked = isLocked;
            _Mode = enMode.Update;
            this.RetakeTestApplicationID = RetakeTestApplicationID;
        }
        
        public static DataTable GetAllTestAppointments()
        {
            return DAL_TestAppointments.GetAllTestAppointments();
        }
        public static clsTestAppointment Find(int TestAppointmentID)
        {
            int TestType = -1, LocalDrivingLicenseApplicationID = -1, CreatedByUserID = -1;
            DateTime AppointmentDate = default;
            float PaidFees = -1;
            bool IsLocked = false;
            int RetakeTestApplicationID = -1;
            bool isFound = DAL_TestAppointments.Find(TestAppointmentID, ref TestType, ref LocalDrivingLicenseApplicationID,
                ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked,ref RetakeTestApplicationID);
            if (isFound)
            {
                return new clsTestAppointment(TestAppointmentID, TestType, LocalDrivingLicenseApplicationID,
                    AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            }
            else
                return null;
        }

        private bool _AddNewTestAppointment()
        {
            this.TestAppointmentID = DAL_TestAppointments.AddNewAppointment(TestTypeID, LocalDrivingLicenseApplicationID,
                AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            return (this.TestAppointmentID != -1);
        }

        private bool _UpdateTestAppointment()
        {
            return DAL_TestAppointments.UpdateTestAppointment(TestAppointmentID, TestTypeID,
                LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees,
                CreatedByUserID, IsLocked, RetakeTestApplicationID);
        }

        public bool Save()
        {
            switch(_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestAppointment())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;
                case enMode.Update:
                    return _UpdateTestAppointment();
            }
            return false;
        }

        public static bool DeleteTestAppointment(int TestAppointmentID)
        {
            return DAL_TestAppointments.DeleteTestAppointment(TestAppointmentID);
        }

        public static bool IsTestAppointmentExist(int TestAppointmentID)
        {
            return DAL_TestAppointments.IsTestAppointmentExist(TestAppointmentID);
        }

    }
}
