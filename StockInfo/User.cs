using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace StockInfo
{
    public class User
    {
        public string username { get; set; }
        private string password;

        public string Pword
        {
            get { return password; }
            set { password = value; }
        }
        //Använd denna ConnectionString till alla Metoder
        string ConnectionString =
                ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

        //Registrera användare
        public string AddUser(string Username, string Password)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("insert into dbo.Users(username, password) values('" + Username + "','" + Password + "')", conn);
            

            try
            {
                cmd.ExecuteNonQuery();
                conn.Close();
                return "Success!";
            }
            catch(Exception ex) 
            {
                return ex.Message;
            }
        }

        
        //Logga in användare
        public string LoginUser(string UserName, string Password)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();

            string checkUser = "select count(*) from dbo.Users where username='" + UserName + "'";
            SqlCommand com = new SqlCommand(checkUser, conn);

            int temp = Convert.ToInt32(com.ExecuteScalar().ToString());
            conn.Close();

            if (temp == 1)
            {
                conn.Open();
                string checkPwQuery = "select password from dbo.Users where username='" + UserName + "'";
                SqlCommand passComm = new SqlCommand(checkPwQuery, conn);
                string password = passComm.ExecuteScalar().ToString();
                if (password == Password)
                {
                    return UserName;
                }
                else
                {
                    return "Password incorrect!";
                }
            }
            else
            {
                return "Username incorrect!";
            }
        }

        //Hämta användar ID efter inloggning och returnera IDt till en Session
        public string GetUserId(string Username)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            
            conn.Open();


            string sql = "SELECT user_id FROM Users WHERE username='" + Username + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            reader.Read();

            string UserId = reader["user_id"].ToString();

            reader.Close();

            return UserId;
        }

        //Uppdatera Användarnamn och/eller lösenord
        public string UpdateUser(string Username, string Password, int UserId)
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string sql = "UPDATE dbo.Users Set Username=@Username, Password=@Password WHERE user_id=@user_id";

                    using(SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", Username);
                        cmd.Parameters.AddWithValue("@Password", Password);
                        cmd.Parameters.AddWithValue("@user_id", UserId);

                        cmd.ExecuteNonQuery();
                    }
                    return "Successfully updated";
                }
            }
            catch (Exception ex)
            {
                // Log the error and display a user-friendly message
                return "An error occurred: " + ex.Message;
            }
        }
    }
}