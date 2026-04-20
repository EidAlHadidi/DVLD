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
    public partial class frmChangePassword : Form
    {
        private int _UserID;
        private clsUser _User;

        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
            _User = clsUser.Find(_UserID);
        }



        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            ctrlPersonInformation1.FillData(_User.PersonID);
            lblUserID.Text = _UserID.ToString();
            lblUserName.Text = _User.UserName.ToString();
            lblIsActive.Text = (_User.IsActive) ? "Yes" : "No";
        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (_User.Password != txtCurrentPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Not the same password!");
            }
            else
                errorProvider1.SetError(txtCurrentPassword, null);
        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewPassword.Text)) 
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "This field cannot be blank");
            }
            else
                errorProvider1.SetError(txtCurrentPassword, null);
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if(txtNewPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "The password does not the same");
            }
            else
                errorProvider1.SetError(txtCurrentPassword, null);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Not all the fields filled correctly", "Filling error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _User.Password = txtNewPassword.Text;
            if(_User.Save())
            {
                MessageBox.Show("Password updated successfully",
                    "Password update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error while updating password", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
