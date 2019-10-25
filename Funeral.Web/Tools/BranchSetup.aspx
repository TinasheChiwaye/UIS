<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="BranchSetup.aspx.cs" Inherits="Funeral.Web.Tools.BranchSetup" %>

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
                    <asp:ValidationSummary runat="server" ID="vSummaryBranchSetup" ValidationGroup="BranchSetup" ForeColor="Red" />
                </div>
            </div>
        </div>

        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Branch setup</h5>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Branch Name <em>*</em> </label>
                                <asp:TextBox MaxLength="30" runat="server" ID="txtBranchName" name="BranchName" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="BranchSetup" ControlToValidate="txtBranchName" ID="RequiredFieldValidator1" ForeColor="red" runat="server" ErrorMessage="Please enter Branch Name"></asp:RequiredFieldValidator>
                                <br />
                                <label>Branch Code </label>
                                <asp:TextBox MaxLength="25" runat="server" ID="txtBranchCode" name="line4" type="text" class="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Phone Number <em>*</em> </label>
                                <asp:TextBox MaxLength="15" runat="server" ID="txtPhone" name="Phone" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="BranchSetup" ControlToValidate="txtPhone" ID="RequiredFieldValidator3" ForeColor="red" runat="server" ErrorMessage="Please enter Phone number"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator1" ValidationGroup="BranchSetup" runat="server" ControlToValidate="txtPhone" ErrorMessage="Phone Number Enter Only Number" ValidationExpression="^([0-9\(\)\/\+ \-]*)$" />
                            </div>
                            <div class="form-group">
                                <label>Cell No </label>
                                <asp:TextBox MaxLength="15" runat="server" ID="txtCellPhone" name="CellPhone" type="text" class="form-control"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator Display="None" ValidationGroup="BranchSetup" ControlToValidate="txtCellPhone" ID="RequiredFieldValidator4" ForeColor="red" runat="server" ErrorMessage="Please enter cell number"></asp:RequiredFieldValidator>--%>
                                <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator3" ValidationGroup="BranchSetup" runat="server" ControlToValidate="txtCellPhone" ErrorMessage="cell Number Enter Only Number" ValidationExpression="^([0-9\(\)\/\+ \-]*)$" />
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Physical Address <em>*</em> </label>
                                <asp:TextBox MaxLength="35" runat="server" ID="txtline1" name="line1" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="BranchSetup" ControlToValidate="txtline1" ID="RequiredFieldValidator12" ForeColor="red" runat="server" ErrorMessage="Please enter Physical Address"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label>Street </label>
                                <asp:TextBox MaxLength="25" runat="server" ID="txtline2" name="line2" type="text" class="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Town</label>
                                <asp:TextBox MaxLength="25" runat="server" ID="txtline3" name="line3" type="text" class="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Province </label>
                                <asp:TextBox MaxLength="25" runat="server" ID="txtline4" name="line4" type="text" class="form-control"></asp:TextBox>
                                <br />
                                <div class="form-group">
                                    <label>Region </label>
                                    <asp:TextBox MaxLength="25" runat="server" ID="txtRegion" name="line4" type="text" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label>Code <em>*</em> </label>
                                <asp:TextBox MaxLength="30" runat="server" ID="txtpostalcode" name="txtPassport" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="BranchSetup" ControlToValidate="txtpostalcode" ID="RequiredFieldValidator16" ForeColor="red" runat="server" ErrorMessage="Please Enter Postal Code"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator6" ValidationGroup="BranchSetup" runat="server" ControlToValidate="txtpostalcode" ErrorMessage="Postal Code Enter Only Number" ValidationExpression="^[0-9]*$" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-lg-12">
            <div class="form-group">
                <div class="btn-group">
                    <asp:Button class="btn btn-sm btn-primary" Visible="true" ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                </div>
                <div class="btn-group">
                    <asp:Button runat="server" ID="btnSubmite" ValidationGroup="BranchSetup" CssClass="btn btn-sm btn-primary" Text="Save" OnClick="btnSubmite_Click" />
                </div>
            </div>
        </div>

        <div class="col-lg-12">
            <div id="Payment-History" class="ui-tabs-panel ui-widget-content ui-corner-bottom">
                <div class="dataTables_wrapper" id="tblMembersSearch_wrapper">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="ibox ">
                                <div class="ibox-title">
                                    <h5>Branches List</h5>
                                </div>
                                <div class="ibox-content">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <asp:Label runat="server" ID="Label1"></asp:Label>
                                        </div>
                                        <asp:Label ID="lblRecords" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12 ">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvBranches" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover"
                                                    AutoGenerateColumns="False" EmptyDataText="There are no jobs to display."
                                                    OnRowCommand="gvBranches_RowCommand">
                                                    <PagerStyle CssClass="pagination-ys" />
                                                    <Columns>
                                                        <%--<asp:BoundField DataField="Brancheid" HeaderText="ID" ReadOnly="True"  />--%>
                                                        <asp:BoundField DataField="BranchName" HeaderText="Branch Name" ReadOnly="True" />
                                                        <asp:BoundField DataField="ModifiedUser" HeaderText="Modified User" ReadOnly="True" />
                                                        <asp:BoundField DataField="LastModified" HeaderText="Modified date" DataFormatString="{0:d}" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                        <asp:BoundField DataField="TelNumber" HeaderText="Telephone" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                                        <asp:BoundField DataField="CellNumber" HeaderText="Cell Number" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                                        <asp:TemplateField HeaderText="Actions">
                                                            <ItemTemplate>
                                                                <% if (this.HasEditRight)
                                                                   {%>
                                                                <asp:LinkButton runat="server" ToolTip='Click here to Edit - ' ID="lbtnEdit" CommandArgument='<%#Eval("Brancheid")%>' CommandName="EditBranch"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                <%} if (this.HasDeleteRight)
                                                                   { %>
                                                                <asp:LinkButton runat="server" ID="lbtnDelete" OnClientClick="return confirm('Are you sure you want to delete?')" CommandArgument='<%#Eval("Brancheid")%>' CommandName="deleteBranch"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptControl" runat="server">
</asp:Content>
