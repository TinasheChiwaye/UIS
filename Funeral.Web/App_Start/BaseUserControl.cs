using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Funeral.Web.App_Start
{
    public class BaseUserControl: System.Web.UI.UserControl
    {
        public string Currency
        {
            get
            {
                if (HttpContext.Current.User != null && HttpContext.Current.User.Identity != null &&
                    System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    FormsIdentity id = (FormsIdentity) System.Web.HttpContext.Current.User.Identity;
                    FormsAuthenticationTicket ticket = id.Ticket;
                    string[] strData = ticket.UserData.Split('|');
                    return strData.Count() == 5 ? strData[4] : "R";
                }
                return "R";
            }

        }
    }
}