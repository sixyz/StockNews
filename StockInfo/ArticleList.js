
var articleIdList = [];

$(document).ready(function () {

    GetArticles();
});


function GetArticles() {
    // Anropa backend WebMethod GetArticles()
    $.ajax({
        type: "POST", 
        url: "ArticleList.aspx/GetArticles", 
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            var article = response.d;
            var count = article.length;
            var artId;

            $("#tbodyData").empty();

            for (var i = 0; i < count; i++) {

                var artId = article[i].ArticleId;
                articleIdList.push(artId);

                var originalDate = article[i].DatePublished;
                var rep = originalDate.replace("T", "");
                var date = rep.substring(0, rep.length - 2);
                var newDate = convertToJSDate(date);

                
                //Presentera Data i bootstrap table
                $("#tbodyData").append(
                    "<tr id='trData" + [i] + "'>" +
                    "<td>" + article[i].Title + "</td >" +
                    "<td><a target='_blank' href='" + article[i].Url + "'><i class='bi bi-arrow-right-circle-fill' style='font-size: 25px;'></i></a></td >" +
                    "<td>" + newDate + "</td>" +
                    "<td>" + article[i].Authors + "</td>" +
                    "<td>" + article[i].Summary + "</td>" +
                    "<td>" + article[i].Source + "</td>" +
                    "<td>" + article[i].Sentiment + "  " + "<i class='bi bi-x-lg' id='btnRemove" + [i] + "' onClick='RemoveArticle(this.id)' style='font-size: 25px';></i></td>" +
                    "</tr>"
                );
            }
        },
        error: function (error) {
            console.error("Error occurred:", error);
        }
    });
}


function RemoveArticle(clicked_id) {
    //Ta bort artikel med hjälp av article_id från databas
    clicked_id = clicked_id.replace("btnRemove", "");
    var articleId = articleIdList[clicked_id];

    $.ajax({
        type: "POST",
        url: "ArticleList.aspx/DeleteArticle", 
        data: JSON.stringify({ id: articleId }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log("Response from server:", response.d);
        },
        error: function (xhr, status, error) {
            console.error("AJAX Error:", status, error);
        }
    });

    articleIdList = [];

    //Hämta den uppdaterade listan
    setTimeout(GetArticles, 1000)
}

function convertToJSDate(yyyymmddhhmmss) {
    // Ensure the input is a string
    const str = yyyymmddhhmmss.toString();

    // Extract year, month, day, hours, minutes, and seconds
    const year = parseInt(str.substring(0, 4), 10);
    const month = parseInt(str.substring(4, 6), 10) - 1; // Month is 0-based
    const day = parseInt(str.substring(6, 8), 10);
    const hours = parseInt(str.substring(8, 10), 10);
    const minutes = parseInt(str.substring(10, 12), 10);

    // Return the JavaScript Date object
    return new Date(year, month, day, hours, minutes);
}

