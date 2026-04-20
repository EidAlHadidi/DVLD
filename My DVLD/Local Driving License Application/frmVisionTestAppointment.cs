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
    public partial class frmVisionTestAppointment : Form
    {
        public frmVisionTestAppointment(int LDL_AppID)
        {
            _LDL_AppID = LDL_AppID;
            InitializeComponent();
        }

        private int _LDL_AppID {  get; set; }

        static DataTable _dtAllTestAppointments = clsTestAppointment.GetAllTestAppointments();
        static DataTable _dtTestAppointments = new DataTable();
        BindingSource BS = new BindingSource();


        private void _RefreshNumberOfRecords()
        {
            lblNumberOfRecords.Text = dgvAppointments.Rows.Count.ToString();
        }

        private bool _IsDgvEmpty(DataGridView dgv)
        {
            if (dgvAppointments.Rows.Count == 0)
            {
                dgvAppointments.Columns.Clear();
                return true;
            }
            else
                return false;
        }

        private void _LoadDataIntoDGV()
        {
            _dtAllTestAppointments = clsTestAppointment.GetAllTestAppointments();
            BS.DataSource = _dtAllTestAppointments;
            BS.Filter = $"LocalDrivingLicenseApplicationID = {_LDL_AppID} and TestTypeID = 1";
            _dtTestAppointments = (DataTable)BS.DataSource;
            _dtTestAppointments = _dtTestAppointments.DefaultView.ToTable(false, "TestAppointmentID", "AppointmentDate",
                "PaidFees", "IsLocked");
            dgvAppointments.DataSource = _dtTestAppointments;
            
        }

        private bool _DoesApplicantHasActiveAppointment()
        {
            foreach(DataGridViewRow Row in dgvAppointments.Rows)
            {
                if (!Convert.ToBoolean(Row.Cells[3].Value))
                {
                    //IsLocked = false ==> it is an active appointment
                    return true;
                }
            }
            return false;
        }

        private void _RefreshRecords()
        {
            _LoadDataIntoDGV();
            if (!_IsDgvEmpty(dgvAppointments) && _DoesApplicantHasActiveAppointment())
            {
                btnNewAppointment.Enabled = false;
            }
            else
                btnNewAppointment.Enabled = true;
            //_RefreshDataGridViewColumnNames();
            _RefreshNumberOfRecords();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmVisionTestAppointment_Load(object sender, EventArgs e)
        {
            ctrlApplicationInfo1.FillData(_LDL_AppID);
            _RefreshRecords();
        }

        private void btnNewAppointment_Click(object sender, EventArgs e)
        {
            if(clsLocalDrivingLicenseApplication.Find(_LDL_AppID).PassedTestsCount > 0)
            {
                MessageBox.Show("This person already passed this test before, you can only retake failed test",
                    "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var frm = new frmScheduleTest(_LDL_AppID);
            frm.Type = "Vision";
            frm.ShowDialog();
            _RefreshRecords();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmScheduleTest(_LDL_AppID,(int)dgvAppointments.CurrentRow.Cells[0].Value);
            if (Convert.ToBoolean(dgvAppointments.CurrentRow.Cells[3].Value))
                frm.IsLocked = true;
            frm.Type = "Vision";
            frm.ShowDialog();
            _RefreshRecords();
            ctrlApplicationInfo1.Refresh();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(dgvAppointments.CurrentRow.Cells[3].Value))
            {
                MessageBox.Show("You cant take test for this appointment because it is locked!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var frm = new frmTakeTest((int)dgvAppointments.CurrentRow.Cells[0].Value);
            frm.Type = "Vision";
            frm.ShowDialog();
            _RefreshRecords();
            ctrlApplicationInfo1.Refresh();
        }
    }
}
