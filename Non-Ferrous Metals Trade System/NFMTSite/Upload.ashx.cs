using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite
{
    /// <summary>
    /// Upload 的摘要说明
    /// </summary>
    public class Upload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Files.Count > 0)
            {
                HttpPostedFile file = context.Request.Files[0];
                string msg = "";
                string error = "";
                if (file.ContentLength == 0)
                    error = "文件长度为0";
                else
                {
                    //file.SaveAs(context.Server.MapPath("file") + "\\" + context.Server.cont.GetFileName(file.FileName));
                    msg = "上传成功";
                }
                string result = "{ error:'" + error + "', msg:'" + msg + "'}";
                context.Response.Write(result);
                context.Response.End();
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