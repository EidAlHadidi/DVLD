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
    public partial class frmManageDetainedLicenses : Form
    {
        public frmManageDetainedLicenses()
        {
            InitializeComponent();
        }

        clsLicense license;

        BindingSource BS = new BindingSource();

        static DataTable _dtAllDetainedLicenses = clsDetainedLicense.GetAllDetainedLicenses_View();

        private void _ModifyDataGridViewColumnNames()
        {
            dgvDetainedLicenses.Columns["DetainID"].HeaderText = "D.ID";
            dgvDetainedLicenses.Columns["LicenseID"].HeaderText = "L.ID";
            dgvDetainedLicenses.Columns["DetainDate"].HeaderText = "D.Date";
            dgvDetainedLicenses.Columns["IsReleased"].HeaderText = "Is Released";
            dgvDetainedLicenses.Columns["FineFees"].HeaderText = "Fine Fees";
            dgvDetainedLicenses.Columns["ReleaseDate"].HeaderText = "Release Date"; 
            dgvDetainedLicenses.Columns["NationalNo"].HeaderText = "N.No.";
            dgvDetainedLicenses.Columns["FullName"].HeaderText = "Full Name";
            dgvDetainedLicenses.Columns["ReleaseApplicationID"].HeaderText = "Release App.ID";
        }

        private void _RefreshNumberOfRecords()
        {
            lblNumberOfRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _RefreshRecords()
        {
            _dtAllDetainedLicenses = clsDetainedLicense.GetAllDetainedLicenses_View();
            BS.DataSource = _dtAllDetainedLicenses;
            dgvDetainedLicenses.DataSource = BS;
            _ModifyDataGridViewColumnNames();
            _RefreshNumberOfRecords();
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            var frm = new frmDetainLicense();
            frm.ShowDialog();
            _RefreshRecords();
        }

        private void btnReleaseLicense_Click(object sender, EventArgs e)
        {
            var frm = new frmReleaseDetainedLicense();
            frm.ShowDialog();
            _RefreshRecords();
        }

        private void frmManageDetainedLicenses_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            _RefreshRecords();
        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Is Released")
                e.Handled = e.KeyChar != '0' && e.KeyChar != '1' && !char.IsControl(e.KeyChar);
            if (cbFilterBy.Text == "Detain ID" || cbFilterBy.Text == "Release Application ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            if (txtFilterBy.Text == string.Empty)
            {
                BS.RemoveFilter();
                _RefreshNumberOfRecords();
                return;
            }
            BS.RemoveFilter();
            switch (cbFilterBy.Text)
            {
                /*
                 Detain ID
                Is Released
                National No.
                Full Name
                Release Application ID
                 */
                case "Detain ID":
                    BS.Filter = $"DetainID= {txtFilterBy.Text.Trim()}";
                    break;
                case "Is Released":
                    BS.Filter = $"IsReleased= {txtFilterBy.Text.Trim()}";
                    break;
                case "National No.":
                    BS.Filter = $"NationalNo like '{txtFilterBy.Text.Trim()}%'";
                    break;
                case "Full Name":
                    BS.Filter = $"FullName like '{txtFilterBy.Text.Trim()}%'";
                    break;
                case "Release Application ID":
                    BS.Filter = $"ReleaseApplicationID = {txtFilterBy.Text.Trim()}";
                    break;
            }
            _RefreshNumberOfRecords();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterBy.Enabled = (cbFilterBy.Text != "None");
            txtFilterBy.Clear();
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            license = clsLicense.Find(Convert.ToInt32(dgvDetainedLicenses.CurrentRow.Cells[1].Value));
            var frm = new frmShowPersonInformation(license.Application.ApplicantPersonID);
            frm.ShowDialog();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            license = clsLicense.Find(Convert.ToInt32(dgvDetainedLicenses.CurrentRow.Cells[1].Value));
            var frm = new frmLicenseInfo(license.LicenseID,true);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            license = clsLicense.Find(Convert.ToInt32(dgvDetainedLicenses.CurrentRow.Cells[1].Value));
            var frm = new frmLicenseHistory(license.Application.ApplicantPersonID);
            frm.ShowDialog();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (Convert.ToBoolean(dgvDetainedLicenses.CurrentRow.Cells[3].Value))
            {
                releaseDetainedLicenseToolStripMenuItem.Enabled = false;
            }
            else
                releaseDetainedLicenseToolStripMenuItem.Enabled = true;
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmReleaseDetainedLicense(Convert.ToInt32(dgvDetainedLicenses.CurrentRow.Cells[1].Value));
            frm.Caller = "Exist";
            frm.ShowDialog();
            _RefreshRecords();
        }
    }
}
