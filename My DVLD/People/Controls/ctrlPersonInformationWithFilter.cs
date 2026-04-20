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
    public partial class ctrlPersonInformationWithFilter : UserControl
    {
        private int _PersonID = -1;

        private clsPerson _Person = null;

        public delegate void PersonSelectDataBack(object sender, int PersonID);

        public event PersonSelectDataBack OnPersonSelected;

        protected virtual void PersonSelected(int PersonID)
        {
            PersonSelectDataBack handler = OnPersonSelected;
            if(handler != null)
            {
                handler?.Invoke(this, PersonID);
            }
        }

        public ctrlPersonInformationWithFilter()
        {
            InitializeComponent();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilterBy.Text == "Person ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }
        
        private void SearchForPerson<T>(T Parameter)
        {
            if(Parameter is int)
                _Person = clsPerson.Find((int)(object)Parameter);
            else
                _Person = clsPerson.Find((string)(object)Parameter);
            
            if (_Person == null)
            {
                MessageBox.Show("Person not found", "Cannot find person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonInformation1.ResetToDefaults();
                _PersonID = -1;
                _Person = null;
                if(OnPersonSelected != null)
                {
                    PersonSelected(-1);
                }
                return;
            }

            _PersonID = _Person.ID;

            ctrlPersonInformation1.FillData(_PersonID);
            if(OnPersonSelected != null)
                PersonSelected(_PersonID);

        }

        private void btnSearchPerson_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtSearch.Text.Trim()))
            {
                MessageBox.Show("The required field is empty!",
                    "Field not filled", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonInformation1.ResetToDefaults();
                return;
            }

            if(cbFilterBy.Text == "National No.")
            {
                string Search = txtSearch.Text;
                SearchForPerson<string>(txtSearch.Text);
            }
            else
            {
                _PersonID = int.Parse(txtSearch.Text);
                SearchForPerson<int>(int.Parse(txtSearch.Text));
            }
        }

        private void HandlePersonSelection(object sender, int PersonID)
        {
            ctrlPersonInformation1.FillData(PersonID);
            if(OnPersonSelected != null)
                PersonSelected(PersonID);
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson();
            frm.onPersonSelected += HandlePersonSelection;
            frm.ShowDialog();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearch.Clear();
        }

        private void ctrlPersonInformationWithFilter_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
        }

        public void MakeFilterDisabled()
        {
            gbFilter.Enabled = false;
        }

        public void MakeFilterEnabled()
        {
            gbFilter.Enabled = true;
        }

        public void FillData(int PersonID)
        {
            ctrlPersonInformation1.FillData(PersonID);
        }


        public void FillWithFilter(int PersonID)
        {
            cbFilterBy.SelectedIndex = 1;
            txtSearch.Text = PersonID.ToString();
            ctrlPersonInformation1.FillData(PersonID);
            MakeFilterDisabled();
        }

    }
}
