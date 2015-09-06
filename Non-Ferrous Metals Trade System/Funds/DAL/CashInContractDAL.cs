/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CashInContractDAL.cs
// 文件功能描述：收款分配至合约dbo.Fun_CashInContract_Ref数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年9月19日
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
    /// 收款分配至合约dbo.Fun_CashInContract_Ref数据交互类。
    /// </summary>
    public class CashInContractDAL : DetailOperate, ICashInContractDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CashInContractDAL()
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
            CashInContract fun_cashincontract_ref = (CashInContract)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@RefId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter corprefidpara = new SqlParameter("@CorpRefId", SqlDbType.Int, 4);
            corprefidpara.Value = fun_cashincontract_ref.CorpRefId;
            paras.Add(corprefidpara);

            SqlParameter allotidpara = new SqlParameter("@AllotId", SqlDbType.Int, 4);
            allotidpara.Value = fun_cashincontract_ref.AllotId;
            paras.Add(allotidpara);

            SqlParameter cashinidpara = new SqlParameter("@CashInId", SqlDbType.Int, 4);
            cashinidpara.Value = fun_cashincontract_ref.CashInId;
            paras.Add(cashinidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = fun_cashincontract_ref.ContractId;
            paras.Add(contractidpara);

            SqlParameter subcontractidpara = new SqlParameter("@SubContractId", SqlDbType.Int, 4);
            subcontractidpara.Value = fun_cashincontract_ref.SubContractId;
            paras.Add(subcontractidpara);

            SqlParameter allotbalapara = new SqlParameter("@AllotBala", SqlDbType.Decimal, 9);
            allotbalapara.Value = fun_cashincontract_ref.AllotBala;
            paras.Add(allotbalapara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = (int)Common.StatusEnum.已生效;
            paras.Add(detailstatuspara);

            SqlParameter fundslogidpara = new SqlParameter("@FundsLogId", SqlDbType.Int, 4);
            fundslogidpara.Value = fun_cashincontract_ref.FundsLogId;
            paras.Add(fundslogidpara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            CashInContract cashincontract = new CashInContract();

            cashincontract.RefId = Convert.ToInt32(dr["RefId"]);

            if (dr["CorpRefId"] != DBNull.Value)
            {
                cashincontract.CorpRefId = Convert.ToInt32(dr["CorpRefId"]);
            }

            if (dr["AllotId"] != DBNull.Value)
            {
                cashincontract.AllotId = Convert.ToInt32(dr["AllotId"]);
            }

            if (dr["CashInId"] != DBNull.Value)
            {
                cashincontract.CashInId = Convert.ToInt32(dr["CashInId"]);
            }

            if (dr["ContractId"] != DBNull.Value)
            {
                cashincontract.ContractId = Convert.ToInt32(dr["ContractId"]);
            }

            if (dr["SubContractId"] != DBNull.Value)
            {
                cashincontract.SubContractId = Convert.ToInt32(dr["SubContractId"]);
            }

            if (dr["AllotBala"] != DBNull.Value)
            {
                cashincontract.AllotBala = Convert.ToDecimal(dr["AllotBala"]);
            }

            if (dr["DetailStatus"] != DBNull.Value)
            {
                cashincontract.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr["DetailStatus"]);
            }

            if (dr["FundsLogId"] != DBNull.Value)
            {
                cashincontract.FundsLogId = Convert.ToInt32(dr["FundsLogId"]);
            }


            return cashincontract;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            CashInContract cashincontract = new CashInContract();

            int indexRefId = dr.GetOrdinal("RefId");
            cashincontract.RefId = Convert.ToInt32(dr[indexRefId]);

            int indexCorpRefId = dr.GetOrdinal("CorpRefId");
            if (dr["CorpRefId"] != DBNull.Value)
            {
                cashincontract.CorpRefId = Convert.ToInt32(dr[indexCorpRefId]);
            }

            int indexAllotId = dr.GetOrdinal("AllotId");
            if (dr["AllotId"] != DBNull.Value)
            {
                cashincontract.AllotId = Convert.ToInt32(dr[indexAllotId]);
            }

            int indexCashInId = dr.GetOrdinal("CashInId");
            if (dr["CashInId"] != DBNull.Value)
            {
                cashincontract.CashInId = Convert.ToInt32(dr[indexCashInId]);
            }

            int indexContractId = dr.GetOrdinal("ContractId");
            if (dr["ContractId"] != DBNull.Value)
            {
                cashincontract.ContractId = Convert.ToInt32(dr[indexContractId]);
            }

            int indexSubContractId = dr.GetOrdinal("SubContractId");
            if (dr["SubContractId"] != DBNull.Value)
            {
                cashincontract.SubContractId = Convert.ToInt32(dr[indexSubContractId]);
            }

            int indexAllotBala = dr.GetOrdinal("AllotBala");
            if (dr["AllotBala"] != DBNull.Value)
            {
                cashincontract.AllotBala = Convert.ToDecimal(dr[indexAllotBala]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                cashincontract.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }

            int indexFundsLogId = dr.GetOrdinal("FundsLogId");
            if (dr["FundsLogId"] != DBNull.Value)
            {
                cashincontract.FundsLogId = Convert.ToInt32(dr[indexFundsLogId]);
            }


            return cashincontract;
        }

        public override string TableName
        {
            get
            {
                return "Fun_CashInContract_Ref";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            CashInContract fun_cashincontract_ref = (CashInContract)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter refidpara = new SqlParameter("@RefId", SqlDbType.Int, 4);
            refidpara.Value = fun_cashincontract_ref.RefId;
            paras.Add(refidpara);

            SqlParameter corprefidpara = new SqlParameter("@CorpRefId", SqlDbType.Int, 4);
            corprefidpara.Value = fun_cashincontract_ref.CorpRefId;
            paras.Add(corprefidpara);

            SqlParameter allotidpara = new SqlParameter("@AllotId", SqlDbType.Int, 4);
            allotidpara.Value = fun_cashincontract_ref.AllotId;
            paras.Add(allotidpara);

            SqlParameter cashinidpara = new SqlParameter("@CashInId", SqlDbType.Int, 4);
            cashinidpara.Value = fun_cashincontract_ref.CashInId;
            paras.Add(cashinidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = fun_cashincontract_ref.ContractId;
            paras.Add(contractidpara);

            SqlParameter subcontractidpara = new SqlParameter("@SubContractId", SqlDbType.Int, 4);
            subcontractidpara.Value = fun_cashincontract_ref.SubContractId;
            paras.Add(subcontractidpara);

            SqlParameter allotbalapara = new SqlParameter("@AllotBala", SqlDbType.Decimal, 9);
            allotbalapara.Value = fun_cashincontract_ref.AllotBala;
            paras.Add(allotbalapara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = fun_cashincontract_ref.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter fundslogidpara = new SqlParameter("@FundsLogId", SqlDbType.Int, 4);
            fundslogidpara.Value = fun_cashincontract_ref.FundsLogId;
            paras.Add(fundslogidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel Load(UserModel user, int subId, StatusEnum status = StatusEnum.已生效)
        {
            string cmdText = string.Format("select * from dbo.Fun_CashInContract_Ref where SubContractId = {0} and DetailStatus={1}", subId, (int)status);
            return Load<Model.CashInContract>(user, CommandType.Text, cmdText);
        }

        public ResultModel LoadByCorpRefId(UserModel user, int corpRefId, StatusEnum status = StatusEnum.已生效)
        {
            ResultModel result = new ResultModel();
            try
            {
                string cmdText = string.Format("select * from dbo.Fun_CashInContract_Ref where CorpRefId = {0} and DetailStatus={1}", corpRefId, (int)status);
                DataTable dt = SqlHelper.ExecuteDataTable(ConnectString, cmdText, null, CommandType.Text);

                List<CashInContract> cashInContracts = new List<CashInContract>();

                foreach (DataRow dr in dt.Rows)
                {
                    CashInContract cashincontract = new CashInContract();
                    cashincontract.RefId = Convert.ToInt32(dr["RefId"]);

                    if (dr["CorpRefId"] != DBNull.Value)
                    {
                        cashincontract.CorpRefId = Convert.ToInt32(dr["CorpRefId"]);
                    }
                    if (dr["AllotId"] != DBNull.Value)
                    {
                        cashincontract.AllotId = Convert.ToInt32(dr["AllotId"]);
                    }
                    if (dr["CashInId"] != DBNull.Value)
                    {
                        cashincontract.CashInId = Convert.ToInt32(dr["CashInId"]);
                    }
                    if (dr["ContractId"] != DBNull.Value)
                    {
                        cashincontract.ContractId = Convert.ToInt32(dr["ContractId"]);
                    }
                    if (dr["SubContractId"] != DBNull.Value)
                    {
                        cashincontract.SubContractId = Convert.ToInt32(dr["SubContractId"]);
                    }
                    if (dr["AllotBala"] != DBNull.Value)
                    {
                        cashincontract.AllotBala = Convert.ToDecimal(dr["AllotBala"]);
                    }
                    if (dr["DetailStatus"] != DBNull.Value)
                    {
                        cashincontract.DetailStatus = (Common.StatusEnum)Enum.Parse(typeof(Common.StatusEnum), dr["DetailStatus"].ToString());
                    }
                    cashInContracts.Add(cashincontract);
                }
                result.AffectCount = dt.Rows.Count;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = cashInContracts;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel LoadByAllot(UserModel user, int allotId, StatusEnum status = StatusEnum.已生效)
        {
            ResultModel result = new ResultModel();
            try
            {
                string cmdText = string.Format("select * from dbo.Fun_CashInContract_Ref where AllotId = {0} and DetailStatus={1}", allotId, (int)status);
                DataTable dt = SqlHelper.ExecuteDataTable(ConnectString, cmdText, null, CommandType.Text);

                List<CashInContract> cashInContracts = new List<CashInContract>();

                foreach (DataRow dr in dt.Rows)
                {
                    CashInContract cashincontract = new CashInContract();
                    cashincontract.RefId = Convert.ToInt32(dr["RefId"]);

                    if (dr["CorpRefId"] != DBNull.Value)
                    {
                        cashincontract.CorpRefId = Convert.ToInt32(dr["CorpRefId"]);
                    }
                    if (dr["AllotId"] != DBNull.Value)
                    {
                        cashincontract.AllotId = Convert.ToInt32(dr["AllotId"]);
                    }
                    if (dr["CashInId"] != DBNull.Value)
                    {
                        cashincontract.CashInId = Convert.ToInt32(dr["CashInId"]);
                    }
                    if (dr["ContractId"] != DBNull.Value)
                    {
                        cashincontract.ContractId = Convert.ToInt32(dr["ContractId"]);
                    }
                    if (dr["SubContractId"] != DBNull.Value)
                    {
                        cashincontract.SubContractId = Convert.ToInt32(dr["SubContractId"]);
                    }
                    if (dr["AllotBala"] != DBNull.Value)
                    {
                        cashincontract.AllotBala = Convert.ToDecimal(dr["AllotBala"]);
                    }
                    if (dr["DetailStatus"] != DBNull.Value)
                    {
                        cashincontract.DetailStatus = (Common.StatusEnum)Enum.Parse(typeof(Common.StatusEnum), dr["DetailStatus"].ToString());
                    }
                    cashInContracts.Add(cashincontract);
                }
                result.AffectCount = dt.Rows.Count;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = cashInContracts;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel LoadDetail(UserModel user, int allotId, StatusEnum status = StatusEnum.已生效)
        {
            string cmdText = string.Format("select * from dbo.Fun_CashInContract_Ref where AllotId = {0} and DetailStatus={1}", allotId, (int)status);
            return Load<Model.CashInContract>(user, CommandType.Text, cmdText);
        }

        public override int MenuId
        {
            get
            {
                return 57;
            }
        }

        public ResultModel InvalidAll(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update dbo.Fun_CashInContract_Ref set DetailStatus = {0} where AllotId = {1}", (int)Common.StatusEnum.已作废, allotId);
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);
                if (i > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "作废成功";
                }
                else
                {
                    result.Message = "作废失败";
                    result.ResultStatus = -1;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }

            return result;
        }

        #endregion
    }
}
