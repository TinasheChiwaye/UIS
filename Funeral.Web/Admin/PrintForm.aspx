<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintForm.aspx.cs" Inherits="Funeral.Web.Admin.PrintForm" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UIS Policy Administration</title>

    <link rel="stylesheet" href="~/Content/Invoice/style.css" media="all" />
    <script>
        function printDiv(divName) {
            var printContents = document.getElementById(divName).innerHTML;
            var originalContents = document.body.innerHTML;

            document.body.innerHTML = printContents;

            window.print();

            document.body.innerHTML = originalContents;
        }
    </script>
</head>
<body>
    <input type="button" onclick="printDiv('PrintDiv')" value="Print This.." />
    <div id="PrintDiv">
        <div>
            <div>
                <header class="clearfix">
                    <h1>
                        <asp:Label ID="lblHeading" runat="server" Text=""></asp:Label></h1>
                    <div id="logo">
                        <asp:Image runat="server" ID="ImagePreview" Height="100px" Width="100px" ImageAlign="Left" />
                    </div>
                    <div id="project" style="font-size: 15px; padding-left: 50px">
                        <table style="padding: 0">
                            <tr>
                                <td style="padding-top: .0em; padding-bottom: .0em; text-align: left;">
                                    <div>
                                        <span><strong style="font-size: 15px;">
                                            <asp:Literal ID="lblAppliName" runat="server" Text=""></asp:Literal></strong></span>
                                    </div>



                                </td>
                            </tr>
                            <tr>
                                <td style="padding-top: .0em; padding-bottom: .0em; text-align: left;">
                                    <div>
                                        <asp:Label ID="lblAdd1" runat="server" Text=""></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-top: .0em; padding-bottom: .0em; text-align: left;">
                                    <div>
                                        <asp:Label ID="lblAdd2" runat="server" Text=""></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-top: .0em; padding-bottom: .0em; text-align: left;">
                                    <div>
                                        <asp:Label ID="lblAdd3" runat="server" Text=""></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-top: .0em; padding-bottom: .0em; text-align: left; padding-left: 0">
                                    <div>
                                        <asp:Label ID="lblAdd4" runat="server" Text=""></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-top: .0em; padding-bottom: .0em; text-align: left;">
                                    <div>
                                        <span>Code:
                                        <asp:Label ID="lblCode" runat="server" Text=""></asp:Label></span>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-top: .0em; padding-bottom: .0em; text-align: left;">
                                    <div>
                                        <span>Email:
                                        <asp:Literal ID="lblEmail" runat="server" Text=""></asp:Literal></span>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-top: .0em; padding-bottom: .0em; text-align: left;">
                                    <div><span>VAT:<asp:Literal ID="lblVat" runat="server" Text=""></asp:Literal></span></div>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-top: .0em; padding-bottom: .0em; text-align: left;">
                                    <div><span>Telephone:&nbsp;<asp:Literal ID="lblTelephone" runat="server" Text=""></asp:Literal></span></div>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-top: .0em; padding-bottom: .0em; text-align: left;">
                                    <div><span>Cell number:&nbsp;<asp:Literal ID="lblCellNumber" runat="server" Text=""></asp:Literal></span></div>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-top: .0em; padding-bottom: .0em; text-align: left;">
                                    <div><span>Email:&nbsp;<asp:Literal ID="lblContactEmail" runat="server" Text=""></asp:Literal></span></div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="company" class="clearfix">
                        <table>
                            <tr>
                                <td>
                                    <b><asp:Label ID="lblNumber1" runat="server" Text=""></asp:Label></b></td>
                                <td><b>:</b></td>
                                <td style="text-align: left;">
                                    <asp:Label ID="lblNumber2" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td><b>Date</b></td>
                                <td><b>:</b></td>
                                <td style="text-align: left;">
                                    <asp:Label ID="lblDate" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblDueDate" runat="server" Text=""></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblsemi" runat="server" Text=""></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:Label ID="lblDueDate1" runat="server" Text=""></asp:Label></td>
                            </tr>
                        </table>
                    </div>
                </header>

                <h1></h1>
                <div id="company" class="clearfix" style="text-align: right">
                    <div>
                        <asp:Label ID="lblInvoiceDateTitle" runat="server" Text=""></asp:Label>:<asp:Label ID="lblInvoiceDate" runat="server" Text=""></asp:Label>
                    </div>
                    <div>
                        <asp:Label ID="lblDecename1" runat="server" Text=""></asp:Label>:<asp:Label ID="lblDecename2" runat="server" Text=""></asp:Label>
                    </div>
                    <div>
                        <asp:Label ID="lblDeceIDnum1" runat="server" Text=""></asp:Label><asp:Label ID="lblDeceIDnum2" runat="server" Text=""></asp:Label>
                    </div>
                    <div>
                        <asp:Label ID="lblDeceBI1" runat="server" Text=""></asp:Label><asp:Label ID="lblDeceBI2" runat="server" Text=""></asp:Label>
                    </div>
                    <div>
                        <asp:Label ID="lblCemetery1" runat="server" Text=""></asp:Label><asp:Label ID="lblCemetery2" runat="server" Text=""></asp:Label>
                    </div>
                    <div>
                        <asp:Label ID="lblDeceDate1" runat="server" Text=""></asp:Label><asp:Label ID="lblDeceDate2" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <h2>To</h2>
                <div id="project2">
                   <%--  <div>
                        <span>
                            <asp:Label ID="lblContactPerson" runat="server" Text=""></asp:Label></span>
                    </div>
                     <div>
                        <span>
                            <asp:Label ID="lblContactNumber" runat="server" Text=""></asp:Label></span>
                    </div>--%>
                    <div>
                        <span>
                            <asp:Label ID="lblName" runat="server" Text=""></asp:Label></span>
                    </div>
                    <div>
                        <span>
                            <asp:Label ID="lblcAdd1" runat="server" Text=""></asp:Label></span>
                    </div>
                    <div>
                        <span>
                            <asp:Label ID="lblcAdd2" runat="server" Text=""></asp:Label></span>
                    </div>
                    <div>
                        <span>
                            <asp:Label ID="lblcAdd3" runat="server" Text=""></asp:Label></span>
                    </div>
                    <div>
                        <span>
                            <asp:Label ID="lblcAdd4" runat="server" Text=""></asp:Label></span>
                    </div>
                    <div>
                        <span>
                            <asp:Label ID="lblcCode" runat="server" Text=""></asp:Label></span>
                    </div>
                </div>
                <br />
                <br />
                <%--    <br />
                <br />--%>
                <h1></h1>


                <asp:Literal runat="server" ID="ltrData"></asp:Literal>

                <%--   <div class="clearfix" runat="server">--%>
                <div style="text-align: right">
                    <table style="float: right; text-align: right" runat="server">
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>

                            <td colspan="4"><b style="font-size: 11px"></b></td>
                            <td class="total">TAX:
                                <asp:Label ID="lblTax" runat="server" Text=""></asp:Label>%</td>
                            <td>
                                <asp:Label ID="lblTax2" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td colspan="4"><b style="font-size: 11px"></b></td>
                            <td class="total">Discount:
                                <asp:Label ID="lblDiscount" runat="server" Text=""></asp:Label>%</td>
                            <td>
                                <asp:Label ID="lblDisAmt2" runat="server" Text=""></asp:Label></td>
                        </tr>
                         <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td colspan="4"><b style="font-size: 11px"></b></td>
                            <td class="total">Paid</td>
                            <td>
                                <asp:Label ID="lblTotalPayment" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td colspan="4" class="grand total"><b style="font-size: 11px">GRAND TOTAL OUTSTANDING </b></td>
                            <td class="grand total">
                                <asp:Label ID="lblGtotal" runat="server" Text=""></asp:Label></td>

                        </tr>
                    </table>
                </div>
                <%-- </div>--%>


                <br />
                <h1></h1>

                <div id="notices">

                    <div>NOTES:</div>
                    <div class="notice">

                        <asp:Label ID="lblNotes" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <asp:Label ID="lblSubtotal" runat="server" Text=""></asp:Label>
                <hr />
                <div>Terms and Conditions:</div>
                <div class="notice">
                    <asp:Label ID="lblTNC" runat="server" Text=""></asp:Label>
                </div>
                <br />

                <h1></h1>
                <div>Bank Details</div>
                <hr />
                <div class="notice">
                    <table style="width: 100%;">
                        <tr>
                            <td style="text-align: left;">
                                <asp:Literal ID="lblBankDetails" runat="server" Text=""></asp:Literal></td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <asp:Label ID="lblMsg1" runat="server" Text=""></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblMsg2" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <footer>
                                    <div style="text-align: center;">
                                        <asp:Label ID="lblSlogan" runat="server" Text=""></asp:Label>
                                    </div>
                                </footer>
                            </td>
                        </tr>
                    </table>

                </div>
            </div>
        </div>
</body>
</html>


