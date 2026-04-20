using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Data
{
    public static class DAL_LicenseClasses
    {
        public static DataTable GetAllLicenseClasses()
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand cmd = new SqlCommand("select * from LicenseClasses", conn);

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

        public static bool Find(int LicenseClassID,ref string ClassName,ref string ClassDescription
            ,ref int MinimumAllowedAge,ref int DefaultValidityLength,ref float ClassFees)
        {
            bool isFound = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from LicenseClasses where LicenseClassID = @ID";
            
            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("ID", LicenseClassID);


            try
            {
                conn.Open();
                SqlDataReader R = cmd.ExecuteReader();
                if(R.Read())
                {
                    isFound = true;
                    ClassName = (string)R["ClassName"];
                    ClassDescription = (string)R["ClassDescription"];
                    MinimumAllowedAge = Convert.ToInt32(R["MinimumAllowedAge"]);
                    DefaultValidityLength = Convert.ToInt32(R["DefaultValidityLength"]);
                    ClassFees = Convert.ToSingle(R["ClassFees"]);
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

    }
}
