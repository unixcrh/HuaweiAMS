<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditEvent.aspx.cs" Inherits="ChannelManagement.forms.EditEvent" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>编辑事件</title>
</head>
<body>
    <div class="container">
        <form id="serverForm" runat="server" class="form-horizontal">
            <res:DataBindingControl runat="server" ID="bindingControl" IsValidateOnSubmit="true" ValidateUnbindProperties="false">
                <ItemBindings>
                    <res:DataBindingItem DataPropertyName="Name" ControlID="Name" />
                    <res:DataBindingItem DataPropertyName="Description" ControlID="Description" />
                    <res:DataBindingItem DataPropertyName="StartTime" ControlID="StartTime" />
                    <res:DataBindingItem DataPropertyName="EndTime" ControlID="EndTime" />
                </ItemBindings>
            </res:DataBindingControl>
            <div class="form-group form-group-sm">
                <label class="col-sm-2 control-label" for="Name">事件名称</label>
                <div class="col-sm-10">
                    <asp:TextBox runat="server" CssClass="form-control" type="text" ID="Name" placeholder="事件名称" />
                </div>
            </div>
            <div class="form-group form-group-sm">
                <label class="col-sm-2 control-label" for="Name">事件描述</label>
                <div class="col-sm-10">

                    <asp:TextBox runat="server" CssClass="form-control" type="text" ID="Description" TextMode="MultiLine" placeholder="事件描述" />
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
                <div class="col-sm-8"></div>
                <div class="col-sm-4" style="">
                    <asp:Button runat="server" AccessKey="S" CssClass="btn btn-primary" ID="save" Text="保存(S)" OnClick="save_Click" />
                    <a runat="server" id="backUrl" class="btn btn-default" href="#">返回</a>
                </div>
            </div>
        </form>
    </div>
</body>
</html>
