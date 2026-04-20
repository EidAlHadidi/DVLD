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
    public partial class frmReleaseDetainedLicense : Form
    {
        clsDetainedLicense _detain;
        clsLicense _license;
        clsApplication _application;
        public string Caller { get; set; }

        public frmReleaseDetainedLicense()
        {
            Caller = "New";
            InitializeComponent();
        }

        public frmReleaseDetainedLicense(int LicenseID)
        {
            _license = clsLicense.Find(LicenseID);
            Caller = "Exist";
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _ResetLabelsToDefault()
        {
            lblDetainID.Text = "??";
            lblDetainDate.Text = "??";
            lblApplicationFees.Text = "??";
            lblTotalFees.Text = "??";

            lblLicenseID.Text = "??";
            lblCreatedBy.Text = "??";
            lblFineFees.Text = "??";
            lblReleaseApplicationID.Text = "??";

            btnRelease.Enabled = false;
            LlblShowLicenseHistory.Enabled = false;
            LlblShowLicensesInfo.Enabled = false;
        }

        private void frmReleaseDetainedLicense_Load(object sender, EventArgs e)
        {
            if(Caller == "Exist")
            {
                ctrlDriverLicenseInfoWithFilter1.FillData(_license.LicenseID);
                ctrlDriverLicenseInfoWithFilter1.MakeFilterDisabled();
            }
            else
            {
                //for 0 args case
                _ResetLabelsToDefault();
            }
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(clsLicense obj)
        {
            if(obj != null)
            {
                _license = clsLicense.Find(obj.LicenseID);
                _detain = clsDetainedLicense.Find_ByLicenseID(_license.LicenseID);
                LlblShowLicenseHistory.Enabled = true;
                lblLicenseID.Text = _license.LicenseID.ToString();
                btnRelease.Enabled = false;
                if (!clsDetainedLicense.IsLicenseDetained(_license.LicenseID))
                {
                    MessageBox.Show("The license is not detained, choose a detained license to be released",
                        "License not detained", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                lblDetainID.Text = _detain.DetainID.ToString();
                lblDetainDate.Text = _detain.DetainDate.ToShortDateString();
                lblCreatedBy.Text = clsUser.Find(clsGlobalSettings.LoggedInUserID).UserName.ToString();
                lblApplicationFees.Text = clsApplicationType.Find(5).ApplicationFees.ToString();
                lblFineFees.Text = _detain.FineFees.ToString();
                lblTotalFees.Text = (_detain.FineFees + clsApplicationType.Find(5).ApplicationFees).ToString();
                btnRelease.Enabled = true;
            }
            else
            {
                _ResetLabelsToDefault();
            }
        }

        private void LlblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var frm = new frmLicenseHistory(_license.Application.ApplicantPersonID);
            frm.ShowDialog();
        }

        private void LlblShowLicensesInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var frm = new frmLicenseInfo(_license.LicenseID,true);
            frm.ShowDialog();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if( MessageBox.Show("Are you sure you want to release this detained license ?",
                "Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question)== DialogResult.Yes)
            {
                _application = new clsApplication();
                _application.ApplicantPersonID = _license.Application.ApplicantPersonID;
                _application.ApplicationDate = DateTime.Now;
                _application.ApplicationTypeID = 5;
                _application.ApplicationStatus = 3;
                _application.LastStatusDate = DateTime.Now;
                _application.PaidFees = clsApplicationType.Find(5).ApplicationFees;
                _application.CreatedByUserID = clsGlobalSettings.LoggedInUserID;
                if (!_application.Save())
                {
                    MessageBox.Show("Error while saving release base application", "Save error"
                        , MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                _detain.IsReleased = true;
                _detain.ReleaseDate = DateTime.Now;
                _detain.ReleasedByUserID = clsGlobalSettings.LoggedInUserID;
                _detain.ReleasedApplicationID = _application.ApplicationID;
                if (!_detain.Save())
                {
                    MessageBox.Show("Error while saving detain record!", "Save error"
                        , MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MessageBox.Show("Detained license released successfully", "Detained license released", MessageBoxButtons.OK, MessageBoxIcon.Information);

                lblReleaseApplicationID.Text = _detain.ReleasedApplicationID.ToString();
                //
                btnRelease.Enabled = false;
                LlblShowLicensesInfo.Enabled = true;
                ctrlDriverLicenseInfoWithFilter1.MakeFilterDisabled();
            }
        }
    }
}
