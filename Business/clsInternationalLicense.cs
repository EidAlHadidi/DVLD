using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Data;

namespace Business
{
	public class clsInternationalLicense
	{
		private enum enMode { AddNew, Update };
		private enMode _Mode = enMode.AddNew;

		public int InternationalLicenseID { get; set; }
		public int ApplicationID { get; set; }
		public int DriverID { get; set; }
		public int IssuedUsingLocalLicenseID { get; set; }
		public DateTime IssueDate { get; set; }
		public DateTime ExpirationDate { get; set; }
		public bool IsActive { get; set; }
		public int CreatedByUserID { get; set; }

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

		public clsLocalDrivingLicenseApplication IssuedUsingLocalLicense
		{
			get
			{
				return clsLocalDrivingLicenseApplication.Find(IssuedUsingLocalLicenseID);
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

		public clsInternationalLicense()
		{
			_Mode = enMode.AddNew;
			InternationalLicenseID = -1;
			ApplicationID = -1;
			DriverID = -1;
			IssuedUsingLocalLicenseID = -1;
			IssueDate = DateTime.MinValue;
			ExpirationDate = DateTime.MinValue;
			IsActive = false;
			CreatedByUserID = -1;
		}

		private clsInternationalLicense(int internationalLicenseID, int applicationID, int driverID, int issuedUsingLocalLicenseID,
			DateTime issueDate, DateTime expirationDate, bool isActive, int createdByUserID)
		{
			_Mode = enMode.Update;
			InternationalLicenseID = internationalLicenseID;
			ApplicationID = applicationID;
			DriverID = driverID;
			IssuedUsingLocalLicenseID = issuedUsingLocalLicenseID;
			IssueDate = issueDate;
			ExpirationDate = expirationDate;
			IsActive = isActive;
			CreatedByUserID = createdByUserID;
		}

		public static DataTable GetAllInternationalLicenses()
		{
			return DAL_InternationalLicenses.GetAllInternationalLicenses();
		}

		public static DataTable GetAllInternationalLicenses_ByPersonID(int PersonID)
		{
			return DAL_InternationalLicenses.GetAllInternationalLicenses_ByPersonID(PersonID);
		}

        public static clsInternationalLicense Find(int InternationalLicenseID)
		{
			int ApplicationID = -1, DriverID = -1, IssuedUsingLocalLicenseID = -1, CreatedByUserID = -1;
			bool isActive = false;
			DateTime IssueDate = DateTime.Now;
			DateTime ExpirationDate = DateTime.Now;

			bool isFound = DAL_InternationalLicenses.Find(InternationalLicenseID, ref ApplicationID, ref DriverID,
				ref IssuedUsingLocalLicenseID, ref IssueDate, ref ExpirationDate, ref isActive, ref CreatedByUserID);

			if (isFound)
			{
				return new clsInternationalLicense(InternationalLicenseID, ApplicationID, DriverID,
					IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, isActive, CreatedByUserID);
			}
			else
				return null;
		}

		private bool _AddNewInternationalLicense()
		{
			this.InternationalLicenseID = DAL_InternationalLicenses.AddNewInternationalLicense(this.ApplicationID,
				this.DriverID, this.IssuedUsingLocalLicenseID, this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);
			return (this.InternationalLicenseID != -1);
		}

		private bool _UpdateInternationalLicense()
		{
			return DAL_InternationalLicenses.UpdateInternaltionalLicense(this.InternationalLicenseID, this.ApplicationID,
				this.DriverID, this.IssuedUsingLocalLicenseID, this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);
		}

		public bool Save()
		{
			switch (_Mode)
			{
				case enMode.AddNew:
					if (_AddNewInternationalLicense())
					{
						_Mode = enMode.Update;
						return true;
					}
					else
						return false;
				case enMode.Update:
					return _UpdateInternationalLicense();
			}
			return false;
		}

		public static bool DeleteInternationalLicense(int InternationalLicenseID)
		{
			return DAL_InternationalLicenses.DeleteInternationalLicense(InternationalLicenseID);
		}

		public static bool DoesInternationalLicenseExist(int InternationalLicenseID)
		{
			return DAL_InternationalLicenses.DoesInternationalLicenseExist(InternationalLicenseID);
		}

		public static bool DoesInternationalLicenseExist(int LicenseID,bool IsActive)
		{
			return DAL_InternationalLicenses.DoesInternationalLicenseExist_ByLienseID(LicenseID, IsActive);
		}


	}
}
