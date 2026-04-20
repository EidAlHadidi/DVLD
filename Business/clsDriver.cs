using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Data;

namespace Business
{
	public class clsDriver
	{
		private enum enMode { AddNew,Update};
		private enMode _Mode = enMode.AddNew;

		public int DriverID { get; set; }
		public int PersonID {  get; set; }
		public clsPerson Person
		{
			get
			{
				return clsPerson.Find(PersonID);
			}
			set
			{

			}
		}

		public int CreatedByUserID { get; set; }
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

		public DateTime CreatedDate { get; set; }

		public clsDriver()
		{
			_Mode = enMode.AddNew;
			DriverID = -1;
			PersonID = -1;
			CreatedByUserID = -1;
			CreatedDate = DateTime.Now;
		}

		private clsDriver(int driverID, int personID, int createdByUserID, DateTime createdDate)
		{
			_Mode = enMode.Update;
			DriverID = driverID;
			PersonID = personID;
			CreatedByUserID = createdByUserID;
			CreatedDate = createdDate;
		}

		public static DataTable GetAllDrivers()
		{
			return DAL_Drivers.GetAllDrivers();
		}

		public static DataTable GetAllDrivers_View()
		{
			return DAL_Drivers.GetAllDrivers_View();
		}


        public static clsDriver Find(int DriverID)
		{
			int PersonID = -1, CreatedByUserID = -1;
			DateTime CreatedDate = default;
			bool isFound = DAL_Drivers.Find(DriverID, ref PersonID, ref CreatedByUserID, ref CreatedDate);
			if (isFound)
			{
				return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
			}
			else
				return null;

		}

		private bool _AddNewDriver()
		{
			this.DriverID = DAL_Drivers.AddNewDriver(this.PersonID,this.CreatedByUserID,this.CreatedDate);
			return (this.DriverID != -1);
		}

		private bool _UpdateDriver()
		{
			return DAL_Drivers.UpdateDriver(this.DriverID, this.PersonID, this.CreatedByUserID, this.CreatedDate);
		}

		public bool Save()
		{
			switch(_Mode)
			{
				case enMode.AddNew:
					if (_AddNewDriver())
					{
						_Mode = enMode.Update;
						return true;
					}
					else
						return false;
					break;
				case enMode.Update:
					return _UpdateDriver();
			}
			return false;
		}

		public static bool DeleteDriver(int DriverID)
		{
			return DAL_Drivers.DeleteDriver(DriverID);
		}

		public static bool DoesDriverExist(int DriverID)
		{
			return DAL_Drivers.DoesDriverExist(DriverID);
		}

	}
}
