using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business;

namespace My_DVLD
{
    public partial class frmTakeTest : Form
    {
        public int _TestAppointmentID { get; set; }

        public clsTestAppointment _appointment;

        public clsTest _test;

        public string Type { get; set; }

        public frmTakeTest(int TestAppointmentID)
        {
            _TestAppointmentID = TestAppointmentID;
            _appointment = clsTestAppointment.Find(_TestAppointmentID);
            _test = new clsTest();
            InitializeComponent();
        }

        private void _LoadFormStates()
        {
            switch (Type)
            {
                case "Vision":
                    gbTestType.Text = "Vision Test";
                    pbTestType.ImageLocation = @"C:\Users\Eid AlHadidi\Documents\Study\Course 19\Icons\Icons\Vision 512.png";
                    break;
                case "Written":
                    gbTestType.Text = "Written Test";
                    pbTestType.ImageLocation = @"C:\Users\Eid AlHadidi\Documents\Study\Course 19\Icons\Icons\Written Test 512.png";
                    break;
                case "Street":
                    gbTestType.Text = "Practical Test";
                    pbTestType.ImageLocation = @"C:\Users\Eid AlHadidi\Documents\Study\Course 19\Icons\Icons\Street Test 32.png";
                    break;
            }
        }

        private void _LoadData()
        {
            _LoadFormStates();
            string NationalNumber = _appointment.LocalDrivingLicenseApplication.Application.Person.NationalNo;
            int TestTypeID = _appointment.TestTypeID;
            lblDLAPP_ID.Text = _appointment.LocalDrivingLicenseApplicationID.ToString();
            lblD_Class.Text = _appointment.LocalDrivingLicenseApplication.LicenseClass.ClassName.ToString();
            lblName.Text = _appointment.LocalDrivingLicenseApplication.Application.Person.FullName.ToString();
            lblTrial.Text = clsTest.CalculateNumberOfTrialsInTest(NationalNumber, TestTypeID,1).ToString();
            lblDate.Text = _appointment.AppointmentDate.ToShortDateString();
            lblFees.Text = _appointment.PaidFees.ToString();
            lblTestID.Text = "Not Taken Yet";
        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save? After that you " +
                "cannot change the pass/fail results after you save?", "Confirm",
                MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _test.TestAppointmentID = _appointment.TestAppointmentID;
                if (rbPass.Checked)
                    _test.TestResult = true;
                else
                    _test.TestResult = false;
                _test.Notes = txtNotes.Text;
                _test.CreatedByUserID = clsGlobalSettings.LoggedInUserID;
                if (_test.Save() && (_appointment.IsLocked = true) && _appointment.Save())
                {
                    MessageBox.Show("Data saved successfully!", "Succeed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblTestID.Text = _test.TestID.ToString();
                    //this.Close();
                }
                else
                    MessageBox.Show("Error while saving the test result!", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                return;
        }
    }
}
