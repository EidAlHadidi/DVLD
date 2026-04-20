using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Data
{
    public static class DAL_People
    {
        public static DataTable GetAllPeople()
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"
                            select People.PersonID,People.NationalNo,People.FirstName,People.SecondName,People.ThirdName,People.LastName,People.DateOfBirth,People.Gendor,Gender = 
                            case People.gendor
                            when 0 then 'Male'
                            when 1 then 'Female' 
                            end 
                            ,People.Address,People.Phone,People.Email,People.NationalityCountryID,Countries.CountryName,People.ImagePath 
                            from people inner join Countries on People.NationalityCountryID = Countries.CountryID;";

            SqlCommand cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
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
        public static bool Find(int ID,ref string NationaNo,ref string FirstName,ref string SecondName,ref string ThirdName,
            ref string LastName,ref DateTime DateOfBirth,ref int Gendor,ref string Address,ref string Phone,
            ref string Email,ref int NationalityCountryID,ref string ImagePath)
        {
            bool IsFound = false;

            //ThirdName, Email and ImagePath are nullable in database.
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from people where PersonID = @PersonID";
            
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@PersonID", ID);
            try
            {
                conn.Open();

                SqlDataReader R = cmd.ExecuteReader();
                if (R.Read())
                {
                    IsFound = true;

                    NationaNo = (string)R["NationalNo"];
                    FirstName = (string)R["FirstName"];
                    SecondName = (string)R["SecondName"];
                    ThirdName = (R["ThirdName"] != DBNull.Value) ? (string)R["ThirdName"] : string.Empty;
                    LastName = (string)R["LastName"];
                    DateOfBirth = (DateTime)R["DateOfBirth"];
                    Gendor = Convert.ToInt32(R["Gendor"]);
                    Address = (string)R["Address"];
                    Phone = (string)R["Phone"];
                    Email = (R["Email"] != DBNull.Value) ? (string)R["Email"] : string.Empty;
                    NationalityCountryID = Convert.ToInt32(R["NationalityCountryID"]);
                    ImagePath = (R["ImagePath"] != DBNull.Value) ? (string)R["ImagePath"] : string.Empty;
                }
                else
				{
                    EventLog.WriteEntry("DVLD", $"Error while Finding person with ID {ID}", EventLogEntryType.Error);
                    IsFound = false;
                }
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

        public static bool Find(string NationalNo,ref int ID, ref string FirstName, ref string SecondName, ref string ThirdName,
            ref string LastName, ref DateTime DateOfBirth, ref int Gendor, ref string Address, ref string Phone,
            ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            bool IsFound = false;

            //ThirdName, Email and ImagePath are nullable in database.
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from people where NationalNo = @NationalNo";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                conn.Open();

                SqlDataReader R = cmd.ExecuteReader();
                if (R.Read())
                {
                    IsFound = true;

                    ID = Convert.ToInt32(R["PersonID"]);
                    FirstName = (string)R["FirstName"];
                    SecondName = (string)R["SecondName"];
                    ThirdName = (R["ThirdName"] != DBNull.Value) ? (string)R["ThirdName"] : string.Empty;
                    LastName = (string)R["LastName"];
                    DateOfBirth = (DateTime)R["DateOfBirth"];
                    Gendor = Convert.ToInt32(R["Gendor"]);
                    Address = (string)R["Address"];
                    Phone = (string)R["Phone"];
                    Email = (R["Email"] != DBNull.Value) ? (string)R["Email"] : string.Empty;
                    NationalityCountryID = Convert.ToInt32(R["NationalityCountryID"]);
                    ImagePath = (R["ImagePath"] != DBNull.Value) ? (string)R["ImagePath"] : string.Empty;
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

        public static int AddNewPerson(string NationalNo,string FirstName,
            string SecondName,string ThirdName,string LastName,DateTime DateOfBirth,int Gendor,string Address,
            string Phone,string Email,int NationalityCountryID,string ImagePath)
        {
            int PersonID = -1;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query =
                    @"
                        INSERT INTO [dbo].[People]
           ([NationalNo]
           ,[FirstName]
           ,[SecondName]
           ,[ThirdName]
           ,[LastName]
           ,[DateOfBirth]
           ,[Gendor]
           ,[Address]
           ,[Phone]
           ,[Email]
           ,[NationalityCountryID]
           ,[ImagePath])
            VALUES
            (
                @NationalNo,@FirstName,@SecondName,@ThirdName,@LastName,
                @DateOfBirth,@Gendor,@Address,@Phone,@Email,@NationalityCountryID,@ImagePath           
		    );
            select SCOPE_IDENTITY();
                     ";
            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@NationalNo", NationalNo);
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@SecondName", SecondName);

            if (string.IsNullOrEmpty(ThirdName))
                cmd.Parameters.AddWithValue("@ThirdName", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@ThirdName", ThirdName);
            
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            cmd.Parameters.AddWithValue("@Gendor", Gendor);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@Phone", Phone);

            if (string.IsNullOrEmpty(Email))
                cmd.Parameters.AddWithValue("@Email", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if (string.IsNullOrEmpty(ImagePath))
                cmd.Parameters.AddWithValue("@ImagePath", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@ImagePath", ImagePath); 

            try
            {
                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                {
                    PersonID = InsertedID;
                }
                else
                    PersonID = -1;
            }
            catch (Exception ex)
            {
                PersonID = -1;
            }
            finally
            {
                conn.Close();
            }


            return (PersonID);
        }

        public static bool UpdatePerson(int ID,string NationalNo, string FirstName,
            string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, int Gendor, string Address,
            string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            int RowsAffected = 0;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE [dbo].[People]
                           SET [NationalNo]           = @NationalNo
                              ,[FirstName]            = @FirstName
                              ,[SecondName]           = @SecondName
                              ,[ThirdName]            = @ThirdName
                              ,[LastName]             = @LastName
                              ,[DateOfBirth]          = @DateOfBirth
                              ,[Gendor]               = @Gendor
                              ,[Address]              = @Address
                              ,[Phone]                = @Phone
                              ,[Email]                = @Email
                              ,[NationalityCountryID] = @NationalityCountryID
                              ,[ImagePath]            = @ImagePath
                         WHERE PersonID = @PersonID;";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@PersonID", ID);
            cmd.Parameters.AddWithValue("@NationalNo", NationalNo);
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@SecondName", SecondName);

            if (string.IsNullOrEmpty(ThirdName))
                cmd.Parameters.AddWithValue("@ThirdName", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@ThirdName", ThirdName);

            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            cmd.Parameters.AddWithValue("@Gendor", Gendor);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@Phone", Phone);

            if (string.IsNullOrEmpty(Email))
                cmd.Parameters.AddWithValue("@Email", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if (string.IsNullOrEmpty(ImagePath))
                cmd.Parameters.AddWithValue("@ImagePath", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@ImagePath", ImagePath);

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

        public static bool DeletePerson(int ID)
        {
            int Rowsaffected = 0;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "delete from people where PersonID = @PersonID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@PersonID", ID);
            try
            {
                conn.Open();
                Rowsaffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conn.Close();
            }
            return (Rowsaffected > 0);
        }

        public static bool IsPersonExist(int PersonID)
        {
            bool isFound = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "select Found=1 from people where PersonID = @PersonID";
            SqlCommand cmd = new SqlCommand(query,conn);
            cmd.Parameters.AddWithValue("PersonID", PersonID);
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
            }
            finally
            {
                conn.Close();
            }
            return isFound;
        }

        public static bool IsPersonExist(string NationalNo)
        {
            bool isFound = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "select Found=1 from people where NationalNo = @NationalNo";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@NationalNo", NationalNo);
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
            }
            finally
            {
                conn.Close();
            }
            return isFound;
        }


    }
}
