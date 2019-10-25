<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="VendorSetup.aspx.cs" Inherits="Funeral.Web.Tools.VendorSetup" %>

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
                    <asp:ValidationSummary runat="server" ID="vSummaryVendorSetup" ValidationGroup="VendorSetup" ForeColor="Red" />
                </div>
            </div>
        </div>

        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Vendor setup</h5>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Vendor Name <em>*</em> </label>
                                <asp:TextBox MaxLength="25" runat="server" ID="txtVendorName" name="VendorName" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="VendorSetup" ControlToValidate="txtVendorName" ID="RequiredFieldValidator1" ForeColor="red" runat="server" ErrorMessage="Please enter Vendor Name"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                            </div>
                            <div class="form-group">
                                <div class="btn-group">
                                    <asp:Button class="btn btn-md btn-primary" Visible="true" ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                </div>
                                <div class="btn-group">
                                    <asp:Button runat="server" ID="btnSubmite" ValidationGroup="VendorSetup" CssClass="btn btn-md btn-primary" Text="Save" OnClick="btnSubmite_Click" />
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
                    <h5>Vendors List</h5>
                </div>
                <div class="ibox-content">
                    <div class="row">
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
                                <label>Search Plan :</label>
                                <asp:Panel ID="pnlSearch" CssClass="input-group" DefaultButton="btnSearch" runat="server">
                                    <asp:TextBox runat="server" ID="txtKeyword" MaxLength="50" CssClass="form-control" placeholder="Search by keyword"></asp:TextBox>
                                    <span class="input-group-btn">&nbsp;<asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-w-m btn-primary" OnClick="btnSearch_Click" />
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
                        <div class="col-lg-12 ">
                            <div class="table-responsive">
                                <asp:GridView ID="gvVendores" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AllowSorting="true" OnSorting="gvVendores_Sorting"
                                    AllowPaging="true" PageSize="25" OnPageIndexChanging="gvVendores_PageIndexChanging" AutoGenerateColumns="False" EmptyDataText="There are no jobs to display."
                                    OnRowCommand="gvVendores_RowCommand">
                                    <PagerStyle CssClass="pagination-ys" />
                                    <Columns>
                                        <%-- <asp:BoundField DataField="pkiVendorID" HeaderText="ID" ReadOnly="True" />--%>
                                        <asp:BoundField DataField="VendorName" HeaderText="Vendor Name" ReadOnly="True" SortExpression="VendorName" />
                                        <asp:BoundField DataField="ModifiedUser" HeaderText="Modified User" ReadOnly="True" SortExpression="ModifiedUser" />
                                        <asp:BoundField DataField="LastModified" HeaderText="Modified date" DataFormatString="{0:d}" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" SortExpression="LastModified" />
                                        <asp:TemplateField HeaderText="Actions">
                                            <ItemTemplate>
                                                <% if (this.HasEditRight)
                                                   {%>
                                                <asp:LinkButton runat="server" ToolTip='Click here to Edit - ' ID="lbtnEdit" CommandArgument='<%#Eval("pkiVendorID")%>' CommandName="EditVendor"><i class="fa fa-edit"></i></asp:LinkButton>
                                                <%} if (this.HasDeleteRight)
                                                   { %>
                                                <asp:LinkButton runat="server" ID="lbtnDelete" OnClientClick="return confirm('Are you sure you want to delete?')" CommandArgument='<%#Eval("pkiVendorID")%>' CommandName="deleteVendor"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
