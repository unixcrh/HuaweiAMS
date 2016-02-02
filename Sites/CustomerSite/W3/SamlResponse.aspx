<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SamlResponse.aspx.cs" Inherits="CutomerSite.W3.SamlResponse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>W3 Response</title>
</head>
<body>
   <form id="serverForm" runat="server">
        <div>
            <textarea runat="server" id="SAMLResponse" cols="80" rows="30"></textarea>
        </div>
        <div>
            <asp:Literal runat="server" ID="ResponseUserID" Mode="Encode"></asp:Literal>
        </div>
    </form>
</body>
</html>
