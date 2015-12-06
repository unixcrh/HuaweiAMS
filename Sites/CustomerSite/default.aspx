<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="CutomerSite._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>测试页面</title>
</head>
<body>
    <form id="serverForm" runat="server">
        <div>
            测试页面
            <ul>
                <li><a runat="server" id="eastAsiaSample" href="../samples/simpleVideo.aspx">East Asia Sample Video</a></li>
                <li><a runat="server" id="northEuropeSample" href="../samples/simpleVideo.aspx">North Europe Sample Video</a></li>
                <li><a runat="server" id="eastAsianSalesTraining" href="../samples/simpleVideo.aspx">East Asia Sales Training</a></li>
                <li><a runat="server" id="eastAsianSalesTrainingWithoutCDN" href="../samples/simpleVideo.aspx">East Asia Sales Training Without CDN</a></li>
                <li><a runat="server" id="europeSalesTraining" href="../samples/simpleVideo.aspx">North Europe Sales Training</a></li>
                <li><a runat="server" id="europeSalesTrainingWithoutCDN" href="../samples/simpleVideo.aspx">North Europe Sales Training Without CDN</a></li>
            </ul>
        </div>
    </form>
</body>
</html>
