<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="smsTempletSetup.aspx.cs" Inherits="Funeral.Web.Tools.smsTempletSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <style>
        .checkbox {
            padding: 4px;
        }

        em {
            color: red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">

        <div class="col-lg-12">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>

        <div class="col-lg-12">
            <div class="form-group">
                <div class="input-group">
                    <asp:ValidationSummary runat="server" ID="vSummarysmsTempletSetup" ValidationGroup="smsTempletSetup" ForeColor="Red" />
                </div>
            </div>
        </div>

        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>SMS Sending Tempplet setup</h5>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-lg-12 form-horizontal">
                            <%--<div class="form-group">
                                <label class="col-lg-2">TO Cellphone Number <em>*</em> </label>
                                <div class="col-lg-6">
                                    <asp:TextBox MaxLength="15" runat="server" ID="txtCellphoneNumber" name="Cellphone" type="text" class="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="None" ValidationGroup="smsTempletSetup" ControlToValidate="txtCellphoneNumber" ID="RequiredFieldValidator10" ForeColor="red" runat="server" ErrorMessage="Please enter Cellphone number"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator4" ValidationGroup="smsTempletSetup" runat="server" ControlToValidate="txtCellphoneNumber" ErrorMessage="Cellphone Number Enter Only Number" ValidationExpression="^[0-9]*$" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-lg-2">Message Text <em>*</em> </label>
                                <div class="col-lg-9">
                                    <asp:TextBox MaxLength="100" runat="server" ID="txtMessage" name="line1" type="Message" class="form-control" TextMode="MultiLine" Rows="6"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="None" ValidationGroup="smsTempletSetup" ControlToValidate="txtMessage" ID="RequiredFieldValidator12" ForeColor="red" runat="server" ErrorMessage="Please enter Message Text"></asp:RequiredFieldValidator>
                                </div>
                            </div>--%>

                            <div class="form-group">
                                <label for="inputEmail" class="col-lg-2">Template Name <em>*</em> </label>
                                <div class="col-lg-6">
                                    <asp:DropDownList ID="ddlTemplate" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlTemplate_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-4">
                                    <asp:Label ID="Label1" runat="server" Text="Characters left: 135"></asp:Label>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-lg-2">Message Text <em>*</em> </label>
                                <div class="col-lg-9">
                                    <asp:TextBox MaxLength="200" runat="server" ID="txtMessage" name="line1" type="Message" class="form-control" TextMode="MultiLine" Rows="6"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="None" ValidationGroup="smsTempletSetup" ControlToValidate="txtMessage" ID="RequiredFieldValidator12" ForeColor="red" runat="server" ErrorMessage="Please enter Message Text"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-8"></div>
                                <div class="col-lg-2">
                                    <asp:Button runat="server" ID="btnSubmite" ValidationGroup="smsTempletSetup" CssClass="btn btn-md btn-primary pull-right" Text="Save" OnClick="btnSubmite_Click" />
                                </div>
                                <div class="col-lg-2">
                                    <asp:Button class="btn btn-md btn-warning pull-left" Visible="true" ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptControl" runat="server">
    <script>
        $(document).ready(function () {
            var input = document.getElementById('<%=txtMessage.ClientID%>');
            var TextLength = input.value.length;
            $(document.getElementById('<%=Label1.ClientID%>')).text("Characters left: " + (135 - TextLength));
        });
        $(document.getElementById('<%=txtMessage.ClientID%>')).keyup(function () {
            $(document.getElementById('<%=Label1.ClientID%>')).text("Characters left: " + (135 - $(this).val().length));
            
        });

        var input = document.getElementById('<%=txtMessage.ClientID%>');
        input.onkeydown = function () {
            
            var TextLength = input.value.length;
            //var strremine = 10 - TextLength;
            

            var key = event.keyCode || event.charCode;

            
            if ((135 - TextLength) <= 0) {
                if (key >= 37 && key <= 40) {
                    return true; // arrow keys
                }
                if (key == 8 || key == 46 || key == 36 || key == 35) {
                    return true; // backspace (8) / delete (46)
                }
                else {
                    return false;
                }
            }
        };

    </script>
</asp:Content>
