<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="Funeral.Web.Admin.Reports.Reports" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <%--<rsweb:ReportViewer ID="ssrsReportViewer1" Width="100%" Height="800" runat="server"></rsweb:ReportViewer>--%>
    <div class="row">
        <div class="col-sm-12">
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
                                                <asp:ValidationSummary runat="server" ID="vSummaryCompRegi" ValidationGroup="CompanyRegi" ForeColor="Red" />
                                                <div class="ibox-content">
                                                    <div class="col-sm-6" runat="server" id="dvAdministrator">
                                                        <div class="form-group">
                                                            <label class="m-l">Company </label>
                                                            <asp:DropDownList ID="ddlCompanyList" Width="97%" CssClass="form-control m-l" AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyList_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                                        </div>
                                                    </div>
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
                                                    </div>
                                                </div>
                                                <div class="ibox-content">
                                                    <div class="panel panel-primary">
                                                        <div class="panel-heading">
                                                            <span class="nav-label"><i class="fa-x fa fa-newspaper-o "></i>Filter Report</span>
                                                        </div>
                                                        <div class="panel-body">
                                                            <div role="form" class="form col-sm-12">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label class="">Date From </label>
                                                                        <%-- <asp:RequiredFieldValidator ValidationGroup="pnlPolicyStatus" ID="RequiredFieldValidator29" ForeColor="Red" ControlToValidate="txtPolicyDateFrom" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                                                        <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtDateFrom" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label class="">Date To </label>
                                                                        <%--<asp:RequiredFieldValidator ValidationGroup="pnlPolicyStatus" ID="RequiredFieldValidator30" ForeColor="Red" ControlToValidate="txtPolicyDateTo" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                                                        <asp:TextBox CssClass="form-control datepicker" placeholder="DD/MM/YYYY" ID="txtDateTo" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label class="">Branch Name </label>
                                                                        <asp:DropDownList ID="ddlBranch" runat="server" DataTextField="BranchName" DataValueField="Brancheid" CssClass="form-control"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label class="">Society </label>
                                                                        <asp:DropDownList ID="ddlSociety" runat="server" DataTextField="SocietyName" DataValueField="pkiSocietyID" CssClass="form-control"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label class="">Agent </label>
                                                                        <asp:DropDownList CssClass="form-control" ID="ddlAgent" runat="server"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label class="">Member Underwriting:</label>
                                                                        <asp:DropDownList CssClass="form-control" ID="ddlUnderwriter" runat="server"></asp:DropDownList>
                                                                        <%-- <asp:RequiredFieldValidator ValidationGroup="pnlMembersUnderWriting" ID="RequiredFieldValidator6" ForeColor="Red" ControlToValidate="ddlUnderwriter" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label class="">Payment Type</label>
                                                                        <asp:DropDownList runat="server" ID="ddlMethod" class="form-control">
                                                                            <asp:ListItem Selected="True"></asp:ListItem>
                                                                            <asp:ListItem Value="1">All</asp:ListItem>
                                                                            <asp:ListItem Value="2">Cash</asp:ListItem>
                                                                            <asp:ListItem Value="3">Easy Pay</asp:ListItem>
                                                                            <asp:ListItem Value="4">Debit Order</asp:ListItem>
                                                                            <asp:ListItem Value="5">Bank Depost</asp:ListItem>
                                                                            <asp:ListItem Value="6">EFT</asp:ListItem>
                                                                            <asp:ListItem Value="7">Other</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label class="">Policy Status:</label>
                                                                        <asp:DropDownList ID="ddlPolicyStatus" runat="server" class="form-control">
                                                                            <asp:ListItem Selected="True"></asp:ListItem>
                                                                            <asp:ListItem>All</asp:ListItem>
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
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label class="">Custom :</label>
                                                                        <asp:DropDownList ID="ddlCustom1" runat="server" class="form-control">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label class="">Custom 2:</label>
                                                                        <asp:DropDownList ID="ddlCustom2" runat="server" class="form-control">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label class="">Custom 3:</label>
                                                                        <asp:DropDownList ID="ddlCustom3" runat="server" class="form-control">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <label class="">Report Export Type:</label>
                                                        <asp:DropDownList ID="rptExportType" runat="server" class="form-control">
                                                            <asp:ListItem>PDF</asp:ListItem>
                                                            <asp:ListItem>Excel</asp:ListItem>
                                                            <asp:ListItem>Word</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <label class="">SendEmail</label>
                                                        <asp:TextBox  runat="server" ID="txtcemail" name="name" type="text" class="form-control"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="CompanyRegi" ErrorMessage="Invalid email." ControlToValidate="txtcemail" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-push-6 col-sm-6">
                                                    <div class="form-group">
                                                        <div class="form-group">
                                                            <asp:Button runat="server" ID="btnExport" ValidationGroup="CompanyRegi" Text="Generate" OnClick="ExportClickEvent" CssClass="btn btn-outline btn-primary pull-right" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:HiddenField ID="hfReportType" runat="server" />
                                    <asp:HiddenField ID="hfAdminReport" runat="server" />
                                    <div role="form" class="form col-sm-12">
                                        <asp:Label runat="server" ID="lblMessage"></asp:Label>
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
    <script type="text/javascript">
        function pageLoad(sender, args) {
            $(".datepicker").datepicker({ format: 'dd MM yyyy' });
            $(".datepicker").on('changeDate', function (ev) {
                $(this).datepicker('hide');
            })
        }
    </script>
    <script type="text/javascript">
        function GetSelectedTextValue(ddlFruits) {
            var selectedText = ddlFruits.options[ddlFruits.selectedIndex].innerHTML;
            var selectedValue = ddlFruits.value;
            document.getElementById('<%=hfReportType.ClientID%>').value = selectedValue;
            $("#<%=ddlAdminReort.ClientID%>").empty();
            GetAllEmployees(selectedValue);

        }
        function GetAllEmployees(selectedValue) {
            //EnableDisablePanale(selectedValue);
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
        function GetSelectedPanel(ddlAdmin) {
                //var selectedText = ddlAdmin.options[ddlAdmin.selectedIndex].innerHTML;
                var selectedValue = ddlAdmin.value;
                document.getElementById('<%=hfAdminReport.ClientID%>').value = selectedValue;
            }
    </script>
</asp:Content>
