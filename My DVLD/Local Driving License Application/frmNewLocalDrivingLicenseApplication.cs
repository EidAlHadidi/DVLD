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
	public partial class frmNewLocalDrivingLicenseApplication : Form
	{
		//because we are in a local driving license application
		clsLocalDrivingLicenseApplication _LocalApplication = new clsLocalDrivingLicenseApplication();
		clsApplication _Application = new clsApplication();
		clsApplicationType _applicationType = clsApplicationType.Find(1);
		clsUser _User = clsUser.Find(clsGlobalSettings.LoggedInUserID);
		int _PersonID = -1;


		public enum enApplicationStatus
		{
			New = 1, Cancelled = 2, Completed = 3
		}

		public frmNewLocalDrivingLicenseApplication()
		{
			InitializeComponent();
		}

		private void _GetLicenseClassesInComboBox()
		{
			DataTable dt = clsLicenseClass.GetAllLicenseClasses();
			foreach (DataRow R in dt.Rows)
			{
				cbLicenseClasses.Items.Add(R["ClassName"].ToString());
			}
		}

		private bool CheckIfRepeated()
		{
			_LocalApplication = clsLocalDrivingLicenseApplication.Find(_PersonID, 1, cbLicenseClasses.SelectedIndex + 1);
			return (_LocalApplication != null);
		}

		private bool CheckIfAlreadyHaveLicense()
		{
			_LocalApplication = clsLocalDrivingLicenseApplication.Find(_PersonID, 3, (cbLicenseClasses.SelectedIndex + 1));
			return _LocalApplication != null;
		}

		//
		private void ctrlPersonInformationWithFilter1_OnPersonSelected(object sender, int PersonID)
		{
			if (clsPerson.isPersonExist(PersonID))
			{
				btnNext.Enabled = true;
				btnSave.Enabled = btnNext.Enabled;
				_PersonID = PersonID;
			}
			else
			{
				btnNext.Enabled = false;
				btnSave.Enabled = btnNext.Enabled;
				_PersonID = -1;
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (CheckIfAlreadyHaveLicense())
			{
				MessageBox.Show("This person already has a license with this license class", "Already has license",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				_LocalApplication = new clsLocalDrivingLicenseApplication();
				return;
			}
			if (CheckIfRepeated())
			{
				MessageBox.Show("Choose another license class," +
					"the selected person already has an active application for the " +
					"selected class with id = " + _LocalApplication.ApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				_LocalApplication = new clsLocalDrivingLicenseApplication();
				return;
			}
			_LocalApplication = new clsLocalDrivingLicenseApplication();

			_Application.ApplicationDate = DateTime.Now;
			_Application.ApplicantPersonID = _PersonID;
			_Application.ApplicationTypeID = _applicationType.ApplicationTypeID;
			_Application.ApplicationStatus = (int)enApplicationStatus.New;
			_Application.LastStatusDate = DateTime.Now;
			_Application.PaidFees = _applicationType.ApplicationFees;
			_Application.CreatedByUserID = _User.UserID;

			_LocalApplication.LicenseClassID = cbLicenseClasses.SelectedIndex + 1;

			if (!_Application.Save())
			{
				MessageBox.Show("Error while saving the application record.",
					"Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			_LocalApplication.ApplicationID = _Application.ApplicationID;
			if (_LocalApplication.Save())
			{
				MessageBox.Show("Application saved successfully");
				lblApplicationID.Text = _LocalApplication.LocalDrivingLicenseApplicationID.ToString();
				lblFormStatus.Text = "Update Local Driving License Application";
				btnSave.Enabled = false;
			}
			else
			{
				MessageBox.Show("Error while saving local driving license application!", "Saving error",
				MessageBoxButtons.OK, MessageBoxIcon.Error);
				clsApplication.DeleteApplication(_Application.ApplicationID);
				return;
			}
		}

		private void frmNewLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
		{
			lblApplicationDate.Text = DateTime.Now.ToShortDateString();
			_GetLicenseClassesInComboBox();
			lblCreatedBy.Text = _User.UserName.ToString();
			lblApplicationFees.Text = _applicationType.ApplicationFees.ToString();
			cbLicenseClasses.SelectedIndex = 2;
			btnSave.Enabled = btnNext.Enabled;
		}

		private void btnNext_Click(object sender, EventArgs e)
		{
			tabControl1.SelectedTab = tpApplicationInfo;
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
