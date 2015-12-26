<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="ChannelManagement.Authenticate.SignIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户认证</title>
    <script src="../scripts/jstz.main.js"></script>
    <script src="../scripts/jstz.rules.js"></script>
    <script type="text/javascript">
        function onDocumentLoad()
        {
            var offset = jstz.get_date_offset(new Date());

            $("#timeOffset").val(offset.toString());
        }
    </script>
    <style type="text/css">
        body {
            padding-top: 40px;
            padding-bottom: 40px;
            background-color: #f5f5f5;
        }

        .form-signin {
            max-width: 450px;
            padding: 19px 29px 29px;
            margin: 0 auto 20px;
            background-color: #fff;
            border: 1px solid #e5e5e5;
            -webkit-border-radius: 5px;
            -moz-border-radius: 5px;
            border-radius: 5px;
            -webkit-box-shadow: 0 1px 2px rgba(0,0,0,.05);
            -moz-box-shadow: 0 1px 2px rgba(0,0,0,.05);
            box-shadow: 0 1px 2px rgba(0,0,0,.05);
        }

            .form-signin .form-signin-heading, .form-signin .checkbox {
                margin-bottom: 10px;
            }
    </style>
</head>
<body onload="onDocumentLoad();">
    <form id="serverForm" runat="server">
        <div class="container">
            <div class="form-signin">
                <div class="row">
                    <h2>用户认证</h2>
                    <div class="form-horizontal" role="form">
                        <div class="form-group">
                            <label for="txtUserName" class="col-lg-2 col-md-2 col-sm-3 control-label">
                                登录名</label>
                            <div class="col-lg-10 col-md-10 col-sm-9 col-xs-12">
                                <asp:TextBox runat="server" CssClass="form-control" ID="signInName" placeholder="登录名" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="txtPassword" class="col-lg-2 col-md-2 col-sm-3 control-label">
                                密码</label>
                            <div class="col-lg-10 col-md-10 col-sm-9 col-xs-12">
                                <input type="password" runat="server" class="form-control" id="password" placeholder="密码" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group">
                            <div class="col-lg-2 col-md-2 col-sm-3 control-label">
                            </div>
                        <div class="col-lg-10 col-md-10 col-sm-9 col-xs-12">
                            <asp:Label CssClass="text-danger" ID="errorMessage" runat="server" Style="line-height: 100%; word-break: break-all"></asp:Label>
                        </div>
                        </div>
                        
                    </div>
                    <div class="row">
                        <div class="col-md-2 col-sm-1 col-xs-1">
                            <input type="hidden" id="timeOffset" runat="server" />
                        </div>
                        <div class="col-md-8 col-sm-10 col-xs-10">
                            <asp:Button runat="server" ID="SignInButton" CssClass="form-control btn btn-large btn-primary btn-default"
                                Text="用户登录" OnClick="SignInButton_Click" />
                        </div>
                        <div class="col-md-2 col-sm-1 col-sm-1">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
