using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Data
{
    public static class DAL_Users
    {
        public static bool Find(int ID,ref int PersonID,ref string UserName,ref string Password ,ref bool IsActive)
        {
            bool isFound = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from users where UserID = @UserID";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@UserID", ID);

            try
            {
                conn.Open();
                SqlDataReader R = cmd.ExecuteReader();
                if (R.Read())
                {
                    isFound = true;
                    PersonID = Convert.ToInt32(R["PersonID"]);
                    UserName = (string)R["UserName"];
                    Password = (string)R["Password"];
                    IsActive = (bool)R["IsActive"];
                }
                else
                    isFound = false;
                R.Close();
            }
            catch (Exception ex)
            {
                isFound=false;
                throw;
            }
            finally
            {
                conn.Close();
            }

            return isFound;
        }

        public static bool Find(string UserName,string Password,ref int UserID,ref int PersonID,ref bool IsActive)
        {
            bool isFound = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from users where UserName = @UserName and Password = @Password";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@UserName", UserName);
            cmd.Parameters.AddWithValue("@Password", Password);

            try
            {
                conn.Open();
                SqlDataReader R = cmd.ExecuteReader();
                if (R.Read())
                {
                    isFound = true;
                    UserID = Convert.ToInt32(R["UserID"]);
                    PersonID = Convert.ToInt32(R["PersonID"]);
                    IsActive = (bool)R["IsActive"];
                }
                else
                    isFound = false;
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

        public static bool Find(int PersonID,ref string UserName, ref string Password, ref bool IsActive,ref int UserID)
        {
            bool isFound = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from users where PersonID = @PersonID";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                conn.Open();
                SqlDataReader R = cmd.ExecuteReader();
                if (R.Read())
                {
                    isFound = true;
                    UserID = Convert.ToInt32(R["UserID"]);
                    UserName = (string)R["UserName"];
                    Password = (string)R["Password"];
                    IsActive = (bool)R["IsActive"];
                }
                else
                    isFound = false;
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

        public static DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query =@"select Users.UserID,Users.PersonID,
                            People.FirstName + ' ' + People.SecondName + ' ' + People.ThirdName 
                            + ' ' + People.LastName as FullName,
                            Users.UserName,Users.Password,Users.IsActive from users
                            inner join People on People.PersonID = Users.PersonID";

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
                throw;
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public static int AddNewUser(int PersonID,string UserName,string Password,bool IsActive)
        {
            int UserID = -1;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Insert into users
                            Values(@PersonID,@UserName,@Password,@IsActive);
                            Select SCOPE_IDENTITY()";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@PersonID", PersonID);
            cmd.Parameters.AddWithValue("@UserName", UserName);
            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.Parameters.AddWithValue("@IsActive", IsActive);

            try
            {
                conn.Open();
                object R = cmd.ExecuteScalar();
                if (R != null && int.TryParse(R.ToString(), out int InsertedID))
                {
                    UserID = InsertedID;
                }
                else
                    UserID = -1;
            }
            catch (Exception ex)
            {
                UserID = -1;
                throw;
            }
            finally
            {
                conn.Close();
            }
            return UserID;
        }

        public static bool UpdateUser(int UserID,int PersonID, string UserName, string Password, bool IsActive)
        {
            int RowsAffected = 0;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"UPDATE [dbo].[Users]
                              SET [PersonID] = @PersonID
                                 ,[UserName] = @UserName
                                 ,[Password] = @Password
                                 ,[IsActive] = @IsActive
                            WHERE UserID = @UserID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@PersonID", PersonID);
            cmd.Parameters.AddWithValue("@UserName", UserName);
            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.Parameters.AddWithValue("@IsActive", IsActive);
            cmd.Parameters.AddWithValue("@UserID", UserID);

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

            return (RowsAffected > 0);
        }

        public static bool DeleteUser(int UserID)
        {
            int RowsAffected = 0;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"delete from users
                            where UserID = @UserID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@UserID", UserID);

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

            return (RowsAffected > 0);
        }

        public static bool IsUserExist(int ID)
        {
            bool isFound = false;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "select Found=1 from Users where UserID = @UserID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@UserID", ID);
            try
            {
                conn.Open();
                SqlDataReader R = cmd.ExecuteReader();
                if(R.HasRows)
                    isFound = true;
                else
                    isFound = false;
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

        public static bool IsUserExist(string UserName,string Password)
        {
            bool isFound = false;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "select Found=1 from Users where UserName = @UserName and Password = @Password";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@UserName", UserName);
            cmd.Parameters.AddWithValue("@password", Password);

            try
            {
                conn.Open();
                SqlDataReader R = cmd.ExecuteReader();
                if (R.HasRows)
                    isFound = true;
                else
                    isFound = false;
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

    }
}
