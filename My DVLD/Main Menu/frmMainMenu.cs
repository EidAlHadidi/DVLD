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
    public partial class frmMainMenu : Form
    {
        private int _LoggedInUserID;
        //private clsUser _LoggedInUser;

        public delegate void SignedOut(object sender);

        public event SignedOut onSignedOut;

        protected virtual void SignedOutClicked()
        {
            SignedOut handler = onSignedOut;
            if(handler != null )
            {
                handler?.Invoke(this);
            }
        }

        public frmMainMenu()
        {
            InitializeComponent();
        }

        public frmMainMenu(int UserID)
        {
            InitializeComponent();
            _LoggedInUserID = UserID;
            //_LoggedInUser = clsUser.Find(_LoggedInUserID);
        }


        private void frmMainMenu_Load(object sender, EventArgs e)
        {
            pbMainMenuImage.BackColor = Color.Black;
        }

        private void miApplications_Click(object sender, EventArgs e)
        {

        }

        private void miPeople_Click(object sender, EventArgs e)
        {
            frmManagePeople frm = new frmManagePeople();
            frm.ShowDialog();
        }

        private void miDrivers_Click(object sender, EventArgs e)
        {
            var frm = new frmListDrivers();
            frm.ShowDialog();
        }

        private void miUsers_Click(object sender, EventArgs e)
        {
            frmManageUsers frm = new frmManageUsers();
            frm.ShowDialog();
        }

        private void miAccountSettings_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripMenuItemSignOut_Click(object sender, EventArgs e)
        {
            SignedOutClicked();
            this.Close();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            frmShowUserInfo frm = new frmShowUserInfo(clsGlobalSettings.LoggedInUserID);
            frm.ShowDialog();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(clsGlobalSettings.LoggedInUserID);
            frm.ShowDialog();
        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageApplicationTypes frm = new frmManageApplicationTypes();
            frm.ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageTestTypes frm = new frmManageTestTypes();
            frm.ShowDialog();
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewLocalDrivingLicenseApplication frm = new frmNewLocalDrivingLicenseApplication();
            frm.ShowDialog();
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Form frm = new frmManageLocalDrivingLicenseApplications();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(("Error : " + ex.Message), "Error");
            }
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmNewInternationalLicenseApplication();
            frm.ShowDialog();
        }

        private void internationalLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmManageInternationalLicenseApplications();
            frm.ShowDialog();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmRenewLocalDrivingLicense();
            frm.ShowDialog();
        }

        private void replacementForLostOrDamagedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmReplaceLicense();
            frm.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmDetainLicense();
            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmReleaseDetainedLicense();
            frm.ShowDialog();
        }

        private void releaseDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmReleaseDetainedLicense();
            frm.ShowDialog();
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmManageLocalDrivingLicenseApplications();
            frm.ShowDialog();
        }

        private void manageDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmManageDetainedLicenses();
            frm.ShowDialog();
        }
    }
}
