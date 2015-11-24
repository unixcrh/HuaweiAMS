<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="simpleVideo.aspx.cs" Inherits="CutomerSite.samples.simpleVideo" %>

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
    <script>
        var myOptions = {
            "nativeControlsForTouch": true,
            autoplay: true,
            controls: true,
            width: "640",
            height: "400",
            poster: ""
        };
        var myPlayer = amp("azuremediaplayer", myOptions);
        myPlayer.src([{ src: "http://cdn-zhshenstudy.streaming.mediaservices.windows.net/ea9b6078-80e1-4954-85ca-4fef0064b112/Robotica_720.ism/manifest", type: "application/vnd.ms-sstr+xml" }, ]);
    </script>
    <footer>
        <br />
        <p>© Microsoft Corporation 2015</p>
    </footer>

</body>
</html>
