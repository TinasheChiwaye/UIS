<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ImportExcel.aspx.cs" Inherits="Funeral.Web.Admin.ImportExcel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <style>
        #GridViewtablecontainer {
            position: relative;
            width: auto;
            height: 500px;
            overflow: auto;
            background-color: #FFF;
            padding: 0 10px 0 0;
        }

        table.ImportedDataGriview {
            border-collapse: separate;
            border-spacing: 0;
            text-align: center;
            border-left: 1px solid #e7eaec;
            border-top: 1px solid #e7eaec;
        }

            table.ImportedDataGriview caption {
                padding: 3px 10px;
                font-size: larger;
                font-weight: bold;
                color: #FFF;
                background: #BF8660;
                border: 1px solid #596380;
                border-top-width: 3px;
                border-left-width: 5px;
            }

            table.ImportedDataGriview thead {
                background: #CFD4E6;
            }

                table.ImportedDataGriview thead td,
                table.ImportedDataGriview thead th {
                    color: #000;
                    font-weight: bold;
                }

            table.ImportedDataGriview td,
            table.ImportedDataGriview th {
                padding: 6px 10px;
                text-align: center;
                border-right: 1px solid #e7eaec;
                border-bottom: 1px solid #e7eaec;
            }
    </style>
    <script src='<%=ResolveUrl("~/Scripts/jquery-2.1.1.min.js") %>' type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/Scripts/GridScroll/x.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/GridScroll/xtableheaderfixed.js")%>"></script>
    <script type='text/javascript'>
        xAddEventListener(window, 'load', function () { new xTableHeaderFixed('ImportedDataGriview', 'GridViewtablecontainer', 0); }, false);
        $(document).ready(function () {
            $(".txtGridViewdatetimepicker").datepicker({ format: 'dd M yyyy' });
        });
        function CheckEnterDateValidate(Control) {
            if (Control.value != null && Control.value != "") {
                $("#" + Control.id).parent().find('span').hide();
                $("#" + Control.id).val(Control.value);
                var Value = $("#" + Control.id).parent();
            }
            else {
                $("#" + Control.id).parent().find('span').show();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>
        <div class="col-lg-12">
            <div class="form-group">
                <div class="input-group">
                    <asp:ValidationSummary runat="server" ID="vSummaryQuotation" ValidationGroup="Qoutation" ForeColor="Red" />
                </div>
            </div>
        </div>
        <div class="col-lg-12">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5>Import Excel File</h5>
                    <div class="ibox-tools">
                        <a class="collapse-link">
                            <i class="fa fa-chevron-up"></i>
                        </a>
                    </div>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label>FileName</label>
                                <asp:FileUpload CssClass="form-control" ID="fn_excelFile" runat="server" />
                                <asp:RequiredFieldValidator ValidationGroup="ImportDetail" ControlToValidate="fn_excelFile" ID="RequiredFieldValidator3" ForeColor="red" runat="server" ErrorMessage="Please select File"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label>Main Member Name</label>
                                <asp:TextBox runat="server" placeholder="Type Main Member Name" ID="txtMainMemberName" name="txtEmail" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ValidationGroup="ImportDetail" ControlToValidate="txtMainMemberName" ID="RequiredFieldValidator1" ForeColor="red" runat="server" ErrorMessage="Please Enter Name"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group" runat="server" id="dvAdministrator">
                                <label>Select Scheme</label>
                                <asp:DropDownList ID="ddlSchemeList" CssClass="form-control" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ValidationGroup="ImportDetail" ControlToValidate="ddlSchemeList" ID="RequiredFieldValidator2" ForeColor="red" runat="server" ErrorMessage="Please select scheme"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <div class="form-group">
                                <asp:Button runat="server" ID="Submit" ValidationGroup="ImportDetail" CssClass="btn btn-md btn-primary" OnClick="Submit_Click" Text="Upload File" />
                            </div>
                        </div>
                    </div>
                    <div runat="server" class="row" id="GridviewData">
                        <div class="col-lg-9">
                            <div class="table-responsive">
                                <asp:GridView ID="gvMapping" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover"
                                    AutoGenerateColumns="True" EmptyDataText="There are no data to display." OnRowDataBound="gvMapping_RowDataBound">
                                    <PagerStyle CssClass="pagination-ys" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Member Column Name">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddMemberColumn" Style="width: 300px;" runat="server"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="table-responsive">
                                <asp:GridView ID="gridDependentMapping" runat="server" CssClass="table table-striped table-bordered table-hover"
                                    AutoGenerateColumns="false" EmptyDataText="There are no data to display.">
                                    <PagerStyle CssClass="pagination-ys" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="System Types">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSystemMemberTypes" Text='<%# Eval("UserTypeName") %>' runat="server"></asp:Label>
                                                <asp:TextBox ID="txtSystemType" Text='<%# Eval("UserTypeName") %>' Visible="false" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Excel">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDependentType" Style="width: 200px;" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <asp:Button runat="server" ID="btnSubmit" Width="95%" CssClass="btn btn-md btn-primary" Visible="false" OnClick="btnSubmit_Click" Text="Submit" />
                        </div>
                    </div>
                    <div runat="server" class="row" id="DivImportedDataList">
                        <div class="col-lg-12">
                            <div id="GridViewtablecontainer">
                                <asp:GridView ID="ImportedDataGriview" runat="server" Width="100%" DataKeyNames="ID" CssClass="table table-striped table-bordered table-hover ImportedDataGriview gvTheGrid" AllowPaging="true" PageSize="50" OnPageIndexChanging="ImportedDataGriview_OnPageIndexChanging"
                                    AutoGenerateColumns="True" OnRowDataBound="ImportedDataGriview_OnRowDataBound" EmptyDataText="There are no data to display.">
                                    <PagerStyle CssClass="pagination-ys" />
                                    <Columns>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <br />
                            <div class="col-sm-6">
                                <asp:HiddenField ID="hdnnewImportedId" runat="server" />
                                <asp:HiddenField ID="hdnSelectedGridColumn" runat="server" />
                            </div>
                            <div class="col-sm-3">
                                <asp:Button runat="server" ID="btnConfirmAndSubmit" Width="100%" CssClass="btn btn-md btn-primary" Visible="false" OnClick="btnConfirmAndSubmit_Click" Text="Confirm and Import to Member" />
                            </div>
                            <div class="col-sm-3">
                                <asp:Button runat="server" ID="btnExceptionReport" Width="100%" CssClass="btn btn-md btn-primary" Visible="false" OnClick="btnExceptionReport_Click" Text="Exception Report" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
    <asp:HiddenField runat="server" ID="hdnColumCount" />
    <asp:HiddenField runat="server" ID="hdnMemberName" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptControl" runat="server">
</asp:Content>
