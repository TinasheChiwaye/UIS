<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Admin.Master" CodeBehind="UnderwriterPremiums.aspx.cs" Inherits="Funeral.Web.Tools.UnderwriterPremiums" %>

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
                    </div>
                </div>
            </div>
            <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>Underwriter Premiums</h5>
                    </div>
                    <div class="ibox-content">
                        <div class="row">
                            <div class="col-lg-6">

                                <div class="form-group">
                                    <label>Underwriter Name</label>
                                    <asp:DropDownList runat="server" ID="ddlPlanUnderwriter" class="form-control">
                                  <asp:ListItem Value="0">Select UnderWriter Name</asp:ListItem>  

                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="None" InitialValue="0" runat="server" ForeColor="Red" ControlToValidate="ddlPlanUnderwriter" ErrorMessage="Please select Underwriter" ValidationGroup="valUnderwriterPremiums" />
                                </div>

                                <div class="form-group">
                                    <label>Premium Amount</label>
                                    <asp:TextBox runat="server" class="form-control" ID="TxtPremiumAmount"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="None" runat="server" ForeColor="Red" ControlToValidate="txtPremiumAmount" ErrorMessage="Please enter Premium Amount" ValidationGroup="valUnderwriterPremiums" />
                                </div>

                                <div class="form-group">
                                    <label>UnderWriter Premium</label>
                                    <asp:TextBox runat="server" class="form-control" ID="txtUnderWriterPremium"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" Display="None" runat="server" ForeColor="Red" ControlToValidate="txtUnderWriterPremium" ErrorMessage="Please enter Premium Amount" ValidationGroup="valUnderwriterPremiums" />
                                </div>
                                <div class="form-group">
                                    <label>Cover Age From</label>
                                    <div class="row"> 
                                        <div class="col-lg-6">
                                         <asp:DropDownList runat="server" ID="ddlCoverAgeFromNum" class="form-control">
                                    </asp:DropDownList>
                                        </div>
                                       <div class="col-lg-6">
                                             <asp:DropDownList runat="server" ID="ddlCoverAgeFromMonthYear" class="form-control">
                                    <asp:ListItem>Month</asp:ListItem>
                                         <asp:ListItem>Year</asp:ListItem>
                                    </asp:DropDownList>
                                       </div>
                                  </div>
                                    </div>
                                    
                                    <%--<asp:TextBox runat="server" class="form-control" ID="TxtCoverAgeFrom"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ForeColor="Red" ControlToValidate="txtCoverAgeFrom" ErrorMessage="Please enter Cover Age From" ValidationGroup="valUnderwriterPremiums" />--%>
                                   
                                <div class="form-group">
                                    <label>Role Player Type</label>
                                    <asp:DropDownList runat="server" ID="ddlDependencyType" class="form-control">
                                    <asp:ListItem Value="0">Select RolePlayer Type</asp:ListItem>  

                                         </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="None" runat="server" ForeColor="Red" ControlToValidate="ddlDependencyType" ErrorMessage="Please enter Dependency Type" ValidationGroup="valUnderwriterPremiums" />
                                </div>

                              </div>
                            <div class="col-lg-6">

                                <div class="form-group">
                                    <label>Plan Name</label>
                                    <asp:DropDownList runat="server" ID="ddlPlanName" class="form-control">
                                    <asp:ListItem Value="0">Select Plan Name</asp:ListItem>  

                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="None" InitialValue="0" runat="server" ForeColor="Red" ControlToValidate="ddlPlanName" ErrorMessage="Please select Plan Name" ValidationGroup="valUnderwriterPremiums" />
                                </div>

                                <div class="form-group">
                                    <label>Cover Amount</label>
                                    <asp:TextBox runat="server" class="form-control" ID="txtCoverAmount"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="None" runat="server" ForeColor="Red" ControlToValidate="txtCoverAmount" ErrorMessage="Please Enter Cover Amount" ValidationGroup="valUnderwriterPremiums" />
                                </div>
                                <div class="form-group">
                                    <label>UnderWriter Cover</label>
                                    <asp:TextBox runat="server" class="form-control" ID="txtUnderWriterCover"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" Display="None" runat="server" ForeColor="Red" ControlToValidate="txtUnderWriterCover" ErrorMessage="Please Enter Cover Amount" ValidationGroup="valUnderwriterPremiums" />
                                </div>

                                <div class="form-group">
                                    <label>Cover Age To</label>
                                    <div class="row">
                                        <div class="col-lg-6">
                                      
                                 <%--   <asp:TextBox runat="server" class="form-control" ID="TxtCoverAgeTo"></asp:TextBox>--%>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ForeColor="Red" ControlToValidate="txtPremiumAmount" ErrorMessage="Please enter Cover Age To" ValidationGroup="valUnderwriterPremiums" />--%>
                                     <asp:DropDownList runat="server" ID="ddlCoverAgeToNum" class="form-control">
                                    </asp:DropDownList>
                                            </div>
                                        <div class="col-lg-6">
                                    <asp:DropDownList runat="server" ID="ddlCoverAgeToMonthYear" class="form-control">
                                        <asp:ListItem>Month</asp:ListItem>
                                         <asp:ListItem>Year</asp:ListItem>
                                    </asp:DropDownList>
                                    </div>
                                        </div>
                                     </div>

                                <div class="form-group">
                                    <%--<label>Applys To Dependants</label>
                                    <asp:DropDownList runat="server" ID="ddlApplysToDependants" class="form-control">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ForeColor="Red" ControlToValidate="ddlApplysToDependants" ErrorMessage="Please select if applys to Dependants" ValidationGroup="valUnderwriterPremiums" />
                               --%> 

                                </div>
                            </div>

                            </div>
                    <div class="row">
                             <div class="col-lg-12">
                            <div class="form-group">
                                <div class="btn-group">
                                    <asp:Button class="btn btn-sm btn-primary" Visible="true" ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                </div>
                                <div class="btn-group">
                                    <asp:Button runat="server" ID="btnSubmite" ValidationGroup="valUnderwriterPremiums" CssClass="btn btn-sm btn-primary" Text="Save" OnClick="btnSubmit_Click" />
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
                    <h5>Underwriter Premium List</h5>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <div class="input-group">
                                    <label>Show</label>
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
                                    entries
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label>Search Underwriter Name :</label>
                                <asp:Panel ID="pnlSearch" CssClass="input-group" DefaultButton="btnSearch" runat="server">
                                    <asp:TextBox runat="server" ID="txtKeyword" MaxLength="50" CssClass="form-control" placeholder="Search by keyword"></asp:TextBox>
                                    <span class="input-group-btn">&nbsp;<asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-w-m btn-primary" OnClick="btnSearch_Click"/>
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
                                <asp:GridView ID="gvUnderwriterPremium" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AllowSorting="True"
                                 OnSorting="gvUnderwriterPremium_Sorting"  AllowPaging="True" PageSize="25"  OnPageIndexChanging="gvUnderwriterPremium_PageIndexChanging" 
                                     AutoGenerateColumns="False" EmptyDataText="There are no underwriter plans to display." 
                                    ShowHeaderWhenEmpty="True" OnRowDataBound="gvUnderwriterPremium_OnRowDataBound" OnRowCommand="gvUnderwriterPremium_RowCommand">
                                   
                                    <PagerStyle CssClass="pagination-ys" />
                                    <Columns>
                                        <asp:BoundField DataField="UnderwriterName" HeaderText="Plan UnderWriter" ReadOnly="True" SortExpression="UnderwriterName" />
                                        <asp:BoundField DataField="PlanName" HeaderText="Plan Name" ReadOnly="True" SortExpression="PlanName" />
                                        <asp:BoundField DataField="PremiumAmount" HeaderText="Premium" ReadOnly="True" SortExpression="PremiumAmount"/>
                                         <asp:BoundField DataField="CoverAmount" HeaderText="Cover Amount" ReadOnly="True" SortExpression="CoverAmount"/>
                                         <asp:BoundField DataField="CoverAgeFrom" HeaderText="Cover Age From" ReadOnly="True" SortExpression="CoverAgeFrom"/>
                                        <asp:BoundField DataField="CoverAgeTo" HeaderText="Cover Age To" ReadOnly="True" SortExpression="CoverAgeTo"/>
                                        <asp:BoundField DataField="RolePlayerType" HeaderText="RolePlayer Type" ReadOnly="True" SortExpression="RolePlayerType"/>
                                         <asp:BoundField DataField="UnderwriterPremium" HeaderText="UnderWriter Premium" ReadOnly="True" SortExpression="UnderWriterPremium"/>
                                         <asp:BoundField DataField="UnderwriterCover" HeaderText="UnderWriter Cover" ReadOnly="True" SortExpression="UnderWriterCover"/>
                                       
                                       
                                       <%-- <asp:BoundField DataField="ModifiedUser" HeaderText="Modified User" ReadOnly="True" SortExpression="ModifiedUser" DataFormatString="{0:n}" />
                                        <asp:BoundField DataField="LastModified" HeaderText="Modified Date" ReadOnly="True" SortExpression="ModifiedDate" DataFormatString ="{0:dd/MM/yyyy}"/>--%>
                                        <asp:TemplateField HeaderText="Actions">
                                            <ItemTemplate>
     
                                                <%--<asp:HyperLink runat="server" ToolTip='Click here to Edit - ' ID="hrLink" NavigateUrl='<%#Eval("pkiMemberID", "~/Admin/ManageMember.aspx?Id={0}") %>'><i class="fa fa-edit"></i></asp:HyperLink>--%>
                                               <%-- <% if (this.HasEditRight)
                                                   {%>--%>
                                                <asp:LinkButton runat="server" ToolTip='Click here to Edit - ' ID="lbtnEdit" CommandArgument='<%#Eval("PkiUnderwriterPremiumId")%>' CommandName="EditPlan"><i class="fa fa-edit"></i></asp:LinkButton>
                                               <%-- <%} if (this.HasDeleteRight)
                                                   { %>--%>
                                                <asp:LinkButton runat="server" ID="lbtnDelete" OnClientClick="return confirm('Are you sure you want to delete?')" CommandArgument='<%#Eval("PkiUnderwriterPremiumId")%>' CommandName="deletePlan"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
