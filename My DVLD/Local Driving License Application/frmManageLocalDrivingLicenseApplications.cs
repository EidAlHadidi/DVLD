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
	public partial class frmManageLocalDrivingLicenseApplications : Form
	{
		BindingSource BS = new BindingSource();

		static DataTable dtAllLocalApps = clsLocalDrivingLicenseApplication.GetLocalDrivingApplications_View();
		static DataTable dtLocalApps = dtAllLocalApps.DefaultView.ToTable(true, "LocalDrivingLicenseApplicationID",
			"ClassName", "NationalNo", "FullName", "ApplicationDate", "PassedTestCount", "Status");

		private void _ModifyDataGridViewColumnNames()
		{
			dgvLocalApplications.Columns["LocalDrivingLicenseApplicationID"].HeaderText = "L.D.L.AppID";
			dgvLocalApplications.Columns["ClassName"].HeaderText = "Driving Class";
			dgvLocalApplications.Columns["NationalNo"].HeaderText = "National No.";
			dgvLocalApplications.Columns["FullName"].HeaderText = "Full Name";
			dgvLocalApplications.Columns["ApplicationDate"].HeaderText = "Application Date";
			dgvLocalApplications.Columns["PassedTestCount"].HeaderText = "Passed Tests";
			dgvLocalApplications.Columns["Status"].HeaderText = "Status";
		}

		private void _RefreshNumberOfRecords()
		{
			lblNumberOfRecords.Text = dgvLocalApplications.Rows.Count.ToString();
		}

		private void _RefreshRecords()
		{
			dtAllLocalApps = clsLocalDrivingLicenseApplication.GetLocalDrivingApplications_View();
			dtLocalApps = dtAllLocalApps.DefaultView.ToTable(true, "LocalDrivingLicenseApplicationID",
			"ClassName", "NationalNo", "FullName", "ApplicationDate", "PassedTestCount", "Status");
			BS.DataSource = dtLocalApps;
			dgvLocalApplications.DataSource = BS;
			_ModifyDataGridViewColumnNames();
			_RefreshNumberOfRecords();
		}

		public frmManageLocalDrivingLicenseApplications()
		{
			InitializeComponent();
		}

		private void btnAddNewLocalApplication_Click(object sender, EventArgs e)
		{
			frmNewLocalDrivingLicenseApplication frm = new frmNewLocalDrivingLicenseApplication();
			frm.ShowDialog();
			_RefreshRecords();
		}

		private void frmManageLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
		{
			cbFilterBy.SelectedIndex = 0;
			_RefreshRecords();
		}

		private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (cbFilterBy.Text == "L.D.L.AppID")
				e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
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
			switch (cbFilterBy.Text)
			{
				case "L.D.L.AppID":
					BS.Filter = $"LocalDrivingLicenseApplicationID = {txtFilterBy.Text.Trim()}";
					break;
				case "National No.":
					BS.Filter = $"NationalNo like '{txtFilterBy.Text.Trim()}%'";
					break;
				case "Full Name":
					BS.Filter = $"FullName like '{txtFilterBy.Text.Trim()}%'";
					break;
				case "Status":
					BS.Filter = $"Status like '{txtFilterBy.Text.Trim()}%'";
					break;
			}
			_RefreshNumberOfRecords();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void cbFilterBy_SelectedIndexChanged_1(object sender, EventArgs e)
		{
			txtFilterBy.Enabled = (cbFilterBy.Text != "None");
			txtFilterBy.Clear();
		}

		private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			clsLocalDrivingLicenseApplication App = clsLocalDrivingLicenseApplication.
					Find((int)dgvLocalApplications.CurrentRow.Cells[0].Value);
			if (App.Application.ApplicationStatus == 3)
			{
				MessageBox.Show("You cant cancel a completed application", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (App.Application.ApplicationStatus == 2)
			{
				MessageBox.Show("This application is already cancelled", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (MessageBox.Show("Are you sure you want to cancel this application?", "Confirm",
				MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{

				App.Application.ApplicationStatus = 2;
				if (App.Save())
				{
					MessageBox.Show("Application cancelled successfully", "Cancelled",
						MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else
				{
					MessageBox.Show("Error while cancelling the application", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				_RefreshRecords();
			}
		}

		private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if(MessageBox.Show("Are you sure you want to delete this application ? ","Confirm Delete",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
			{
				if (clsLocalDrivingLicenseApplication.DeleteLDL_Application(
					(int)dgvLocalApplications.CurrentRow.Cells[0].Value,true))
				{
					MessageBox.Show("Application deleted successfully", "Delete Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else
					MessageBox.Show("Error while deleting record!", "Deletion Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			_RefreshRecords();
		}

        private void showAToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void scheduleToolStripMenuItem_Click(object sender, EventArgs e)
        {
			var frm = new frmVisionTestAppointment((int)dgvLocalApplications.CurrentRow.Cells[0].Value);
			frm.ShowDialog();
			_RefreshRecords();
        }

		//Handling context menu strip available options based on selected record

		private void _HandleNewApplicationsContextMenuOptions(int PassedTests)
		{
            issueDrivingLicenseToolStripMenuItem.Enabled = false;
            showLicenseToolStripMenuItem.Enabled = false;
            switch (PassedTests)
			{
				case 0:
					scheduleWrittenTestToolStripMenuItem.Enabled = false;
					scheduleStreetTestToolStripMenuItem.Enabled = false;
                    break;
				case 1:
					scheduleToolStripMenuItem.Enabled = false;
					scheduleStreetTestToolStripMenuItem.Enabled=false;
					break;
				case 2:
					scheduleToolStripMenuItem.Enabled = false;
					scheduleWrittenTestToolStripMenuItem.Enabled = false;
					break;
				case 3:
					scheduleTestToolStripMenuItem.Enabled = false;
					issueDrivingLicenseToolStripMenuItem.Enabled = true;
					break;
            }
		}

		private void _HandleContextMenuStripOptions()
		{
			int PassedTests = Convert.ToInt32(dgvLocalApplications.CurrentRow.Cells[5].Value);
			string Status = dgvLocalApplications.CurrentRow.Cells[6].Value.ToString().Trim();
			switch (Status)
			{
				case "New":
					_HandleNewApplicationsContextMenuOptions(PassedTests);
					break;
				case "Cancelled":
					scheduleTestToolStripMenuItem.Enabled = false;
					issueDrivingLicenseToolStripMenuItem.Enabled=false;
					showLicenseToolStripMenuItem.Enabled=false;
					cancelApplicationToolStripMenuItem.Enabled=false;
					editApplicationToolStripMenuItem.Enabled=false;
					break;
				case "Completed":
					editApplicationToolStripMenuItem.Enabled=false;
					deleteApplicationToolStripMenuItem.Enabled=false;
					cancelApplicationToolStripMenuItem.Enabled = false;
					scheduleTestToolStripMenuItem.Enabled = false;
					issueDrivingLicenseToolStripMenuItem.Enabled = false;
                    break;
			}
		}

		private void _RefreshContextMenuStrip()
		{
			showAToolStripMenuItem.Enabled = true;
			editApplicationToolStripMenuItem.Enabled = true;
			deleteApplicationToolStripMenuItem.Enabled = true;
			cancelApplicationToolStripMenuItem.Enabled = true;
			scheduleTestToolStripMenuItem.Enabled = true;
			scheduleToolStripMenuItem.Enabled = true;
			scheduleWrittenTestToolStripMenuItem.Enabled = true;
			scheduleStreetTestToolStripMenuItem.Enabled = true;
			issueDrivingLicenseToolStripMenuItem.Enabled = true;
            showLicenseToolStripMenuItem.Enabled = true;
			showPersonLicenseToolStripMenuItem.Enabled = true;
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
			_RefreshContextMenuStrip();
			_HandleContextMenuStripOptions();
        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmWrittenTestAppointment((int)dgvLocalApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshRecords();
        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
			var frm = new frmStreetTestAppointments((int)dgvLocalApplications.CurrentRow.Cells[0].Value);
			frm.ShowDialog();
			_RefreshRecords();
        }

        private void issueDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
			var frm = new frmIssueDriverLicenseForTheFirstTime((int)dgvLocalApplications.CurrentRow.Cells[0].Value);
			frm.ShowDialog();
			_RefreshRecords();

        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
			var frm = new frmLicenseInfo((int)dgvLocalApplications.CurrentRow.Cells[0].Value);
			frm.ShowDialog();
        }

        private void showPersonLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
			clsLicense lic = clsLicense.Find_ByLocalApplication((int)dgvLocalApplications.CurrentRow.Cells[0].Value);
			var frm = new frmLicenseHistory(lic.Application.ApplicantPersonID);
			frm.ShowDialog();
        }
    }
}
