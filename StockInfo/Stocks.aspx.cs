using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace StockInfo
{
    public partial class Stocks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Importerade alla aktie-tickers till databas från fil
            //string[] lines = File.ReadAllLines("C:\\Users\\nilss\\source\\repos\\StockInfo\\StockInfo\\ticker.txt");
            //List<string> tickers = new List<string>();
            //foreach (string line in lines) 
            //{
            //    string[] values = line.Split('\t');
            //    tickers.Add(values[0]);
            //}
            //
            //Stock stock = new Stock();
            //stock.ImportStockTickers(tickers);
            //string response = "Success!";

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
        public static List<Stock> GetStockTickers(string search)
        {
            Stock stock = new Stock();
            //GetTickers metod i Stock.cs
            return stock.GetTickers(search);
        }
    }
}