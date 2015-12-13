<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllEvents.aspx.cs" Inherits="CutomerSite.list.AllEvents" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>所有事件</title>
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1,user-scalable=no" />
    <script src="../scripts/jquery-2.1.4.min.js"></script>
    <script src="../scripts/mui.min.js"></script>
    <link href="../css/mui/mui.min.css" rel="stylesheet" />
    <link href="../css/main.css" rel="stylesheet" />
</head>
<body>
    <div>
        <input runat="server" id="firstPageData" />
        <input runat="server" id="totalCount" />
        <input runat="server" id="pageSize" />
    </div>
    <header class="mui-bar mui-bar-nav">
        <a class="mui-icon mui-icon-bars mui-pull-left"></a>
        <a class="mui-icon mui-icon-search mui-pull-right"></a>
        <h1 class="mui-title">所有事件</h1>
    </header>
    <div id="refreshContainer" class="mui-content mui-scroll-wrapper">
        <div class="mui-scroll">
            <!--数据列表-->
            <ul id="listContainer" class="mui-table-view">
                <%--<li class="mui-table-view-cell mui-media">
                    <a href="javascript:;">
                        <img class="mui-media-object mui-pull-left" src="../images/shuijiao.jpg">
                        <div>
                            <div>我是标题1我是标题1我是标题1我是标题1</div>
                            <p class='mui-ellipsis'>能和心爱的人一起睡觉，是件幸福的事情；可是，打呼噜怎么办？</p>
                            <div style="margin-top: 10px;">
                                <span class="mui-icon mui-icon-star"></span>
                                <span class="mui-icon mui-icon-star"></span>
                                <span class="mui-icon mui-icon-star"></span>
                                <span class="mui-icon mui-icon-star"></span>
                                <span class="mui-icon mui-icon-star"></span>
                            </div>
                        </div>
                    </a>
                </li>
                <li class="mui-table-view-cell mui-media">
                    <a href="javascript:;">
                        <img class="mui-media-object mui-pull-left" src="images/muwu.jpg">
                        <div class="mui-media-body">
                            木屋
                            <p class='mui-ellipsis'>想要这样一间小木屋，夏天挫冰吃瓜，冬天围炉取暖.</p>
                        </div>
                    </a>
                </li>
                <li class="mui-table-view-cell mui-media">
                    <a href="javascript:;">
                        <img class="mui-media-object mui-pull-left" src="images/cbd.jpg">
                        <div class="mui-media-body">
                            CBD
                            <p class='mui-ellipsis'>烤炉模式的城，到黄昏，如同打翻的调色盘一般.</p>
                        </div>
                    </a>
                </li>
                <li class="mui-table-view-cell mui-media">
                    <a href="javascript:;">
                        <img class="mui-media-object mui-pull-left" src="images/shuijiao.jpg">
                        <div class="mui-media-body">
                            <p class='mui-ellipsis'>能和心爱的人一起睡觉，是件幸福的事情；可是，打呼噜怎么办？</p>
                        </div>
                    </a>
                </li>
                <li class="mui-table-view-cell mui-media">
                    <a href="javascript:;">
                        <img class="mui-media-object mui-pull-left" src="images/muwu.jpg">
                        <div class="mui-media-body">
                            木屋
                            <p class='mui-ellipsis'>想要这样一间小木屋，夏天挫冰吃瓜，冬天围炉取暖.</p>
                        </div>
                    </a>
                </li>--%>
            </ul>
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
        var pageIndex = 0;
        var totalCount = -1;
        var pageSize = 5;

        $(document).ready(function () {
            var jsonData = $("#firstPageData").val();
            var currentPageData = JSON.parse(jsonData);

            appendData(currentPageData);
        });

        function appendData(pageData) {
            pageIndex = pageData.pageIndex;
            totalCount = pageData.totalCount;
            pageSize = pageData.pageSize;

            $.each(pageData.events, function (i, data) {
                var li = $("<li>").addClass("mui-table-view-cell mui-media").appendTo("#listContainer");
                //li.click(function () { window.location.href = 'a.aspx' });
                var anchor = $("<a>").attr("href", "../forms/EventPlayer.aspx?id=" + data.id).appendTo(li);
                var img = $("<img>").attr("src", data.logo).addClass("mui-media-object mui-pull-left").appendTo(anchor);
                var div = $("<div>").addClass("mui-media-body").text(data.name).appendTo(anchor);

                $("<p>").addClass("mui-ellipsis").text(data.description).appendTo(div);
            });
        }

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
                        $.getJSON("../services/QueryService.ashx?opType=AllEvents", function (data) {

                            if (typeof (data.stackTrace) != "undefined") {
                                showBack.error("对不起，网络连接异常");
                                console.error(data.message);
                            }
                            else {
                                $("#listContainer").empty();
                                appendData(data);
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
                    }
                },
                up: {
                    contentrefresh: "正在加载...", //可选，正在加载状态时，上拉加载控件上显示的标题内容
                    contentnomore: '没有更多数据了', //可选，请求完毕若没有更多数据时显示的提醒内容；
                    callback: function () {
                        if ((pageIndex + 1) * pageSize < totalCount) {
                            var url = "../services/QueryService.ashx?opType=AllEvents&pageIndex=" + pageIndex + 1 + "&totalCount=" + totalCount;

                            $.getJSON(url, function (data) {
                                if (typeof (data.stackTrace) != "undefined") {
                                    showBack.error("对不起，网络连接异常");
                                    console.error(data.message);
                                }
                                else {
                                    appendData(data);
                                }
                            }).done(function () {
                                console.log("second success");
                                mui('#refreshContainer').pullRefresh().endPullupToRefresh();
                            }).fail(function (e) {
                                console.log("error");
                                showBack.error("对不起，网络连接异常");

                            }).always(function () {
                                mui('#refreshContainer').pullRefresh().endPullupToRefresh();
                            });
                        }
                        else {
                            mui('#refreshContainer').pullRefresh().endPullupToRefresh();
                        }
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
</body>
</html>
