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
using System.IO;
using System.Drawing.Imaging;

namespace My_DVLD
{
    public partial class frmAddEditPerson : Form
    {
        const string ImagesFolder = @"C:\DVLD-People-Images\";

        public delegate void DataBack(object sender, int PersonID);

        public event DataBack onPersonSelected;

        protected virtual void PersonSelected(int PersonID)
        {
            DataBack handler = onPersonSelected;
            if (handler != null)
                handler?.Invoke(this, PersonID);
        }

        public enum enMode { AddNew, Update }

        private enMode _Mode = enMode.AddNew;

        public enum enGender { Male = 0, Female = 1 }
    
        private enGender _Gender;

        private int _ID = -1;

        private clsPerson _Person;

        public frmAddEditPerson(int ID)
        {
            InitializeComponent();
            _ID = ID;
            _Person = clsPerson.Find(ID);
            if (_Person != null)
            {
                _Mode = enMode.Update;
            }
            else
            {
                if (MessageBox.Show("There is no person with ID : " + ID.ToString() + " .... do you want to add a new person ? "
                    , "No person exists", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
                {
                    _Mode = enMode.AddNew;
                    _Person = new clsPerson();
                }
                else
                    this.Close();
            }
        }

        public frmAddEditPerson()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
            _Person = new clsPerson();
        }

        private void _SetDateTimeForPicker()
        {
            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);
            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
        }

        private void _FillCountriesInComboBox()
        {
            DataTable dt = clsCountry.GetAllCountries();
            foreach(DataRow dr in dt.Rows)
            {
                cbCountries.Items.Add(dr[1].ToString());
            }
        }

        private void ValidateEmptyString(object sender,CancelEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (string.IsNullOrEmpty(txt.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txt, "This field is required!");
            }
            else
                errorProvider1.SetError(txt, null);
        }

        private void ValidateNationalNumber(object sender,CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNationalNo.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "This field is required");
                return;
            }
            else
                errorProvider1.SetError(txtNationalNo, null);

            if (clsPerson.isPersonExist(txtNationalNo.Text) && _Person.NationalNo != txtNationalNo.Text)
            {
                e.Cancel= true;
                errorProvider1.SetError(txtNationalNo, "This national number is already assigned for another person");
            }
            else
                errorProvider1.SetError(txtNationalNo, null);

        }

        private void ValidateEmail(object sender,CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text))
                return;
            if(!clsValidation.isValidEmail(txtEmail.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Email is not valid!");
            }
            else
                errorProvider1.SetError(txtEmail, null);
        }
    
        private string _MakeGuidName()
        {
            Guid ImageName = Guid.NewGuid();
            FileInfo FI = new FileInfo(pbImage.ImageLocation);
            string extn = FI.Extension;

            return ImageName.ToString() + extn;
        }

        private void _HandleImage()
        {
            if (string.IsNullOrEmpty(pbImage.ImageLocation))
            {
                if (!string.IsNullOrEmpty(_Person.ImagePath) && _Mode == enMode.Update)
                {
                    File.Delete(_Person.ImagePath);
                    _Person.ImagePath = null;
                }
                return;
            }

            if (_Person.ImagePath == pbImage.ImageLocation)
                return;

            //if it continued to here that means the user updated or deleted the image in update mode

            //it will go inside this function if the user changed his picture to another.
            if (!string.IsNullOrEmpty(_Person.ImagePath))
            {
                File.Delete(_Person.ImagePath);
                _Person.ImagePath = null;
            }

            string NewName = _MakeGuidName();

            string newImagePath = ImagesFolder + NewName;

            File.Copy(pbImage.ImageLocation, newImagePath, true);

            pbImage.ImageLocation = newImagePath;

        }

        private void _SetFormAttributes()
        {
            _SetDateTimeForPicker();
            _FillCountriesInComboBox();
            cbCountries.Text = "Jordan";
            openFileDialog1.Filter = "Images | *.png;*.jpg;*.jpeg;*.gif";
            openFileDialog1.InitialDirectory = @"C:\Users\eidgh\Downloads\";
            LlblRemoveImage.Visible = (pbImage.ImageLocation != null);
        }

        // end my private methods

        private void _LoadData()
        {
            lblFormStatus.Text = "Update Person ID = " + _ID;
            lblPersonID.Text = _ID.ToString();

            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            if (!string.IsNullOrEmpty(_Person.ThirdName))
                txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;
            txtNationalNo.Text = _Person.NationalNo;
            dtpDateOfBirth.Value = _Person.DateOfBirth;
            if (_Person.Gendor == (int)enGender.Male)
            {
                rbMale.Checked = true;
            }
            else
            {
                rbFemale.Checked = true;
            }
            txtPhone.Text = _Person.Phone;
            if(!string.IsNullOrEmpty(_Person.Email))
                txtEmail.Text = _Person.Email;
            cbCountries.Text = _Person.CountryInfo.CountryName;
            txtAddress.Text = _Person.Address;
            if (!string.IsNullOrEmpty(_Person.ImagePath))
                pbImage.ImageLocation = _Person.ImagePath;

        }

        //end Update mode methods
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Not all fields filled correctly!", "Filling Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _Person.NationalNo = txtNationalNo.Text;
            _Person.FirstName = txtFirstName.Text;
            _Person.SecondName = txtSecondName.Text;
            _Person.ThirdName = txtThirdName.Text;
            _Person.LastName = txtLastName.Text;
            _Person.DateOfBirth = dtpDateOfBirth.Value;
            if (rbMale.Checked)
                _Person.Gendor = (int)enGender.Male;
            else
                _Person.Gendor = (int)enGender.Female;
            _Person.Phone = txtPhone.Text;
            _Person.Email = txtEmail.Text;
            _Person.NationalityCountryID = cbCountries.SelectedIndex + 1;
            _Person.Address = txtAddress.Text;


            _HandleImage();

            _Person.ImagePath = pbImage.ImageLocation;

            if (_Person.Save())
            {
                MessageBox.Show("Person Saved Successfully", "Save succeed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error while saving data", "Save error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _ID = _Person.ID;
            lblFormStatus.Text = "Update Person ID = " + _ID.ToString();
            lblPersonID.Text = _ID.ToString();
            PersonSelected(_ID);

            //MessageBox.Show("Data saved successfully");
        }
        
        private void frmAddEditPerson_Load(object sender, EventArgs e)
        {
            if(_Mode == enMode.Update)
            {
                _LoadData();
            }
            _SetFormAttributes();
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if(pbImage.ImageLocation == null)
            {
                pbImage.Image = Properties.Resources.Male_512;
            }
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if(pbImage.ImageLocation == null)
            {
                pbImage.Image = Properties.Resources.Female_512;
            }
        }

        private void LlblSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pbImage.ImageLocation = openFileDialog1.FileName;
                LlblRemoveImage.Visible = true;
            }
        }

        private void LlblRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbImage.ImageLocation = null;
            if (rbMale.Checked)
            {
                pbImage.Image = Properties.Resources.Male_512;
            }
            else
                pbImage.Image = Properties.Resources.Female_512;
            LlblRemoveImage.Visible = false;
        }
    }
}
