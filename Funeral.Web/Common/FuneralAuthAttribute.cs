using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Security.Principal;
using System.Web.Security;
using System.Web;
using System.Data;
using Funeral.Model;
using System.Web.UI;

namespace Funeral.Web.Common
{
    public enum Rights
    {
        HasAdd,
        HasEdit,
        HasDelete
    }
    public class FuneralAuthAttribute : ActionFilterAttribute, IActionFilter
    {
        public int PageId { get; set; }
        public Rights[] Right { get; set; }

        public FuneralAuthAttribute()
        {
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (PageId != 0)
                {
                    List<Model.tblPageModel> obj = SideMenuModelList;

                    if (!(obj.Where(x => x.ID == PageId).Any()))
                    {
                        //filterContext.HttpContext.Response.Redirect("~/Admin/403Error.aspx");
                        filterContext.HttpContext.Response.Redirect(string.Format("{0}://{1}{2}/{3}", filterContext.HttpContext.Request.Url.Scheme, filterContext.HttpContext.Request.Url.Authority, filterContext.HttpContext.Request.ApplicationPath, "Error"));
                    }
                }
            }
            else
            {
                filterContext.HttpContext.Response.Redirect("~/Admin/Login.aspx");
                return;
            }

            #region Rights Implimentation
            //if (Right.Contains(Rights.HasAdd))
            //{
            //    if ((bool)HttpContext.Current.Session["HasCreateRight"] == false)
            //    {
            //        //filterContext.HttpContext.Response.Redirect("~/Admin/403Error.aspx");
            //        filterContext.HttpContext.Response.Redirect(string.Format("{0}://{1}{2}/{3}", filterContext.HttpContext.Request.Url.Scheme, filterContext.HttpContext.Request.Url.Authority, filterContext.HttpContext.Request.ApplicationPath, "Error"));
            //        return;
            //    }
            //}
            //if (Right.Contains(Rights.HasEdit))
            //{
            //    if ((bool)HttpContext.Current.Session["HasEditRight"] == false)
            //    {
            //        filterContext.HttpContext.Response.Redirect(string.Format("{0}://{1}{2}/{3}", filterContext.HttpContext.Request.Url.Scheme, filterContext.HttpContext.Request.Url.Authority, filterContext.HttpContext.Request.ApplicationPath, "Error"));
            //        return;
            //    }
            //}
            //if (Right.Contains(Rights.HasDelete))
            //{
            //    if ((bool)HttpContext.Current.Session["HasDeleteRight"] == false)
            //    {
            //        filterContext.HttpContext.Response.Redirect(string.Format("{0}://{1}{2}/{3}", filterContext.HttpContext.Request.Url.Scheme, filterContext.HttpContext.Request.Url.Authority, filterContext.HttpContext.Request.ApplicationPath, "Error"));
            //        return;
            //    }
            //}
            #endregion

            base.OnActionExecuting(filterContext);
        }

        public int UserID
        {
            get
            {
                if (HttpContext.Current.User != null && HttpContext.Current.User.Identity != null && HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
                    FormsAuthenticationTicket ticket = id.Ticket;
                    string[] strData = ticket.UserData.Split('|');
                    if (strData.Any())
                        return Convert.ToInt32(strData[3]);
                    else
                        return 0;
                }
                else
                {
                    return 0;
                }
            }

        }

        public Guid ParlourId
        {
            get
            {
                if (HttpContext.Current.User != null && HttpContext.Current.User.Identity != null && HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
                    FormsAuthenticationTicket ticket = id.Ticket;
                    string[] strData = ticket.UserData.Split('|');
                    if (strData.Count() > 0)
                        return new Guid(string.IsNullOrEmpty(strData[0]) ? "00000000-0000-0000-0000-000000000000" : strData[0]);
                    else
                        return new Guid("00000000-0000-0000-0000-000000000000");
                }
                else
                {
                    return new Guid("00000000-0000-0000-0000-000000000000");
                }
            }

        }

        public List<Funeral.Model.tblPageModel> SideMenuModelList
        {
            get
            {
                if (HttpContext.Current.Session["SideMenuModelList"] == null)
                {
                    FuneralServiceReference.FuneralServicesClient privateClient = new FuneralServiceReference.FuneralServicesClient();
                    Funeral.Model.tblPageModel[] obj = privateClient.LoadSideMenu(ParlourId, UserID);
                    HttpContext.Current.Session["SideMenuModelList"] = obj.ToList();
                    return obj.ToList();
                }
                else
                {
                    return HttpContext.Current.Session["SideMenuModelList"] as List<Funeral.Model.tblPageModel>;
                }
            }
            set
            {
                HttpContext.Current.Session["SideMenuModelList"] = value;
            }
        }

    }

    public class RedirectController : Controller
    {
        public ActionResult RedirectToLogin()
        {
            //Response.Redirect("~/Admin/Login.aspx", false);
            return Redirect("~/Admin/Login.aspx");
        }
    }
}