using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Funeral.Web.App_Start
{
    public enum isPageRight
    {
        HasAccess,
        HasAdd,
        HasEdit,
        HasDelete,
        HasReversalPayment
    }
    public class PageRightsAttribute : ActionFilterAttribute
    {
        public int CurrentPageId { get; set; }
        public isPageRight[] Right { get; set; }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {  

            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (CurrentPageId != 0)
                {
                    List<Model.tblPageModel> obj = SideMenuModelList;

                    if (!(obj.Where(x => x.ID == CurrentPageId).Any()))
                    {
                        
                        filterContext.HttpContext.Response.Redirect(string.Format("{0}://{1}{2}/{3}", filterContext.HttpContext.Request.Url.Scheme, filterContext.HttpContext.Request.Url.Authority, filterContext.HttpContext.Request.ApplicationPath, "Admin/Error/Error403"));
                    }
                    else
                    {
                        filterContext.Controller.ViewBag.HasAccess = obj.Where(x => x.ID == CurrentPageId).Select(x => x.HasAccess).FirstOrDefault();
                        filterContext.Controller.ViewBag.HasCreateRight = obj.Where(x => x.ID == CurrentPageId).Select(x => x.IsWrite).FirstOrDefault();
                        filterContext.Controller.ViewBag.HasReadRight = obj.Where(x => x.ID == CurrentPageId).Select(x => x.IsRead).FirstOrDefault();
                        filterContext.Controller.ViewBag.HasDeleteRight = obj.Where(x => x.ID == CurrentPageId).Select(x => x.IsDelete).FirstOrDefault();
                        filterContext.Controller.ViewBag.HasEditRight = obj.Where(x => x.ID == CurrentPageId).Select(x => x.IsUpdate).FirstOrDefault();
                        filterContext.Controller.ViewBag.HasReversalPayment = obj.Where(x => x.ID == CurrentPageId).Select(x => x.IsReversalPayment).FirstOrDefault();                        
                        filterContext.Controller.ViewBag.AdministratorOrSuperUser = false;

                        List<Funeral.Model.SecureUserGroupsModel> grp = SecurUserGroupModel;//privateClient.EditSecurUserbyID(UserID);
                        List<int> list = new List<int>();
                        list.Add(4);
                        list.Add(12);
                        if (!(grp.Where(x => list.Contains(x.fkiSecureGroupID)).FirstOrDefault() == null))
                        {
                            filterContext.Controller.ViewBag.HasAccess = true;
                            filterContext.Controller.ViewBag.HasCreateRight = true;
                            filterContext.Controller.ViewBag.HasReadRight = true;
                            filterContext.Controller.ViewBag.HasDeleteRight = true;
                            filterContext.Controller.ViewBag.HasEditRight = true;
                            filterContext.Controller.ViewBag.HasReversalPayment = true;                           
                            filterContext.Controller.ViewBag.AdministratorOrSuperUser = true;
                        }
                    }
                }
            }
            else
            {
                filterContext.HttpContext.Response.Redirect("~/Admin/Login.aspx");
                return;
            }

            base.OnActionExecuting(filterContext);
        }
        public List<Funeral.Model.tblPageModel> SideMenuModelList
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["SideMenuModelList"] == null)
                {
                    FuneralServiceReference.FuneralServicesClient privateClient = new FuneralServiceReference.FuneralServicesClient();
                    Funeral.Model.tblPageModel[] obj = privateClient.LoadSideMenu(ParlourId, UserID);
                    System.Web.HttpContext.Current.Session["SideMenuModelList"] = obj.ToList();
                    return obj.ToList();
                }
                else
                {
                    return System.Web.HttpContext.Current.Session["SideMenuModelList"] as List<Funeral.Model.tblPageModel>;
                }
            }
            set
            {
                System.Web.HttpContext.Current.Session["SideMenuModelList"] = value;
            }
        }

        public List<Funeral.Model.SecureUserGroupsModel> SecurUserGroupModel
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["SecurUserGroupModel"] == null)
                {
                    FuneralServiceReference.FuneralServicesClient privateClient = new FuneralServiceReference.FuneralServicesClient();
                    Funeral.Model.SecureUserGroupsModel[] obj = privateClient.EditSecurUserbyID(UserID);
                    System.Web.HttpContext.Current.Session["SecurUserGroupModel"] = obj.ToList();
                    return obj.ToList();
                }
                else
                {
                    return System.Web.HttpContext.Current.Session["SecurUserGroupModel"] as List<Funeral.Model.SecureUserGroupsModel>;
                }
            }
            set
            {
                System.Web.HttpContext.Current.Session["SecurUserGroupModel"] = value;
            }
        }

        public Guid ParlourId
        {
            get
            {
                if (System.Web.HttpContext.Current.User != null && System.Web.HttpContext.Current.User.Identity != null && System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    FormsIdentity id = (FormsIdentity)System.Web.HttpContext.Current.User.Identity;
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

        public int UserID
        {
            get
            {
                if (System.Web.HttpContext.Current.User != null && System.Web.HttpContext.Current.User.Identity != null && System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    FormsIdentity id = (FormsIdentity)System.Web.HttpContext.Current.User.Identity;
                    FormsAuthenticationTicket ticket = id.Ticket;
                    string[] strData = ticket.UserData.Split('|');
                    if (strData.Any())
                        return Convert.ToInt32(strData[3]);
                    else
                        return 0;
                    //return new Guid(string.IsNullOrEmpty(ticket.UserData) ? "00000000-0000-0000-0000-000000000000" : ticket.UserData);
                }
                else
                {
                    return 0;
                    //return new Guid("00000000-0000-0000-0000-000000000000");
                }
            }

        }
    }
}