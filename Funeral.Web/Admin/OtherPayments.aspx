<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="OtherPayments.aspx.cs" Inherits="Funeral.Web.Admin.OtherPayments" %>

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


                        <div class="form-group">
                            <div class="col-sm-10">
                                <label>Search Members:</label>
                                <div class="row">
                                    <asp:Panel ID="pnlSearch" CssClass="input-group" DefaultButton="btnSearch" runat="server">
                                        <div class="col-md-4">
                                            <asp:TextBox runat="server" ID="txtPolicyNo" MaxLength="50" CssClass="form-control" placeholder="Search by Policy No"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox runat="server" ID="txtIDNo" MaxLength="50" CssClass="form-control" placeholder="Search by ID No"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-w-m btn-primary" OnClick="btnSearch_Click" />
                                        </div>
                                    </asp:Panel>
                                </div>
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
                                        
                                        <asp:TemplateField HeaderText="Actions">
                                            <ItemTemplate>
                                                <%if (this.HasEditRight)
                                                  { %>
                                               <%-- <asp:HyperLink runat="server" ToolTip='Click here to edit member ' ID="hrLink" NavigateUrl='<%# "~/Admin/ManageMembersPayment.aspx?Id="+ Eval("pkiMemberID") + "&ParlourId="+ Eval("parlourid")  %>'><i class="fa fa-edit"></i></asp:HyperLink>
                                                &nbsp;  --%>                                                      
                                                         <asp:HyperLink runat="server" ToolTip='Click here to manage member payment ' ID="HyperLink2" NavigateUrl='<%# "~/Admin/OtherPayment.aspx?Id="+ Eval("pkiMemberID") + "&ParlourId="+ Eval("parlourid")  %>'><input type="button" Class="btn btn-w-m btn-primary" value="Other Payment" /></asp:HyperLink>
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
