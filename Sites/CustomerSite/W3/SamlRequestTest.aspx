<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SamlRequestTest.aspx.cs" Inherits="CutomerSite.W3.SamlRequestTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Saml Request Test</title>
</head>
<body>
    <form id="loginForm" name="loginForm" method="post"
        action="https://uniportal.huawei.com/saaslogin/sp">
        <textarea id="SAMLRequest" runat="server" cols="80" rows="30"></textarea>
        <input id="RelayState" name="RelayState" type="hidden" value="" />
        <p>
            <input type="submit" id="singin" value="Sign In" />
        </p>
        <div runat="server" id="privateCAInfo"></div>
    </form>
    <textarea id="SAMLRequestXml" runat="server" cols="80" rows="30"></textarea>
</body>
</html>
