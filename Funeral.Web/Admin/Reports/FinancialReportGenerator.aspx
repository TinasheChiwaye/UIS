<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="FinancialReportGenerator.aspx.cs" Inherits="Funeral.Web.Admin.Reports.FinancialReportGenerator" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
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
    </style>

    <div class="row">

        <div class="col-sm-12 sideBar padding-top-20">
            <div class="row ">


                <div class="panel panel-primary">
                    <div class="panel-heading">

                        <span class="nav-label"><i class="fa-x fa fa-newspaper-o "></i>Report Setting</span>
                    </div>
                    <div class="panel-body">

                        <div class="row">

                            <div class="panel blank-panel">

                                <div class="panel-heading">
                                    <div class="panel-title m-b-md">
                                        <h4>Generate Report by Selecting Reference</h4>
                                    </div>
                                    <div class="panel-options">
                                        <ul class="nav nav-tabs">
                                            <li class="active"><a data-toggle="tab" href="#tab-1">Agent Commision</a></li>
                                            <li><a data-toggle="tab" href="#tab-2">Cash of Bank Payments</a></li>
                                            <li><a data-toggle="tab" href="#tab-3">Claims Reports</a></li>
                                            <li><a data-toggle="tab" href="#tab-4">Detailed Payment</a></li>

                                            <li><a data-toggle="tab" href="#tab-5">Expenses</a></li>
                                            <li><a data-toggle="tab" href="#tab-6">Funerl Payment</a></li>
                                            <li><a data-toggle="tab" href="#tab-7">Payment by Branch</a></li>
                                            <li><a data-toggle="tab" href="#tab-8">Payment by Date</a></li>

                                            <li><a data-toggle="tab" href="#tab-9">Payment by Society</a></li>
                                            <li><a data-toggle="tab" href="#tab-10">Quotation by Date</a></li>
                                            <li><a data-toggle="tab" href="#tab-11">Underwriter Payment</a></li>
                                        </ul>
                                    </div>
                                </div>

                                <div class="panel-body">
                                    <div class="tab-content">
                                        <div id="tab-1" class="tab-pane active">
                                            <div class="row">
                                                <div class="col-sm-12 form-inline">
                                                    <div class="form-group">
                                                        <label class="" for="email">Agent :</label>
                                                        <asp:TextBox CssClass="form-control" placeholder="Enter Agent Name" ID="txtAgent" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="" for="email">Date From:</label>
                                                        <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtAgentDateFrom" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" ValidationGroup="AgentComm" ForeColor="Red" ControlToValidate="txtAgentDateFrom" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="" for="email">Date To:</label>
                                                        <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtAgentDateTo" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" ValidationGroup="AgentComm" ForeColor="Red" ControlToValidate="txtAgentDateTo" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    </div>

                                                    <button runat="server" id="btnAgent" validationgroup="AgentComm" onserverclick="btnSubmit_Click" class="btn btn-outline btn-primary pull-right" title="Generate">
                                                        <i class="fa fa-check"></i>Generate
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="tab-2" class="tab-pane">
                                            <div class="row">
                                                <div class="col-sm-12 form-inline">
                                                    <div class="form-group">
                                                        <label class="" for="email">Under Writer:</label>
                                                        <asp:TextBox CssClass="form-control" placeholder="Enter user Name" ID="txtCashBankUser" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="" for="email">Date From:</label>
                                                        <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtBankPaymentDateFrom" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" ValidationGroup="CashBankPayment" ForeColor="Red" ControlToValidate="txtBankPaymentDateFrom" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="" for="email">Date To:</label>
                                                        <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtBankPaymentDateTo" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" ValidationGroup="CashBankPayment" ForeColor="Red" ControlToValidate="txtBankPaymentDateTo" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <button runat="server" id="btnCashofBank" validationgroup="CashBankPayment" onserverclick="btnSubmit_Click" class="btn btn-outline btn-primary pull-right" title="Generate">
                                                        <i class="fa fa-check"></i>Generate
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="tab-3" class="tab-pane">
                                            <div class="row">
                                                <div class="form-group">
                                                <div class="col-sm-5 form-inline ">
                                                            <label class="" for="email">Claim Status:</label>                                                            
                                                            <asp:DropDownList ID="ddlPolicyStatus" runat="server" CssClass="form-control" Style="min-width: 60%">
                                                                <asp:ListItem Selected="True">All</asp:ListItem>
                                                                <asp:ListItem>Payment Done</asp:ListItem>
                                                                <asp:ListItem>Funeral Done</asp:ListItem>
                                                                
                                                            </asp:DropDownList>
                                                    </div>
                                                        </div>
                                                <div class="col-sm-7 form-inline">
                                                    <div class="form-group">
                                                        <label class="" for="email">Date From:</label>
                                                        <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtClaimDateFrom" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="Claims" ForeColor="Red" ControlToValidate="txtClaimDateFrom" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="" for="email">Date To:</label>
                                                        <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtClaimDateTo" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="Claims" ForeColor="Red" ControlToValidate="txtClaimDateTo" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    </div>                                                    
                                                    <button runat="server" id="btnClaims" validationgroup="Claims" onserverclick="btnSubmit_Click" class="btn btn-outline btn-primary pull-right" title="Generate">
                                                        <i class="fa fa-check"></i>Generate
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="tab-4" class="tab-pane">
                                            <div class="row">
                                                <div class="col-sm-12 form-inline">
                                                    <div class="form-group">
                                                        <label class="" for="email">Date From:</label>
                                                        <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtDatailedPaymentDatefrom" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ValidationGroup="DetailedPayement" ID="RequiredFieldValidator5" ForeColor="Red" ControlToValidate="txtDatailedPaymentDatefrom" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="" for="email">Date to:</label>
                                                        <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtDatailedPaymentDateTo" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ValidationGroup="DetailedPayement" ID="RequiredFieldValidator6" ForeColor="Red" ControlToValidate="txtDatailedPaymentDateTo" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <button runat="server" id="btnDetailedPayment" validationgroup="DetailedPayement" onserverclick="btnSubmit_Click" class="btn btn-outline btn-primary pull-right" title="Generate">
                                                        <i class="fa fa-check"></i>Generate
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="tab-5" class="tab-pane">
                                            <div class="row">
                                                <div class="col-sm-12 form-inline">
                                                    <label class="" for="email">Under Writer:</label>
                                                    <asp:DropDownList ID="ddlExpenseBranch" runat="server" CssClass="form-control" Style="max-width: 40%"></asp:DropDownList>
                                                    <div class="form-group">
                                                        <label class="" for="email">Date From:</label>
                                                        <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtExpensDateFrom" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ValidationGroup="expense" ID="RequiredFieldValidator17" ForeColor="Red" ControlToValidate="txtExpensDateFrom" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="" for="email">Date to:</label>
                                                        <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtExpensDateTo" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ValidationGroup="expense" ID="RequiredFieldValidator18" ForeColor="Red" ControlToValidate="txtExpensDateTo" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <button runat="server" validationgroup="expense" id="btnExpenses" onserverclick="btnSubmit_Click" class="btn btn-outline btn-primary pull-right" title="Generate">
                                                        <i class="fa fa-check"></i>Generate
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="tab-6" class="tab-pane">
                                            <div class="row">
                                                <div class="col-sm-12 form-inline">
                                                    <div class="form-group">
                                                        <label class="" for="email">Date From:</label>
                                                        <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtDateFrom" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ValidationGroup="FuneralPayment" ID="RequiredFieldValidator3" ForeColor="Red" ControlToValidate="txtDateFrom" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="" for="email">Date to:</label>
                                                        <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtDateTo" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ValidationGroup="FuneralPayment" ID="RequiredFieldValidator1" ForeColor="Red" ControlToValidate="txtDateTo" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <button runat="server" id="btnFuneralPayment" validationgroup="FuneralPayment" onserverclick="btnSubmit_Click" class="btn btn-outline btn-primary pull-right" title="Generate">
                                                        <i class="fa fa-check"></i>Generate
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="tab-7" class="tab-pane">
                                            <div class="row">
                                                <div class="col-sm-12 form-inline">
                                                    <label class="" for="email">Branch :</label>
                                                    <asp:DropDownList ID="ddlBranch" runat="server" DataTextField="BranchName" DataValueField="Brancheid" CssClass="form-control" Style="max-width: 40%"></asp:DropDownList>
                                                    <div class="form-group">
                                                        <label class="" for="email">Date From:</label>
                                                        <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtBranchDateFrom" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="PaymentBranch" ForeColor="Red" ControlToValidate="txtBranchDateFrom" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="" for="email">Date To:</label>
                                                        <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtBranchDateTo" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ValidationGroup="PaymentBranch" ForeColor="Red" ControlToValidate="txtBranchDateTo" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <button runat="server" id="btnBranch" validationgroup="PaymentBranch" onserverclick="btnSubmit_Click" class="btn btn-outline btn-primary pull-right" title="Generate">
                                                        <i class="fa fa-check"></i>Generate
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="tab-8" class="tab-pane">
                                            <div class="row">
                                                <div class="col-sm-12 form-inline">
                                                    <div class="form-group">
                                                        <label class="" for="email">Date From:</label>
                                                        <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtPaymentbyDateFrom" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ValidationGroup="PaymentbyDate" ForeColor="Red" ControlToValidate="txtPaymentbyDateFrom" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="" for="email">Date To:</label>
                                                        <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtPaymentbyDateTo" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ValidationGroup="PaymentbyDate" ForeColor="Red" ControlToValidate="txtPaymentbyDateTo" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <button runat="server" id="btnPaymentbyDate" validationgroup="PaymentbyDate" onserverclick="btnSubmit_Click" class="btn btn-outline btn-primary pull-right" title="Generate">
                                                        <i class="fa fa-check"></i>Generate
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="tab-9" class="tab-pane">
                                            <div class="row">
                                                <div class="col-sm-12 form-inline">
                                                    <label class="" for="email">Society :</label>
                                                    <asp:DropDownList ID="ddlSociety" runat="server" DataTextField="SocietyName" DataValueField="pkiSocietyID" CssClass="form-control" Style="max-width: 40%"></asp:DropDownList>
                                                    <div class="form-group">
                                                        <label class="" for="email">Date From:</label>
                                                        <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtsocietyDateFrom" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ValidationGroup="PaymentbySociety" ForeColor="Red" ControlToValidate="txtsocietyDateFrom" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="" for="email">Date To:</label>
                                                        <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtsocietyDateTo" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ValidationGroup="PaymentbySociety" ForeColor="Red" ControlToValidate="txtsocietyDateTo" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <button runat="server" id="btnSocietty" validationgroup="PaymentbySociety" onserverclick="btnSubmit_Click" class="btn btn-outline btn-primary pull-right" title="Generate">
                                                        <i class="fa fa-check"></i>Generate
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="tab-10" class="tab-pane">
                                            <div class="row">
                                                <div class="col-sm-12 form-inline">
                                                    <div class="form-group">
                                                        <label class="" for="email">Date From:</label>
                                                        <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtQuotationDateFrom" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ValidationGroup="QuotationReport" ID="RequiredFieldValidator2" ForeColor="Red" ControlToValidate="txtQuotationDateFrom" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="" for="email">Date to:</label>
                                                        <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtQuotationDateTo" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ValidationGroup="QuotationReport" ID="RequiredFieldValidator4" ForeColor="Red" ControlToValidate="txtQuotationDateTo" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <button runat="server" id="btnQuoteByDate" validationgroup="QuotationReport" onserverclick="btnSubmit_Click" class="btn btn-outline btn-primary pull-right" title="Generate">
                                                        <i class="fa fa-check"></i>Generate
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="tab-11" class="tab-pane">
                                            <div class="row">
                                                <div class="col-sm-12 form-inline">
                                                    <div class="form-group">
                                                        <label class="" for="email">Under Writer:</label>
                                                        <asp:TextBox CssClass="form-control" placeholder="Enter Under Writer    " ID="txtUnderwiter" runat="server"></asp:TextBox>
                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" validationgroup="Underwiter" ForeColor="Red" ControlToValidate="txtUnderwiter" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="" for="email">Date From:</label>
                                                        <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtUnderwriterDateFrom" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ValidationGroup="Underwiter" ForeColor="Red" ControlToValidate="txtUnderwriterDateFrom" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="" for="email">Date To:</label>
                                                        <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtUnderwriterDateTo" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" ValidationGroup="Underwiter" ForeColor="Red" ControlToValidate="txtUnderwriterDateTo" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <button runat="server" id="btnUnderwriter" validationgroup="Underwiter" onserverclick="btnSubmit_Click" class="btn btn-outline btn-primary pull-right" title="Generate">
                                                        <i class="fa fa-check"></i>Generate
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:CheckBoxList ID="chklPolicyStatus" runat="server" RepeatDirection="Horizontal"></asp:CheckBoxList>
                            </div>
                        </div>

                    </div>
                </div>



            </div>

        </div>

    </div>
    <div class="row">
        <div class="col-sm-12">
            <div class="form-inline">
                <div class="form-group">
                    <label class="" for="email">Select Report Format</label>
                    <asp:DropDownList ID="ddlReportFormat" CssClass="form-control" runat="server">
                        <asp:ListItem>Word</asp:ListItem>
                        <asp:ListItem>Pdf</asp:ListItem>
                        <asp:ListItem>Excel</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="form-group ">
                    <label class="" for="email">Enter Email Address: </label>
                    <asp:TextBox class="form-control" ValidationGroup="sendmail" ID="txtReportEmail" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="txtReportEmail" ForeColor="Red" ValidationGroup="sendmail" ID="RequiredFieldValidator23" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ControlToValidate="txtReportEmail" ID="RegularExpressionValidator1" runat="server" ErrorMessage="*Invalid Email Address." ValidationGroup="sendmail" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </div>
                <asp:Button ValidationGroup="sendmail" ID="btnSendMail" runat="server" Text="Send Mail" OnClick="btnSendMail_Click" class="btn btn-outline btn-primary" />
            </div>
        </div>


    </div>
    <div class="row">
        <div class="col-sm-12">

            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
            <rsweb:ReportViewer ID="rvMembersByDateRange" runat="server" CssClass="ReportView" ShowPrintButton="true" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" Width="99%" WaitMessageFont-Size="14pt" Height="613px">
                <LocalReport ReportPath="Admin\Reports\ReportLayouts\rdlcExpenses.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="objdsFuneralPayment" Name="dsExpenses" />

                    </DataSources>
                </LocalReport>

            </rsweb:ReportViewer>
            <asp:ObjectDataSource ID="ObjectDataSourceExpense" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="Funeral.Web.Reports.MembersJoinedByDateTableAdapters.allmembersTableAdapter">
                <SelectParameters>
                    <asp:Parameter DbType="Guid" Name="parlourid" />
                    <asp:Parameter Name="branch" Type="String" />
                    <asp:Parameter Name="StartDate" Type="DateTime" />
                    <asp:Parameter Name="EndDate" Type="DateTime" />
                </SelectParameters>
            </asp:ObjectDataSource>

            <asp:ObjectDataSource ID="objdsFuneralPayment" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="Funeral.Web.Admin.Reports.DataSets.dsFuneralPaymentTableAdapters.RPTReturnFuneralPaymentsTableAdapter">
                <SelectParameters>
                    <asp:Parameter Name="DateFrom" Type="DateTime" />
                    <asp:Parameter Name="DateTo" Type="DateTime" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <br />
            <asp:ObjectDataSource ID="objdsQuoteByDate" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="Funeral.Web.Admin.Reports.DataSets.dsQuotationByDateTableAdapters.RPTQuotationByDateTableAdapter">
                <SelectParameters>
                    <asp:Parameter Name="DateFrom" Type="DateTime" />
                    <asp:Parameter Name="DateTo" Type="DateTime" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <br />
            <asp:ObjectDataSource ID="objdsDetailedPayment" runat="server"></asp:ObjectDataSource>
            <br />
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptControl" runat="server">
    <script>
        $(document).ready(function () {
            $(".datepicker").datepicker({ format: 'dd MM yyyy' });
            $(".datepicker").on('changeDate', function (ev) {
                $(this).datepicker('hide');
            })
        })
    </script>
</asp:Content>
