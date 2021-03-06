﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompletedEvents.aspx.cs" Inherits="CutomerSite.list.CompletedEvents" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>往期直播</title>
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
    </style>
</head>
<body>
    <div>
        <input runat="server" id="firstPageData" />
        <input runat="server" id="totalCount" />
        <input runat="server" id="pageSize" />
    </div>
    <div id="headerContainer" class="hidden">
    </div>
    <header id="header" class="mui-bar mui-bar-nav">
        <a class="mui-icon mui-icon-arrowleft mui-pull-left"></a>
        <h1 class="mui-title">往期直播</h1>
        <a id="menu" class="mui-action-menu mui-icon mui-icon-bars mui-pull-right" href="#topPopover"></a>
    </header>
    <div runat="server" id="uid"></div>
    <div id="refreshContainer" class="mui-content mui-scroll-wrapper">
        <div class="mui-scroll">
            <!--数据列表-->
            <ul id="listContainer" class="mui-table-view">
            </ul>
        </div>
    </div>
    <div id="menu_back" class="menu_back"></div>
    <!--右上角弹出菜单-->
    <div id="topPopover" class="mui-popover">
        <div class="mui-popover-arrow"></div>
        <div class="mui-scroll-wrapper">
            <div class="mui-scroll">
                <ul class="mui-table-view">
                    <li class="mui-table-view-cell"><a href="UpcomingEvents.aspx">即将直播</a>
                    </li>
                    <li class="mui-table-view-cell"><a href="CompletedEvents.aspx">往期直播</a>
                    </li>
                </ul>
            </div>
        </div>
    </div>
    <script>
        var pageIndex = 0;
        var totalCount = -1;
        var pageSize = 5;

        var serviceURL = "../services/QueryService.ashx?opType=AllEvents";

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

            $.each(pageData.events, function (i, data) {
                var li = $("<li>").addClass("mui-table-view-cell mui-media").appendTo("#listContainer");

                var anchor = $("<a>").attr("href", appendTimeOffsetToUrl("../forms/NoMuiEventPlayer.aspx?id=" + data.id)).appendTo(li);
                var img = $("<img>").attr("src", data.logo).addClass("mui-media-object mui-pull-left").appendTo(anchor);
                var div = $("<div>").addClass("mui-media-body").text(data.name).appendTo(anchor);

                $("<p>").addClass("mui-ellipsis").text(data.speakers).appendTo(div);
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
                            var url = "../services/QueryService.ashx?opType=AllEvents&pageIndex=" + pageIndex + 1 + "&totalCount=" + totalCount;

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
        //点击左上角侧滑图标，打开侧滑菜单；
        //document.querySelector('.mui-icon-bars').addEventListener('tap', function (e) {
        //    $("#menu").animate({
        //        marginLeft: '0%'
        //    }, 100);
        //    $("#menu_back").show();
        //});
        document.querySelector('.menu_back').addEventListener('tap', function (e) {
            $("#menu").animate({
                marginLeft: '-100%'
            }, 300);
            $("#menu_back").hide();
        });


    </script>
</body>
</html>

