<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllChannels.aspx.cs" Inherits="ChannelManagement.list.AllChannels" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>所有频道</title>
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

        //function onCopyRtmpClick(a)
        //{
        //    event.returnValue = false;
        //    var text = $(a).next().attr("href");

        //    if (window.clipboardData)
        //    {
        //        window.clipboardData.clearData();

        //        window.clipboardData.setData("Text", text);

        //        alert("已经成功复制到剪帖板上！");
        //    }

        //    return false;
        //}
    </script>
</head>
<body>
    <form id="serverForm" runat="server">
        <div class="container">
            <res:DeluxeGrid ID="dataGrid" runat="server" GridLines="None" AllowPaging="True"
                GridTitle="频道" EnableViewState="false" AutoGenerateColumns="False" UseAccessibleHeader="False"
                ShowExportControl="True"
                DataKeyNames="ID" AllowSorting="True" ShowCheckBoxes="false" CheckBoxPosition="Left">
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
                    <asp:BoundField DataField="State" HeaderText="状态">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="State" HeaderText="状态">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="地址">
                        <ItemTemplate>
                            <%--<a class="btn btn-xs btn-info" onclick="onCopyRtmpClick(this);">
                                <span class="blue">
                                    <i class="icon-copy bigger-120"></i>
                                </span>
                            </a>--%>
                            <a href="<%# HttpUtility.HtmlAttributeEncode((string)Eval("PrimaryInputUrl"))%>" onclick="onRtmpClick(this);">显示...</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:HyperLinkField HeaderText="事件" Text="查看..." DataNavigateUrlFields="ID" DataNavigateUrlFormatString="../list/EventsInChannel.aspx?channelID={0}" />
                </Columns>
                <PagerSettings Position="TopAndBottom" Mode="NextPreviousFirstLast" />
            </res:DeluxeGrid>
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
