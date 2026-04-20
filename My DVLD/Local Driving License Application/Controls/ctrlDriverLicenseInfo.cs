using Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My_DVLD
{
    public partial class ctrlDriverLicenseInfo : UserControl
    {
        private int _LicenseID { get; set; }
        private clsLicense license {  get; set; }
        private clsPerson person
        {
            get
            {
                return clsPerson.Find(license.Application.ApplicantPersonID);
            }
        }
        public ctrlDriverLicenseInfo()
        {
            InitializeComponent();
        }

        private void _SetDefaultImage(int Gender)
        {
            if(Gender == 0)
            {
                pbPersonalImage.ImageLocation = @"C:\Users\Eid AlHadidi\Documents\Study\Course 19\Icons\Icons\Male 512.png".ToString();
            }
            else
                pbPersonalImage.ImageLocation = @"C:\Users\Eid AlHadidi\Documents\Study\Course 19\Icons\Icons\Female 512.png".ToString();
        }

        private void _SetImage()
        {
            if(string.IsNullOrEmpty(person.ImagePath))
            {
                _SetDefaultImage(person.Gendor);
            }
            else
                pbPersonalImage.ImageLocation = person.ImagePath;
        }

        public void FillData(int ID,bool SearchByLocalApp = false)
        {
            if (SearchByLocalApp)
            {
                license = clsLicense.Find_ByLocalApplication(ID);
            }
            else
                license = clsLicense.Find(ID);

            if (license == null)
            {
                MessageBox.Show("Error while loading data", "Loading error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            lblLicenseClass.Text = license.License_Class.ClassName;
            lblName.Text = person.FullName;
            lblLicenseID.Text = license.LicenseID.ToString();
            lblNationalNo.Text = person.NationalNo.ToString();
            // Male
            if (person.Gendor == 0)
            {
                lblGender.Text = "Male";
                pbGender.ImageLocation = "C:\\Users\\Eid AlHadidi\\Documents\\Study\\Course 19\\Icons\\Icons\\Man 32.png";
            }
            else
            {
                lblGender.Text = "Female";
                pbGender.ImageLocation = "C:\\Users\\Eid AlHadidi\\Documents\\Study\\Course 19\\Icons\\Icons\\Woman 32.png";
            }
            lblIssueDate.Text = license.IssueDate.ToShortDateString();
            //1 - FirstTime, 2 - Renew, 3 - Replacement for Damaged, 4 - Replacement for Lost.
            switch (license.IssueReason)
            {
                case 1:
                    lblIssueReason.Text = "First Time";
                    break;
                case 2:
                    lblIssueReason.Text = "Renew";
                    break;
                case 3:
                    lblIssueReason.Text = "Replacement for Damaged";
                    break;
                case 4:
                    lblIssueReason.Text = "Replacement for Lost";
                    break;
                default:
                    lblIssueReason.Text = "First Time";
                    break;
            }
            lblNotes.Text = license.Notes.ToString();
            if (license.IsActive)
            {
                lblIsActive.Text = "Yes";
            }
            else
                lblIsActive.Text = "No";
            lblDateOfBirth.Text = person.DateOfBirth.ToShortDateString();
            lblDriverID.Text = license.DriverID.ToString();
            lblExpirationDate.Text = license.ExpirationDate.ToShortDateString();
            if (clsDetainedLicense.IsLicenseDetained(license.LicenseID))
            {
                lblIsDetained.Text = "Yes";
            }
            else
                lblIsDetained.Text = "No";
            _SetImage();
        }

        public void ResetToDefaults()
        {
            lblLicenseClass.Text = "??";
            lblName.Text = "??";
            lblLicenseID.Text = "??";
            lblNationalNo.Text = "??";
            lblGender.Text = "??";
            lblIssueDate.Text = "??";
            lblIssueReason.Text = "??";
            lblNotes.Text = "??";
            lblIsActive.Text = "??";
            lblDateOfBirth.Text = "??";
            lblDriverID.Text = "??";
            lblExpirationDate.Text = "??";
            lblIsDetained.Text = "??";
            _SetDefaultImage(0);
        }

    }
}
