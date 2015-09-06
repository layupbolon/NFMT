/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ContractDeptDAL.cs
// 文件功能描述：合约执行部门明细dbo.Con_ContractDept数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年7月25日
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
    /// 合约执行部门明细dbo.Con_ContractDept数据交互类。
    /// </summary>
    public class ContractDeptDAL : ApplyOperate, IContractDeptDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ContractDeptDAL()
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
            ContractDept con_contractdept = (ContractDept)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@DetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = con_contractdept.ContractId;
            paras.Add(contractidpara);

            SqlParameter deptidpara = new SqlParameter("@DeptId", SqlDbType.Int, 4);
            deptidpara.Value = con_contractdept.DeptId;
            paras.Add(deptidpara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = con_contractdept.DetailStatus;
            paras.Add(detailstatuspara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            ContractDept contractdept = new ContractDept();

            int indexDetailId = dr.GetOrdinal("DetailId");
            contractdept.DetailId = Convert.ToInt32(dr[indexDetailId]);

            int indexContractId = dr.GetOrdinal("ContractId");
            if (dr["ContractId"] != DBNull.Value)
            {
                contractdept.ContractId = Convert.ToInt32(dr[indexContractId]);
            }

            int indexDeptId = dr.GetOrdinal("DeptId");
            if (dr["DeptId"] != DBNull.Value)
            {
                contractdept.DeptId = Convert.ToInt32(dr[indexDeptId]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                contractdept.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }


            return contractdept;
        }

        public override string TableName
        {
            get
            {
                return "Con_ContractDept";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            ContractDept con_contractdept = (ContractDept)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = con_contractdept.DetailId;
            paras.Add(detailidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = con_contractdept.ContractId;
            paras.Add(contractidpara);

            SqlParameter deptidpara = new SqlParameter("@DeptId", SqlDbType.Int, 4);
            deptidpara.Value = con_contractdept.DeptId;
            paras.Add(deptidpara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = con_contractdept.DetailStatus;
            paras.Add(detailstatuspara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel LoadDeptByContractId(UserModel user, int contractId)
        {
            ResultModel result = new ResultModel();
            try
            {
                List<SqlParameter> paras = new List<SqlParameter>();
                SqlParameter para = new SqlParameter("@contractId", SqlDbType.Int, 4);
                para.Value = contractId;
                paras.Add(para);

                int status = (int)Common.StatusEnum.已生效;
                string cmdText = string.Format("select * from NFMT.dbo.Con_ContractDept where ContractId =@contractId and DetailStatus={0}", status);

                DataTable dt = SqlHelper.ExecuteDataTable(this.ConnectString, cmdText, paras.ToArray(), CommandType.Text);

                List<ContractDept> contractDepts = new List<ContractDept>();

                foreach (DataRow dr in dt.Rows)
                {
                    ContractDept contractdept = new ContractDept();
                    contractdept.DetailId = Convert.ToInt32(dr["DetailId"]);

                    if (dr["ContractId"] != DBNull.Value)
                    {
                        contractdept.ContractId = Convert.ToInt32(dr["ContractId"]);
                    }
                    if (dr["DeptId"] != DBNull.Value)
                    {
                        contractdept.DeptId = Convert.ToInt32(dr["DeptId"]);
                    }
                    if (dr["DetailStatus"] != DBNull.Value)
                    {
                        contractdept.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr["DetailStatus"]);
                    }
                    contractDepts.Add(contractdept);
                }
                result.AffectCount = dt.Rows.Count;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = contractDepts;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        #endregion

    }
}
