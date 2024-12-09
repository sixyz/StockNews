using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StockInfo
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            User usr = new User();

            //Metod LoginUser() i User.cs
            string userName = usr.LoginUser(txtUsername.Text, txtPassword.Text);

            if (userName == "Password incorrect!" || userName == "Username incorrect!")
            {
                Response.Write("Username and/or password is incorrect!");
            }
            else
            {
                Session["User"] = userName;
                Response.Redirect("Account.aspx");
            }
        }
    }
}