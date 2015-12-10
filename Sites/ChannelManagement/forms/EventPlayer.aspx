<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EventPlayer.aspx.cs" Inherits="ChannelManagement.forms.EventPlayer" %>
<%@ Register Src="~/Templates/ChannelHeader.ascx" TagPrefix="ams" TagName="ChannelHeader" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>节目播放</title>
    <link href="../css/azuremediaplayer.min.css" rel="stylesheet" />
    <script src="../scripts/azuremediaplayer.min.js"></script>
</head>
<body>
    <div class="container">
        <ams:ChannelHeader runat="server" ID="ChannelHeader" CurrentName="节目播放" />
        <form id="serverForm" runat="server">
            <div>
                <h2>Azure Media Service系列讲座（1）</h2>
            </div>
            <div>
                <h5>2015-12-10 13:00:00到2015-12-10：14:00</h5>
                <cite title="Source Title">Source Title</cite>
                <input runat="server" type="hidden" id="videoUrl" />
            </div>
            <div id="videoContainer">

                <script>
                    var videoWidth = 500;
                    if ($(window).width() < 750) {
                        videoWidth = $(window).width() * 11 / 16;
                    }
                    document.getElementById("videoContainer").style.height = videoWidth + "px";
                    $("#videoContainer").append('<video id="azuremediaplayer" class="azuremediaplayer amp-default-skin amp-big-play-centered" preload="auto" width="100%" height="100%" poster="../images/amsPoster1.png" tabindex=0><p class="amp-no-js">To view this video please enable JavaScript, and consider upgrading to a web browser that supports HTML5 video</p></video>');
                    $(document).ready(function () {
                        window.addEventListener('resize', function (event) {
                            if (window.innerWidth < 750) {
                                document.getElementById("videoContainer").style.height = $(window).width() * 11 / 16 + "px";
                            } else {
                                document.getElementById("videoContainer").style.height = "500px";
                            }
                        });
                    });
                </script>

                <!--<video id="azuremediaplayer" class="azuremediaplayer amp-default-skin amp-big-play-centered" autoplay controls preload="auto" width="100%" height="100%" poster="" tabindex=0></video>-->
            </div>
            <%--<script>
                var myOptions = {
                    "nativeControlsForTouch": false,
                    autoplay: true,
                    controls: true,
                    //width: "640",
                    //height: "400",
                    //poster: ""
                };
                var myPlayer = amp("azuremediaplayer", myOptions);

                myPlayer.src([{ src: document.getElementById("videoUrl").value, type: "application/vnd.ms-sstr+xml" }]);
            </script>--%>
            <div>
                <br />
                <p>
                    简单介绍了如何Windows Azure Media Services的总体功能，基本覆盖了媒体应用服务端需要的所有功能，其中的亮点包括：

1，流媒体编码自动适应不同的移动平台

2，Live Stream现场直播

具体内容，大家可以看视频，我就不多说了。我们来看一下开发者如何利用Media Services的功能。
                </p>
            </div>
        </form>
    </div>
</body>
</html>
