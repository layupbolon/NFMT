/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ContractCorporationDetailDAL.cs
// 文件功能描述：合约抬头明细dbo.Con_ContractCorporationDetail数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年7月24日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.Contract.Model;
using NFMT.DBUtility;
using NFMT.Contract.IDAL;
using NFMT.Common;

namespace NFMT.Contract.DAL
{
    /// <summary>
    /// 合约抬头明细dbo.Con_ContractCorporationDetail数据交互类。
    /// </summary>
    public class ContractCorporationDetailDAL : ApplyOperate, IContractCorporationDetailDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ContractCorporationDetailDAL()
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
            ContractCorporationDetail con_contractcorporationdetail = (ContractCorporationDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@DetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = con_contractcorporationdetail.ContractId;
            paras.Add(contractidpara);

            SqlParameter corpidpara = new SqlParameter("@CorpId", SqlDbType.Int, 4);
            corpidpara.Value = con_contractcorporationdetail.CorpId;
            paras.Add(corpidpara);

            if (!string.IsNullOrEmpty(con_contractcorporationdetail.CorpName))
            {
                SqlParameter corpnamepara = new SqlParameter("@CorpName", SqlDbType.VarChar, 200);
                corpnamepara.Value = con_contractcorporationdetail.CorpName;
                paras.Add(corpnamepara);
            }

            SqlParameter deptidpara = new SqlParameter("@DeptId", SqlDbType.Int, 4);
            deptidpara.Value = con_contractcorporationdetail.DeptId;
            paras.Add(deptidpara);

            if (!string.IsNullOrEmpty(con_contractcorporationdetail.DeptName))
            {
                SqlParameter deptnamepara = new SqlParameter("@DeptName", SqlDbType.VarChar, 80);
                deptnamepara.Value = con_contractcorporationdetail.DeptName;
                paras.Add(deptnamepara);
            }

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = (int)Common.StatusEnum.已生效;
            paras.Add(detailstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);

            SqlParameter isdefaultcorppara = new SqlParameter("@IsDefaultCorp", SqlDbType.Bit, 1);
            isdefaultcorppara.Value = con_contractcorporationdetail.IsDefaultCorp;
            paras.Add(isdefaultcorppara);

            SqlParameter isinnercorppara = new SqlParameter("@IsInnerCorp", SqlDbType.Bit, 1);
            isinnercorppara.Value = con_contractcorporationdetail.IsInnerCorp;
            paras.Add(isinnercorppara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            ContractCorporationDetail contractcorporationdetail = new ContractCorporationDetail();

            int indexDetailId = dr.GetOrdinal("DetailId");
            contractcorporationdetail.DetailId = Convert.ToInt32(dr[indexDetailId]);

            int indexContractId = dr.GetOrdinal("ContractId");
            if (dr["ContractId"] != DBNull.Value)
            {
                contractcorporationdetail.ContractId = Convert.ToInt32(dr[indexContractId]);
            }

            int indexCorpId = dr.GetOrdinal("CorpId");
            if (dr["CorpId"] != DBNull.Value)
            {
                contractcorporationdetail.CorpId = Convert.ToInt32(dr[indexCorpId]);
            }

            int indexCorpName = dr.GetOrdinal("CorpName");
            if (dr["CorpName"] != DBNull.Value)
            {
                contractcorporationdetail.CorpName = Convert.ToString(dr[indexCorpName]);
            }

            int indexDeptId = dr.GetOrdinal("DeptId");
            if (dr["DeptId"] != DBNull.Value)
            {
                contractcorporationdetail.DeptId = Convert.ToInt32(dr[indexDeptId]);
            }

            int indexDeptName = dr.GetOrdinal("DeptName");
            if (dr["DeptName"] != DBNull.Value)
            {
                contractcorporationdetail.DeptName = Convert.ToString(dr[indexDeptName]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                contractcorporationdetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                contractcorporationdetail.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                contractcorporationdetail.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                contractcorporationdetail.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                contractcorporationdetail.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }

            int indexIsDefaultCorp = dr.GetOrdinal("IsDefaultCorp");
            if (dr["IsDefaultCorp"] != DBNull.Value)
            {
                contractcorporationdetail.IsDefaultCorp = Convert.ToBoolean(dr[indexIsDefaultCorp]);
            }

            int indexIsInnerCorp = dr.GetOrdinal("IsInnerCorp");
            if (dr["IsInnerCorp"] != DBNull.Value)
            {
                contractcorporationdetail.IsInnerCorp = Convert.ToBoolean(dr[indexIsInnerCorp]);
            }


            return contractcorporationdetail;
        }

        public override string TableName
        {
            get
            {
                return "Con_ContractCorporationDetail";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            ContractCorporationDetail con_contractcorporationdetail = (ContractCorporationDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = con_contractcorporationdetail.DetailId;
            paras.Add(detailidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = con_contractcorporationdetail.ContractId;
            paras.Add(contractidpara);

            SqlParameter corpidpara = new SqlParameter("@CorpId", SqlDbType.Int, 4);
            corpidpara.Value = con_contractcorporationdetail.CorpId;
            paras.Add(corpidpara);

            if (!string.IsNullOrEmpty(con_contractcorporationdetail.CorpName))
            {
                SqlParameter corpnamepara = new SqlParameter("@CorpName", SqlDbType.VarChar, 200);
                corpnamepara.Value = con_contractcorporationdetail.CorpName;
                paras.Add(corpnamepara);
            }

            SqlParameter deptidpara = new SqlParameter("@DeptId", SqlDbType.Int, 4);
            deptidpara.Value = con_contractcorporationdetail.DeptId;
            paras.Add(deptidpara);

            if (!string.IsNullOrEmpty(con_contractcorporationdetail.DeptName))
            {
                SqlParameter deptnamepara = new SqlParameter("@DeptName", SqlDbType.VarChar, 80);
                deptnamepara.Value = con_contractcorporationdetail.DeptName;
                paras.Add(deptnamepara);
            }

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = con_contractcorporationdetail.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);

            SqlParameter isdefaultcorppara = new SqlParameter("@IsDefaultCorp", SqlDbType.Bit, 1);
            isdefaultcorppara.Value = con_contractcorporationdetail.IsDefaultCorp;
            paras.Add(isdefaultcorppara);

            SqlParameter isinnercorppara = new SqlParameter("@IsInnerCorp", SqlDbType.Bit, 1);
            isinnercorppara.Value = con_contractcorporationdetail.IsInnerCorp;
            paras.Add(isinnercorppara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel LoadCorpListByContractId(UserModel user, int contractId, bool isSelf)
        {
            ResultModel result = new ResultModel();
            try
            {
                int status = (int)Common.StatusEnum.已生效;
                string cmdText = string.Format("select * from dbo.Con_ContractCorporationDetail ccd where ccd.ContractId =@contractId and ccd.IsInnerCorp =@isInnerCorp and ccd.DetailStatus = {0}", status);
                SqlParameter[] paras = new SqlParameter[2];
                paras[0] = new SqlParameter("@contractId", contractId);
                paras[1] = new SqlParameter("@isInnerCorp", isSelf);

                DataTable dt = SqlHelper.ExecuteDataTable(this.ConnectString, cmdText, paras, CommandType.Text);

                List<ContractCorporationDetail> contractCorporationDetails = new List<ContractCorporationDetail>();

                foreach (DataRow dr in dt.Rows)
                {
                    ContractCorporationDetail contractcorporationdetail = new ContractCorporationDetail();
                    contractcorporationdetail.DetailId = Convert.ToInt32(dr["DetailId"]);

                    if (dr["ContractId"] != DBNull.Value)
                    {
                        contractcorporationdetail.ContractId = Convert.ToInt32(dr["ContractId"]);
                    }
                    //if (dr["TradeDirection"] != DBNull.Value)
                    //{
                    //    contractcorporationdetail.TradeDirection = Convert.ToInt32(dr["TradeDirection"]);
                    //}
                    if (dr["CorpId"] != DBNull.Value)
                    {
                        contractcorporationdetail.CorpId = Convert.ToInt32(dr["CorpId"]);
                    }
                    if (dr["CorpName"] != DBNull.Value)
                    {
                        contractcorporationdetail.CorpName = Convert.ToString(dr["CorpName"]);
                    }
                    if (dr["DeptId"] != DBNull.Value)
                    {
                        contractcorporationdetail.DeptId = Convert.ToInt32(dr["DeptId"]);
                    }
                    if (dr["DeptName"] != DBNull.Value)
                    {
                        contractcorporationdetail.DeptName = Convert.ToString(dr["DeptName"]);
                    }
                    if (dr["DetailStatus"] != DBNull.Value)
                    {
                        contractcorporationdetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr["DetailStatus"]);
                    }
                    if (dr["CreatorId"] != DBNull.Value)
                    {
                        contractcorporationdetail.CreatorId = Convert.ToInt32(dr["CreatorId"]);
                    }
                    if (dr["CreateTime"] != DBNull.Value)
                    {
                        contractcorporationdetail.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
                    }
                    if (dr["LastModifyId"] != DBNull.Value)
                    {
                        contractcorporationdetail.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
                    }
                    if (dr["LastModifyTime"] != DBNull.Value)
                    {
                        contractcorporationdetail.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
                    }
                    if (dr["IsDefaultCorp"] != DBNull.Value)
                    {
                        contractcorporationdetail.IsDefaultCorp = Convert.ToBoolean(dr["IsDefaultCorp"]);
                    }
                    if (dr["IsInnerCorp"] != DBNull.Value)
                    {
                        contractcorporationdetail.IsInnerCorp = Convert.ToBoolean(dr["IsInnerCorp"]);
                    }
                    contractCorporationDetails.Add(contractcorporationdetail);
                }
                result.AffectCount = dt.Rows.Count;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = contractCorporationDetails;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel LoadByContractId(UserModel user, int contractId, bool isSelf, StatusEnum status = StatusEnum.已生效)
        {
            int isInner = 0;
            if (isSelf)
                isInner = 1;

            //string cmdText = string.Format("select * from dbo.Con_SubCorporationDetail where DetailStatus>={0} and IsInnerCorp ={1} and ContractId={2} ", (int)status, isInner, contractId);

            string str = "select {0} from {1} where DetailStatus>={2} and IsInnerCorp ={3} and ContractId={4} ";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("if not exists (");
            sb.AppendFormat(str, "1", "dbo.Con_SubCorporationDetail", (int)status, isInner, contractId);
            sb.Append(")");
            sb.Append(Environment.NewLine);
            sb.AppendFormat(str, "*", "dbo.Con_ContractCorporationDetail", (int)status, isInner, contractId);
            sb.Append(Environment.NewLine);
            sb.Append("else");
            sb.Append(Environment.NewLine);
            sb.AppendFormat(str, "*", "dbo.Con_SubCorporationDetail", (int)status, isInner, contractId);

            ResultModel result = base.Load<Model.ContractCorporationDetail>(user, CommandType.Text, sb.ToString());

            return result;
        }

        #endregion

    }
}
