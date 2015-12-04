<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EventsInChannel.aspx.cs" Inherits="ChannelManagement.list.EventsInChannel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>频道中的事件</title>
</head>
<body>
    <form id="serverForm" runat="server">
        <div class="container">
            <div>
                <a runat="server" id="addEventButton" class="btn btn-success">增加事件...</a>
            </div>
            <res:DeluxeGrid ID="dataGrid" runat="server" GridLines="None" AllowPaging="True"
                GridTitle="事件列表" EnableViewState="false" AutoGenerateColumns="False" UseAccessibleHeader="False"
                ShowExportControl="True" DataSourceID="eventDataSource"
                DataKeyNames="ID" AllowSorting="True" CheckBoxPosition="Left">
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
                    <asp:BoundField DataField="State" HeaderText="状态">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                </Columns>
                <PagerSettings Position="TopAndBottom" Mode="NextPreviousFirstLast" />
            </res:DeluxeGrid>
            <res:DeluxeObjectDataSource runat="server" ID="eventDataSource" EnablePaging="true" TypeName="MCS.Library.Cloud.AMS.Data.DataSources.AMSEventDataSource" OnSelecting="eventDataSource_Selecting">
            </res:DeluxeObjectDataSource>
        </div>
    </form>
</body>
</html>
