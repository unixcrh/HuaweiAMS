<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MoreMenu.aspx.cs" Inherits="CutomerSite.list.MoreMenu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>更多...</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <script src="../scripts/jstz.main.js"></script>
    <script src="../scripts/jstz.rules.js"></script>
    <script src="../scripts/amsCommon.js"></script>

    <style type="text/css">
        * {
            padding: 0px;
            margin: 0px;
        }

        html, body {
            position: relative;
            height: 100%;
        }

        body {
            font-family: Microsoft YaHei;
            font-size: 16px;
            color: #000;
            margin: 0;
            padding: 0;
        }

        .hide {
            display: none;
        }

        .myddDiv {
            height: 100%;
            width: 100%;
            background-color: #EBEBEB;
            position: absolute;
        }

            .myddDiv .doudou-item {
                height: 50px;
                background-color: #FFFFFF;
                clear: both;
                position: relative;
                overflow: hidden;
            }

                .myddDiv .doudou-item .div-img {
                    text-align: center;
                    height: 100%;
                    position: absolute;
                    left: 3%;
                    top: 0;
                }

                .myddDiv .doudou-item .icon {
                    position: absolute;
                    height: 60%;
                    top: 10px;
                }

                .myddDiv .doudou-item .title {
                    height: 60%;
                    line-height: 100%;
                    position: absolute;
                    left: 14%;
                    top: 35%;
                    color: black;
                }

                .myddDiv .doudou-item .arrow {
                    height: 50%;
                    position: absolute;
                    right: 3%;
                    top: 25%;
                }

                .myddDiv .doudou-item .paocount {
                    position: absolute;
                    border-radius: 18px;
                    background-color: #f44336;
                    right: 11%;
                    top: 16px;
                    color: #ffffff;
                    font-size: 12px;
                    padding: 3px 5px 0px 5px;
                    height: 15px;
                    line-height: 15px;
                    text-align: center;
                    min-width: 8px;
                }

            .myddDiv .his-item {
                clear: both;
                position: relative;
                height: 76%;
            }

                .myddDiv .his-item .hisDiv {
                    border-bottom: 1px solid #CCD1D9;
                    height: 14%;
                    background-color: #f9f9f9;
                }

                    .myddDiv .his-item .hisDiv .hisImgDiv {
                        text-align: center;
                        height: 100%;
                        float: left;
                        margin-left: 10px;
                    }

                        .myddDiv .his-item .hisDiv .hisImgDiv .hisImg {
                            height: 60%;
                        }

        .hisInfoDiv {
            padding-top: 2%;
            height: 80%;
            width: 80%;
            margin-left: 15%;
        }

            .hisInfoDiv .hisContent {
                white-space: nowrap;
                overflow: hidden;
                text-overflow: ellipsis;
                font-size: 0.9em;
                padding-top: 2%;
            }

            .hisInfoDiv .hisTime {
                margin-top: 1.5%;
                font-size: 0.7em;
                color: #999999;
            }

        .soundbtn {
            position: absolute;
            height: 100%;
            width: 17%;
            right: 3%;
            border: 0;
            background-color: #FFFFFF;
            outline: none;
            /* border-radius: 25% / 50%; */
        }

        .soundimg {
            height: 50%;
            margin-top: 3px;
        }
    </style>
    <script type="text/javascript">
        function onDocumentLoad() {
            appendTimeOffset(document.getElementById("upcomingEventsLink"));
            appendTimeOffset(document.getElementById("completedEventsLink"));
        }

        function appendTimeOffset(link) {
            link.href = appendTimeOffsetToUrl(link.href);
        }
    </script>
</head>
<body onload="onDocumentLoad();">
    <div class="myddDiv" id="myddDiv">
        <div class="doudou-item" style="margin: 10px 0px" id="secret">
            <a id="upcomingEventsLink" href="UpcomingEvents.aspx">
                <div class="div-img" id="cImgDiv">
                    <img class="icon" id="cImg" src="../images/movie.png" />
                </div>
                <div class="title" id="cNameDiv">即将直播</div>
                <img id="ctImg" class="arrow" src="../images/rightArrow.png" />
            </a>
        </div>
        <div class="doudou-item" id="notice">
            <a id="completedEventsLink" href="CompletedEvents.aspx">
                <div class="div-img" id="cImgDiv">
                    <img class="icon" id="cImg" src="../images/movie.png" />
                </div>
                <div class="title" id="cNameDiv">往期直播</div>
                <img id="ctImg" class="arrow" src="../images/rightArrow.png" />
            </a>
        </div>
        <div class="doudou-item" id="notice">
            <a href="http://amsplayer.azurewebsites.net/azuremediaplayer.html?url=http%3a%2f%2famshuaweichn.streaming.mediaservices.chinacloudapi.cn%2f94706d4f-ea13-4eb4-a938-a58c9b9a0897%2fXBox%2520Video.ism%2fmanifest">
                <div class="div-img" id="cImgDiv">
                    <img class="icon" id="cImg" src="../images/movie.png" />
                </div>
                <div class="title" id="cNameDiv">内部测试</div>
                <img id="ctImg" class="arrow" src="../images/rightArrow.png" />
            </a>
        </div>
    </div>
</body>
</html>
