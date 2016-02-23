<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RequestTest.aspx.cs" Inherits="CutomerSite.W3.RequestTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="loginForm" name="loginForm" method="post"
        action="https://uniportal-beta.huawei.com/saaslogin/sp">
        <div>
            <asp:Literal runat="server" ID="identity" Mode="Encode"></asp:Literal>
        </div>
        <textarea id="SAMLRequest" runat="server" cols="80" rows="30"></textarea>
        <input id="RelayState" name="RelayState" type="hidden" value="" />
        <p>
            <input type="submit" id="singin" value="Sign In" />
        </p>
    </form>
</body>
</html>
