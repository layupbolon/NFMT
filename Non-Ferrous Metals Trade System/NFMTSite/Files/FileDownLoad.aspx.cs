using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Files
{
    public partial class FileDownLoad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            if (!IsPostBack)
            {
                int attachId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out attachId) || attachId <= 0)
                {
                    Response.Write("下载文件序号出错");
                    Response.End();
                }

                NFMT.Operate.BLL.AttachBLL bll = new NFMT.Operate.BLL.AttachBLL();
                NFMT.Common.ResultModel result = bll.Get(user, attachId);
                if (result.ResultStatus != 0)
                {
                    Response.Write(result.Message);
                    Response.End();
                }

                NFMT.Operate.Model.Attach attach = result.ReturnValue as NFMT.Operate.Model.Attach;
                if (attach == null)
                {
                    Response.Write("获取附件出错");
                    Response.End();
                }

                DownLoadFile(attach.AttachName, attach.ServerAttachName);
            }
        }

        private void DownLoadFile(string fileName, string filePath)
        {
            try
            {
                //以字符流的形式下载文件
                FileStream fs = new FileStream(filePath, FileMode.Open);
                byte[] bytes = new byte[(int)fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                fs.Close();
                Response.ContentType = "application/octet-stream";
                //通知浏览器下载文件而不是打开
                Response.AddHeader("Content-Disposition", string.Format("attachment; filename=\"{0}\"", fileName));
                Response.BinaryWrite(bytes);
                Response.Flush();
            }
            catch (Exception e)
            {
                Response.Write(e.Message);
            }
            finally
            {
                Response.End();
            }
        }
    }
}