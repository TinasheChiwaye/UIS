<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewRegistration.aspx.cs" Inherits="Funeral.Web.Admin.NewRegistration" %>
<html>
    <head runat="server">
    <title>UIS Dashboard</title>
        <%--<title>New Registration</title>--%>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <asp:PlaceHolder runat="server">
        <link href='<%=ResolveUrl("~/Content/bootstrap.min.css") %>' rel="stylesheet" />
        <link href='<%=ResolveUrl("~/font-awesome/css") %>' rel="stylesheet" />
        <link href='<%=ResolveUrl("~/Content/animate.css") %>' rel="stylesheet" />
        <link href='<%=ResolveUrl("~/Content/style.css") %>' rel="stylesheet" />
    </asp:PlaceHolder>
    <style type="text/css">
        .auto-style1 {
            width: 441px;
            height: 363px;
        }
        em {
            color: red;
        }
        .loginscreen.middle-box {
         width: 700px;
        }

        .middle-box {
        max-width: 800px;
         z-index: 100;
         margin: 0 auto;
         padding-top: 0px;
        }
        .ibox {
    
            
    margin-bottom: 0px !important;
    
}
        #lnkLogin{
              float:right;
              color:#337ab7;
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
    <body class="gray-bg">
    <form id="form2" runat="server" class="m-t" role="form">
        <div class="middle-box loginscreen animated fadeInDown">
            <div class="ibox">
                <div class="ibox-title text-center" >
                  <img alt="" class="auto-style1" src='<%=ResolveUrl("~/Admin/Images/unpluggit.png") %>' />
                    <h2>UIS Policy Administration Registerd</h2>
                    <h2>&nbsp; </h2>
                </div>
                <div class="ibox-title text-center">
                    <span class="clear"><span class="block m-t-xs">
                        <strong>Support Contacts</strong><br />
                        <asp:Label runat="server" ID="Label2" Text="">Tel:&nbsp 011 321 0194</asp:Label><br />
                        <asp:Label runat="server" ID="Label3" Text="">Email:&nbsp support@unpluggit.co.za</asp:Label><br />
                        <asp:Label runat="server" ID="Label4" Text="">&nbsp &nbsp  log a Call For Help Desk</asp:Label><br />
                    </span></span>
                </div>
                <div class="ibox-content">
                      <div class="row">

        <div class="col-lg-12">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
            <h2><a runat="server" ID="lnkLogin" visible="false" href="Login.aspx"><small>Back To Login</small></a></h2>
            
        </div>

        <div class="col-lg-12">
            <div class="form-group">
                <div class="input-group">
                    <asp:ValidationSummary runat="server" ID="vSummaryRegistration" ValidationGroup="Registration" ForeColor="Red" />
                </div>
            </div>
        </div>

        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Company Details</h5>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Company Name <em>*</em> </label>
                                <asp:TextBox MaxLength="100" runat="server" ID="txtCompanyName" name="CompanyName" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="Registration" ControlToValidate="txtCompanyName" ID="RequiredFieldValidator1" ForeColor="red" runat="server" ErrorMessage="Please enter Company Name"></asp:RequiredFieldValidator>
                                <%--<asp:RegularExpressionValidator Display="None" ID="RegExp1" ValidationGroup="Registration" runat="server" ErrorMessage="Company Name Enter Only characters" ControlToValidate="txtCompanyName" ValidationExpression="[a-zA-Z ]*$" />--%>
                            </div>
                            <div class="form-group">
                                <label>Registration Number <em>*</em>  </label>
                                <asp:TextBox MaxLength="25" runat="server" ID="txtRegistrationNumber" name="RegistrationNumber" type="text" class="form-control"></asp:TextBox>
                               <asp:RequiredFieldValidator Display="None" ValidationGroup="Registration" ControlToValidate="txtRegistrationNumber" ID="RequiredFieldValidator2" ForeColor="red" runat="server" ErrorMessage="Please enter Registration Number"></asp:RequiredFieldValidator>
                                <%--<asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator1" ValidationGroup="Registration" runat="server" ControlToValidate="txtRegistrationNumber" ErrorMessage="Registration Number Enter Only characters" ValidationExpression="[a-zA-Z ]*$" />--%>
                            </div>
                              <div class="form-group">
                                <label>FSB Number <em>*</em> </label>
                                <asp:TextBox MaxLength="50" runat="server" ID="txtFsbNumber" name="FsbNumber" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="Registration" ControlToValidate="txtFsbNumber" ID="RequiredFieldValidator4" ForeColor="red" runat="server" ErrorMessage="Please enter Fsb Number"></asp:RequiredFieldValidator>
                            </div>
                          
                            </div>
                        <div class="col-lg-6">
                              <div class="form-group">
                                <label>full name<em>*</em> </label>
                                <asp:TextBox MaxLength="25" runat="server" ID="txtFirstName" name="FirstName" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="Registration" ControlToValidate="txtFirstName" ID="RequiredFieldValidator3" ForeColor="red" runat="server" ErrorMessage="Please enter  full name"></asp:RequiredFieldValidator>
                                <%--<asp:RegularExpressionValidator Display="None" ID="RegExp1" ValidationGroup="Registration" runat="server" ErrorMessage="Company Name Enter Only characters" ControlToValidate="txtCompanyName" ValidationExpression="[a-zA-Z ]*$" />--%>
                            </div>
                              <div class="form-group">
                                <label>Surname <em>*</em> </label>
                                <asp:TextBox MaxLength="25" runat="server" ID="txtLastName" name="LastName" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="Registration" ControlToValidate="txtLastName" ID="RequiredFieldValidator7" ForeColor="red" runat="server" ErrorMessage="Please enter Surname"></asp:RequiredFieldValidator>
                                <%--<asp:RegularExpressionValidator Display="None" ID="RegExp1" ValidationGroup="Registration" runat="server" ErrorMessage="Company Name Enter Only characters" ControlToValidate="txtCompanyName" ValidationExpression="[a-zA-Z ]*$" />--%>
                            </div>
                              <div class="form-group">
                                <label>Cellphone Number <em>*</em>  </label>
                                <asp:TextBox MaxLength="15" runat="server" ID="txtCellphone" name="Cellphone" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="Registration" ControlToValidate="txtCellphone" ID="RequiredFieldValidator5" ForeColor="red" runat="server" ErrorMessage="Please enter  cell phone number"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator6" ValidationGroup="Registration" runat="server" ControlToValidate="txtCellphone" ErrorMessage="Cellphone Number Enter Only Number" ValidationExpression="^([0-9\(\)\/\+ \-]*)$" />
                            </div>
                          
                         </div>
                      </div>
                    </div>
                </div>
            </div>
        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Address</h5>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <%--<div class="col-lg-12 form-horizontal">--%>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>line 1 <em>*</em> </label>
                                <div>
                                    <asp:TextBox MaxLength="100" runat="server" ID="txtline1" name="line1" type="text" class="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="None" ValidationGroup="Registration" ControlToValidate="txtline1" ID="RequiredFieldValidator8" ForeColor="red" runat="server" ErrorMessage="Please enter line 1"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label >line 3</label>
                                <div>
                                    <asp:TextBox MaxLength="100" runat="server" ID="txtline3" name="line3" type="text" class="form-control"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator Display="None" ValidationGroup="Registration" ControlToValidate="txtline3" ID="RequiredFieldValidator14" ForeColor="red" runat="server" ErrorMessage="Please enter line 3"></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                            
                            </div>
                        <div class="col-lg-6">
                               

                            <div class="form-group">
                                <label>line 2 </label>
                                <div>
                                    <asp:TextBox MaxLength="100" runat="server" ID="txtline2" name="line2" type="text" class="form-control"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator Display="None" ValidationGroup="Registration" ControlToValidate="txtline2" ID="RequiredFieldValidator13" ForeColor="red" runat="server" ErrorMessage="Please enter line 2"></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>

                            <div class="form-group">
                                <label>line 4 </label>
                                <div>
                                    <asp:TextBox MaxLength="100" runat="server" ID="txtline4" name="line4" type="text" class="form-control"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator Display="None" ValidationGroup="Registration" ControlToValidate="txtline4" ID="RequiredFieldValidator15" ForeColor="red" runat="server" ErrorMessage="Please enter line 4"></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                            </div>
                        <div class="col-lg-6">    
                        <div class="form-group">
                                <%--<label class="col-lg-2">postal code <em>*</em> </label>
                                <div class="col-lg-10">--%>
                                    <label>postal code <em>*</em> </label>
                                <div>
                                    <asp:TextBox MaxLength="30" runat="server" ID="txtpostalcode" name="txtPassport" type="text" class="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="None" ValidationGroup="Registration" ControlToValidate="txtpostalcode" ID="RequiredFieldValidator9" ForeColor="red" runat="server" ErrorMessage="Please Enter Postal Code"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator10" ValidationGroup="Registration" runat="server" ControlToValidate="txtpostalcode" ErrorMessage="Postal Code Enter Only Number" ValidationExpression="^[0-9]*$" />
                                </div>
                            </div>
                        </div>
                    </div>
                    </div>
                </div>
            </div>
      
                           
            <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Login Details</h5>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        
                             <div class="col-lg-4">    
                             <div class="form-group">
                                <label>Email ID  <em>*</em></label>
                                <asp:TextBox MaxLength="30" runat="server" ID="txtEmail" name="Email" type="text" class="form-control"></asp:TextBox>
                                  <asp:RequiredFieldValidator Display="None" ValidationGroup="Registration" ControlToValidate="txtEmail" ID="RequiredFieldValidator11" ForeColor="red" runat="server" ErrorMessage="Please enter Email"></asp:RequiredFieldValidator>
                                <%-- <asp:RegularExpressionValidator Display="None" runat="server" ID="revEmailValidation" ValidationGroup="Registration" ControlToValidate="txtEmail" ErrorMessage="Please enter valid email address" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>--%>
                                <%--<asp:RequiredFieldValidator Display="None" ValidationGroup="Registration" ControlToValidate="txtEmail" ID="RequiredFieldValidator5" ForeColor="red" runat="server" ErrorMessage="Please enter Email Address"></asp:RequiredFieldValidator>--%>
                            </div>
                           </div>
                             <div class="col-lg-4">    
                       
                             <div class="form-group">
                                <label>Password <em>*</em> </label>
                                 <asp:TextBox runat="server" TextMode="Password" ID="password" PlaceHolder="Enter password" CssClass="form-control" required="" />
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="Registration" ControlToValidate="password" ID="RequiredFieldValidator12" ForeColor="red" runat="server" ErrorMessage="Please enter password"></asp:RequiredFieldValidator>
                                <%--<asp:RegularExpressionValidator Display="None" ID="RegExp1" ValidationGroup="Registration" runat="server" ErrorMessage="Company Name Enter Only characters" ControlToValidate="txtCompanyName" ValidationExpression="[a-zA-Z ]*$" />--%>
                            </div>
                                 </div>
                             <div class="col-lg-4">    
                          <div class="form-group">
                                <label>Confirm Password <em>*</em> </label>
                               <asp:TextBox runat="server" TextMode="Password" ID="ConfirmPassword" PlaceHolder="Enter Confirm password" CssClass="form-control" required="" />
                              <asp:RequiredFieldValidator Display="None" ValidationGroup="Registration" ControlToValidate="ConfirmPassword" ID="RequiredFieldValidator13" ForeColor="red" runat="server" ErrorMessage="Please enter ConfirmPassword"></asp:RequiredFieldValidator>  
                               
                             <asp:CompareValidator id="comparePasswords" runat="server" ValidationGroup="Registration" ControlToCompare="password" ControlToValidate="ConfirmPassword" ErrorMessage="Your passwords do not match up!" Display="None" />
        
                               <%-- <asp:RegularExpressionValidator Display="None" runat="server" ID="revEmailValidation" ValidationGroup="Registration" ControlToValidate="txtEmail" ErrorMessage="Please enter valid email address" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>--%>
                                <%--<asp:RequiredFieldValidator Display="None" ValidationGroup="Registration" ControlToValidate="txtEmail" ID="RequiredFieldValidator5" ForeColor="red" runat="server" ErrorMessage="Please enter Email Address"></asp:RequiredFieldValidator>--%>
                            </div>
                                 </div>
                       
                        </div>
                    </div>
                </div>
                </div>


        <div class="col-lg-12">
            <div class="form-group text-center">
                <div class="btn-group">
                    <asp:Button class="btn btn-sm btn-primary" Visible="true" ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                </div>
               <%-- "--%>
                <div class="btn-group">
                    <asp:Button runat="server" ID="btnSubmite" ValidationGroup="Registration" CssClass="btn btn-sm btn-primary" Text="Save" OnClick="btnSubmite_Click" />
               
                </div>
            </div>
        </div>

   </div>
                    </div>
                </div>
            </div>
        <script src='<%=ResolveUrl("~/Scripts/jquery-2.1.1.js") %>' type="text/javascript"></script>
        <script src='<%=ResolveUrl("~/Scripts/bootstrap.min.js") %>' type="text/javascript"></script>
   <script type="text/javascript">
       $(document).ready(function () {
           $("#btnSubmite").click(function () {
            
               if (($('#<%=ConfirmPassword.ClientID%>').val()) != ($('#<%=password.ClientID%>').val())) {
                   $('#<%=lblMessage.ClientID%>').val(ShowMessage(ref lblMessage, MessageType.Danger, "User Already Exists."));
               }
           });
           


       });

   </script>
             </form>
</body>
</html>