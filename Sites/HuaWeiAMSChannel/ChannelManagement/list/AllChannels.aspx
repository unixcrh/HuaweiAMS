<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllChannels.aspx.cs" Inherits="ChannelManagement.list.AllChannels" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>所有频道</title>
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
                    <asp:HyperLinkField HeaderText="事件" Text="查看..." DataNavigateUrlFields="ID" DataNavigateUrlFormatString="../list/EventsInChannel.aspx?channelID={0}" />
                </Columns>
                <PagerSettings Position="TopAndBottom" Mode="NextPreviousFirstLast" />
            </res:DeluxeGrid>
        </div>
    </form>
</body>
</html>