using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NFMT.WorkFlow.Model;
using NFMT.WorkFlow.BLL;
using System.Data;

namespace NFMTSite.WorkFlow
{
    public partial class NodeConditionInfo : System.Web.UI.Page
    {
        NodeConditionBLL bll = new NodeConditionBLL();
        string NodeID;
        NFMT.Common.UserModel user;

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
            NodeID = Request.QueryString["NodeID"].ToString();
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
            if (e.Row.FindControl("btnCancel") != null)
            {
                Button btn = (Button)e.Row.FindControl("btnCancel");
                btn.Click += new EventHandler(btnCancel_Click);
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("/WorkFlow/NodeConditionAdd.aspx?action=add&NodeID=" + NodeID);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            GridViewRow gvr = (GridViewRow)button.Parent.Parent;
            string ConditionId = GridView1.DataKeys[gvr.RowIndex].Value.ToString();
            Response.Redirect("/WorkFlow/NodeConditionAdd.aspx?action=edit&ConditionId=" + ConditionId+"&NodeID=" + NodeID);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                GridViewRow gvr = (GridViewRow)button.Parent.Parent;
                string ConditionId = GridView1.DataKeys[gvr.RowIndex].Value.ToString();

                //bll.Cancel(user, (NodeCondition)bll.Get(user, Convert.ToInt32(ConditionId)).ReturnValue);
            }
            finally
            {
                DataBingding();
            }
        }

        private void btnAudit_Click(object sender, EventArgs e)
        {

        }

        private void DataBingding()
        {
            
            DataTable dt = (DataTable)bll.Load(user, new NFMT.Common.SelectModel()
            {
                ColumnName = "ConditionId,ConditionStatus,NodeId,BefValue,AftValue,ConditionType,TrueNodeId,FalseNodeId",
                TableName = "NodeCondition",
                WhereStr = "NodeId=" + NodeID,
                OrderStr = "ConditionId",
                PageIndex = 1,
                PageSize = 10
            }).ReturnValue;
            this.GridView1.DataSource = dt;
            this.GridView1.DataBind();
        }
    }
}