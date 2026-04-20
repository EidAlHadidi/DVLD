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
    public partial class frmManageInternationalLicenseApplications : Form
    {
        public frmManageInternationalLicenseApplications()
        {
            InitializeComponent();
        }

        BindingSource BS = new BindingSource();

        static DataTable _dtAllInternationalApps = clsInternationalLicense.GetAllInternationalLicenses();
        static DataTable _InternationalApps = _dtAllInternationalApps.DefaultView.ToTable(false,
            "InternationalLicenseID", "ApplicationID", "DriverID", 
            "IssuedUsingLocalLicenseID", "IssueDate", "ExpirationDate", "IsActive");

        private void _ModifyDataGridViewColumnNames()
        {
            dgvInternationalApplications.Columns["InternationalLicenseID"].HeaderText = "Int.License ID";
            dgvInternationalApplications.Columns["ApplicationID"].HeaderText = "Application ID";
            dgvInternationalApplications.Columns["DriverID"].HeaderText = "Driver ID";
            dgvInternationalApplications.Columns["IssuedUsingLocalLicenseID"].HeaderText = "L.License ID";
            dgvInternationalApplications.Columns["IssueDate"].HeaderText = "IssueDate";
            dgvInternationalApplications.Columns["ExpirationDate"].HeaderText = "Expiration Date";
            dgvInternationalApplications.Columns["IsActive"].HeaderText = "Is Active";
        }

        private void _RefreshNumberOfRecords()
        {
            lblNumberOfRecords.Text = dgvInternationalApplications.Rows.Count.ToString();
        }

        private void _RefreshRecords()
        {
            _dtAllInternationalApps = clsInternationalLicense.GetAllInternationalLicenses();
            _InternationalApps = _dtAllInternationalApps.DefaultView.ToTable(false,
            "InternationalLicenseID", "ApplicationID", "DriverID",
            "IssuedUsingLocalLicenseID", "IssueDate", "ExpirationDate", "IsActive");
            BS.DataSource = _InternationalApps;
            dgvInternationalApplications.DataSource = BS;
            _ModifyDataGridViewColumnNames();
            _RefreshNumberOfRecords();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNewInternationalApplication_Click(object sender, EventArgs e)
        {
            var frm = new frmNewInternationalLicenseApplication();
            frm.ShowDialog();
            _RefreshRecords();
        }

        private void frmManageInternationalLicenseApplications_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            _RefreshRecords();
        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
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
                case "Int.License ID":
                    BS.Filter = $"InternationalLicenseID = {txtFilterBy.Text.Trim()}";
                    break;
                case "Application ID":
                    BS.Filter = $"ApplicationID = {txtFilterBy.Text.Trim()}";
                    break;
                case "Driver ID":
                    BS.Filter = $"DriverID = {txtFilterBy.Text.Trim()}";
                    break;
                case "L.License ID":
                    BS.Filter = $"IssuedUsingLocalLicenseID = {txtFilterBy.Text.Trim()}";
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
            clsDriver driver = clsDriver.Find((int)dgvInternationalApplications.CurrentRow.Cells[2].Value);
            var frm = new frmShowPersonInformation(driver.PersonID);
            frm.ShowDialog();
            _RefreshRecords();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmLicenseInfo((int)dgvInternationalApplications.CurrentRow.Cells[3].Value,true);
            frm.ShowDialog();
            _RefreshRecords();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLicense license = clsLicense.Find((int)dgvInternationalApplications.CurrentRow.Cells[3].Value);
            var frm = new frmLicenseHistory(license.Application.ApplicantPersonID);
            frm.ShowDialog();
            _RefreshRecords();
        }
    }
}
