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
    public static class DAL_LocalDrivingLicenseApplications
    {
        public static DataTable GetAllApplications()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "select * from LocalDrivingLicenseApplications";
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
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public static DataTable GetAllApplications_FullView()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "select * from LocalDrivingLicenseApplications_View";
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

            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public static DataTable GetAllApplications_View()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "select * from LocalDrivingLicenseApplications_View";
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

            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public static bool Find(int ID,ref int ApplicationID,ref int LicenseClass)
        {
            bool IsFound = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from LocalDrivingLicenseApplications where LocalDrivingLicenseApplicationID = @ID";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@ID", ID);

            try
            {
                conn.Open();
                SqlDataReader R = cmd.ExecuteReader();
                if (R.Read())
                {
                    IsFound = true;
                    ApplicationID = Convert.ToInt32(R["ApplicationID"]);
                    LicenseClass = Convert.ToInt32(R["LicenseClassID"]);
                }
                else
                    IsFound = false;
                R.Close();

            }
            catch (Exception)
            {
                IsFound = false;
            }
            finally
            {
                conn.Close();
            }

            return IsFound;
        }
        
        public static bool Find(int PersonID,int ApplicationStatus,int LicenseClassID,
            ref int ApplicationID,ref int LocalApplicationID)
        {
            bool isFound = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from LocalDrivingLicenseFullApplications_View
                            where ApplicantPersonID = @ApplicantPersonID and ApplicationStatus = @ApplicationStatus
                            and LicenseClassID = @LicenseClassID
                            ";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
            cmd.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            cmd.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                conn.Open();
                SqlDataReader R = cmd.ExecuteReader();
                if(R.Read())
                {
                    isFound = true;
                    ApplicationID = Convert.ToInt32(R["ApplicationID"]);
                    LocalApplicationID = Convert.ToInt32(R["LocalDrivingLicenseApplicationID"]);
                }
                R.Close();
            }
            catch (Exception ex)
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

       
        public static int AddNewLocalApplication(int ApplicationID,int LicenseClass)
        {
            int LocalID = -1;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"INSERT INTO [dbo].[LocalDrivingLicenseApplications]
                              ([ApplicationID]
                              ,[LicenseClassID])
                        VALUES
                              (@ApplicationID,@LicenseClassID);
                          SELECT SCOPE_IDENTITY();";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            cmd.Parameters.AddWithValue("@LicenseClassID", LicenseClass);
            try
            {
                conn.Open();
                object R = cmd.ExecuteScalar();
                if(R!=null && int.TryParse(R.ToString(),out int InsertedID))
                {
                    LocalID = InsertedID;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return LocalID;
        }

        public static bool DeleteLocalApplication(int ID)
        {
            int RowsAffected = 0;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"delete from LocalDrivingLicenseApplications where LocalDrivingLicenseApplicationID = @ID";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@ID", ID);

            try
            {
                conn.Open();

                RowsAffected = cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {

            }
            finally
            {
                conn.Close();
            }

            return (RowsAffected > 0);
        }

        public static bool IsLocalDrivingLicenseApplicationExist(int LDLApplicationID)
        {
            bool isFound = false;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select Found=1 from LocalDrivingLicenseApplications
                        where LocalDrivingLicenseApplicationID = @ID
                        ";
            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("ID", LDLApplicationID);

            try
            {
                conn.Open();
                SqlDataReader R = cmd.ExecuteReader();
                if(R.HasRows)
                {
                    isFound = true;
                }
                R.Close();
            }
            catch (Exception)
            {
                isFound = false;
            }
            finally
            {
                conn.Close();
            }

            return isFound;
        }

        public static bool IsLocalDrivingLicenseApplicationExist(int PersonID,int ApplicationTypeID,
            int Status)
        {
            bool isFound = false;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select found=1 from LocalDrivingLicenseApplications inner join Applications
                            on Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                            where ApplicantPersonID = @PersonID and ApplicationTypeID 
                            = @ApplicationTypeID and ApplicationStatus = @Status";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@PersonID", PersonID);
            cmd.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            cmd.Parameters.AddWithValue("@Status", Status);

            try
            {
                conn.Open();
                SqlDataReader R = cmd.ExecuteReader();
                if(R.HasRows)
                {
                    isFound = true;
                }
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

        public static bool UpdateLocalDrivingLicenseApplication(int ApplicationID
            , int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID
            , int ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID, int LicenseClassID
            , int LocalDrivingLicenseApplicationID)
        {
            int RowsAffected = 0;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE [dbo].[Applications]
                           SET [ApplicantPersonID] = @ApplicantPersonID
                              ,[ApplicationDate]   = @ApplicationDate
                              ,[ApplicationTypeID] = @ApplicationTypeID
                              ,[ApplicationStatus] = @ApplicationStatus
                              ,[LastStatusDate]    = @LastStatusDate
                              ,[PaidFees]          = @PaidFees
                              ,[CreatedByUserID]   = @CreatedByUserID
                         WHERE ApplicationID = @ApplicationID;

                        UPDATE [dbo].[LocalDrivingLicenseApplications]
                           SET [ApplicationID] = @ApplicationID
                              ,[LicenseClassID] = @LicenseClassID
                         WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                         ";

            //string query = @"UPDATE [dbo].[LocalDrivingLicenseFullApplications_View]
            //   SET [ApplicationID]     =  @ApplicationID
            //      ,[ApplicantPersonID] = @ApplicantPersonID
            //      ,[ApplicationDate]   = @ApplicationDate
            //      ,[ApplicationTypeID] = @ApplicationTypeID
            //      ,[ApplicationStatus] = @ApplicationStatus
            //      ,[LastStatusDate]    = @LastStatusDate
            //      ,[PaidFees]          = @PaidFees
            //      ,[CreatedByUserID]   = @CreatedByUserID
            //      ,[LicenseClassID]    = @LicenseClassID
            // WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            cmd.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            cmd.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            cmd.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            cmd.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            cmd.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            cmd.Parameters.AddWithValue("@PaidFees", PaidFees);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            cmd.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                conn.Open();
                RowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return RowsAffected > 0;
        }

        //This method is for counting number of passed tests for an applicant

        public static int GetPassedTests(int LocalDrivingLicenseApplicationID)
        {
            int PassedTests = -1;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT       
                            COUNT(dbo.TestAppointments.TestTypeID) AS PassedTestCount
                            FROM            dbo.Tests 
                            INNER JOIN
                            dbo.TestAppointments ON dbo.Tests.TestAppointmentID = dbo.TestAppointments.TestAppointmentID
                            WHERE(dbo.TestAppointments.LocalDrivingLicenseApplicationID 
                            = @ID) AND (dbo.Tests.TestResult = 1)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ID", LocalDrivingLicenseApplicationID);
            try
            {
                conn.Open();
                object R = cmd.ExecuteScalar();
                if (R != null && int.TryParse(R.ToString(),out int TestsCount))
                {
                    PassedTests = TestsCount;
                }
            }
            catch (Exception)
            {
                PassedTests = -1;
                throw;
            }
            finally
            {
                conn.Close();
            }

            return PassedTests;
        }

        //This method is Find operation for LocalDrivingLicenseApplication_View

        public static bool Find(int ID, ref string ClassName, ref string NatioanlNo, ref string FullName
           , ref DateTime ApplicationDate, ref int PassedTestCount, ref string Status)
        {
            bool isFound = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select * from LocalDrivingLicenseApplications_View where 
                           LocalDrivingLicenseApplicationID = @ID;";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ID", ID);
            try
            {
                conn.Open();
                SqlDataReader R = cmd.ExecuteReader();
                if (R.Read())
                {
                    isFound = true;
                    ClassName = (string)R["ClassName"];
                    NatioanlNo = (string)R["NationalNo"];
                    FullName = (string)R["FullName"];
                    ApplicationDate = (DateTime)R["ApplicationDate"];
                    PassedTestCount = Convert.ToInt32(R["PassedTestCount"]);
                    Status = (string)R["Status"];
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
