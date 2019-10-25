<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="FuneralPayment.aspx.cs" Inherits="Funeral.Web.Admin.FuneralPayment" %>

<%@ Register Src="~/UserControl/ctrlFuneralPaymentHistory.ascx" TagName="ucFuneralPaymentHistory" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="hdnTotalPremium" />
    <asp:HiddenField runat="server" ID="hdnAmount" />
    <div class="row">
        <div class="col-lg-12">
            <div class="ibox ">
                <div class="row">
                    <div class="col-lg-6">
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h5>Payment Details</h5>
                            </div>
                            <div class="ibox-content">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label>Funeral Number</label>
                                            <asp:TextBox MaxLength="255" runat="server" ReadOnly="true" ID="txtFuneralId" name="name" type="text" class="form-control"></asp:TextBox>
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
                                            <label>Total Amount</label>
                                            <asp:TextBox MaxLength="255" CssClass="form-control" runat="server" ID="txtAmount" name="name" type="text" class="form-control"></asp:TextBox>
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
                                            <label>Month Paid<em>*</em>  </label>
                                            <asp:TextBox TextMode="MultiLine" CssClass="form-control" runat="server" ID="txtMohthPaid" name="name" type="text" class="form-control"></asp:TextBox>
                                        </div>

                                        <div class="form-group">
                                            <asp:Button runat="server" ID="btnBack" CssClass="btn btn-md btn-primary" Text="Click here to go back" Width="95%" OnClick="btnBack_Click" />
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <asp:Button runat="server" ID="btnPay" CssClass="btn btn-md btn-primary" Text="Add Payment" Width="95%" ValidationGroup="tab1" OnClick="btnPay_Click" />
                                            <asp:Button runat="server" ID="btnPrint" Enabled="false" CssClass="btn btn-md btn-primary" Text="Print" Width="95%" OnClick="btnPrint_Click" />

                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <div class="ibox ">
                            <div class="ibox-title">
                                <h5>Invoice</h5>
                                <br />
                                <br />
                                <div class="table-responsive">
                                    <asp:GridView ID="gvServiceList" OnRowDataBound="gvServiceList_RowDataBound" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="There are no Service added.">
                                        <PagerStyle CssClass="pagination-ys" />
                                        <Columns>
                                            <asp:BoundField DataField="ServiceName" HeaderText="Service Name" ReadOnly="True" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />

                                            <asp:BoundField DataField="ServiceDesc" HeaderText="Desctiption" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />

                                            <asp:BoundField DataField="ServiceRate" HeaderText="Rate" ReadOnly="True" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                            <asp:BoundField DataField="Quantity" HeaderText="QTY" ReadOnly="True" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />


                                            <asp:BoundField DataField="Amount" HeaderText="Amount" ReadOnly="True" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />

                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="modal-header">           
            <h4 class="modal-title">Payment history</h4>
        </div>
        <uc:ucFuneralPaymentHistory runat="server" ID="ucPaymenthistory" />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptControl" runat="server">
    <script type="text/javascript">
      <%--  $(document).ready(function () {
            $("#<%=txtNextPaymentDate.ClientID %>").datepicker({ format: 'dd MM yyyy' });
        })--%>

        function ConfirmReInstate() {
            var del;
            del = confirm("Would you like to Reinstate this policy?");
            if (del) {
                return false;
            }
        }

        function ConfirmReJoin() {
            var del;
            del = confirm("Would you like to Rejoin this policy?");
            if (del) {
                return false;
            }
        }

    </script>
</asp:Content>

