using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StockInfo
{
    public partial class ArticleList : System.Web.UI.Page
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

        [WebMethod]
        public static List<Article> GetArticles()
        {
            Article article = new Article();
            //GetArticles() metod i Article.cs
            return article.GetArticlesFromDB();
        }

        [WebMethod]
        public static string DeleteArticle(int id)
        {
           Article article = new Article();
            string response = article.Delete(id);
            return response;
        }


    }
}