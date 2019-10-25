<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlTombstonePaymentHistory.ascx.cs" Inherits="Funeral.Web.UserControl.ctrlTombstonePaymentHistory" %>
<div class="col-lg-12">
            <div class="table-responsive">
                <asp:GridView ID="gvInvoices" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover"
                    AllowPaging="true" PageSize="25" OnPageIndexChanging="gvInvoices_PageIndexChanging" AutoGenerateColumns="False" EmptyDataText="There are no payment history."
                    OnRowCommand="gvInvoices_RowCommand">
                    <PagerStyle CssClass="pagination-ys" />
                    <Columns>
                        <asp:BoundField DataField="DatePaid" HeaderText="Date paid" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                        <asp:BoundField DataField="AmountPaid" DataFormatString="{0:n}" HeaderText="Amount" ReadOnly="True" />
                        <asp:BoundField DataField="RecievedBy" HeaderText="Recieved by" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />                        
                        <asp:BoundField DataField="Notes" HeaderText="Months Paid & Notes" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                &nbsp;
                                 <asp:LinkButton runat="server" ID="LinkButton1" ToolTip="Click here Print " CommandArgument='<%#Eval("InvoiceID") %>' CommandName="PrintPremium"><i class="glyphicon glyphicon-print"></i></asp:LinkButton>
                                <%--<asp:LinkButton runat="server" ID="LinkButton2" ToolTip="Click here Print " CommandArgument='<%#Eval("InvoiceID") %>' CommandName="PrintFullPremium"><i class="glyphicon glyphicon-export"></i></asp:LinkButton>--%>
                            </ItemTemplate>
                        </asp:TemplateField>                     
                    </Columns>
                </asp:GridView>
            </div>
</div>