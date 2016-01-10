<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelativeChannels.aspx.cs" Inherits="ChannelManagement.list.RelativeChannels" %>

<%@ Register Src="~/Templates/ChannelHeader.ascx" TagPrefix="ams" TagName="ChannelHeader" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>相关频道</title>
    <script type="text/javascript">
        function onRtmpClick(a) {
            event.returnValue = false;
            $("#rtmpUrl").text(a.href);

            var options = {
                title: "视频源地址",
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
                    id: 'inputSource',
                    clone: false
                }
            };
            $HGModalBox.show(options);

            return false;
        }

        function onAddChannelClick(a) {
            event.returnValue = false;

            var options = {
                title: "增加频道",
                width: 520,
                height: 280,
                okBtn: {
                    visible: true,
                    text: "确定"
                },
                cancelBtn: {
                    visible: true,
                    text: "取消"
                },
                onOk: function () {
                    document.getElementById("postAddChannelBtn").click();
                },
                control: {
                    id: 'addChannel',
                    clone: false
                }
            };
            $HGModalBox.show(options);

            return false;
        }

        function onDeleteChannelButtonClick() {
            var canDelete = $find("dataGrid").get_clientSelectedKeys().length > 0;

            if (canDelete > 0)
                canDelete = window.confirm("确定删除频道吗？");
            else
                $HGClientMsg.stop("请选择需要删除的频道", "", "错误");

            return canDelete;
        }
    </script>
</head>
<body>
    <form id="serverForm" runat="server">
        <div class="container">
            <ams:ChannelHeader runat="server" ID="ChannelHeader" CurrentName="相关频道列表" />
            <div>
                <a runat="server" id="addChannelButton" class="btn btn-success" onclick="return onAddChannelClick();">增加频道...</a>
                <asp:Button runat="server" ID="deleteEventButton" class="btn btn-default" Text="删除频道..." OnClientClick="return onDeleteChannelButtonClick();" OnClick="deleteChannelButton_Click" />
            </div>
            <res:DeluxeGrid ID="dataGrid" runat="server" GridLines="None" AllowPaging="True" ShowCheckBoxes="true"
                GridTitle="相关频道" EnableViewState="false" AutoGenerateColumns="False" UseAccessibleHeader="False"
                ShowExportControl="True" DataSourceID="eventChannelDataSource"
                DataKeyNames="ID" AllowSorting="True" CheckBoxPosition="Left" EmptyDataText="该事件下没有频道">
                <FooterStyle />
                <RowStyle />
                <EditRowStyle />
                <SelectedRowStyle />
                <PagerStyle />
                <HeaderStyle />
                <AlternatingRowStyle />
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="名称" SortExpression="Name">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="AMSAccountName" HeaderText="媒体账户" SortExpression="AMSAccountName" HeaderStyle-CssClass="visible-md visible-lg hidden-sm">
                        <ItemStyle HorizontalAlign="Left" CssClass="visible-md visible-lg hidden-sm" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Description" HeaderText="描述" SortExpression="Description" HeaderStyle-CssClass="visible-md visible-lg hidden-sm">
                        <ItemStyle HorizontalAlign="Left" CssClass="visible-md visible-lg hidden-sm" />
                    </asp:BoundField>
                    <asp:BoundField DataField="State" HeaderText="状态" SortExpression="State">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="默认" HeaderStyle-CssClass="visible-md visible-lg hidden-sm hidden-xs" ItemStyle-CssClass="visible-md visible-lg hidden-sm hidden-xs">
                        <ItemTemplate>
                            <%# (bool)(Eval("IsDefault")) ? "是" : "否" %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="地址">
                        <ItemTemplate>
                            <a href="<%# HttpUtility.HtmlAttributeEncode((string)Eval("PrimaryInputUrl"))%>" onclick="return onRtmpClick(this);">显示...</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <div class="visible-md visible-lg hidden-sm hidden-xs btn-group">
                                <a title="播放..." class="btn btn-xs btn-success" href='../forms/EventPlayer.aspx?channelID=<%#Eval("ID") %>&id=<%#Eval("EventID") %>'>
                                    <i class="icon-play bigger-120"></i>
                                </a>
                            </div>
                            <div class="visible-xs visible-sm hidden-md hidden-lg">
                                <div class="dropdown">
                                    <button class="btn btn-minier btn-primary dropdown-toggle" data-toggle="dropdown">
                                        <i class="icon-cog icon-only bigger-110"></i>
                                    </button>
                                    <ul class="dropdown-menu dropdown-only-icon dropdown-yellow pull-right dropdown-caret dropdown-close">
                                        <li>
                                            <a title="播放..." href='../forms/EventPlayer.aspx?channelID=<%#Eval("ID") %>&id=<%#Eval("EventID") %>' class="tooltip-info" data-rel="tooltip" data-original-title="View">
                                                <span class="blue">
                                                    <i class="icon-play bigger-120"></i>
                                                </span>
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerSettings Position="TopAndBottom" Mode="NextPreviousFirstLast" />
            </res:DeluxeGrid>
            <res:DeluxeObjectDataSource runat="server" ID="eventChannelDataSource" EnablePaging="true" TypeName="MCS.Library.Cloud.AMS.Data.DataSources.AMSChannelInEventDataSource" OnSelecting="eventChannelDataSource_Selecting">
            </res:DeluxeObjectDataSource>
            <div>
                <asp:LinkButton ID="refreshBtn" runat="server" OnClick="refreshBtn_Click"></asp:LinkButton>
            </div>
        </div>
        <div id="inputSource" class="modal-body" style="display: none">
            <div class="form-group">
                <label class="col-md-3 control-lable" for="rtmpUrl">
                    视频源：</label>
                <div class="col-md-9">
                    <textarea id="rtmpUrl" class="form-control" readonly="readonly" rows="5"></textarea>
                </div>
            </div>
            <br />
            <br />
        </div>
        <div id="addChannel" class="modal-body" style="display: none">
            <label class="col-md-3 control-lable" for="rtmpUrl">
                频道：</label>
            <div class="col-md-9">
                <asp:DropDownList runat="server" ID="unusedChannels" CssClass="form-control" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
                <asp:LinkButton ID="postAddChannelBtn" runat="server" CssClass="hidden" OnClick="postAddChannelBtn_Click" />
            </div>
        </div>
    </form>
</body>
</html>
