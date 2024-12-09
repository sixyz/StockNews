
$(document).ready(function () {

    const apiKey = 'KEY';  //Alpha Vantage API nyckel
    const symbol = 'NVDA'; //Sök efter nyheter om aktien Nvidia 
    const url = `https://www.alphavantage.co/query?function=NEWS_SENTIMENT&tickers=${symbol}&apikey=${apiKey}`;

    // Fetch news data from Alpha Vantage API
    fetch(url)
        .then(response => response.json())
        .then(data => {
            if (data['feed']) {
                const feed = data['feed'];
                var count = feed.length;
                //Presentera data i bootstrap Accordion med dropdown/collapse funktion
                for (var i = 0; i < count; i++) {
                    $("#accordionData").append(
                        "<div class='accordion-item'>" +
                        "<h2 class='accordion-header' id='heading" + [i] + "'>" +
                        "<button class='accordion-button collapsed' id='btnTitle" + [i] + "' type = 'button' data-bs-toggle='collapse' data-bs-target='#collapse" + [i] + "' aria - expanded='false' aria - controls='collapse" + [i] + "' > " + feed[i].title + "'</button>" +
                        "</button>" +
                        "</h2>" +
                        "<div id='collapse" + [i] + "' class='accordion-collapse collapse' aria-labelledby='heading" + [i] + " data-bs-parent='#accordionExample'>" +
                        "<div class='accordion-body'>" +
                        "<strong id='strongUrl" + [i] + "'> " +
                        feed[i].url +
                        "</strong>" +
                        "<p id='pDatePublished" + [i] + "'>" +
                        feed[i].time_published +
                        "</p>" +
                        "<p id='pAuthors" + [i] + "'> " +
                        feed[i].authors +
                        "</p>" +
                        "<p id='pSummary" + [i] + "'>" +
                        feed[i].summary +
                        "</p>" +
                        "<p id='pSource" + [i] + "'> " +
                        feed[i].source +
                        "</p>" +
                        "<p id='pSentiment" + [i] + "'> " +
                        feed[i].overall_sentiment_label +
                        "</p>" +
                        "<i class='bi bi-save' id='btnSave" + [i] + "' onClick='SaveArticle(this.id)'></i >" +
                        "</div>" +
                        "</div>" +
                        "</div>");
                }

            } else {
                document.getElementById('accordionData').innerHTML = 'No data available';
            }
        })
        .catch(error => {
            document.getElementById('accordionData').innerHTML = 'Error fetching';
        });
});

//Spara artikel data i databas
function SaveArticle(clicked_id) {

    clicked_id = clicked_id.replace("btnSave", "");

    var title = document.getElementById("btnTitle" + clicked_id).innerHTML;

    var url = document.getElementById("strongUrl" + clicked_id).innerHTML;

    var datePublished = document.getElementById("pDatePublished" + clicked_id).innerHTML;

    var authors = document.getElementById("pAuthors" + clicked_id).innerHTML;

    var summary = document.getElementById("pSummary" + clicked_id).innerHTML;

    var source = document.getElementById("pSource" + clicked_id).innerHTML;

    var sentiment = document.getElementById("pSentiment" + clicked_id).innerHTML;



    var data = {
        Title: title,
        Url: url,
        DatePublished: datePublished,
        Authors: authors,
        Summary: summary,
        Source: source,
        Sentiment: sentiment
    };

    //Anropa WebMethod SaveArticle()
    $.ajax({
        url: "/NewsFeed.aspx/SaveArticle",         
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({ data: data }), 
        success: function (response) {
            console.log("Response from server:", response.d); 
            document.getElementById("btnSave" + clicked_id).className = "bi bi-save-fill";
        },
        error: function (xhr, status, error) {
            console.error("Error:", error);
        }
    });
}