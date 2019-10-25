<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintPaymentReceipt.aspx.cs" Inherits="Funeral.Web.Admin.PrintPaymentReceipt" %>

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
            <div id="HideArea" runat="server">

                <table border="0" style="width: 100%; font-family: 'Times New Roman';font-size:10px;" cellpadding="2" cellspacing="0">
                    <tr>
                        <th>
                            <h4 align="center">
                                <asp:Literal ID="ltrHead" runat="server"></asp:Literal></h4>
                        </th>
                    </tr>
                    <tr>
                        <th>
                            <h5 align="center" style="font-weight:bold;">
                                <asp:Literal ID="ltrParlourname" runat="server"></asp:Literal></h5>
                        </th>
                    </tr>
                    <tr>
                        <th class="dash"></th>
                    </tr>
                    <tr>
                       
                        <td style="font-weight:bold">
                            <asp:Label ID="lblReceiptNumber" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="font-weight:bold">
                            <asp:Label ID="lbladd1" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="font-weight:bold">
                            <asp:Label ID="lbladd2" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="font-weight:bold">
                            <asp:Label ID="lbladd3" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="font-weight:bold">
                            <asp:Label ID="lbladd4" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="font-weight:bold">
                            <asp:Label ID="lblfpsnub" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="font-weight:bold">
                            <asp:Label ID="lblTelCell" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="dash"></td>
                    </tr>
                    <tr>
                        <td class="dash"></td>
                    </tr>
                </table>
            </div>
            <table border="0" style="width: 100%; font-family: 'Times New Roman'; font-size:10px;" cellpadding="2" cellspacing="0">
                <tr>
                    <td colspan="2">
                        <h4 align="center">
                            <asp:Literal ID="tableTitle" runat="server"></asp:Literal></h4>
                    </td>
                </tr>
<%--                <tr>
                    <td>Receipt Nr...</td>
                    <td>
                        <asp:Label ID="lblreceipt" runat="server"></asp:Label></td>
                </tr>--%>
<%--                <tr>
                    <td style="width: 108px">Brnach Name...</td>
                    <td>
                        <asp:Label ID="lblBranchname" runat="server"></asp:Label></td>
                </tr>--%>
                <tr>
                    <td id="ReceiptId" runat="server">Receipt Number.....</td>
                    <td>
                        <asp:Label ID="lblReceiptId" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>Policy Nr.....</td>
                    <td>
                        <asp:Label ID="lblPolicy" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>Date Paid.....</td>
                    <td>
                        <asp:Label ID="lblDatePaid" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>Amount Paid...</td>
                    <td>
                        <asp:Label ID="lblAmtpaid" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>Policy Holder.</td>
                    <td>
                        <asp:Label ID="lblPolicyholder" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>Received By...</td>
                    <td>
                        <asp:Label ID="lblRecivedBy" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>Time Printed..</td>
                    <td>
                        <asp:Label ID="lblTimePrint" runat="server"></asp:Label></td>
                </tr>
<%--                <tr>
                    <td>Method.......</td>
                    <td>
                        <asp:Label ID="lblmethod" runat="server"></asp:Label></td>
                </tr>--%>
                <tr>
                    <td>Underwriter...</td>
                    <td>
                        <asp:Label ID="lblunderwriter" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>Month Paid..</td>
                    <td>
                        <asp:Label ID="lblMonthPaid" runat="server"></asp:Label></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
