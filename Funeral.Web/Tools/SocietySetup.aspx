<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="SocietySetup.aspx.cs" Inherits="Funeral.Web.Tools.SocietySetup" %>

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
                    <asp:ValidationSummary runat="server" ID="vSummarySocietySetup" ValidationGroup="SocietySetup" ForeColor="Red" />
                </div>
            </div>
        </div>
        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Society setup</h5>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Society Name <em>*</em> </label>
                                <asp:TextBox MaxLength="25" runat="server" ID="txtSocietyName" name="SocietyName" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="SocietySetup" ControlToValidate="txtSocietyName" ID="RequiredFieldValidator1" ForeColor="red" runat="server" ErrorMessage="Please enter Society Name"></asp:RequiredFieldValidator>
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
                                    <asp:Button runat="server" ID="btnSubmite" ValidationGroup="SocietySetup" CssClass="btn btn-md btn-primary" Text="Save" OnClick="btnSubmite_Click" />
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
                    <h5>Societies List</h5>
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
                                <asp:GridView ID="gvSocietyes" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover"
                                    AutoGenerateColumns="False" EmptyDataText="There are no jobs to display."
                                    OnRowCommand="gvSocietyes_RowCommand">
                                    <PagerStyle CssClass="pagination-ys" />
                                    <Columns>
                                        <%-- <asp:BoundField DataField="pkiSocietyID" HeaderText="ID" ReadOnly="True" />--%>
                                        <asp:BoundField DataField="SocietyName" HeaderText="Society Name" ReadOnly="True" />
                                        <asp:BoundField DataField="ModifiedUser" HeaderText="Modified User" ReadOnly="True" />
                                        <asp:BoundField DataField="LastModified" HeaderText="Modified date" DataFormatString="{0:d}" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                        <asp:TemplateField HeaderText="Actions">
                                            <ItemTemplate>
                                                <% if (this.HasEditRight)
                                                   {%>
                                                <asp:LinkButton runat="server" ToolTip='Click here to Edit - ' ID="lbtnEdit" CommandArgument='<%#Eval("pkiSocietyID")%>' CommandName="EditSociety"><i class="fa fa-edit"></i></asp:LinkButton>
                                                <%} if (this.HasDeleteRight)
                                                   { %>
                                                <asp:LinkButton runat="server" ID="lbtnDelete" OnClientClick="return confirm('Are you sure you want to delete?')" CommandArgument='<%#Eval("pkiSocietyID")%>' CommandName="deleteSociety"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
