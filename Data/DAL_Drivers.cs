using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Data
{
	public static class DAL_Drivers
	{
		public static DataTable GetAllDrivers()
		{
			DataTable dt = new DataTable();
			SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
			string query = "select * from drivers";
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

		public static DataTable GetAllDrivers_View()
		{
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "select * from Drivers_View";
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

		public static bool Find(int DriverID,ref int PersonID,ref int createdByUserID,ref DateTime CreatedDate)
		{
			bool isFound = false;

			SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
			string query = "select * from drivers where DriverID = @DriverID";
			SqlCommand cmd = new SqlCommand(query, conn);
			cmd.Parameters.AddWithValue("@DriverID", DriverID);
			try
			{
				conn.Open();
				SqlDataReader R = cmd.ExecuteReader();
				if (R.Read())
				{
					isFound = true;
					PersonID = Convert.ToInt32(R["PersonID"]);
					createdByUserID = Convert.ToInt32(R["CreatedByUserID"]);
					CreatedDate = (DateTime)R["CreatedDate"];
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
		
		public static int AddNewDriver(int PersonID,int CreatedByUserID,DateTime CreatedDate)
		{
			int DriverID = -1;
			SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
			string query = @"INSERT INTO [dbo].[Drivers]
           ([PersonID]
           ,[CreatedByUserID]
           ,[CreatedDate])
     VALUES
           (
			@PersonID,@CreatedByUserID,@CreatedDate
           );
			SELECT SCOPE_IDENTITY();";
			SqlCommand cmd = new SqlCommand(query, conn);
			cmd.Parameters.AddWithValue("@PersonID",PersonID);
			cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
			cmd.Parameters.AddWithValue("@CreatedDate", CreatedDate);
			try
			{
				conn.Open();
				object R = cmd.ExecuteScalar();
				if (R != null && int.TryParse(R.ToString(), out int InsertedID))
				{
					DriverID = InsertedID;
				}
				else
					DriverID = -1;
			}
			catch (Exception)
			{
				DriverID = -1;
				throw;
			}
			finally
			{
				conn.Close();
			}
			return DriverID;
		}

		public static bool UpdateDriver(int DriverID,int PersonID, int CreatedByUserID, DateTime CreatedDate)
		{
			int RowsAffected = 0;
			SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
			string query = @"UPDATE [dbo].[Drivers]
						  SET [PersonID] = @PersonID
						     ,[CreatedByUserID] = @CreatedByUserID
						     ,[CreatedDate] = @CreatedDate
						WHERE DriverID = @DriverID";
			SqlCommand cmd = new SqlCommand(query, conn);
			cmd.Parameters.AddWithValue("@PersonID", PersonID);
			cmd.Parameters.AddWithValue("@DriverID", DriverID);
			cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
			cmd.Parameters.AddWithValue("@CreatedDate", CreatedDate);

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

		public static bool DeleteDriver(int DriverID)
		{
			int RowsAffected = 0;	
			SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
			string query = "delete from Drivers where DriverID = @DriverID";
			SqlCommand cmd = new SqlCommand(query, conn);
			cmd.Parameters.AddWithValue("@DriverID", DriverID);
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

		public static bool DoesDriverExist(int DriverID)
		{
			bool isFound = false;
			SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
			string query = "select found=1 from drivers where DriverID = @DriverID";
			SqlCommand cmd = new SqlCommand(query, conn);
			cmd.Parameters.AddWithValue("@DriverID", DriverID);
			try
			{
				conn.Open();
				SqlDataReader R = cmd.ExecuteReader();
				if (R.HasRows)
				{
					isFound = true;
				}
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
