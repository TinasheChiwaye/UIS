using Funeral.BAL;
using Funeral.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Funeral.Web.Handler
{
    /// <summary>
    /// Summary description for DocumentHandler
    /// </summary>
    public class DocumentHandler : IHttpHandler
    {
        FuneralServiceReference.FuneralServicesClient client = new FuneralServiceReference.FuneralServicesClient();
        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.IsAuthenticated)
            {
                if (context.Request.QueryString["DocID"] != null)
                {
                    SupportedDocumentModel objModel = MembersBAL.SelectSupportDocumentsById(Convert.ToInt32(context.Request.QueryString["DocID"]));
                    if (objModel != null)
                    {
                        Byte[] bytes = objModel.ImageFile;
                        context.Response.Buffer = true;
                        context.Response.Charset = "";
                        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        context.Response.ContentType = string.IsNullOrEmpty(objModel.DocContentType) ? "application/octet-stream" : objModel.DocContentType;
                        context.Response.AddHeader("content-disposition", "attachment;filename=" + objModel.ImageName);
                        context.Response.BinaryWrite(bytes);
                        context.Response.Flush();
                        context.Response.End();
                    }
                }
                else if (context.Request.QueryString["DocFID"] != null)
                {
                    FuneralDocumentModel objModel = client.SelectFuneralDocumentsByPKId(Convert.ToInt32(context.Request.QueryString["DocFID"]));
                    if (objModel != null)
                    {
                        Byte[] bytes = objModel.ImageFile;
                        context.Response.Buffer = true;
                        context.Response.Charset = "";
                        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        context.Response.ContentType = string.IsNullOrEmpty(objModel.DocContentType) ? "application/octet-stream" : objModel.DocContentType;
                        context.Response.AddHeader("content-disposition", "attachment;filename=" + objModel.ImageName);
                        context.Response.BinaryWrite(bytes);
                        context.Response.Flush();
                        context.Response.End();
                    }
                }
                else if (context.Request.QueryString["DocClaimID"] != null)
                {
                    ClaimDocumentModel objModel = client.SelectClaimsDocumentsByPKId(Convert.ToInt32(context.Request.QueryString["DocClaimID"]));
                    if (objModel != null)
                    {
                        Byte[] bytes = objModel.ImageFile;
                        context.Response.Buffer = true;
                        context.Response.Charset = "";
                        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        context.Response.ContentType = string.IsNullOrEmpty(objModel.DocContentType) ? "application/octet-stream" : objModel.DocContentType;
                        context.Response.AddHeader("content-disposition", "attachment;filename=" + objModel.ImageName);
                        context.Response.BinaryWrite(bytes);
                        context.Response.Flush();
                        context.Response.End();
                    }
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}