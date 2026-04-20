using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Data
{
    public static class DAL_ApplicationTypes
    {
        public static DataTable GetApplicationTypes()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand cmd = new SqlCommand("select * from applicationtypes", conn);

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
        
        public static bool Find(int ID,ref string Title,ref float Fees)
        {
            bool isFound = false;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "select * from applicationTypes where ApplicationTypeID = @ApplicationTypeID";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ApplicationTypeID", ID);

            try
            {
                conn.Open();
                SqlDataReader R = cmd.ExecuteReader();
                if (R.Read())
                {
                    isFound = true;
                    Title = (string)R["ApplicationTypeTitle"];
                    Fees = Convert.ToSingle(R["ApplicationFees"]);
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
        
        public static bool UpdateApplicationType(int ID,string Title,float Fees)
        {
            int RowsAffected = 0;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"UPDATE [dbo].[ApplicationTypes]
                              SET [ApplicationTypeTitle] = @Title
                                 ,[ApplicationFees] = @Fees
                            WHERE ApplicationTypeID = @ID";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@Title", Title);
            cmd.Parameters.AddWithValue("@Fees", Fees);
            cmd.Parameters.AddWithValue("@ID", ID);

            try
            {
                conn.Open();
                RowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return (RowsAffected > 0);
        }

    }
}
