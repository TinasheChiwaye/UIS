<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="TombStoneServiceSelect.aspx.cs" Inherits="Funeral.Web.Admin.TombStoneServiceSelect" %>
<%@ Register Src="~/UserControl/ctrlTombstonePaymentHistory.ascx" TagName="ucTombstonePaymentHistory" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="scriptManager1" runat="server"></asp:ScriptManager>

    <div class="row">
        <div class="col-lg-12">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
            <div class="ibox ">

                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <div class="row">
                            <div class="col-sm-6">
                                <h5>Invoice</h5>
                            </div>
                            <div class="col-sm-6">
                                <div class="pull-right">
                                    <%--  <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                             <asp:Button ID="Accept" class="pull-right" CssClass="btn btn-sm btn-primary" runat="server" Text="Accept" OnClick="btnAcceptClick"/>
                                
                                    <asp:Button ID="txtRejectbtn" class="pull-right" CssClass="btn btn-sm btn-primary" runat="server" Text="Reject" data-target="#GSCCModal" data-toggle="modal" />
                         
                                     </ContentTemplate>
                                    </asp:UpdatePanel>--%>
                                </div>
                            </div>

                        </div>

                    </div>
                    <div class="ibox-content">
                        <div class="row">
                            <div class="col-lg-6">
                                <div class="form-group">
                                    <div class="form-group">
                                        <label>Bill From</label>
                                        <asp:TextBox runat="server" ID="txtcompanyRules" name="CompanyRules" type="text" class="form-control" TextMode="MultiLine" Rows="6" ReadOnly="true"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator Display="None" ValidationGroup="CompanySetup" ControlToValidate="txtcompanyRules" ID="RequiredFieldValidator20" ForeColor="red" runat="server" ErrorMessage="Please enter Company Rules"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group">
                                        <label>Bill to </label>
                                        <asp:TextBox runat="server" ID="txtTo" name="companyrules" type="text" class="form-control" TextMode="multiline" Rows="6" ReadOnly="true"></asp:TextBox>
                                        <%-- <asp:requiredfieldvalidator display="none" validationgroup="companysetup" controltovalidate="txtcompanyrules" id="requiredfieldvalidator20" forecolor="red" runat="server" errormessage="please enter company rules"></asp:requiredfieldvalidator>--%>
                                    </div>

                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="form-group">
                                    <div class="col-lg-12">
                                        <div class="col-lg-6">
                                            <div class="form-group">
                                            </div>

                                        </div>
                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <div class="form-group">
                                                    <label>Invoice Number</label>
                                                    <asp:TextBox MaxLength="10" runat="server" ID="txtInvoiceNumber" name="txtqtynum" type="text" class="form-control" ReadOnly="true"></asp:TextBox>
                                                    <%--<asp:DropDownList ID="ddlCemetery" runat="server" DataTextField="Name" DataValueField="CountryCode" CssClass="form-control m-b"></asp:DropDownList>--%>
                                                </div>
                                                <div class="form-group">
                                                    <label>Date</label>
                                                    <asp:TextBox MaxLength="30" runat="server" ID="txtDate" name="txtDriver" type="text" class="form-control" ReadOnly="true"></asp:TextBox>

                                                    <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtCitizenship" ID="RequiredFieldValidator9" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>                                                    <%--<asp:RequiredFieldValidator ValidationGroup="tab1" ControlToValidate="txtPassport" ID="RequiredFieldValidator10" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group">
                                                    <label>Due Date</label>
                                                    <asp:TextBox MaxLength="30" runat="server" ID="txtDueDate" type="text" class="form-control"></asp:TextBox>
                                                    <%--<asp:DropDownList ID="ddlCemetery" runat="server" DataTextField="Name" DataValueField="CountryCode" CssClass="form-control m-b"></asp:DropDownList>--%>
                                                    <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtCitizenship" ID="RequiredFieldValidator9" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <%--<div class="form-group">
                                                    <label>Balance Due</label>
                                                    <asp:TextBox MaxLength="30" runat="server" ID="txtBalance" name="txtGraveNo" type="text" Enabled="false" class="form-control"></asp:TextBox>--%>
                                                <%--<asp:RequiredFieldValidator ValidationGroup="tab1" ControlToValidate="txtPassport" ID="RequiredFieldValidator10" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>Services</h5>
                    </div>
                    <div class="ibox-content">
                        <asp:UpdatePanel ID="UpddlBranch" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <label>Package</label>
                                        <asp:DropDownList runat="server" ID="ddlPackage" class="form-control m-b" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>Select Services</label>
                                        <asp:DropDownList runat="server" ID="ddlServices" class="form-control m-b" AutoPostBack="true" OnSelectedIndexChanged="ChangeData">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlServices" InitialValue="0" ErrorMessage="Please select something" ValidationGroup="vadd" />
                                    </div>
                                    <div class="form-group">
                                        <label>Qty</label>
                                        <asp:TextBox MaxLength="10" runat="server" ID="txtNumber" name="txtNumber" type="number" class="form-control" OnTextChanged="calculation" AutoPostBack="true"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="This Field Required" ControlToValidate="txtNumber" ValidationGroup="vadd"></asp:RequiredFieldValidator>
                                    </div>

                                </div>
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <label>Desctiption</label>
                                        <asp:TextBox MaxLength="10" runat="server" ID="txtSerDes" name="" type="text" class="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>Rate</label>
                                        <asp:TextBox MaxLength="10" runat="server" ID="txtRate" name="" type="text" class="form-control" AutoPostBack="true" OnTextChanged="calculation" onkeypress="return isDecimalNumber1(event,this);">0</asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="This Field Required" ControlToValidate="txtRate" ValidationGroup="vadd"></asp:RequiredFieldValidator>

                                    </div>
                                    <div class="form-group">
                                        <label>Total</label>
                                        <asp:TextBox MaxLength="10" runat="server" ID="txtTotal" name="" type="text" class="form-control" ReadOnly="true"></asp:TextBox>

                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="form-group">
                            <asp:Button ID="btnPackageService" CssClass="btn btn-sm btn-primary" runat="server" Text="Add Package" OnClick="btnPackageService_Click" />
                            <asp:Button ID="AddServ" CssClass="btn btn-sm btn-primary" runat="server" Text="Add Service" ValidationGroup="vadd" OnClick="btncrtService_Click" />
                        </div>

                        <%--start --%>
                        <asp:GridView ID="gvServiceList" OnRowDataBound="gvServiceList_RowDataBound" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="There are no Service added." OnRowCommand="gvService_RowCommand">
                            <PagerStyle CssClass="pagination-ys" />
                            <Columns>
                                <asp:BoundField DataField="ServiceName" HeaderText="Service Name" ReadOnly="True" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />

                                <asp:BoundField DataField="ServiceDesc" HeaderText="Desctiption" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />

                                <asp:BoundField DataField="ServiceRate" HeaderText="Rate" ReadOnly="True" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="Quantity" HeaderText="QTY" ReadOnly="True" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <%--<asp:BoundField DataField="DateOfQuotation" HeaderText="QuoatationDate" ReadOnly="True" SortExpression="DateOfQuotation" DataFormatString="{0:d}" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />--%>

                                <asp:BoundField DataField="Amount" HeaderText="Amount" ReadOnly="True" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lbtnEditDependant" ToolTip="Click here To Edit Service" CommandArgument='<%#Eval("pkiTombStoneSelectionID") %>' CommandName="EditService"><i class="fa fa-edit"></i></asp:LinkButton>
                                        &nbsp;
                                               <asp:LinkButton runat="server" ID="lbtnDeleteDependatn" ToolTip="Click here To Delete Service" OnClientClick="return confirm('Are you sure you want to delete it?')" CommandArgument='<%#Eval("pkiTombStoneSelectionID") %>' CommandName="DeleteService"><i class="fa fa-trash"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>Payment</h5>
                    </div>
                    <div class="ibox-content">
                        <div class="row">
                            <uc:ucTombstonePaymentHistory runat="server" ID="ucPaymentHistory1" />
                        </div>
                    </div>
                </div>
                <div class="ibox float-e-margins">
                    <div class="ibox-content">
                        <div class="row">
                            <div class="col-lg-8">
                                <div class="form-group">
                                    <label class="col-sm-2">Notes</label>
                                    <div class="col-sm-10">
                                        <asp:TextBox runat="server" ID="txtNotes" name="CompanyRules" type="text" class="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                    <br />
                                    </div>
                                    <%--<asp:RequiredFieldValidator Display="None" ValidationGroup="CompanySetup" ControlToValidate="txtcompanyRules" ID="RequiredFieldValidator20" ForeColor="red" runat="server" ErrorMessage="Please enter Company Rules"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2">Terms and Condition </label>
                                    <div class="col-sm-10">
                                        <asp:TextBox runat="server" ID="txtTnc" name="CompanyRules" type="text" class="form-control" TextMode="MultiLine" Rows="4" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <%--<asp:RequiredFieldValidator Display="None" ValidationGroup="CompanySetup" ControlToValidate="txtcompanyRules" ID="RequiredFieldValidator20" ForeColor="red" runat="server" ErrorMessage="Please enter Company Rules"></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="form-group row">
                                            <label class="col-sm-2">SubTotal</label>
                                            <div class="col-sm-10">
                                                <asp:TextBox MaxLength="10" runat="server" CssClass="form-control" ID="SubTotal" name="txtTime" type="text" Enabled="false" class="form-control">0</asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label class="col-sm-2">Tax</label>
                                            <div class="col-sm-10">
                                                <asp:DropDownList ID="ddlTax" CssClass="form-control" runat="server" OnSelectedIndexChanged="eveDiscountChange" AutoPostBack="true">
                                                   <%-- <asp:ListItem Value="0.00">0%</asp:ListItem>
                                                    <asp:ListItem Value="14.00">14%</asp:ListItem>--%>
                                                    <%-- <asp:ListItem>25</asp:ListItem>--%>
                                                </asp:DropDownList>
                                                <asp:Label ID="lblTax1" runat="server" Text="Tax Amount:"></asp:Label>
                                                <asp:Label ID="lblTax2" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label class="col-sm-2">Discount in %</label>
                                            <div class="col-sm-10">
                                                <asp:TextBox MaxLength="10" runat="server" ID="txtDiscount" onkeypress="return isDecimalNumber(event,this);" name="txtTime" type="text" class="form-control" AutoPostBack="true" OnTextChanged="eveDiscountChange">0</asp:TextBox>
                                                <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtDiscount" runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="\d+"></asp:RegularExpressionValidator>--%>
                                                <%--<asp:DropDownList ID="ddlCemetery" runat="server" DataTextField="Name" DataValueField="CountryCode" CssClass="form-control m-b"></asp:DropDownList>--%>
                                                <asp:Label ID="lblDisAmt1" runat="server" Text="Discount Amount:"></asp:Label>
                                                <asp:Label ID="lblDisAmt2" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <label class="col-sm-2">Total</label>
                                            <div class="col-sm-10">
                                                <asp:TextBox MaxLength="30" runat="server" ID="txtGrandTotal" name="ttxtCemetery" type="text" class="form-control" ReadOnly="true"></asp:TextBox>
                                                <%--<asp:DropDownList ID="ddlCemetery" runat="server" DataTextField="Name" DataValueField="CountryCode" CssClass="form-control m-b"></asp:DropDownList>--%>
                                                <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtCitizenship" ID="RequiredFieldValidator9" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Button ID="ReCalculate" CssClass="btn btn-sm btn-primary" runat="server" Text="ReCalculate" OnClick="eveDiscountChange" />
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="form-group">
                            </div>
                            <div class="col-lg-6">
                                <div class="btn-group">
                                    <div class="form-group">
                                        <asp:Button ID="btnCancel" CssClass="btn btn-sm btn-primary" runat="server" Text="Cancel" OnClick="btnCancel_click" />
                                    </div>
                                </div>
                                <div class="btn-group">
                                    <div class="form-group">
                                        <asp:Button ID="btnSave" CssClass="btn btn-sm btn-primary" runat="server" Text="Save" OnClick="SaveAllDetails" />
                                    </div>
                                </div>
                                <div class="btn-group">
                                    <div class="form-group">
                                        <asp:Button ID="btnPrint" CssClass="btn btn-sm btn-primary" runat="server" Text="Print" OnClick="btnPrint_Click" />
                                    </div>
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
    <script type="text/javascript" language="javascript">

        var count = 0;
        function isDecimalNumber(evt, c) {
            count = count + 1;
            var charCode = (evt.which) ? evt.which : event.keyCode;
            var dot1 = c.value.indexOf('.');
            var dot2 = c.value.lastIndexOf('.');
            if (count > 4 && dot1 == -1) {
                c.value = "";
                count = 0;
            }
            if (dot1 > 2) {
                c.value = "";
            }
            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            else if (charCode == 46 && (dot1 == dot2) && dot1 != -1 && dot2 != -1)
                return false;

            return true;
        }
    </script>
    <script type="text/javascript" language="javascript">

        var count = 0;
        function isDecimalNumber1(evt, c) {
            count = count + 1;
            var charCode = (evt.which) ? evt.which : event.keyCode;
            var dot1 = c.value.indexOf('.');
            var dot2 = c.value.lastIndexOf('.');
            if (count > 10 && dot1 == -1) {
                c.value = "";
                count = 0;
            }
            if (dot1 > 6) {
                c.value = "";
            }
            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            else if (charCode == 46 && (dot1 == dot2) && dot1 != -1 && dot2 != -1)
                return false;

            return true;
        }
    </script>
    <%-- <div class="span4 proj-div" data-toggle="modal" data-target="#GSCCModal">Clickable content, graphics, whatever</div>--%>

    <div id="GSCCModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;  </button>
                    <h4 class="modal-title" id="myModalLabel">Quotation Reject Message</h4>
                </div>
                <div class="modal-body">
                    <asp:TextBox MaxLength="30" runat="server" ID="txtReject" name="ttxtCemetery" type="text" class="form-control" ValidationGroup="rjt_btn"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="This Field Required" ControlToValidate="txtReject" ValidationGroup="rjt_btn"></asp:RequiredFieldValidator>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <asp:Button ID="Submit" runat="server" type="button" class="btn btn-primary" Text="Submit" ValidationGroup="rjt_btn" />

                </div>
            </div>
        </div>
    </div>

</asp:Content>
