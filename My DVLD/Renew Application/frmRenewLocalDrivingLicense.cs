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
    public partial class frmRenewLocalDrivingLicense : Form
    {
        int LicenseID { get; set; }
        clsLicense license { get; set; }

        clsLicense _newLicense { get; set; }
        clsApplication _newApplication { get; set; }

        public DateTime IssueDate = DateTime.Now;

        public frmRenewLocalDrivingLicense()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _FillApplicationData()
        {
            lblLicenseFees.Text = clsLicenseClass.Find(license.LicenseClass).ClassFees.ToString();
            lblOldLicenseID.Text = license.LicenseID.ToString();
            lblExpirationDate.Text = IssueDate.AddYears(clsLicenseClass.Find(license.LicenseClass).DefaultValidityLength).ToShortDateString();
            lblTotalFees.Text = (int.Parse(lblApplicationFees.Text) + int.Parse(lblLicenseFees.Text)).ToString();
        }

        private bool _DoesApplicantHasActiveLicense()
        {
            return (clsLicense.DoesLicenseExist(license.Driver.Person.NationalNo, license.LicenseClass, true));
        }

        private bool _UpdateLicenseActivation()
        {
            if (license.ExpirationDate.CompareTo(DateTime.Now) == -1)
                license.IsActive = false;
            else
                license.IsActive = true;

            return license.Save();
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected_1(clsLicense obj)
        {
            if(obj != null)
            {
                LicenseID = obj.LicenseID;
                license = clsLicense.Find(LicenseID);
                LlblShowLicenseHistory.Enabled = true;

                if (!_UpdateLicenseActivation())
                {
                    MessageBox.Show("Error while updating license IsActive field", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (license.IsActive)
                {
                    MessageBox.Show($"Selected License is not yet expired,it will expire on:\n{license.ExpirationDate.ToShortDateString()}"
                        , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (_DoesApplicantHasActiveLicense())
                {
                    MessageBox.Show("This person already has an active license with the same license class!"
                        , "Renew error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                _FillApplicationData();
                btnRenew.Enabled = true;

            }
            else
            {
                btnRenew.Enabled= false;
                lblRenewLicenseApplicationID.Text = "??";
                lblRenewedLicenseID.Text = "??";
                lblOldLicenseID.Text = "??";
                lblLicenseFees.Text = "??";
                lblTotalFees.Text = "??";
                lblExpirationDate.Text = "??";
                txtNotes.Text = "??";
                LlblShowLicenseHistory.Enabled = false;
                LlblShowLicensesInfo.Enabled = false;
            }
        }

        private void frmRenewLocalDrivingLicense_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblIssueDate.Text = IssueDate.ToShortDateString();
            lblApplicationFees.Text = clsApplicationType.Find(2).ApplicationFees.ToString();
            lblCreatedBy.Text = clsUser.Find(clsGlobalSettings.LoggedInUserID).UserName.ToString();
        }

        private void LlblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var frm = new frmLicenseHistory(license.Application.ApplicantPersonID);
            frm.ShowDialog();
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to renew the license?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _newApplication = new clsApplication();
                _newApplication.ApplicantPersonID = license.Application.ApplicantPersonID;
                _newApplication.ApplicationDate = Convert.ToDateTime(lblApplicationDate.Text.Trim());
                _newApplication.ApplicationTypeID = 2;
                _newApplication.ApplicationStatus = 3;
                _newApplication.LastStatusDate = DateTime.Now;
                _newApplication.PaidFees = clsApplicationType.Find(2).ApplicationFees;
                _newApplication.CreatedByUserID = clsGlobalSettings.LoggedInUserID;

                if (!_newApplication.Save())
                {
                    MessageBox.Show("Error when saving base application!", "Saving error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _newLicense = new clsLicense();
                _newLicense.ApplicationID = _newApplication.ApplicationID;
                _newLicense.DriverID = license.DriverID;
                _newLicense.LicenseClass = license.LicenseClass;
                _newLicense.IssueDate = IssueDate;
                _newLicense.ExpirationDate = Convert.ToDateTime(lblExpirationDate.Text.Trim());
                _newLicense.Notes = txtNotes.Text;
                _newLicense.PaidFees = clsLicenseClass.Find(license.LicenseClass).ClassFees;
                _newLicense.IsActive = true;
                _newLicense.IssueReason = 2;
                _newLicense.CreatedByUserID = clsGlobalSettings.LoggedInUserID;
                license.IsActive = false;
                if(!(_newLicense.Save() && license.Save()))
                {
                    MessageBox.Show("Error while issuing license!", "Issue error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    clsApplication.DeleteApplication(_newApplication.ApplicationID);
                    return;
                }
                MessageBox.Show($"New license issued successfully with ID {_newLicense.LicenseID}",
                    "Issue success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                LlblShowLicensesInfo.Enabled = true;
                lblRenewLicenseApplicationID.Text = _newLicense.ApplicationID.ToString();
                lblRenewedLicenseID.Text = _newLicense.LicenseID.ToString();
                btnRenew.Enabled = false;
            }
            else
                return;
        }

        private void LlblShowLicensesInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var frm = new frmLicenseInfo(_newLicense.LicenseID, true);
            frm.ShowDialog();
        }
    }
}
