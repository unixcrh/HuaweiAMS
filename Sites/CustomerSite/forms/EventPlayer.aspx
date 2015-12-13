<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EventPlayer.aspx.cs" Inherits="CutomerSite.forms.EventPlayer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>事件播放</title>
    <link href="../css/azuremediaplayer.min.css" rel="stylesheet" />
    <link href="../css/mui/mui.min.css" rel="stylesheet" />
    <link href="../css/main.css" rel="stylesheet" />
    <script src="../scripts/jquery-2.1.4.min.js"></script>
    <script src="../scripts/mui.min.js"></script>
    <script src="../scripts/azuremediaplayer.min.js"></script>
</head>
<body>
    <div>
        <input runat="server" id="pageEventData" type="hidden" />
        <header class="mui-bar mui-bar-nav">
            <a class="mui-icon mui-icon-bars mui-pull-left"></a>
            <a class="mui-icon mui-icon-search mui-pull-right"></a>
            <h1 class="mui-title" id="eventName"></h1>
        </header>
        <div id="refreshContainer" class="mui-content mui-scroll-wrapper">
            <div class="mui-scroll">
                <div>
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
                            setTimeout(function () {
                                $($(".mui-table-view").children()[0]).before($(".mui-table-view").children()[2]);
                                mui('#refreshContainer').pullRefresh().endPulldownToRefresh();
                            }, 2000);
                        } //必选，刷新函数，根据具体业务来编写，比如通过ajax从服务器获取新数据；
                    },
                    up: {
                        contentrefresh: "正在加载...", //可选，正在加载状态时，上拉加载控件上显示的标题内容
                        contentnomore: '没有更多数据了', //可选，请求完毕若没有更多数据时显示的提醒内容；
                        callback: function () {
                            setTimeout(function () {
                                showBack.error("对不起，网络连接异常");
                                mui('#refreshContainer').pullRefresh().endPullupToRefresh();
                                /*$(".mui-table-view").append($(".mui-table-view").html());//必选，刷新函数，根据具体业务来编写，比如通过ajax从服务器获取新数据；
                              */
                            }, 2000);

                        }
                    }
                }
            });
            //点击左上角侧滑图标，打开侧滑菜单；
            document.querySelector('.mui-icon-bars').addEventListener('tap', function (e) {
                $("#menu").animate({
                    marginLeft: '0%'
                }, 300);
                $("#menu_back").show();
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
            });

            function initData(eventData) {
                $("#timeScope").text(eventData.startTime + "到" + eventData.endTime);

                $("#eventName").text(eventData.name);
                $("#description").text(eventData.description);
                $("#speakers").text(eventData.speakers);
                $("#views").text(eventData.views + "次观看");

                var myOptions = {
                    "nativecontrolsfortouch": false,
                    poster: eventData.poster,
                    autoplay: false,
                    controls: true,
                    preload: "auto"
                };
                var myPlayer = amp("azuremediaplayer", myOptions);

                myPlayer.src([{ src: eventData.url, type: "application/vnd.ms-sstr+xml" }]);
            }
        </script>
    </div>
</body>
</html>
