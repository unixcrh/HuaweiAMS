﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EventsInChannel.aspx.cs" Inherits="ChannelManagement.list.EventsInChannel" %>

<%@ Register Src="~/Templates/ChannelHeader.ascx" TagPrefix="ams" TagName="ChannelHeader" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>频道中的事件</title>
    <script type="text/javascript">
        function onDeleteEventButtonClick() {
            var canDelete = $find("dataGrid").get_clientSelectedKeys().length > 0;

            if (canDelete > 0)
                canDelete = window.confirm("确定删除事件吗？");
            else
                $HGClientMsg.stop("请选择需要删除的事件", "", "错误");

            return canDelete;
        }

        function onStopEventButtonClick() {
            var canDelete = $find("dataGrid").get_clientSelectedKeys().length > 0;

            if (canDelete > 0)
                canDelete = window.confirm("确定停止事件吗？");
            else
                $HGClientMsg.stop("请选择需要停止的事件", "", "错误");

            return canDelete;
        }

        function onDeleteEventClick(id) {
            if (window.confirm("确定删除事件吗？") == false)
                return false;
        }

        function onPlaybackUrlClick(a) {
            event.returnValue = false;
            $("#playbackUrl").text(a.href);

            var options = {
                title: "播放地址",
                width: 520,
                height: 280,
                okBtn: {
                    visible: false,
                    text: "确定"
                },
                cancelBtn: {
                    visible: true,
                    text: "关闭"
                },
                onOk: function () {
                },
                control: {
                    id: 'playbackUrlContainer',
                    clone: false
                }
            };
            $HGModalBox.show(options);

            return false;
        }
    </script>
</head>
<body>
    <form id="serverForm" runat="server">
        <div class="container">
            <ams:ChannelHeader runat="server" ID="ChannelHeader" CurrentName="事件列表" />
            <div>
                <a runat="server" id="addEventButton" class="btn btn-success">增加事件...</a>
                <asp:Button runat="server" ID="deleteEventButton" class="btn btn-danger" Text="删除事件..." OnClientClick="return onDeleteEventButtonClick();" OnClick="deleteEventButton_Click" />
                <asp:Button runat="server" ID="stopEventButton" class="btn btn-warning" Text="停止事件..." OnClientClick="return onStopEventButtonClick();" OnClick="stopEventButton_Click" />
            </div>
            <res:DeluxeGrid ID="dataGrid" runat="server" GridLines="None" AllowPaging="True" ShowCheckBoxes="true"
                GridTitle="事件列表" EnableViewState="false" AutoGenerateColumns="False" UseAccessibleHeader="False"
                ShowExportControl="True" DataSourceID="eventDataSource"
                DataKeyNames="ID" AllowSorting="True" CheckBoxPosition="Left" EmptyDataText="该频道下没有事件">
                <FooterStyle />
                <RowStyle />
                <EditRowStyle />
                <SelectedRowStyle />
                <PagerStyle />
                <HeaderStyle />
                <AlternatingRowStyle />
                <Columns>
                    <asp:HyperLinkField SortExpression="Name" HeaderText="事件" DataTextField="Name" DataNavigateUrlFields="ChannelID,ID" DataNavigateUrlFormatString="../forms/EditEvent.aspx?channelID={0}&id={1}" />
                    <asp:TemplateField HeaderText="开始时间" SortExpression="StartTime">
                        <ItemTemplate>
                            <%#((DateTime)Eval("StartTime")) == DateTime.MinValue ? string.Empty : ((DateTime)Eval("StartTime")).ToString("yyyy-MM-dd HH:mm:ss")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="结束时间" SortExpression="EndTime">
                        <ItemTemplate>
                            <%#((DateTime)Eval("EndTime")) == DateTime.MinValue ? string.Empty : ((DateTime)Eval("EndTime")).ToString("yyyy-MM-dd HH:mm:ss")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="State" HeaderText="状态" SortExpression="State">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:HyperLinkField HeaderText="频道" Text="查看..." DataNavigateUrlFields="ChannelID,ID" DataNavigateUrlFormatString="RelativeChannels.aspx?channelID={0}&id={1}" />
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <div class="visible-md visible-lg hidden-sm hidden-xs btn-group">
                                <a title="播放..." class="btn btn-xs btn-success" href='../forms/EventPlayer.aspx?channelID=<%#Eval("ChannelID") %>&id=<%#Eval("ID") %>'>
                                    <i class="icon-play bigger-120"></i>
                                </a>

                                <a title="编辑..." class="btn btn-xs btn-info" href="../forms/EditEvent.aspx?channelID=<%#Eval("ChannelID") %>&id=<%#Eval("ID") %>">
                                    <i class="icon-edit bigger-120"></i>
                                </a>
                                <a title="播放地址..." class="btn btn-xs btn-info" href="http://amshuawei-customer.azurewebsites.net/forms/NoMuiEventPlayer.aspx?channelID=<%#Eval("ChannelID") %>&id=<%#Eval("ID") %>" onclick="return onPlaybackUrlClick(this);">
                                    <i class="icon-share bigger-120"></i>
                                </a>
                            </div>
                            <div class="visible-xs visible-sm hidden-md hidden-lg">
                                <div class="dropdown">
                                    <button class="btn btn-minier btn-primary dropdown-toggle" data-toggle="dropdown">
                                        <i class="icon-cog icon-only bigger-110"></i>
                                    </button>
                                    <ul class="dropdown-menu dropdown-only-icon dropdown-yellow pull-right dropdown-caret dropdown-close">
                                        <li>
                                            <a title="播放..." href='../forms/EventPlayer.aspx?channelID=<%#Eval("ChannelID") %>&id=<%#Eval("ID") %>' class="tooltip-info" data-rel="tooltip" data-original-title="View">
                                                <span class="blue">
                                                    <i class="icon-play bigger-120"></i>
                                                </span>
                                            </a>
                                        </li>
                                        <li>
                                            <a title="编辑..." href="../forms/EditEvent.aspx?channelID=<%#Eval("ChannelID") %>&id=<%#Eval("ID") %>" class="tooltip-success" data-rel="tooltip" data-original-title="Edit">
                                                <span class="green">
                                                    <i class="icon-edit bigger-120"></i>
                                                </span>
                                            </a>
                                        </li>
                                        <li>
                                            <a title="播放地址..." href="http://amshuawei-customer.azurewebsites.net/forms/NoMuiEventPlayer.aspx?channelID=<%#Eval("ChannelID") %>&id=<%#Eval("ID") %>" class="tooltip-success" data-rel="tooltip" data-original-title="Edit" onclick="return onPlaybackUrlClick(this);">
                                                <span class="green">
                                                    <i class="icon-share bigger-120"></i>
                                                </span>
                                            </a>
                                        </li>
                                        <%--<li>
                                            <a href="#" class="tooltip-error" data-rel="tooltip" title="" data-original-title="Delete">
                                                <span class="red">
                                                    <i class="icon-trash bigger-120"></i>
                                                </span>
                                            </a>
                                        </li>--%>
                                    </ul>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerSettings Position="TopAndBottom" Mode="NextPreviousFirstLast" />
            </res:DeluxeGrid>
            <res:DeluxeObjectDataSource runat="server" ID="eventDataSource" EnablePaging="true" TypeName="MCS.Library.Cloud.AMS.Data.DataSources.AMSEventDataSource" OnSelecting="eventDataSource_Selecting">
            </res:DeluxeObjectDataSource>
            <div>
                <asp:LinkButton ID="refreshBtn" runat="server" OnClick="refreshBtn_Click"></asp:LinkButton>
            </div>
            <div id="playbackUrlContainer" class="modal-body" style="display: none">
                <div class="form-group">
                    <label class="col-md-3 control-lable" for="playbackUrl">
                        播放地址：</label>
                    <div class="col-md-9">
                        <textarea id="playbackUrl" class="form-control" readonly="readonly" rows="5"></textarea>
                    </div>
                </div>
                <br />
                <br />
            </div>
        </div>
    </form>
</body>
</html>
