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
    public partial class frmLicenseHistory : Form
    {
        DataTable _AllInternationalLiceses;
        DataTable _AllLocalLicenses;
        DataTable _LocalLicenses;
        DataTable _InternationalLicenses;
        clsPerson Person;
        public frmLicenseHistory(int PersonID)
        {
            Person = clsPerson.Find(PersonID);
            InitializeComponent();
        }

        //public frmLicenseHistory(int PersonID,bool SearchByPersonID)
        //{
        //    clsPerson person = clsPerson.Find(PersonID);
        //    InitializeComponent();
        //}

        private void _ModifyInternationalLicensesColumnNames()
        {
            dgvInternationalLicensesHistory.Columns["InternationalLicenseID"].HeaderText = "Int.License ID";
            dgvInternationalLicensesHistory.Columns["ApplicationID"].HeaderText = "Application ID";
            dgvInternationalLicensesHistory.Columns["IssuedUsingLocalLicenseID"].HeaderText = "L.License ID";
            dgvInternationalLicensesHistory.Columns["IssueDate"].HeaderText = "Issue Date";
            dgvInternationalLicensesHistory.Columns["ExpirationDate"].HeaderText = "Expiration Date";
            dgvInternationalLicensesHistory.Columns["IsActive"].HeaderText = "Is Active";
        }

        private void _FillDGV_International(int ID)
        {
            _AllInternationalLiceses = clsInternationalLicense.GetAllInternationalLicenses_ByPersonID(ID);
            if(_AllInternationalLiceses.Rows.Count != 0)
            {
                _InternationalLicenses = _AllInternationalLiceses.DefaultView.ToTable(false, "InternationalLicenseID", "ApplicationID",
                    "IssuedUsingLocalLicenseID", "IssueDate", "ExpirationDate", "IsActive");
                dgvInternationalLicensesHistory.DataSource = _InternationalLicenses;
                _ModifyInternationalLicensesColumnNames();
            }
            else
            {
                dgvInternationalLicensesHistory.Columns.Clear();
            }
        }

        private void _ModifyLocalLicensesColumnNames()
        {
            dgvLocalLicensesHistory.Columns["LicenseID"].HeaderText = "Lic.ID";   
            dgvLocalLicensesHistory.Columns["ApplicationID"].HeaderText = "App.ID";   
            dgvLocalLicensesHistory.Columns["ClassName"].HeaderText = "Class Name";   
            dgvLocalLicensesHistory.Columns["IssueDate"].HeaderText = "Issue Date";   
            dgvLocalLicensesHistory.Columns["ExpirationDate"].HeaderText = "Expiration Date";   
            dgvLocalLicensesHistory.Columns["IsActive"].HeaderText = "Is Active";   
        }

        private void _FillDGV_Local(int ID)
        {
            _AllLocalLicenses = clsLicense.LicensesView_PersonID(ID);
            if(_AllLocalLicenses.Rows.Count != 0)
            {
                _LocalLicenses = _AllLocalLicenses.DefaultView.ToTable(false, "LicenseID",
                    "ApplicationID", "ClassName", "IssueDate", "ExpirationDate", "IsActive");
                dgvLocalLicensesHistory.DataSource = _LocalLicenses;
                _ModifyLocalLicensesColumnNames();
            }
            else
            {
                dgvLocalLicensesHistory.Columns.Clear();
            }
        }

        private void _FillData()
        {
            ctrlPersonInformationWithFilter1.FillWithFilter(Person.ID);
            _RefresRecords();
        }

        private void _RefresRecords()
        {
            _FillDGV_Local(Person.ID);
            _FillDGV_International(Person.ID);
            lblNumberOfInternationalLicenses.Text = dgvInternationalLicensesHistory.Rows.Count.ToString();
            lblNumberOfLocalLicenses.Text = dgvLocalLicensesHistory.Rows.Count.ToString();
        }

        private void frmLicenseHistory_Load(object sender, EventArgs e)
        {
            _FillData();
            _RefresRecords();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmLicenseInfo(Convert.ToInt32(dgvLocalLicensesHistory.CurrentRow.Cells[0].Value),true);
            frm.ShowDialog();
        }

        private void showLicenseInfoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var frm = new frmInternationalDriverInfo(Convert.ToInt32(dgvInternationalLicensesHistory.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
        }
    }
}
