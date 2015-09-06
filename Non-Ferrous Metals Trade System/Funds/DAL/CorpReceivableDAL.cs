/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CorpReceivableDAL.cs
// 文件功能描述：收款分配至集团或公司dbo.Fun_CorpReceivable_Ref数据交互类。
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
    /// 收款分配至集团或公司dbo.Fun_CorpReceivable_Ref数据交互类。
    /// </summary>
    public class CorpReceivableDAL : ExecOperate, ICorpReceivableDAL
    {
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public CorpReceivableDAL()
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
            CorpReceivable fun_corpreceivable_ref = (CorpReceivable)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@RefId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter allotidpara = new SqlParameter("@AllotId", SqlDbType.Int, 4);
            allotidpara.Value = fun_corpreceivable_ref.AllotId;
            paras.Add(allotidpara);

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = fun_corpreceivable_ref.DetailId;
            paras.Add(detailidpara);

            SqlParameter blocidpara = new SqlParameter("@BlocId", SqlDbType.Int, 4);
            blocidpara.Value = fun_corpreceivable_ref.BlocId;
            paras.Add(blocidpara);

            SqlParameter corpidpara = new SqlParameter("@CorpId", SqlDbType.Int, 4);
            corpidpara.Value = fun_corpreceivable_ref.CorpId;
            paras.Add(corpidpara);

            SqlParameter recidpara = new SqlParameter("@RecId", SqlDbType.Int, 4);
            recidpara.Value = fun_corpreceivable_ref.RecId;
            paras.Add(recidpara);

            SqlParameter issharepara = new SqlParameter("@IsShare", SqlDbType.Bit, 1);
            issharepara.Value = fun_corpreceivable_ref.IsShare;
            paras.Add(issharepara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            CorpReceivable corpreceivable = new CorpReceivable();

            int indexRefId = dr.GetOrdinal("RefId");
            corpreceivable.RefId = Convert.ToInt32(dr[indexRefId]);

            int indexAllotId = dr.GetOrdinal("AllotId");
            if (dr["AllotId"] != DBNull.Value)
            {
                corpreceivable.AllotId = Convert.ToInt32(dr[indexAllotId]);
            }

            int indexDetailId = dr.GetOrdinal("DetailId");
            if (dr["DetailId"] != DBNull.Value)
            {
                corpreceivable.DetailId = Convert.ToInt32(dr[indexDetailId]);
            }

            int indexBlocId = dr.GetOrdinal("BlocId");
            if (dr["BlocId"] != DBNull.Value)
            {
                corpreceivable.BlocId = Convert.ToInt32(dr[indexBlocId]);
            }

            int indexCorpId = dr.GetOrdinal("CorpId");
            if (dr["CorpId"] != DBNull.Value)
            {
                corpreceivable.CorpId = Convert.ToInt32(dr[indexCorpId]);
            }

            int indexRecId = dr.GetOrdinal("RecId");
            if (dr["RecId"] != DBNull.Value)
            {
                corpreceivable.RecId = Convert.ToInt32(dr[indexRecId]);
            }

            int indexIsShare = dr.GetOrdinal("IsShare");
            if (dr["IsShare"] != DBNull.Value)
            {
                corpreceivable.IsShare = Convert.ToBoolean(dr[indexIsShare]);
            }


            return corpreceivable;
        }

        public override string TableName
        {
            get
            {
                return "Fun_CorpReceivable_Ref";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            CorpReceivable fun_corpreceivable_ref = (CorpReceivable)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter refidpara = new SqlParameter("@RefId", SqlDbType.Int, 4);
            refidpara.Value = fun_corpreceivable_ref.RefId;
            paras.Add(refidpara);

            SqlParameter allotidpara = new SqlParameter("@AllotId", SqlDbType.Int, 4);
            allotidpara.Value = fun_corpreceivable_ref.AllotId;
            paras.Add(allotidpara);

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = fun_corpreceivable_ref.DetailId;
            paras.Add(detailidpara);

            SqlParameter blocidpara = new SqlParameter("@BlocId", SqlDbType.Int, 4);
            blocidpara.Value = fun_corpreceivable_ref.BlocId;
            paras.Add(blocidpara);

            SqlParameter corpidpara = new SqlParameter("@CorpId", SqlDbType.Int, 4);
            corpidpara.Value = fun_corpreceivable_ref.CorpId;
            paras.Add(corpidpara);

            SqlParameter recidpara = new SqlParameter("@RecId", SqlDbType.Int, 4);
            recidpara.Value = fun_corpreceivable_ref.RecId;
            paras.Add(recidpara);

            SqlParameter issharepara = new SqlParameter("@IsShare", SqlDbType.Bit, 1);
            issharepara.Value = fun_corpreceivable_ref.IsShare;
            paras.Add(issharepara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel Load(UserModel user,int allotId,NFMT.Common.StatusEnum status = NFMT.Common.StatusEnum.已生效)
        {
            ResultModel result = new ResultModel();
            try
            {
                string cmdText = string.Format("select crr.* from dbo.Fun_CorpReceivable_Ref crr inner join dbo.Fun_RecAllotDetail rad on crr.DetailId = crr.DetailId and rad.DetailStatus>={0} where crr.AllotId ={1}",(int)status,allotId);
                DataTable dt = SqlHelper.ExecuteDataTable(ConnectString,cmdText, null, CommandType.Text);

                List<CorpReceivable> corpReceivables = new List<CorpReceivable>();

                foreach (DataRow dr in dt.Rows)
                {
                    CorpReceivable corpreceivable = new CorpReceivable();
                    corpreceivable.RefId = Convert.ToInt32(dr["RefId"]);

                    if (dr["AllotId"] != DBNull.Value)
                    {
                        corpreceivable.AllotId = Convert.ToInt32(dr["AllotId"]);
                    }
                    if (dr["DetailId"] != DBNull.Value)
                    {
                        corpreceivable.DetailId = Convert.ToInt32(dr["DetailId"]);
                    }
                    if (dr["BlocId"] != DBNull.Value)
                    {
                        corpreceivable.BlocId = Convert.ToInt32(dr["BlocId"]);
                    }
                    if (dr["CorpId"] != DBNull.Value)
                    {
                        corpreceivable.CorpId = Convert.ToInt32(dr["CorpId"]);
                    }
                    if (dr["RecId"] != DBNull.Value)
                    {
                        corpreceivable.RecId = Convert.ToInt32(dr["RecId"]);
                    }
                    if (dr["IsShare"] != DBNull.Value)
                    {
                        corpreceivable.IsShare = Convert.ToBoolean(dr["IsShare"]);
                    }
                    corpReceivables.Add(corpreceivable);
                }
                result.AffectCount = dt.Rows.Count;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = corpReceivables;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel GetCorpId(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select top 1 CorpId from dbo.Fun_CorpReceivable_Ref where AllotId = {0}", allotId);
                object obj = NFMT.DBUtility.SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, sql, null);
                int stockId = 0;
                if (obj != null && int.TryParse(obj.ToString(), out stockId) && stockId > 0)
                {
                    result.ResultStatus = 0;
                    result.ReturnValue = stockId;
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
                string sql = string.Format("select RecId from dbo.Fun_CorpReceivable_Ref where AllotId = {0} and RefStatus = {1}", allotId, (int)Common.StatusEnum.已生效);
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

        public ResultModel GetRowsDetail(UserModel user, int allotId,Common.StatusEnum status = StatusEnum.已生效)
        {
            ResultModel result = new ResultModel();
            try
            {  
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("select ");
                sb.Append(" ref.RecId as ReceivableId,r.ReceiveDate,c.CorpName as InnerCorp,b.BankName,r.PayCorpName as OutCorp ");
                sb.Append(" ,CONVERT(varchar,r.PayBala)+cu.CurrencyName as PayBala ");
                sb.Append(" ,rad.AllotBala as CanAllotBala ");

                sb.Append("from  dbo.Fun_CorpReceivable_Ref ref  ");
                sb.Append(" inner join dbo.Fun_RecAllotDetail rad on ref.DetailId = rad.DetailId ");
                sb.Append(" left join dbo.Fun_Receivable r on ref.RecId = r.ReceivableId ");
                sb.Append(" left join NFMT_Basic.dbo.Currency cu on r.CurrencyId = cu.CurrencyId ");
                sb.Append(" left join NFMT_User.dbo.Corporation c on r.ReceivableCorpId = c.CorpId ");
                sb.Append(" left join NFMT_Basic.dbo.Bank b on r.ReceivableBank = b.BankId ");
                sb.AppendFormat("where rad.DetailStatus = {0} and ref.AllotId = {1} ", (int)status, allotId);
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
                string sql = string.Format("update dbo.Fun_CorpReceivable_Ref set RefStatus = {0} where AllotId = {1}", (int)Common.StatusEnum.已作废, allotId);
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

        public ResultModel GetIsShare(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select top 1 IsShare from dbo.Fun_CorpReceivable_Ref where AllotId = {0}", allotId);
                object obj = NFMT.DBUtility.SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, sql, null);
                bool isShare = false;
                if (obj != null && bool.TryParse(obj.ToString(), out isShare))
                {
                    result.ResultStatus = 0;
                    result.ReturnValue = isShare;
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

        public ResultModel GetCorpReceivableByAllotId(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select * from Fun_CorpReceivable_Ref where AllotId = {0} and RefStatus = {1}", allotId, (int)Common.StatusEnum.已生效);
                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sql, null, CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<CorpReceivable> corpReceivables = new List<CorpReceivable>();

                    foreach (DataRow dr in dt.Rows)
                    {
                        CorpReceivable corpreceivable = new CorpReceivable();
                        corpreceivable.RefId = Convert.ToInt32(dr["RefId"]);

                        if (dr["AllotId"] != DBNull.Value)
                        {
                            corpreceivable.AllotId = Convert.ToInt32(dr["AllotId"]);
                        }
                        if (dr["DetailId"] != DBNull.Value)
                        {
                            corpreceivable.DetailId = Convert.ToInt32(dr["DetailId"]);
                        }
                        if (dr["BlocId"] != DBNull.Value)
                        {
                            corpreceivable.BlocId = Convert.ToInt32(dr["BlocId"]);
                        }
                        if (dr["CorpId"] != DBNull.Value)
                        {
                            corpreceivable.CorpId = Convert.ToInt32(dr["CorpId"]);
                        }
                        if (dr["RecId"] != DBNull.Value)
                        {
                            corpreceivable.RecId = Convert.ToInt32(dr["RecId"]);
                        }
                        if (dr["IsShare"] != DBNull.Value)
                        {
                            corpreceivable.IsShare = Convert.ToBoolean(dr["IsShare"]);
                        }
                        corpReceivables.Add(corpreceivable);
                    }
                    result.AffectCount = dt.Rows.Count;
                    result.Message = "获取列表成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = corpReceivables;
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
