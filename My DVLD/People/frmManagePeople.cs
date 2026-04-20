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
    public partial class frmManagePeople : Form
    {

        static DataTable dtAllPeople = clsPerson.GetAllPeople();

        static DataTable dtPeople = dtAllPeople.DefaultView.ToTable(false,"PersonID"
            , "NationalNo", "FirstName", "SecondName", "ThirdName", "LastName", "DateOfBirth", "Gender",
            "CountryName", "Phone", "Email");

        static BindingSource BS = new BindingSource();

        private void _RefreshNumberOfRecords()
        {
            lblNumberOfRecords.Text = dgvPeople.Rows.Count.ToString();
        }
        private void _RefreshRecords()
        {
            dtAllPeople = clsPerson.GetAllPeople();
            dtPeople = dtAllPeople.DefaultView.ToTable(false, "PersonID"
            , "NationalNo", "FirstName", "SecondName", "ThirdName", "LastName", "DateOfBirth", "Gender",
            "CountryName", "Phone", "Email");
            BS.DataSource = dtPeople;
            dgvPeople.DataSource = BS;
            _RefreshNumberOfRecords();
        }

        public frmManagePeople()
        {
            InitializeComponent();
        }

        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            cbFilterPeopleBy.SelectedIndex = 0;
            _RefreshRecords();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson();
            frm.ShowDialog();
            _RefreshRecords();
        }
        
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int SelectedPersonID = (int)dgvPeople.CurrentRow.Cells[0].Value;
            if(MessageBox.Show("Are you sure you want to delete person [" + SelectedPersonID + "] ?",
                "Confirm Deletion",MessageBoxButtons.OKCancel,MessageBoxIcon.Exclamation) == DialogResult.OK)
            {
                if(clsPerson.DeletePerson(SelectedPersonID))
                {
                    MessageBox.Show($"Person [{SelectedPersonID}] deleted successfully",
                        "Delete Succeed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"This person [{SelectedPersonID}] has data linked to it",
                        "Delete failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            _RefreshRecords();
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Will developed soon", "Not Ready", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Will developed soon", "Not Ready", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson();
            frm.ShowDialog();
            _RefreshRecords();
        }

        private void editToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson((int)dgvPeople.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshRecords();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int SelectedPerson = (int)dgvPeople.CurrentRow.Cells[0].Value;
            frmShowPersonInformation frm = new frmShowPersonInformation(SelectedPerson);
            frm.ShowDialog();
            _RefreshRecords();
        }

        private void cbFilterPeopleBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterBy.Clear();
            if (cbFilterPeopleBy.Text != "None")
            {
                txtFilterBy.Visible = true;
            }
            else
                txtFilterBy.Visible = false;
        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilterPeopleBy.Text == "Person ID")
            {
                e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
            }
        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            if (txtFilterBy.Text == string.Empty)
            {
                BS.RemoveFilter();
                _RefreshNumberOfRecords();
                return;
            }
            BS.RemoveFilter();
            switch(cbFilterPeopleBy.Text)
            {
                case "Person ID":
                    BS.Filter = $"PersonID = {txtFilterBy.Text}";
                    break;
                case "National No.":
                    BS.Filter = $"NationalNo like '{txtFilterBy.Text}%'";
                    break;
                case "First Name":
                    BS.Filter = $"FirstName like '{txtFilterBy.Text}%'";
                    break;
                case "Second Name":
                    BS.Filter = $"SecondName like '{txtFilterBy.Text}%'";
                    break;
                case "Third Name":
                    BS.Filter = $"ThirdName like '{txtFilterBy.Text}%'";
                    break;
                case "Last Name":
                    BS.Filter = $"LastName like '{txtFilterBy.Text}%'";
                    break;
                case "Gender":
                    BS.Filter = $"Gender like '{txtFilterBy.Text}%'";
                    break;
                case "Nationality":
                    BS.Filter = $"CountryName like '{txtFilterBy.Text}%'";
                    break;
                case "Phone":
                    BS.Filter = $"Phone like '{txtFilterBy.Text}%'";
                    break;
                case "Email":
                    BS.Filter = $"Email like '{txtFilterBy.Text}%'";
                    break;
            }
            _RefreshNumberOfRecords();
        }
    }
}
