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
    public partial class frmManageUsers : Form
    {
        static DataTable dtAllUsers = clsUser.GetAllUsers();
        static DataTable dtUsers = dtAllUsers.DefaultView.ToTable(true, "UserID", "PersonID", "FullName", 
            "UserName", "IsActive");
        BindingSource BS = new BindingSource();

        private void _RefreshNumberOfRecords()
        {
            lblNumberOfRecords.Text = dgvUsers.Rows.Count.ToString();
        }

        private void _RefreshRecords()
        {
            dtAllUsers = clsUser.GetAllUsers();
            dtUsers = dtAllUsers.DefaultView.ToTable(true, "UserID", "PersonID", "FullName",
                "UserName", "IsActive");
            BS.DataSource = dtUsers;
            dgvUsers.DataSource = BS;
            _RefreshNumberOfRecords();
        }

        //end my methods

        public frmManageUsers()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmManageUsers_Load(object sender, EventArgs e)
        {
            cbFilterUsersBy.SelectedIndex = 0;
            _RefreshRecords();
        }

        private void cbFilterUsersBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterBy.Clear();
            cbUserStatus.SelectedIndex = 0;
            if (cbFilterUsersBy.Text == "None")
            {
                txtFilterBy.Visible = cbUserStatus.Visible = false;
            }
            else
            {
                txtFilterBy.Visible = (cbFilterUsersBy.Text != "Is Active");
                cbUserStatus.Visible = (cbFilterUsersBy.Text == "Is Active");
            }
        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterUsersBy.Text == "Person ID" || cbFilterUsersBy.Text == "User ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
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
            switch (cbFilterUsersBy.Text)
            {
                case "User ID":
                    BS.Filter = $"UserID = {txtFilterBy.Text.Trim()}";
                    break;
                case "User Name":
                    BS.Filter = $"UserName like '{txtFilterBy.Text.Trim()}%'";
                    break;
                case "Person ID":
                    BS.Filter = $"PersonID = {txtFilterBy.Text.Trim()}";
                    break;
                case "Full Name":
                    BS.Filter = $"FullName like '{txtFilterBy.Text.Trim()}%'";
                    break;

            }
            _RefreshNumberOfRecords();
        }

        private void cbUserStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BS.RemoveFilter();
            if(cbUserStatus.Text == "Active")
            {
                BS.Filter = "IsActive = true";
            }
            else if(cbUserStatus.Text == "Not Active")
            {
                BS.Filter = "IsActive = false";
            }

            _RefreshNumberOfRecords();
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            frmAddEditUser frm = new frmAddEditUser();
            frm.ShowDialog();
            _RefreshRecords();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditUser frm = new frmAddEditUser((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshRecords();
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditUser frm = new frmAddEditUser();
            frm.ShowDialog();
            _RefreshRecords();
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Will developed soon", "Not ready", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Will developed soon", "Not ready", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsUser.DeleteUser((int)dgvUsers.CurrentRow.Cells[0].Value))
            {
                MessageBox.Show("User Deleted Successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("User not deleted because it has data linked to" +
                    " it", "Deleting error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            _RefreshRecords();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshRecords();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowUserInfo frm = new frmShowUserInfo((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshRecords();
        }
    }
}
