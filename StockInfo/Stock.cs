using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace StockInfo
{
    public class Stock
    {
        public string ticker { get; set; }
        public string StocktwitsUrl { get; set; }
        public string YahooUrl { get; set; }




        string ConnectionString =
                ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;


        public void ImportStockTickers(List<string> tickers)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            { 
                conn.Open();

                string query = "INSERT INTO dbo.Stocks (ticker) VALUES (@ticker)";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    // Add parameter to command
                    command.Parameters.Add(new SqlParameter("@ticker", System.Data.SqlDbType.NVarChar, 50));

                    foreach (string ticker in tickers)
                    {
                        // Set parameter value
                        command.Parameters["@ticker"].Value = ticker;

                        // Execute the query
                        command.ExecuteNonQuery();
                    }
                }
            }
        }


        public List<Stock> GetTickers(string search)
        {
            List<Stock> tickers = new List<Stock>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "SELECT ticker FROM dbo.Stocks WHERE ticker LIKE @Search + '%'";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@Search", search);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tickers.Add(new Stock
                            {
                                ticker = reader.GetString(0)
                            });
                        }
                    }
                }
            }
            return tickers;
        }
        

    }



}