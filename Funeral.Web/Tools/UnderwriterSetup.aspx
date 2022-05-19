<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Admin.Master" CodeBehind="UnderwriterSetup.aspx.cs" Inherits="Funeral.Web.Tools.UnderwriterSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="scriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>
        <div class="col-lg-12">
            <div class="form-group">
                <div class="input-group">
                    <asp:ValidationSummary runat="server" ID="vSummaryPlansPremium" ValidationGroup="valUnderwriterPremiums" ForeColor="Red" />
                    <asp:ValidationSummary runat="server" ID="vSummaryUnderwriterSetup" ValidationGroup="valUnderwriterSetup" ForeColor="Red" />

                </div>
            </div>
        </div>
        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>Underwriter Setup</h5>
                    </div>
                    <div class="ibox-content">
                        <div class="row">
                            <div class="col-lg-6">

                                <div class="form-group">
                                    <label>Underwriter Name</label>
                                    <asp:TextBox runat="server" class="form-control" ID="TxtUnderwriterName"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None" ForeColor="Red" ControlToValidate="TxtUnderwriterName" ErrorMessage="Please Enter Underwriter Name" ValidationGroup="valUnderwriterSetup" />
                                </div>

                                <div class="form-group">
                                    <label>Address Line 1</label>
                                    <asp:TextBox runat="server" class="form-control" ID="TxtAddressLine1"></asp:TextBox>

                                </div>

                                <div class="form-group">
                                    <label>Address Line 2</label>
                                    <asp:TextBox runat="server" class="form-control" ID="TxtAddressLine2"></asp:TextBox>

                                </div>


                                <div class="form-group">
                                    <label>City</label>
                                    <asp:TextBox runat="server" class="form-control" ID="TxtCity"></asp:TextBox>

                                </div>


                                <div class="form-group">
                                    <label>Province</label>
                                    <asp:TextBox runat="server" class="form-control" ID="TxtProvince"></asp:TextBox>

                                </div>

                                <div class="form-group">
                                    <label>Contact Number</label>
                                    <asp:TextBox runat="server" class="form-control" ID="TxtContactNumber"></asp:TextBox>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="None" ForeColor="Red" ControlToValidate="TxtContactNumber" ErrorMessage="Please enter ContactNumber" ValidationGroup="valUnderwriterSetup" />
                                </div>

                                <div class="form-group">
                                    <label>FSP Number</label>
                                    <asp:TextBox runat="server" class="form-control" ID="txtFSPNNumber"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="None" ForeColor="Red" ControlToValidate="txtFSPNNumber" ErrorMessage="Please Enter FSPN Number" ValidationGroup="valUnderwriterSetup" />
                                </div>
                                <div class="form-group">
                                    <label>Select Underwriter Logo</label>
                                    <asp:FileUpload ID="fuUnderwriterLogo" runat="server" class="form-control" />
                                    <%--<asp:RequiredFieldValidator Display="None" ValidationGroup="CompanySetup" ControlToValidate="txtcompanyRules" ID="RequiredFieldValidator20" ForeColor="red" runat="server" ErrorMessage="Please enter Company Rules"></asp:RequiredFieldValidator>--%>
                                </div>
                                <p class="formfield">
                                    <label for="textarea">Logo</label>
                                    <asp:Image runat="server" ID="Preview" Height="150px" Width="100%" />

                                </p>
                                <%--<asp:TextBox runat="server" class="form-control" ID="TxtCoverAgeFrom"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ForeColor="Red" ControlToValidate="txtCoverAgeFrom" ErrorMessage="Please enter Cover Age From" ValidationGroup="valUnderwriterPremiums" />--%>
                                <asp:Button runat="server" ID="btnUpload" CssClass="btn btn-sm btn-primary pull-right" OnClick="btnUploadLogo_Click" Text="Upload" Enabled="false" />


                            </div>

                            <div class="col-lg-6">

                                <div class="form-group">
                                    <label>Contact Person</label>
                                    <asp:TextBox runat="server" class="form-control" ID="TxtContactPerson"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="None" ForeColor="Red" ControlToValidate="TxtContactPerson" ErrorMessage="Please enter Contact Person" ValidationGroup="valUnderwriterSetup" />
                                </div>


                                <div class="form-group">
                                    <label>PostalCode</label>
                                    <asp:TextBox runat="server" class="form-control" ID="TxtPostalCode"></asp:TextBox>

                                </div>


                                <div class="form-group">
                                    <label>Country</label>
                                    <asp:TextBox runat="server" class="form-control" ID="TxtCountry"></asp:TextBox>
                                </div>

                                <div class="form-group">
                                    <label>Contact Email</label>
                                    <asp:TextBox runat="server" class="form-control" ID="TxtContactEmail"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="None" ForeColor="Red" ControlToValidate="TxtContactEmail" ErrorMessage="Please Enter Contact Email" ValidationGroup="valUnderwriterSetup" />
                                </div>



                                <div class="form-group">
                                    <label>Select Scheme<em>*</em> </label>
                                    <asp:DropDownList ID="ddlSchemeList" class="form-control" runat="server">                                        
                                    </asp:DropDownList>                                    
                                </div>
                                <div class="form-group">
                                    <%--<label>FSPNNumber</label>
                                    <asp:TextBox runat="server" class="form-control" ID="TextBox1"></asp:TextBox>
                                        </div>--%>
                                </div>

                            </div>
                            <div class="col-lg-12">
                                <div class="form-group">
                                    <div class="btn-group">
                                        <asp:Button class="btn btn-sm btn-primary" Visible="true" ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                    </div>
                                    <div class="btn-group">
                                        <asp:Button runat="server" ID="btnSubmite" ValidationGroup="valUnderwriterSetup" CssClass="btn btn-sm btn-primary" Text="Save" OnClick="btnSubmit_Click" />
                                    </div>
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
                    <h5>Underwriter Setup List</h5>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label>Select Scheme<em>*</em> </label>
                                <asp:DropDownList ID="ddlSchemeTableList" class="form-control" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-6 pull-right">
                            <div class="form-group">
                                <label>Search Underwriter Name :</label>
                                <asp:Panel ID="pnlSearch" CssClass="input-group" DefaultButton="btnSearch" runat="server">
                                    <asp:TextBox runat="server" ID="txtKeyword" MaxLength="50" CssClass="form-control" placeholder="Search by keyword"></asp:TextBox>
                                    <span class="input-group-btn">&nbsp;<asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-w-m btn-primary" OnClick="btnSearch_Click" />
                                    </span>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <div class="row">                        
                        <div class="col-lg-6">
                            <div class="form-group">
                                <div class="input-group">
                                    <label>Show entries</label>
                                    <asp:DropDownList AutoPostBack="true" ID="ddlPageSize" CssClass="form-control" runat="server">
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
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12 ">
                            <div class="table-responsive">
                                <asp:GridView ID="gvUnderwriterSetup" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AllowSorting="True"
                                    AllowPaging="True" PageSize="25" AutoGenerateColumns="False" EmptyDataText="There are no underwriter plans to display." ShowHeaderWhenEmpty="True"
                                    OnSorting="gvUnderwriterSetup_Sorting" OnPageIndexChanging="gvUnderwriterSetup_PageIndexChanging"
                                    OnRowDataBound="gvUnderwriterSetup_OnRowDataBound" OnRowCommand="gvUnderwriterSetup_RowCommand">

                                    <PagerStyle CssClass="pagination-ys" />

                                    <Columns>
                                        <asp:BoundField DataField="UnderwriterName" HeaderText="Underwriter Name" ReadOnly="True" SortExpression="UnderwriterName" />
                                        <asp:BoundField DataField="ContactPerson" HeaderText="ContactPerson" ReadOnly="True" SortExpression="ContactPerson" />
                                        <asp:BoundField DataField="ContactEmail" HeaderText="ContactEmail" ReadOnly="True" SortExpression="ContactEmail" />
                                        <asp:BoundField DataField="ModifiedBy" HeaderText="Modified User" ReadOnly="True" SortExpression="ModifiedUser" DataFormatString="{0:n}" />
                                        <asp:BoundField DataField="ModifiedDate" HeaderText="Modified Date" ReadOnly="True" SortExpression="ModifiedDate" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:TemplateField HeaderText="Actions">
                                            <ItemTemplate>

                                                <%--<asp:HyperLink runat="server" ToolTip='Click here to Edit - ' ID="hrLink" NavigateUrl='<%#Eval("pkiMemberID", "~/Admin/ManageMember.aspx?Id={0}") %>'><i class="fa fa-edit"></i></asp:HyperLink>--%>
                                                <%-- <% if (this.HasEditRight)
                                                   {%>--%>
                                                <asp:LinkButton runat="server" ToolTip='Click here to Edit - ' ID="lbtnEdit" CommandArgument='<%#Eval("PkiUnderWriterSetupId")%>' CommandName="EditPlan"><i class="fa fa-edit"></i></asp:LinkButton>
                                                <%-- <%} if (this.HasDeleteRight)
                                                   { %>--%>
                                                <asp:LinkButton runat="server" ID="lbtnDelete" OnClientClick="return confirm('Are you sure you want to delete?')" CommandArgument='<%#Eval("PkiUnderWriterSetupId")%>' CommandName="deletePlan"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                <%-- <%} %>--%>
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
