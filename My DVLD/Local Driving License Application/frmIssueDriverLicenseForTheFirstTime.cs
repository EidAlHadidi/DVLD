using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business;

namespace My_DVLD
{
    public partial class frmIssueDriverLicenseForTheFirstTime : Form
    {
        private int _LDL_AppID {get;set;}
        private clsLocalDrivingLicenseApplication _app;
        private clsDriver _Driver;
        public frmIssueDriverLicenseForTheFirstTime(int LocalAppID)
        {
            _LDL_AppID = LocalAppID;
            _app = clsLocalDrivingLicenseApplication.Find(_LDL_AppID);
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmIssueDriverLicenseForTheFirstTime_Load(object sender, EventArgs e)
        {
            ctrlApplicationInfo1.FillData(_LDL_AppID);

        }

        private bool _CreateDriverRecord()
        {
            int PersonID = _app.Application.ApplicantPersonID;
            _Driver = new clsDriver();
            _Driver.PersonID = PersonID;
            _Driver.CreatedByUserID = clsGlobalSettings.LoggedInUserID;
            _Driver.CreatedDate = DateTime.Now;
            return _Driver.Save();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            //Making driver record
            //Issue reasons: 1:First time,2:Renew,3:Replacement for damaged,4:Replacement for lost
            if (_CreateDriverRecord())
            {
                clsLicense license = new clsLicense();
                license.ApplicationID = _app.ApplicationID;
                license.DriverID = _Driver.DriverID;
                license.LicenseClass = _app.LicenseClassID;
                license.IssueDate = DateTime.Now;
                int YearsToAdd = clsLicenseClass.Find(license.LicenseClass).DefaultValidityLength;
                license.ExpirationDate = license.IssueDate.AddYears(YearsToAdd);
                license.Notes = txtNotes.Text.ToString();
                license.PaidFees = _app.LicenseClass.ClassFees;
                license.IsActive = true;
                license.IssueReason = 1;
                license.CreatedByUserID = clsGlobalSettings.LoggedInUserID;
                if (license.Save())
                {
                    MessageBox.Show($"License is created successfully with license ID {license.LicenseID}",
                        "Creation success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _app.Application.ApplicationStatus = 3;
                    _app.Application.LastStatusDate = DateTime.Now;
                    _app.Save();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error in creating license record!", "Creation error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Error in creating driver record!", "Creation error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }
    }
}
