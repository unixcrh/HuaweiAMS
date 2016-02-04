<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignInBridge.aspx.cs" Inherits="CutomerSite.W3.SignInBridge" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function onDocumentLoad() {
            document.getElementById("singin").click();
        }
    </script>
</head>
<body>
    <form id="loginForm" name="loginForm" method="post" runat="server"
        action="https://uniportal.huawei.com/saaslogin/sp">
        <textarea id="SAMLRequest" runat="server" cols="80" rows="30"></textarea>
        <input id="RelayState" name="RelayState" type="hidden" value="" />
        <p>
            <input type="submit" id="singin" value="Sign In" />
        </p>
        <div runat="server" id="privateCAInfo"></div>
    </form>
</body>
</html>
