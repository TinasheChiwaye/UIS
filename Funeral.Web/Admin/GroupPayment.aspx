<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="MemberPayment.aspx.cs" Inherits="Funeral.Web.Admin.MemberPayment" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Members</h5>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="form-group">
                            <div class="col-sm-10">
                                <div class="row">
                                    <asp:Panel ID="pnlSearch" Width="100%" CssClass="input-group" DefaultButton="btnSearch" runat="server">
                                        <div class="col-md-3" runat="server" id="dvAdministrator">
                                            <div class="form-group">
                                                <label>Company </label>
                                                <asp:DropDownList ID="ddlCompanyList" CssClass="form-control" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div>
                                            <label>Search Members:</label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox runat="server" ID="txtPolicyNo" MaxLength="50" CssClass="form-control" placeholder="Search by Policy No"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox runat="server" ID="txtIDNo" MaxLength="50" CssClass="form-control" placeholder="Search by ID No"></asp:TextBox>
                                        </div>
                                        <div>
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-w-m btn-primary" OnClick="btnSearch_Click" />
                                        </div>
                                        <br />
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="ddlPolicyStatus" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="All" Value="" />
                                                <asp:ListItem Text="Active" Value="Active" />
                                                <asp:ListItem Text="Cancelled" Value="Cancelled" />
                                                <asp:ListItem Text="Deceased" Value="Deceased" />
                                                <asp:ListItem Text="Lapsed" Value="Lapsed" />
                                                <asp:ListItem Text="Deleted" Value="Delete" />
                                                <asp:ListItem Text="NTU" Value="NTU" />
                                                <asp:ListItem Text="On Trial" Value="On Trial" />
                                            </asp:DropDownList>
                                        </div>

                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="col-sm-2">
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
                    <br />
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:Label runat="server" ID="Label1"></asp:Label>
                        </div>
                        <asp:Label ID="lblRecords" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="row">
                        <div class="col-lg-12 ">
                            <div class="table-responsive">
                                <asp:GridView ID="gvMembers" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover"
                                    AllowPaging="true" PageSize="25" AutoGenerateColumns="False" EmptyDataText="There are no Members to display." OnPageIndexChanging="gvMembers_PageIndexChanging1">
                                    <PagerStyle CssClass="pagination-ys" />
                                    <Columns>
                                         <asp:BoundField DataField="MemeberNumber" HeaderText="Policy No" ReadOnly="True" />
                                        <asp:BoundField DataField="IDNumber" HeaderText="ID No" ReadOnly="True" />
                                        <asp:BoundField DataField="FullNames" HeaderText="Member" ReadOnly="True" />
                                        <asp:BoundField DataField="EasyPayNo" HeaderText="EasyPayNo" ReadOnly="True" />
                                         <asp:BoundField DataField="CoverDate" HeaderText="Cover date" DataFormatString="{0:d}" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                       <asp:BoundField DataField="PolicyStatus" HeaderText="Policy Status" ReadOnly="True" />
                                         <%--<asp:HyperLinkField DataTextField="PolicyStatus" HeaderText="Policy Status" DataNavigateUrlFields="pkiMemberID,parlourid" DataNavigateUrlFormatString="~/Admin/ManageMembersPayment.aspx?Id={0}&ParlourId={1}" />--%>
                                       <%-- <asp:TemplateField HeaderText="Policy Status">
                                            <ItemTemplate>
                                                <asp:HyperLink runat="server" ID="hlPolicyStatus" Text='<%# (Convert.ToString(Eval("PolicyStatus"))=="1"?"Active":(Convert.ToString(Eval("PolicyStatus"))=="0"?"waiting":Eval("PolicyStatus"))) %>' NavigateUrl='<%# "~/Admin/ManageMembersPayment.aspx?Id="+ Eval("pkiMemberID") + "&ParlourId="+ Eval("parlourid")  %>'><i class="fa fa-edit"></i></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Actions">
                                            <ItemTemplate>
                                                <%if (this.HasEditRight)
                                                    { %>
                                               <%-- <asp:HyperLink runat="server" ToolTip='Click here to edit member' ID="hrLink" NavigateUrl='<%# "~/Admin/ManageMembersPayment.aspx?Id="+ Eval("pkiMemberID") + "&ParlourId="+ Eval("parlourid")  %>'><i class="fa fa-edit"></i></asp:HyperLink>
                                                &nbsp;--%>
           <asp:HyperLink runat="server" ToolTip='Click here to manage member payment - ' ID="HyperLink2" NavigateUrl='<%# String.Format("~/Admin/ManageMembersPayment.aspx?Id={0}&&ParlourId={1}", Eval("pkiMemberID"), Eval("parlourid")) %>'
text ="Make payment" Class="btn btn-w-m btn-primary"></asp:HyperLink>

                                                       <%-- <asp:HyperLink runat="server" ToolTip='Click here to manage member payment - ' ID="HyperLink1" NavigateUrl='<%# "~/Admin/ManageMembersPayment.aspx?Id="+ Eval("pkiMemberID") + "&ParlourId="+ Eval("parlourid")  %>'><input type="button" Class="btn btn-w-m btn-primary" value="Make Payment" /></asp:HyperLink>--%>
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
