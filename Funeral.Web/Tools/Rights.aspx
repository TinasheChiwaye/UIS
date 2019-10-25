<%@ Page Title="Menu Rights" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Rights.aspx.cs" Inherits="Funeral.Web.Tools.Rights" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <style>
        em {
            color: red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>
        <div class="col-lg-12">
            <div class="form-group">
                <div class="input-group">
                    <asp:ValidationSummary runat="server" ID="vSummaryCustom" ValidationGroup="Custom" ForeColor="Red" />
                </div>
            </div>
        </div>
        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-title">
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group row">
                                <label for="ddlGroupId" class="col-lg-4 col-form-label">Groups <em>*</em> </label>
                                <div class="col-lg-8">
                                    <asp:DropDownList ID="ddlGroupId" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="itemSelected"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Page & Rights List</h5>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <asp:Label ID="lblRecords" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="row">
                        <div class="col-lg-12 ">
                            <div class="table-responsive">
                                <asp:GridView ID="gvRight" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover"
                                    AutoGenerateColumns="False" EmptyDataText="There are no record to display.">
                                    <PagerStyle CssClass="pagination-ys" />
                                    <Columns>
                                        <asp:BoundField DataField="PageName" HeaderText="Page Name" ReadOnly="True" />
                                        <asp:TemplateField HeaderText="hasRights">
                                            <HeaderTemplate>
                                                <asp:CheckBox runat="server" Text="Select All" ID="chkAllRow" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="chkRow" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="hasRights">
                                            <HeaderTemplate>
                                                <asp:CheckBox runat="server" Text="Has Rights" ID="chkAllhasRights" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdfRightId" runat="server" Value='<%# Bind("ID") %>' />
                                                <asp:HiddenField ID="hdnPageId" runat="server" Value='<%# Bind("PageId") %>' />
                                                <asp:CheckBox ID="chkhasRights" Checked='<%# Eval("HasAccess") %>' CssClass="gridCB" runat="server"></asp:CheckBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IsRead">
                                            <HeaderTemplate>
                                                <asp:CheckBox runat="server" Text="Is Read" ID="chkAllIsRead" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkIsRead" Checked='<%# Eval("IsRead") %>' CssClass="gridCB" runat="server"></asp:CheckBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IsWrite">
                                            <HeaderTemplate>
                                                <asp:CheckBox runat="server" Text="Is Write" ID="chkAllIsWrite" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkIsWrite" Checked='<%# Eval("IsWrite") %>' CssClass="gridCB" runat="server"></asp:CheckBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IsDelete">
                                            <HeaderTemplate>
                                                <asp:CheckBox runat="server" Text="Is Delete" ID="chkAllIsDelete" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkIsDelete" Checked='<%# Eval("IsDelete") %>' CssClass="gridCB" runat="server"></asp:CheckBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IsUpdate">
                                            <HeaderTemplate>
                                                <asp:CheckBox runat="server" Text="Is Update" ID="chkAllIsUpdate" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkIsUpdate" Checked='<%# Eval("IsUpdate") %>' CssClass="gridCB" runat="server"></asp:CheckBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Is Payment Reversal">
                                            <HeaderTemplate>
                                                <asp:CheckBox runat="server" Text="Is Payment Reversal" ID="chkAllIsPaymentReversal" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkIsPaymentReversal" Checked='<%# Eval("IsReversalPayment") %>' Visible='<%# (Eval("PageId").ToString()=="34" || Eval("PageId").ToString()=="35")?true:false %>' CssClass="gridCB" runat="server"></asp:CheckBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="col-lg-12 ">
                            <div class="input-group pull-right">
                                <asp:Button ID="bntSubmintData" runat="server" OnClick="bntSubmintData_click" CssClass="btn btn-primary pull-right" Text="Save" />
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
        $(document).ready(function () {
            //////// IsRead Access Cntrol 
            $("#<%=gvRight.ClientID%> input[id*='chkIsRead']:checkbox").click(function () {
                var totalCheckboxes = $("#<%=gvRight.ClientID%> input[id*='chkIsRead']:checkbox").size();
                var checkedCheckboxes = $("#<%=gvRight.ClientID%> input[id*='chkIsRead']:checkbox:checked").size();
                $("#<%=gvRight.ClientID%> input[id*='chkAllIsRead']:checkbox").prop('checked', totalCheckboxes === checkedCheckboxes);
            });
            $("#<%=gvRight.ClientID%> input[id*='chkAllIsRead']:checkbox").click(function () {
                if ($(this).is(':checked')) {
                    $("#<%=gvRight.ClientID%> input[id*='chkIsRead']:checkbox").prop('checked', true);
                } else {
                    $("#<%=gvRight.ClientID%> input[id*='chkIsRead']:checkbox").prop('checked', false);
                }
            });
            //// Is Write Access control
            $("#<%=gvRight.ClientID%> input[id*='chkIsWrite']:checkbox").click(function () {
                var totalCheckboxes = $("#<%=gvRight.ClientID%> input[id*='chkIsWrite']:checkbox").size();
                var checkedCheckboxes = $("#<%=gvRight.ClientID%> input[id*='chkIsWrite']:checkbox:checked").size();
                $("#<%=gvRight.ClientID%> input[id*='chkAllIsWrite']:checkbox").prop('checked', totalCheckboxes === checkedCheckboxes);
            });
            $("#<%=gvRight.ClientID%> input[id*='chkAllIsWrite']:checkbox").click(function () {
                if ($(this).is(':checked')) {
                    $("#<%=gvRight.ClientID%> input[id*='chkIsWrite']:checkbox").prop('checked', true);
                } else {
                    $("#<%=gvRight.ClientID%> input[id*='chkIsWrite']:checkbox").prop('checked', false);
                }
            });
            //// Is Update Access control
            $("#<%=gvRight.ClientID%> input[id*='chkIsUpdate']:checkbox").click(function () {
                var totalCheckboxes = $("#<%=gvRight.ClientID%> input[id*='chkIsUpdate']:checkbox").size();
                var checkedCheckboxes = $("#<%=gvRight.ClientID%> input[id*='chkIsUpdate']:checkbox:checked").size();
                $("#<%=gvRight.ClientID%> input[id*='chkAllIsUpdate']:checkbox").prop('checked', totalCheckboxes === checkedCheckboxes);
            });
            $("#<%=gvRight.ClientID%> input[id*='chkAllIsUpdate']:checkbox").click(function () {
                if ($(this).is(':checked')) {
                    $("#<%=gvRight.ClientID%> input[id*='chkIsUpdate']:checkbox").prop('checked', true);
                } else {
                    $("#<%=gvRight.ClientID%> input[id*='chkIsUpdate']:checkbox").prop('checked', false);
                }
            });
            //// Is Delete Access control
            $("#<%=gvRight.ClientID%> input[id*='chkIsDelete']:checkbox").click(function () {
                var totalCheckboxes = $("#<%=gvRight.ClientID%> input[id*='chkIsDelete']:checkbox").size();
                var checkedCheckboxes = $("#<%=gvRight.ClientID%> input[id*='chkIsDelete']:checkbox:checked").size();
                $("#<%=gvRight.ClientID%> input[id*='chkAllIsDelete']:checkbox").prop('checked', totalCheckboxes === checkedCheckboxes);
            });
            $("#<%=gvRight.ClientID%> input[id*='chkAllIsDelete']:checkbox").click(function () {
                if ($(this).is(':checked')) {
                    $("#<%=gvRight.ClientID%> input[id*='chkIsDelete']:checkbox").prop('checked', true);
                } else {
                    $("#<%=gvRight.ClientID%> input[id*='chkIsDelete']:checkbox").prop('checked', false);
                }
            });
            //// Is Rights Access control
            $("#<%=gvRight.ClientID%> input[id*='chkhasRights']:checkbox").click(function () {
                var totalCheckboxes = $("#<%=gvRight.ClientID%> input[id*='chkhasRights']:checkbox").size();
                var checkedCheckboxes = $("#<%=gvRight.ClientID%> input[id*='chkhasRights']:checkbox:checked").size();
                $("#<%=gvRight.ClientID%> input[id*='chkAllhasRights']:checkbox").prop('checked', totalCheckboxes === checkedCheckboxes);
            });
            $("#<%=gvRight.ClientID%> input[id*='chkAllhasRights']:checkbox").click(function () {
                if ($(this).is(':checked')) {
                    $("#<%=gvRight.ClientID%> input[id*='chkhasRights']:checkbox").prop('checked', true);
                } else {
                    $("#<%=gvRight.ClientID%> input[id*='chkhasRights']:checkbox").prop('checked', false);
                }
            });
            $("#<%=gvRight.ClientID%> input[id*='chkAllRow']:checkbox").click(function () {
                if ($(this).is(':checked')) {
                    $("#<%=gvRight.ClientID%> input[id*='chkIs']:checkbox").prop('checked', true);
                    $("#<%=gvRight.ClientID%> input[id*='chkhas']:checkbox").prop('checked', true);
                    $("#<%=gvRight.ClientID%> input[id*='chkRow']:checkbox").prop('checked', true);
                } else {
                    $("#<%=gvRight.ClientID%> input[id*='chkIs']:checkbox").prop('checked', false);
                    $("#<%=gvRight.ClientID%> input[id*='chkhas']:checkbox").prop('checked', false);
                    $("#<%=gvRight.ClientID%> input[id*='chkRow']:checkbox").prop('checked', false);
                }
            });
            $("#<%=gvRight.ClientID%> input[id*='chkRow']:checkbox").click(function () {
                if ($(this).is(':checked')) {
                    $("#<%=gvRight.ClientID%> input[id='" + $(this).attr('id').replace('chkRow', 'chkhasRights') + "']:checkbox").prop('checked', true);
                    $("#<%=gvRight.ClientID%> input[id='" + $(this).attr('id').replace('chkRow', 'chkIsDelete') + "']:checkbox").prop('checked', true);
                    $("#<%=gvRight.ClientID%> input[id='" + $(this).attr('id').replace('chkRow', 'chkIsUpdate') + "']:checkbox").prop('checked', true);
                    $("#<%=gvRight.ClientID%> input[id='" + $(this).attr('id').replace('chkRow', 'chkIsWrite') + "']:checkbox").prop('checked', true);
                    $("#<%=gvRight.ClientID%> input[id='" + $(this).attr('id').replace('chkRow', 'chkIsRead') + "']:checkbox").prop('checked', true);
                } else {
                    $("#<%=gvRight.ClientID%> input[id='" + $(this).attr('id').replace('chkRow', 'chkhasRights') + "']:checkbox").prop('checked', false);
                    $("#<%=gvRight.ClientID%> input[id='" + $(this).attr('id').replace('chkRow', 'chkIsDelete') + "']:checkbox").prop('checked', false);
                    $("#<%=gvRight.ClientID%> input[id='" + $(this).attr('id').replace('chkRow', 'chkIsUpdate') + "']:checkbox").prop('checked', false);
                    $("#<%=gvRight.ClientID%> input[id='" + $(this).attr('id').replace('chkRow', 'chkIsWrite') + "']:checkbox").prop('checked', false);
                    $("#<%=gvRight.ClientID%> input[id='" + $(this).attr('id').replace('chkRow', 'chkIsRead') + "']:checkbox").prop('checked', false);
                }
            });
        });
    </script>
</asp:Content>
