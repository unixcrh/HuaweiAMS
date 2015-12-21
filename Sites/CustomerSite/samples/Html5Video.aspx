<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Html5Video.aspx.cs" Inherits="CutomerSite.samples.Html5Video" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Html 5视频</title>
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1,user-scalable=no" />
</head>
<body>
    <form id="serverForm" runat="server">
        <div>
            <video id="azuremediaplayer_html5_api" oncontextmenu="return false;" style="width: 640px; height: 480px" autoplay="autoplay"
                preload="auto" src="http://vjs.zencdn.net/v/oceans.mp4">
                <source src="http://vjs.zencdn.net/v/oceans.mp4" type='video/mp4; codecs="avc1.42E01E, mp4a.40.2"' />
            </video>
        </div>
    </form>
</body>
</html>
