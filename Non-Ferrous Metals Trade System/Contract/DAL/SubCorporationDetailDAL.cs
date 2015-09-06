/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SubCorporationDetailDAL.cs
// 文件功能描述：子合约抬头明细dbo.Con_SubCorporationDetail数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年12月5日
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
    /// 子合约抬头明细dbo.Con_SubCorporationDetail数据交互类。
    /// </summary>
    public class SubCorporationDetailDAL : DetailOperate, ISubCorporationDetailDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SubCorporationDetailDAL()
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
            SubCorporationDetail con_subcorporationdetail = (SubCorporationDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@DetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = con_subcorporationdetail.ContractId;
            paras.Add(contractidpara);

            SqlParameter subidpara = new SqlParameter("@SubId", SqlDbType.Int, 4);
            subidpara.Value = con_subcorporationdetail.SubId;
            paras.Add(subidpara);

            SqlParameter corpidpara = new SqlParameter("@CorpId", SqlDbType.Int, 4);
            corpidpara.Value = con_subcorporationdetail.CorpId;
            paras.Add(corpidpara);

            if (!string.IsNullOrEmpty(con_subcorporationdetail.CorpName))
            {
                SqlParameter corpnamepara = new SqlParameter("@CorpName", SqlDbType.VarChar, 200);
                corpnamepara.Value = con_subcorporationdetail.CorpName;
                paras.Add(corpnamepara);
            }

            SqlParameter deptidpara = new SqlParameter("@DeptId", SqlDbType.Int, 4);
            deptidpara.Value = con_subcorporationdetail.DeptId;
            paras.Add(deptidpara);

            if (!string.IsNullOrEmpty(con_subcorporationdetail.DeptName))
            {
                SqlParameter deptnamepara = new SqlParameter("@DeptName", SqlDbType.VarChar, 80);
                deptnamepara.Value = con_subcorporationdetail.DeptName;
                paras.Add(deptnamepara);
            }

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = con_subcorporationdetail.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);

            SqlParameter isdefaultcorppara = new SqlParameter("@IsDefaultCorp", SqlDbType.Bit, 1);
            isdefaultcorppara.Value = con_subcorporationdetail.IsDefaultCorp;
            paras.Add(isdefaultcorppara);

            SqlParameter isinnercorppara = new SqlParameter("@IsInnerCorp", SqlDbType.Bit, 1);
            isinnercorppara.Value = con_subcorporationdetail.IsInnerCorp;
            paras.Add(isinnercorppara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            SubCorporationDetail subcorporationdetail = new SubCorporationDetail();

            int indexDetailId = dr.GetOrdinal("DetailId");
            subcorporationdetail.DetailId = Convert.ToInt32(dr[indexDetailId]);

            int indexContractId = dr.GetOrdinal("ContractId");
            if (dr["ContractId"] != DBNull.Value)
            {
                subcorporationdetail.ContractId = Convert.ToInt32(dr[indexContractId]);
            }

            int indexSubId = dr.GetOrdinal("SubId");
            if (dr["SubId"] != DBNull.Value)
            {
                subcorporationdetail.SubId = Convert.ToInt32(dr[indexSubId]);
            }

            int indexCorpId = dr.GetOrdinal("CorpId");
            if (dr["CorpId"] != DBNull.Value)
            {
                subcorporationdetail.CorpId = Convert.ToInt32(dr[indexCorpId]);
            }

            int indexCorpName = dr.GetOrdinal("CorpName");
            if (dr["CorpName"] != DBNull.Value)
            {
                subcorporationdetail.CorpName = Convert.ToString(dr[indexCorpName]);
            }

            int indexDeptId = dr.GetOrdinal("DeptId");
            if (dr["DeptId"] != DBNull.Value)
            {
                subcorporationdetail.DeptId = Convert.ToInt32(dr[indexDeptId]);
            }

            int indexDeptName = dr.GetOrdinal("DeptName");
            if (dr["DeptName"] != DBNull.Value)
            {
                subcorporationdetail.DeptName = Convert.ToString(dr[indexDeptName]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                subcorporationdetail.DetailStatus = (StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                subcorporationdetail.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                subcorporationdetail.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                subcorporationdetail.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                subcorporationdetail.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }

            int indexIsDefaultCorp = dr.GetOrdinal("IsDefaultCorp");
            if (dr["IsDefaultCorp"] != DBNull.Value)
            {
                subcorporationdetail.IsDefaultCorp = Convert.ToBoolean(dr[indexIsDefaultCorp]);
            }

            int indexIsInnerCorp = dr.GetOrdinal("IsInnerCorp");
            if (dr["IsInnerCorp"] != DBNull.Value)
            {
                subcorporationdetail.IsInnerCorp = Convert.ToBoolean(dr[indexIsInnerCorp]);
            }


            return subcorporationdetail;
        }

        public override string TableName
        {
            get
            {
                return "Con_SubCorporationDetail";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            SubCorporationDetail con_subcorporationdetail = (SubCorporationDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = con_subcorporationdetail.DetailId;
            paras.Add(detailidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = con_subcorporationdetail.ContractId;
            paras.Add(contractidpara);

            SqlParameter subidpara = new SqlParameter("@SubId", SqlDbType.Int, 4);
            subidpara.Value = con_subcorporationdetail.SubId;
            paras.Add(subidpara);

            SqlParameter corpidpara = new SqlParameter("@CorpId", SqlDbType.Int, 4);
            corpidpara.Value = con_subcorporationdetail.CorpId;
            paras.Add(corpidpara);

            if (!string.IsNullOrEmpty(con_subcorporationdetail.CorpName))
            {
                SqlParameter corpnamepara = new SqlParameter("@CorpName", SqlDbType.VarChar, 200);
                corpnamepara.Value = con_subcorporationdetail.CorpName;
                paras.Add(corpnamepara);
            }

            SqlParameter deptidpara = new SqlParameter("@DeptId", SqlDbType.Int, 4);
            deptidpara.Value = con_subcorporationdetail.DeptId;
            paras.Add(deptidpara);

            if (!string.IsNullOrEmpty(con_subcorporationdetail.DeptName))
            {
                SqlParameter deptnamepara = new SqlParameter("@DeptName", SqlDbType.VarChar, 80);
                deptnamepara.Value = con_subcorporationdetail.DeptName;
                paras.Add(deptnamepara);
            }

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = con_subcorporationdetail.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);

            SqlParameter isdefaultcorppara = new SqlParameter("@IsDefaultCorp", SqlDbType.Bit, 1);
            isdefaultcorppara.Value = con_subcorporationdetail.IsDefaultCorp;
            paras.Add(isdefaultcorppara);

            SqlParameter isinnercorppara = new SqlParameter("@IsInnerCorp", SqlDbType.Bit, 1);
            isinnercorppara.Value = con_subcorporationdetail.IsInnerCorp;
            paras.Add(isinnercorppara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel Load(UserModel user, int subId, bool isSelf,StatusEnum status = StatusEnum.已生效)
        {
            int isInner = 0;
            if (isSelf)
                isInner = 1;

            string cmdText = string.Format("select * from dbo.Con_SubCorporationDetail where DetailStatus>={0} and IsInnerCorp ={1} and SubId={2} ", (int)status, isInner, subId);

            ResultModel result = base.Load<Model.SubCorporationDetail>(user, CommandType.Text, cmdText);

            return result;
        }

        #endregion
    }
}
