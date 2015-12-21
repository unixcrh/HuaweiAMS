<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="CutomerSite._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>测试页面</title>
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1,user-scalable=no" />
    <link href="css/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="serverForm" runat="server">
        <div>
            测试页面
            <ul>
                <li><a runat="server" id="eastAsiaSample" class="btn btn-default" href="../samples/simpleVideo.aspx">East Asia Sample Video</a></li>
                <li><a runat="server" id="northEuropeSample" class="btn btn-default" href="../samples/simpleVideo.aspx">North Europe Sample Video</a></li>
                <li><a runat="server" id="eastAsianSalesTraining" class="btn btn-default" href="../samples/simpleVideo.aspx">East Asia Sales Training</a></li>
                <li><a runat="server" id="eastAsianSalesTrainingWithoutCDN" class="btn btn-default" href="../samples/simpleVideo.aspx">East Asia Sales Training Without CDN</a></li>
                <li><a runat="server" id="europeSalesTraining" class="btn btn-default" href="../samples/simpleVideo.aspx">North Europe Sales Training</a></li>
                <li><a runat="server" id="europeSalesTrainingWithoutCDN" class="btn btn-default" href="../samples/simpleVideo.aspx">North Europe Sales Training Without CDN</a></li>
                <li><a runat="server" id="A1" class="btn btn-default" href="~/list/AllEvents.aspx">Menu...</a></li>
                <li><a runat="server" id="A4" class="btn btn-default" href="~/list/MoreMenu.aspx">More Menu...</a></li>
                <li><a runat="server" id="A2" class="btn btn-default" href="~/samples/Html5Video.aspx">纯Html5 MP4...</a></li>
                <li><a runat="server" id="A3" class="btn btn-default" href="~/samples/FlashVideo.aspx">优先使用Flash播放...</a></li>
            </ul>
        </div>
    </form>
</body>
</html>
