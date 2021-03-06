﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoMuiEventPlayer.aspx.cs" Inherits="CutomerSite.forms.NoMuiEventPlayer" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title id="videoTitle" runat="server"></title>

    <script src="../scripts/azuremediaplayer.min.js"></script>
    <script src="../scripts/jquery-2.1.4.min.js"></script>
    <script src="../scripts/bootstrap.min.js"></script>
    <script src="../Helpers/applicationInfo.aspx"></script>
    <script src="../scripts/lepus.mobile.js"></script>
    <script src="../scripts/lepus-util.js"></script>
    <script src="../scripts/lepus-webview-sdk.js"></script>
    <script src="../scripts/amsCommon.js"></script>

    <script src="../scripts/jstz.main.js"></script>
    <script src="../scripts/jstz.rules.js"></script>

    <link href="../css/azuremediaplayer.min.css" rel="stylesheet" />
    <link href="../css/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/main.css" rel="stylesheet" />
    <style type="text/css">
        .hidden {
            display: none;
        }

        .fullScreen {
            position: absolute;
            z-index: 1000;
            margin: 0px;
            padding: 0px;
            width: 100%;
            height: 100%;
            top: 0px;
            left: 0px;
            opacity: 1.0;
            background-color: black;
        }

        .bodyFullScreen {
            background-color: black;
        }
    </style>
</head>
<body>
    <div>
        <input runat="server" id="pageEventData" type="hidden" />
        <input runat="server" id="fixedBitrate" type="hidden" />

        <input runat="server" id="videoAddressType" type="hidden" />
        <input runat="server" id="targetAddressType" type="hidden" />
        <input type="hidden" id="eventID" runat="server" />
        <input type="hidden" id="channelID" runat="server" />
        <input type="hidden" id="techOrder" runat="server" />
        <input type="hidden" id="targetTechOrder" runat="server" />
    </div>
    <div class="container">
        <div>
            <div class="hidden">
                <div class="outerVideo hidden">
                    <div class="row">
                        <p id="timeScope"></p>
                    </div>
                    <div class="row">
                        <p id="views"></p>
                    </div>
                    <div class="row">
                        <cite id="speakers"></cite>
                    </div>
                </div>
            </div>
            <div class="row">
                <div id="videoContainer" >
                    <video id="azuremediaplayer" class="azuremediaplayer aazuremediaplayer amp-default-skin amp-big-play-centered" width="100%" height="100%" tabindex="0"></video>
                </div>
                <div class="outerVideo hidden">
                    <div class="row">
                        <p id="description" />
                    </div>
                </div>
            </div>
            <div class="hidden">
                <div class="row outerVideo hidden" id="buttonContainer">
                    <a class="btn btn-default" id="switchVideoAddressType" runat="server">切换到备用CDN</a>
                    <select id="channels" runat="server" class="form-control" style="width: 120px; display: inline"></select>
                    <div class="btn btn-default hidden" id="fullscreenBtn">全屏</div>
                    <div class="btn btn-default hidden" id="pauseBtn">暂停</div>
                    <div class="btn btn-default hidden" id="playBtn">播放</div>
                    <div class="btn btn-success" id="refreshBtn">刷新</div>
                    <div class="btn btn-default" id="switchTechOrder" runat="server">动态码率</div>
                </div>
            </div>
            <div class="outerVideo">
                <div>
                    <p id="userAgent" runat="server" />
                </div>
                <div>
                    <p id="allCookies" runat="server" />
                </div>
            </div>
        </div>
        <script>
            function getServiceUrl() {
                var url = "../services/QueryService.ashx?opType=SingleEvent&id=" + $("#eventID").val() + "&" + $("#channelID").val();

                return appendTimeOffsetToUrl(url);
            }

            function refreshPage() {
                var url = document.location.href;

                var paramIndex = url.indexOf("?");

                if (paramIndex >= 0)
                    url = url.substr(0, paramIndex);

                var result = url + "?id=" + $("#eventID").val() + "&videoAddressType=" + $("#videoAddressType").val() +
                    "&techOrder=" + $("#techOrder").val() + "&channelID=" + $("#channelID").val();

                window.location.replace(result);
            }

            var videoWidth = 500;
            if ($(window).width() < 750) {
                videoWidth = $(window).width() * 11 / 16;
            }

            document.getElementById("videoContainer").style.height = videoWidth + "px";

            $(document).ready(function () {
                window.addEventListener('resize', function (event) {

                    if (fullScreenState.isFullScreen == false) {
                        if (window.innerWidth < 750) {
                            document.getElementById("videoContainer").style.height = $(window).width() * 11 / 16 + "px";
                        } else {
                            document.getElementById("videoContainer").style.height = "500px";
                        }
                    }
                });

                var jsonData = $("#pageEventData").val();

                if (jsonData != "") {
                    var eventData = JSON.parse(jsonData);

                    initData(eventData);
                }

                initButtons();

                $(".vjs-fullscreen-control").addClass("hidden");

                $("<div id='thisFullscreen' class='vjs-fullscreen-control vjs-control outline-enabled-control'></div>").click(function () {
                    switchFullscreen(myPlayer);
                }).insertAfter(".vjs-fullscreen-control");

                ams.initMenu();
            });

            function initButtons() {
                $("#switchVideoAddressType").click(function () {
                    $("#videoAddressType").val($("#targetAddressType").val());
                    refreshPage();
                    return false;
                });

                $("#fullscreenBtn").click(function () {
                    switchFullscreen(myPlayer);
                });

                $("#playBtn").click(function () {
                    myPlayer.play();
                });

                $("#pauseBtn").click(function () {
                    myPlayer.pause();
                });

                $("#refreshBtn").click(function () {
                    initLoadData();
                });

                $("#channels").change(function () {
                    $("#channelID").val($("#channels").val());
                    refreshPage();
                });

                $("#switchTechOrder").click(function () {
                    $("#techOrder").val($("#targetTechOrder").val());
                    refreshPage();
                });
            }

            function initLoadData() {
                showBack.message("加载数据...", true);

                $.getJSON(getServiceUrl(), function (data) {
                    if (afterReloadAllData(data))
                        showBack.hide();
                }).done(function () {

                }).fail(function (e) {
                    showBack.error("对不起，网络连接异常");
                }).always(function () {

                });
            }

            function afterReloadAllData(data) {
                var result = true;

                if (typeof (data.stackTrace) != "undefined") {
                    result = false;
                    showBack.error("对不起，网络连接异常");
                }
                else {
                    $("#listContainer").empty();
                    initData(data);
                }

                return result;
            }

            var fullScreenState = {
                switching: false,
                isFullScreen: false,
                originalHeight: "500px"
            }

            var playingState = {
                switching: false,
                paused: true
            }

            function switchPlayingState(player) {
                if (playingState.switching == false) {
                    playingState.switching = true;

                    window.setTimeout(function () {

                        if (playingState.paused)
                            player.play();
                        else
                            player.pause();

                        playingState.paused = !playingState.paused;
                        playingState.switching = false;
                    }, 100);
                }
            }

            function switchFullscreen(player) {
                if (fullScreenState.switching == false) {
                    if (fullScreenState.isFullScreen == false) {
                        if (player.isFullscreen() == false) {
                            fullScreenState.switching = true;

                            window.setTimeout(enterFullscreen, 100);
                        }
                    }
                    else {
                        if (player.isFullscreen() == false) {
                            fullScreenState.switching = true;

                            window.setTimeout(exitFullscreen, 100);
                        }
                    }
                }
            }

            function enterFullscreen() {
                fullScreenState.originalHeight = document.getElementById("videoContainer").style.height;
                $("#videoContainer").removeAttr("style").addClass("fullScreen");
                //$("#refreshContainer").removeClass("mui-content");
                $("body").addClass("bodyFullScreen");
                $(".outerVideo").addClass("hidden");

                ams.enterFullScreen();

                fullScreenState.switching = false;
                fullScreenState.isFullScreen = true;
            }

            function exitFullscreen() {
                if (fullScreenState.isFullScreen) {
                    $("#videoContainer").removeClass("fullScreen");
                    //$("#refreshContainer").addClass("mui-content");
                    $("body").removeClass("bodyFullScreen");

                    $(".outerVideo").removeClass("hidden");

                    document.getElementById("videoContainer").style.height = fullScreenState.originalHeight;

                    ams.exitFullScreen();

                    fullScreenState.isFullScreen = false;
                    fullScreenState.switching = false;
                }
            }

            var myPlayer = null;
            var html5TechOrder = ["html5", "azureHtml5JS", "flashSS", "silverlightSS"];
            var mseTechOrder = ["azureHtml5JS", "html5", "flashSS", "silverlightSS"];

            function initData(eventData) {
                $("#eventID").val(eventData.id);
                $("#timeScope").text(eventData.startTime + "到" + eventData.endTime);

                $("#eventName").text(eventData.name);
                $("#description").text(eventData.description);
                $("#speakers").text(eventData.speakers);
                $("#views").text(eventData.views + "次观看");

                var myOptions = {
                    "nativeControlsForTouch": false,
                    autoplay: false,
                    controls: true,
                    heuristicProfile: "High Quality",
                    poster: eventData.poster,
                    techOrder: $("#techOrder").val() == "Html5" ? html5TechOrder : mseTechOrder,
                    preload: "auto"
                };

                if (eventData.url != "") {
                    myPlayer = amp("azuremediaplayer", myOptions);

                    myPlayer.addEventListener(amp.eventName.loadedmetadata, function () {
                        try {
                            var stream = myPlayer.currentVideoStreamList().streams ? myPlayer.currentVideoStreamList().streams[0] : undefined;

                            if (stream && $("#fixedBitrate").val() == "true") {
                                //var midTrack = Math.floor(stream.tracks.length / 2);

                                //stream.selectTrackByIndex(midTrack);
                                stream.selectTrackByIndex(0);
                            }
                        }
                        catch (e) {
                        }
                    });

                    myPlayer.src([{ src: eventData.url, type: "application/vnd.ms-sstr+xml" }]);
                }
            }
        </script>
    </div>
</body>
</html>
