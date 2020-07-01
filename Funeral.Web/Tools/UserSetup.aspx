<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="UserSetup.aspx.cs" Inherits="Funeral.Web.Tools.UserSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <style>
        .chkChoice td {
            padding-left: 30px;
        }

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
                    <asp:ValidationSummary runat="server" ID="vSummaryUserSetup" ValidationGroup="UserSetup" ForeColor="Red" />
                </div>
            </div>
        </div>
        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Personal Details</h5>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-lg-6">
                            
                            <div class="form-group">
                                <label>Full Name <em>*</em>  </label>
                                <asp:TextBox MaxLength="50" runat="server" ID="txtFullName" name="FullName" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="UserSetup" ControlToValidate="txtFullName" ID="RequiredFieldValidator2" ForeColor="red" runat="server" ErrorMessage="Please enter Full Name"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label>Surname <em>*</em> </label>
                                <asp:TextBox MaxLength="50" runat="server" ID="txtSurname" name="Surname" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="UserSetup" ControlToValidate="txtSurname" ID="RequiredFieldValidator1" ForeColor="red" runat="server" ErrorMessage="Please enter Surname"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label>ID Number/ passport Number&nbsp; </label>
                                <asp:TextBox MaxLength="50" runat="server" ID="txtIDNumber" name="IDNumber" type="text" class="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Cell No&nbsp; </label>
                                <asp:TextBox MaxLength="15" runat="server" ID="txtCellPhone" name="CellPhone" type="text" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Physical Address </label>
                                <asp:TextBox MaxLength="50" runat="server" ID="txtline1" name="line1" type="text" class="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Street </label>
                                <asp:TextBox MaxLength="50" runat="server" ID="txtline2" name="line2" type="text" class="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Town</label>
                                <asp:TextBox MaxLength="50" runat="server" ID="txtline3" name="line3" type="text" class="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Province </label>
                                <asp:TextBox MaxLength="50" runat="server" ID="txtline4" name="line4" type="text" class="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Code&nbsp; </label>
                                <asp:TextBox MaxLength="30" runat="server" ID="txtpostalcode" name="txtPassport" type="text" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Access Details</h5>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>UserName  <em>*</em> </label>
                                <asp:TextBox MaxLength="30" runat="server" ID="txtUserNameUser" name="UserName" type="text" class="form-control"></asp:TextBox>
                                <asp:RegularExpressionValidator Display="None" runat="server" ID="revEmailValidation" ValidationGroup="UserSetup" ControlToValidate="txtUserNameUser" ErrorMessage="Please enter valid UserName/email address" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="UserSetup" ControlToValidate="txtUserNameUser" ID="RequiredFieldValidator5" ForeColor="red" runat="server" ErrorMessage="Please enter UserName"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Password <em>*</em> </label>
                                <asp:TextBox MaxLength="25" runat="server" ID="txtPasswordpass" name="Password" type="text" class="form-control" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="UserSetup" ControlToValidate="txtPasswordpass" ID="RequiredFieldValidator3" ForeColor="red" runat="server" ErrorMessage="Please enter Password"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Select Branch</label>
                                <asp:DropDownList ID="ddlBankBranch" class="form-control" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <%--<div class="col-lg-6">
                            <div class="form-group">
                                <label>Custom Grouping 1</label>
                                <asp:DropDownList ID="ddlCustom1" class="form-control" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>--%>
                        <%--<div class="col-lg-6">
                            <div class="form-group">
                                <label>Custom Grouping 2</label>
                                <asp:DropDownList ID="ddlCustom2" class="form-control" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>--%>
                       <%-- <div class="col-lg-6">
                            <div class="form-group">
                                <label>Custom Grouping 3</label>
                                <asp:DropDownList ID="ddlCustom3" class="form-control" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>--%>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>User Security Group</h5>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="form-group">
                            <asp:CheckBoxList ID="chkSecurityGroup" Width="100%" runat="server" DataTextField="sSecureGroupName" DataValueField="pkiSecureGroupID" RepeatColumns="3" RepeatLayout="Table"></asp:CheckBoxList>
                            <%-- MaxLength="30" runat="server" ID="TextBox1" name="UserName" type="text" class="form-control"></asp:CheckBoxList>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--<div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Agent</h5>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Name <em>*</em> </label>
                                <asp:TextBox MaxLength="25" runat="server" ID="txtAgentName" name="Name" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="UserSetup" ControlToValidate="txtAgentName" ID="RequiredFieldValidator6" ForeColor="red" runat="server" ErrorMessage="Please enter Agent Name"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Surname <em>*</em> </label>
                                <asp:TextBox MaxLength="25" runat="server" ID="txtAgentSurname" name="Surname" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="UserSetup" ControlToValidate="txtAgentSurname" ID="RequiredFieldValidator7" ForeColor="red" runat="server" ErrorMessage="Please enter Agent Surname"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>--%>
        <div class="col-lg-12">
            <div class="form-group">
                <div class="btn-group">
                    <asp:Button class="btn btn-sm btn-primary" Visible="true" ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                </div>
                <% if (this.HasCreateRight)
                    {%>
                <div class="btn-group">
                    <asp:Button runat="server" ID="btnSubmite" ValidationGroup="UserSetup" CssClass="btn btn-sm btn-primary" Text="Save" OnClick="btnSubmite_Click" />
                </div>
                <%} %>
            </div>
        </div>
        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>User List</h5>
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
                                <label>Search User :</label>
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
                                <asp:GridView ID="gvUsers" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AllowSorting="true" OnSorting="gvUsers_Sorting"
                                    AllowPaging="true" PageSize="25" OnPageIndexChanging="gvUsers_PageIndexChanging" AutoGenerateColumns="False" EmptyDataText="There are no jobs to display."
                                    OnRowCommand="gvUsers_RowCommand">
                                    <PagerStyle CssClass="pagination-ys" />
                                    <Columns>
                                        <asp:BoundField DataField="UserName" HeaderText="User Name" ReadOnly="True" SortExpression="UserName" />
                                        <asp:BoundField DataField="EmployeeSurname" HeaderText="Surname" ReadOnly="True" SortExpression="EmployeeSurname" />
                                        <asp:BoundField DataField="EmployeeFullname" HeaderText="Full Name" ReadOnly="True" SortExpression="EmployeeFullname" />
                                        <asp:BoundField DataField="EmployeeIDNumber" HeaderText="ID Number" ReadOnly="True" SortExpression="EmployeeIDNumber" />
                                        <asp:BoundField DataField="EmployeeContactNumber" HeaderText="Telephone" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" SortExpression="EmployeeContactNumber" />
                                        <asp:BoundField DataField="LastModified" HeaderText="Modified date" DataFormatString="{0:d}" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" SortExpression="LastModified" />
                                        <%--<asp:HyperLinkField DataTextField="PolicyStatus" HeaderText="Policy Status" DataNavigateUrlFields="MemeberNumber,parlourid" DataNavigateUrlFormatString="~/Admin/ManageMembersPayment.aspx?Id={0}&ParlourId={1}" />--%>
                                        <asp:TemplateField HeaderText="Actions">
                                            <ItemTemplate>
                                                <%--<asp:HyperLink runat="server" ToolTip='Click here to Edit - ' ID="hrLink" NavigateUrl='<%#Eval("pkiMemberID", "~/Admin/ManageMember.aspx?Id={0}") %>'><i class="fa fa-edit"></i></asp:HyperLink>--%>
                                                <% if (this.HasEditRight)
                                                    {%>
                                                <asp:LinkButton runat="server" ToolTip='Click here to Edit - ' ID="lbtnEdit" CommandArgument='<%#Eval("PkiUserID")%>' CommandName="EditUser"><i class="fa fa-edit"></i></asp:LinkButton>
                                                &nbsp;
                                                <%}if (this.HasDeleteRight)
                                                {%>
                                                <asp:LinkButton runat="server" ID="lbtnDelete" OnClientClick="return confirm('Are you sure you want to delete?')" CommandArgument='<%#Eval("PkiUserID")%>' CommandName="deleteUser"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                <%}%>
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
    <!--SerachBox Enter CLick Event-->
    <script type="text/javascript">
        document.getElementById('<%=txtKeyword.ClientID%>').setAttribute("onkeyup", "runScript(event);return false;");
        function runScript(e) {
            if (e.keyCode == 13) {
                document.getElementById('<%=btnSearch.ClientID%>').click();
                return false;
            }
        }
    </script>
</asp:Content>
