using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using System.Data;
using System.Windows.Forms;

namespace Business
{
	public class clsLicense
	{
		private enum enMode { AddNew,Update};
		private enMode _Mode = enMode.AddNew;

		public int LicenseID {  get; set; }
		public int ApplicationID {  get; set; }
		public int DriverID { get; set; }
		public int LicenseClass {  get; set; }
		public DateTime IssueDate { get; set; }
		public DateTime ExpirationDate { get; set; }
		public string Notes {  get; set; }
		public float PaidFees {  get; set; }
		public bool IsActive {  get; set; }
		public short IssueReason {  get; set; }
		public int CreatedByUserID { get; set; }

		public int IssuedFromLocalApplicationID { get; set; }
		public clsLocalDrivingLicenseApplication IssuedFromLocalApplication
		{
			get
			{
				return clsLocalDrivingLicenseApplication.Find(IssuedFromLocalApplicationID);
			}
		}

		public clsApplication Application
		{
			get
			{
				return clsApplication.Find(ApplicationID);
			}
			set
			{

			}
		}
		public clsDriver Driver
		{
			get
			{
				return clsDriver.Find(DriverID);
			}
			set
			{

			}
		}
		public clsLicenseClass License_Class
		{
			get
			{
				return clsLicenseClass.Find(LicenseClass);
			}
			set
			{

			}
		}
		public clsUser CreatedByUser
		{
			get
			{
				return clsUser.Find(CreatedByUserID);
			}
			set
			{

			}
		}

		public clsLicense()
		{
			_Mode = enMode.AddNew;
			LicenseID = -1;
			ApplicationID = -1;
			DriverID = -1;
			LicenseClass = -1;
			IssueDate = DateTime.MinValue;
			ExpirationDate = DateTime.MinValue;
			Notes = string.Empty;
			PaidFees = -1f;
			IsActive = false;
			IssueReason = -1;
			CreatedByUserID = -1;
		}

		private clsLicense(int licenseID, int applicationID, int driverID, int licenseClass,
			DateTime issueDate, DateTime expirationDate, string notes, float paidFees, bool isActive,
			short issueReason, int createdByUserID,int IssuedFromLocalApplicationID)
		{
			_Mode = enMode.Update;
			LicenseID = licenseID;
			ApplicationID = applicationID;
			DriverID = driverID;
			LicenseClass = licenseClass;
			IssueDate = issueDate;
			ExpirationDate = expirationDate;
			Notes = notes;
			PaidFees = paidFees;
			IsActive = isActive;
			IssueReason = issueReason;
			CreatedByUserID = createdByUserID;
			this.IssuedFromLocalApplicationID = IssuedFromLocalApplicationID;
		}

		public static DataTable GetAllLicenses()
		{
			return DAL_Licenses.GetAllLicenses();
		}
	
		public static DataTable LicensesView_PersonID(int PersonID)
		{
			return DAL_Licenses.LicensesView_PersonID(PersonID);
		}

		public static clsLicense Find(int LicenseID)
		{
			int ApplicationID = -1, DriverID = -1, LicenseClass = -1, CreatedByUserID = -1, IssuedFromLocalApplicationID = -1;
			DateTime IssueDate = default, ExpirationDate = default;
			string Notes = string.Empty;
			float PaidFees = -1;
			bool IsActive = false;
			short IssueReason = -1;

			bool isFound = DAL_Licenses.Find(LicenseID, ref ApplicationID, ref DriverID, ref LicenseClass, ref IssueDate,
				ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID,ref IssuedFromLocalApplicationID,true);
			if (isFound)
			{
				return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass, IssueDate,
				ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID,IssuedFromLocalApplicationID);
			}
			else
				return null;
		}

		public static clsLicense Find_ByLocalApplication(int LocalApplicationID)
		{
            int LicenseID = -1,ApplicationID = -1, DriverID = -1, LicenseClass = -1, CreatedByUserID = -1;
            DateTime IssueDate = default, ExpirationDate = default;
            string Notes = string.Empty;
            float PaidFees = -1;
            bool IsActive = false;
            short IssueReason = -1;

            bool isFound = DAL_Licenses.Find(LocalApplicationID,ref LicenseID, ref ApplicationID, ref DriverID, ref LicenseClass, ref IssueDate,
                ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID);
            if (isFound)
            {
                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass, IssueDate,
                ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID, LocalApplicationID);
            }
            else
                return null;
        }

		private bool _AddNewLicense()
		{
			this.LicenseID = DAL_Licenses.AddNewLicense(this.ApplicationID, this.DriverID, this.LicenseClass, this.IssueDate, this.ExpirationDate,
				this.Notes, this.PaidFees, this.IsActive, this.IssueReason, this.CreatedByUserID);
			return (this.LicenseID != -1);
		}

		private bool _UpdateLicense()
		{
			return DAL_Licenses.UpdateLicense(this.LicenseID,this.ApplicationID,this.DriverID,this.LicenseClass,
				this.IssueDate,this.ExpirationDate,this.Notes,this.PaidFees
				,this.IsActive, this.IssueReason,this.CreatedByUserID);
		}

		public bool Save()
		{
			switch(_Mode)
			{
				case enMode.AddNew:
					if (_AddNewLicense())
					{
						_Mode = enMode.Update;
						return true;
					}
					else
						return false;
				case enMode.Update:
					return _UpdateLicense();
			}
			return false;
		}

		public static bool DeleteLicense(int LicenseID)
		{
			return DAL_Licenses.DeleteLicense(LicenseID);
		}

		public static bool DoesLicenseExist(int LicenseID)
		{
			return DAL_Licenses.DoesLicenseExist(LicenseID);
		}

		public static bool DoesLicenseExist(string NationalNo,int LicenseClass,bool IsActive)
		{
			return DAL_Licenses.DoesLicenseExist(NationalNo,LicenseClass,IsActive);
		}


    }
}
