using Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My_DVLD
{
    public partial class frmScheduleTest : Form
    {
        private clsTestAppointment _test= new clsTestAppointment();
        private clsApplication _Application;
        clsLocalDrivingLicenseApplication _Localapp;
        public string Type { get; set; }
        public int _LocalAppID { get; set; }

        public int _TestTypeID { get; set; }

        public bool IsLocked = false;

        private enum enMode { AddNew, Update};
        private enMode _Mode = enMode.AddNew; 

        public frmScheduleTest(int LocalAppID)
        {
            _LocalAppID = LocalAppID;
            _Localapp = clsLocalDrivingLicenseApplication.Find(_LocalAppID);
            _Mode = enMode.AddNew;
            InitializeComponent();
        }

        public frmScheduleTest(int LocalAppID,int TestAppointmentID)
        {
            _test = clsTestAppointment.Find(TestAppointmentID);
            if (_test != null)
                _Mode = enMode.Update;
            else
                _test = new clsTestAppointment();

            _LocalAppID = LocalAppID;
            _Localapp = clsLocalDrivingLicenseApplication.Find(_LocalAppID);

            InitializeComponent();
        }

        private void _LoadFormStates()
        {
            switch (Type)
            {
                case "Vision":
                    _TestTypeID = 1;
                    gbTestType.Text = "Vision Test";
                    pbTestType.ImageLocation = @"C:\Users\Eid AlHadidi\Documents\Study\Course 19\Icons\Icons\Vision 512.png";
                    break;
                case "Written":
                    _TestTypeID = 2;
                    gbTestType.Text = "Written Test";
                    pbTestType.ImageLocation = @"C:\Users\Eid AlHadidi\Documents\Study\Course 19\Icons\Icons\Written Test 512.png";
                    break;
                case "Street":
                    _TestTypeID = 3;
                    gbTestType.Text = "Practical Test";
                    pbTestType.ImageLocation = @"C:\Users\Eid AlHadidi\Documents\Study\Course 19\Icons\Icons\driving-test 512.png";
                    break;
            }
        }
        
        private void _LoadData()
        {
            _LoadFormStates();
            
            lblDLAPP_ID.Text = _Localapp.LocalDrivingLicenseApplicationID.ToString();
            lblD_Class.Text = _Localapp.LicenseClass.ClassName.ToString();
            lblName.Text = _Localapp.Application.Person.FullName.ToString();
            //Maybe not a general instruction..the MOST important instruction in the form!!
            lblTrial.Text = clsTest.CalculateNumberOfTrialsInTest(_Localapp.Application.Person.NationalNo, _TestTypeID,1).ToString();
            if(_Mode == enMode.AddNew)
                dtpDate.Value = DateTime.Now;
            else
                dtpDate.Value = _test.AppointmentDate;
            lblFees.Text = clsTestType.Find(_TestTypeID).TestTypeFees.ToString();

            //Handling retake test process
            if(lblTrial.Text != "0")
            {
                lblTestState.Text = "Schedule Retake Test";
                lblTestState.Location = new Point(lblTestState.Location.X - 50,lblTestState.Location.Y);
                gbRetakeTestInfo.Enabled = true;
                lblRAppFees.Text = clsApplicationType.Find(7).ApplicationFees.ToString();
                if (_Mode == enMode.Update)
                    lblRTestAppID.Text = _test.RetakeTestApplicationID.ToString();
            }
            else
            {
                gbRetakeTestInfo.Enabled = false;
                lblRAppFees.Text = "0";
            }
            lblTotalFees.Text = (int.Parse(lblFees.Text) + int.Parse(lblRAppFees.Text)).ToString();

            if (IsLocked)
            {
                dtpDate.Enabled = false;
                btnSave.Enabled = false;
                lblTestLocked.Visible = true;
            }
        }

        private void _SaveData(int TestTypeID,bool IsWithRetake = false)
        {
            
            /*
            This code was faded(low brightness) because of not referenced yet
            To make it faded again go to Tools->Options->Text editor -> C# -> Advanced >- Check fade checkboxes(3 checkboxes)


            Retake application attribute(shoule make retake test application)
            The retake application process should be done before test appointment!
            The retake application SHOULD have the same personal information in the local driving license application
            */
            if(IsWithRetake)
            {
                if (_Mode == enMode.Update)
                {
                    _Application = clsApplication.Find(Convert.ToInt32(lblRTestAppID.Text));
                }
                else
                    _Application = new clsApplication();
                _Application.ApplicantPersonID = _Localapp.Application.ApplicantPersonID;
                _Application.ApplicationDate = DateTime.Now;
                _Application.ApplicationTypeID = 7; //Application 7 is defined as retake test application
                _Application.ApplicationStatus = 1;
                _Application.LastStatusDate = DateTime.Now;
                _Application.PaidFees = clsApplicationType.Find(7).ApplicationFees;
                _Application.CreatedByUserID = clsGlobalSettings.LoggedInUserID;
                if (_Application.Save())
                {
                    MessageBox.Show("Retake application created successfully", "Debugging retake app", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error while saving retake base application", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                _test.RetakeTestApplicationID = _Application.ApplicationID;
                lblRTestAppID.Text = _test.RetakeTestApplicationID.ToString();
            }
            //This method should handle the saving for the 3 types of tests(vision,written,street) tests.
            //General attributes
            _test.TestTypeID = TestTypeID;
            _test.LocalDrivingLicenseApplicationID = Convert.ToInt32(lblDLAPP_ID.Text);
            _test.AppointmentDate = dtpDate.Value;
            _test.PaidFees = Convert.ToSingle(lblTotalFees.Text);
            _test.CreatedByUserID = clsGlobalSettings.LoggedInUserID;
            _test.IsLocked = false;
        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _SaveData(_TestTypeID,gbRetakeTestInfo.Enabled);
            if (_test.Save())
            {
                MessageBox.Show($"Test appointment saved successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
                MessageBox.Show($"Error occured while saving test appointment!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
