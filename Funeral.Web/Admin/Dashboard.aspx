<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Funeral.Web.Admin.Dashboard" %>

<%@ Register Src="../UserControl/ctrDashboardChart.ascx" TagPrefix="uc1" TagName="ctrDashboardChart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Funeral Policy Admin</h5>
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </div>
                <div class="ibox-content" id="DivpaymentCollection" runat="server">
                    <div class="row">
                        <div class="col-lg-4">
                            <div class="widget style1 red-bg">
                                <div class="row">
                                    <div class="col-sm-4 text-left">
                                        <i class="fa fa-envelope-o fa-5x"></i>
                                    </div>
                                    <div class="col-sm-8 text-right">
                                        <span>SMS Credits</span>
                                        <h2 class="font-bold" id="OpenModel">
                                            <label id="lblTotalSMSCreditsCount" runat="server" data-toggle="modal" data-target="#divSMSTopupModal"></label>
                                        </h2>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="widget style1 red-bg">
                                <div class="row">
                                    <div class="col-sm-4 text-left">
                                        <i class="fa fa-bell fa-5x"></i>
                                    </div>
                                    <div class="col-sm-8 text-right">
                                        <span>Outstanding Payments</span>
                                        <h2 class="font-bold">
                                            <label id="lblOutstandingPaymentsCount" runat="server" data-toggle="modal" data-target="#divOutstandingSendMessages"></label>
                                        </h2>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="widget style1 red-bg">
                                <div class="row">
                                    <div class="col-sm-4 text-left">
                                        <i class="fa fa-trophy fa-5x"></i>
                                    </div>
                                    <div class="col-sm-8 text-right">
                                        <span>Premium Collected</span>
                                        <h2 class="font-bold">
                                            <%--<label id="lblPaymentUpDownIcon" runat="server"></label>--%>
                                             <label id="lblTodayTotalPayment" runat="server" data-toggle="modal" data-target="#divGenerateCashUpReport_FromDashboard"></label>
                                        </h2>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="ibox-content">
                    <uc1:ctrDashboardChart runat="server" ID="ContactUC" Header="User Contact Us Page" />
                </div>
            </div>
            <div id="HideData" runat="server" class="ibox ">
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="pull-right">
                                <asp:DropDownList ID="ddlCompanyList" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ChangeCompanydata"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <div class="input-group">
                                    <label>Show</label>
                                    <asp:DropDownList AutoPostBack="true" ID="ddlPageSize" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>25</asp:ListItem>
                                        <asp:ListItem>50</asp:ListItem>
                                        <asp:ListItem>100</asp:ListItem>
                                        <asp:ListItem>200</asp:ListItem>
                                        <asp:ListItem>250</asp:ListItem>
                                        <asp:ListItem>500</asp:ListItem>
                                    </asp:DropDownList>
                                    entries
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-6">
                            <div class="form-group">
                                <label>Search Service :</label>

                                <asp:Panel ID="pnlSearch" CssClass="input-group" DefaultButton="btnSearch" runat="server">
                                    <asp:TextBox runat="server" ID="txtKeyword" MaxLength="50" CssClass="form-control" placeholder="Search by keyword"></asp:TextBox>
                                    <span class="input-group-btn">&nbsp;<asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-w-m btn-primary" OnClick="btnSearch_Click" />
                                        <br />
                                    </span>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:Label runat="server" ID="Label1"></asp:Label>
                        </div>
                        <asp:Label ID="lblRecords" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="table-responsive">
                                <asp:GridView ID="gvPolicyPremium" OnRowDataBound="gvPolicyPremium_RowDataBound" OnPageIndexChanging="gvPolicyPremium_PageIndexChanging"
                                    OnSorting="gvPolicyPremium_Sorting" AllowSorting="true" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover"
                                    AutoGenerateColumns="False" AllowPaging="True" PageSize="25" EmptyDataText="There are no Services  added.">
                                    <PagerStyle CssClass="pagination-ys" />
                                    <Columns>
                                        <asp:BoundField DataField="DatePaid" HeaderText="Date Paid " SortExpression="DatePaid" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                        <asp:BoundField DataField="AmountPaid" HeaderText="Amount Paid" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                        <asp:BoundField DataField="RecievedBy" HeaderText="RecievedBy" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                        <asp:BoundField DataField="PaymentBranch" HeaderText="Payment Branch" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>


    <!-- Modal -->


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptControl" runat="server">
    <div class="modal fade" id="divSMSTopupModal" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Topup SMS ?</h4>
                </div>
                <div class="modal-body">
                    <div class="col-sm-12">
                        <div class="form-group row">

                            <input id="txtTopupSMS" runat="server" class="form-control" placeholder="Enter Topup SMS"/>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" ID="btnAddTopUp" class="btn btn-default" Text="Save"  OnClick="btnAddTopUp_Click" />
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

     <div class="modal fade" id="divOutstandingSendMessages" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Send SMS to All Members Outstanding for the Month ?</h4>
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" ID="btnSendMessagesoutstaning" class="btn btn-default" Text="Yes"  OnClick="btnSendMessagesoutstaning_Click" />
                    <button type="button" class="btn btn-default" data-dismiss="modal">No</button>
                </div>
            </div>
        </div>
    </div>
     <div class="modal fade" id="divGenerateCashUpReport_FromDashboard" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Would you like to generate daily Cashup report ?</h4>
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" ID="btnGenerateCashupReport" class="btn btn-default" Text="Yes"  OnClick="btnGenerateCashupReport_Click" />
                    <button type="button" class="btn btn-default" data-dismiss="modal">No</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
