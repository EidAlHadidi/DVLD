using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace Data
{
    public static class DAL_Applications
    {
        public static DataTable GetAllApplications()
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select ApplicationID,ApplicantPersonID,ApplicationDate,ApplicationTypeID,
                            ApplicationStatus,
                            case ApplicationStatus
                            when 1 then 'New'
                            when 2 then 'Cancelled'
                            when 3 then 'Completed'
                            end as Status,
                            LastStatusDate,PaidFees,CreatedByUserID";

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
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }

            return dt;
        }

        public static bool Find(int ApplicationID,ref int ApplicantPersonID,ref DateTime ApplicationDate,
            ref int ApplicationTypeID,ref int ApplicationStatus,ref DateTime LastStatusDate,ref float PaidFees,
            ref int CreatedByUserID)
        {
            bool IsFound = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from Applications where ApplicationID = @ApplicationID";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                conn.Open();
                SqlDataReader R = cmd.ExecuteReader();
                if (R.Read())
                {
                    IsFound = true;
                    ApplicantPersonID = Convert.ToInt32(R["ApplicantPersonID"]);
                    ApplicationDate = (DateTime)R["ApplicationDate"];
                    ApplicationTypeID = Convert.ToInt32(R["ApplicationTypeID"]);
                    ApplicationStatus = Convert.ToInt32(R["ApplicationStatus"]);
                    LastStatusDate = (DateTime)R["LastStatusDate"];
                    PaidFees = Convert.ToSingle(R["PaidFees"]);
                    CreatedByUserID = Convert.ToInt32(R["CreatedByUserID"]);
                }
                else
                    IsFound = false;
                R.Close();
            }
            catch (Exception ex)
            {
                IsFound = false;
            }
            finally
            {
                conn.Close();
            }

            return IsFound;
        }

        public static bool Find(int ApplicantPersonID, int ApplicationTypeID, int ApplicationStatus,
            ref int ApplicationID, ref DateTime ApplicationDate, ref DateTime LastStatusDate, ref float PaidFees,
            ref int CreatedByUserID)
        {
            bool IsFound = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from Applications where ApplicantPersonID = @ApplicantPersonID 
                            and ApplicationTypeID = @ApplicationTypeID and ApplicationStatus = @ApplicationStatus";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            cmd.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            cmd.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                conn.Open();
                SqlDataReader R = cmd.ExecuteReader();
                if (R.Read())
                {
                    IsFound = true;
                    ApplicantPersonID = Convert.ToInt32(R["ApplicationID"]);
                    ApplicationDate = (DateTime)R["ApplicationDate"];
                    LastStatusDate = (DateTime)R["LastStatusDate"];
                    PaidFees = Convert.ToSingle(R["PaidFees"]);
                    CreatedByUserID = Convert.ToInt32(R["CreatedByUserID"]);
                    ApplicationID = Convert.ToInt32(R["ApplicationID"]);
                }
                else
                    IsFound = false;
                R.Close();
            }
            catch (Exception ex)
            {
                IsFound = false;
            }
            finally
            {
                conn.Close();
            }

            return IsFound;
        }

        public static int AddNewApplication(int ApplicantPersonID,DateTime ApplicationDate,
            int ApplicationTypeID, int ApplicationStatus,DateTime LastStatusDate,float PaidFees,
            int CreatedByUserID)
        {
            int AppID = -1;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO [dbo].[Applications]
                                       ([ApplicantPersonID]
                                       ,[ApplicationDate]
                                       ,[ApplicationTypeID]
                                       ,[ApplicationStatus]
                                       ,[LastStatusDate]
                                       ,[PaidFees]
                                       ,[CreatedByUserID])
                                 VALUES
                                       (@ApplicantPersonID,@ApplicationDate,@ApplicationTypeID,
                            		   @ApplicationStatus,@LastStatusDate,@PaidFees,@CreatedByUserID);
                            select SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            cmd.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            cmd.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            cmd.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            cmd.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            cmd.Parameters.AddWithValue("@PaidFees", PaidFees);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                conn.Open();
                object R = cmd.ExecuteScalar();
                if (R != null && int.TryParse(R.ToString(), out int InsertedID))
                {
                    AppID = InsertedID;
                }
                else
                    AppID = -1;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }

            return AppID;
        }

        public static bool UpdateApplication(int ApplicationID,int ApplicantPersonID, DateTime ApplicationDate,
            int ApplicationTypeID, int ApplicationStatus, DateTime LastStatusDate, float PaidFees,
            int CreatedByUserID)
        {
            int RowsAffected = 0;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE [dbo].[Applications]
                               SET [ApplicantPersonID] =  @ApplicantPersonID
                                  ,[ApplicationDate]   =  @ApplicationDate
                                  ,[ApplicationTypeID] =  @ApplicationTypeID
                                  ,[ApplicationStatus] =  @ApplicationStatus
                                  ,[LastStatusDate]    =  @LastStatusDate
                                  ,[PaidFees]          =  @PaidFees
                                  ,[CreatedByUserID]   =  @CreatedByUserID
                             WHERE ApplicationID       =  @ApplicationID";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            cmd.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            cmd.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            cmd.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            cmd.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            cmd.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            cmd.Parameters.AddWithValue("@PaidFees", PaidFees);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                conn.Open();
                RowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                RowsAffected = 0;
            }
            finally
            {
                conn.Close();
            }

            return (RowsAffected > 0);
        }

        public static bool DeleteApplication(int ApplicationID)
        {
            int RowsAffected = 0;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "delete from Applications where ApplicationID = @ApplicationID";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            
            try
            {
                conn.Open();
                RowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                RowsAffected = 0;
            }
            finally
            {
                conn.Close();
            }

            return (RowsAffected > 0);
        }

        public static bool isApplicationExist(int ApplicationID)
        {
            bool isFound = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select Found=1 from Applications where ApplicationID = @ApplicationID";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);

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
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                conn.Close();
            }

            return isFound;
        }

        public static bool isApplicationExist(int ApplicantPersonID,int ApplicationTypeID,int ApplicationStatus)
        {
            bool isFound = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select Found=1 from Applications where ApplicantPersonID = @ApplicantPersonID 
                            and ApplicationTypeID = @ApplicationTypeID and ApplicationStatus = @ApplicationStatus";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            cmd.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            cmd.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);

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
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                conn.Close();
            }

            return isFound;
        }


    }
}
