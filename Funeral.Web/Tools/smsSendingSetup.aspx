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
                                <div class="col-lg-4">
                                    <asp:TextBox MaxLength="15" runat="server" ID="txtCellphoneNumber" name="Cellphone" type="text" class="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="None" ValidationGroup="smsSetup" ControlToValidate="txtCellphoneNumber" ID="RequiredFieldValidator10" ForeColor="red" runat="server" ErrorMessage="Please enter Cellphone number"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator4" ValidationGroup="smsSetup" runat="server" ControlToValidate="txtCellphoneNumber" ErrorMessage="Cellphone Number Enter Only Number" ValidationExpression="^[0-9]*$" />
                                </div>
                                <div class="col-lg-3">
                                    <asp:Label ID="Label1" runat="server" Text="Characters left: 160"></asp:Label>
                                </div>
                                <div class="col-lg-3">
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
                            <asp:GridView ID="gvTemplate" style="margin-top:20px;" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false">
                                  <PagerStyle CssClass="pagination-ys" />
                                    <Columns>
                                        <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True" />
                                        <asp:BoundField DataField="PlaceHolder" HeaderText="Placeholder" ReadOnly="True" />
                                    </Columns>
                           </asp:GridView>
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
            $(document.getElementById('<%=Label1.ClientID%>')).text("Characters left: " + (160 - TextLength));
        });
        $(document.getElementById('<%=txtMessage.ClientID%>')).keyup(function () {
            $(document.getElementById('<%=Label1.ClientID%>')).text("Characters left: " + (160 - $(this).val().length));
            
        });
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
                var input = document.getElementById('<%=txtMessage.ClientID%>');
        input.onkeydown = function () {
            
            var TextLength = input.value.length;
            //var strremine = 10 - TextLength;
            

            var key = event.keyCode || event.charCode;

            
            //if ((160 - TextLength) <= 0) {
            //    if (key >= 37 && key <= 40) {
            //        return true; // arrow keys
            //    }
            //    if (key == 8 || key == 46 || key == 36 || key == 35) {
            //        return true; // backspace (8) / delete (46)
            //    }
            //    else {
            //        return false;
            //    }
            //}
        };

    </script>
</asp:Content>
