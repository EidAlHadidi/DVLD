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
    public partial class ctrlPersonInformation : UserControl
    {
        public ctrlPersonInformation()
        {
            InitializeComponent();
        }

        public enum enGender { Male, Female }

        private clsPerson _Person;

        private int _PersonID;

        public void ResetToDefaults()
        {
            lblPersonID.Text = "[???]";
            lblFullName.Text = "[???]";
            lblNationalNo.Text = "[???]";
            lblGendor.Text = "[???]";
            lblEmail.Text = "[???]";
            lblAddress.Text = "[???]";
            lblDateOfBirth.Text = "[???]";
            lblPhone.Text = "[???]";
            lblCountry.Text = "[???]";
            pbImage.Image = Properties.Resources.Male_512;
            LlblEditPersonInformation.LinkVisited = false;
            LlblEditPersonInformation.Enabled = false;
        }

        public void FillData(int PersonID)
        {
            _PersonID = PersonID;
            ResetToDefaults();
            _Person = clsPerson.Find(PersonID);
            if(_Person == null)
            {
                ResetToDefaults();
                MessageBox.Show($"Error while showing person {PersonID} information",
                    "Person not found",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            lblPersonID.Text = _Person.ID.ToString();
            lblFullName.Text = _Person.FullName.ToString();
            lblNationalNo.Text = _Person.NationalNo.ToString();
            if(_Person.Gendor == (int)enGender.Male)
            {
                pbGendor.Image = Properties.Resources.Man_32;
                lblGendor.Text = "Male";
            }
            else
            {
                pbGendor.Image = Properties.Resources.Woman_32;
                lblGendor.Text = "Female";
            }
            if (!string.IsNullOrEmpty(_Person.Email))
                lblEmail.Text = _Person.Email;
            lblAddress.Text = _Person.Address;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToShortDateString();
            lblPhone.Text = _Person.Phone;
            lblCountry.Text = _Person.CountryInfo.CountryName;
            if(!string.IsNullOrEmpty(_Person.ImagePath))
            {
                pbImage.ImageLocation = _Person.ImagePath;
            }
            else
            {
                if(_Person.Gendor == (int)enGender.Male)
                {
                    pbImage.Image = Properties.Resources.Male_512;
                }
                else
                {
                    pbImage.Image = Properties.Resources.Female_512;
                }
            }
            LlblEditPersonInformation.Enabled = true;
        }

        private void DataBackMethod(object sender,int PersonID)
        {
            FillData(PersonID);
        }

        private void LlblEditPersonInformation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson(_PersonID);
            frm.onPersonSelected += DataBackMethod;
            frm.ShowDialog();
        }
    }
}
