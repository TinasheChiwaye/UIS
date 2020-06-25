<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Admin.Master" CodeBehind="DebitOrder.aspx.cs" Inherits="Funeral.Web.Admin.DebitOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="../Content/plugins/datapicker/bootstrap-timepicker.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="scriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">

        <%--<div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>Generate Debit Order</h5>
                    </div>
                    <div class="ibox-content">
                        <div class="row">
                            <div class="col-lg-6">
                                <div class="form-group">
                                    <label>Date From:</label>
                                    <asp:TextBox runat="server" class="form-control datepicker" ID="TxtFromDatePicker" type="Text" placeholder="DD/MM/YYYY" MaxLength="10"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>Debit Order Instruction</label>
                                    <asp:DropDownList ID="ddlDebitOrderInstruction" runat="server" DataTextField="Instruction" DataValueField="InstructionID" CssClass="form-control m-b"></asp:DropDownList>
                                </div>
                               <div class="form-group">
                                    <label>DebitOrder Status</label>
                                    <asp:DropDownList ID="ddlDebitOrderStatus" runat="server" DataTextField="Status" DataValueField="pkiDebitStatusID" CssClass="form-control m-b"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>Payment Gateway</label>
                                    
                                    <asp:RadioButton ID="btnNetcash" GroupName="Gender" runat="server" Text="Netcash" />
                                    <asp:RadioButton ID="btnMyGate" GroupName="Gender" runat="server" Text="MyGate" />

                                </div>

                            </div>
                            <div class="col-lg-6">
                                <div class="form-group">
                                    <label>Date To:</label>
                                    <asp:TextBox runat="server" class="form-control datepicker" ID="TextToDatePicker" type="Text" placeholder="DD/MM/YYYY"></asp:TextBox>
                                </div>
                                 <div class="form-group">
                                    <label>Payment Gateway</label>
                                    <asp:DropDownList ID="ddlPaymentGateway" runat="server" DataTextField="GatewayType" DataValueField="GatewayID" CssClass="form-control m-b"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>Batch Date</label>
                                    <asp:TextBox runat="server" class="form-control datepicker" ID="TextBatchDate" type="Text" placeholder="DD/MM/YYYY"></asp:TextBox>
                                </div>


                               
                            </div>
                            <div class="col-lg-12">
                                <div class="form-group">
                                    <div class="btn-group">
                                        <asp:Button class="btn btn-sm btn-primary" Visible="true" ID="btnDebitOrderSearch" runat="server" Text="Search"  />
                                    </div>
                                    <div class="btn-group">
                                        <asp:Button runat="server" ID="btnClear"  CssClass="btn btn-sm btn-primary" Text="Clear" OnClick="btn_Clear"  />
                                    </div>
                                     <div class="btn-group">
                                        <asp:Button runat="server" ID="btnDownloadStm"  CssClass="btn btn-sm btn-primary" Text="Download Statement"  />
                                    </div>
                                     <div class="btn-group">
                                        <asp:Button runat="server" ID="btnEasyPayCol"  CssClass="btn btn-sm btn-primary" Text="EasyPay Collection"  />
                                    </div>
                                     <div class="btn-group">
                                        <asp:Button runat="server" ID="btnDebitOrderSearch"  CssClass="btn btn-sm btn-primary" Text="Search"  />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>--%>
        <%--<div class="row">
            <div class="col-lg-12 ">
                <div class="table-responsive">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                        OnPageIndexChanging="OnPageIndexChanging" PageSize="10">
                        <Columns>
                            <asp:BoundField ItemStyle-Width="150px" DataField="CustomerID" HeaderText="Customer ID" />
                            <asp:BoundField ItemStyle-Width="150px" DataField="ContactName" HeaderText="Contact Name" />
                            <asp:BoundField ItemStyle-Width="150px" DataField="City" HeaderText="City" />
                            <asp:BoundField ItemStyle-Width="150px" DataField="Country" HeaderText="Country" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>--%>
        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Debit Order Batches</h5>
                    <div class="ibox-tools">
                        <a class="collapse-link">
                            <i class="fa fa-chevron-up"></i>
                        </a>
                        <%--<a class="dropdown-toggle">
                            <i class="fa fa-wrench"></i>
                        </a>
                        <a class="close-link">
                            <i class="fa fa-times"></i>
                        </a>--%>
                    </div>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <div class="input-group">
                                    <label>Show</label>
                                    <asp:DropDownList AutoPostBack="true" ID="ddlBatchPageSize" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
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
                                <label>Search Batch :</label>

                                <asp:Panel ID="Panel1" CssClass="input-group" DefaultButton="btnSearch" runat="server">
                                    <asp:TextBox runat="server" ID="TextBox1" MaxLength="50" CssClass="form-control" placeholder="Search by keyword"></asp:TextBox>
                                    <span class="input-group-btn">&nbsp;<asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn btn-w-m btn-primary" OnClick="btnSearch_Click" />
                                        <br />
                                    </span>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:Label runat="server" ID="Label2"></asp:Label>
                        </div>
                        <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="row">
                        <div class="col-lg-12 ">
                            <div class="table-responsive">
                                <asp:GridView ID="gvDebitBatches" OnRowDataBound="gvDebitBatches_RowDataBound" OnPageIndexChanging="gvDebitBatches_PageIndexChanging" OnSorting="gvDebitBatches_Sorting" AllowSorting="True" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" AllowPaging="True" PageSize="25" EmptyDataText="There are no Batches  added." OnRowCommand="gvDebitBatches_RowCommand" OnSelectedIndexChanged="gvDebitBatches_SelectedIndexChanged">
                                    <PagerStyle CssClass="pagination-ys" />
                                    <Columns>                         
                                        
                                        <asp:BoundField DataField="pkiDebitBatch" HeaderText="pkiDebitBatch" SortExpression="pkiDebitBatch" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" Visible="False">
                                            <HeaderStyle CssClass="visible-lg"></HeaderStyle>

                                           <ItemStyle CssClass="visible-lg"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ActionDate" HeaderText="Action Date" SortExpression="ActionDate" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" DataFormatString="{0:dd/M/yyy}">
                                            <HeaderStyle CssClass="visible-lg"></HeaderStyle>

                                            <ItemStyle CssClass="visible-lg"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ServiceType" HeaderText="Service Type" SortExpression="ServiceType" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg">
                                            <HeaderStyle CssClass="visible-lg"></HeaderStyle>

                                            <ItemStyle CssClass="visible-lg"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="BatchName" HeaderText="Batch Name" SortExpression="BatchName" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg">
                                            <HeaderStyle CssClass="visible-lg"></HeaderStyle>

                                            <ItemStyle CssClass="visible-lg"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Volume" HeaderText="Volume" SortExpression="Volume" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg">
                                            <HeaderStyle CssClass="visible-lg"></HeaderStyle>

                                            <ItemStyle CssClass="visible-lg"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg">
                                            <HeaderStyle CssClass="visible-lg"></HeaderStyle>

                                            <ItemStyle CssClass="visible-lg"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="UnpaidValue" HeaderText="Unpaid Value" SortExpression="ActionDate" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg">
                                            <HeaderStyle CssClass="visible-lg"></HeaderStyle>

                                            <ItemStyle CssClass="visible-lg"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="UnpaidVolumes" HeaderText="Unpaid Volumes" SortExpression="UnpaidVolumes" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg">
                                            <HeaderStyle CssClass="visible-lg"></HeaderStyle>

                                            <ItemStyle CssClass="visible-lg"></ItemStyle>
                                        </asp:BoundField>
                                        <%--<asp:BoundField DataField="DateOfFuneral" HeaderText="DateOfFuneral" SortExpression="DateOfFuneral" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" DataFormatString="{0:dd/M/yyy}" />--%>
                                        <asp:TemplateField HeaderText="Actions">
                                            <ItemTemplate>
                                                <asp:Button runat="server" ID="btnDebitOrderSearch" CssClass="btn btn-sm btn-primary" Text="Download" CommandArgument='<%#Eval("pkiDebitBatch")%>' CommandName="DownloadTransactions"  />
                                                &nbsp
                                                <asp:Button runat="server" ID="btnViewBatch" CssClass="btn btn-sm btn-primary" Text="View"   CommandArgument='<%#Eval("pkiDebitBatch")%>' CommandName="ViewTransactions" />
                                                
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
        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Debit Order Transactions</h5>
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
                                <label>Search Transaction :</label>

                                <asp:Panel ID="pnlSearch" CssClass="input-group" DefaultButton="btnSearch" runat="server">
                                    <asp:TextBox runat="server" ID="txtKeyword" MaxLength="50" CssClass="form-control" placeholder="Search by keyword"></asp:TextBox>
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
                                <asp:GridView ID="gvDebitTransactionsList" OnRowDataBound="gvDebitTransactionsList_RowDataBound" OnPageIndexChanging="gvDebitTransactionsList_PageIndexChanging" OnSorting="gvDebitTransactionsList_Sorting" AllowSorting="true" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" AllowPaging="True" PageSize="25" Visible="false" EmptyDataText="There are no Funeral  added.">

                                    <PagerStyle CssClass="pagination-ys" />
                                    <Columns>

                                        <%--<asp:BoundField DataField="SequenceNo" HeaderText="Sequence No" SortExpression="FullNames" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />--%>
                                        <asp:BoundField DataField="BranchCode" HeaderText="Branch Code" SortExpression="Surname" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                        <asp:BoundField DataField="AccountType" HeaderText="Account Type" SortExpression="IDNumber" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                        <asp:BoundField DataField="AccountNo" HeaderText="Account No" SortExpression="IDNumber" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                        <asp:BoundField DataField="DebitAmount" HeaderText="Debit Amount" SortExpression="IDNumber" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                        <asp:BoundField DataField="DebitDate" HeaderText="Debit Date" SortExpression="DateOfDeath" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" DataFormatString="{0:dd/M/yyy}" />
                                        <asp:BoundField DataField="BatchAmount" HeaderText="Batch Amount" SortExpression="IDNumber" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                        <asp:BoundField DataField="Reference" HeaderText="Reference" SortExpression="IDNumber" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                        <asp:BoundField DataField="AccountHolder" HeaderText="Account Holder" SortExpression="IDNumber" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                        <asp:BoundField DataField="CellNotification" HeaderText="Cell Notification" SortExpression="IDNumber" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                        <asp:BoundField DataField="EmailNotification" HeaderText="Email Notification" SortExpression="IDNumber" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                        <asp:BoundField DataField="TransactionRefNo" HeaderText="Transaction Ref No" SortExpression="IDNumber" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                        <asp:BoundField DataField="Tel" HeaderText="Tel" SortExpression="Tel" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                        <asp:BoundField DataField="PolicyNo" HeaderText="PolicyNo" SortExpression="PolicyNo" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                        <asp:TemplateField HeaderText="Actions">
                                            <ItemTemplate>
                                                <%--<% if(this.HasEditRight) {%>--%>
                                                <asp:Button runat="server" ID="btnAuthorise" CssClass="btn btn-sm btn-primary" Text="Authorise" />
                                                &nbsp
                                                <asp:Button runat="server" ID="btnCancel" CssClass="btn btn-sm btn-primary" Text="Cancel" />
                                                &nbsp
                                                <asp:Button runat="server" ID="btnReSubmit" CssClass="btn btn-sm btn-primary" Text="Re-Submit" />
                                                <%--&nbsp;--%>
                                                <%-- <%}if(this.HasDeleteRight){ %>
											   <asp:LinkButton runat="server" ID="lbtnDeleteFunral" ToolTip="Click here To Delete Funeral" OnClientClick="return confirm('Are you sure you want to delete it?')" CommandArgument='<%#Eval("pkiTransactionID") %>' CommandName="DeleteFuneral"><i class="fa fa-trash"></i></asp:LinkButton>
                                                &nbsp;--%>
                                                <%--<%}if(this.HasReadRight){ %>
											   <asp:LinkButton runat="server" ID="lbPrintFunral" ToolTip="Click here Print " CommandArgument='<%#Eval("pkiTransactionID") %>' CommandName="PrintFuneral"><i class="fa fa-search"></i></asp:LinkButton>--%>
                                                <%--<%} %>--%>
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
        function pageLoad(sender, args) {
            $(".datepicker").datepicker({ format: 'dd MM yyyy', autoclose: true });
            $(".datepicker").on('changeDate', function (ev) {
                $(this).datepicker('hide');
            })
        }


        function DownoadReport(GetParlourId, referenceNumber) {
            var GetUrl = "@Url.Action("DownloadReportPartial","SchemaPayment",new {ParlourId="GetParlourId",ReferenceNumber="getreferenceNumber"})".replace("GetParlourId", GetParlourId).replace("getreferenceNumber", referenceNumber).replace("amp;", "");;
            jQuery.ajax({
                url: GetUrl,
                contentType: 'application/json; charset=utf-8',
                datatype: 'json',
                type: 'GET',
                cache: false,
            }).done(function (result) {
                window.location = GetUrl;
            });
        }
    </script>
</asp:Content>

