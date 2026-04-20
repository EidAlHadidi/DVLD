using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Data
{
	public static class DAL_InternationalLicenses
	{
		public static DataTable GetAllInternationalLicenses()
		{
			DataTable dt = new DataTable();

			SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
			string query = "select * from InternationalLicenses";

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

		public static DataTable GetAllInternationalLicenses_ByPersonID(int PersonID)
		{
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
			string query = @"select InternationalLicenses.*,People.PersonID from InternationalLicenses 
							inner join Applications on Applications.ApplicationID = InternationalLicenses.ApplicationID
							inner join People on People.PersonID = Applications.ApplicantPersonID
							where PersonID = @PersonID";
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

        public static bool Find(int InternationalLicenseID, ref int ApplicationID, ref int DriverID,
			ref int IssuedUsingLocalLicenseID, ref DateTime IssueDate,
			ref DateTime ExpirationDate, ref bool IsActive, ref int CreatedByUserID)
		{
			bool IsFound = false;

			SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
			string query = "select * from InternationalLicenses where InternationalLicenseID = @InternationalLicenseID";
			SqlCommand cmd = new SqlCommand(query, conn);
			cmd.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

			try
			{
				conn.Open();
				SqlDataReader R = cmd.ExecuteReader();
				if (R.Read())
				{
					IsFound = true;
					ApplicationID = Convert.ToInt32(R["ApplicationID"]);
					DriverID = Convert.ToInt32(R["DriverID"]);
					IssuedUsingLocalLicenseID = Convert.ToInt32(R["IssuedUsingLocalLicenseID"]);
					IssueDate = (DateTime)R["IssueDate"];
					ExpirationDate = (DateTime)R["ExpirationDate"];
					IsActive = (bool)R["IsActive"];
					CreatedByUserID = Convert.ToInt32(R["CreatedByUserID"]);
				}
				else
					IsFound = false;
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

			return IsFound;
		}
        
		public static int AddNewInternationalLicense(int ApplicationID, int DriverID,
			int IssuedUsingLocalLicenseID, DateTime IssueDate,
			 DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
		{
			int InternationalLicenseID = -1;

			SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
			string query = @"INSERT INTO [dbo].[InternationalLicenses]
           ([ApplicationID]
           ,[DriverID]
           ,[IssuedUsingLocalLicenseID]
           ,[IssueDate]
           ,[ExpirationDate]
           ,[IsActive]
           ,[CreatedByUserID])
     VALUES
           (@ApplicationID,@DriverID,@IssuedUsingLocalLicenseID,@IssueDate,@ExpirationDate,@IsActive,@CreatedByUserID);
			SELECT SCOPE_IDENTITY();";
			SqlCommand cmd = new SqlCommand(query, conn);
			cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
			cmd.Parameters.AddWithValue("@DriverID", DriverID);
			cmd.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
			cmd.Parameters.AddWithValue("@IssueDate", IssueDate);
			cmd.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
			cmd.Parameters.AddWithValue("@IsActive", IsActive);
			cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

			try
			{
				conn.Open();
				object R = cmd.ExecuteScalar();
				if (R != null && int.TryParse(R.ToString(), out int InsertedID))
				{
					InternationalLicenseID = InsertedID;
				}
				else
					InternationalLicenseID = -1;
			}
			catch (Exception)
			{

				throw;
			}
			finally
			{
				conn.Close();
			}

			return InternationalLicenseID;
		}

		public static bool UpdateInternaltionalLicense(int InternationalLicenseID,int ApplicationID, int DriverID,
			int IssuedUsingLocalLicenseID, DateTime IssueDate,
			 DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
		{
			int RowsAffected = 0;

			SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
			string query = @"UPDATE [dbo].[InternationalLicenses]
					   SET [ApplicationID] = @ApplicationID
					      ,[DriverID] = @DriverID
					      ,[IssuedUsingLocalLicenseID] = @IssuedUsingLocalLicenseID
					      ,[IssueDate] = @IssueDate
					      ,[ExpirationDate] = @ExpirationDate
					      ,[IsActive] = @IsActive
					      ,[CreatedByUserID] = @CreatedByUserID
					 WHERE InternationalLicenseID = @InternationalLicenseID";

			SqlCommand cmd = new SqlCommand(query, conn);
			cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
			cmd.Parameters.AddWithValue("@DriverID", DriverID);
			cmd.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
			cmd.Parameters.AddWithValue("@IssueDate", IssueDate);
			cmd.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
			cmd.Parameters.AddWithValue("@IsActive", IsActive);
			cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
			cmd.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

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

		public static bool DeleteInternationalLicense(int InternationalLicenseID)
		{
			int RowsAffected = 0;

			SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

			string query = "delete from InternationalLicenses where InternationalLicenseID = @InternationalLicenseID";

			SqlCommand cmd = new SqlCommand(query, conn);
			
			cmd.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

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

		public static bool DoesInternationalLicenseExist(int InternationalLicenseID)
		{
			bool isFound = false;

			SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

			string query = "select found=1 from InternationalLicenses where InternationalLicenseID = @InternationalLicenseID";

			SqlCommand cmd = new SqlCommand(query, conn);

			cmd.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

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

		public static bool DoesInternationalLicenseExist_ByLienseID(int LicenseID, bool IsActive)
		{
			bool isFound = false;

			SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

			string query = "select found=1 from InternationalLicenses where IssuedUsingLocalLicenseID = @IssuedUsingLocalLicenseID and IsActive = @IsActive";

			SqlCommand cmd = new SqlCommand(query, conn);

			cmd.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", LicenseID);
			cmd.Parameters.AddWithValue("@IsActive", Convert.ToInt32(IsActive));
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
