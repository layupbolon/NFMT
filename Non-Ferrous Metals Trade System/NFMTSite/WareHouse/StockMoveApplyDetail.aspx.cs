using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.WareHouse
{
    public partial class StockMoveApplyDetail : System.Web.UI.Page
    {
        public int id = 0;
        public int applyId = 0;
        public NFMT.Operate.Model.Apply apply = new NFMT.Operate.Model.Apply();
        public NFMT.WareHouse.Model.StockMoveApply stockMoveApply = new NFMT.WareHouse.Model.StockMoveApply();

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string redirectURL = string.Format("{0}WareHouse/StockMoveApplyList.aspx", NFMT.Common.DefaultValue.NftmSiteName);

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 45, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.提交审核, NFMT.Common.OperateEnum.作废, NFMT.Common.OperateEnum.撤返, NFMT.Common.OperateEnum.确认完成 });

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                this.navigation1.Routes.Add("移库申请", redirectURL);
                this.navigation1.Routes.Add("移库申请明细", string.Empty);

                NFMT.WareHouse.BLL.StockMoveApplyBLL stockMoveApplyBLL = new NFMT.WareHouse.BLL.StockMoveApplyBLL();
                if (!string.IsNullOrEmpty(Request.QueryString["aid"]) && int.TryParse(Request.QueryString["aid"], out applyId) && applyId > 0)
                {
                    result = stockMoveApplyBLL.GetStockMoveApplyByApplyId(user, applyId);
                    if (result.ResultStatus != 0)
                        Response.Redirect(redirectURL);

                    stockMoveApply = result.ReturnValue as NFMT.WareHouse.Model.StockMoveApply;
                    if (stockMoveApply == null)
                        Response.Redirect(redirectURL);
                }
                else
                {
                    if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out id))
                        Response.Redirect(redirectURL);

                    //获取移库申请
                    result = stockMoveApplyBLL.Get(user, id);
                    if (result.ResultStatus != 0)
                        Response.Redirect(redirectURL);

                    stockMoveApply = result.ReturnValue as NFMT.WareHouse.Model.StockMoveApply;
                    if (stockMoveApply == null)
                        Response.Redirect(redirectURL);
                }

                id = stockMoveApply.StockMoveApplyId;
                applyId = stockMoveApply.ApplyId;
                this.hidid.Value = id.ToString();

                NFMT.WareHouse.BLL.StockMoveBLL stockMoveBLL = new NFMT.WareHouse.BLL.StockMoveBLL();
                result = stockMoveBLL.GetStockMoveIdByApplyId(user, id);
                if (result.ResultStatus == 0)
                {
                    this.hidStockMoveId.Value = result.ReturnValue.ToString();
                }

                //获取申请信息
                NFMT.Operate.BLL.ApplyBLL applyBLL = new NFMT.Operate.BLL.ApplyBLL();
                result = applyBLL.Get(user, stockMoveApply.ApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectURL);

                apply = result.ReturnValue as NFMT.Operate.Model.Apply;
                if (apply == null)
                    Response.Redirect(redirectURL);

                NFMT.User.Model.Department dept = NFMT.User.UserProvider.Departments.SingleOrDefault(a => a.DeptId == apply.ApplyDept);
                if (dept == null)
                    Response.Redirect(redirectURL);

                NFMT.User.Model.Corporation corp = NFMT.User.UserProvider.Corporations.SingleOrDefault(a => a.CorpId == apply.ApplyCorp);
                if (corp == null)
                    Response.Redirect(redirectURL);

                NFMT.User.Model.Employee emp = NFMT.User.UserProvider.Employees.SingleOrDefault(a => a.EmpId == apply.EmpId);
                if (emp == null)
                    Response.Redirect(redirectURL);

                this.txbApplyDept.InnerText = dept.DeptName;
                this.txbMemo.InnerText = apply.ApplyDesc;
                this.txbApplyCorp.InnerText = corp.CorpName;
                this.txbApplyPerson.InnerText = emp.Name;

                string json = serializer.Serialize(apply);
                this.hidmodel.Value = json;
            }
        }
    }
}