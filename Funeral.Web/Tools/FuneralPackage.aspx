<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="FuneralPackage.aspx.cs" Inherits="Funeral.Web.Tools.FuneralPackage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="scriptManager1" runat="server"></asp:ScriptManager>

    <div class="row">
        <div class="col-lg-12">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
            <div class="ibox ">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>Package Management</h5>
                    </div>
                    <div class="ibox-content">
                        <div class="row">
                            <div class="col-lg-6">
                                <div class="form-group">
                                    <label>Package Name</label>
                                    <asp:TextBox runat="server" class="form-control" ID="txtPackageName"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="btnSavePackage" CssClass="btn btn-sm btn-primary" runat="server" Text="Save Package" OnClick="btnSavePackage_Click" />

                                </div>
                                <div class="form-group">
                                    <%--start --%>
                                    <asp:GridView ID="gvPackageList" OnPageIndexChanging="gvPackageList_PageIndexChanging" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="There are no package available." OnRowCommand="gvPackageList_RowCommand">
                                        <PagerStyle CssClass="pagination-ys" />
                                        <Columns>
                                            <asp:BoundField DataField="PackageName" HeaderText="Package Name" ReadOnly="True" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                            <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="R{0:0.00}" ReadOnly="True" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                            <asp:TemplateField HeaderText="Actions">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lbtnEdit" ToolTip="Click here To Edit Service" CommandArgument='<%#Eval("PackageName")+"~"+Eval("pkiPackageID") %>' CommandName="EditService"><i class="fa fa-edit"></i></asp:LinkButton>
                                                    &nbsp;
                                               <asp:LinkButton runat="server" ID="lbtnDelete" ToolTip="Click here To Delete package" OnClientClick="return confirm('Are you sure you want to delete it?')" CommandArgument='<%#Eval("pkiPackageID") %>' CommandName="DeletePackage"><i class="fa fa-trash"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="form-group">
                                    <label>Service Name</label>
                                    <asp:DropDownList runat="server" ID="ddlServices" class="form-control m-b">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlServices" InitialValue="0" ErrorMessage="Please select something" ValidationGroup="vadd" />
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="btnAddService" CssClass="btn btn-sm btn-primary" runat="server" Text="Add Service" ValidationGroup="vadd" OnClick="btnAddService_Click" />
                                </div>
                                <div class="form-group">
                                    <asp:GridView ID="gvPackageService" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="There are no Service added." OnRowCommand="gvPackageService_RowCommand">
                                        <PagerStyle CssClass="pagination-ys" />
                                        <Columns>
                                            <asp:BoundField DataField="ServiceName" HeaderText="Service Name" ReadOnly="True" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                            <asp:TemplateField HeaderText="Actions">
                                                <ItemTemplate>
                                                    <% if (this.HasEditRight)
                                                       {%>
                                                    <asp:LinkButton runat="server" ID="lbtnEditDependant" ToolTip="Click here To Edit Service" CommandArgument='<%#Eval("pkiPackageID") %>' CommandName="EditService"><i class="fa fa-edit"></i></asp:LinkButton>
                                                    &nbsp;
                                                <%} if (this.HasDeleteRight)
                                                       { %>
                                                    <asp:LinkButton runat="server" ID="lbtnDeleteDependatn" ToolTip="Click here To Delete Service" OnClientClick="return confirm('Are you sure you want to delete it?')" CommandArgument='<%#Eval("pkiPackageID") %>' CommandName="DeleteService"><i class="fa fa-trash"></i></asp:LinkButton>
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
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptControl" runat="server">
</asp:Content>
