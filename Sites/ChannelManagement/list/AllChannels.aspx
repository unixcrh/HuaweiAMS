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
                GridTitle="订单列表" EnableViewState="false" AutoGenerateColumns="False" UseAccessibleHeader="False"
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
                </Columns>
                <PagerSettings Position="TopAndBottom" Mode="NextPreviousFirstLast" />
            </res:DeluxeGrid>
            <%--<asp:ObjectDataSource ID="objectDataSource" runat="server" SelectCountMethod="GetFilteredDataCount"
                SelectMethod="GetFilteredData" TypeName="MCS.Web.Responsive.WebControls.Test.DeluxeGrid.OrdersDataViewAdapter"
                EnablePaging="True" SortParameterName="sortExpression">
                <SelectParameters>
                    <asp:ControlParameter ControlID="prioritySelector" PropertyName="SelectedValue" Name="priority"
                        Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>--%>
        </div>
    </form>
</body>
</html>
