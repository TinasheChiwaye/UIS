<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintStatement.aspx.cs" Inherits="Funeral.Web.Admin.PrintStatement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UIS Policy Administration</title>
    <style>
        .dash {
            border: 1px dashed #cccccc;
            border-collapse: collapse;
        }

        h4 {
            display: block;
            -webkit-margin-before: 5px;
            -webkit-margin-after: 5px;
            -webkit-margin-start: 0px;
            -webkit-margin-end: 0px;
            font-weight: bold;
        }
    </style>
    <script>
        function printDiv() {
            var printContents = document.getElementById('PrintDiv').innerHTML;
            var originalContents = document.body.innerHTML;
            document.body.innerHTML = printContents;
            window.print();
            document.body.innerHTML = originalContents;
        }
        window.onload = printDiv;
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="PrintDiv">
            <div class="row">
        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-title">
                  <h5>Policy Name :  <asp:Label ID="lblPolicy" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;->Payment History</h5>
                    <asp:HiddenField runat="server" ID="hdnLastpaymentDate" /> <br /><br />
                    <div class="table-responsive">
                        <asp:GridView ID="gvInvoices" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black">
                           
                            <Columns>
                                <asp:BoundField DataField="DatePaid" HeaderText="Date paid" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" >
<HeaderStyle CssClass="visible-lg"></HeaderStyle>

<ItemStyle CssClass="visible-lg"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="AmountPaid" HeaderText="Amount" ReadOnly="True" />
                                <asp:BoundField DataField="RecievedBy" HeaderText="Recieved by" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" >
<HeaderStyle CssClass="visible-lg"></HeaderStyle>

<ItemStyle CssClass="visible-lg"></ItemStyle>
                                </asp:BoundField>
                                 <asp:BoundField DataField="Notes" HeaderText="Months Paid & Notes" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" >
                               
<HeaderStyle CssClass="visible-lg"></HeaderStyle>

<ItemStyle CssClass="visible-lg"></ItemStyle>
                                </asp:BoundField>
                               
                            </Columns>
                            <FooterStyle BackColor="#CCCCCC" />
                            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                            <RowStyle BackColor="White" />
                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#808080" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#383838" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>

        </div>
    </form>
</body>
</html>
