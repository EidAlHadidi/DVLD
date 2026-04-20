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
    public partial class frmAddEditUser : Form
    {
        public frmAddEditUser()
        {
            InitializeComponent();
        }

        public frmAddEditUser(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
            _User = clsUser.Find(_UserID);
            _PersonID = _User.PersonID;
            Mode = enMode.Update;
        }

        public enum enMode { AddNew, Update }

        public enMode Mode = enMode.AddNew;

        private int _PersonID =-1;
        private clsUser _User;
        private int _UserID;
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if(Mode == enMode.AddNew)
            {
                if(_PersonID == -1)
                {
                    MessageBox.Show("No person selected!", "No person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                _User = clsUser.Find(_PersonID,true);
                if (_User != null)
                {
                    MessageBox.Show("This person already has a user",
                        "Person linked with user", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                    _User = new clsUser();
                tcUserInfo.SelectedTab = tpLogInfo;
            }
            else
                tcUserInfo.SelectedTab = tpLogInfo;
        }

        private void ctrlPersonInformationWithFilter1_OnPersonSelected_1(object sender, int PersonID)
        {
            _PersonID = PersonID;
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Password doesn't match");
            }
            else
                errorProvider1.SetError(txtConfirmPassword, null);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Not all fields filled correctly", "Filling error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _User.PersonID = _PersonID;
            _User.UserName = txtUserName.Text;
            _User.Password = txtPassword.Text;
            _User.IsActive = ckbIsActive.Checked;
            if(_User.Save())
            {
                MessageBox.Show("User Saved Successfully", "succeed"
                    , MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error while creating user",
                    "Creation error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            lblUserID.Text = _User.UserID.ToString();
            lblMode.Text = "Update User";
        }

        private void ValidateEmptyString(object sender,CancelEventArgs e)
        {
            TextBox temp = (TextBox)sender;
            if (temp.Text == string.Empty)
            {
                e.Cancel = true;
                errorProvider1.SetError(temp, "This field cannot be blank");
            }
            else
                errorProvider1.SetError(temp, null);
        }

        private void _LoadData()
        {
            _User = clsUser.Find(_UserID);
            lblMode.Text = "Update User = " + _UserID.ToString();
            ctrlPersonInformationWithFilter1.MakeFilterDisabled();
            ctrlPersonInformationWithFilter1.FillData(_User.PersonID);
            txtUserName.Text = _User.UserName;
            txtPassword.Text = _User.Password;
            txtConfirmPassword.Text = _User.Password;
            ckbIsActive.Checked = _User.IsActive;
        }

        private void frmAddEditUser_Load(object sender, EventArgs e)
        {
            if(Mode == enMode.Update)
                _LoadData();
        }
    }
}
