﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="simpleVideo.aspx.cs" Inherits="CutomerSite.samples.simpleVideo" %>

<!DOCTYPE html>

<html>
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>Azure Media Player</title>
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!--*****START OF Azure Media Player Scripts*****-->
    <!--Note: DO NOT USE the "latest" folder in production. Replace "latest" with a version number like "1.0.0"-->
    <!--EX:<script src="//amp.azure.net/libs/amp/1.0.0/azuremediaplayer.min.js"></script>-->
    <!--Azure Media Player versions can be queried from //amp.azure.net/libs/amp/latest/docs/changelog.html-->
    <link href="../css/azuremediaplayer.min.css" rel="stylesheet">
    <script src="../scripts/azuremediaplayer.min.js"></script>
    <!--*****END OF Azure Media Player Scripts*****-->

</head>
<body>
    <h1>Sample: Clear</h1>
    <video id="azuremediaplayer" class="azuremediaplayer amp-default-skin amp-big-play-centered" tabindex="0"></video>
    <form runat="server" id="serverForm">
        <asp:TextBox runat="server" ID="videoUrl" Style="width: 600px"></asp:TextBox>
        <asp:Button runat="server" ID="changeVideo" Text="播放" OnClick="changeVideo_Click" />
    </form>
    <textarea id="txtLog" style="width: 640px" rows="10"></textarea>
    <script>
        var myOptions = {
            "nativeControlsForTouch": false,
            autoplay: false,
            controls: true,
            heuristicProfile: "High Quality",
            width: "640",
            height: "400",
            poster: "https://c.s-microsoft.com/zh-cn/CMSImages/Store_Anniversary_1127_1920x720_ZH_CN.jpg?version=e78bd56d-0106-e9e9-995d-472b15eb58a6"
        };
        var myPlayer = amp("azuremediaplayer", myOptions);

        myPlayer.addEventListener(amp.eventName.loadedmetadata, function () {
            var txtLog = document.getElementById("txtLog");
            myPlayer.addEventListener(amp.eventName.downloadbitratechanged, bitrateEvent);
            myPlayer.addEventListener(amp.eventName.playbackbitratechanged, bitrateEvent);

            var videoBuffer = myPlayer.videoBufferData();
            var audioBuffer = myPlayer.audioBufferData();

            if (videoBuffer) {
                videoBuffer.addEventListener(amp.bufferDataEventName.downloadrequested, logVideoBufferData);
                videoBuffer.addEventListener(amp.bufferDataEventName.downloadcompleted, logVideoBufferData);
                videoBuffer.addEventListener(amp.bufferDataEventName.downloadfailed, logVideoBufferData);
            }

            if (audioBuffer) {
                audioBuffer.addEventListener(amp.bufferDataEventName.downloadrequested, logAudioBufferData);
                audioBuffer.addEventListener(amp.bufferDataEventName.downloadcompleted, logAudioBufferData);
                audioBuffer.addEventListener(amp.bufferDataEventName.downloadfailed, logAudioBufferData);
            }

            function bitrateEvent(evt) {
                txtLog.innerHTML += evt.type + ": "
                + (amp.eventName.downloadbitratechanged === evt.type
                ? myPlayer.currentDownloadBitrate() : myPlayer.currentPlaybackBitrate()) + "\n";
            }

            function logVideoBufferData(evt) { logBufferData(evt, "Video", videoBuffer); };
            function logAudioBufferData(evt) { logBufferData(evt, "Audio", audioBuffer); };

            function logBufferData(evt, type, bufferData) {
                if (evt.type === amp.bufferDataEventName.downloadfailed) {
                    txtLog.innerHTML += type + " download failed"
                    + " code: 0x" + bufferData.downloadFailed.code.toString(8)
                    + " msg: " + bufferData.downloadFailed.message + "\n";
                }
                else if (evt.type === amp.bufferDataEventName.downloadrequested) {
                    var downloadRequested = bufferData.downloadRequested
                }
                else if (evt.type === amp.bufferDataEventName.downloadcompleted) {
                    var downloadCompleted = bufferData.downloadCompleted;
                    txtLog.innerHTML += type + " download completed"
                        + " bufferLevel: " + bufferData.bufferLevel + "\n";
                }
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
