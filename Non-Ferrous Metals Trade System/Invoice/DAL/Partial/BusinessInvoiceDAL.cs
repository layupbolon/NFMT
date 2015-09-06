using NFMT.Common;
using NFMT.DBUtility;
using NFMT.Invoice.IDAL;
using NFMT.Invoice.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Invoice.DAL
{
    public partial class BusinessInvoiceDAL : ExecOperate, IBusinessInvoiceDAL
    {
        #region 新增方法

        public ResultModel GetByInvoiceId(UserModel user, int invoiceId)
        {
            ResultModel result = new ResultModel();

            if (invoiceId < 1)
            {
                result.Message = "序号不能小于1";
                return result;
            }

            List<SqlParameter> paras = new List<SqlParameter>();
            SqlParameter para = new SqlParameter("@invoiceId", SqlDbType.Int, 4);
            para.Value = invoiceId;
            paras.Add(para);

            SqlDataReader dr = null;

            try
            {
                string cmdText = "select * from dbo.Inv_BusinessInvoice where InvoiceId=@invoiceId";
                dr = SqlHelper.ExecuteReader(ConnectString, CommandType.Text, cmdText, paras.ToArray());

                IModel businessinvoice = null;

                if (dr.Read())
                {
                    businessinvoice = this.CreateModel(dr);

                    result.AffectCount = 1;
                    result.Message = "读取成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = businessinvoice;
                }
                else
                {
                    result.Message = "读取失败或无数据";
                    result.AffectCount = 0;
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            finally
            {
                if (dr != null)
                    dr.Dispose();
            }
            return result;
        }

        public ResultModel Load(UserModel user, int refInvoiceId)
        {
            ResultModel result = new ResultModel();
            try
            {
                string cmdText = string.Format("select * from dbo.Inv_BusinessInvoice where RefInvoiceId ={0}", refInvoiceId);

                DataTable dt = SqlHelper.ExecuteDataTable(ConnectString, cmdText, null, CommandType.Text);

                List<BusinessInvoice> businessInvoices = new List<BusinessInvoice>();

                foreach (DataRow dr in dt.Rows)
                {
                    BusinessInvoice businessinvoice = new BusinessInvoice();
                    businessinvoice.BusinessInvoiceId = Convert.ToInt32(dr["BusinessInvoiceId"]);

                    if (dr["InvoiceId"] != DBNull.Value)
                    {
                        businessinvoice.InvoiceId = Convert.ToInt32(dr["InvoiceId"]);
                    }
                    if (dr["RefInvoiceId"] != DBNull.Value)
                    {
                        businessinvoice.RefInvoiceId = Convert.ToInt32(dr["RefInvoiceId"]);
                    }
                    if (dr["ContractId"] != DBNull.Value)
                    {
                        businessinvoice.ContractId = Convert.ToInt32(dr["ContractId"]);
                    }
                    if (dr["SubContractId"] != DBNull.Value)
                    {
                        businessinvoice.SubContractId = Convert.ToInt32(dr["SubContractId"]);
                    }
                    if (dr["AssetId"] != DBNull.Value)
                    {
                        businessinvoice.AssetId = Convert.ToInt32(dr["AssetId"]);
                    }
                    if (dr["IntegerAmount"] != DBNull.Value)
                    {
                        businessinvoice.IntegerAmount = Convert.ToDecimal(dr["IntegerAmount"]);
                    }
                    if (dr["NetAmount"] != DBNull.Value)
                    {
                        businessinvoice.NetAmount = Convert.ToDecimal(dr["NetAmount"]);
                    }
                    if (dr["UnitPrice"] != DBNull.Value)
                    {
                        businessinvoice.UnitPrice = Convert.ToDecimal(dr["UnitPrice"]);
                    }
                    if (dr["MUId"] != DBNull.Value)
                    {
                        businessinvoice.MUId = Convert.ToInt32(dr["MUId"]);
                    }
                    if (dr["MarginRatio"] != DBNull.Value)
                    {
                        businessinvoice.MarginRatio = Convert.ToDecimal(dr["MarginRatio"]);
                    }
                    if (dr["VATRatio"] != DBNull.Value)
                    {
                        businessinvoice.VATRatio = Convert.ToDecimal(dr["VATRatio"]);
                    }
                    if (dr["VATBala"] != DBNull.Value)
                    {
                        businessinvoice.VATBala = Convert.ToDecimal(dr["VATBala"]);
                    }
                    businessInvoices.Add(businessinvoice);
                }
                result.AffectCount = dt.Rows.Count;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = businessInvoices;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel LoadBySubId(UserModel user, int subId, NFMT.Invoice.InvoiceTypeEnum invoiceType)
        {
            ResultModel result = new ResultModel();

            SqlDataReader dr = null;
            try
            {
                int entryStatus = (int)NFMT.Common.StatusEnum.已录入;

                string cmdText = string.Format("select bi.* from dbo.Inv_BusinessInvoice bi inner join dbo.Invoice inv on bi.InvoiceId = inv.InvoiceId where bi.SubContractId ={0} and inv.InvoiceType = {1} and inv.InvoiceStatus>={2}", subId, (int)invoiceType, entryStatus);
                dr = SqlHelper.ExecuteReader(ConnectString, CommandType.Text, cmdText, null);

                List<Model.BusinessInvoice> models = new List<BusinessInvoice>();

                int i = 0;
                while (dr.Read())
                {
                    models.Add(CreateModel<BusinessInvoice>(dr));
                    i++;
                }

                result.AffectCount = i;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = models;
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }
            finally
            {
                if (dr != null)
                    dr.Dispose();
            }

            return result;
        }

        public override IAuthority Authority
        {
            get
            {
                NFMT.Authority.CorpAuth auth = new Authority.CorpAuth();
                auth.AuthColumnNames.Add("case when inv.InvoiceDirection = 34 then inv.OutCorpId when inv.InvoiceDirection=33 then inv.InCorpId end");

                return auth;
            }
        }

        public override int MenuId
        {
            get
            {
                return 61;
            }
        }

        public ResultModel CheckContractSubBusinessInvoiceApplyConfirm(UserModel user, int subId)
        {
            ResultModel result = new ResultModel();

            string cmdText = "select COUNT(*) from dbo.Inv_BusinessInvoice bi inner join dbo.Invoice inv on inv.InvoiceId = bi.InvoiceId where bi.SubContractId = 1 and inv.InvoiceStatus in (@entryStatus,@readyStatus)";

            SqlParameter[] paras = new SqlParameter[3];
            paras[0] = new SqlParameter("@subId", subId);
            paras[1] = new SqlParameter("@entryStatus", (int)NFMT.Common.StatusEnum.已录入);
            paras[1] = new SqlParameter("@readyStatus", (int)NFMT.Common.StatusEnum.已生效);

            object obj = DBUtility.SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, cmdText, paras);
            int i = 0;
            if (obj == null || !int.TryParse(obj.ToString(), out i) || i <= 0)
            {
                result.ResultStatus = -1;
                result.Message = "检验业务发票失败";
                return result;
            }
            if (i > 0)
            {
                result.ResultStatus = -1;
                result.Message = "子合约中含有未完成的业务发票，不能进行确认完成操作。";
                return result;
            }

            result.ResultStatus = 0;
            result.Message = "业务发票全部完成";

            return result;
        }

        public ResultModel GetLastGrossAmount(UserModel user, int stockLogId)
        {
            ResultModel result = new ResultModel();

            string cmdText = string.Format("select isnull(sl.GrossAmount,0) - ISNULL(invDetail.IntegerAmount,0) as LastAmount from dbo.St_StockLog sl left join (select SUM(bid.IntegerAmount) as IntegerAmount,StockLogId from dbo.Inv_BusinessInvoiceDetail bid where bid.StockLogId ={0} and bid.DetailStatus>={1} group by StockLogId) as invDetail on invDetail.StockLogId = sl.StockLogId where sl.StockLogId = {0} and sl.LogStatus >= {2}",stockLogId,(int)NFMT.Common.StatusEnum.已录入,(int)Common.StatusEnum.已生效);

            object obj = SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, cmdText, null);

            decimal lastAmount = 0;
            if (obj != null && decimal.TryParse(obj.ToString(), out lastAmount) && lastAmount >= 0)
            {
                result.ResultStatus = 0;
                result.ReturnValue = lastAmount;
                result.Message = "获取成功";
            }

            return result;
        }

        #endregion
    }
}
