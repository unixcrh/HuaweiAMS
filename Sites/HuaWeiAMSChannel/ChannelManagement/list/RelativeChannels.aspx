<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelativeChannels.aspx.cs" Inherits="ChannelManagement.list.RelativeChannels" %>

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
    </script>
</head>
<body>
    <form id="serverForm" runat="server">
        <div class="container">
            <div>
                <a runat="server" id="addChannelButton" class="btn btn-success">增加频道...</a>
            </div>
            <res:DeluxeGrid ID="dataGrid" runat="server" GridLines="None" AllowPaging="True" ShowCheckBoxes="true"
                GridTitle="事件列表" EnableViewState="false" AutoGenerateColumns="False" UseAccessibleHeader="False"
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
                    <asp:BoundField DataField="Name" HeaderText="名称">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="AMSAccountName" HeaderText="媒体账户">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Description" HeaderText="描述">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="State" HeaderText="状态" SortExpression="State">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
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
    </form>
</body>
</html>
