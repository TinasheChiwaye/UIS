<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reportrdlc.aspx.cs" Inherits="Funeral.Web.Admin.Reports.reportrdlc" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../fonts/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../../Content/animate.css" rel="stylesheet" type="text/css" />
    <link href="../../Content/style.css" rel="stylesheet" type="text/css" />
    <link href="../../Content/plugins/datapicker/datepicker3.css" rel="stylesheet" />
    <link href="../../Content/plugins/blueimp/css/blueimp-gallery.min.css" rel="stylesheet" />
    <script src="../../Scripts/jquery-2.1.1.min.js" type="text/javascript"></script>
    <script src="../../Scripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../../Scripts/plugins/metisMenu/jquery.metisMenu.js" type="text/javascript"></script>
    <script src="../../Scripts/plugins/slimScroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <script src="../../Scripts/app/inspinia.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Scripts/Plugins/dataTables/jquery.dataTables.js"></script>
    <script src="../../Scripts/plugins/datapicker/bootstrap-datepicker.js"></script>
    <script src="../../Scripts/plugins/blueimp/jquery.blueimp-gallery.min.js"></script>
    <script type="text/javascript">
        function redirectParent(strMemberId, ParlourId) {
            window.top.location.href = '../../Admin/ManageMembersPayment.aspx?Id=' + strMemberId + '&ParlourId=' + ParlourId;
        }
        $(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
                Datatableupdate();
                Dependaendants();
            });
        });
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(CheckAllEmp);
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(CheckAllDepn);
            Datatableupdate();
            Dependaendants();
        });
        function CheckAllEmp(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=gvOutstanding.ClientID %>");
            if (GridVwHeaderChckbox != null) {
                for (i = 0; i < GridVwHeaderChckbox.rows.length; i++) {
                    gvOutstanding.getElementsByTagName("input")[i].checked = Checkbox.checked;
                }
            }
        }
        function Datatableupdate() {
            $("input:checkbox").click(function () {
                var checkedCheckboxes = $("#<%=gvOutstanding.ClientID%> input[id*='SelectCheckBox']:checkbox:checked").size();
                var totalCheckboxes = $("#<%=gvOutstanding.ClientID%> input[id*='SelectCheckBox']:checkbox").size();
                var numberOfChecked = $('input:checkbox:checked').length;
                var totalCheckboxes = $('input:checkbox').length - 1;
                if (totalCheckboxes == checkedCheckboxes) {
                    document.getElementById("gvOutstanding_chkboxSelectAll").checked = 'true';
                    $("#<%=gvOutstanding.ClientID%> input[id*='chkboxSelectAll']:checkbox").attr('checked', true);
                }
                else {
                    $("#<%=gvOutstanding.ClientID%> input[id*='chkboxSelectAll']:checkbox").attr('checked', false);
                }
            });
        }
        function PrintGridData() {
            var prtGrid = document.getElementById('<%=gvOutstanding.ClientID %>');
            prtGrid.border = 0;
            var prtwin = window.open('', 'PrintGridViewData', 'left=100,top=100,width=1000,height=1000,tollbar=0,scrollbars=1,status=0,resizable=1');
            prtwin.document.write(prtGrid.outerHTML);
            prtwin.document.close();
            prtwin.focus();
            prtwin.print();
            prtwin.close();
        }
        function CheckAllDepn(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=gvDepandandants.ClientID %>");
            if (GridVwHeaderChckbox != null) {
                for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                    gvDepandandants.getElementsByTagName("input")[i].checked = Checkbox.checked;
                }
            }
        }
        function Dependaendants() {
            $("input:checkbox").click(function () {
                var checkedCheckboxes = $("#<%=gvDepandandants.ClientID%> input[id*='SelectDepCheckBox']:checkbox:checked").size();
                var totalCheckboxes = $("#<%=gvDepandandants.ClientID%> input[id*='SelectDepCheckBox']:checkbox").size();
                var numberOfChecked = $('input:checkbox:checked').length;
                var totalCheckboxes = $('input:checkbox').length - 1;
                if (totalCheckboxes == checkedCheckboxes) {
                    document.getElementById("gvDepandandants_chkboxDepSelectAll").checked = 'true';
                    $("#<%=gvDepandandants.ClientID%> input[id*='chkboxDepSelectAll']:checkbox").attr('checked', true);
                }
                else {
                    $("#<%=gvDepandandants.ClientID%> input[id*='chkboxDepSelectAll']:checkbox").attr('checked', false);
                }
            });
        }
    </script>
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

        #form1 {
            background-color: white;
        }
    </style>
</head>
<body style="background-color: #F3F3F4;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="ibox float-e-margins">
            <div class="ibox-title">
                <h5>Email Generated Report</h5>
            </div>
            <div class="ibox-content">
                <div class="col-sm-12">
                    <div role="form" class="form-horizontal">
                        <div class="col-sm-5">
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-sm-5 control-label">Report Format</label>
                                    <div class="col-sm-7">
                                        <asp:DropDownList ID="ddlReportFormat" CssClass="form-control" runat="server">
                                            <asp:ListItem>Word</asp:ListItem>
                                            <asp:ListItem>Pdf</asp:ListItem>
                                            <asp:ListItem>Excel</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-5">
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-sm-5 control-label">Email Address: </label>
                                    <div class="col-sm-7">
                                        <asp:TextBox class="form-control" ValidationGroup="sendmail" ID="txtReportEmail" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ControlToValidate="txtReportEmail" ForeColor="Red" ValidationGroup="sendmail" ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ControlToValidate="txtReportEmail" ID="RegularExpressionValidator1" runat="server" ErrorMessage="*Invalid Email Address." ValidationGroup="sendmail" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:Button ValidationGroup="sendmail" ID="btnSendMail" runat="server" Text="Send Mail" OnClick="btnSendMail_Click" class="btn btn-outline pull-right btn-warning" />
                    </div>
                </div>
                <div class="col-sm-12">
                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>
        <div style="background-color: #F3F3F4;">
            <div class="row" id="divgrid" runat="server" visible="false">
                <div class="col-sm-12">
                    <div role="form" class="form col-sm-12">
                        <div class="ibox float-e-margins">
                            <div>
                                <div role="form" class="form-horizontal">
                                    <div class="col-sm-5">
                                        <h3 style="margin: 0px"><b>Outstanding Payments</b>
                                        </h3>
                                    </div>
                                    <div class="col-sm-7">
                                        <div class="row">
                                            <div class="form-group">
                                                <div class="col-sm-12">
                                                    <asp:Button ID="btnReminder" runat="server" Text="Send Reminder" OnClick="btnReminder_Click" class="btn btn-outline btn-default" />
                                                    <asp:Button ID="btnoutstandmember" runat="server" Text="View member Transaction History" OnClick="btnoutstandmember_Click" class="btn btn-outline btn-default" />
                                                    <asp:Button ID="btnOutProcessPayment" runat="server" Text="Proceed to member Payment" OnClick="btnOutProcessPayment_Click" class="btn btn-outline btn-default" />
                                                    <asp:Button ID="btnPrintOutstanding" runat="server" Text="Print" OnClientClick="PrintGridData()" class="btn btn-outline btn-default" />
                                                </div>
                                            </div>
                                        </div>
                                        <h3 style="margin: 0px"><b>
                                            <asp:Label ID="lblMassageChk1" runat="server" Text=""></asp:Label></b></h3>
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div role="form" class="form col-sm-12">
                        <asp:GridView EmptyDataText="Data not Found." ID="gvOutstanding" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" CssClass="gvOutstanding"
                            EnableEventValidation="false" CellPadding="5" CellSpacing="2" BorderStyle="None" GridLines="None" BorderWidth="0px" BorderColor="#1AB394" ForeColor="#330033">
                            <Columns>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMemberID" runat="server" Text='<%#Eval("pkiMemberID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="SelectCheckBox" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Width="3%" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Memebernumber" HeaderText="Policy No" SortExpression="Policy No" ItemStyle-Width="8%" />
                                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" ItemStyle-Width="8%" />
                                <asp:BoundField DataField="Surname" HeaderText="Surname" SortExpression="Surname" ItemStyle-Width="5%" />
                                <asp:BoundField DataField="IdNumber" HeaderText="ID Number" SortExpression="IdNumber" ItemStyle-Width="5%" />
                                <asp:BoundField DataField="Member" HeaderText="Adress" SortExpression="Adress" ItemStyle-Width="35%" />

                                <asp:BoundField DataField="Cellphone" HeaderText="Cellphone" SortExpression="Cellphone" ItemStyle-Width="8%" />
                                <asp:BoundField DataField="Balance" HeaderText="Balance Due" SortExpression="Balance Due" DataFormatString="{0:n}" ItemStyle-Width="8%" />
                                <asp:BoundField DataField="PlanName" HeaderText="plan Name" SortExpression="plan Name" ItemStyle-Width="15%" />
                                <asp:BoundField DataField="Premium" HeaderText="Subscription" SortExpression="Subscription" DataFormatString="{0:n}" ItemStyle-Width="8%" />
                            </Columns>
                            <AlternatingRowStyle BackColor="White" />
                            <HeaderStyle BackColor="#1AB394" Font-Bold="True" ForeColor="Black" Height="34px" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <div class="row" id="divgvDependendance" runat="server" visible="false">
                <div class="col-sm-12">
                    <div role="form" class="form col-sm-12">
                        <div class="ibox float-e-margins">
                            <div>
                                <div role="form" class="form-horizontal">
                                    <div class="col-sm-7">
                                        <h3 style="margin: 0px"><b>Dependants Over Age 21</b>
                                        </h3>
                                    </div>
                                    <div class="col-sm-5">
                                        <div class="row">
                                            <div class="form-group">
                                                <div class="col-sm-3">
                                                    <asp:Button ID="btnSendReminderDpndc" runat="server" Text="Send Reminder" OnClick="btnSendReminderDpndc_Click" class="btn btn-outline pull-left btn-default" />
                                                </div>
                                            </div>
                                        </div>
                                        <h3 style="margin: 0px"><b>
                                            <asp:Label ID="lblMassageChk" runat="server" Text=""></asp:Label></b></h3>
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div role="form" class="form col-sm-12">
                        <asp:GridView EmptyDataText="Data not Found." ID="gvDepandandants" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" CssClass="gvOutstanding"
                            EnableEventValidation="false" CellPadding="5" CellSpacing="2" BorderStyle="None" GridLines="None" Width="100%" BorderWidth="0px" BorderColor="#1AB394" ForeColor="#330033">
                            <Columns>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMemberID" runat="server" Text='<%#Eval("MemberId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkboxDepSelectAll" runat="server" onclick="CheckAllDepn(this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="SelectDepCheckBox" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Width="3%" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="MemeberNumber" HeaderText="Policy No" SortExpression="Policy No" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="member" HeaderText="Member" SortExpression="Member" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="Cellphone" HeaderText="Cellphone" SortExpression="Cellphone" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="planName" HeaderText="Plan" SortExpression="Plan" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="DependentName" HeaderText="Dependant" SortExpression="Dependant" ItemStyle-Width="15%" />
                                <asp:BoundField DataField="IDNumber" HeaderText="ID No" SortExpression="ID No" ItemStyle-Width="15%" />
                                <asp:BoundField DataField="DependencyType" HeaderText="Dependant Type" SortExpression="Dependant Type" ItemStyle-Width="12%" />
                                <asp:BoundField DataField="monthDate" HeaderText="Month" SortExpression="Month" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="RealAge" HeaderText="Age" SortExpression="Age" ItemStyle-Width="5%" />
                            </Columns>
                            <AlternatingRowStyle BackColor="White" />
                            <HeaderStyle BackColor="#1AB394" Font-Bold="True" ForeColor="Black" Height="34px" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <div class="row" id="divrdlc" runat="server" visible="false">
                <div class="col-sm-12">
                    <rsweb:ReportViewer ID="rvMembersByDateRange"  ShowPrintButton="true" runat="server" CssClass="ReportView" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" Width="99%" WaitMessageFont-Size="14pt" Height="613px" ViewStateMode="Enabled">
                        <LocalReport ReportPath="Admin\Reports\ReportLayouts\JoinedMembersByDate.rdlc">
                            <DataSources>
                                <rsweb:ReportDataSource DataSourceId="ObjectDataSourceJoinedMembersbyDate" Name="dsJoinedMembersByDate" />
                            </DataSources>
                        </LocalReport>
                    </rsweb:ReportViewer>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
