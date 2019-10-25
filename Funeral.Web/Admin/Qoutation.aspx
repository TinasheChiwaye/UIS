<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Qoutation.aspx.cs" Inherits="Funeral.Web.Admin.Qoutation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <script>
        $(function () {
            $("#tabs1").tabs();
        });
        $(document).ready(function () {
            $('#tblMembersSearch').dataTable({
                "oLanguage": { "sSearch": "Search Members:" },
                "iDisplayLength": 10,
                "sPaginationType": "full_numbers",
                "aaSorting": [[0, "asc"]]
            });
        });
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
                    <h5>Quotations Contact Info</h5>
                    <div class="ibox-tools">
                        <a class="collapse-link">
                            <i class="fa fa-chevron-up"></i>
                        </a>
                        <a class="dropdown-toggle">
                            <i class="fa fa-wrench"></i>
                        </a>
                        <a class="close-link">
                            <i class="fa fa-times"></i>
                        </a>
                    </div>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Title</label>
                                <asp:DropDownList runat="server" ID="ddlMethod" class="form-control m-b">
                                    <asp:ListItem Value="Select" Text="Select"></asp:ListItem>
                                    <asp:ListItem Value="Mr" Text="Mr"></asp:ListItem>
                                    <asp:ListItem Value="Dr" Text="Dr"></asp:ListItem>
                                    <asp:ListItem Value="Miss" Text="Miss"></asp:ListItem>
                                    <asp:ListItem Value="Mrs" Text="Mrs"></asp:ListItem>
                                    <asp:ListItem Value="Prof" Text="Prof"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Last Name  <em>*</em> </label>
                                <asp:TextBox MaxLength="25" runat="server" ID="txtLastName" name="name" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="Qoutation" ControlToValidate="txtLastName" ID="RequiredFieldValidator1" ForeColor="red" runat="server" ErrorMessage="Please enter Last name"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator Display="None" ID="RegExp1" ValidationGroup="Qoutation" runat="server" ErrorMessage="Last Name Enter Only characters" ControlToValidate="txtLastName" ValidationExpression="[a-zA-Z ]*$" />
                            </div>
                            <div class="form-group">
                                <label>First Name  <em>*</em>  </label>
                                <asp:TextBox MaxLength="25" runat="server" ID="txtFirstname" name="name" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="Qoutation" ControlToValidate="txtFirstName" ID="RequiredFieldValidator2" ForeColor="red" runat="server" ErrorMessage="Please enter first name"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator1" ValidationGroup="Qoutation" runat="server" ControlToValidate="txtFirstname" ErrorMessage="First Name Enter Only characters" ValidationExpression="[a-zA-Z ]*$" />
                            </div>
                            <div class="form-group">
                                <label>Cellphone Number <em>*</em>  </label>
                                <asp:TextBox MaxLength="10" runat="server" ID="txtCellphone" name="txtCellphone" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="Qoutation" ControlToValidate="txtCellphone" ID="RequiredFieldValidator6" ForeColor="red" runat="server" ErrorMessage="Please enter cell phone number"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator2" ValidationGroup="Qoutation" runat="server" ControlToValidate="txtCellphone" ErrorMessage="Cellphone Number Enter Only Number" ValidationExpression="^[0-9]+$" />
                            </div>
                            <div class="form-group">
                                <label>Telephone Number</label>
                                <asp:TextBox MaxLength="10" runat="server" ID="txtTelePhone" name="txtTelePhone" type="text" class="form-control"></asp:TextBox>
                                <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator3" ValidationGroup="Qoutation" runat="server" ControlToValidate="txtTelePhone" ErrorMessage="Telephone Number Enter Only Number" ValidationExpression="^[0-9]*$" />
                            </div>
                            <div class="col-lg-6">
                                <div class="form-group">
                                    <asp:Button runat="server" ID="btnService" Width="95%" CssClass="btn btn-md btn-primary" Text="Create Quotation" OnClick="btncrtQoutation_Click" ValidationGroup="Qoutation" />
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="form-group">
                                    <asp:Button runat="server" ID="SelectServices" Width="95%" CssClass="btn btn-md btn-primary" Text="Select Services" OnClick="btncrtQoutationServices_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Physical Address</label>
                                <asp:TextBox MaxLength="50" runat="server" ID="txtPhysicalAddress" name="name" type="text" class="form-control"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtProvince" ID="RequiredFieldValidator13" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                            </div>
                            <div class="form-group">
                                <label>Street Address</label>
                                <asp:TextBox MaxLength="50" runat="server" ID="txtStreetAddress" name="name" type="text" class="form-control"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ValidationGroup="tab2" ControlToValidate="txtStreetAddress" ID="RequiredFieldValidator5" ForeColor="red" runat="server" Display="None" ErrorMessage="Please enter street address"></asp:RequiredFieldValidator>--%>
                            </div>
                            <div class="form-group">
                                <label>Town or City</label>
                                <asp:TextBox MaxLength="50" runat="server" ID="txtTown" name="name" type="text" class="form-control"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtTown" ID="RequiredFieldValidator12" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                            </div>
                            <div class="form-group">
                                <label>Province</label>
                                <asp:TextBox MaxLength="10" runat="server" ID="txtProvince" name="name" type="text" class="form-control"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtStreetPostalAddress" ID="RequiredFieldValidator15" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                            </div>
                            <div class="form-group">
                                <label>Code</label>
                                <asp:TextBox MaxLength="50" runat="server" ID="txtCode" name="name" type="text" class="form-control"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtCode" ID="RequiredFieldValidator14" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                            </div>


                            <div class="col-lg-6">
                                <div class="form-group">
                                    <asp:Button runat="server" ID="btnClear" Width="95%" CssClass="btn btn-md btn-primary" Text="Clear" OnClick="btnClear_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-12">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5>Quotation List</h5>
                    <div class="ibox-tools">
                        <a class="collapse-link">
                            <i class="fa fa-chevron-up"></i>
                        </a>
                        <a class="dropdown-toggle">
                            <i class="fa fa-wrench"></i>
                        </a>
                        <a class="close-link">
                            <i class="fa fa-times"></i>
                        </a>
                    </div>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-sm-6">
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
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label>Search Members:</label>
                                <asp:Panel ID="pnlSearch" CssClass="input-group" DefaultButton="btnSearch" runat="server">
                                    <asp:TextBox runat="server" ID="txtKeyword" MaxLength="50" CssClass="form-control" placeholder="Search by keyword"></asp:TextBox>
                                    <span class="input-group-btn">&nbsp;<asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-w-m btn-primary" OnClick="btnSearch_Click" />
                                    </span>
                                </asp:Panel>
                            </div>
                        </div>
                        <div class="col-sm-12 ">
                            <div class="table-responsive">
                                <asp:GridView ID="gvQuotationList" OnRowDataBound="gvQuotationList_RowDataBound" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AllowPaging="true" PageSize="25" OnPageIndexChanging="gvQuotation_PageIndexChanging" AutoGenerateColumns="False" EmptyDataText="There are no Quotation  added." OnRowCommand="gvQuotation_RowCommand" OnSorting="gvQuotation_Sorting" AllowSorting="true">
                                    <PagerStyle CssClass="pagination-ys" />
                                    <Columns>

                                        <asp:TemplateField HeaderText="Name" SortExpression="ContactLastName">
                                            <ItemTemplate>
                                                <%#Eval("ContactLastName") %>&nbsp<%#Eval("ContactFirstName") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CellNumber" HeaderText="Cell Number" ReadOnly="True" SortExpression="CellNumber" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                        <asp:BoundField DataField="DateOfQuotation" HeaderText="QuotationDate" ReadOnly="True" SortExpression="DateOfQuotation" DataFormatString="{0:dd/M/yyy}" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                        <asp:BoundField DataField="AddressLine3" HeaderText="Town / City" ReadOnly="True" SortExpression="AddressLine3" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                        <asp:TemplateField HeaderText="Quotation Status" SortExpression="QuotationStatus">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldoctype" runat="server" Text='<%# ( (Eval("QuotationStatus")).ToString()=="Accept"?"Accept":((Eval("QuotationStatus")).ToString()=="Reject"?"Reject":"Pending")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Actions">
                                            <ItemTemplate>
                                                <% if (this.HasEditRight)
                                                { %>
                                                <asp:LinkButton runat="server" ID="lbtnEditDependant" ToolTip="Click here To Edite Quotation" CommandArgument='<%#Eval("QuotationID") %>' CommandName="EditQuotation"><i class="fa fa-edit"></i></asp:LinkButton>
                                                &nbsp;
                                                <%}
                                                if (this.HasDeleteRight)
                                                { %>
                                                <asp:LinkButton runat="server" ID="lbtnDeleteDependatn" ToolTip="Click here To Delete Quotation" OnClientClick="return confirm('Are you sure you want to delete it?')" CommandArgument='<%#Eval("QuotationID") %>' CommandName="DeleteQuotation"><i class="fa fa-trash"></i></asp:LinkButton>
                                                &nbsp;
                                                <%}
                                                if (this.HasReadRight)
                                                { %>
                                                <asp:LinkButton runat="server" ID="LinkButton1" ToolTip="Click here Print " CommandArgument='<%#Eval("QuotationID") %>' CommandName="PrintQuotation"><i class="fa fa-search"></i></asp:LinkButton>
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

