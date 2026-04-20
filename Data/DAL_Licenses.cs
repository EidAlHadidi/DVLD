using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
	public static class DAL_Licenses
	{
		public static DataTable GetAllLicenses()
		{
			DataTable dt = new DataTable();

			SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
			string query = "select * from licenses";
			SqlCommand cmd = new SqlCommand(query, conn);
			try
			{
				conn.Open();
				SqlDataReader R = cmd.ExecuteReader();
				if(R.HasRows)
				{
					dt.Load(R);
				}
				R.Close();
			}
			catch (Exception)
			{

				throw;
			}
			finally
			{
				conn.Close();
			}

			return dt;
		}

		public static DataTable LicensesView_PersonID(int PersonID)
		{
			DataTable dt = new DataTable();

			SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
			string query = "select * from LicenseHistoryByPersonID where PersonID = @PersonID";
			SqlCommand cmd = new SqlCommand(query, conn);
			cmd.Parameters.AddWithValue("@PersonID", PersonID);
			try
			{
				conn.Open();
				SqlDataReader R = cmd.ExecuteReader();
				if (R.HasRows)
				{
					dt.Load(R);
				}
				R.Close();

			}
			catch (Exception)
			{

				throw;
			}
			finally
			{
				conn.Close();
			}


            return dt;
		}

		public static bool Find(int LicenseID,ref int ApplicationID,ref int DriverID,ref int LicenseClass,
			ref DateTime IssueDate,ref DateTime ExpirationDate,ref string Notes,ref float PaidFees,
			ref bool IsActive,ref short IssueReason,ref int CreatedByUserID,ref int IssuedByLocalAppID,bool SearchByLicenseID)
		{
			bool isFound = false;

			SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
			string query = @"select Licenses.*,LocalDrivingLicenseApplicationID from Licenses
							left join LocalDrivingLicenseApplications on LocalDrivingLicenseApplications.ApplicationID = Licenses.ApplicationID
							where LicenseID = @LicenseID";
			SqlCommand cmd = new SqlCommand(query, conn);
			cmd.Parameters.AddWithValue("@LicenseID", LicenseID);

			try
			{
				conn.Open();
				SqlDataReader R = cmd.ExecuteReader();
				if(R.Read())
				{
					isFound = true;
					ApplicationID = Convert.ToInt32(R["ApplicationID"]);
					DriverID = Convert.ToInt32(R["DriverID"]);
					LicenseClass = Convert.ToInt32(R["LicenseClass"]);
					IssueDate = (DateTime)R["IssueDate"];
					ExpirationDate = (DateTime)R["ExpirationDate"];
					Notes = (R["Notes"] == DBNull.Value) ? Notes = string.Empty : (string)R["Notes"];
					PaidFees = Convert.ToSingle(R["PaidFees"]);
					IsActive = (bool)R["IsActive"];
					IssueReason = Convert.ToInt16(R["IssueReason"]);
					CreatedByUserID = Convert.ToInt32(R["CreatedByUserID"]);
					IssuedByLocalAppID = (R["LocalDrivingLicenseApplicationID"] == DBNull.Value) ? IssuedByLocalAppID = -1 : Convert.ToInt32(R["LocalDrivingLicenseApplicationID"]);
				}
				else
					isFound = false;
				R.Close();
			}
			catch (Exception)
			{

				throw;
			}
			finally
			{
				conn.Close();
			}

			return isFound;
		}

        public static bool Find(int LocalAppID,ref int LicenseID, ref int ApplicationID, ref int DriverID, ref int LicenseClass,
            ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref float PaidFees,
            ref bool IsActive, ref short IssueReason, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select licenses.*,LocalDrivingLicenseApplicationID from Licenses 
							inner join LocalDrivingLicenseApplications on 
							LocalDrivingLicenseApplications.ApplicationID = Licenses.ApplicationID
							where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalAppID);

            try
            {
                conn.Open();
                SqlDataReader R = cmd.ExecuteReader();
                if (R.Read())
                {
                    isFound = true;
					LicenseID = Convert.ToInt32(R["LicenseID"]);
                    ApplicationID = Convert.ToInt32(R["ApplicationID"]);
                    DriverID = Convert.ToInt32(R["DriverID"]);
                    LicenseClass = Convert.ToInt32(R["LicenseClass"]);
                    IssueDate = (DateTime)R["IssueDate"];
                    ExpirationDate = (DateTime)R["ExpirationDate"];
                    Notes = (R["Notes"] == DBNull.Value) ? Notes = string.Empty : (string)R["Notes"];
                    PaidFees = Convert.ToSingle(R["PaidFees"]);
                    IsActive = (bool)R["IsActive"];
                    IssueReason = Convert.ToInt16(R["IssueReason"]);
                    CreatedByUserID = Convert.ToInt32(R["CreatedByUserID"]);
                }
                else
                    isFound = false;
                R.Close();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return isFound;
        }

        public static int AddNewLicense(int ApplicationID, int DriverID, int LicenseClass,
			 DateTime IssueDate, DateTime ExpirationDate, string Notes, float PaidFees,
			 bool IsActive, short IssueReason, int CreatedByUserID)
		{
			int LicenseID = -1;

			SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
			string query = @"INSERT INTO [dbo].[Licenses]
	          ([ApplicationID]
	          ,[DriverID]
	          ,[LicenseClass]
	          ,[IssueDate]
	          ,[ExpirationDate]
	          ,[Notes]
	          ,[PaidFees]
	          ,[IsActive]
	          ,[IssueReason]
	          ,[CreatedByUserID])
	    VALUES
	          (@ApplicationID,@DriverID,@LicenseClass,@IssueDate,@ExpirationDate
				,@Notes,@PaidFees,@IsActive,@IssueReason,@CreatedByUserID);
				SELECT SCOPE_IDENTITY();";
			SqlCommand cmd = new SqlCommand(query, conn);
			cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
			cmd.Parameters.AddWithValue("@DriverID", DriverID);
			cmd.Parameters.AddWithValue("@LicenseClass", LicenseClass);
			cmd.Parameters.AddWithValue("@IssueDate", IssueDate);
			cmd.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);

			if (!string.IsNullOrEmpty(Notes))
				cmd.Parameters.AddWithValue("@Notes", Notes);
			else
				cmd.Parameters.AddWithValue("@Notes", DBNull.Value);
			
			cmd.Parameters.AddWithValue("@PaidFees", PaidFees);
			cmd.Parameters.AddWithValue("@IsActive", IsActive);
			cmd.Parameters.AddWithValue("@IssueReason", IssueReason);
			cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

			try
			{
				conn.Open();
				object R = cmd.ExecuteScalar();
				if (R != null && int.TryParse(R.ToString(), out int InsertedID))
				{
					LicenseID = InsertedID;
				}
				else
					LicenseID = -1;
			}
			catch (Exception)
			{
				LicenseID = -1;
				throw;
			}
			finally
			{
				conn.Close();
			}

			return LicenseID;
		}

		public static bool UpdateLicense(int LicenseID,int ApplicationID, int DriverID, int LicenseClass,
			 DateTime IssueDate, DateTime ExpirationDate, string Notes, float PaidFees,
			 bool IsActive, short IssueReason, int CreatedByUserID)
		{
			int RowsAffected = 0;

			SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

			string query = @"UPDATE [dbo].[Licenses]
					   SET [ApplicationID] = @ApplicationID
					      ,[DriverID] = @DriverID
					      ,[LicenseClass] = @LicenseClass
					      ,[IssueDate] = @IssueDate
					      ,[ExpirationDate] = @ExpirationDate
					      ,[Notes] = @Notes
					      ,[PaidFees] = @PaidFees
					      ,[IsActive] = @IsActive
					      ,[IssueReason] = @IssueReason
					      ,[CreatedByUserID] = @CreatedByUserID
					 WHERE LicenseID = @LicenseID";

			SqlCommand cmd = new SqlCommand(query, conn);
			cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
			cmd.Parameters.AddWithValue("@DriverID", DriverID);
			cmd.Parameters.AddWithValue("@LicenseClass", LicenseClass);
			cmd.Parameters.AddWithValue("@IssueDate", IssueDate);
			cmd.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
			if (!string.IsNullOrEmpty(Notes))
				cmd.Parameters.AddWithValue("@Notes", Notes);
			else
				cmd.Parameters.AddWithValue("@Notes", DBNull.Value);
			cmd.Parameters.AddWithValue("@PaidFees", PaidFees);
			cmd.Parameters.AddWithValue("@IsActive", IsActive);
			cmd.Parameters.AddWithValue("@IssueReason", IssueReason);
			cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
			cmd.Parameters.AddWithValue("@LicenseID", LicenseID);

			try
			{
				conn.Open();
				RowsAffected = cmd.ExecuteNonQuery();
			}
			catch (Exception)
			{

				throw;
			}
			finally
			{
				conn.Close();
			}

			return RowsAffected > 0;
		}

		public static bool DeleteLicense(int LicenseID)
		{
			int RowsAffected = 0;

			SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
			string query = "delete from Licenses where LicenseID = @LicenseID";
			SqlCommand cmd = new SqlCommand(query, conn);
			cmd.Parameters.AddWithValue("@LicenseID", LicenseID);
			try
			{
				conn.Open();
				RowsAffected = cmd.ExecuteNonQuery();
			}
			catch (Exception)
			{

				throw;
			}
			finally
			{
				conn.Close();
			}

			return RowsAffected > 0;
		}

		public static bool DoesLicenseExist(int LicenseID)
		{
			bool isFound = false;

			SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
			string query = "select found=1 from Licenses where LicenseID = @LicenseID";
			SqlCommand cmd = new SqlCommand(query, conn);
			cmd.Parameters.AddWithValue("@LicenseID", LicenseID);

			try
			{
				conn.Open();
				SqlDataReader R = cmd.ExecuteReader();
				if(R.HasRows)
				{
					isFound = true;
				}
				else
					isFound = false;
				R.Close();
			}
			catch (Exception)
			{

				throw;
			}
			finally
			{
				conn.Close();
			}

			return isFound;
		}

		public static bool DoesLicenseExist(string NationalNo,int LicenseClass,bool IsActive)
		{
            bool isFound = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select found=1 from licenses
							inner join drivers on drivers.DriverID = licenses.DriverID
							inner join people on Drivers.PersonID = people.PersonID
							where NationalNo = @NationalNo and 
							IsActive = @IsActive and LicenseClass = @LicenseClass";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@NationalNo", NationalNo);
            cmd.Parameters.AddWithValue("@IsActive", IsActive);
            cmd.Parameters.AddWithValue("@LicenseClass", LicenseClass);

            try
            {
                conn.Open();
                SqlDataReader R = cmd.ExecuteReader();
                if (R.HasRows)
                {
                    isFound = true;
                }
                else
                    isFound = false;
                R.Close();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return isFound;
        }

	}
}
