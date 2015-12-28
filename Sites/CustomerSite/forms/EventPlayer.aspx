<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EventPlayer.aspx.cs" Inherits="CutomerSite.forms.EventPlayer" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title id="videoTitle" runat="server"></title>
    <link href="../css/azuremediaplayer.min.css" rel="stylesheet" />
    <link href="../css/mui/mui.min.css" rel="stylesheet" />
    <link href="../css/main.css" rel="stylesheet" />
    <script src="../scripts/azuremediaplayer.min.js"></script>
    <script src="../scripts/jquery-2.1.4.min.js"></script>
    <script src="../scripts/mui.min.js"></script>
    <script src="../Helpers/applicationInfo.aspx"></script>
    <script src="../scripts/lepus.mobile.js"></script>
    <script src="../scripts/lepus-util.js"></script>
    <script src="../scripts/lepus-webview-sdk.js"></script>
    <script src="../scripts/amsCommon.js"></script>

    <script src="../scripts/jstz.main.js"></script>
    <script src="../scripts/jstz.rules.js"></script>

    <style type="text/css">
        #topPopover .mui-popover-arrow {
            left: auto;
            right: 6px;
        }

        .mui-popover {
            height: 300px;
        }

        .hidden {
            display: none;
        }

        .fullScreen {
            z-index: 1000;
            border: solid 1px double;
            margin: 0px;
            padding: 0px;
            width: 100%;
            height: 100%;
            top: 0px;
            left: 0px;
            opacity: 1.0;
            position: absolute;
            background-color: black;
        }
    </style>
</head>
<body>
    <div>
        <input runat="server" id="pageEventData" type="hidden" />
        <input runat="server" id="fixedBitrate" type="hidden" />
        <input runat="server" id="videoAddressType" type="hidden" />
        <header class="mui-bar mui-bar-nav hidden">
            <a class="mui-icon mui-icon-bars mui-pull-left"></a>
            <a class="mui-icon mui-icon-search mui-pull-right"></a>
            <h1 class="mui-title" id="eventName"></h1>
        </header>
        <div id="refreshContainer" class="mui-content mui-scroll-wrapper">
            <div class="mui-scroll">
                <div>
                    <input type="hidden" id="eventID" />
                    <p class='mui-ellipsis' id="timeScope"></p>
                </div>
                <div>
                    <p class='mui-ellipsis' id="views"></p>
                </div>
                <div>
                    <cite class='mui-ellipsis' id="speakers"></cite>
                </div>
                <div id="videoContainer">
                    <video id="azuremediaplayer" class="azuremediaplayer aazuremediaplayer amp-default-skin amp-big-play-centered" width="100%" height="100%" tabindex="0"></video>
                </div>
                <div>
                    <p id="description" />
                </div>
                <div id="buttonContainer">
                    <a class="mui-btn" id="switchVideoAddressType" runat="server">切换到</a>
                    <a class="mui-btn" id="pauseBtn">暂停</a>
                    <a class="mui-btn" id="playBtn">播放</a>
                </div>
                <div>
                    <p id="userAgent" runat="server" />
                </div>
                <div>
                    <p id="allCookies" runat="server" />
                </div>
                <div>
                    <p>client cookies:</p>
                    <p id="clientCookies" style="overflow: auto"></p>
                </div>
            </div>
        </div>
        <div id="menu_back" class="menu_back"></div>
        <div id="menu" class="mui-content menu">
            <div class="title">侧滑导航</div>
            <div class="content">
                这是首页侧滑导航示例，你可以在这里放置任何内容；
            </div>
        </div>
        <script>
            var menu = null;
            var main = null;
            var showMenu = false;

            function getServiceUrl() {
                var url = "../services/QueryService.ashx?opType=SingleEvent&id=" + $("#eventID").val();

                return appendTimeOffsetToUrl(url);
            }

            mui.init({
                pullRefresh: {
                    container: "#refreshContainer", //下拉刷新容器标识，querySelector能定位的css选择器均可，比如：id、.class等
                    down: {
                        contentdown: "下拉可以刷新", //可选，在下拉可刷新状态时，下拉刷新控件上显示的标题内容
                        contentover: "释放立即刷新", //可选，在释放可刷新状态时，下拉刷新控件上显示的标题内容
                        contentrefresh: "正在刷新...", //可选，正在刷新状态时，下拉刷新控件上显示的标题内容
                        callback: function () {
                            $.getJSON(getServiceUrl(), function (data) {

                                afterReloadAllData(data);
                            }).done(function () {
                                mui('#refreshContainer').pullRefresh().endPulldownToRefresh();
                            }).fail(function (e) {
                                showBack.error("对不起，网络连接异常");
                            }).always(function () {
                                mui('#refreshContainer').pullRefresh().endPullupToRefresh();
                            });
                        } //必选，刷新函数，根据具体业务来编写，比如通过ajax从服务器获取新数据；
                    },
                }
            });

            document.querySelector('.menu_back').addEventListener('tap', function (e) {
                $("#menu").animate({
                    marginLeft: '-100%'
                }, 300);
                $("#menu_back").hide();
            });
        </script>
        <script>
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

                mui("#buttonContainer").on("tap", "#switchVideoAddressType", function () {
                    window.location.href = $(this).attr("href");
                });

                ams.initMenu();
                initLoadData();

                $("#clientCookies").text(document.cookie);
            });

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
                fullScreenState.switching = false;
                fullScreenState.isFullScreen = true;
            }

            function exitFullscreen() {
                if (fullScreenState.isFullScreen) {
                    $("#videoContainer").removeClass("fullScreen");
                    document.getElementById("videoContainer").style.height = fullScreenState.originalHeight;
                    fullScreenState.isFullScreen = false;
                    fullScreenState.switching = false;
                }
            }

            function initData(eventData) {
                $("#eventID").val(eventData.id);
                $("#timeScope").text(eventData.startTime + "到" + eventData.endTime);

                $("#eventName").text(eventData.name);
                $("#description").text(eventData.description);
                $("#speakers").text(eventData.speakers);
                $("#views").text(eventData.views + "次观看");

                //var myOptions = {
                //    "nativecontrolsfortouch": false,
                //    //poster: eventData.poster,
                //    autoplay: false,
                //    controls: true
                //    //preload: "auto"
                //};

                var myOptions = {
                    "nativeControlsForTouch": false,
                    autoplay: false,
                    controls: true,
                    preload: "auto"
                };

                if (eventData.url != "") {
                    var myPlayer = amp("azuremediaplayer", myOptions);

                    myPlayer.addEventListener(amp.eventName.loadedmetadata, function () {
                        var stream = myPlayer.currentVideoStreamList().streams ? myPlayer.currentVideoStreamList().streams[0] : undefined;
                        if (stream && $("#fixedBitrate").val() == "true") {
                            var track0 = stream.tracks[0];

                            stream.selectTrackByIndex(0);
                        }
                    });

                    //myPlayer.addEventListener(amp.eventName.fullscreenchange, function () {
                    //    switchFullscreen(myPlayer);
                    //});

                    mui("#buttonContainer").on("tap", "#playBtn", function () {
                        myPlayer.play();
                    });

                    mui("#buttonContainer").on("tap", "#pauseBtn", function () {
                        myPlayer.pause();
                    });

                    mui("#videoContainer").on("tap", ".amp-controlbaricons-left", function () {
                        switchPlayingState(myPlayer);
                    });

                    //logo class = .amp-logo
                    mui("#videoContainer").on("tap", ".vjs-fullscreen-control", function () {
                        switchFullscreen(myPlayer);
                    });

                    myPlayer.src([{ src: eventData.url, type: "application/vnd.ms-sstr+xml" }]);
                }
            }
        </script>
    </div>
</body>
</html>
