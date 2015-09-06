/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ContractReceivableDAL.cs
// 文件功能描述：收款分配至合约dbo.Fun_ContractReceivable_Ref数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年9月16日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.Funds.Model;
using NFMT.DBUtility;
using NFMT.Funds.IDAL;
using NFMT.Common;

namespace NFMT.Funds.DAL
{
    /// <summary>
    /// 收款分配至合约dbo.Fun_ContractReceivable_Ref数据交互类。
    /// </summary>
    public class ContractReceivableDAL : ExecOperate , IContractReceivableDAL
    {
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public ContractReceivableDAL()
		{
		}
        
		#endregion

        #region 数据库操作

        public override string ConnectString
        {
            get
            {
                return NFMT.DBUtility.SqlHelper.ConnectionStringNFMT;
            }
        }

        public override List<SqlParameter> CreateInsertParameters(IModel obj, ref SqlParameter returnValue)
        {
            ContractReceivable fun_contractreceivable_ref = (ContractReceivable)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@RefId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter corprefidpara = new SqlParameter("@CorpRefId", SqlDbType.Int, 4);
            corprefidpara.Value = fun_contractreceivable_ref.CorpRefId;
            paras.Add(corprefidpara);

            SqlParameter allotidpara = new SqlParameter("@AllotId", SqlDbType.Int, 4);
            allotidpara.Value = fun_contractreceivable_ref.AllotId;
            paras.Add(allotidpara);

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = fun_contractreceivable_ref.DetailId;
            paras.Add(detailidpara);

            SqlParameter recidpara = new SqlParameter("@RecId", SqlDbType.Int, 4);
            recidpara.Value = fun_contractreceivable_ref.RecId;
            paras.Add(recidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = fun_contractreceivable_ref.ContractId;
            paras.Add(contractidpara);

            SqlParameter subcontractidpara = new SqlParameter("@SubContractId", SqlDbType.Int, 4);
            subcontractidpara.Value = fun_contractreceivable_ref.SubContractId;
            paras.Add(subcontractidpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            ContractReceivable contractreceivable = new ContractReceivable();

            int indexRefId = dr.GetOrdinal("RefId");
            contractreceivable.RefId = Convert.ToInt32(dr[indexRefId]);

            int indexCorpRefId = dr.GetOrdinal("CorpRefId");
            if (dr["CorpRefId"] != DBNull.Value)
            {
                contractreceivable.CorpRefId = Convert.ToInt32(dr[indexCorpRefId]);
            }

            int indexAllotId = dr.GetOrdinal("AllotId");
            if (dr["AllotId"] != DBNull.Value)
            {
                contractreceivable.AllotId = Convert.ToInt32(dr[indexAllotId]);
            }

            int indexDetailId = dr.GetOrdinal("DetailId");
            if (dr["DetailId"] != DBNull.Value)
            {
                contractreceivable.DetailId = Convert.ToInt32(dr[indexDetailId]);
            }

            int indexRecId = dr.GetOrdinal("RecId");
            if (dr["RecId"] != DBNull.Value)
            {
                contractreceivable.RecId = Convert.ToInt32(dr[indexRecId]);
            }

            int indexContractId = dr.GetOrdinal("ContractId");
            if (dr["ContractId"] != DBNull.Value)
            {
                contractreceivable.ContractId = Convert.ToInt32(dr[indexContractId]);
            }

            int indexSubContractId = dr.GetOrdinal("SubContractId");
            if (dr["SubContractId"] != DBNull.Value)
            {
                contractreceivable.SubContractId = Convert.ToInt32(dr[indexSubContractId]);
            }


            return contractreceivable;
        }

        public override string TableName
        {
            get
            {
                return "Fun_ContractReceivable_Ref";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            ContractReceivable fun_contractreceivable_ref = (ContractReceivable)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter refidpara = new SqlParameter("@RefId", SqlDbType.Int, 4);
            refidpara.Value = fun_contractreceivable_ref.RefId;
            paras.Add(refidpara);

            SqlParameter corprefidpara = new SqlParameter("@CorpRefId", SqlDbType.Int, 4);
            corprefidpara.Value = fun_contractreceivable_ref.CorpRefId;
            paras.Add(corprefidpara);

            SqlParameter allotidpara = new SqlParameter("@AllotId", SqlDbType.Int, 4);
            allotidpara.Value = fun_contractreceivable_ref.AllotId;
            paras.Add(allotidpara);

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = fun_contractreceivable_ref.DetailId;
            paras.Add(detailidpara);

            SqlParameter recidpara = new SqlParameter("@RecId", SqlDbType.Int, 4);
            recidpara.Value = fun_contractreceivable_ref.RecId;
            paras.Add(recidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = fun_contractreceivable_ref.ContractId;
            paras.Add(contractidpara);

            SqlParameter subcontractidpara = new SqlParameter("@SubContractId", SqlDbType.Int, 4);
            subcontractidpara.Value = fun_contractreceivable_ref.SubContractId;
            paras.Add(subcontractidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetSubId(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select top 1 SubContractId from dbo.Fun_ContractReceivable_Ref where AllotId = {0}", allotId);
                object obj = NFMT.DBUtility.SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, sql, null);
                int subId = 0;
                if (obj != null && int.TryParse(obj.ToString(), out subId) && subId > 0)
                {
                    result.ResultStatus = 0;
                    result.ReturnValue = subId;
                    result.Message = "获取成功";
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "获取失败";
                }

            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }
            return result;
        }

        public ResultModel GetReceIds(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();
            string receIds = string.Empty;

            try
            {
                string sql = string.Format("select RecId from dbo.Fun_ContractReceivable_Ref where AllotId = {0} and RefStatus = {1}", allotId, (int)Common.StatusEnum.已生效);
                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sql, null, CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        receIds += dr["RecId"] + ",";
                    }
                    if (!string.IsNullOrEmpty(receIds))
                        receIds = receIds.Substring(0, receIds.Length - 1);

                    result.ReturnValue = receIds;
                    result.Message = "获取成功";
                    result.ResultStatus = 0;
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "获取失败";
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }

            return result;
        }

        public ResultModel GetRowsDetail(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("select ref.RecId as ReceivableId,r.ReceiveDate,c.CorpName as InnerCorp,b.BankName,r.PayCorpName as OutCorp,CONVERT(varchar,r.PayBala)+cu.CurrencyName as PayBala,ref.AllotBala as CanAllotBala,c3.CorpId as CorpCode,c3.CorpName as Corp ");
                sb.Append(" from  dbo.Fun_ContractReceivable_Ref ref  ");
                sb.Append(" left join dbo.Fun_Receivable r on ref.RecId = r.ReceivableId ");
                sb.Append(" left join NFMT_Basic.dbo.Currency cu on r.CurrencyId = cu.CurrencyId ");
                sb.Append(" left join NFMT_User.dbo.Corporation c on r.ReceivableCorpId = c.CorpId ");
                sb.Append(" left join NFMT_Basic.dbo.Bank b on r.ReceivableBank = b.BankId ");
                sb.Append(" left join NFMT_User.dbo.Corporation c2 on r.PayCorpId = c2.CorpId ");
                sb.Append(" left join dbo.Fun_CorpReceivable_Ref crr on crr.RefId = ref.CorpRefId ");
                sb.Append(" left join NFMT_User.dbo.Corporation c3 on crr.CorpId = c3.CorpId ");
                sb.AppendFormat(" where ref.RefStatus = {0} and ref.AllotId = {1} ", (int)Common.StatusEnum.已生效, allotId);
                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sb.ToString(), null, CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result.ResultStatus = 0;
                    result.ReturnValue = dt;
                    result.Message = "获取成功";
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "获取失败";
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }
            return result;
        }

        public ResultModel InvalidAll(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();
            try
            {
                string sql = string.Format("update dbo.Fun_ContractReceivable_Ref set RefStatus = {0} where AllotId = {1}", (int)Common.StatusEnum.已作废, allotId);
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);
                if (i > 0)
                {
                    result.ResultStatus = 0;
                    result.AffectCount = i;
                    result.Message = "作废成功";
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "作废失败";
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }
            return result;
        }

        public ResultModel GetRowsDetailByCorp(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("select ref.RefId,c.CorpName,crr.AllotBala as AllotBala,ref.AllotBala as CanAllotBala ");
                sb.Append(" from dbo.Fun_ContractReceivable_Ref ref  ");
                sb.Append(" left join dbo.Fun_CorpReceivable_Ref crr on ref.CorpRefId = crr.RefId ");
                sb.Append(" left join NFMT_User.dbo.Corporation c on crr.CorpId = c.CorpId ");
                sb.AppendFormat(" where ref.RefStatus = {0} and ref.AllotId = {1} ", (int)Common.StatusEnum.已生效, allotId);
                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sb.ToString(), null, CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result.ResultStatus = 0;
                    result.ReturnValue = dt;
                    result.Message = "获取成功";
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "获取失败";
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }
            return result;
        }

        public ResultModel GetCorpRefIds(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();
            string receIds = string.Empty;

            try
            {
                string sql = string.Format("select CorpRefId from dbo.Fun_ContractReceivable_Ref where AllotId = {0} and RefStatus = {1}", allotId, (int)Common.StatusEnum.已生效);
                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sql, null, CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        receIds += dr["CorpRefId"] + ",";
                    }
                    if (!string.IsNullOrEmpty(receIds))
                        receIds = receIds.Substring(0, receIds.Length - 1);

                    result.ReturnValue = receIds;
                    result.Message = "获取成功";
                    result.ResultStatus = 0;
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "获取失败";
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }

            return result;
        }

        public ResultModel GetContractReceivableByAllotId(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select * from Fun_ContractReceivable_Ref where AllotId = {0} and RefStatus = {1}", allotId, (int)Common.StatusEnum.已生效);
                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sql, null, CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<ContractReceivable> contractReceivables = new List<ContractReceivable>();

                    foreach (DataRow dr in dt.Rows)
                    {
                        ContractReceivable contractreceivable = new ContractReceivable();
                        contractreceivable.RefId = Convert.ToInt32(dr["RefId"]);

                        if (dr["CorpRefId"] != DBNull.Value)
                        {
                            contractreceivable.CorpRefId = Convert.ToInt32(dr["CorpRefId"]);
                        }
                        if (dr["AllotId"] != DBNull.Value)
                        {
                            contractreceivable.AllotId = Convert.ToInt32(dr["AllotId"]);
                        }
                        if (dr["DetailId"] != DBNull.Value)
                        {
                            contractreceivable.DetailId = Convert.ToInt32(dr["DetailId"]);
                        }
                        if (dr["RecId"] != DBNull.Value)
                        {
                            contractreceivable.RecId = Convert.ToInt32(dr["RecId"]);
                        }
                        if (dr["ContractId"] != DBNull.Value)
                        {
                            contractreceivable.ContractId = Convert.ToInt32(dr["ContractId"]);
                        }
                        if (dr["SubContractId"] != DBNull.Value)
                        {
                            contractreceivable.SubContractId = Convert.ToInt32(dr["SubContractId"]);
                        }
                        contractReceivables.Add(contractreceivable);
                    }
                    result.AffectCount = dt.Rows.Count;
                    result.Message = "获取列表成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = contractReceivables;
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "获取列表失败";
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }

            return result;
        }

        #endregion
    }
}
