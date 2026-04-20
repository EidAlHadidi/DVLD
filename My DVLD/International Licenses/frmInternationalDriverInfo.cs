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
    public partial class frmInternationalDriverInfo : Form
    {
        private int _ID { get; set; }

        public frmInternationalDriverInfo(int ID)
        {
            InitializeComponent();
            _ID = ID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmInternationalDriverInfo_Load(object sender, EventArgs e)
        {
            ctrlDriverInternationalLicenseInfo1.FillData(_ID);
        }
    }
}
