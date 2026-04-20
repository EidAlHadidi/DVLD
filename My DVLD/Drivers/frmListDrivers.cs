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
    public partial class frmListDrivers : Form
    {
        DataTable _allDrivers = clsDriver.GetAllDrivers_View();

        BindingSource BS = new BindingSource();

        private void _RefreshNumberOfRecords()
        {
            lblNumberOfRecords.Text = dgvDrivers.Rows.Count.ToString();
        }

        private void _RefreshRecords()
        {
            _allDrivers = clsDriver.GetAllDrivers_View();
            BS.DataSource = _allDrivers;
            dgvDrivers.DataSource = BS;
            _RefreshNumberOfRecords();
        }

        public frmListDrivers()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListDrivers_Load(object sender, EventArgs e)
        {
            cbFilterDriversBy.SelectedIndex = 0;
            _RefreshRecords();
        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilterDriversBy.Text == "Person ID")
            {
                e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
            }
        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            if(txtFilterBy.Text == string.Empty)
            {
                BS.RemoveFilter();
                _RefreshNumberOfRecords();
                return;
            }
            BS.RemoveFilter();
            switch (cbFilterDriversBy.Text)
            {
                case "Person ID":
                    BS.Filter = $"PersonID = {txtFilterBy.Text}";
                    break;
                case "Driver ID":
                    BS.Filter = $"DriverID = {txtFilterBy.Text}";
                    break;
                case "National No.":
                    BS.Filter = $"NationalNo like '{txtFilterBy.Text}%'";
                    break;
                case "Full Name":
                    BS.Filter = $"FullName like '{txtFilterBy.Text}%'";
                    break;
            }
            _RefreshNumberOfRecords();
        }

        private void cbFilterDriversBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterBy.Clear();
            if (cbFilterDriversBy.Text == "None")
            {
                txtFilterBy.Visible = false;
            }
            else
                txtFilterBy.Visible = true;
        }
    }
}
