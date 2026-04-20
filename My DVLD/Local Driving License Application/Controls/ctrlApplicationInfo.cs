using Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My_DVLD
{
    public partial class ctrlApplicationInfo : UserControl
    {
        public ctrlApplicationInfo()
        {
            InitializeComponent();
        }
     
        clsLocalDrivingLicenseApplication _LocalApp;
        
        public string ApplicationStatus
        {
            get
            {
                switch (_LocalApp.Application.ApplicationStatus)
                {
                    case 1:
                        return "New";
                    case 2:
                        return "Cancelled";
                    case 3:
                        return "Completed";
                    default:
                        return "Not Specified";
                }
            }
        }


        public void FillData(int ID)
        {
            if ((_LocalApp = clsLocalDrivingLicenseApplication.Find(ID)) == null)
            {
                MessageBox.Show("Error while loading license data!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            lblDLAPP_ID.Text = _LocalApp.LocalDrivingLicenseApplicationID.ToString();
            lblAppliedForLicense.Text = _LocalApp.LicenseClass.ClassName.ToString();
            lblPassedTests.Text = $"{_LocalApp.PassedTestsCount.ToString()}/3";
            lblApplicationID.Text = _LocalApp.ApplicationID.ToString();
            lblStatus.Text = ApplicationStatus.ToString();
            lblFees.Text = _LocalApp.Application.ApplicationType.ApplicationFees.ToString();
            lblType.Text = _LocalApp.Application.ApplicationType.ApplicationTypeTitle.ToString();
            lblApplicant.Text = _LocalApp.Application.Person.FullName.ToString();
            lblDate.Text = _LocalApp.Application.ApplicationDate.ToString();
            lblStatusDate.Text = _LocalApp.Application.LastStatusDate.ToString();
            lblCreatedByUser.Text = _LocalApp.Application.CreatedUser.UserName.ToString();
        }

        private void ctrlApplicationInfo_Load(object sender, EventArgs e)
        {
            
        }

        private void LlblViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var frm = new frmShowPersonInformation(_LocalApp.Application.ApplicantPersonID);
            frm.ShowDialog();
        }
    }
}
