<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="smsSendingSetup.aspx.cs" Inherits="Funeral.Web.Tools.smsSendingSetup" %>

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
                    <asp:ValidationSummary runat="server" ID="vSummarysmsSetup" ValidationGroup="smsSetup" ForeColor="Red" />
                </div>
            </div>
        </div>

        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>SMS Sending setup</h5>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-lg-12 form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-2">TO Cellphone Number <em>*</em> </label>
                                <div class="col-lg-6">
                                    <asp:TextBox MaxLength="15" runat="server" ID="txtCellphoneNumber" name="Cellphone" type="text" class="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="None" ValidationGroup="smsSetup" ControlToValidate="txtCellphoneNumber" ID="RequiredFieldValidator10" ForeColor="red" runat="server" ErrorMessage="Please enter Cellphone number"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator4" ValidationGroup="smsSetup" runat="server" ControlToValidate="txtCellphoneNumber" ErrorMessage="Cellphone Number Enter Only Number" ValidationExpression="^[0-9]*$" />
                                </div>
                                <div class="col-lg-4">
                                    <asp:CheckBox ID="chkAllMember" runat="server" Text="All Members" OnClick="GetAllMember();" CssClass="checkbox" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-lg-2">Message Text <em>*</em> </label>
                                <div class="col-lg-9">
                                    <asp:TextBox MaxLength="100" runat="server" ID="txtMessage" name="line1" type="Message" class="form-control" TextMode="MultiLine" Rows="6"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="None" ValidationGroup="smsSetup" ControlToValidate="txtMessage" ID="RequiredFieldValidator12" ForeColor="red" runat="server" ErrorMessage="Please enter Message Text"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-8"></div>
                                <div class="col-lg-2">
                                    <asp:Button runat="server" ID="btnSubmite" ValidationGroup="smsSetup" CssClass="btn btn-md btn-primary pull-right" Text="Send SMS" OnClick="btnSubmite_Click" />
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
            GetAllMember();
        });
        function GetAllMember() {
            var RFvalName = document.getElementById("<%=RequiredFieldValidator10.ClientID%>");
            var REvalName = document.getElementById("<%=RegularExpressionValidator4.ClientID%>");
            var txtToSms = document.getElementById("<%=txtCellphoneNumber.ClientID%>");
            if (document.getElementById('<%=chkAllMember.ClientID%>').checked) {
                //alert(chk.checked);
                //document.getElementById('<%=txtCellphoneNumber.ClientID%>').value = 10;
                txtToSms.disabled = true;
                ValidatorEnable(RFvalName, false);
                ValidatorEnable(REvalName, false);
            }
            else {
                txtToSms.disabled = false;
                ValidatorEnable(RFvalName, true);
                ValidatorEnable(REvalName, true);
            }
        }
    </script>
</asp:Content>
