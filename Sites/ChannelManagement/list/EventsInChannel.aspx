<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EventsInChannel.aspx.cs" Inherits="ChannelManagement.list.EventsInChannel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>频道中的事件</title>
    <script type="text/javascript">
        function onDeleteEventButtonClick()
        {
            var canDelete = $find("dataGrid").get_clientSelectedKeys().length > 0;

            if (canDelete > 0)
                canDelete = window.confirm("确定删除事件吗？");
            else
                $HGClientMsg.stop("请选择需要删除的事件", "", "错误");

            return canDelete;
        }
    </script>
</head>
<body>
    <form id="serverForm" runat="server">
        <div class="container">
            <div>
                <a runat="server" id="addEventButton" class="btn btn-success">增加事件...</a>
                <asp:Button runat="server" ID="deleteEventButton" class="btn btn-default" Text="删除事件..." OnClientClick="return onDeleteEventButtonClick();" OnClick="deleteEventButton_Click"/>
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
                    <asp:HyperLinkField HeaderText="事件" DataTextField="Name" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="../forms/EditEvent.aspx?id={0}" />
                    <asp:TemplateField HeaderText="开始时间">
                        <ItemTemplate>
                            <%#((DateTime)Eval("StartTime")) == DateTime.MinValue ? string.Empty : ((DateTime)Eval("StartTime")).ToString("yyyy-MM-dd HH:mm:ss")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="开始时间">
                        <ItemTemplate>
                            <%#((DateTime)Eval("EndTime")) == DateTime.MinValue ? string.Empty : ((DateTime)Eval("EndTime")).ToString("yyyy-MM-dd HH:mm:ss")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="State" HeaderText="状态">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                </Columns>
                <PagerSettings Position="TopAndBottom" Mode="NextPreviousFirstLast" />
            </res:DeluxeGrid>
            <res:DeluxeObjectDataSource runat="server" ID="eventDataSource" EnablePaging="true" TypeName="MCS.Library.Cloud.AMS.Data.DataSources.AMSEventDataSource" OnSelecting="eventDataSource_Selecting">
            </res:DeluxeObjectDataSource>
            <div>
                <asp:LinkButton ID="refreshBtn" runat="server" OnClick="refreshBtn_Click"></asp:LinkButton>
            </div>
        </div>
    </form>
</body>
</html>
