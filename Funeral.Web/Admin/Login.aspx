<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Funeral.Web.Admin.Login" %>

<html>
<head runat="server">
    <title>UIS Dashboard</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <asp:PlaceHolder runat="server">
        <link href='<%=ResolveUrl("~/Content/bootstrap.min.css") %>' rel="stylesheet" />
        <link href='<%=ResolveUrl("~/font-awesome/css") %>' rel="stylesheet" />
        <link href='<%=ResolveUrl("~/Content/animate.css") %>' rel="stylesheet" />
        <link href='<%=ResolveUrl("~/Content/style.css") %>' rel="stylesheet" />
    </asp:PlaceHolder>
    <script src='<%=ResolveUrl("~/Scripts/jquery-2.1.1.js") %>' type="text/javascript"></script>
    <script src='<%=ResolveUrl("~/Scripts/bootstrap.min.js") %>' type="text/javascript"></script>
    <style type="text/css">
        .auto-style1 {
            width: 400px;
            height: auto;
        }

        .responsive {
            width: 100%;
            height: auto;
        }
    </style>
    <script>
        (function (i, s, o, g, r, a, m) {
            i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                (i[r].q = i[r].q || []).push(arguments)
            }, i[r].l = 1 * new Date(); a = s.createElement(o),
                m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
        })(window, document, 'script', 'https://www.google-analytics.com/analytics.js', 'ga');

        ga('create', 'UA-90748185-1', 'auto');
        ga('send', 'pageview');

    </script>
</head>
<body style="background-color: #fafafa">
    <div class="col-sm-4">
        <img alt="" class="responsive" src='<%=ResolveUrl("~/Content/images/LoginPageImage/featureLeft1.png") %>' />
        <img alt="" class="responsive" src='<%=ResolveUrl("~/Content/images/LoginPageImage/SalesLeft1.png") %>' />
    </div>
    <form id="form1" runat="server" class="m-t" role="form">

        <div class="col-sm-4 text-center animated fadeInDown">
            <div class="ibox">
                <div class="ibox-title" style="background-color: red;">
                    <h2>
                        <img alt="" class="auto-style1" src='<%=ResolveUrl("~/Admin/Images/unpluggit.png") %>' /></h2>
                    <h2 style="color: white; font-weight: bold">UIS Policy Administration</h2>
                </div>
                <div class="ibox-content" style="border: 2px solid red;">
                    <span class="clear">
                        <span class="block m-t-xs">
                            <strong>Support Contacts</strong><br />
                            <asp:Label runat="server" ID="Label2" Text="">Tel:&nbsp 011 321 0194</asp:Label><br />
                            <asp:Label runat="server" ID="Label3" Text="">Email:&nbsp support@unpluggit.co.za</asp:Label><br />
                            <asp:Label runat="server" ID="Label4" Text="">&nbsp &nbsp  log a Call For Help Desk</asp:Label><br />
                        </span>
                    </span>
                    <hr />
                    <div>
                        <div class="form-group">
                            <asp:TextBox runat="server" ID="username" PlaceHolder="Enter username" CssClass="form-control" required="" />
                        </div>
                        <div class="form-group">
                            <asp:TextBox runat="server" TextMode="Password" ID="password" PlaceHolder="Enter password" CssClass="form-control" required="" />
                        </div>
                        <asp:Button CssClass="btn btn-primary block full-width m-b" Style="background-color: red; font-weight: bold" runat="server" ID="Button1" Text="Login" OnClick="Login_click" />
                        <p class="m-t"><b><small>Unplugg IT Solutions &copy; <%=DateTime.Now.Year %></small></b> </p>
                        <p>
                            <asp:Label runat="server" ID="lblMessage"></asp:Label>
                        </p>
                    </div>
                </div>
            </div>
        </div>

    </form>
    <div class="col-sm-4">
        <img alt="" class="responsive" src='<%=ResolveUrl("~/Content/images/LoginPageImage/PlatformRight1.png") %>' />
        <img alt="" class="responsive" src='<%=ResolveUrl("~/Content/images/LoginPageImage/screensRight1.png") %>' />
    </div>

</body>
</html>
