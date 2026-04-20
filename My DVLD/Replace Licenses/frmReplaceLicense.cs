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
    public partial class frmReplaceLicense : Form
    {
        clsLicense license { get; set; }

        int _ApplicationType { get; set; }

        clsApplication _Application;
        clsLicense _newLicense;

        DateTime _ApplicationDate = DateTime.Now;

        public frmReplaceLicense()
        {
            InitializeComponent();
        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            _ApplicationType = 4;
            lblReplacementTitle.Text = "Replacement for Damaged License";
            gbApplicationInfoFor.Text = "Application Info for Damaged License Replacement";
            this.Text = "Replacement for Damaged License";
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            _ApplicationType = 3;
            lblReplacementTitle.Text = "Replacement for Lost License";
            gbApplicationInfoFor.Text = "Application Info for Lost License Replacement";
            lblApplicationFees.Text = clsApplicationType.Find(_ApplicationType).ApplicationFees.ToString();
            this.Text = "Replacement for Lost License";
        }

        private void frmReplaceLicense_Load(object sender, EventArgs e)
        {
            rbDamagedLicense.Checked = true;
            
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblApplicationFees.Text = clsApplicationType.Find(_ApplicationType).ApplicationFees.ToString();
            lblApplicationFees.Text = clsApplicationType.Find(_ApplicationType).ApplicationFees.ToString();
            lblCreatedBy.Text = clsUser.Find(clsGlobalSettings.LoggedInUserID).UserName.ToString();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(clsLicense obj)
        {
            if(obj != null)
            {
                int LicenseID = obj.LicenseID;
                license = clsLicense.Find(LicenseID);
                LlblShowLicenseHistory.Enabled = true;
                LlblShowLicensesInfo.Enabled = false;
                lblOldLicenseID.Text = license.LicenseID.ToString();
                if (!license.IsActive)
                {
                    MessageBox.Show("Selected license is not active, choose an active one",
                        "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                btnIssueReplacement.Enabled = true;
            }
            else
            {
                lblReplaceLicenseApplicationID.Text = "??";
                lblReplacedLicenseID.Text = "??";
                lblOldLicenseID.Text = "??";
                ctrlDriverLicenseInfoWithFilter1.MakeFilterEnbled();
                gbReplacementFor.Enabled = true;
                return;
            }
        }

        private void LlblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var frm = new frmLicenseHistory(license.Application.ApplicantPersonID);
            frm.ShowDialog();
        }

        private void LlblShowLicensesInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var frm = new frmLicenseInfo(_newLicense.LicenseID, true);
            frm.ShowDialog();
        }

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            _Application = new clsApplication();
            _Application.ApplicantPersonID = license.Application.ApplicantPersonID;
            _Application.ApplicationDate = _ApplicationDate;
            _Application.ApplicationTypeID = _ApplicationType;
            _Application.ApplicationStatus = 3;
            _Application.LastStatusDate = DateTime.Now;
            _Application.PaidFees = clsApplicationType.Find(_ApplicationType).ApplicationFees;
            _Application.CreatedByUserID = clsGlobalSettings.LoggedInUserID;

            if (!_Application.Save())
            {
                MessageBox.Show("Error while saving base application", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _newLicense = new clsLicense();
            _newLicense.ApplicationID = _Application.ApplicationID;
            _newLicense.DriverID = license.DriverID;
            _newLicense.LicenseClass = license.LicenseClass;
            _newLicense.IssueDate = DateTime.Now;
            _newLicense.ExpirationDate = license.ExpirationDate;
            _newLicense.Notes = string.Empty;
            _newLicense.PaidFees = 0f;
            _newLicense.IsActive = true;
            if(_ApplicationType == 3)
            {
                _newLicense.IssueReason = 4;
            }
            else if(_ApplicationType == 4)
            {
                _newLicense.IssueReason = 3;
            }
            _newLicense.CreatedByUserID = clsGlobalSettings.LoggedInUserID;

            if (!_newLicense.Save())
            {
                MessageBox.Show("Error while issuing license", "Issue error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsApplication.DeleteApplication(_Application.ApplicationID);
                return;
            }
            MessageBox.Show($"License issued successfully with ID {_newLicense.LicenseID}", "Issue succeed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            license.IsActive = false;
            license.Save();
            //Make filter disabled
            ctrlDriverLicenseInfoWithFilter1.MakeFilterDisabled();
            //make groupbox diabled
            gbReplacementFor.Enabled = false;
            //make issue button disabled
            btnIssueReplacement.Enabled = false;
            //make show new license info enabled
            LlblShowLicensesInfo.Enabled = true;

            lblReplaceLicenseApplicationID.Text = _Application.ApplicationID.ToString();
            lblReplacedLicenseID.Text = _newLicense.LicenseID.ToString();

        }
    }
}
