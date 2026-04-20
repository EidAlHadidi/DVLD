using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.ComponentModel;

namespace Data
{
	public static class DAL_DetainedLicenses
	{
		public static DataTable GetAllDetainedLicenses()
		{
			DataTable dt = new DataTable();
			SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
			string query = "select * from DetainedLicenses";
			SqlCommand cmd = new SqlCommand(query, conn);
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

        public static DataTable GetAllDetainedLicenses_View()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "select * from DetainedLicenses_View";
            SqlCommand cmd = new SqlCommand(query, conn);
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

        public static bool Find(int DetainID, ref int LicenseID, ref DateTime DetainDate,
			ref float FineFees, ref int CreatedByUserID, ref bool IsReleased, ref DateTime ReleaseDate,
			ref int ReleasedByUserID, ref int ReleaseApplicationID)
		{
			bool isFound = false;

			SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
			string query = "select * from DetainedLicenses where DetainID = @DetainID";
			SqlCommand cmd = new SqlCommand(query, conn);
			cmd.Parameters.AddWithValue("@DetainID", DetainID);
			try
			{
				conn.Open();
				SqlDataReader R = cmd.ExecuteReader();
				if (R.Read())
				{
					isFound = true;
					LicenseID = Convert.ToInt32(R["LicenseID"]);
					DetainDate = (DateTime)R["DetainDate"];
					FineFees = Convert.ToSingle(R["FineFees"]);
					CreatedByUserID = Convert.ToInt32(R["CreatedByUserID"]);
					IsReleased = (bool)R["IsReleased"];
					if (R["ReleaseDate"] == DBNull.Value)
					{
						ReleaseDate = DateTime.MinValue;
					}
					else
						ReleaseDate = (DateTime)R["ReleaseDate"];

					if (R["ReleasedByUserID"] == DBNull.Value)
					{
						ReleasedByUserID = -1;
					}
					else
						ReleasedByUserID = Convert.ToInt32(R["ReleasedByUserID"]);

					if (R["ReleaseApplicationID"] == DBNull.Value)
					{
						ReleaseApplicationID = -1;
					}
					else
						ReleaseApplicationID = Convert.ToInt32(R["ReleaseApplicationID"]);
				}
				else
					isFound = false;
				R.Close();
			}
			catch (Exception)
			{
				isFound = false;
				throw;
			}
			finally
			{
				conn.Close();
			}


			return isFound;
		}

        public static bool Find_ByLicenseID(int LicenseID, ref int DetainID, ref DateTime DetainDate,
            ref float FineFees, ref int CreatedByUserID, ref bool IsReleased, ref DateTime ReleaseDate,
            ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            bool isFound = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "select top 1 * from DetainedLicenses where LicenseID = @LicenseID and IsReleased = 0";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@LicenseID", LicenseID);
            try
            {
                conn.Open();
                SqlDataReader R = cmd.ExecuteReader();
                if (R.Read())
                {
                    isFound = true;
                    DetainID = Convert.ToInt32(R["DetainID"]);
                    DetainDate = (DateTime)R["DetainDate"];
                    FineFees = Convert.ToSingle(R["FineFees"]);
                    CreatedByUserID = Convert.ToInt32(R["CreatedByUserID"]);
                    IsReleased = (bool)R["IsReleased"];
                    if (R["ReleaseDate"] == DBNull.Value)
                    {
                        ReleaseDate = DateTime.MinValue;
                    }
                    else
                        ReleaseDate = (DateTime)R["ReleaseDate"];

                    if (R["ReleasedByUserID"] == DBNull.Value)
                    {
                        ReleasedByUserID = -1;
                    }
                    else
                        ReleasedByUserID = Convert.ToInt32(R["ReleasedByUserID"]);

                    if (R["ReleaseApplicationID"] == DBNull.Value)
                    {
                        ReleaseApplicationID = -1;
                    }
                    else
                        ReleaseApplicationID = Convert.ToInt32(R["ReleaseApplicationID"]);
                }
                else
                    isFound = false;
                R.Close();
            }
            catch (Exception)
            {
                isFound = false;
                throw;
            }
            finally
            {
                conn.Close();
            }


            return isFound;
        }


        public static int AddNewDeatinedLicense(int LicenseID, DateTime DetainDate,
		 float FineFees, int CreatedByUserID, bool IsReleased, DateTime ReleaseDate,
		 int ReleasedByUserID, int ReleaseApplicationID)
		{
			int DetainID = -1;

			SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

			string query = @"INSERT INTO [dbo].[DetainedLicenses]
           ([LicenseID]
           ,[DetainDate]
           ,[FineFees]
           ,[CreatedByUserID]
           ,[IsReleased]
           ,[ReleaseDate]
           ,[ReleasedByUserID]
           ,[ReleaseApplicationID])
     VALUES
           (@LicenseID,@DetainDate,@FineFees,@CreatedByUserID,@IsReleased
		   ,@ReleaseDate,@ReleasedByUserID,@ReleaseApplicationID);
			SELECT SCOPE_IDENTITY();";

			SqlCommand cmd = new SqlCommand(query, conn);

			cmd.Parameters.AddWithValue("@LicenseID", LicenseID);
			cmd.Parameters.AddWithValue("@DetainDate", DetainDate);
			cmd.Parameters.AddWithValue("@FineFees", FineFees);
			cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
			cmd.Parameters.AddWithValue("@IsReleased", IsReleased);
			if(ReleaseDate  == DateTime.MinValue)
			{
				cmd.Parameters.AddWithValue("@ReleaseDate", DBNull.Value);
			}
			else
				cmd.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);

			if (ReleasedByUserID == -1)
				cmd.Parameters.AddWithValue("@ReleasedByUserID", DBNull.Value);
			else
				cmd.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);

			if (ReleaseApplicationID == -1)
				cmd.Parameters.AddWithValue("@ReleaseApplicationID", DBNull.Value);
			else
				cmd.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);

			try
			{
				conn.Open();
				object R = cmd.ExecuteScalar();
				if (R != null && int.TryParse(R.ToString(), out int InsertedID))
				{
					DetainID = InsertedID;
				}
				else
					DetainID = -1;
			}
			catch (Exception)
			{
				DetainID = -1;
				throw;
			}
			finally
			{
				conn.Close();
			}

			return DetainID;
		}

		public static bool UpdateDetainedLicense(int DetainID, int LicenseID, DateTime DetainDate,
		 float FineFees, int CreatedByUserID, bool IsReleased, DateTime ReleaseDate,
		 int ReleasedByUserID, int ReleaseApplicationID)
		{
			int RowsAffected = 0;

			SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
			string query = @"UPDATE [dbo].[DetainedLicenses]
						   SET [LicenseID] = @LicenseID
						      ,[DetainDate] = @DetainDate
						      ,[FineFees] = @FineFees
						      ,[CreatedByUserID] = @CreatedByUserID
						      ,[IsReleased] = @IsReleased
						      ,[ReleaseDate] = @ReleaseDate
						      ,[ReleasedByUserID] = @ReleasedByUserID
						      ,[ReleaseApplicationID] = @ReleaseApplicationID
						 WHERE DetainID = @DetainID";
			SqlCommand cmd = new SqlCommand(query, conn);

			cmd.Parameters.AddWithValue("@LicenseID", LicenseID);
			cmd.Parameters.AddWithValue("@DetainDate", DetainDate);
			cmd.Parameters.AddWithValue("@FineFees", FineFees);
			cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
			cmd.Parameters.AddWithValue("@IsReleased", IsReleased);
			if (ReleaseDate == DateTime.MinValue)
			{
				cmd.Parameters.AddWithValue("@ReleaseDate", DBNull.Value);
			}
			else
				cmd.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);

			if (ReleasedByUserID == -1)
				cmd.Parameters.AddWithValue("@ReleasedByUserID", DBNull.Value);
			else
				cmd.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);

			if (ReleaseApplicationID == -1)
				cmd.Parameters.AddWithValue("@ReleaseApplicationID", DBNull.Value);
			else
				cmd.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
			cmd.Parameters.AddWithValue("@DetainID", DetainID);
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

			return (RowsAffected > 0);
		}

		public static bool DeleteDetainedLicense(int DetainID)
		{
			int RowsAffected = 0;

			SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
			string query = @"delete from DetainedLicenses where DetainID = @DetainID";
			SqlCommand cmd = new SqlCommand(query, conn);
			cmd.Parameters.AddWithValue("@DetainID", DetainID);
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

			return (RowsAffected > 0);
		}

		public static bool DoesDetainedLicenseExist(int DetainID)
		{
			bool isFound = false;

			SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
			string query = "select found=1 from DetainedLicenses where DetainID = @DetainID";
			SqlCommand cmd = new SqlCommand(query,conn);
			cmd.Parameters.AddWithValue("@DetainID", DetainID);
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
				isFound = false;
				throw;
			}
			finally
			{
				conn.Close();
			}

			return isFound;
		}

		public static bool IsLicenseDetained(int LicenseID)
		{
			bool isFound = false;

			SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
			string query = "select found=1 from DetainedLicenses where LicenseID = @LicenseID and IsReleased = 0";
			SqlCommand cmd = new SqlCommand(query, conn);
			cmd.Parameters.AddWithValue("@LicenseID", LicenseID);
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
				isFound = false;
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
