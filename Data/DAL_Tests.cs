using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;

namespace Data
{
    public static class DAL_Tests
    {
        public static DataTable GetAllTests()
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "Select * from tests";

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

        public static DataTable GetAllTests_MyView()
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "Select * from Tests_MyView";

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
        public static bool Find(int TestID, ref int TestAppointmentID, ref bool TestResult,
            ref string Notes, ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "select * from Tests where TestID = @TestID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@TestID", TestID);
            try
            {
                conn.Open();
                SqlDataReader R = cmd.ExecuteReader();
                if (R.Read())
                {
                    isFound = true;
                    TestAppointmentID = Convert.ToInt32(R["TestAppointmentID"]);
                    TestResult = (bool)R["TestResult"];
                    Notes = R["Notes"] != DBNull.Value ? (string)R["Notes"] : string.Empty;
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

        public static bool Find(string NationalNo,int TestTypeID,int ApplicationStatus,ref string TestTypeTitle,ref int NumberOfTrials)
        {
            bool isFound = false;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select NationalNo,TestTypeID,TestTypeTitle,NumberOfTrials,ApplicationStatus
                                    from Tests_MyView2 where NationalNo = @NationalNo AND TestTypeID = @TestTypeID
                                     and ApplicationStatus = @ApplicationStatus";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@NationalNo", NationalNo);
            cmd.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            cmd.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);

            try
            {
                conn.Open();
                SqlDataReader R = cmd.ExecuteReader();
                if (R.Read())
                {
                    isFound = true;
                    TestTypeTitle = (string)R["TestTypeTitle"];
                    NumberOfTrials = Convert.ToInt32(R["NumberOfTrials"]);
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

        public static int AddNewTest(int TestAppointmentID, bool TestResult,
            string Notes, int CreatedByUserID)
        {
            int TestID = -1;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"INSERT INTO [dbo].[Tests]
           ([TestAppointmentID]
           ,[TestResult]
           ,[Notes]
           ,[CreatedByUserID])
     VALUES
           (@TestAppointmentID,@TestResult,@Notes,@CreatedByUserID);
            SELECT SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            cmd.Parameters.AddWithValue("@TestResult", TestResult);

            if (string.IsNullOrEmpty(Notes))
            {
                cmd.Parameters.AddWithValue("@Notes", DBNull.Value);
            }
            else
                cmd.Parameters.AddWithValue("@Notes", Notes);

            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                conn.Open();
                object Result = cmd.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int InsertedID))
                {
                    TestID = InsertedID;
                }
                else
                    TestID = -1;
            }
            catch (Exception)
            {
                
                throw;
            }
            finally
            {
                conn.Close();
            }

            return TestID;
        }
        public static bool UpdateTest(int TestID, int TestAppointmentID, bool TestResult
            , string Notes, int CreatedByUserID)
        {
            int RowsAffected = 0;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"UPDATE [dbo].[Tests]
                           SET [TestAppointmentID] = @TestAppointmentID
                              ,[TestResult] = @TestResult
                              ,[Notes] = @Notes
                              ,[CreatedByUserID] = @CreatedByUserID
                         WHERE TestID = @TestID";
            
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            cmd.Parameters.AddWithValue("@TestResult", TestResult);
            if(string.IsNullOrEmpty(Notes))
                cmd.Parameters.AddWithValue("@Notes", Notes);
            else
                cmd.Parameters.AddWithValue("@Notes", DBNull.Value);

            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            cmd.Parameters.AddWithValue("@TestID", TestID);
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

        public static bool DeleteTest(int TestID)
        {
			int Rowsaffected = 0;
			SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
			string query = "delete from Tests where TestID = @TestID";
			SqlCommand cmd = new SqlCommand(query, conn);
			cmd.Parameters.AddWithValue("@TestID", TestID);
			try
			{
				conn.Open();
				Rowsaffected = cmd.ExecuteNonQuery();
			}
			catch (Exception ex)
			{

                throw;
			}
			finally
			{
				conn.Close();
			}
			return (Rowsaffected > 0);
		}

        public static bool DoesTestExist(int TestID)
        {
            bool isFound = false;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "select found=1 from Tests where TestID = @TestID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@TestID", TestID);
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