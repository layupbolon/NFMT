using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Control
{
    public partial class jqxAttach : System.Web.UI.UserControl
    {
        private string attachId = string.Empty;
        public string AttachId
        {
            get { return attachId; }
            set { attachId = value; }
        }

        private string uploadUrl = string.Format("{0}Files/Handler/FileUpLoadHandler.ashx", NFMT.Common.DefaultValue.NfmtSiteName);
        public string UploadUrl
        {
            get { return uploadUrl; }
            set { uploadUrl = value; }
        }

        private string fileInputName = string.Empty;
        public string FileInputName
        {
            get { return fileInputName; }
            set { fileInputName = value; }
        }

        //private bool multipleFilesUpload = false;
        ///// <summary>
        ///// 是否多文件上传
        ///// </summary>
        //public bool MultipleFilesUpload
        //{
        //    get { return this.multipleFilesUpload; }
        //    set { this.multipleFilesUpload = value; }
        //}
    }
}