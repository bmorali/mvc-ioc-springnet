using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCAuthenticationSample.Utils
{
    public static class UrlResorseHelper
    {
        static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
       
        public static string GetApplicationPath(string path)
        {
            string res;

            string appPath = (HttpContext.Current.Request.ApplicationPath == "/") ?
                String.Empty : HttpContext.Current.Request.ApplicationPath;

            res = string.Format("{0}/{1}", appPath, RenameFileReference(path).TrimStart('/'));

            return res;

        }

        public static string RenameFileReference(string path)
        {
            System.IO.FileInfo file = new System.IO.FileInfo(HttpContext.Current.Server.MapPath(path));
            string fileName = string.Empty;
            try
            {
                
                if (!file.Exists)
                {
                    if (file.FullName == HttpContext.Current.Request.PhysicalApplicationPath + path)
                        return path;

                    file = new System.IO.FileInfo(HttpContext.Current.Request.PhysicalApplicationPath + path);
                    if (!file.Exists)
                        return path;
                }

                fileName = file.Name;
                
            }
            catch (Exception ex)

            {
                log.Fatal(String.Format("exception for passed path: {0}, {1} ", path, ex.Message));
            }

            return path.Replace(file.Name, fileName);
        }
    }
}
