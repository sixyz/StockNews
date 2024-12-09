using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Configuration;
using System.Web.Script.Serialization;

namespace StockInfo
{
    public class Article
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string DatePublished { get; set; }
        public string Authors { get; set; }
        public string Summary { get; set; }
        public string Source { get; set; }
        public string Sentiment { get; set; }

        string ConnectionString =
                ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

        public void AddArticle(string userId, string title, string url, string datePublished, string authors, string summary, string source, string sentiment)
        {
            int uId = Convert.ToInt32(userId);

            // Define the query for insertion
            string query = "INSERT INTO dbo.Articles (user_id, title, url, date_published, authors, summary, source, sentiment) VALUES (@User_id, @Title, @Url, @Date_published, @Authors, @Summary, @Source, @Sentiment)";

            // Create a connection
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                // Open the connection
                conn.Open();

                // Create a command to execute the query
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    // Add parameters to avoid SQL injection
                    command.Parameters.AddWithValue("@User_id", uId);
                    command.Parameters.AddWithValue("@Title", title);
                    command.Parameters.AddWithValue("@Url", url);
                    command.Parameters.AddWithValue("@Date_published", datePublished);
                    command.Parameters.AddWithValue("@Authors", authors);
                    command.Parameters.AddWithValue("@Summary", summary);
                    command.Parameters.AddWithValue("@Source", source);
                    command.Parameters.AddWithValue("@Sentiment", sentiment);

                    // Execute the query
                    command.ExecuteNonQuery();
                }

            }
        }



        public string Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    // Define the SQL DELETE command
                    string query = "DELETE FROM dbo.Articles WHERE article_id = @article_id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add the parameter for the ID
                        command.Parameters.AddWithValue("@article_id", id);

                        // Execute the command
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return "Record deleted successfully!";
                        }
                        else
                        {
                            return "No record found with the specified ID.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    return "Error: " + ex.Message;
                }
            }
        }




        public List<Article> GetArticlesFromDB()
        {
            List<Article> articles = new List<Article>();

            string userId = HttpContext.Current.Session["UserId"].ToString();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "SELECT article_id, title, url, date_published, authors, summary, source, sentiment FROM dbo.Articles WHERE user_id='" + userId + "'";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        articles.Add(new Article
                        {
                            ArticleId = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Url = reader.GetString(2),
                            DatePublished = reader.GetString(3), 
                            Authors = reader.GetString(4),
                            Summary = reader.GetString(5),
                            Source = reader.GetString(6), 
                            Sentiment = reader.GetString(7)
                        });
                    }
                }
            }

            return articles;
        }
    }
}
    