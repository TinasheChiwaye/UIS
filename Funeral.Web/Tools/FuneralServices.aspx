<%@ Page Title="Manage Service" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="FuneralServices.aspx.cs" Inherits="Funeral.Web.Tools.FuneralServices" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
            <div class="ibox ">
                <div class="ibox-title">
                    <div class="row">
                        <div class="col-lg-6">
                            <h5>Manage Services</h5>
                        </div>

                        <div class="col-lg-6">
                            <div class="pull-right">
                                <asp:DropDownList ID="ddlCompanyList" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ChangeCompanydata"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form-group">
                                <div class="input-group">
                                    <asp:ValidationSummary runat="server" ID="vSummaryFuneral" ValidationGroup="tab1" ForeColor="Red" />
                                </div>
                            </div>
                        </div>

                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Service Name  <em>*</em> </label>
                                <asp:TextBox runat="server" ID="txtServicename" name="name" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="tab1" ControlToValidate="txtServicename" ID="RequiredFieldValidator1" ForeColor="red" runat="server" ErrorMessage="Please enter Service Name"></asp:RequiredFieldValidator>

                            </div>
                            <div class="form-group">
                                <label>Service Cost <em>*</em> </label>
                                <asp:TextBox  runat="server" ID="txtServiceCost" name="name" type="text" class="form-control" onkeypress="return isDecimalNumber1(event,this);"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="tab1" ControlToValidate="txtServiceCost" ID="RequiredFieldValidator2" ForeColor="red" runat="server" ErrorMessage="Please enter Service Cost"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label>Vendor Name</label>
                                 <asp:DropDownList ID="ddlVendor" CssClass="form-control" runat="server" AutoPostBack="false">
                                <asp:ListItem Value="0">Select Vendor Name</asp:ListItem> 
                                     </asp:DropDownList>
                                <%--<asp:TextBox MaxLength="25" runat="server" ID="TextBox1" name="name" type="text" class="form-control" onkeypress="return isDecimalNumber1(event,this);"></asp:TextBox>--%>
                                
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Service Description  </label>
                                <asp:TextBox runat="server" ID="txtServiceDesc" name="name" type="text" class="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>QTY</label>
                                <asp:TextBox  runat="server" ID="txtQty" name="name" type="text" class="form-control" onkeypress="return isDecimalNumber1(event,this);"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Cost Of Sale</label>
                                <asp:TextBox  runat="server" ID="txtCostOfSale" name="name" type="text" class="form-control" onkeypress="return isDecimalNumber1(event,this);"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form-group">

                                <div class="btn-group">
                                    <asp:Button ID="btnAddService" CssClass="btn btn-sm btn-primary" runat="server" Text="Save My Service" ValidationGroup="tab1" OnClick="btnSaveFuneralService_Click" />
                                </div>

                                <div class="btn-group">
                                    <asp:Button ID="btnReset" CssClass="btn btn-sm btn-primary" Enabled="false" runat="server" Text="Reset Field" OnClick="btn_ClearControl" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="ibox ">
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
                                <label>Search Service :</label>

                                <asp:Panel ID="pnlSearch" CssClass="input-group" DefaultButton="btnSearch" runat="server">
                                    <asp:TextBox runat="server" ID="txtKeyword" CssClass="form-control" placeholder="Search by keyword"></asp:TextBox>
                                    <span class="input-group-btn">&nbsp;<asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-w-m btn-primary" OnClick="btnSearch_Click" />
                                        <br />
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
                                <asp:GridView ID="gvFuneralServiceList" OnRowDataBound="gvFuneralServiceList_RowDataBound" OnPageIndexChanging="gvFuneralService_PageIndexChanging" OnRowCommand="gvFuneralService_RowCommand" OnSorting="gvFuneralService_Sorting" AllowSorting="true" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" AllowPaging="True" PageSize="25" EmptyDataText="There are no Services  added.">

                                    <PagerStyle CssClass="pagination-ys" />
                                    <Columns>

                                        <asp:BoundField DataField="ServiceName" HeaderText="Service Name" SortExpression="ServiceName" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                        <asp:BoundField DataField="ServiceDesc" HeaderText="Service Description" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                        <asp:BoundField DataField="ServiceCost" HeaderText="Service Cost" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                        <asp:BoundField DataField="QTY" HeaderText="QTY" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                        <asp:BoundField DataField="VendorName" HeaderText="VendorName" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                        <asp:BoundField DataField="CostOfSale" HeaderText="CostOfSale" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />

                                        <asp:TemplateField HeaderText="Actions">
                                            <ItemTemplate>
                                                <% if (this.HasEditRight)
                                                   {%>
                                                <asp:LinkButton runat="server" ID="lbtnEditDependant" ToolTip="Click here To Edite Funeral" CommandArgument='<%#Eval("pkiServiceID") %>' CommandName="EditFuneralService"><i class="fa fa-edit"></i></asp:LinkButton>
                                                &nbsp;
                                           <%} if (this.HasDeleteRight)
                                               {%>
                                                <asp:LinkButton runat="server" ID="lbtnDelete" OnClientClick="return confirm('Are you sure you want to delete?')" CommandArgument='<%#Eval("pkiServiceID")%>' CommandName="deleteFuneral"><i class="fa fa-trash" aria-hidden="true"></i></asp:LinkButton>
                                                <% }%>
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

    <script type="text/javascript" language="javascript">

        var count = 0;
        function isDecimalNumber1(evt, c) {
            count = count + 1;
            var charCode = (evt.which) ? evt.which : event.keyCode;
            var dot1 = c.value.indexOf('.');
            var dot2 = c.value.lastIndexOf('.');
            if (count > 10 && dot1 == -1) {
                c.value = "";
                count = 0;
            }
            if (dot1 > 6) {
                c.value = "";
            }
            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            else if (charCode == 46 && (dot1 == dot2) && dot1 != -1 && dot2 != -1)
                return false;

            return true;
        }
    </script>
</asp:Content>
