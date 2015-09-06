using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NFMT.WorkFlow.Model;
using NFMT.WorkFlow.BLL;

namespace NFMTSite.WorkFlow
{
    public partial class NodeInfo : System.Web.UI.Page
    {
        NodeBLL bll = new NodeBLL();
        private string MasterId;

        protected void Page_Load(object sender, EventArgs e)
        {
            MasterId = Request.QueryString["MasterID"].ToString();
            if (!IsPostBack)
            {
                DataBingding();
            }
        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;

            if (e.Row.FindControl("btnEdit") != null)
            {
                Button btn = (Button)e.Row.FindControl("btnEdit");
                btn.Click += new EventHandler(btnEdit_Click);
            }
            if (e.Row.FindControl("btnNodeCondition") != null)
            {
                Button btn = (Button)e.Row.FindControl("btnNodeCondition");
                btn.Click += new EventHandler(btnNodeCondition_Click);
            }
            if (e.Row.FindControl("btnAudit") != null)
            {
                Button btn = (Button)e.Row.FindControl("btnAudit");
                btn.Click += new EventHandler(btnAudit_Click);
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.GridView1.EditIndex = e.NewEditIndex;
            DataBingding();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            GridViewRow gvr = (GridViewRow)button.Parent.Parent;
            string pk = GridView1.DataKeys[gvr.RowIndex].Value.ToString();
            Response.Redirect("/WorkFlow/NodeInfoAdd.aspx?masterid=" + MasterId + "&action=edit&pk=" + pk);
        }

        /// <summary>
        /// 节点条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNodeCondition_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            GridViewRow gvr = (GridViewRow)button.Parent.Parent;
            string NodeID = GridView1.DataKeys[gvr.RowIndex].Value.ToString();
            Response.Redirect("/WorkFlow/NodeConditionInfo.aspx?NodeID=" + NodeID);
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAudit_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 新增节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("/WorkFlow/NodeInfoAdd.aspx?action=add&masterid=" + MasterId);
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void DataBingding()
        {
            NFMT.Common.UserModel user = new NFMT.Common.UserModel()
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
            DataTable dt = (DataTable)bll.Load(user, new NFMT.Common.SelectModel()
            {
                ColumnName = "NodeId,NodeStatus,NodeLevel,MasterId,NodeName,NodeType,IsFirst,IsLast,PreNodeId,RoleId,AuthGroupId",
                TableName = "Node",
                WhereStr = "MasterId=" + MasterId,
                OrderStr = "NodeId",
                PageIndex = 1,
                PageSize = 10
            }).ReturnValue;
            this.GridView1.DataSource = dt;
            this.GridView1.DataBind();
        }
    }
}