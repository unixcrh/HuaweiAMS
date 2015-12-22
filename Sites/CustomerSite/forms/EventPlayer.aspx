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
    </style>
</head>
<body>
    <div>
        <input runat="server" id="pageEventData" type="hidden" />
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
                <div id="buttonContainer">
                    <a class="mui-btn" id="switchVideoAddressType" runat="server">切换到</a>
                </div>
                <div>
                    <p id="description" />
                </div>
                <div>
                    <p id="userAgent" runat="server" />
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

            mui.init({
                pullRefresh: {
                    container: "#refreshContainer", //下拉刷新容器标识，querySelector能定位的css选择器均可，比如：id、.class等
                    down: {
                        contentdown: "下拉可以刷新", //可选，在下拉可刷新状态时，下拉刷新控件上显示的标题内容
                        contentover: "释放立即刷新", //可选，在释放可刷新状态时，下拉刷新控件上显示的标题内容
                        contentrefresh: "正在刷新...", //可选，正在刷新状态时，下拉刷新控件上显示的标题内容
                        callback: function () {
                            $.getJSON("../services/QueryService.ashx?opType=SingleEvent&id=" + $("#eventID").val(), function (data) {

                                if (typeof (data.stackTrace) != "undefined") {
                                    showBack.error("对不起，网络连接异常");
                                    console.error(data.message);
                                }
                                else {
                                    $("#listContainer").empty();
                                    initData(data);
                                }
                            }).done(function () {
                                console.log("second success");
                                mui('#refreshContainer').pullRefresh().endPulldownToRefresh();
                            }).fail(function (e) {
                                console.log("error");

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
            var showBack = function () {
                function init() {
                    var backHtml = "<div class=\"back\"></div>" +
                        "<div class=\"warm\"></div>";
                    var newNode = document.createElement("div");
                    newNode.setAttribute("id", "back");
                    newNode.innerHTML = backHtml;
                    return newNode;
                }
                var instance;
                return {
                    show: function () {
                        if (instance) {
                            $("#back").show();
                        } else {
                            instance = init();
                            document.body.appendChild(instance);
                        }
                    }, hide: function () {
                        $("#back").hide();
                    }, error: function (txt) {
                        showBack.show();
                        $("#back").children('.warm').html(txt);
                        $("#back").children('.warm').show();
                        //显示两秒之后刷新
                        setTimeout(function () {
                            $("#back").children('.warm').fadeOut('slow', function () {
                                $("#back").hide();
                            });
                        }, 2000)
                    }
                }
            }();
        </script>
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

                var jsonData = $("#pageEventData").val();

                if (jsonData != "") {
                    var eventData = JSON.parse(jsonData);

                    initData(eventData);
                }

                mui("#buttonContainer").on("tap", "a", function () {
                    window.location.href = $(this).attr("href");
                });

                ams.initMenu();
            });

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
                        if (stream) {
                            var track0 = stream.tracks[0];

                            stream.selectTrackByIndex(0);
                        }
                    });

                    myPlayer.src([{ src: eventData.url, type: "application/vnd.ms-sstr+xml" }]);
                }
            }
        </script>
    </div>
</body>
</html>
