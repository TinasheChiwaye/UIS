<%@ Page Title="Custom Field Management" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="CustomManagement.aspx.cs" Inherits="Funeral.Web.Tools.CustomManagement" %>

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
                    <asp:HiddenField ID="hdnId" runat="server" />
                </div>
            </div>
        </div>

        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Custom Management</h5>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Select Custom Grouping Type <em>*</em> </label>
                                <asp:DropDownList ID="ddlCustomType" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomType_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Name <em>*</em> </label>
                                <asp:TextBox MaxLength="35" runat="server" ID="txtName" name="Phone" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="Custom" ControlToValidate="txtName" ID="RequiredFieldValidator3" ForeColor="red" runat="server" ErrorMessage="Please enter custom name"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label>Description </label>
                                <asp:TextBox Rows="5" Columns="50" runat="server" ID="txtDescription" type="text" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-12">
            <div class="form-group">
                <div class="btn-group">
                    <asp:Button class="btn btn-sm btn-primary" Visible="true" ID="btnCancel" runat="server" Text="Cancel" />
                </div>
                <div class="btn-group">
                    <asp:Button runat="server" ID="btnSubmit" ValidationGroup="Custom" CssClass="btn btn-sm btn-primary" Text="Save" OnClick="btnSubmit_Click" />
                </div>
            </div>
        </div>
        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Custom List</h5>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <asp:Label ID="lblRecords" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="row">
                        <div class="col-lg-12 ">
                            <div class="table-responsive">
                                <asp:GridView ID="gvCustom" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover"
                                    AutoGenerateColumns="False" EmptyDataText="There are no record to display."
                                    OnRowCommand="gvCustom_RowCommand">
                                    <PagerStyle CssClass="pagination-ys" />
                                    <Columns>
                                        <asp:BoundField DataField="Name" HeaderText="Branch Name" ReadOnly="True" />
                                        <asp:TemplateField HeaderText="Actions">
                                            <ItemTemplate>
                                                <% if (this.HasEditRight)
                                                   {%>
                                                <asp:LinkButton runat="server" ToolTip='Click here to Edit - ' ID="lbtnEdit" CommandArgument='<%#Eval("Id")%>' CommandName="EditCustom"><i class="fa fa-edit"></i></asp:LinkButton>
                                                <%} if (this.HasDeleteRight)
                                                   { %>
                                                <asp:LinkButton runat="server" ID="lbtnDelete" OnClientClick="return confirm('Are you sure you want to delete?')" CommandArgument='<%#Eval("Id")%>' CommandName="DeleteCustom"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                <%} %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
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
