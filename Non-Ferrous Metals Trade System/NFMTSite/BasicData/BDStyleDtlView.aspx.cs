﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.BasicData
{
    public partial class BDStyleDtlView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 75, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.查询 });

                int id = 0;

                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    if (int.TryParse(Request.QueryString["id"], out id))
                    {
                        if (id == 0)
                            Response.Redirect("BasicData/BDStyleList.aspx");
                    }

                    //获取当前用户
                    //NFMT.Common.UserModel user = NFMT.Common.DefaultValue.SysUser;
                    NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

                    NFMT.Data.Model.BDStyleDetail detail = NFMT.Data.BasicDataProvider.StyleDetails.FirstOrDefault(temp => temp.StyleDetailId == id);
                    if (detail == null || detail.StyleDetailId == 0)
                    {
                        this.ClientScript.RegisterStartupScript(this.GetType(), "loaddataerror", "<script>alert(\"明细序号错误\");</script>");
                        return;
                    }

                    NFMT.Data.Model.BDStyle style = NFMT.Data.BasicDataProvider.BDStyles.FirstOrDefault(temp => temp.BDStyleId == detail.BDStyleId);
                    if (style == null || style.BDStyleId == 0)
                    {
                        this.ClientScript.RegisterStartupScript(this.GetType(), "loaddataerror", "<script>alert(\"明细序号错误\");</script>");
                        return;
                    }                    

                    this.spnStyleName.InnerHtml = style.BDStyleName;
                    this.spnStyleStatus.InnerHtml = style.BDStyleStatusName;
                    this.hidDetailId.Value = detail.StyleDetailId.ToString();
                    this.spnDetailCode.InnerHtml = detail.DetailCode;
                    this.spnDetailName.InnerHtml = detail.DetailName;
                    this.spnDetailStatus.InnerHtml = detail.DetailStatus.ToString();

                    this.navigation1.Routes.Add("类型列表", "BDStyleList.aspx");
                    this.navigation1.Routes.Add("类型明细列表", string.Format("BDStyleDtlList.aspx?id={0}", style.BDStyleId));
                    this.navigation1.Routes.Add("类型明细查看", string.Empty);
                }                
            }

            
        }
    }
}