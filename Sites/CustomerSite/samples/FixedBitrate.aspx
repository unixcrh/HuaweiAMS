<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FixedBitrate.aspx.cs" Inherits="CutomerSite.samples.FixedBitrate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择固定码率</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="../css/azuremediaplayer.min.css" rel="stylesheet" />
    <script src="../scripts/azuremediaplayer.min.js"></script>
</head>
<body>
    <h1>Sample: Fixed Bitrate</h1>
    <video id="azuremediaplayer" class="azuremediaplayer amp-default-skin amp-big-play-centered" tabindex="0"></video>
    <form runat="server" id="serverForm">
        <asp:TextBox runat="server" ID="videoUrl" Style="width: 600px"></asp:TextBox>
        <asp:Button runat="server" ID="changeVideo" Text="播放" OnClick="changeVideo_Click" />
    </form>
    <div id="selectedBitrate"></div>
    <script>
        var myOptions = {
            "nativeControlsForTouch": false,
            autoplay: false,
            controls: true,
            width: "640",
            height: "400"
        };
        var myPlayer = amp("azuremediaplayer", myOptions);

        myPlayer.addEventListener(amp.eventName.loadedmetadata, function () {
            var stream = myPlayer.currentVideoStreamList().streams ? myPlayer.currentVideoStreamList().streams[0] : undefined;
            if (stream) {
                var track0 = stream.tracks[0];

                stream.selectTrackByIndex(0);
                selectedBitrate.innerText = "Selected bitrate is: " + track0.bitrate;
            }
        });

        myPlayer.src([{ src: document.getElementById("videoUrl").value, type: "application/vnd.ms-sstr+xml" }]);
    </script>
    <footer>
        <br />
        <p>© Microsoft Corporation 2015</p>
    </footer>
</body>
</html>
