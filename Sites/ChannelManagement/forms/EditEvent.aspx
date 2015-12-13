<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditEvent.aspx.cs" Inherits="ChannelManagement.forms.EditEvent" %>

<%@ Register Src="~/Templates/ChannelHeader.ascx" TagPrefix="ams" TagName="ChannelHeader" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>编辑事件</title>
    <%--<script src="../scripts/jquery-2.1.4.min.js"></script>--%>
    <style type="text/css">
        .poster {
            width: 320px;
            height: 200px;
            border: silver 1px solid;
        }

        .logo {
            width: 200px;
            height: 120px;
            border: silver 1px solid;
        }
    </style>
    <script>
        $(document).ready(function () {
            $("#uploadPoster").click(function () {
                doUpload("poster");

                return false;
            });

            $("#uploadLogo").click(function () {
                doUpload("logo");

                return false;
            });

            generateFrame();

            $("#responseButton").click(function () {

                var resultUrl = $("#responseUrl").val();

                if (resultUrl.indexOf("Error:") == -1) {
                    switch ($("#fileType").val()) {
                        case "poster":
                            $("#poster").attr("src", resultUrl);
                            $("#PosterUrl").val(resultUrl);
                            break;
                        case "logo":
                            $("#logo").attr("src", resultUrl);
                            $("#LogoUrl").val(resultUrl);
                            break;
                    }
                }
                else
                    alert(resultUrl);

                setTimeout(function () {
                    generateFrame();
                }, 10);
            });

            $(".poster").attr("src", $("#PosterUrl").val());
            $(".logo").attr("src", $("#LogoUrl").val());
        });

        function doUpload(fileType) {
            $("#uploadFile").change(function () {
                if ($(this).val() != "") {
                    $("#fileID").val($("#EventID").val());
                    $("#fileType").val(fileType);

                    $("#fileSubmit").click();
                }
            });

            $("#uploadFile").click();
        }

        function generateFrame() {
            $("#frameContainer").empty();
            var frame = $("<iframe>").attr("name", "imageFrame").appendTo("#frameContainer");
        }
    </script>
</head>
<body>
    <div class="container">
        <ams:ChannelHeader runat="server" ID="ChannelHeader" CurrentName="编辑频道" />
        <form id="serverForm" runat="server" class="form-horizontal">
            <res:DataBindingControl runat="server" ID="bindingControl" IsValidateOnSubmit="true" ValidateUnbindProperties="false">
                <ItemBindings>
                    <res:DataBindingItem DataPropertyName="ID" ControlID="EventID" ControlPropertyName="Value" />
                    <res:DataBindingItem DataPropertyName="Name" ControlID="Name" />
                    <res:DataBindingItem DataPropertyName="Description" ControlID="Description" />
                    <res:DataBindingItem DataPropertyName="Speakers" ControlID="Speakers" ControlPropertyName="Text" />
                    <res:DataBindingItem DataPropertyName="StartTime" ControlID="StartTime" />
                    <res:DataBindingItem DataPropertyName="EndTime" ControlID="EndTime" />
                    <res:DataBindingItem DataPropertyName="PosterUrl" ControlID="PosterUrl" ControlPropertyName="Value" />
                    <res:DataBindingItem DataPropertyName="LogoUrl" ControlID="LogoUrl" ControlPropertyName="Value" />
                </ItemBindings>
            </res:DataBindingControl>
            <input runat="server" type="hidden" id="EventID" />
            <div class="form-group form-group-sm">
                <label class="col-sm-2 control-label" for="Name">事件名称</label>
                <div class="col-sm-10">
                    <asp:TextBox runat="server" CssClass="form-control" type="text" ID="Name" placeholder="事件名称" />
                </div>
            </div>
            <div class="form-group form-group-sm">
                <label class="col-sm-2 control-label" for="Name">演讲者</label>
                <div class="col-sm-10">
                    <asp:TextBox runat="server" CssClass="form-control" type="text" ID="Speakers" placeholder="演讲者" />
                </div>
            </div>
            <div class="form-group form-group-sm">
                <label class="col-sm-2 control-label" for="Name">事件描述</label>
                <div class="col-sm-10">
                    <asp:TextBox runat="server" CssClass="form-control" Rows="5" type="text" ID="Description" TextMode="MultiLine" placeholder="事件描述" />
                </div>
            </div>
            <div class="form-group form-group-sm">
                <label class="col-sm-2 control-label" for="Name">起始时间</label>
                <div class="col-sm-10">
                    <res:DateTimePicker runat="server" ID="StartTime" Mode="DateTimePicker" />
                </div>
            </div>
            <div class="form-group form-group-sm">
                <label class="col-sm-2 control-label" for="Name">结束时间</label>
                <div class="col-sm-10">
                    <res:DateTimePicker runat="server" ID="EndTime" Mode="DateTimePicker" />
                </div>
            </div>
            <div class="form-group form-group-sm">
                <input type="hidden" runat="server" id="PosterUrl" />
                <label class="col-sm-2 control-label" for="Name">海报</label>
                <div class="col-sm-4" id="posterContainer">
                    <img id="poster" class="poster" />
                </div>
                <div class="col-sm-6">
                    <button id="uploadPoster" class="btn btn-default">上传...</button>
                    <%--<div class="progress progress-striped active">
                        <div class="bar" style="width: 40%;"></div>
                    </div>--%>
                </div>
            </div>
            <div class="form-group form-group-sm">
                <input type="hidden" runat="server" id="LogoUrl" />
                <label class="col-sm-2 control-label" for="Name">图标</label>
                <div class="col-sm-4">
                    <img id="logo" class="logo" />
                </div>
                <div class="col-sm-6">
                    <button id="uploadLogo" class="btn btn-default">上传...</button>
                </div>
            </div>
            <div class="form-group form-group-sm">
                <div class="col-sm-8"></div>
                <div class="col-sm-4" style="">
                    <asp:Button runat="server" AccessKey="S" CssClass="btn btn-primary" ID="save" Text="保存(S)" OnClick="save_Click" />
                    <a runat="server" id="backUrl" class="btn btn-default" href="#">返回</a>
                </div>
            </div>
        </form>
        <div class="hidden">
            <input id="responseUrl" type="hidden" />
            <input id="responseButton" type="button" />
            <form action="UploadReceiver.ashx" target="imageFrame" method="post" enctype="multipart/form-data">
                <input type="hidden" id="fileID" name="fileID" />
                <input type="hidden" id="fileType" name="fileType" />
                <input type="file" id="uploadFile" name="uploadFile" />
                <input type="submit" id="fileSubmit" />
            </form>
            <div id="frameContainer">
                <%--<iframe name="imageFrame"></iframe>--%>
            </div>
        </div>
    </div>
</body>
</html>
