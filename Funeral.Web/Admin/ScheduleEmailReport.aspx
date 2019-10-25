<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ScheduleEmailReport.aspx.cs" Inherits="Funeral.Web.Admin.ScheduleEmailReport" %>



<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="../Content/bootstrap-datetimepicker.min.css" rel="stylesheet" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .bootstrap-datetimepicker-widget {
            top: 60%;
            left: 17%;
            width: 250px;
            padding: 4px;
            margin-top: 1px;
            z-index: 3000;
            -webkit-border-radius: 4px;
            -moz-border-radius: 4px;
            border-radius: 4px;
        }
    </style>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="row">
        <div class="col-lg-12">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>

        </div>
        <div class="col-lg-12">
            <asp:HiddenField ID="hdnScheduleId" runat="server" />
            <asp:HiddenField ID="hfReportType" runat="server" />
            <asp:HiddenField ID="hfAdminReport" runat="server" />

            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Schedule For Email Report</h5>

                    <%--  <asp:Label ID="lblResult" Style="padding-left: 375px" ForeColor="Red" runat="server" Text=""></asp:Label>--%>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="panel blank-panel">


                            <div class="panel-body">
                                <div class="tab-content">
                                    <div id="tab-1" class="tab-pane active">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <asp:ValidationSummary runat="server" ID="vSummaryTab1" ValidationGroup="tab1" ForeColor="Red" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-6">
                                                <div class="form-group">
                                                    <label for="exampleInputEmail2">Report Type</label>
                                                    <asp:DropDownList ID="ddlReportType" class="form-control" runat="server" onchange="GetSelectedPanel(this)"></asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                    <label for="exampleInputEmail2">Report </label>
                                                    <asp:DropDownList ID="ddlReort" class="form-control" runat="server" onchange="GetSelectedPanelReport()">
                                                        <asp:ListItem Text="Select Report" Value="0"></asp:ListItem>
                                                        <%--<asp:ListItem Text="Report1" Value="Report1"></asp:ListItem>
                                                        <asp:ListItem Text="Report2" Value="Report2"></asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                    <label>Report Duration  <em>*</em> </label>
                                                    <asp:DropDownList ID="ddlfrequency" runat="server" DataTextField="Name" DataValueField="FrequencyCode" CssClass="form-control m-b">
                                                        <asp:ListItem Value="Daily">Daily</asp:ListItem>
                                                        <asp:ListItem Value="Weekly">Weekly</asp:ListItem>
                                                        <asp:ListItem Value="Monthly">Monthly</asp:ListItem>
                                                        <asp:ListItem Value="Yearly">Yearly</asp:ListItem>
                                                        <asp:ListItem Value="Custome">Custome</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                                <%-- bootstrap-timepicker timepicker
                                                --%>
                                                <div class="form-group">
                                                    <label>Time  <em>*</em>  </label>
                                                    <asp:TextBox MaxLength="25" type="text" runat="server" ID="txtTime" name="name" class="form-control input-small"></asp:TextBox>
                                                    <span class="add-on">
                                                        <i data-time-icon="icon-time" data-date-icon="icon-calendar"></i>
                                                    </span>
                                                    <asp:RequiredFieldValidator Display="None" ValidationGroup="tab1" ControlToValidate="txtTime" ID="RequiredFieldValidator2" ForeColor="red" runat="server" ErrorMessage="Please enter Time"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group">
                                                    <label>Email Address</label>
                                                    <asp:TextBox MaxLength="50" runat="server" ID="txtEmail" name="txtEmail" type="text" class="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator Display="None" runat="server" ID="revEmailValidation" ValidationGroup="tab1" ControlToValidate="txtEmail" ErrorMessage="Please enter valid email address" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                    <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtPolicyPremium" ID="RequiredFieldValidator17" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                                </div>



                                            </div>
                                            <div id="tab5" style="display: none" class="col-lg-6">
                                                <div class="form-group">
                                                    <label>Date From </label>
                                                    <asp:TextBox MaxLength="30" runat="server" ID="txtDateFrom" name="name" type="text" class="form-control"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <label>Date To </label>
                                                    <asp:TextBox MaxLength="30" runat="server" ID="txtDateTo" name="name" type="text" class="form-control"></asp:TextBox>
                                                </div>

                                            </div>
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <div class="btn-group">
                                                        <asp:Button runat="server" ID="btnSave" ValidationGroup="tab1" OnClick="btnSave_Click" CssClass="btn btn-sm btn-primary" Text="Save" />
                                                    </div>
                                                    <div class="btn-group">
                                                        <asp:Button class="btn btn-sm btn-primary" Visible="true" ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" />
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
    </div>

    <div class="row">
        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Schedule Detail</h5>
                </div>
                <div class="table-responsive">
                    <asp:GridView ID="gvSchedule" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover"
                        AllowPaging="true" PageSize="25" OnRowCommand="gvSchedule_RowCommand" AutoGenerateColumns="False" EmptyDataText="There are no Schedule.">

                        <PagerStyle CssClass="pagination-ys" />
                        <Columns>
                            <asp:BoundField DataField="ReportType" HeaderText="Report Type" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="Report" HeaderText="Report" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="Frequency" HeaderText="Duration" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="Time" HeaderText="Time" ReadOnly="True" />
                            <asp:BoundField DataField="Email" HeaderText="Email-Id" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />

                            <asp:TemplateField HeaderText="Actions">
                                <ItemTemplate>
                                    <%if (this.HasEditRight)
                                        { %>
                                    <%-- <asp:HyperLink runat="server" ToolTip='Click here to download - ' ID="hrLink" NavigateUrl='<%#Eval("pkiScheduleId", "~/Admin/ManageMember.aspx?Id={0}") %>'><i class="fa fa-edit"></i></asp:HyperLink>
                                    --%>  &nbsp;
                                                  <asp:LinkButton runat="server" ID="lbtnedit" CommandArgument='<%#Eval("pkiScheduleId")%>' CommandName="EditSchedule"><i class="fa fa-edit"></i></asp:LinkButton>

                                    <%}
                                        if (this.HasDeleteRight)
                                        { %>
                                                         &nbsp;
                                                <asp:LinkButton runat="server" ID="lbtnDelete" OnClientClick="return confirm('Are you sure you want to delete?')" CommandArgument='<%#Eval("pkiScheduleId")%>' CommandName="DeleteSchedule"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                    <%--<asp:LinkButton runat="server" ID="lbtnDeleteDependatn" ToolTip="Click here To Delete Member" OnClientClick="return confirm('Are you sure you want to delete it?')" CommandArgument='<%#Eval("pkiMemberID") %>' CommandName="deleteMemeber"><i class="fa fa-trash"></i></asp:LinkButton>--%>
                                    <%} %>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                </div>

            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptControl" runat="server">
    <script src="../Scripts/bootstrap-datetimepicker.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {

            $("#<%=txtDateFrom.ClientID %>").datepicker({ format: 'dd MM yyyy' });
            $("#<%=txtDateTo.ClientID %>").datepicker({ format: 'dd MM yyyy' });
            $("#<%=txtTime.ClientID %>").datetimepicker({
                pickDate: false,
                format: 'HH:mm:ss',
                autoclose: true,
            });
            $("#<%=txtTime.ClientID %>").focusout(function () { 
                $("#<%=txtTime.ClientID %>").datetimepicker('hide');
            })
            $("#<%=txtEmail.ClientID%>").click(function (e) {
                $("#<%=txtTime.ClientID %>").datetimepicker('hide');
                
            });
        });


        $(function () {
            $('#<%=ddlfrequency.ClientID%>').change(function () {
                if ($('#<%=ddlfrequency.ClientID%>').val() == "Custome") {
                    $("#tab5").show();
                } else {
                    $("#tab5").hide();
                }
            });
        });

        function ShowTab5() {
            if ($('#<%=ddlfrequency.ClientID%>').val() == "Custome") {
                $("#tab5").show();
            } else {
                $("#tab5").hide();
            }
        }

        function GetSelectedPanel(ddlAdmin) {
            var selectedValue = $("#MainContent_ddlReportType :selected").val();
            document.getElementById('<%=hfReportType.ClientID%>').value = selectedValue;
            $("#<%=ddlReort.ClientID%>").empty();
            GetAllEmployees(selectedValue);

        }
        function GetAllEmployees(selectedValue) {
            //EnableDisablePanale(selectedValue);
            var DropDownList2 = $("#<%=ddlReort.ClientID%>");
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


            function GetSelectedPanelReport() {
                //var selectedText = ddlAdmin.options[ddlAdmin.selectedIndex].innerHTML;
                var selectedValue = $('#<%=ddlReort.ClientID%>').val();
                console.log(selectedValue);
                document.getElementById('<%=hfAdminReport.ClientID%>').value = selectedValue;
            }
    </script>
</asp:Content>
