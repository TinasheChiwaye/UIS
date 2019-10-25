using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Funeral.Web.Common
{
    public class IdValidationClass
    {

        public IdValidationClass()
        {

        }
       
        public static Boolean IdValidation(string IdNumber)
        {
            return Funeral.Validation.IDValidation.IsValidID(IdNumber);  

        }
    }

    public class FuneralGeneralMethods
    {

        /// <summary>
        /// Get Upload Folder Path
        /// </summary>
        /// <param name="UploadFolderName"></param>
        /// <returns></returns>
        public static string GetUploadPath(string UploadFolderName)
        {
            string pathName = CreateFolder("~/Upload") + (string.IsNullOrEmpty(UploadFolderName) ? string.Empty : "/" + UploadFolderName);
            if (!System.IO.Directory.Exists(pathName))
                System.IO.Directory.CreateDirectory(pathName);
            if (pathName.EndsWith("/"))
                return pathName;
            else
                return pathName + "/";
        }

        public static string CreateFolder(string FolderName)
        {
            string pathName = HttpContext.Current.Server.MapPath(FolderName);
            if (!System.IO.Directory.Exists(pathName))
                System.IO.Directory.CreateDirectory(pathName);
            if (pathName.EndsWith("/"))
                return pathName;
            else
                return pathName + "/";
        }
    }
}