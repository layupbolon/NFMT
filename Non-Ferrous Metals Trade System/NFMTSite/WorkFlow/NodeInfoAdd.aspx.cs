using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NFMT.WorkFlow.Model;
using NFMT.WorkFlow.BLL;

namespace NFMTSite.WorkFlow
{
    public partial class NodeInfoAdd : System.Web.UI.Page
    {
        NodeBLL bll = new NodeBLL();   
        string action = string.Empty;
        string pk = string.Empty;
        string MasterID;
        Node node;
        
        protected string title;

        protected void Page_Load(object sender, EventArgs e)
        {
            action = Request.QueryString["action"].ToString();
            MasterID = Request.QueryString["masterid"].ToString();
            if (!string.IsNullOrEmpty(action) && action == "edit")
            {
                title = "修改节点信息";
                pk = Request.QueryString["pk"].ToString();
                node = (Node)(bll.Get((NFMT.Common.UserModel)Session["UserModel"], Convert.ToInt32(pk)).ReturnValue);
                ShowData(node);
            }
            else
            {
                title = "新增节点信息";
            }
        }

        private void ShowData(Node node)
        {
            ddlNodelevel.ClearSelection();
            ddlNodelevel.Items.FindByText(node.NodeLevel.ToString()).Selected = true;
            txtNodeName.Text = node.NodeName;
            ddlNodeType.ClearSelection();
            ddlNodeType.Items.FindByValue(node.NodeType.ToString()).Selected = true;
            ckbIsFirst.Checked = node.IsFirst;
            ckbIsLast.Checked = node.IsLast;
            txtPreNodeId.Text = node.PreNodeId.ToString();
            //txtRoleId.Text = node.RoleId.ToString();
            txtAuthGroupId.Text = node.AuthGroupId.ToString();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(action) && action == "edit")
                {
                    node.NodeLevel = Convert.ToInt32(Request["ddlNodelevel"].ToString());
                    node.NodeName = Request["txtNodeName"].ToString();
                    node.NodeType = Convert.ToInt32(Request["ddlNodeType"].ToString());
                    node.IsFirst = Request["ckbIsFirst"] != null && Request["ckbIsFirst"].ToString().ToLower() == "on" ? true : false;
                    node.IsLast = Request["ckbIsLast"] != null && Request["ckbIsLast"].ToString().ToLower() == "on" ? true : false;
                    node.PreNodeId = Convert.ToInt32(Request["txtPreNodeId"].ToString());
                    //node.RoleId = Convert.ToInt32(Request["txtRoleId"].ToString());
                    node.AuthGroupId = Convert.ToInt32(Request["txtAuthGroupId"].ToString());

                    if ((int)(bll.Update((NFMT.Common.UserModel)Session["UserModel"], node).ReturnValue) > 0)
                    {
                        Response.Redirect("/WorkFlow/NodeInfo.aspx?MasterID=" + this.MasterID);
                    }
                    else
                    {
                        lblMsg.InnerHtml = "<script type='text/javascript'>alert('修改失败!');</script>";
                    }
                }
                else
                {
                    Node noderesult = new Node()
                    {
                        MasterId = Convert.ToInt32(Request.QueryString["masterid"].ToString()),
                        NodeLevel = Convert.ToInt32(ddlNodelevel.SelectedValue),
                        NodeName = Request["txtNodeName"].ToString(),
                        NodeType = Convert.ToInt32(ddlNodeType.SelectedValue),
                        IsFirst = ckbIsFirst.Checked,
                        IsLast = ckbIsLast.Checked,
                        PreNodeId = Convert.ToInt32(txtPreNodeId.Text.ToString()),
                        //RoleId = Convert.ToInt32(txtRoleId.Text.ToString()),
                        AuthGroupId = Convert.ToInt32(txtAuthGroupId.Text.ToString())
                    };
                    if ((int)bll.Insert((NFMT.Common.UserModel)Session["UserModel"], noderesult).AffectCount > 0)
                    {
                        Response.Redirect("/WorkFlow/NodeInfo.aspx?MasterID=" + this.MasterID);
                    }
                    else
                    {
                        lblMsg.InnerHtml = "<script type='text/javascript'>alert('添加失败!');</script>";
                    }
                }
            }
            catch (Exception)
            {
                lblMsg.InnerHtml = "<script type='text/javascript'>alert('操作失败!');</script>";
            }
            
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("/WorkFlow/NodeInfo.aspx?MasterID=" + this.MasterID);
        }
    }
}