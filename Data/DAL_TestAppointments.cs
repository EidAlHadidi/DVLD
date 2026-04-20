//Tested class

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;

namespace Data
{
    public static class DAL_TestAppointments
    {
        //Read operations
        public static DataTable GetAllTestAppointments()
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from TestAppointments";

            SqlCommand cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                SqlDataReader R = cmd.ExecuteReader();
                if(R.HasRows)
                    dt.Load(R);
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

        public static bool Find(int TestAppointmentID,ref int TestTypeID,ref int LocalDrivingLicenseApplicationID,
            ref DateTime AppointmentDate,ref float PaidFees,ref int CreatedByUserID,ref bool isLocked,ref int RetakeTestApplicationID)
        {
            bool isFound = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "select * from TestAppointments where TestAppointmentID = @TestAppointmentID";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            try
            {
                conn.Open();
                SqlDataReader R = cmd.ExecuteReader();
                if(R.Read())
                {
                    isFound = true;
                    TestTypeID = Convert.ToInt32(R["TestTypeID"]);
                    LocalDrivingLicenseApplicationID = Convert.ToInt32(R["LocalDrivingLicenseApplicationID"]);
                    AppointmentDate = (DateTime)R["AppointmentDate"];
                    PaidFees = Convert.ToSingle(R["PaidFees"]);
                    CreatedByUserID = Convert.ToInt32(R["CreatedByUserID"]);
                    isLocked = (bool)R["IsLocked"];
                    if (R["RetakeTestApplicationID"] != DBNull.Value)
                    {
                        RetakeTestApplicationID = Convert.ToInt32(R["RetakeTestApplicationID"]);
                    }
                    else
                        RetakeTestApplicationID = -1;
                }
                else
                    isFound = false;
                R.Close();
            }
            catch (Exception e)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
            return isFound;
        }

        public static int AddNewAppointment(int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate,
            float PaidFees, int CreatedByUserID, bool isLocked, int RetakeTestApplicationID)
        {
            int TestAppointmentID = -1;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"INSERT INTO [dbo].[TestAppointments]
           ([TestTypeID]
           ,[LocalDrivingLicenseApplicationID]
           ,[AppointmentDate]
           ,[PaidFees]
           ,[CreatedByUserID]
           ,[IsLocked]
           ,[RetakeTestApplicationID])
            VALUES
                    (@TestTypeID,@LocalDrivingLicenseApplicationID,@AppointmentDate,@PaidFees,@CreatedByUserID,@isLocked,@RetakeTestApplicationID);
            SELECT SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            cmd.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            cmd.Parameters.AddWithValue("@PaidFees", PaidFees);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            cmd.Parameters.AddWithValue("@isLocked", isLocked);
            if (RetakeTestApplicationID == -1)
            {
                cmd.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
            }
            else
                cmd.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);

            try
                {
                    conn.Open();
                    object Result = cmd.ExecuteScalar();
                    if (Result != null && int.TryParse(Result.ToString(), out int InsertedID))
                    {
                        TestAppointmentID = InsertedID;
                    }
                    else
                        TestAppointmentID = -1;
                }
                catch (Exception e)
                {
                    TestAppointmentID = -1;
                    throw;
                }
                finally
                {
                    conn.Close();
                }

            return TestAppointmentID;
        }

        public static bool UpdateTestAppointment(int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID,
            DateTime AppointmentDate,float PaidFees, int CreatedByUserID, bool isLocked,int RetakeTestApplicationID)
        {
            int RowsAffected = 0;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE [dbo].[TestAppointments]
                           SET [TestTypeID] =@TestTypeID
                              ,[LocalDrivingLicenseApplicationID] =@LocalDrivingLicenseApplicationID
                              ,[AppointmentDate] =@AppointmentDate
                              ,[PaidFees] = @PaidFees
                              ,[CreatedByUserID] =@CreatedByUserID
                              ,[IsLocked] = @IsLocked
                              ,[RetakeTestApplicationID] = @RetakeTestApplicationID
                         WHERE TestAppointmentID = @TestAppointmentID";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            cmd.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            cmd.Parameters.AddWithValue("@PaidFees", PaidFees);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            cmd.Parameters.AddWithValue("@isLocked", isLocked);
            cmd.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            if(RetakeTestApplicationID == -1)
                cmd.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);

            try
            {
                conn.Open();
                RowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return (RowsAffected > 0);
        }

        public static bool DeleteTestAppointment(int TestAppointmentID)
        {
            int Rowsaffected = 0;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"DELETE FROM [dbo].[TestAppointments]
                             WHERE TestAppointmentID = @TestAppointmentID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            try
            {
                conn.Open();
                Rowsaffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return (Rowsaffected > 0);
        }

        public static bool IsTestAppointmentExist(int TestAppointmentID)
        {
            bool isFound = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select Found = 1 from TestAppointments where TestAppointmentID = @TestAppointmentID";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
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
            catch (Exception e)
            {
                isFound=false;
                throw e;
            }
            finally
            {
                conn.Close();
            }

            return isFound;
        }

    }
}
