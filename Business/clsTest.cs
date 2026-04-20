using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Data;
using System.Threading;
using System.Windows.Forms;

namespace Business
{
    public class clsTest
    {
        private enum enMode { AddNew,Update};
        private enMode _Mode = enMode.AddNew;
        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public clsTestAppointment TestAppointment
        {
            get
            {
                return clsTestAppointment.Find(TestAppointmentID);
            }
            set
            {

            }
        }
        public bool TestResult {  get; set; }
        public string Notes {  get; set; }
        public int CreatedByUserID {  get; set; }
        public int NumberOfTrials { get; set; }

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

        public clsTest()
        {
            _Mode = enMode.AddNew;
            TestID = -1;
            TestAppointmentID = -1;
            TestResult = false;
            Notes = string.Empty;
            CreatedByUserID = -1;
        }

        private clsTest(int testID, int testAppointmentID,bool testResult, string notes, int createdByUserID)
        {
            this._Mode = enMode.Update;
            this.TestID = testID;
            this.TestAppointmentID = testAppointmentID;
            this.TestResult = testResult;
            this.Notes = notes;
            this.CreatedByUserID = createdByUserID;
        }
        
        private clsTest(string NationalNo,int TestTypeID,string TestTypeTitle,int NumberOfTrials)
        {
            this.NumberOfTrials = NumberOfTrials;
        }

        public static DataTable GetAllTests()
        {
            return DAL_Tests.GetAllTests();
        }

        public static DataTable GetAllTests_MyView()
        {
            return DAL_Tests.GetAllTests_MyView();
        }

        public static clsTest Find(int TestID)
        {
            int TestAppointmentID = -1, CreatedByUserID = -1;
            string Notes = string.Empty;
            bool TestResult = false;
            bool isFound = DAL_Tests.Find(TestID, ref TestAppointmentID, ref TestResult, ref Notes, ref CreatedByUserID);
            if (isFound)
            {
                return new clsTest(TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID);
            }
            else
                return null;
        }

        public static clsTest Find(string NationalNo,int TestTypeID,int ApplicationStatus)
        {
            string TestTypeTitle = string.Empty;
            int NumberOfTrials = -1;
            if (DAL_Tests.Find(NationalNo, TestTypeID, ApplicationStatus, ref TestTypeTitle, ref NumberOfTrials))
            {
                return new clsTest(NationalNo, TestTypeID, TestTypeTitle, NumberOfTrials);
            }
            else
                return null;
        }

        private bool _AddNewTest()
        {
            this.TestID = DAL_Tests.AddNewTest(TestAppointmentID, TestResult, Notes, CreatedByUserID);
            return (this.TestID != 1);
        }

        private bool _UpdateTest()
        {
            return DAL_Tests.UpdateTest(this.TestID, this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);
        }

        public bool Save()
        {
            switch(_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTest())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;
                case enMode.Update:
                    return _UpdateTest();
            }
            return false;
        }

        public static bool DeleteTest(int TestID)
        {
            return DAL_Tests.DeleteTest(TestID);
        }

        public static bool DoesTestExist(int TestID)
        {
            return DAL_Tests.DoesTestExist(TestID);
        }

        public static int CalculateNumberOfTrialsInTest(string NationalNo,int TestTypeID,int ApplicationStatus)
        {
            clsTest test = clsTest.Find(NationalNo, TestTypeID,ApplicationStatus);
            return (test == null) ? 0 : test.NumberOfTrials;
        }

    }
}
