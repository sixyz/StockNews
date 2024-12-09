using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StockInfo
{
    public partial class Account : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Hämta kund id med hjälp av användarnamnet
            if (Session["User"] != null)
            {
                User usr = new User();
                //GetUserId() metod i User.cs
                string UserId = usr.GetUserId(Session["User"].ToString());
                Session["UserId"] = UserId;
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            User usr = new User();

            int UserId = Convert.ToInt32(Session["UserId"]);

            //Metod UpdateUser i User.cs
            string response = usr.UpdateUser(txtUsername.Text, txtPassword.Text, UserId);

            if(response.Contains("An error occurred:"))
            {
                lblUpdate.Text = response;
            }
            else
            {
                lblUpdate.Visible = true;
            }
        }
    }
}