using Funeral.BAL;
using Funeral.Model;
using System;
using System.Configuration;
using System.Web.Mvc;

namespace Funeral.Web.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(ForgotPassword forgotPassword)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var details = AdminBAL.GetLoginDetails(forgotPassword.Email);
                    if (details != null)
                    {
                        var ForgotId = AdminBAL.AddUpdateTo_ForgotPassword(0, details.PkiUserID, details.parlourid, "");
                        var url = Request.Url.AbsoluteUri.Replace("ForgotPassword", "ResetNewPassword?code=" + AdminBAL.encription(details.parlourid.ToString()) + "&secureId=" + AdminBAL.encription(details.PkiUserID.ToString()) + "&forgotId=" + AdminBAL.encription(ForgotId.ToString()));
                        var fromMail = ConfigurationManager.AppSettings["ReportEmailSenderId"].ToString().Trim();
                        bool IsSent = AdminBAL.SendForgotPasswordLink(details.Email, fromMail, details.Name, url);
                        if (IsSent)
                        {
                            AdminBAL.UpdateAdminLoginPassword(details.PkiUserID, details.parlourid, "");
                            TempData["forgotmessage"] = "<div class='ibox-content'><div class='alert alert-success'>we have sent new password generat link to your email</div>";
                        }
                        else
                        {
                            TempData["forgotmessage"] = "<div class='ibox-content'><div class='alert alert-danger'>sorry, email could not be sent,try again</div>";
                        }
                    }
                    else
                    {
                        TempData["forgotmessage"] = "<div class='ibox-content'><div class='alert alert-danger'>sorry,email not registered or may not be exiested</div>";
                    }
                }
                else
                {
                    TempData["forgotmessage"] = "<div class='ibox-content'><div class='alert alert-danger'>sorry, email could not be sent,try again</div>";
                }
                return RedirectToAction("ForgotPassword", "Login");
            }
            catch (Exception e)
            {
                TempData["forgotmessage"] = "<div class='ibox-content'><div class='alert alert-danger'>" + e.Message + "</div>";
                return RedirectToAction("ForgotPassword", "Login");
            }
        }
        public ActionResult ResetNewPassword(string code, string secureId, string forgotId)
        {
            if (code != null && secureId != null && forgotId != null)
            {
                var GetForgotPassword = AdminBAL.GetForgotPasswordDetails(Convert.ToInt32(AdminBAL.Decryption(forgotId)));
                if (GetForgotPassword != null)
                {
                    if (GetForgotPassword.IsChanged == false)
                    {
                        NewPassword newPassword = new NewPassword();
                        newPassword.Code = code;
                        newPassword.secureId = secureId;
                        newPassword.ForgotId = forgotId;
                        return View(newPassword);
                    }
                    else
                    {
                        TempData["forgotmessage"] = "<div class='ibox-content'><div class='alert alert-danger'>Link has been expired</div>";
                        return Redirect("/Admin/Login.aspx");
                    }
                }
                else
                {
                    TempData["forgotmessage"] = "<div class='ibox-content'><div class='alert alert-danger'>Link has been expired</div>";
                    return Redirect("/Admin/Login.aspx");
                }
            }
            else
            {
                TempData["forgotmessage"] = "<div class='ibox-content'><div class='alert alert-danger'>Link has been expired</div>";
                return Redirect("/Admin/Login.aspx");
            }
        }
        [HttpPost]
        public ActionResult ResetNewPassword(NewPassword newPassword)
        {
            if (newPassword != null)
            {
                var getModel = newPassword;
                Guid ParlourId = new Guid(AdminBAL.Decryption(getModel.Code));
                getModel.ParlourId = ParlourId;
                getModel.UserId = Convert.ToInt32(AdminBAL.Decryption(getModel.secureId));
                getModel.ForgotId = AdminBAL.Decryption(getModel.ForgotId);
                try
                {
                    if (ModelState.IsValid)
                    {
                        AdminBAL.AddUpdateTo_ForgotPassword(Convert.ToInt32(getModel.ForgotId), getModel.UserId, getModel.ParlourId, getModel.Password);
                        AdminBAL.UpdateAdminLoginPassword(getModel.UserId, getModel.ParlourId, getModel.Password);
                        TempData["forgotmessage"] = "<div class='ibox-content'><div class='alert alert-success'>Password changed successfully</div>";
                        return Redirect("/Admin/Login.aspx");

                    }
                    else
                    {
                        string getErros = "<ul>";
                        foreach (ModelState modelState in ModelState.Values)
                        {
                            foreach (ModelError error in modelState.Errors)
                            {
                                getErros += "<li>" + error.ErrorMessage + "</li>";
                            }
                        }
                        getErros += "</ul>";
                        TempData["forgotmessage"] = "<div class='ibox-content'><div class='alert alert-danger'>" + getErros + "</div>";
                        return RedirectToAction("ResetNewPassword", "Login", new { code = getModel.Code, secureId = getModel.secureId });
                    }
                }
                catch (Exception e)
                {
                    TempData["forgotmessage"] = "<div class='ibox-content'><div class='alert alert-danger'>" + e.Message + "</div>";
                    return RedirectToAction("ResetNewPassword", "Login", new { code = getModel.Code, secureId = getModel.secureId });
                }
            }
            else
            {
                TempData["forgotmessage"] = "<div class='ibox-content'><div class='alert alert-danger'>Link has been expired</div>";
                return Redirect("/Admin/Login.aspx");
            }
        }
    }
}