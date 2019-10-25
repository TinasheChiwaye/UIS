using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Neodynamic.SDK.Web;

namespace Funeral.Web.Admin
{
    public partial class PrinterReciept : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Neodynamic.SDK.Web.WebClientPrint.CreateScript();


            //Is a Print Request?
            if (WebClientPrint.ProcessPrintJob(Request))
            {

                bool useDefaultPrinter = (Request["useDefaultPrinter"] == "checked");
                string printerName = Server.UrlDecode(Request["printerName"]);


                /*  
                 * Response.Redirect("PrinterReciept.aspx?PolicyNr=" + model.MemeberNumber.ToString() +
                "&DatePaid=" + model.PaymentDate.ToString("YYYY-MMM-dd") +
                "&AmountPaid=" + model.Amount.ToString() +
                "&LateAmountPaid=" + model.LatePaymentPenalty.ToString() +
                "&PolicyHolder=" + model.FullNames +
                "&ReceivedBy=" + model.ReceivedBy +
                "&TimePrinted=" + DateTime.Now.ToString("YYYY-MMM-dd hh:mm") +
                "&Method=" + model.MethodOfPayment +
                "&Notes=" + model.Notes +
                "&Plan=" + model.PlanName);

                 * 
 *  Dim zlabel As New Com.SharpZebra.Example.BorderedLabel("   Branch Name...... " & vbTab & vbTab & ConfigurationManager.AppSettings("branch"), _
                                                   "   Receipt Nr....... " & vbTab & vbTab & intReceipt, _
                   "   Policy Nr........ " & vbTab & vbTab & txtMemberNo.Text.ToUpper, _
                   "   Date Paid........ " & vbTab & vbTab & dtDatepaid.ToString("dd-MMM-yyyy"), _
                   "   Amount Paid...... " & vbTab & vbTab & FormatCurrency(txtAmount.Text, 2), _
                   "   Policy Holder.... " & vbTab & vbTab & BL.pFullNames & "  " & BL.pSurname, _
                   "   Received By...... " & vbTab & vbTab & txtRecievedBy.Text, _
                   "   Time Printed..... " & vbTab & vbTab & DateTime.Now.ToString("dd-MMM-yyyy hh:mm"), _
                   "   Method........... " & vbTab & vbTab & strMethod, _
                   notes, _
                   "   Underwriter...... " & vbTab & vbTab & BL.pAddress4, "123", strAppName, dtAdditionalAppSettings.Rows(0).Item("slogan"))
 
 * 
 */


                //Create ESC/POS commands for sample receipt
                string ESC = "0x1B"; //ESC byte in hex notation
                string NewLine = "0x0A"; //LF byte in hex notation

                string cmds = ESC + "@"; //Initializes the printer (ESC @)
                cmds += ESC + "!" + "0x38"; //Emphasized + Double-height + Double-width mode selected (ESC ! (8 + 16 + 32)) 56 dec => 38 hex
                cmds += "UnpluggIT"; //text to print
                cmds += NewLine + NewLine;
                cmds += ESC + "!" + "0x00"; //Character font A selected (ESC ! 0)
                cmds += "Policy Nr........  ";
                cmds += NewLine;
                cmds += "Date Paid........  ";
                cmds += NewLine + NewLine;
                cmds += "Amount Paid......  ";
                cmds += NewLine;
                cmds += "Policy Holder....  ";
                cmds += NewLine;
                cmds += "Time Printed.....  " + System.DateTime.Now.ToString("dd-MMM-yyyy"); 
                cmds += NewLine;
                cmds += "Method...........  ";
                cmds += NewLine;
                cmds += "Product Name.....  ";
                cmds += NewLine;
                cmds += System.DateTime.Now.ToString("dd-MMM-yyyy");

                cmds += "0x1D 0x56 <m>";

                //Create a ClientPrintJob and send it back to the client!
                ClientPrintJob cpj = new ClientPrintJob();
                //set ESC/POS commands to print...
                cpj.PrinterCommands = cmds;
                cpj.FormatHexValues = true;

                //set client printer...
                if (useDefaultPrinter || printerName == "null")
                    cpj.ClientPrinter = new DefaultPrinter();
                else
                    cpj.ClientPrinter = new InstalledPrinter(printerName);
                //send it...
                cpj.SendToClient(Response);

            }
        }
    
    }
}