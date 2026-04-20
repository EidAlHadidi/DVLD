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
    public partial class frmDetainLicense : Form
    {
        public frmDetainLicense()
        {
            InitializeComponent();
        }

        clsLicense license;
        clsDetainedLicense detain;

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDetainLicense_Load(object sender, EventArgs e)
        {
            lblDetainDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedBy.Text = clsUser.Find(clsGlobalSettings.LoggedInUserID).UserName.ToString();
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(clsLicense obj)
        {
            if(obj!= null)
            {
                license = clsLicense.Find(obj.LicenseID);
                LlblShowLicenseHistory.Enabled = true;
                lblLicenseID.Text = license.LicenseID.ToString();
                if (!license.IsActive)
                {
                    MessageBox.Show("The selected licese is not active", "Not active", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (clsDetainedLicense.IsLicenseDetained(license.LicenseID))
                {
                    MessageBox.Show("The selected license is already detained!", "Detained license", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                btnDetain.Enabled = true;

            }
            else
            {
                lblLicenseID.Text = "??";
                btnDetain.Enabled = false;
                LlblShowLicenseHistory.Enabled = false;
            }
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to detain this license?",
                "Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                detain = new clsDetainedLicense();
                detain.LicenseID = license.LicenseID;
                detain.DetainDate = DateTime.Now;
                detain.FineFees = string.IsNullOrEmpty(txtFineFees.Text) ? 0f : Convert.ToSingle(txtFineFees.Text);
                detain.CreatedByUserID = clsGlobalSettings.LoggedInUserID;
                detain.IsReleased = false;
                if (!detain.Save())
                {
                    MessageBox.Show("Error occured while detain process"
                        , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MessageBox.Show($"License detained successfully with ID {detain.DetainID}",
                    "License Detained", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LlblShowLicensesInfo.Enabled = true;
                btnDetain.Enabled = false;
                ctrlDriverLicenseInfoWithFilter1.MakeFilterDisabled();
                lblDetainID.Text = detain.DetainID.ToString();
            }
        }

        private void LlblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var frm = new frmLicenseHistory(license.Application.ApplicantPersonID);
            frm.ShowDialog();
        }

        private void txtFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void LlblShowLicensesInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var frm = new frmLicenseInfo(detain.LicenseID, true);
            frm.ShowDialog();
        }
    }
}
