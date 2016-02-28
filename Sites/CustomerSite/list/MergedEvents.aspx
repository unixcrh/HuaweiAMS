<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MergedEvents.aspx.cs" Inherits="CutomerSite.list.MergedEvents" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>视频节目</title>
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1,user-scalable=no" />
    <link href="../css/mui/mui.min.css" rel="stylesheet" />
    <link href="../css/main.css" rel="stylesheet" />

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

        .smallFont {
            font-size: 9px;
        }

        .segmentBar {
            left: 0;
            right: 0;
            height: 32px;
            background-color: #ec971f;
            text-align: left;
            padding-left: 8px;
        }

        .segmentTitle {
            font-size: 17px;
            font-weight: 500;
            line-height: 32px;
            display: block;
            width: 100%;
            margin: 0 0px;
            padding: 0;
            text-align: center;
            white-space: nowrap;
            color: white;
        }
    </style>
</head>
<body>
    <div>
        <input runat="server" id="firstPageData" />
        <input runat="server" id="totalCount" />
        <input runat="server" id="pageSize" />
    </div>
    <div id="refreshContainer" class="mui-content mui-scroll-wrapper">
        <div class="mui-scroll">
            <header id="upcomingHeader" class="segmentBar">
                <h1 class="segmentTitle">即将直播</h1>
            </header>
            <!--数据列表-->
            <ul id="upcomingListContainer" class="mui-table-view">
            </ul>
            <header id="completedHeader" class="segmentBar">
                <h1 class="segmentTitle">往期直播</h1>
            </header>
            <!--数据列表-->
            <ul id="completedListContainer" class="mui-table-view">
            </ul>
        </div>
    </div>
    <div id="menu_back" class="menu_back"></div>
    <script>
        var pageIndex = 0;
        var totalCount = -1;
        var pageSize = 5;

        var serviceURL = "../services/QueryService.ashx?opType=MergedEvents";

        function getServiceURL() {
            return appendTimeOffsetToUrl(serviceURL);
        }

        $(document).ready(function () {
            ams.initTopBar();
            var jsonData = $("#firstPageData").val();
            var currentPageData = JSON.parse(jsonData);

            mui(".mui-table-view").on("tap", "a", function () {
                window.location.href = $(this).attr("href");
            });

            appendData(currentPageData);

            ams.initMenu();
            //initLoadData();
        });

        function initLoadData() {
            showBack.message("加载数据...", true);

            $.getJSON(getServiceURL(), function (data) {
                if (afterReloadAllData(data))
                    showBack.hide();
            }).done(function () {

            }).fail(function (e) {
                showBack.error("对不起，网络连接异常");
            }).always(function () {

            });
        }

        function appendData(pageData) {
            pageIndex = pageData.pageIndex;
            totalCount = pageData.totalCount;
            pageSize = pageData.pageSize;

            this.bindData($("#upcomingListContainer"), pageData.events.upcomingEvents);
            this.bindData($("#completedListContainer"), pageData.events.startedEvents);

            if (pageData.events.upcomingEvents.length == 0)
                $("#upcomingHeader").addClass("hidden");
            else
                $("#upcomingHeader").removeClass("hidden");

            if (pageData.events.startedEvents.length == 0)
                $("#completedHeader").addClass("hidden");
            else
                $("#completedHeader").removeClass("hidden");
        }

        function bindData(container, events) {
            $.each(events, function (i, data) {
                var li = $("<li>").addClass("mui-table-view-cell mui-media").appendTo(container);

                var anchor = $("<a>").attr("href", appendTimeOffsetToUrl("../forms/NoMuiEventPlayer.aspx?id=" + data.id)).appendTo(li);
                var img = $("<img>").attr("src", data.logo).addClass("mui-media-object mui-pull-left").appendTo(anchor);
                var div = $("<div>").addClass("mui-media-body").text(data.name).appendTo(anchor);

                var speaker = $("<p>").addClass("mui-ellipsis").text(data.speakers).appendTo(div);
                $("<p>").addClass("mui-ellipsis smallFont").text(data.timeDescription).appendTo(speaker);
            });
        }

        function afterReloadAllData(data) {
            var result = true;

            if (typeof (data.stackTrace) != "undefined") {
                result = false;
                showBack.error("对不起，网络连接异常");
            }
            else {
                $("#upcomingListContainer").empty();
                $("#completedListContainer").empty();
                appendData(data);
            }

            return result;
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
                        $.getJSON(getServiceURL(), function (data) {
                            afterReloadAllData(data);
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
                            var url = "../services/QueryService.ashx?opType=MergedEvents&pageIndex=" + pageIndex + 1 + "&totalCount=" + totalCount;

                            $.getJSON(appendTimeOffsetToUrl(url), function (data) {
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

        document.querySelector('.menu_back').addEventListener('tap', function (e) {
            $("#menu").animate({
                marginLeft: '-100%'
            }, 300);
            $("#menu_back").hide();
        });
    </script>
</body>
</html>
