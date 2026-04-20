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
    public partial class frmUpdateTestType : Form
    {
        private int _TestTypeID;
        private clsTestType _TestType;
        public frmUpdateTestType(int TestTypeID)
        {
            InitializeComponent();
            _TestTypeID = TestTypeID;
        }

        private void ValidateEmptyString(object sender, CancelEventArgs e)
        {
            TextBox temp = (TextBox)sender;
            if (string.IsNullOrEmpty(temp.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(temp, "This  field cannot be blank");
            }
            else
                errorProvider1.SetError(temp, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUpdateTestType_Load(object sender, EventArgs e)
        {
            _TestType = clsTestType.Find(_TestTypeID);
            lblTestTypeID.Text = _TestTypeID.ToString();
            txtTitle.Text = _TestType.TestTypeTitle;
            txtDescription.Text = _TestType.TestTypeDescription;
            txtFees.Text = _TestType.TestTypeFees.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Not all fields filled correctly!", "Filling Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _TestType.TestTypeTitle = txtTitle.Text;
            _TestType.TestTypeDescription = txtDescription.Text;
            _TestType.TestTypeFees = Convert.ToSingle(txtFees.Text);
            if (_TestType.Save())
            {
                MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Data Not Saved", "Not Saved", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void KeyPressValidate(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
