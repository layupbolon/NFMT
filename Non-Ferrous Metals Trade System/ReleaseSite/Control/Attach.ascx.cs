using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReleaseSite.Control
{
    public partial class Attach : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.Text.StringBuilder sbJs = new System.Text.StringBuilder();

            sbJs.AppendLine("<script type=\"text/javascript\" src=\"../js/ajaxfileupload.js\"></script>");

            sbJs.AppendLine("<script type=\"text/javascript\">");
            sbJs.AppendFormat("var attachUrl = \"../Files/Handler/GetAttachListHandler.ashx?cid={0}&s={1}&t={2}\";", this.BusinessIdValue, (int)NFMT.Common.StatusEnum.已生效, (int)this.AttachType);
            sbJs.AppendLine("$(\"#jqxAttachExpander\").jqxExpander({ width: \"98%\" });");
            sbJs.AppendLine("</script>");

            if (this.AttachStyle == SiteUtility.AttachStyleEnum.查看)
            {
                sb.AppendLine("<div id=\"jqxAttachGrid\"></div>");

                sbJs.AppendLine("<script type=\"text/javascript\" src=\"../js/AttachDetail.js\"></script>");
            }
            else if (this.AttachStyle == SiteUtility.AttachStyleEnum.新增)
            {
                sb.AppendLine("<ul id=\"ulAttach\">");
                sb.AppendLine("<li>");
                sb.AppendLine("<strong>附件1：</strong>");
                sb.AppendLine("<input id=\"file1\" type=\"file\" name=\"file1\" onchange=\"addli(2);\" />");
                sb.AppendLine("</li>");
                sb.AppendLine("</ul>");
            }
            else if (this.AttachStyle == SiteUtility.AttachStyleEnum.修改)
            {
                sb.AppendLine("<div id=\"jqxAttachGrid\"></div>");
                sb.AppendLine("<div>");
                sb.AppendLine("<ul id=\"ulAttach\">");
                sb.AppendLine("<li>");
                sb.AppendLine("<strong>附件1：</strong>");
                sb.AppendLine("<input id=\"file1\" type=\"file\" name=\"file1\" onchange=\"addli(2);\" />");
                sb.AppendLine("</li>");
                sb.AppendLine("</ul>");
                sb.AppendLine("</div>");

                sbJs.AppendLine("<script type=\"text/javascript\" src=\"../js/AttachList.js\"></script>");
            }

            this.attachInfo.InnerHtml = sb.ToString();
            this.attachJs.Text = sbJs.ToString();
        }

        private SiteUtility.AttachStyleEnum attachStyle = SiteUtility.AttachStyleEnum.查看;
        public SiteUtility.AttachStyleEnum AttachStyle
        {
            get { return this.attachStyle; }
            set { this.attachStyle = value; }
        }

        public NFMT.Operate.AttachType AttachType { get; set; }
        public int BusinessIdValue { get; set; }
    }
}