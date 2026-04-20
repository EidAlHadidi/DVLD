using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class clsTestType
    {
        public int TestTypeID { get; set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public float TestTypeFees { get; set; }

        public clsTestType()
        {
            TestTypeID = -1;
            TestTypeTitle = "";
            TestTypeFees = 0;
            TestTypeDescription = "";
        }

        private clsTestType(int testTypeID, string testTypeTitle, string testTypeDescription, float testTypeFees)
        {
            TestTypeID = testTypeID;
            TestTypeTitle = testTypeTitle;
            TestTypeDescription = testTypeDescription;
            TestTypeFees = testTypeFees;
        }

        public static DataTable GetAllTestTypes()
        {
            return DAL_TestTypes.GetTestTypes();
        }

        public static clsTestType Find(int ID)
        {
            string Title = "", Description = "";
            float Fees = 0;
            if (DAL_TestTypes.Find(ID,ref Title,ref Description,ref Fees))
            {
                return new clsTestType(ID, Title, Description, Fees);
            }
            else
                return null;
        }

        private bool _UpdatetestType()
        {
            return DAL_TestTypes.UpdateApplicationType(TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);
        }

        public bool Save()
        {
            return _UpdatetestType();
        }
    }
}
