
//"Livesöknings" funktion
$(document).ready(function () {
    $("#txtSearch").on("keyup", function () {
        var query = $(this).val();
        if (query.length > 1) { 
            $.ajax({
                //Anropa backend WebMethod GetStockTickers()
                url: "Stocks.aspx/GetStockTickers",
                method: "POST",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ search: query }),
                dataType: "json",
                success: function (response) {
                    var results = response.d;
                    //Rensa htlm & list-group före hämtning
                    var html = "";
                    $(".list-group").empty();
                    var stockTwits = "https://stocktwits.com/symbol/";
                    var yahoo = "https://finance.yahoo.com/quote/";
                    //Lista sökresultat samt hänvisa till aktien i Stocktwits/Yahoo
                    results.forEach(function (tickers, i) {

                        html += "<a id='aTicker" + i + "' href='#' class='list-group-item list-group-item-action'>" + tickers.ticker + "  " +
                            "<object><a id='stUrl" + i + "' target='_blank' href='" + stockTwits + tickers.ticker + "' <i class='bi bi-1-square'></i></a></object>" + "  " +
                            "<object><a id='yUrl" + i + "' target='_blank' href='" + yahoo + tickers.ticker + "/" + "' <i class='bi bi-2-square'></i></a></object>" + "  " +
                            "</a>";
                        
                    });
                    $(".list-group").append(html);
                },
                error: function (xhr, status, error) {
                    console.error(error);
                }
            });
        } else {
            console.log("error");
        }
    });
});


