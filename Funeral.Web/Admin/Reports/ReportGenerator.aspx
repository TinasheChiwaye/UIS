<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ReportGenerator.aspx.cs" Inherits="Funeral.Web.Admin.Reports.ReportGenerator" %>

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
    <script>
        /* document.addEventListener("click", function (e) {
             if (e.target.id == "MainContent_chklPolicyStatus_0") {
                 ToggleClick();
             }
         });
    
             $('#MainContent_chklPolicyStatus_0').click(function () {
                 
             });
       
         
             function ToggleClick()
             {
                 if (document.getElementById('MainContent_chklPolicyStatus_0').checked) {
                     ResetCheckAll();
                 }
                 else {
                     CheckAll();
                 }
             }
 
         function CheckAll()
         {
             $('#MainContent_chklPolicyStatus input:checkbox').each(function () {
                 $(this).prop('checked', true);
             })
         }
         function ResetCheckAll() {
             $('#MainContent_chklPolicyStatus input:checkbox').each(function () {
                 $(this).prop('checked', false);
             })
         }
         */
    </script>
    <div class="row">
        <asp:Panel ID="pnlReportSetter" runat="server">
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
                                                <li class="active"><a data-toggle="tab" href="#tab-1">All Members</a></li>
                                                <li class=""><a data-toggle="tab" href="#tab-2">Members Joined By Date</a></li>
                                                <li class=""><a data-toggle="tab" href="#tab-3">Policy Status</a></li>
                                                <li class=""><a data-toggle="tab" href="#tab-4">Members per Branch</a></li>
                                                <li class=""><a data-toggle="tab" href="#tab-5">Member Plan</a></li>
                                                <li class=""><a data-toggle="tab" href="#tab-6">Member Underwriting</a></li>
                                                <li class=""><a data-toggle="tab" href="#tab-7">Agent Report</a></li>
                                                <li class=""><a data-toggle="tab" href="#tab-8">Funeral arrangement report</a></li>
                                                <li class=""><a data-toggle="tab" href="#tab-9">Outstanding payment</a></li>
                                            </ul>
                                        </div>
                                    </div>
                                    <div class="panel-body">
                                        <div class="tab-content">
                                            <div id="tab-1" class="tab-pane active">
                                                <button runat="server" validationgroup="pnlAllMembers" id="btnPnlAllJoinedMemebrs" onserverclick="btnSubmit_Click" class="btn btn-outline btn-primary pull-right" title="Generate">
                                                    <i class="fa fa-check"></i>Generate
                                                </button>
                                            </div>
                                            <div id="tab-2" class="tab-pane">
                                                <asp:Panel runat="server" ID="pnlMembersByJoinedDate">
                                                    <div class="row">
                                                        <div class="col-sm-3 form-inline">
                                                            <label class="" for="email">Branch Name : </label>
                                                            <asp:DropDownList ID="ddlMemberBranch" runat="server" DataTextField="BranchName" DataValueField="Brancheid" CssClass="form-control m-b"></asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-3 form-inline">
                                                            <label class="" for="email">Member Plan: </label>
                                                            <asp:DropDownList ID="ddlMemberPlan" runat="server" DataTextField="PlanName" DataValueField="pkiPlanID" CssClass="form-control m-b" Style="max-width: 60%"></asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-6 form-inline">
                                                            <div class="form-group">
                                                                <label class="" for="email">Date From:</label>
                                                                <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtDateFrom" runat="server"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ValidationGroup="pnlMembersByJoinedDate" ID="RequiredFieldValidator3" ForeColor="Red" ControlToValidate="txtDateFrom" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="" for="email">Date From:</label>
                                                                <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtDateTo" runat="server"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ValidationGroup="pnlMembersByJoinedDate" ID="RequiredFieldValidator1" ForeColor="Red" ControlToValidate="txtDateTo" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <button runat="server" validationgroup="pnlMembersByJoinedDate" id="btnPnlMembersByJoinedDate" onserverclick="btnSubmit_Click" class="btn btn-outline btn-primary pull-right" title="Generate">
                                                                <i class="fa fa-check"></i>Generate
                                                            </button>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                            <div id="tab-3" class="tab-pane ">
                                                <asp:Panel runat="server" ID="pnlPolicyStatus">
                                                    <div class="row">
                                                        <div class="col-sm-5 form-inline ">
                                                            <label class="" for="email">Policy Status:</label>
                                                            <%-- <asp:TextBox CssClass="form-control" ID="txtPolicyStatus" runat="server"></asp:TextBox--%>

                                                            <asp:DropDownList ID="ddlPolicyStatus" runat="server" CssClass="form-control" Style="min-width: 60%">
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
                                                            <%--<asp:RequiredFieldValidator ValidationGroup="pnlPolicyStatus" InitialValue="All" ID="RequiredFieldValidator2" ForeColor="Red" ControlToValidate="ddlPolicyStatus" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                        <div class="col-sm-7 form-inline ">

                                                            <div class="form-group">
                                                                <label class="" for="email">Date From:</label>
                                                                <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtPolicyDateFrom" runat="server"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ValidationGroup="pnlPolicyStatus" ID="RequiredFieldValidator7" ForeColor="Red" ControlToValidate="txtPolicyDateFrom" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="" for="email">Date From:</label>
                                                                <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtPolicyDateTo" runat="server"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ValidationGroup="pnlPolicyStatus" ID="RequiredFieldValidator8" ForeColor="Red" ControlToValidate="txtPolicyDateTo" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <button runat="server" validationgroup="pnlPolicyStatus" id="btnPnlPolicyStatus" onserverclick="btnSubmit_Click" class="btn btn-outline btn-primary pull-right" title="Generate">
                                                                <i class="fa fa-check"></i>Generate
                                                            </button>
                                                            <%--   <i class='fa fa-check'></i><asp:Button ID="btnSubmit" CssClass="btn btn-outline btn-primary" runat="server" Text=" Generate" OnClick="btnSubmit_Click" />--%>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                            <div id="tab-4" class="tab-pane ">
                                                <asp:Panel Visible="True" runat="server" ID="pnlMembersPerBranch">
                                                    <div class="row">
                                                        <div class="col-sm-6 form-inline">
                                                            <label class="" for="email">Branch Name : </label>
                                                            <asp:DropDownList ID="ddlBranch" runat="server" DataTextField="BranchName" DataValueField="Brancheid" CssClass="form-control" Style="max-width: 60%"></asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-6 form-inline">
                                                            <button runat="server" validationgroup="pnlMembersPerBranch" id="btnPnlMembersPerBranch" onserverclick="btnSubmit_Click" class="btn btn-outline btn-primary pull-right" title="Generate">
                                                                <i class="fa fa-check"></i>Generate
                                                            </button>
                                                            <%--   <i class='fa fa-check'></i><asp:Button ID="btnSubmit" CssClass="btn btn-outline btn-primary" runat="server" Text=" Generate" OnClick="btnSubmit_Click" />--%>
                                                        </div>
                                                    </div>
                                                </asp:Panel>

                                            </div>
                                            <div id="tab-5" class="tab-pane ">
                                                <asp:Panel Visible="True" runat="server" ID="pnlMembersPerPlan">
                                                    <div class="row">
                                                        <div class="col-sm-6 form-inline">
                                                            <label class="" for="email">Member Plan: </label>
                                                            <asp:DropDownList ID="ddlPlanname" runat="server" DataTextField="PlanName" DataValueField="pkiPlanID" CssClass="form-control" Style="max-width: 60%"></asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-6 form-inline">
                                                            <button runat="server" validationgroup="pnlMembersPerPlan" id="btnPnlMembersPerPlan" onserverclick="btnSubmit_Click" class="btn btn-outline btn-primary pull-right" title="Generate">
                                                                <i class="fa fa-check"></i>Generate
                                                            </button>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                            <div id="tab-6" class="tab-pane ">
                                                <asp:Panel Visible="True" runat="server" ID="pnlMembersUnderWriting">
                                                    <div class="row">
                                                        <div class="col-sm-6 form-inline">
                                                            <label class="" for="email">Member Underwriting:</label>
                                                            <asp:DropDownList CssClass="form-control" ID="ddlUnderwriter" runat="server" Style="max-width: 60%"></asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-6 form-inline">
                                                            <button runat="server" validationgroup="pnlMembersUnderWriting" id="btnPnlMembersUnderWriting" onserverclick="btnSubmit_Click" class="btn btn-outline btn-primary pull-right" title="Generate">
                                                                <i class="fa fa-check"></i>Generate
                                                            </button>
                                                            <%--   <i class='fa fa-check'></i><asp:Button ID="btnSubmit" CssClass="btn btn-outline btn-primary" runat="server" Text=" Generate" OnClick="btnSubmit_Click" />--%>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                            <div id="tab-7" class="tab-pane ">
                                                <asp:Panel Visible="True" runat="server" ID="Panel2">
                                                    <div class="row">
                                                        <div class="col-sm-12 form-inline">
                                                            <div class="form-group">
                                                                <label class="" for="email">Agent :</label>
                                                                <asp:TextBox CssClass="form-control" placeholder="Enter Agent Name" ID="txtAgent" runat="server"></asp:TextBox>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="" for="email">Date From:</label>
                                                                <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtAgentDateFrom" runat="server"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" ValidationGroup="AgentReport" ForeColor="Red" ControlToValidate="txtAgentDateFrom" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="" for="email">Date To:</label>
                                                                <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtAgentDateTo" runat="server"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator20" ValidationGroup="AgentReport" ForeColor="Red" ControlToValidate="txtAgentDateTo" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <button runat="server" validationgroup="AgentReport" id="btnAgentReport" onserverclick="btnSubmit_Click" class="btn btn-outline btn-primary pull-right" title="Generate">
                                                                <i class="fa fa-check"></i>Generate
                                                            </button>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                            <div id="tab-8" class="tab-pane ">
                                                <asp:Panel Visible="True" runat="server" ID="Panel3">
                                                    <div class="row">
                                                        <div class="col-sm-12 form-inline">
                                                            <div class="form-group">
                                                                <label class="" for="email">Date From:</label>
                                                                <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtFuneralArrangmentDateFrom" runat="server"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="FuneralArrangment" ForeColor="Red" ControlToValidate="txtFuneralArrangmentDateFrom" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="" for="email">Date To:</label>
                                                                <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtFuneralArrangmentDateTo" runat="server"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="FuneralArrangment" ForeColor="Red" ControlToValidate="txtFuneralArrangmentDateTo" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <button runat="server" validationgroup="FuneralArrangment" id="btnFuneralArrangement" onserverclick="btnSubmit_Click" class="btn btn-outline btn-primary pull-right" title="Generate">
                                                                <i class="fa fa-check"></i>Generate
                                                            </button>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                            <div id="tab-9" class="tab-pane ">
                                                <asp:Panel Visible="True" runat="server" ID="Panel1">
                                                    <div class="row">
                                                        <div class="col-sm-12 form-inline">
                                                            <div class="form-group">
                                                                <label class="" for="email">Date From:</label>
                                                                <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtOutstandingPaymentDateFrom" runat="server"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="outstandingPayment" ForeColor="Red" ControlToValidate="txtOutstandingPaymentDateFrom" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="" for="email">Date To:</label>
                                                                <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtOutstandingPaymentDateTo" runat="server"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ValidationGroup="outstandingPayment" ForeColor="Red" ControlToValidate="txtOutstandingPaymentDateTo" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <button runat="server" validationgroup="outstandingPayment" id="btnoutstandingPayment" onserverclick="btnSubmit_Click" class="btn btn-outline btn-primary pull-right" title="Generate">
                                                                <i class="fa fa-check"></i>Generate
                                                            </button>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%--<div class="row">
                                <div class="col-sm-12">
                                    <asp:CheckBoxList ID="chklPolicyStatus" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True">All</asp:ListItem>
                                        <asp:ListItem>Active</asp:ListItem>
                                        <asp:ListItem>Cancelled</asp:ListItem>
                                        <asp:ListItem>Deceased</asp:ListItem>
                                        <asp:ListItem>Delete</asp:ListItem>
                                        <asp:ListItem>Historic</asp:ListItem>
                                        <asp:ListItem>Lapsed</asp:ListItem>
                                        <asp:ListItem>On Trial</asp:ListItem>
                                        <asp:ListItem>Single</asp:ListItem>
                                    </asp:CheckBoxList>
                                </div>
                            </div>--%>
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
                                            <asp:RequiredFieldValidator ControlToValidate="txtReportEmail" ForeColor="Red" ValidationGroup="sendmail" ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ControlToValidate="txtReportEmail" ID="RegularExpressionValidator1" runat="server" ErrorMessage="*Invalid Email Address." ValidationGroup="sendmail" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                        </div>
                                        <asp:Button ValidationGroup="sendmail" ID="btnSendMail" runat="server" Text="Send Mail" OnClick="btnSendMail_Click" class="btn btn-outline btn-primary" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
    <div class="row">
        <div class="col-sm-12">
            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
            <rsweb:ReportViewer ID="rvMembersByDateRange" runat="server" ShowPrintButton="true" CssClass="ReportView" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" Width="99%" WaitMessageFont-Size="14pt" Height="613px" ViewStateMode="Enabled">
                <LocalReport ReportPath="Admin\Reports\ReportLayouts\JoinedMembersByDate.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="ObjectDataSourceJoinedMembersbyDate" Name="dsJoinedMembersByDate" />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>
            <asp:ObjectDataSource ID="ObjectDataSourceJoinedMembersbyDate" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="Funeral.Web.Reports.MembersJoinedByDateTableAdapters.allmembersTableAdapter">
                <SelectParameters>
                    <asp:Parameter DbType="Guid" Name="parlourid" />
                    <asp:Parameter Name="branch" Type="String" />
                    <asp:Parameter Name="StartDate" Type="DateTime" />
                    <asp:Parameter Name="EndDate" Type="DateTime" />
                </SelectParameters>
            </asp:ObjectDataSource>
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
