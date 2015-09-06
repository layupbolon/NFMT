using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NFMT.WorkFlow.BLL;
using NFMT.WorkFlow.Model;
using System.Reflection;
using System.Collections;
using NFMT.Common;

namespace NFMTSite.WorkFlow
{
    public partial class MasterInfo : System.Web.UI.Page
    {
        FlowMasterBLL bll = new FlowMasterBLL();
        private NFMT.Common.UserModel user;

        protected void Page_Load(object sender, EventArgs e)
        {
            user = new NFMT.Common.UserModel()
            {
                AccountId = 1,
                AccountName = "abc",
                //BlocId = 1,
                //CorpIds = new List<int> { 1 },
                //DeptIds = new List<int> { 1 },
                EmpId = 1,
                EmpName = "张三"
            };
            Session["UserModel"] = user;
            if (!IsPostBack)
            {
                DdlBinding();
                DataBingding(ddlMasterStatus.SelectedValue);
                btnAudit.Attributes.Add("onclick", "return confirm('是否通过审核？')");
            }
        }

        /// <summary>
        /// 新增模版
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("/WorkFlow/MasterInfoAdd.aspx?action=add");
        }

        protected void ddlMasterStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataBingding(ddlMasterStatus.SelectedValue);
            ControlStatus(ddlMasterStatus.SelectedValue);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click1(object sender, EventArgs e)
        {
            Response.Redirect("/WorkFlow/MasterInfoAdd.aspx?action=edit&pk=" + GetSelectedValue());
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAudit_Click1(object sender, EventArgs e)
        {
            btnAudit.Attributes.Add("onclick", "return confirm('是否通过审核？')");
            bll.Audit(user, (FlowMaster)bll.Get(user, Convert.ToInt32(GetSelectedValue())).ReturnValue, true);
            DataBingding(ddlMasterStatus.SelectedValue);
        }

        /// <summary>
        /// 明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDetail_Click1(object sender, EventArgs e)
        {
            Response.Redirect("/WorkFlow/NodeInfo.aspx?MasterID=" + GetSelectedValue());
        }

        /// <summary>
        /// 冻结
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFreeze_Click(object sender, EventArgs e)
        {
            bll.Freeze(user, (FlowMaster)bll.Get(user, Convert.ToInt32(GetSelectedValue())).ReturnValue);
            DataBingding(ddlMasterStatus.SelectedValue);
        }

        /// <summary>
        /// 解冻
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUnFreeze_Click(object sender, EventArgs e)
        {
            bll.UnFreeze(user, (FlowMaster)bll.Get(user, Convert.ToInt32(GetSelectedValue())).ReturnValue);
            DataBingding(ddlMasterStatus.SelectedValue);
        }

        /// <summary>
        /// 提交审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bll.Submit(user, (FlowMaster)bll.Get(user, Convert.ToInt32(GetSelectedValue())).ReturnValue);
            DataBingding(ddlMasterStatus.SelectedValue);
        }

        /// <summary>
        /// 获取选中行
        /// </summary>
        /// <returns></returns>
        private string GetSelectedValue()
        {
            List<string> list = new List<string>();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (((CheckBox)GridView1.Rows[i].FindControl("btnSelect")).Checked)
                {
                    list.Add(GridView1.DataKeys[i].Value.ToString());
                }
            }
            if (list.Count > 1)
                return string.Empty;

            return list[0];
        }

        /// <summary>
        /// 下拉框绑定
        /// </summary>
        private void DdlBinding()
        {
            ddlMasterStatus.Items.Clear();
            //ddlMasterStatus.Items.Add(new ListItem("", ""));
            foreach (StatusEnum status in Enum.GetValues(typeof(NFMT.Common.StatusEnum)))
            {
                ddlMasterStatus.Items.Add(new ListItem(status.ToString(), Convert.ToInt32(status).ToString()));
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void DataBingding(string MasterStatus)
        {
            string wherestr = string.Empty;
            if (!string.IsNullOrEmpty(MasterStatus))
            {
                wherestr = "MasterStatus=" + MasterStatus;
            }
            DataTable dt = (DataTable)bll.Load(user, new NFMT.Common.SelectModel()
            {
                ColumnName = "MasterId,MasterName,MasterStatus,CreatorId,CreateTime,LastModifyId,LastModifyTime",
                TableName = "FlowMaster",
                WhereStr = wherestr,
                OrderStr = "MasterId",
                PageIndex = 1,
                PageSize = 10
            }).ReturnValue;
            this.GridView1.DataSource = dt;
            this.GridView1.DataBind();
        }

        private void ControlStatus(string MasterStatus)
        {
            this.btnAdd.Visible = true;
            this.btnDetail.Visible = true;
            switch ((NFMT.Common.StatusEnum)Enum.Parse(typeof(NFMT.Common.StatusEnum), MasterStatus))
            {
                case StatusEnum.已录入:
                    this.btnEdit.Visible = true;
                    this.btnSubmit.Visible = true;
                    this.btnAudit.Visible = false;
                    this.btnFreeze.Visible = false;
                    this.btnUnFreeze.Visible = false;
                    break;
                case StatusEnum.待审核:
                    this.btnEdit.Visible = false;
                    this.btnSubmit.Visible = false;
                    this.btnAudit.Visible = true;
                    this.btnFreeze.Visible = false;
                    this.btnUnFreeze.Visible = false;
                    break;
                case StatusEnum.已生效:
                    this.btnEdit.Visible = false;
                    this.btnSubmit.Visible = false;
                    this.btnAudit.Visible = false;
                    this.btnFreeze.Visible = true;
                    this.btnUnFreeze.Visible = false;
                    break;
                case StatusEnum.已冻结:
                    this.btnEdit.Visible = false;
                    this.btnSubmit.Visible = false;
                    this.btnAudit.Visible = false;
                    this.btnFreeze.Visible = false;
                    this.btnUnFreeze.Visible = true;
                    break;
                default:
                    this.btnEdit.Visible = false;
                    this.btnSubmit.Visible = false;
                    this.btnAudit.Visible = false;
                    this.btnFreeze.Visible = false;
                    this.btnUnFreeze.Visible = false;
                    break;
            }
        }
    }
}