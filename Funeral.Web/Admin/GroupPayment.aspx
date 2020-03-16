<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="GroupPayment.aspx.cs" Inherits="Funeral.Web.Admin.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <div class="ibox ">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h5>Payment Details</h5>
                            </div>
                            <div class="ibox-content" style="height: 570px;">
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <label>Select Branch</label>
                                        <asp:DropDownList ID="ddlBankBranch" class="form-control" runat="server">
                                            <asp:ListItem Value="0">Select Branch</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>Recieved By</label>
                                        <asp:TextBox runat="server" ID="txtChildren" ReadOnly="false" name="name" type="text" class="form-control"></asp:TextBox>
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
                                    <div class="form-group">
                                        <label>Amount</label>
                                        <asp:TextBox MaxLength="255" runat="server" ID="txtAmount" name="name" ReadOnly="false" type="text" class="form-control"></asp:TextBox>
                                    </div>

                                </div>
                                <div class="col-lg-6" style="margin-right:1%;">
                                   <%-- <div class="col-lg-12">--%>
                                        <div class="form-group">
                                            <label>Notes</label>
                                            <textarea id="TextArea1" cols="20" name="S1" rows="2" class="form-control"></textarea>
                                        </div>
                                    <%--</div>--%>
                                </div>
                                <div class="">
                                    <div class="form-group"></div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <asp:Button runat="server" ID="btnAdd" Width="95%" CssClass="btn btn-md btn-primary" Text="Add Payment" />
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <asp:Button runat="server" ID="btnPrint" Width="95%" CssClass="btn btn-md btn-primary" Text="Print" />
                                        </div>
                                    </div>
                                </div>
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Payment History</h5>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="col-lg-12">
                                            <div class="form-group">
                                                <div class="form-group">
                                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" AllowPaging="True" AllowSorting="True">
                                                        <PagerStyle CssClass="pagination-ys" />
                                                        <Columns>
                                                            <asp:BoundField DataField="AmountPaid" HeaderText="AmountPaid" SortExpression="AmountPaid" />
                                                            <asp:BoundField DataField="PaidBy" HeaderText="PaidBy" SortExpression="PaidBy" />
                                                            <asp:BoundField DataField="DatePaid" HeaderText="DatePaid" SortExpression="DatePaid" />
                                                            <asp:BoundField DataField="RecievedBy" HeaderText="RecievedBy" SortExpression="RecievedBy" />
                                                            <asp:BoundField DataField="PaymentBranch" HeaderText="PaymentBranch" SortExpression="PaymentBranch" />
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
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptControl" runat="server">
</asp:Content>
