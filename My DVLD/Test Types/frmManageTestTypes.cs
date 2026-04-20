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
    public partial class frmManageTestTypes : Form
    {
        public frmManageTestTypes()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _RefreshRecords()
        {
            dgvTestTypes.DataSource = clsTestType.GetAllTestTypes();
            lblNumberOfRecords.Text = dgvTestTypes.Rows.Count.ToString();
        }

        private void frmManageTestTypes_Load(object sender, EventArgs e)
        {
            _RefreshRecords();
        }

        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateTestType frm = new frmUpdateTestType((int)dgvTestTypes.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshRecords();
        }
    }
}
