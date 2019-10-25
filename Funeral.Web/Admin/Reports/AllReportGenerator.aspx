<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AllReportGenerator.aspx.cs" Inherits="Funeral.Web.Admin.Reports.AllReportGenerator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <style>
        #MainContent_chklPolicyStatus tr td {
            padding: 8px;
        }

        .col-sm-4, .col-sm-2 {
            border-right: solid 2px #1ab394;
        }

        #MainContent_chklPolicyStatus tr td input[type=checkbox], #MainContent_chklPolicyStatus tr td input[type=radio] {
            vertical-align: sub;
            margin-right: 3px;
        }

        .gvOutstanding td, .gvOutstanding th {
            padding: 5px;
        }
    </style>
    <div class="row">
        <div class="col-sm-12">
            <asp:Panel ID="pnlReportSetter" runat="server">
                <div class="col-sm-12 sideBar padding-top-20">
                    <div class="row ">
                        <div class="col-sm-12">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <span class="nav-label"><i class="fa-x fa fa-newspaper-o "></i>Generate Report</span>
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="panel blank-panel">
                                            <div class="panel-heading">
                                                <div class="panel-title">
                                                    <h4>Generate Report by Selecting Reference</h4>                                                    
                                                    <div class="ibox-content">
                                                        <div role="form" class="form col-sm-12">
                                                            <div class="col-sm-6">
                                                                <div class="form-group">
                                                                    <label for="exampleInputEmail2">Report Type</label>
                                                                    <asp:DropDownList ID="ddlReportType" class="form-control" runat="server" onchange="GetSelectedTextValue(this)"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <div class="form-group">
                                                                    <label for="exampleInputEmail2">Report </label>
                                                                    <asp:DropDownList ID="ddlAdminReort" class="form-control" runat="server" onchange="GetSelectedPanel(this)">
                                                                        <asp:ListItem Text="Select Report" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <asp:Panel runat="server" ID="pnlAdminReport">
                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <asp:Panel ID="pnlAllMembers" runat="server">
                                                                            <div class="form-group">
                                                                                <button runat="server" validationgroup="pnlAllMembers" id="btnPnlAllJoinedMemebrs" onserverclick="btnSubmit_Click" class="btn btn-outline pull-right btn-primary" title="Generate">
                                                                                    <i class="fa fa-check"></i>Generate
                                                                                </button>
                                                                            </div>
                                                                        </asp:Panel>
                                                                        <asp:Panel ID="pnlMembersJoinedByDate" runat="server">
                                                                            <div class="col-sm-6">
                                                                            <div class="form-group">
                                                                                <label class="" for="email">Branch Name </label>
                                                                                <asp:DropDownList ID="ddlMemberBranch" runat="server" DataTextField="BranchName" DataValueField="Brancheid" CssClass="form-control m-b"></asp:DropDownList>
                                                                            </div>
                                                                                </div>
                                                                            <div class="col-sm-6">
                                                                            <div class="form-group">
                                                                                <label class="" for="email">Member Plan </label>
                                                                                <asp:DropDownList ID="ddlMemberPlan" runat="server" DataTextField="PlanName" DataValueField="pkiPlanID" CssClass="form-control m-b"></asp:DropDownList>
                                                                            </div>
                                                                                </div>
                                                                            <div class="col-sm-6">
                                                                                <div class="form-group">
                                                                                    <label class="" for="email">Date From </label>
                                                                                    <asp:RequiredFieldValidator ValidationGroup="pnlMembersByJoinedDate" ID="RequiredFieldValidator3" ForeColor="Red" ControlToValidate="txtDateFrom" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                    <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtDateFrom" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-6">
                                                                                <div class="form-group">
                                                                                    <label class="" for="email">Date To </label>
                                                                                    <asp:RequiredFieldValidator ValidationGroup="pnlMembersByJoinedDate" ID="RequiredFieldValidator1" ForeColor="Red" ControlToValidate="txtDateTo" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                    <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtDateTo" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <button runat="server" validationgroup="pnlMembersByJoinedDate" id="btnPnlMembersByJoinedDate" onserverclick="btnSubmit_Click" class="btn btn-outline pull-right btn-primary" title="Generate">
                                                                                    <i class="fa fa-check"></i>Generate
                                                                                </button>
                                                                            </div>
                                                                        </asp:Panel>
                                                                        <asp:Panel ID="pnlPolicyStatus" runat="server">
                                                                            <div class="form-group">
                                                                                <label class="" for="email">Policy Status:</label>
                                                                                <asp:DropDownList ID="ddlPolicyStatus" runat="server" class="form-control">
                                                                                    <asp:ListItem Selected="True">All</asp:ListItem>
                                                                                    <asp:ListItem>Active</asp:ListItem>
                                                                                    <asp:ListItem>Cancelled</asp:ListItem>
                                                                                    <asp:ListItem>Deceased</asp:ListItem>
                                                                                    <asp:ListItem>Delete</asp:ListItem>
                                                                                    <asp:ListItem>Historic</asp:ListItem>
                                                                                    <asp:ListItem>Lapsed</asp:ListItem>
                                                                                    <asp:ListItem>On Trial</asp:ListItem>
                                                                                    <asp:ListItem>Single</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                            <div class="col-sm-6">
                                                                                <div class="form-group">
                                                                                    <label class="" for="email">Date From </label>
                                                                                    <asp:RequiredFieldValidator ValidationGroup="pnlPolicyStatus" ID="RequiredFieldValidator29" ForeColor="Red" ControlToValidate="txtPolicyDateFrom" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                    <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtPolicyDateFrom" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-6">
                                                                                <div class="form-group">
                                                                                    <label class="" for="email">Date From </label>
                                                                                    <asp:RequiredFieldValidator ValidationGroup="pnlPolicyStatus" ID="RequiredFieldValidator30" ForeColor="Red" ControlToValidate="txtPolicyDateTo" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                    <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtPolicyDateTo" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <div class="form-group">
                                                                                    <button runat="server" validationgroup="pnlPolicyStatus" id="btnPnlPolicyStatus" onserverclick="btnSubmit_Click" class="btn btn-outline pull-right btn-primary" title="Generate">
                                                                                        <i class="fa fa-check"></i>Generate
                                                                                    </button>
                                                                                </div>
                                                                            </div>
                                                                        </asp:Panel>
                                                                        <asp:Panel ID="pnlMembersPerBranch" runat="server">
                                                                            <div class="form-group">
                                                                                <label class="" for="email">Branch Name </label>
                                                                                <asp:DropDownList ID="ddlMemberPerBranch" runat="server" DataTextField="BranchName" DataValueField="Brancheid" CssClass="form-control"></asp:DropDownList>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <button runat="server" validationgroup="pnlMembersPerBranch" id="btnPnlMembersPerBranch" onserverclick="btnSubmit_Click" class="btn btn-outline pull-right btn-primary" title="Generate">
                                                                                    <i class="fa fa-check"></i>Generate
                                                                                </button>
                                                                            </div>
                                                                        </asp:Panel>
                                                                        <asp:Panel ID="pnlMemberPlan" runat="server">
                                                                            <div class="form-group">
                                                                                <label class="" for="email">Member Plan </label>
                                                                                <asp:DropDownList ID="ddlPlanname" runat="server" DataTextField="PlanName" DataValueField="pkiPlanID" CssClass="form-control"></asp:DropDownList>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <button runat="server" validationgroup="pnlMembersPerPlan" id="btnPnlMembersPerPlan" onserverclick="btnSubmit_Click" class="btn btn-outline pull-right btn-primary" title="Generate">
                                                                                    <i class="fa fa-check"></i>Generate
                                                                                </button>
                                                                            </div>
                                                                        </asp:Panel>
                                                                        <asp:Panel ID="pnlMemberUnderwriting" runat="server">
                                                                            <div class="form-group">
                                                                                <label class="" for="email">Member Underwriting:</label>
                                                                                <asp:DropDownList CssClass="form-control" ID="ddlUnderwriter" runat="server"></asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ValidationGroup="pnlMembersUnderWriting" ID="RequiredFieldValidator6" ForeColor="Red" ControlToValidate="ddlUnderwriter" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <button runat="server" validationgroup="pnlMembersUnderWriting" id="btnPnlMembersUnderWriting" onserverclick="btnSubmit_Click" class="btn btn-outline pull-right btn-primary" title="Generate">
                                                                                    <i class="fa fa-check"></i>Generate
                                                                                </button>
                                                                            </div>
                                                                        </asp:Panel>

                                                                        <asp:Panel ID="pnlAgentReport" runat="server">
                                                                            <div class="form-group">
                                                                                <label class="" for="email">Agent </label>
                                                                                <asp:DropDownList CssClass="form-control" ID="ddlAgent" runat="server"></asp:DropDownList>
                                                                            </div>
                                                                            <div class="col-sm-6">
                                                                                <div class="form-group">
                                                                                    <label class="" for="email">Date From </label>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="AgentReport" ForeColor="Red" ControlToValidate="txtAgentAdminDateFrom" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                    <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtAgentAdminDateFrom" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-6">
                                                                                <div class="form-group">
                                                                                    <label class="" for="email">Date To </label>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="AgentReport" ForeColor="Red" ControlToValidate="txtAgentAdminDateTo" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                    <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtAgentAdminDateTo" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <button runat="server" validationgroup="AgentReport" id="btnAgentReport" onserverclick="btnSubmit_Click" class="btn btn-outline pull-right btn-primary" title="Generate">
                                                                                <i class="fa fa-check"></i>Generate
                                                                            </button>
                                                                        </asp:Panel>
                                                                        <asp:Panel ID="pnlFuneralarrangementreport" runat="server">
                                                                            <div class="col-sm-6">
                                                                                <div class="form-group">
                                                                                    <label class="" for="email">Date From </label>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator31" ValidationGroup="FuneralArrangment" ForeColor="Red" ControlToValidate="txtFuneralArrangmentDateFrom" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                    <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtFuneralArrangmentDateFrom" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-6">
                                                                                <div class="form-group">
                                                                                    <label class="" for="email">Date To </label>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator32" ValidationGroup="FuneralArrangment" ForeColor="Red" ControlToValidate="txtFuneralArrangmentDateTo" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                    <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtFuneralArrangmentDateTo" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <button runat="server" validationgroup="FuneralArrangment" id="btnFuneralArrangement" onserverclick="btnSubmit_Click" class="btn btn-outline pull-right btn-primary" title="Generate">
                                                                                <i class="fa fa-check"></i>Generate
                                                                            </button>
                                                                        </asp:Panel>
                                                                        <asp:Panel ID="pnlOutstandingpayment" runat="server">
                                                                            <button runat="server" validationgroup="outstandingPayment" id="btnoutstandingPayment" onserverclick="btnSubmit_Click" class="btn btn-outline pull-right btn-primary" title="Generate">
                                                                                <i class="fa fa-check"></i>Generate
                                                                            </button>
                                                                        </asp:Panel>
                                                                        <asp:Panel ID="pnlQuotationByDate" runat="server">
                                                                            <div class="col-sm-6">
                                                                                <div class="form-group">
                                                                                    <label class="" for="email">Date From </label>
                                                                                    <asp:RequiredFieldValidator ValidationGroup="QuotationReport" ID="RequiredFieldValidator25" ForeColor="Red" ControlToValidate="txtQuotationDateFrom" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                    <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtQuotationDateFrom" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-6">
                                                                                <div class="form-group">
                                                                                    <label class="" for="email">Date to </label>
                                                                                    <asp:RequiredFieldValidator ValidationGroup="QuotationReport" ID="RequiredFieldValidator26" ForeColor="Red" ControlToValidate="txtQuotationDateTo" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                    <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtQuotationDateTo" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <button runat="server" id="btnQuoteByDate" validationgroup="QuotationReport" onserverclick="btnSubmit_Click" class="btn btn-outline pull-right btn-primary" title="Generate">
                                                                                <i class="fa fa-check"></i>Generate
                                                                            </button>
                                                                        </asp:Panel>
                                                                        <asp:Panel ID="pnlDependantsOver21" runat="server">
                                                                            <button runat="server" validationgroup="DepandendatsPayment" id="btnDepandantsOver" onserverclick="btnSubmit_Click" class="btn btn-outline pull-right btn-primary" title="Generate">
                                                                                <i class="fa fa-check"></i>Generate
                                                                            </button>
                                                                        </asp:Panel>
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>
                                                            <!--Finance Report Section -->
                                                            <asp:Panel runat="server" ID="pnlFinanceReport">
                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <asp:Panel ID="pnlAgentCommision" runat="server">
                                                                            <div class="form-group">
                                                                                <label class="" for="email">Agent </label>
                                                                                <asp:DropDownList CssClass="form-control" ID="ddlAgentComition" runat="server"></asp:DropDownList>
                                                                            </div>
                                                                            <div class="col-sm-6">
                                                                                <div class="form-group">
                                                                                    <label class="" for="email">Date From </label>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" ValidationGroup="AgentComm" ForeColor="Red" ControlToValidate="txtAgentDateFrom" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                    <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtAgentDateFrom" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-6">
                                                                                <div class="form-group">
                                                                                    <label class="" for="email">Date To </label>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" ValidationGroup="AgentComm" ForeColor="Red" ControlToValidate="txtAgentDateTo" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                    <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtAgentDateTo" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <button runat="server" id="btnAgent" validationgroup="AgentComm" onserverclick="btnSubmit_Click" class="btn btn-outline pull-right btn-primary" title="Generate">
                                                                                <i class="fa fa-check"></i>Generate
                                                                            </button>
                                                                        </asp:Panel>
                                                                        <asp:Panel ID="pnlClaimsReports" runat="server">
                                                                            <div class="form-group">
                                                                                <label class="" for="email">Claim Status:</label>
                                                                                <asp:DropDownList ID="ddlClaims" runat="server" CssClass="form-control" Style="min-width: 60%">
                                                                                    <asp:ListItem Selected="True">All</asp:ListItem>
                                                                                    <asp:ListItem>Payment Done</asp:ListItem>
                                                                                    <asp:ListItem>Funeral Done</asp:ListItem>

                                                                                </asp:DropDownList>
                                                                            </div>
                                                                            <div class="col-sm-6">
                                                                                <div class="form-group">
                                                                                    <label class="" for="email">Date From </label>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="Claims" ForeColor="Red" ControlToValidate="txtClaimDateFrom" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                    <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtClaimDateFrom" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-6">
                                                                                <div class="form-group">
                                                                                    <label class="" for="email">Date To </label>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="Claims" ForeColor="Red" ControlToValidate="txtClaimDateTo" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                    <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtClaimDateTo" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <button runat="server" id="btnClaims" validationgroup="Claims" onserverclick="btnSubmit_Click" class="btn btn-outline pull-right btn-primary" title="Generate">
                                                                                <i class="fa fa-check"></i>Generate
                                                                            </button>
                                                                        </asp:Panel>
                                                                        <asp:Panel ID="pnlMethodofpayment" runat="server">
                                                                            <div class="form-group">
                                                                                <label class="" for="email">Method</label>
                                                                                <asp:DropDownList runat="server" ID="ddlMethod" class="form-control">
                                                                                    <asp:ListItem Value="0">All</asp:ListItem>
                                                                                    <asp:ListItem Value="1">Cash</asp:ListItem>
                                                                                    <asp:ListItem Value="2">Easy Pay</asp:ListItem>
                                                                                    <asp:ListItem Value="3">Debit Order</asp:ListItem>
                                                                                    <asp:ListItem Value="4">Bank Depost</asp:ListItem>
                                                                                    <asp:ListItem Value="5">EFT</asp:ListItem>
                                                                                    <asp:ListItem Value="6">Other</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                            <div class="col-sm-6">
                                                                                <div class="form-group">
                                                                                    <label class="" for="email">Date From </label>
                                                                                    <asp:RequiredFieldValidator ValidationGroup="DetailedPayement" ID="RequiredFieldValidator9" ForeColor="Red" ControlToValidate="txtDatailedPaymentDatefrom" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                    <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtDatailedPaymentDatefrom" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-6">
                                                                                <div class="form-group">
                                                                                    <label class="" for="email">Date to </label>
                                                                                    <asp:RequiredFieldValidator ValidationGroup="DetailedPayement" ID="RequiredFieldValidator10" ForeColor="Red" ControlToValidate="txtDatailedPaymentDateTo" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                    <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtDatailedPaymentDateTo" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <button runat="server" id="btnDetailedPayment" validationgroup="DetailedPayement" onserverclick="btnSubmit_Click" class="btn btn-outline pull-right btn-primary" title="Generate">
                                                                                <i class="fa fa-check"></i>Generate
                                                                            </button>
                                                                        </asp:Panel>
                                                                        <asp:Panel ID="pnlExpenses" runat="server">
                                                                            <div class="col-sm-6">
                                                                                <div class="form-group">
                                                                                    <label class="" for="email">Date From </label>
                                                                                    <asp:RequiredFieldValidator ValidationGroup="expense" ID="RequiredFieldValidator17" ForeColor="Red" ControlToValidate="txtExpensDateFrom" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                    <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtExpensDateFrom" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-6">
                                                                                <div class="form-group">
                                                                                    <label class="" for="email">Date to </label>
                                                                                    <asp:RequiredFieldValidator ValidationGroup="expense" ID="RequiredFieldValidator18" ForeColor="Red" ControlToValidate="txtExpensDateTo" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                    <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtExpensDateTo" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <button runat="server" validationgroup="expense" id="btnExpenses" onserverclick="btnSubmit_Click" class="btn btn-outline pull-right btn-primary" title="Generate">
                                                                                <i class="fa fa-check"></i>Generate
                                                                            </button>
                                                                        </asp:Panel>
                                                                        <asp:Panel ID="pnlFuneralPayment" runat="server">
                                                                            <div class="col-sm-6">
                                                                                <div class="form-group">
                                                                                    <label class="" for="email">Date From </label>
                                                                                    <asp:RequiredFieldValidator ValidationGroup="FuneralPayment" ID="RequiredFieldValidator11" ForeColor="Red" ControlToValidate="txtFFPDateFrom" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                    <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtFFPDateFrom" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-6">
                                                                                <div class="form-group">
                                                                                    <label class="" for="email">Date to </label>
                                                                                    <asp:RequiredFieldValidator ValidationGroup="FuneralPayment" ID="RequiredFieldValidator12" ForeColor="Red" ControlToValidate="txtFFPDateTo" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                    <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtFFPDateTo" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <button runat="server" id="btnFuneralPayment" validationgroup="FuneralPayment" onserverclick="btnSubmit_Click" class="btn btn-outline pull-right btn-primary" title="Generate">
                                                                                <i class="fa fa-check"></i>Generate
                                                                            </button>
                                                                        </asp:Panel>
                                                                        <asp:Panel ID="pnlPaymentByBranch" runat="server">
                                                                            <div class="form-group">
                                                                                <label class="" for="email">Branch </label>
                                                                                <asp:DropDownList ID="ddlBranch" runat="server" DataTextField="BranchName" DataValueField="Brancheid" CssClass="form-control m-b"></asp:DropDownList>
                                                                            </div>
                                                                            <div class="col-sm-6">
                                                                                <div class="form-group">
                                                                                    <label class="" for="email">Date From </label>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ValidationGroup="PaymentBranch" ForeColor="Red" ControlToValidate="txtBranchDateFrom" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                    <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtBranchDateFrom" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-6">
                                                                                <div class="form-group">
                                                                                    <label class="" for="email">Date To </label>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ValidationGroup="PaymentBranch" ForeColor="Red" ControlToValidate="txtBranchDateTo" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                    <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtBranchDateTo" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <button runat="server" id="btnBranch" validationgroup="PaymentBranch" onserverclick="btnSubmit_Click" class="btn btn-outline pull-right btn-primary" title="Generate">
                                                                                <i class="fa fa-check"></i>Generate
                                                                            </button>
                                                                        </asp:Panel>
                                                                        <asp:Panel ID="pnlPaymentByDate" runat="server">
                                                                            <div class="col-sm-6">
                                                                                <div class="form-group">
                                                                                    <label class="" for="email">Date From </label>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ValidationGroup="PaymentbyDate" ForeColor="Red" ControlToValidate="txtPaymentbyDateFrom" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                    <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtPaymentbyDateFrom" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-6">
                                                                                <div class="form-group">
                                                                                    <label class="" for="email">Date To </label>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" ValidationGroup="PaymentbyDate" ForeColor="Red" ControlToValidate="txtPaymentbyDateTo" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                    <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtPaymentbyDateTo" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <button runat="server" id="btnPaymentbyDate" validationgroup="PaymentbyDate" onserverclick="btnSubmit_Click" class="btn btn-outline pull-right btn-primary" title="Generate">
                                                                                <i class="fa fa-check"></i>Generate
                                                                            </button>
                                                                        </asp:Panel>
                                                                        <asp:Panel ID="pnlPaymentBySociety" runat="server">
                                                                            <div class="form-group">
                                                                                <label class="" for="email">Society </label>
                                                                                <asp:DropDownList ID="ddlSociety" runat="server" DataTextField="SocietyName" DataValueField="pkiSocietyID" CssClass="form-control"></asp:DropDownList>
                                                                            </div>
                                                                            <div class="col-sm-6">
                                                                                <div class="form-group">
                                                                                    <label class="" for="email">Date From </label>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator23" ValidationGroup="PaymentbySociety" ForeColor="Red" ControlToValidate="txtsocietyDateFrom" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                    <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtsocietyDateFrom" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-6">
                                                                                <div class="form-group">
                                                                                    <label class="" for="email">Date To </label>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator24" ValidationGroup="PaymentbySociety" ForeColor="Red" ControlToValidate="txtsocietyDateTo" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                    <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtsocietyDateTo" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <button runat="server" id="btnSocietty" validationgroup="PaymentbySociety" onserverclick="btnSubmit_Click" class="btn btn-outline pull-right btn-primary" title="Generate">
                                                                                <i class="fa fa-check"></i>Generate
                                                                            </button>
                                                                        </asp:Panel>
                                                                        <asp:Panel ID="pnlUnderwriterPayment" runat="server">
                                                                            <div class="form-group">
                                                                                <label class="" for="email">Under Writer </label>
                                                                                <asp:DropDownList CssClass="form-control" ID="ddlUnderwriterPayment" runat="server"></asp:DropDownList>
                                                                            </div>
                                                                            <div class="col-sm-6">
                                                                                <div class="form-group">
                                                                                    <label class="" for="email">Date From </label>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator27" ValidationGroup="Underwiter" ForeColor="Red" ControlToValidate="txtUnderwriterDateFrom" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                    <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtUnderwriterDateFrom" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-6">
                                                                                <div class="form-group">
                                                                                    <label class="" for="email">Date To </label>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator28" ValidationGroup="Underwiter" ForeColor="Red" ControlToValidate="txtUnderwriterDateTo" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                    <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtUnderwriterDateTo" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <button runat="server" id="btnUnderwriter" validationgroup="Underwiter" onserverclick="btnSubmit_Click" class="btn btn-outline pull-right btn-primary" title="Generate">
                                                                                <i class="fa fa-check"></i>Generate
                                                                            </button>
                                                                        </asp:Panel>
                                                                        <asp:Panel ID="pnlMyCashUp" runat="server">
                                                                            <div class="col-sm-6">
                                                                                <div class="form-group">
                                                                                    <label class="" for="email">Date From </label>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" ValidationGroup="MyCashUp" ForeColor="Red" ControlToValidate="txtDateMyCashUpFrom" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                    <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtDateMyCashUpFrom" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-6">
                                                                                <div class="form-group">
                                                                                    <label class="" for="email">Date To </label>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22" ValidationGroup="MyCashUp" ForeColor="Red" ControlToValidate="txtDateMyCashUpTo" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                    <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtDateMyCashUpTo" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <button runat="server" id="btnMyCashUp" validationgroup="MyCashUp" onserverclick="btnSubmit_Click" class="btn btn-outline pull-right btn-primary" title="Generate">
                                                                                <i class="fa fa-check"></i>Generate
                                                                            </button>
                                                                        </asp:Panel>
                                                                        <asp:Panel ID="pnlBranches" runat="server">
                                                                            <div class="form-group">
                                                                                <button runat="server" validationgroup="pnlBranches" id="btnBranches" onserverclick="btnSubmit_Click" class="btn btn-outline pull-right btn-primary" title="Generate">
                                                                                    <i class="fa fa-check"></i>Generate
                                                                                </button>
                                                                            </div>
                                                                        </asp:Panel>
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>
                                                            <asp:HiddenField ID="hfReportType" runat="server" />
                                                            <asp:HiddenField ID="hfAdminReport" runat="server" />
                                                        </div>
                                                        <div role="form" class="form col-sm-12">
                                                            <asp:Label runat="server" ID="lblMessage"></asp:Label>
                                                        </div>
                                                        <div role="form" class="form col-sm-12">
                                                            <iframe id="iframe1" frameborder="0" style="width: 100%; height: 100%; min-height: 750px;" runat="server"></iframe>
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
            </asp:Panel>
        </div>
    </div>
    <div class="row" id="divrdlc" runat="server">
        <div class="col-sm-12">
        </div>
        <div class="col-sm-12">
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptControl" runat="server">
    <script type="text/javascript">
        function pageLoad(sender, args) {
            $(".datepicker").datepicker({ format: 'dd MM yyyy' });
            $(".datepicker").on('changeDate', function (ev) {
                $(this).datepicker('hide');
            })
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            DisableAllPanel();
            var ddlListval = document.getElementById('<%=hfReportType.ClientID%>').value;
            $("#<%=ddlAdminReort.ClientID%>").empty();
            GetAllEmployees(ddlListval);
        });
    </script>
    <script type="text/javascript">
        function GetSelectedTextValue(ddlFruits) {
            var selectedText = ddlFruits.options[ddlFruits.selectedIndex].innerHTML;
            var selectedValue = ddlFruits.value;
            document.getElementById('<%=hfReportType.ClientID%>').value = selectedValue;
            $("#<%=ddlAdminReort.ClientID%>").empty();
            GetAllEmployees(selectedValue);
            var iframe = document.getElementById('<%=iframe1.ClientID%>');
            iframe.src = "";
        }
        function GetSelectedPanel(ddlAdmin) {
            var selectedText = ddlAdmin.options[ddlAdmin.selectedIndex].innerHTML;
            var selectedValue = ddlAdmin.value;
            document.getElementById('<%=hfAdminReport.ClientID%>').value = selectedValue;
            EnableDisablePanale(selectedValue);
            var iframe = document.getElementById('<%=iframe1.ClientID%>');
            iframe.src = "";
        }
        function GetAllEmployees(selectedValue) {
            EnableDisablePanale(selectedValue);
            var DropDownList2 = $("#<%=ddlAdminReort.ClientID%>");
            $.ajax({
                type: "POST",
                url: '<%= ResolveUrl("~/Admin/Reports/AllReportTypeService.asmx/GetAllEmployees") %>',
                data: "{'employeeId':'" + selectedValue + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var elementselect = document.getElementById('<%=hfAdminReport.ClientID%>').value;
                    var Employees = response.d;
                    $.each(Employees, function (index, employee) {
                        if (employee.Id == elementselect) {
                            DropDownList2.append('<option Selected="True" value="' + employee.Id + '">' + employee.Name + '</option>');
                            EnableDisablePanale(elementselect);
                        }
                        else {
                            DropDownList2.append('<option value="' + employee.Id + '">' + employee.Name + '</option>');
                        }
                    });
                },
                failure: function (msg) {
                    alert(msg);
                }
            });
            }
            function EnableDisablePanale(selectedValue) {
                DisableAllPanel();
                var ReportTypeValue = selectedValue.split(' ').join('');
                if (ReportTypeValue != "0" && ReportTypeValue != "" && ReportTypeValue != null) {
                    document.getElementById('MainContent_pnl' + ReportTypeValue).style.display = 'block';
                }
            }
            function DisableAllPanel() {
                var searchEles = document.getElementById('<%=pnlAdminReport.ClientID%>').querySelectorAll('[id^="MainContent_pnl"]');
                for (i = 0; i < searchEles.length; i++) {
                    searchEles[i].style.display = 'none';
                }
                var searchEles1 = document.getElementById('<%=pnlFinanceReport.ClientID%>').querySelectorAll('[id^="MainContent_pnl"]');
                for (i = 0; i < searchEles1.length; i++) {
                    searchEles1[i].style.display = 'none';
                }
            }
    </script>
</asp:Content>
