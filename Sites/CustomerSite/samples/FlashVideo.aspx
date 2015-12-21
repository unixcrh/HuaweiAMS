<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlashVideo.aspx.cs" Inherits="CutomerSite.samples.FlashVideo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Flash播放</title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link href="../css/azuremediaplayer.min.css" rel="stylesheet">
    <script src="../scripts/azuremediaplayer.min.js"></script>
</head>
<body>
    <form id="serverForm" runat="server">
        <video id="azuremediaplayer" class="azuremediaplayer amp-default-skin amp-big-play-centered" tabindex="0"> </video>
        <div>
            <script>
                var myOptions = {
                    techOrder: ["flashSS", "azureHtml5JS", "silverlightSS", "html5", ],
                    "nativeControlsForTouch": false,
                    autoplay: true,
                    controls: true,
                    width: "640",
                    height: "400"
                };
                var myPlayer = amp("azuremediaplayer", myOptions);

                myPlayer.src([{ src: "http://amshuaweichn.streaming.mediaservices.chinacloudapi.cn/94706d4f-ea13-4eb4-a938-a58c9b9a0897/XBox%20Video.ism/manifest", type: "application/vnd.ms-sstr+xml" }]);
            </script>
        </div>
    </form>
</body>
</html>
