<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AgentInfoSetup.aspx.cs" Inherits="Funeral.Web.Tools.AgentInfoSetup" %>

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
                    <asp:ValidationSummary runat="server" ID="vSummaryAgentSetup" ValidationGroup="AgentSetup" ForeColor="Red" />
                </div>
            </div>
        </div>

        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Agent Setup</h5>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Full Name<em>*</em> </label>
                                <asp:TextBox MaxLength="25" runat="server" ID="txtsurname" name="surname" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="AgentSetup" ControlToValidate="txtsurname" ID="RequiredFieldValidator1" ForeColor="red" runat="server" ErrorMessage="Please enter surname"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label>Surname <em>*</em>  </label>
                                <asp:TextBox MaxLength="50" runat="server" ID="txtfullname" name="fullname" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="AgentSetup" ControlToValidate="txtfullname" ID="RequiredFieldValidator2" ForeColor="red" runat="server" ErrorMessage="Please enter fullname"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label>Percentage <em>*</em></label>
                                <asp:TextBox MaxLength="4" runat="server" ID="txtpercentage" name="P    ercentage" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="AgentSetup" ControlToValidate="txtpercentage" ID="RequiredFieldValidator6" ForeColor="red" runat="server" ErrorMessage="Please enter Percentage"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator1" ValidationGroup="AgentSetup" runat="server" ControlToValidate="txtpercentage" ErrorMessage="Percentage Enter Only Number With 2 Desimal" ValidationExpression="((\d+)((\.\d{1,2})?))$" />
                            </div>
                            <div class="form-group">
                                <label>ContactNumber<em>*</em> </label>
                                <asp:TextBox MaxLength="30" runat="server" ID="txtcontactnumber" name="Contactnumber" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="AgentSetup" ControlToValidate="txtcontactnumber" ID="RequiredFieldValidator8" ForeColor="red" runat="server" ErrorMessage="Please enter Contactnumber"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator5" ValidationGroup="AgentSetup" runat="server" ControlToValidate="txtcontactnumber" ErrorMessage="Cellphone Number Enter Only Number" ValidationExpression="^([0-9\(\)\/\+ \-]*)$" />
                            </div>
                            <div class="form-group">
                                <label>Email <em>*</em> </label>
                                <asp:TextBox MaxLength="50" runat="server" ID="txtEmail" name="Email" type="text" class="form-control"></asp:TextBox>
                                <asp:RegularExpressionValidator Display="None" runat="server" ID="revEmailValidation" ValidationGroup="AgentSetup" ControlToValidate="txtEmail" ErrorMessage="Please enter valid email address" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="AgentSetup" ControlToValidate="txtEmail" ID="RequiredFieldValidator5" ForeColor="red" runat="server" ErrorMessage="Please enter Email Address"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Address1<em>*</em> </label>
                                <asp:TextBox MaxLength="50" runat="server" ID="txtaddress1" name="Address1" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="AgentSetup" ControlToValidate="txtaddress1" ID="RequiredFieldValidator4" ForeColor="red" runat="server" ErrorMessage="Please enter Address1"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label>Address2</label>
                                <asp:TextBox MaxLength="50" runat="server" ID="txtaddress2" name="Address2" type="text" class="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Address3 </label>
                                <asp:TextBox MaxLength="50" runat="server" ID="txtaddress3" name="Address3" type="text" class="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Address4 </label>
                                <asp:TextBox MaxLength="50" runat="server" ID="txtaddress4" name="Address4" type="text" class="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Code 1 <em>*</em> </label>
                                <asp:TextBox MaxLength="30" runat="server" ID="txtcode" name="Code" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="AgentSetup" ControlToValidate="txtcode" ID="RequiredFieldValidator3" ForeColor="red" runat="server" ErrorMessage="Please enter Code"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator6" ValidationGroup="AgentSetup" runat="server" ControlToValidate="txtcode" ErrorMessage="Postal Code Enter Only Number" ValidationExpression="^[0-9]*$" />
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
                    <asp:Button runat="server" ID="btnSubmite" ValidationGroup="AgentSetup" CssClass="btn btn-sm btn-primary" Text="Save" OnClick="btnSubmite_Click" />
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
                                    <h5>Agent List</h5>
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
                                                <label>Search Agent :</label>
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
                                                <asp:GridView ID="gvCompany" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AllowSorting="true" OnSorting="gvCompany_Sorting"
                                                    AllowPaging="true" PageSize="25" OnPageIndexChanging="gvCompany_PageIndexChanging" AutoGenerateColumns="False" EmptyDataText="There are no jobs to display."
                                                    OnRowCommand="gvCompany_RowCommand">
                                                    <PagerStyle CssClass="pagination-ys" />
                                                    <Columns>
                                                        <asp:BoundField DataField="Surname" HeaderText="Surname" ReadOnly="True" SortExpression="Surname" />
                                                        <asp:BoundField DataField="Fullname" HeaderText="Fullname" ReadOnly="True" SortExpression="Fullname" />
                                                        <asp:BoundField DataField="percentage" HeaderText="percentage" ReadOnly="True" SortExpression="percentage" />
                                                        <asp:BoundField DataField="ContactNumber" HeaderText="ContactNumber" ReadOnly="True" SortExpression="ContactNumber" />
                                                        <asp:BoundField DataField="Address1" HeaderText="Address" ReadOnly="True" SortExpression="Address1" />
                                                        <asp:BoundField DataField="Email" HeaderText="Email" ReadOnly="True" SortExpression="Email" />
                                                        <%--<asp:BoundField DataField="LastModified" HeaderText="Modified date" DataFormatString="{0:d}" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" SortExpression="LastModified" />--%>
                                                        <%--<asp:HyperLinkField DataTextField="PolicyStatus" HeaderText="Policy Status" DataNavigateUrlFields="MemeberNumber,parlourid" DataNavigateUrlFormatString="~/Admin/ManageMembersPayment.aspx?Id={0}&ParlourId={1}" />--%>
                                                        <asp:TemplateField HeaderText="Actions">
                                                            <ItemTemplate>
                                                                <% if (this.HasEditRight)
                                                                   {%>
                                                                <asp:LinkButton runat="server" ToolTip='Click here to Edit - ' ID="lbtnEdit" CommandArgument='<%#Eval("ID")%>' CommandName="EditCompany"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                <%--  <asp:HyperLink runat="server" ToolTip='Click here to Add Application User - ' ID="hrLink" NavigateUrl='<%#Eval("parlourid", "~/Tools/UserSetup.aspx?CompanyParlourID={0}") %>'><i class="fa fa-plus"></i></asp:HyperLink>--%>
                                                                <%} if (this.HasDeleteRight)
                                                                   { %>
                                                                <asp:LinkButton runat="server" ID="lbtnDelete" OnClientClick="return confirm('Are you sure you want to delete?')" CommandArgument='<%#Eval("ID")%>' CommandName="deleteCompany"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptControl" runat="server">
</asp:Content>
