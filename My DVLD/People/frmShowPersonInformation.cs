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
    public partial class frmShowPersonInformation : Form
    {
        private int _PersonID;

        public frmShowPersonInformation(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowPersonInformation_Load(object sender, EventArgs e)
        {
            ctrlPersonInformation1.FillData(_PersonID);
        }
    }
}
