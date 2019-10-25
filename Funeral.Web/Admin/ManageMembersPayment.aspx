<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ManageMembersPayment.aspx.cs" Inherits="Funeral.Web.Admin.ManageMembersPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <%--<asp:Button runat="server" ID="btnReunstate" Width="95%" CssClass="btn btn-md btn-primary" Text="Reinstate Policy" OnClick="btnReunstate_Click" />--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="hdnTotalPremium" />
    <asp:HiddenField runat="server" ID="hdnParlourid" />
    <asp:HiddenField runat="server" ID="hdnAmount" />
     <asp:HiddenField runat="server" ID="hdnMemberId" />
    <asp:HiddenField runat="server" ID="hdnOldStatus" />
   
    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-lg-6">
                    <asp:Label runat="server" ID="lblMessage"></asp:Label>
                </div>
            </div>
            <div class="ibox ">
                <div class="row">
                    <div class="col-lg-6">
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h5>Payment Details</h5>
                            </div>
                            <div class="ibox-content">
                                <div class="form-group">
                                    <label>Member No  <em>*</em> </label>
                                    <asp:TextBox MaxLength="255" runat="server" ID="txtMemberNo" name="name" type="text" class="form-control" AutoPostBack="true" OnTextChanged="txtMemberNo_TextChanged"></asp:TextBox>
                                </div>
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label>Member    </label>
                                            <asp:TextBox MaxLength="255" runat="server" ReadOnly="true" ID="txtMember" name="name" type="text" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label>Received By  </label>
                                            <asp:TextBox MaxLength="255" runat="server" ReadOnly="true" ID="txtReceivedBy" name="name" type="text" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label>Amount</label>
                                            <asp:TextBox MaxLength="255" CssClass="form-control" runat="server" ID="txtAmount" name="name" type="text" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label>Method</label>
                                            <asp:DropDownList runat="server" ID="ddlMethod" class="form-control m-b">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">Cash</asp:ListItem>
                                                <asp:ListItem Value="2">Easy Pay</asp:ListItem>
                                                <asp:ListItem Value="3">Debit Order</asp:ListItem>
                                                <asp:ListItem Value="4">Bank Depost</asp:ListItem>
                                                <asp:ListItem Value="5">EFT</asp:ListItem>
                                                <asp:ListItem Value="6">Other</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label>Date</label>
                                            <asp:TextBox MaxLength="255" runat="server" ReadOnly="true" ID="txtNextPaymentDate" name="name" type="text" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label>No of Months</label>
                                            <asp:DropDownList runat="server" CssClass="form-control m-b" ID="ddlNoOfMonths" class="form-control m-b">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                <asp:ListItem Value="6">6</asp:ListItem>
                                                <asp:ListItem Value="7">7</asp:ListItem>
                                                <asp:ListItem Value="8">8</asp:ListItem>
                                                <asp:ListItem Value="9">9</asp:ListItem>
                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                <asp:ListItem Value="11">11</asp:ListItem>
                                                <asp:ListItem Value="12">12</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ValidationGroup="tab1" ControlToValidate="ddlNoOfMonths" InitialValue="0"
                                                ID="rfvNoOfMonth" ForeColor="red" runat="server" ErrorMessage="Select Number of Months"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label>Month Paid<em>*</em>  </label>
                                            <asp:TextBox TextMode="MultiLine" CssClass="form-control" runat="server" ID="txtMohthPaid" name="name" type="text" class="form-control"></asp:TextBox>
                                        </div>

                                        <div class="form-group">
                                            <asp:Button runat="server" ID="btnPay" CssClass="btn btn-md btn-primary" Text="Add Payment" Width="95%" ValidationGroup="tab1" OnClick="btnPay_Click" />
                                        </div>
                                        
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <asp:Button runat="server" ID="btnPrint" CssClass="btn btn-md btn-primary" Text="Print" Width="95%" OnClick="btnPrint_Click" />
                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-lg-6">
                                        
                                        <div class="form-group">
                                            <asp:Button runat="server" ID="btnReunstate" Width="95%" CssClass="btn btn-md btn-primary" Text="Reinstate Policy" OnClick="btnReunstate_Click" OnClientClick="ConfirmReInstate()" />
                                            
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <asp:Button runat="server" ID="btnRejoin" Width="95%" CssClass="btn btn-md btn-primary" Text="Rejoin Policy" OnClick="btnRejoin_Click" OnClientClick="ConfirmReJoin()" />
                                            <%--<script src="../Scripts/bootstrap-confirmation.min.js"></script>--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h5>Member Plan Details</h5>
                            </div>
                            <div class="ibox-content">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label>Plan  <em>*</em> </label>
                                            <asp:TextBox MaxLength="255" runat="server" ID="txtPlan" ReadOnly="true" name="name" type="text" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label>Premium  <em>*</em> </label>
                                            <asp:TextBox MaxLength="255" runat="server" ID="txtPremium" ReadOnly="true" name="name" type="text" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label>Spouse</label>
                                            <%--<asp:DropDownList runat="server" ID="ddlSpouse" class="form-control m-b" Enabled="false"></asp:DropDownList>--%>
                                            <asp:TextBox runat="server" ID="txtSpouse" ReadOnly="true" name="name" type="text" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label>Children</label>
                                            <%--<asp:DropDownList runat="server" ID="ddlChildren" class="form-control m-b" Enabled="false"></asp:DropDownList>--%>
                                            <asp:TextBox runat="server" ID="txtChildren" ReadOnly="true" name="name" type="text" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label>Adults</label>
                                            <%--<asp:DropDownList runat="server" ID="ddlAdults" class="form-control m-b" Enabled="false"></asp:DropDownList>--%>
                                            <asp:TextBox runat="server" ID="txtAdults" ReadOnly="true" name="name" type="text" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label>Waiting Period</label>
                                            <%--<asp:DropDownList runat="server" ID="ddlWaitingPerios" class="form-control m-b" Enabled="false"></asp:DropDownList>--%>
                                            <asp:TextBox runat="server" ID="txtWaitingPeriod" ReadOnly="true" name="name" type="text" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label>Cover</label>
                                            <asp:TextBox MaxLength="255" runat="server" ID="txtCover" ReadOnly="true" name="name" type="text" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label>Total Premium</label>
                                            <asp:TextBox runat="server" ID="txtTotalPremium" ReadOnly="true" name="name" type="text" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label>Member Status</label>
                                            <asp:TextBox MaxLength="255" runat="server" ID="txtMemberStauts" ReadOnly="true" name="name" type="text" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label>Policy Balance</label>
                                            <asp:TextBox MaxLength="255" runat="server" ID="txtPolicyBalance" ReadOnly="true" name="name" type="text" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label>Policy Laps</label>
                                            <asp:TextBox MaxLength="255" runat="server" ID="txtPolicyLaps" ReadOnly="true" name="name" type="text" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label>Month Owing</label>
                                            <asp:TextBox MaxLength="255" runat="server" ID="txtMonthOwing" ReadOnly="true" name="name" type="text" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label>Joining Fee</label>
                                            <asp:TextBox MaxLength="255" runat="server" ID="txtJoiningFee" ReadOnly="true" name="name" type="text" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label>Late Penalty  <em>*</em> </label>
                                            <asp:TextBox MaxLength="255" runat="server" ID="txtLatePenalty" ReadOnly="true" name="name" type="text" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3">
                                        <div class="form-group">
                                               <asp:LinkButton runat="server" CssClass="btn btn-md btn-primary" ID="lbtnStatement" OnClick="lbtnStatement_Click" ToolTip="Click here Print " >Statement</asp:LinkButton>
                                     
                                            <%--<asp:Button runat="server" ID="Button4" CssClass="btn btn-md btn-primary" Text="Statement" />
                                     --%>   </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Payment History</h5>
                    <asp:HiddenField runat="server" ID="hdnLastpaymentDate" /> <br /><br />
                    <div class="table-responsive">
                        <asp:GridView ID="gvInvoices" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover"
                            AllowPaging="true" PageSize="25" OnPageIndexChanging="gvInvoices_PageIndexChanging" AutoGenerateColumns="False" EmptyDataText="There are no payment history."
                            OnRowCommand="gvInvoices_RowCommand" OnRowDataBound="gvInvoices_RowDataBound">
                            <PagerStyle CssClass="pagination-ys" />
                            <Columns>
                                <asp:BoundField DataField="DatePaid" HeaderText="Date paid" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                <asp:BoundField DataField="AmountPaid" HeaderText="Amount" ReadOnly="True" />
                                <asp:BoundField DataField="RecievedBy" HeaderText="Recieved by" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                <%--<asp:BoundField DataField="PaidBy" HeaderText="Paid By" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />--%>
                                <asp:BoundField DataField="Notes" HeaderText="Months Paid & Notes" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <%if(this.HasReadRight){ %>
                                        &nbsp;
                                               <asp:LinkButton runat="server" ID="LinkButton1" ToolTip="Click here Print " CommandArgument='<%#Eval("InvoiceID") %>' CommandName="PrintPremium"><i class="glyphicon glyphicon-print"></i></asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="LinkButton2" ToolTip="Click here Print " CommandArgument='<%#Eval("InvoiceID") %>' CommandName="PrintFullPremium"><i class="glyphicon glyphicon-export"></i></asp:LinkButton>
                                        <%} %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <% if(this.HasReversalPayment) {%>
                                        <asp:Button runat="server" CommandName="PaymentReversal" OnClientClick="return confirm('You are about to make a REVERSAL PAYMENT. Continue?')" ID="btnbtnreversal" CommandArgument='<%#Eval("InvoiceID") %>' CssClass="btn btn-w-m btn-primary" Text="Reversal Payment" />
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptControl" runat="server">
    <%--<script src="../Scripts/bootstrap-confirmation.min.js"></script>--%>
    <script type="text/javascript">

        function ConfirmReInstate() {
            var del;
            del = confirm("Would you like to Reinstate this policy?");
            if (del) {
                return false;
            }
        }

        function ConfirmReJoin() {
            var del;
            del = confirm("Would you like to Rejoin this policy?");
            if (del) {
                return false;
            }
        }

        $(document).ready(function () {
            $("#<%=txtAmount.ClientID %>").change(function () {
                $("#<%=hdnAmount.ClientID %>").val($("#<%=txtAmount.ClientID %>").val());
            })
          <%--  $("#<%=btnReunstate.ClientID%>").confirmation();
            $("#<%=btnRejoin.ClientID%>").confirmation();--%>
            $("#<%=txtNextPaymentDate.ClientID %>").datepicker({ format: 'dd MM yyyy' });

            $("#<%=ddlNoOfMonths.ClientID%>").change(function () {
                var NoOfMonths = parseInt($("#<%=ddlNoOfMonths.ClientID%>").val());
                var Premium = $("#<%=txtTotalPremium.ClientID%>").val().replace("<%=Currency.Trim()%> ", "");
                var LatePanelty = $("#<%=txtLatePenalty.ClientID%>").val().replace("<%=Currency.Trim()%> ", "");

                <%--$("#<%=txtAmount.ClientID %>").val("R" + " " + parseInt(NoOfMonths * Premium + LatePanelty).toFixed(2));--%>
                $.ajax({
                    type: "POST",
                    url: '<%=ResolveUrl("~/admin/ManageMembersPayment.aspx/CalculateAmount")%>',
                    data: '{"noOfMonths":"' + NoOfMonths + '","TotalPremieum":"' + Premium + '","LatePanelty":"' + LatePanelty + '","NextDate":"' + $("#<%=hdnLastpaymentDate.ClientID%>").val() + '"}',
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        var stringData = data.d.split('~');
                        $("#<%=txtAmount.ClientID %>").val(stringData[0]);
                        $("#<%=hdnAmount.ClientID %>").val(stringData[2]);
                        $("#<%=txtMohthPaid.ClientID%>").val(stringData[1]);
                    },
                    failure: function (response) {
                        alert(response.d);
                    }
                });
            });
        });
    </script>
</asp:Content>
