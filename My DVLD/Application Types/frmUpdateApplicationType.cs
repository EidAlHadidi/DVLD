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
    public partial class frmUpdateApplicationType : Form
    {
        private int _ApplicationTypeID;
        private clsApplicationType _ApplicationType;

        public frmUpdateApplicationType(int ApplicationTypeID)
        {
            InitializeComponent();
            _ApplicationTypeID = ApplicationTypeID;
        }

        private void KeyPressValidate(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void frmUpdateApplicationType_Load(object sender, EventArgs e)
        {
            _ApplicationType = clsApplicationType.Find(_ApplicationTypeID);
            lblApplicationTypeID.Text = _ApplicationTypeID.ToString();
            txtTitle.Text = _ApplicationType.ApplicationTypeTitle;
            txtFees.Text = _ApplicationType.ApplicationFees.ToString();
        }

        private void ValidateEmptyString(object sender,CancelEventArgs e)
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Not all fields filled correctly", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _ApplicationType.ApplicationTypeTitle = txtTitle.Text;
            _ApplicationType.ApplicationFees = Convert.ToSingle(txtFees.Text);
            if (_ApplicationType.Save())
            {
                MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Data not saved", "Not Saved", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
