using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StockInfo
{
    public partial class Newsfeed : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                User usr = new User();
           
                string UserId = usr.GetUserId(Session["User"].ToString());
                Session["UserId"] = UserId;
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }


        [WebMethod]
        public static string SaveArticle(Article data)
        {
            string userId = HttpContext.Current.Session["UserId"].ToString();
            string title = data.Title;
            string url = data.Url;
            string datePublished = data.DatePublished;
            string authors = data.Authors;
            string summary = data.Summary;
            string source = data.Source;
            string sentiment = data.Sentiment;

            Article article = new Article();
            article.AddArticle(userId, title, url, datePublished, authors, summary, source, sentiment);

            return "Article has been saved!";
            //return "The following data will be saved to the database " + title + "," + url + "," + datePublished + "," + authors + "," + summary + "," + source + "," + sentiment;
        }


    }
        

    }
