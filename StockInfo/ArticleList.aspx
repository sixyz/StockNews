<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleList.aspx.cs" Inherits="StockInfo.ArticleList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons/font/bootstrap-icons.css" rel="stylesheet"/>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous"/>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
    <script src="ArticleList.js"></script>
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>

</head>
<body>
    <form id="form1" runat="server">

                    <div>

                    <div>

    <nav class="navbar navbar-expand-lg navbar-light bg-light">
    <div class="container-fluid">
    <a class="navbar-brand" href="#">News feed</a>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarNav">
      <ul class="navbar-nav">
        <li class="nav-item">
          <a class="nav-link active" aria-current="page" href="Home.aspx">Home</a>
        </li>
        <li class="nav-item">
          <a class="nav-link" href="Newsfeed.aspx">News feed</a>
        </li>
        <li class="nav-item">
            <a class="nav-link active" aria-current="page" href="Stocks.aspx">Stocks</a>
        </li>
        <li class="nav-item">
            <a class="nav-link active" aria-current="page" href="Account.aspx">Account</a>
        </li>
      </ul>
    </div>
  </div>
</nav>
                        </div>

        </div>

        <table class="table table-hover" id="tblArticles">
            <thead>
    <tr>
      <th scope="col">Title</th>
      <th scope="col">Url</th>
      <th scope="col">Date published</th>
      <th scope="col">Authors</th>
      <th scope="col">Summary</th>
      <th scope="col">Source</th>
      <th scope="col">Sentiment</th>
    </tr>
  </thead>
            <tbody id="tbodyData">
                
            </tbody>

        </table>

    </form>
</body>
</html>
