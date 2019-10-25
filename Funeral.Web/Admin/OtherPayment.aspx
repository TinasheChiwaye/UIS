<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Admin.Master" CodeBehind="OtherPayment.aspx.cs" Inherits="Funeral.Web.Admin.OtherPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <%--<script src="<%=ResolveUrl("~/Scripts/plugins/datapicker/bootstrap-datepicker.js")%>"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--<asp:HiddenField runat="server" ID="hdnTotalPremium" />
    <asp:HiddenField runat="server" ID="hdnAmount" />--%>
        <div class="col-lg-12">
            <div class="row">
                <div class="col-lg-6">
                    <asp:Label runat="server" ID="lblMessage"></asp:Label>
                </div>
            </div>
            <div class="ibox ">
                <div class="row">
                    <div class="col-lg-6">
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h5>Payment Details</h5>
                            </div>
                            <div class="ibox-content">
                                <div class="form-group">
                                    <label>Member No  <em>*</em> </label>
                                    <asp:TextBox MaxLength="255" runat="server" ID="txtMemberNo" name="name" type="text" class="form-control" AutoPostBack="true"></asp:TextBox>
                                </div>
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label>Member    </label>
                                            <asp:TextBox MaxLength="255" runat="server" ReadOnly="true" ID="txtMember" name="name" type="text" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label>Received By  </label>
                                            <asp:TextBox MaxLength="255" runat="server" ReadOnly="true" ID="txtReceivedBy" name="name" type="text" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label>Amount</label>
                                            <asp:TextBox MaxLength="255" CssClass="form-control" runat="server" ID="txtAmount" name="name" type="text" class="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ValidationGroup="tab1" ControlToValidate="txtAmount"
                                                ID="RequiredFieldValidator3" ForeColor="red" runat="server" ErrorMessage="Enter payment amount"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label>Method</label>
                                            <asp:DropDownList runat="server" ID="ddlMethod" class="form-control m-b">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">Cash</asp:ListItem>
                                                <asp:ListItem Value="2">Easy Pay</asp:ListItem>
                                                <asp:ListItem Value="3">Debit Order</asp:ListItem>
                                                <asp:ListItem Value="4">Bank Depost</asp:ListItem>
                                                <asp:ListItem Value="5">EFT</asp:ListItem>
                                                <asp:ListItem Value="6">Other</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label>Date</label>
                                            <asp:TextBox MaxLength="255" runat="server" ReadOnly="true" ID="txtNextPaymentDate" name="name" type="text" class="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ValidationGroup="tab1" ControlToValidate="txtNextPaymentDate"
                                                ID="RequiredFieldValidator2" ForeColor="red" runat="server" ErrorMessage="Enter payment date"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label>PaymentType</label>
                                            <asp:DropDownList runat="server" CssClass="form-control m-b" ID="ddlPaymentType" class="form-control m-b">                                              
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ValidationGroup="tab1" ControlToValidate="ddlPaymentType" InitialValue="0"
                                                ID="RequiredFieldValidator1" ForeColor="red" runat="server" ErrorMessage="Select payment type"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label>Notes<em>*</em>  </label>
                                            <asp:TextBox TextMode="MultiLine" CssClass="form-control" runat="server" ID="txtnotes" name="name" type="text" class="form-control"></asp:TextBox>
                                        </div>

                                        <div class="form-group">
                                            <asp:Button runat="server" ID="btnPay" CssClass="btn btn-md btn-primary" Text="Save" Width="95%" ValidationGroup="tab1" onclick="btnPay_click" />
                                        </div>
                                    </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
      
     <div class="row">
        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Payment History</h5>
                    <asp:HiddenField runat="server" ID="hdnLastpaymentDate" />
                    <br />
                    <br />
                    <div class="table-responsive">
                        <asp:GridView ID="gvOtherPayment" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover"
                            AllowPaging="true" PageSize="25" OnPageIndexChanging="gvInvoices_PageIndexChanging"   AutoGenerateColumns="False" EmptyDataText="There are no payment history ."  OnRowCommand="gvInvoices_RowCommand">
                
                            <PagerStyle CssClass="pagination-ys" />
                            <Columns>
                                <asp:BoundField DataField="DatePaid" HeaderText="Date paid" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="AmountPaid" HeaderText="Amount" ReadOnly="True" />
                                <asp:BoundField DataField="RecievedBy" HeaderText="Recieved by" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                <%--<asp:BoundField DataField="PaidBy" HeaderText="Paid By" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />--%>
                                <asp:BoundField DataField="Notes" HeaderText="Months Paid & Notes" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <%if(this.HasEditRight){ %>
                                        <asp:LinkButton  runat="server" ID="LinkButton3" ToolTip="Click here Edit "   CommandArgument='<%#Eval("pkiInvoiceID") %>' CommandName="EditInvoice"><i class="fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton> &nbsp;
                                        <%}if(this.HasReadRight){ %>
                                        <asp:LinkButton runat="server" ID="LinkButton1" ToolTip="Click here Print " CommandArgument='<%#Eval("pkiInvoiceID") %>' CommandName="PrintPremium"><i class="glyphicon glyphicon-print"></i></asp:LinkButton> &nbsp;
                                        <asp:LinkButton runat="server" ID="LinkButton2" ToolTip="Click here Print " CommandArgument='<%#Eval("pkiInvoiceID") %>' CommandName="PrintFullPremium"><i class="glyphicon glyphicon-export"></i></asp:LinkButton>
                                    <%} %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                  
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="ScriptControl" runat="server">
     <script type="text/javascript">
         $(document).ready(function () {

             $("#<%=txtNextPaymentDate.ClientID %>").datepicker({ format: 'dd MM yyyy' });

         });
         </script>

    </asp:Content>