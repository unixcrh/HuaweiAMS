<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EventPlayer.aspx.cs" Inherits="ChannelManagement.forms.EventPlayer" %>

<%@ Register Src="~/Templates/ChannelHeader.ascx" TagPrefix="ams" TagName="ChannelHeader" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>事件播放</title>
    <link href="../css/azuremediaplayer.min.css" rel="stylesheet" />
    <script src="../scripts/azuremediaplayer.min.js"></script>
</head>
<body>
    <div class="container">
        <ams:ChannelHeader runat="server" ID="ChannelHeader" CurrentName="节目播放" />
        <form id="serverForm" runat="server">
            <res:DataBindingControl runat="server" ID="bindingControl" IsValidateOnSubmit="true" ValidateUnbindProperties="false">
                <ItemBindings>
                    <res:DataBindingItem DataPropertyName="Name" ControlID="Name" />
                    <res:DataBindingItem DataPropertyName="Description" ControlID="Description" />
                    <res:DataBindingItem DataPropertyName="StartTime" ControlID="StartTime" Format="{0:yyyy-MM-dd HH:mm:ss}" />
                    <res:DataBindingItem DataPropertyName="EndTime" ControlID="EndTime" Format="{0:yyyy-MM-dd HH:mm:ss}" />
                    <res:DataBindingItem DataPropertyName="Speakers" ControlID="Speakers" ControlPropertyName="Text" />
                    <res:DataBindingItem DataPropertyName="CDNPlaybackUrl" ControlID="videoUrl" ControlPropertyName="Value" />
                    <res:DataBindingItem DataPropertyName="Views" ControlID="Views" ControlPropertyName="Text" Format="{0:#,##0}" />
                    <res:DataBindingItem DataPropertyName="PosterUrl" ControlID="PosterUrl" ControlPropertyName="Value" />
                </ItemBindings>
            </res:DataBindingControl>
            <div>
                <h2>
                    <asp:Literal runat="server" ID="Name" Mode="Encode" />
                </h2>
            </div>
            <div>
                <h5>
                    <asp:Literal runat="server" ID="StartTime" Mode="Encode" />到<asp:Literal runat="server" ID="EndTime" Mode="Encode" /></h5>
                <cite>
                    <asp:Literal runat="server" ID="Speakers" Mode="Encode" /></cite>
                <h6>
                    <asp:Literal runat="server" ID="Views" Mode="Encode" />次观看</h6>
                <input runat="server" type="hidden" id="videoUrl" />
                <input runat="server" type="hidden" id="PosterUrl" />
            </div>
            <div id="videoContainer">
                <video id="azuremediaplayer" class="azuremediaplayer aazuremediaplayer amp-default-skin amp-big-play-centered" width="100%" height="100%" tabindex="0"></video>
            </div>
            <script>
                var videoWidth = 500;
                if ($(window).width() < 750) {
                    videoWidth = $(window).width() * 11 / 16;
                }

                document.getElementById("videoContainer").style.height = videoWidth + "px";

                $(document).ready(function () {
                    window.addEventListener('resize', function (event) {
                        if (window.innerWidth < 750) {
                            document.getElementById("videoContainer").style.height = $(window).width() * 11 / 16 + "px";
                        } else {
                            document.getElementById("videoContainer").style.height = "500px";
                        }
                    });


                    var myOptions = {
                        "nativecontrolsfortouch": false,
                        poster: $("#PosterUrl").val(),
                        autoplay: true,
                        controls: true,
                        preload: "auto"
                    };
                    var myPlayer = amp("azuremediaplayer", myOptions);

                    myPlayer.src([{ src: document.getElementById("videoUrl").value, type: "application/vnd.ms-sstr+xml" }]);
                });
            </script>
            <div>
                <br />
                <p>
                    <asp:Literal runat="server" ID="Description" Mode="Encode" />
                </p>
            </div>
        </form>
    </div>
</body>
</html>
