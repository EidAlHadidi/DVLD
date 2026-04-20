using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Data
{
    public static class DAL_TestTypes
    {
        public static DataTable GetTestTypes()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand cmd = new SqlCommand("select * from TestTypes", conn);

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

        public static bool Find(int ID, ref string TestTypeTitle, ref string TestTypeDescription, ref float TestTypeFees)
        {
            bool isFound = false;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "select * from TestTypes where TestTypeID = @TestTypeID";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@TestTypeID", ID);

            try
            {
                conn.Open();
                SqlDataReader R = cmd.ExecuteReader();
                if (R.Read())
                {
                    isFound = true;
                    TestTypeTitle = (string)R["TestTypeTitle"];
                    TestTypeDescription = (string)R["TestTypeDescription"];
                    TestTypeFees = Convert.ToSingle(R["TestTypeFees"]);
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

        public static bool UpdateApplicationType(int ID, string Title, string Description,float Fees)
        {
            int RowsAffected = 0;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"UPDATE [dbo].[TestTypes]
                               SET [TestTypeTitle] = @Title
                                  ,[TestTypeDescription] = @Description
                                  ,[TestTypeFees] = @Fees
                             WHERE TestTypeID = @ID";
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@Title", Title);
            cmd.Parameters.AddWithValue("@Description", Description);
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
