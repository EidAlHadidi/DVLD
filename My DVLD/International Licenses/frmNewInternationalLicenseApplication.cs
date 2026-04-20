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
    public partial class frmNewInternationalLicenseApplication : Form
    {
        private clsLicense license;

        private clsApplication _Application = new clsApplication();
        private clsInternationalLicense _InternationalLicense = new clsInternationalLicense();

        public frmNewInternationalLicenseApplication()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _FillApplicationData()
        {
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblIssueDate.Text = DateTime.Now.ToShortDateString();
            lblExpirationDate.Text = DateTime.Now.AddYears(1).ToShortDateString();
            lblFees.Text = clsApplicationType.Find(6).ApplicationFees.ToString();
            lblCreatedBy.Text = clsUser.Find(clsGlobalSettings.LoggedInUserID).UserName;
        }

        private bool _CheckIfApplicantHasActiveInternationalLicense()
        {
            return clsInternationalLicense.DoesInternationalLicenseExist(license.LicenseID, true);
        }



        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(clsLicense obj)

        {
            if(obj != null)
            {
                license = clsLicense.Find(obj.LicenseID);
                lblLocalLicenseID.Text = license.LicenseID.ToString();
                LlblShowLicenseHistory.Enabled = true;
                if (_CheckIfApplicantHasActiveInternationalLicense())
                {
                    MessageBox.Show("This applicant already has an active international license", "Has active license", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (license.LicenseClass != 3)
                {
                    MessageBox.Show("International licenses is only issued for licenses from class 3 ", "Wrong license class", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!license.IsActive)
                {
                    MessageBox.Show("You cannot issue an international license with expired local license", "Expired local license", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                btnIssue.Enabled = true;
            }
            else
            {
                LlblShowLicenseHistory.Enabled=false;
                lblLocalLicenseID.Text = "??";
                btnIssue.Enabled = false;
            }
        }

        private void frmNewInternationalLicenseApplication_Load(object sender, EventArgs e)
        {
            _FillApplicationData();
        }

        private void LlblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var frm = new frmLicenseHistory(license.Application.ApplicantPersonID);
            frm.ShowDialog();

        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            //This is the process to make the base appliction record
            _Application = new clsApplication();
            _InternationalLicense = new clsInternationalLicense();
            _Application.ApplicantPersonID = license.Application.ApplicantPersonID;
            _Application.ApplicationDate = Convert.ToDateTime(lblApplicationDate.Text);
            _Application.ApplicationTypeID = 6;
            _Application.ApplicationStatus = 3;
            _Application.LastStatusDate = DateTime.Now;
            _Application.PaidFees = clsApplicationType.Find(6).ApplicationFees;
            _Application.CreatedByUserID = clsGlobalSettings.LoggedInUserID;
            if (!_Application.Save())
            {
                MessageBox.Show("Error while saving the base application", "Saving error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _InternationalLicense.ApplicationID = _Application.ApplicationID;
            _InternationalLicense.DriverID = license.DriverID;
            _InternationalLicense.IssuedUsingLocalLicenseID = license.LicenseID;
            _InternationalLicense.IssueDate = Convert.ToDateTime(lblIssueDate.Text);
            _InternationalLicense.ExpirationDate = Convert.ToDateTime(lblExpirationDate.Text);
            _InternationalLicense.IsActive = true;
            _InternationalLicense.CreatedByUserID = clsGlobalSettings.LoggedInUserID;
            if(!_InternationalLicense.Save())
            {
                MessageBox.Show("Error while saving the international license application", "Saving error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsApplication.DeleteApplication(_Application.ApplicationID);
                return;
            }
            MessageBox.Show($"International license issued successfully with ID {_InternationalLicense.InternationalLicenseID}", "License Issued", MessageBoxButtons.OK);
            btnIssue.Enabled = false;
            LlblShowLicensesInfo.Enabled = true;
            ctrlDriverLicenseInfoWithFilter1.MakeFilterDisabled();
            lblInternationalApplicationID.Text = _Application.ApplicationID.ToString();
            lblInternationalLicenseID.Text = _InternationalLicense.InternationalLicenseID.ToString();
        }

        private void LlblShowLicensesInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var frm = new frmInternationalDriverInfo(_InternationalLicense.InternationalLicenseID);
            frm.ShowDialog();
        }
    }
}
