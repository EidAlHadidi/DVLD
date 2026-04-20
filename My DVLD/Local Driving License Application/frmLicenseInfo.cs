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
    public partial class frmLicenseInfo : Form
    {
        private int _ID { get; set; }
        
        private bool _FillByLocal { get; set; }

        public frmLicenseInfo(int LocalDrivingLicenseApplicationID)
        {
            _ID = LocalDrivingLicenseApplicationID;
            _FillByLocal = true;
            InitializeComponent();
        }

        public frmLicenseInfo(int LicenseID,bool FillByLicenseID)
        {
            _ID = LicenseID;
            _FillByLocal = false;
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLicenseInfo_Load(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfo1.FillData(_ID,_FillByLocal);
        }
    }
}
