using Funeral.BAL;
using Funeral.Model;
using System;
using System.IO;
using System.Text;
using System.Web;

namespace Funeral.Web.Handler
{
    /// <summary>
    /// Summary description for DocumentHandler
    /// </summary>
    public class DocumentHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.IsAuthenticated)
            {
                if (context.Request.QueryString["DocID"] != null)
                {
                    SupportedDocumentModel objModel = MembersBAL.SelectSupportDocumentsById(Convert.ToInt32(context.Request.QueryString["DocID"]));
                    if (objModel != null)
                    {
                        DownloadFile(context, objModel.ImageFile, objModel.ImageName, objModel.DocContentType);
                    }
                }
                else if (context.Request.QueryString["DocFID"] != null)
                {
                    FuneralDocumentModel objModel = FuneralBAL.SelectFuneralDocumentsByPKId(Convert.ToInt32(context.Request.QueryString["DocFID"]));
                    if (objModel != null)
                    {
                        DownloadFile(context, objModel.ImageFile, objModel.ImageName, objModel.DocContentType);
                    }
                }
                else if (context.Request.QueryString["DocClaimID"] != null)
                {
                    ClaimDocumentModel objModel = ClaimsBAL.SelectClaimsDocumentsByPKId(Convert.ToInt32(context.Request.QueryString["DocClaimID"]));
                    if (objModel != null)
                    {
                        DownloadFile(context, objModel.ImageFile, objModel.ImageName, objModel.DocContentType);
                    }
                }
            }
        }

        private void DownloadFile(HttpContext context, byte[] ImageFile, string FileName, string DocContentType)
        {
            var asciiCode = Encoding.ASCII.GetString(ImageFile);
            var filePath = HttpContext.Current.Server.MapPath(asciiCode);
            byte[] Content = File.ReadAllBytes(filePath);
            context.Response.ContentType = string.IsNullOrEmpty(DocContentType) ? "application/octet-stream" : DocContentType;
            context.Response.AddHeader("content-disposition", "attachment; filename=" + FileName);
            context.Response.BufferOutput = true;
            context.Response.OutputStream.Write(Content, 0, Content.Length);
            context.Response.End();
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