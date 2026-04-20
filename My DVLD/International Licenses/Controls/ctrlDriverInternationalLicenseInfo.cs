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
    public partial class ctrlDriverInternationalLicenseInfo : UserControl
    {
        public ctrlDriverInternationalLicenseInfo()
        {
            InitializeComponent();
        }

        private clsInternationalLicense _InternationalLicense = new clsInternationalLicense();
        private clsPerson _person;
       
        public void FillData(int InternationlLicenseID)
        {
            _InternationalLicense = clsInternationalLicense.Find(InternationlLicenseID);
            _person = clsPerson.Find(_InternationalLicense.Application.ApplicantPersonID);
            lblInternationalLicenseID.Text = InternationlLicenseID.ToString();
            lblName.Text = _person.FullName;
            lblLicenseID.Text = _InternationalLicense.IssuedUsingLocalLicenseID.ToString();
            lblNationalNo.Text = _person.NationalNo;
            lblGender.Text = _person.Gendor == 0 ? "Male" : "Female";
            lblIssueDate.Text = _InternationalLicense.IssueDate.ToShortDateString();
            lblApplicationID.Text = _InternationalLicense.ApplicationID.ToString();
            lblIsActive.Text = _InternationalLicense.IsActive ? "Yes" : "No";
            lblDateOfBirth.Text = _person.DateOfBirth.ToShortDateString();
            lblDriverID.Text = _InternationalLicense.DriverID.ToString();
            lblExpirationDate.Text = _InternationalLicense.ExpirationDate.ToShortDateString();
            _SetImage();
        }

        private void _SetDefaultImage(int Gender)
        {
            if (Gender == 0)
            {
                pbPersonalImage.ImageLocation = @"C:\Users\Eid AlHadidi\Documents\Study\Course 19\Icons\Icons\Male 512.png".ToString();
            }
            else
                pbPersonalImage.ImageLocation = @"C:\Users\Eid AlHadidi\Documents\Study\Course 19\Icons\Icons\Female 512.png".ToString();
        }

        private void _SetImage()
        {
            if (string.IsNullOrEmpty(_person.ImagePath))
            {
                _SetDefaultImage(_person.Gendor);
            }
            else
                pbPersonalImage.ImageLocation = _person.ImagePath;
        }


    }
}
