using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StockInfo
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegisterUser_Click(object sender, EventArgs e)
        {
            string Username = txtUsername.Text;
            string Password = txtPassword.Text;

            if (Username.Length < 6 || Username.Length > 12)
            {
                Response.Write("Username should have between 6 and 12 characters");
                return;
            }
            if (Password.Length < 6 || Password.Length > 12)
            {
                Response.Write("Password should have between 6 and 12 characters");
                return;
            }
            if (!Password.Any(char.IsUpper))
            {
                Response.Write("The password need to contain atleast one upper case");
                return;
            }
            if (Password.Contains(" "))
            {
                Response.Write("No white space allowed");
                return;
            }
            else
            {
                User usr = new User();
                //Metod AddUser() i User.cs
                string response = usr.AddUser(Username, Password);
                
                if(response == "Success!")
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    Response.Write(response);
                }
                
            }
        }
    }
}