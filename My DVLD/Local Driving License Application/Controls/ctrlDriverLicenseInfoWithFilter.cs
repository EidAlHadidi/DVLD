using Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My_DVLD
{
    public partial class ctrlDriverLicenseInfoWithFilter : UserControl
    {

        public event Action<clsLicense> OnLicenseSelected;

        protected virtual void LicenseSelected(clsLicense license)
        {
            Action<clsLicense> handler = OnLicenseSelected;
            if(handler != null)
            {
                handler(license);
            }
        }

        public int LicenseID { get; set; }

        public clsLicense license
        {
            get
            {
                return clsLicense.Find(LicenseID);
            }
        }

        public ctrlDriverLicenseInfoWithFilter()
        {
            InitializeComponent();
        }

        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
        }

        private void ctrlDriverLicenseInfoWithFilter_Load(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfo1.ResetToDefaults();
            txtLicenseID.Clear();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtLicenseID.Text))
            {
                return;
            }
            LicenseID = int.Parse(txtLicenseID.Text.Trim());
            if (clsLicense.DoesLicenseExist(LicenseID))
            {
                ctrlDriverLicenseInfo1.FillData(license.LicenseID);
                LicenseSelected(license);
            }
            else
            {
                MessageBox.Show($"No driver with ID {LicenseID}");
                LicenseSelected(null);
                ctrlDriverLicenseInfo1.ResetToDefaults();
                txtLicenseID.Clear();
            }
        }

        public void MakeFilterDisabled()
        {
            gbFilter.Enabled = false;
        }

        public void MakeFilterEnbled()
        {
            gbFilter.Enabled = true;
        }

        public void FillData(int LicenseID)
        {
            EventArgs e = new EventArgs();
            txtLicenseID.Text = LicenseID.ToString();
            btnSearch_Click(this,e);
        }

    }
}
