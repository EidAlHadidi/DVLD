using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using Business;
using Microsoft.Win32;

namespace My_DVLD
{
    public partial class frmLoginScreen : Form
    {
        const string LogPath = @"C:\Users\Eid AlHadidi\Documents\Study\C# Projects\My DVLD\LogInfo.txt";

        const char Seperator = '#';

        public frmLoginScreen()
        {
            InitializeComponent();
        }

        private int _UserID = -1;
        private clsUser _User;

        private bool _CheckUserAndPassword()
        {
            bool LoginSucceed = false;

            _User = clsUser.Find(txtUserName.Text.Trim(),txtPassword.Text.Trim());

            if (_User == null)
                LoginSucceed = false;
            else
            {
                LoginSucceed = true;
                _UserID = _User.UserID;
            }

            return LoginSucceed;
        }

        private void _RememberMe(string UserName,string Password)
        {
            StreamWriter SW = new StreamWriter(LogPath);
            if(UserName == string.Empty && Password == string.Empty)
            {
                return;
            }
            using(SW)
            {
                SW.WriteLine(UserName + Seperator + Password);
            }
            SW.Close();
        }

        private void _RememberMeAnotherMethod(string UserName,string Password)
        {
            string UserValueName = "Username", UserPasswordName = "Password", UserValue = UserName, PasswordValue = Password;
            string KeyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD";
            try
            {
                Registry.SetValue(KeyPath, UserValueName, "");
                Registry.SetValue(KeyPath, UserPasswordName, "");
            }
            catch (Exception)
            {

                throw;
            }
            if (UserName == string.Empty || Password == string.Empty)
                return;
            try
            {
                Registry.SetValue(KeyPath, UserValueName,UserValue );
                Registry.SetValue(KeyPath, UserPasswordName,Password);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void _LoadUserNameAndPassword()
        {
            StreamReader SR = new StreamReader(LogPath);
            using (SR)
            {
                string Line = SR.ReadLine();
                if(!string.IsNullOrEmpty(Line))
                {
                    string[] Fields = Line.Split(Seperator);
                    txtUserName.Text = Fields[0];
                    txtPassword.Text = Fields[1];
                }
            }
            SR.Close();
        }
		private void _LoadUserNameAndPasswordAnotherMethod()
		{
            string KeyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD";
            string ValueName = "Username";
            string ValueName_Password = "Password";
			try
            {
                string Username = Registry.GetValue(KeyPath, ValueName, null) as string;
                string Password = Registry.GetValue(KeyPath, ValueName_Password, null) as string;
				if (Username != null)
				{
                    txtUserName.Text = Username;
				}
                if (Password != null)
                    txtPassword.Text = Password;
               
			}
            catch (Exception)
            {

                throw;
            }
		}
		private void HandleSignOut(object sender)
        {
            this.Show();
        }

        private void LoginIn()
        {
            clsGlobalSettings.LoggedInUserID = _UserID;
            frmMainMenu frm = new frmMainMenu();
            this.Hide();
            frm.Show();
            frm.onSignedOut += HandleSignOut;
        }

        // end my methods

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!_CheckUserAndPassword())
            {
                MessageBox.Show("Invalid Username/Password", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if(_User.IsActive == false)
                {
                    MessageBox.Show("The user is not active!", "Not Active", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (cbRememberMe.Checked)
                {
                    _RememberMeAnotherMethod(txtUserName.Text.Trim(), txtPassword.Text.Trim());
                }
                else
                    _RememberMeAnotherMethod("", "");

                LoginIn();

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLoginScreen_Load(object sender, EventArgs e)
        {
            _LoadUserNameAndPasswordAnotherMethod();
        }

	}
}
