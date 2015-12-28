<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EventsInChannel2.aspx.cs" Inherits="ChannelManagement.list.EventsInChannel2" %>

<%@ Register Src="~/Templates/ChannelHeader.ascx" TagPrefix="ams" TagName="ChannelHeader" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>频道中的事件</title>
</head>
<body>
    <form id="serverForm" runat="server">
        <div class="container">
            <ams:channelheader runat="server" id="ChannelHeader" currentname="事件列表" />
            <div>
                <a runat="server" id="addEventButton" class="btn btn-success">增加事件...</a>
            </div>
            <asp:GridView ID="dataGrid" runat="server" GridLines="None" AllowPaging="True"
                GridTitle="事件列表" EnableViewState="false" AutoGenerateColumns="False" UseAccessibleHeader="False"
                ShowExportControl="True" DataSourceID="eventDataSource" PageSize="5"
                DataKeyNames="ID" AllowSorting="True">
                <FooterStyle />
                <RowStyle />
                <EditRowStyle />
                <SelectedRowStyle />
                <PagerStyle />
                <HeaderStyle />
                <AlternatingRowStyle />
                <Columns>
                    <asp:HyperLinkField SortExpression="Name" HeaderText="事件" DataTextField="Name" DataNavigateUrlFields="ChannelID,ID" DataNavigateUrlFormatString="../forms/EditEvent.aspx?channelID={0}&id={1}" />
                    <asp:TemplateField HeaderText="开始时间" SortExpression="Name">
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
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <div class="visible-md visible-lg hidden-sm hidden-xs btn-group">
                                <a class="btn btn-xs btn-success" href='../forms/EventPlayer.aspx?channelID=<%#Eval("ChannelID") %>&id=<%#Eval("ID") %>'>
                                    <i class="icon-play bigger-120"></i>
                                </a>

                                <a class="btn btn-xs btn-info" href="../forms/EditEvent.aspx?channelID=<%#Eval("ChannelID") %>&id=<%#Eval("ID") %>">
                                    <i class="icon-edit bigger-120"></i>
                                </a>

                                <%-- <asp:LinkButton runat="server" CssClass="btn btn-xs btn-danger" CommandName="DeleteEvent" CommandArgument='<%#Eval("ID") %>'>
                                    <i class="icon-trash bigger-120"></i>
                                </asp:LinkButton>--%>

                                <%--<button class="btn btn-xs btn-warning">
                                    <i class="icon-flag bigger-120"></i>
                                </button>--%>
                            </div>
                            <div class="visible-xs visible-sm hidden-md hidden-lg">
                                <div class="dropdown">
                                    <button class="btn btn-minier btn-primary dropdown-toggle" data-toggle="dropdown">
                                        <i class="icon-cog icon-only bigger-110"></i>
                                    </button>
                                    <ul class="dropdown-menu dropdown-only-icon dropdown-yellow pull-right dropdown-caret dropdown-close">
                                        <li>
                                            <a href='../forms/EventPlayer.aspx?channelID=<%#Eval("ChannelID") %>&id=<%#Eval("ID") %>' class="tooltip-info" data-rel="tooltip" title="" data-original-title="View">
                                                <span class="blue">
                                                    <i class="icon-play bigger-120"></i>
                                                </span>
                                            </a>
                                        </li>
                                        <li>
                                            <a href="../forms/EditEvent.aspx?channelID=<%#Eval("ChannelID") %>&id=<%#Eval("ID") %>" class="tooltip-success" data-rel="tooltip" title="" data-original-title="Edit">
                                                <span class="green">
                                                    <i class="icon-edit bigger-120"></i>
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
            </asp:GridView>
            <res:DeluxeObjectDataSource runat="server" ID="eventDataSource" EnablePaging="true" TypeName="MCS.Library.Cloud.AMS.Data.DataSources.AMSEventDataSource" OnSelecting="eventDataSource_Selecting">
            </res:DeluxeObjectDataSource>
            <div>
                <asp:LinkButton ID="refreshBtn" runat="server"></asp:LinkButton>
            </div>
        </div>
    </form>
</body>
</html>
