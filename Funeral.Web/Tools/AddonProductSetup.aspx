<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AddonProductSetup.aspx.cs" Inherits="Funeral.Web.Tools.AddonProductSetup" %>

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
                    <asp:ValidationSummary runat="server" ID="vSummaryAddonSetup" ValidationGroup="AddonSetup" ForeColor="Red" />
                </div>
            </div>
        </div>

        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Addon Products setup</h5>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Products Name <em>*</em> </label>
                                <asp:TextBox MaxLength="25" runat="server" ID="txtAddonName" name="AddonName" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="AddonSetup" ControlToValidate="txtAddonName" ID="RequiredFieldValidator1" ForeColor="red" runat="server" ErrorMessage="Please enter Products Name"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label>Description <em>*</em> </label>
                                <asp:TextBox MaxLength="50" runat="server" ID="txtDescription" name="Description" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="AddonSetup" ControlToValidate="txtDescription" ID="RequiredFieldValidator2" ForeColor="red" runat="server" ErrorMessage="Please enter Description"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label>Start Date <em>*</em> </label>
                                <asp:TextBox MaxLength="25" runat="server" ID="txtStartDate" name="txtStartDate" type="text" class="form-control"></asp:TextBox>
<%--                                <asp:RequiredFieldValidator Display="None" ValidationGroup="AddonSetup" ControlToValidate="txtInceptionDate"" ID="RequiredFieldValidator5" ForeColor="red" runat="server" ErrorMessage="Please enter Inception Date"></asp:RequiredFieldValidator>--%>
                                <%--<asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator2" ValidationGroup="AddonSetup" runat="server" ControlToValidate="txtInceptionDate"" ErrorMessage="Please enter correct Inception Date"  />--%>
                            </div>
                            <div class="form-group">
                                <label>Cover Date <em>*</em> </label>
                                <asp:TextBox MaxLength="25" runat="server" ID="txtCoverDate" name="txtCoverDate" type="text" class="form-control"></asp:TextBox>
<%--                                <asp:RequiredFieldValidator Display="None" ValidationGroup="AddonSetup" ControlToValidate="txtInceptionDate"" ID="RequiredFieldValidator5" ForeColor="red" runat="server" ErrorMessage="Please enter Inception Date"></asp:RequiredFieldValidator>--%>
                                <%--<asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator2" ValidationGroup="AddonSetup" runat="server" ControlToValidate="txtInceptionDate"" ErrorMessage="Please enter correct Inception Date"  />--%>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Premium<em>*</em> </label>
                                <asp:TextBox MaxLength="25" runat="server" ID="txtPremium" name="Premium" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="AddonSetup" ControlToValidate="txtPremium" ID="RequiredFieldValidator3" ForeColor="red" runat="server" ErrorMessage="Please enter Premium"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator7" ValidationGroup="AddonSetup" runat="server" ControlToValidate="txtPremium" ErrorMessage="Premium Enter Only Number With 2 Desimal" ValidationExpression="((\d+)((\.\d{1,2})?))$" />
                            </div>

                             
                            <div class="form-group">
                                <label>Inception Date <em>*</em> </label>
                                <asp:TextBox MaxLength="25" runat="server" ID="txtInceptionDate" name="txtInceptionDate" type="text" class="form-control"></asp:TextBox>
<%--                                <asp:RequiredFieldValidator Display="None" ValidationGroup="AddonSetup" ControlToValidate="txtInceptionDate"" ID="RequiredFieldValidator5" ForeColor="red" runat="server" ErrorMessage="Please enter Inception Date"></asp:RequiredFieldValidator>--%>
                                <%--<asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator2" ValidationGroup="AddonSetup" runat="server" ControlToValidate="txtInceptionDate"" ErrorMessage="Please enter correct Inception Date"  />--%>
                            </div>

                            <div class="form-group">
                                <label>Cover <em>*</em> </label>
                                <asp:TextBox MaxLength="25" runat="server" ID="txtCover" name="Cover" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="AddonSetup" ControlToValidate="txtCover" ID="RequiredFieldValidator4" ForeColor="red" runat="server" ErrorMessage="Please enter Cover"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator1" ValidationGroup="AddonSetup" runat="server" ControlToValidate="txtCover" ErrorMessage="Cover Enter Only Number With 2 Desimal" ValidationExpression="((\d+)((\.\d{1,2})?))$" />
                            </div>
                             <div class="form-group">
                                <label>Waiting Period <em>*</em> </label>
                                <asp:TextBox MaxLength="25" runat="server" ID="txtWaitPeriod" name="txtWaitPeriod" type="text" class="form-control"></asp:TextBox>
                               <%-- <asp:RequiredFieldValidator Display="None" ValidationGroup="AddonSetup" ControlToValidate="txtCover" ID="RequiredFieldValidator5" ForeColor="red" runat="server" ErrorMessage="Please enter Cover"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator2" ValidationGroup="AddonSetup" runat="server" ControlToValidate="txtCover" ErrorMessage="Cover Enter Only Number With 2 Desimal" ValidationExpression="((\d+)((\.\d{1,2})?))$" />--%>
                            </div>

                              

                            <div class="form-group">
                                <div class="checkbox">
                                    <label>
                                        <asp:CheckBox ID="chkongoing" runat="server" Text="Is Product Ongoing" /></label>
                                </div>
                                <div class="checkbox">
                                    <label>
                                        <asp:CheckBox ID="chkLaybye" runat="server" Text="Is Product Laybye" /></label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-lg-12">
            <div class="form-group">
                <div class="btn-group">
                    <asp:Button class="btn btn-md btn-primary" Visible="true" ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                </div>
                <div class="btn-group">
                    <asp:Button runat="server" ID="btnSubmite" ValidationGroup="AddonSetup" CssClass="btn btn-md btn-primary" Text="Save" OnClick="btnSubmite_Click" />
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
                                    <h5>Addon Products List</h5>
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
                                                <asp:GridView ID="gvAddones" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover"
                                                    AutoGenerateColumns="False" EmptyDataText="There are no jobs to display."
                                                    OnRowCommand="gvAddones_RowCommand">
                                                    <PagerStyle CssClass="pagination-ys" />
                                                    <Columns>
                                                        <%--<asp:BoundField DataField="pkiProductID" HeaderText="ID" ReadOnly="True" />--%>
                                                        <asp:BoundField DataField="ProductName" HeaderText="ProductName Name" ReadOnly="True" />
                                                        <asp:BoundField DataField="ProductCost" HeaderText="Product Cost" ReadOnly="True" />
                                                        <asp:BoundField DataField="ProductDesc" HeaderText="Descriptions" ReadOnly="True" />
                                                        <asp:BoundField DataField="ModifiedUser" HeaderText="Modified User" ReadOnly="True" />
                                                        <asp:BoundField DataField="LastModified" HeaderText="Modified date" DataFormatString="{0:d}" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                        <asp:TemplateField HeaderText="Actions">
                                                            <ItemTemplate>
                                                                <% if (this.HasEditRight)
                                                                   {%>
                                                                <asp:LinkButton runat="server" ToolTip='Click here to Edit - ' ID="lbtnEdit" CommandArgument='<%#Eval("pkiProductID")%>' CommandName="EditAddon"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                <%} if (this.HasDeleteRight)
                                                                   { %>
                                                                <asp:LinkButton runat="server" ID="lbtnDelete" OnClientClick="return confirm('Are you sure you want to delete?')" CommandArgument='<%#Eval("pkiProductID")%>' CommandName="deleteAddon"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
    <script type="text/javascript">


      <%--  $(function () {

            $("#<%=txte_sdate.ClientID %>").datepicker({ format: 'dd MM yyyy' });

        });

        $(document).ready(function () {
            var currentTime = new Date();
            var startDateTo = new Date(currentTime.getFullYear(), currentTime.getMonth() + 1, 0);
            //set InceptionDates
<%--            $("#<%=txte_sdate.ClientID %>").datepicker();--%>
            $("#<%=txtCoverDate.ClientID %>").datepicker({ format: 'dd MM yyyy' });
            //$("#<%=txtInceptionDate.ClientID %>").val('<%=System.DateTime.Now.ToString("dd MMM yyyy")%>');
            </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptControl" runat="server">
    
</asp:Content>
