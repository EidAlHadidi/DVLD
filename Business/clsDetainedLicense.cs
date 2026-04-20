using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Data;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace Business
{
	public class clsDetainedLicense
	{
		private enum enMode { AddNew,Update};
		private enMode _Mode = enMode.AddNew;

		public int DetainID { get; set; }
		public int LicenseID { get; set; }
		public DateTime DetainDate { get; set; }
		public float FineFees { get; set; }
		public int CreatedByUserID { get; set; }
		public bool IsReleased { get; set; }
		public DateTime ReleaseDate { get; set; }
		public int ReleasedByUserID { get; set; }
		public int ReleasedApplicationID { get; set; }

		public clsLicense License
		{
			get
			{
				return clsLicense.Find(LicenseID);
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

		public clsUser ReleasedByUser
		{
			get
			{
				if (ReleasedByUserID == CreatedByUserID)
				{
					return CreatedByUser;
				}
				else
					return clsUser.Find(ReleasedByUserID);
			}
			set
			{

			}
		}
	
		public clsApplication ReleasedApplication
		{
			get
			{
				return clsApplication.Find(ReleasedApplicationID);
			}
			set
			{

			}
		}
	
		public clsDetainedLicense()
		{
			_Mode = enMode.AddNew;
			DetainID = -1;
			LicenseID = -1;
			DetainDate = DateTime.Now;
			FineFees = -1f;
			CreatedByUserID = -1;
			IsReleased = false;
			ReleaseDate = DateTime.Now;
			ReleasedByUserID = -1;
			ReleasedApplicationID = -1;
		}

		private clsDetainedLicense(int detainID, int licenseID, DateTime detainDate, float fineFees,
			int createdByUserID, bool isReleased, DateTime releaseDate, int releasedByUserID, int releasedApplicationID)
		{
			_Mode = enMode.Update;
			DetainID = detainID;
			LicenseID = licenseID;
			DetainDate = detainDate;
			FineFees = fineFees;
			CreatedByUserID = createdByUserID;
			IsReleased = isReleased;
			ReleaseDate = releaseDate;
			ReleasedByUserID = releasedByUserID;
			ReleasedApplicationID = releasedApplicationID;
		}

		public static DataTable GetAllDetainedLicenses()
		{
			return DAL_DetainedLicenses.GetAllDetainedLicenses();
		}

		public static DataTable GetAllDetainedLicenses_View()
		{
			return DAL_DetainedLicenses.GetAllDetainedLicenses_View();
		}

        public static clsDetainedLicense Find(int DetainID)
		{
			int LicenseID = -1, CreatedByUserID = -1;
			int ReleasedByUserID = -1,ReleaseApplicationID = -1;
			DateTime DetainDate = DateTime.Now;
			DateTime ReleaseDate = DateTime.Now;
			float FineFees = -1f;
			bool IsReleased = false;	
			
			bool IsFound = DAL_DetainedLicenses.Find(DetainID,ref LicenseID,ref DetainDate,
				ref FineFees,ref CreatedByUserID,ref IsReleased
				,ref ReleaseDate,ref ReleasedByUserID,ref  ReleaseApplicationID);
			if (IsFound)
			{
				return new clsDetainedLicense(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased,
					ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
			}
			else
				return null;
			
		}

		public static clsDetainedLicense Find_ByLicenseID(int LicenseID)
		{
            int DetainID = -1, CreatedByUserID = -1;
            int ReleasedByUserID = -1, ReleaseApplicationID = -1;
            DateTime DetainDate = DateTime.Now;
            DateTime ReleaseDate = DateTime.Now;
            float FineFees = -1f;
            bool IsReleased = false;

            bool IsFound = DAL_DetainedLicenses.Find_ByLicenseID(LicenseID, ref DetainID, ref DetainDate,
                ref FineFees, ref CreatedByUserID, ref IsReleased
                , ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID);
            if (IsFound)
            {
                return new clsDetainedLicense(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased,
                    ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            }
            else
                return null;
        }

		private bool _AddNewDetainedLicense()
		{
			this.DetainID = DAL_DetainedLicenses.AddNewDeatinedLicense(this.LicenseID, this.DetainDate,
				this.FineFees, this.CreatedByUserID, this.IsReleased,
				this.ReleaseDate, this.ReleasedByUserID, this.ReleasedApplicationID);
			return (this.DetainID != -1);
		}
		
		private bool _UpdateDetainedLicense()
		{
			return DAL_DetainedLicenses.UpdateDetainedLicense(this.DetainID, this.LicenseID, this.DetainDate, this.FineFees
				, this.CreatedByUserID, this.IsReleased, this.ReleaseDate, this.ReleasedByUserID, this.ReleasedApplicationID);
		}

		public bool Save()
		{
			switch(_Mode)
			{
				case enMode.AddNew:
					if (_AddNewDetainedLicense())
					{
						_Mode = enMode.Update;
						return true;
					}
					else
						return false;
				case enMode.Update:
					return _UpdateDetainedLicense();
			}
			return false;
		}

		public static bool DeleteDetainedLicense(int DetainID)
		{
			return DAL_DetainedLicenses.DeleteDetainedLicense(DetainID);
		}

		public static bool DoesDetainedLicenseExist(int DetainID)
		{
			return DAL_DetainedLicenses.DoesDetainedLicenseExist(DetainID);
		}

		public static bool IsLicenseDetained(int LicenseID)
		{
			return DAL_DetainedLicenses.IsLicenseDetained(LicenseID);
		}

	}
}
