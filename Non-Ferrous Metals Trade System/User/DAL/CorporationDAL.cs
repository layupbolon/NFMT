/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CorporationDAL.cs
// 文件功能描述：公司dbo.Corporation数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.User.Model;
using NFMT.DBUtility;
using NFMT.User.IDAL;
using NFMT.Common;

namespace NFMT.User.DAL
{
    /// <summary>
    /// 公司dbo.Corporation数据交互类。
    /// </summary>
    public class CorporationDAL : DataOperate, ICorporationDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CorporationDAL()
        {
        }

        #endregion

        #region 数据库操作

        public override string ConnectString
        {
            get
            {
                return NFMT.DBUtility.SqlHelper.ConnectionStringUser;
            }
        }

        public override List<SqlParameter> CreateInsertParameters(IModel obj, ref SqlParameter returnValue)
        {
            Corporation corporation = (Corporation)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@CorpId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter parentidpara = new SqlParameter("@ParentId", SqlDbType.Int, 4);
            parentidpara.Value = corporation.ParentId;
            paras.Add(parentidpara);

            if (!string.IsNullOrEmpty(corporation.CorpCode))
            {
                SqlParameter corpcodepara = new SqlParameter("@CorpCode", SqlDbType.VarChar, 80);
                corpcodepara.Value = corporation.CorpCode;
                paras.Add(corpcodepara);
            }

            SqlParameter corpnamepara = new SqlParameter("@CorpName", SqlDbType.VarChar, 40);
            corpnamepara.Value = corporation.CorpName;
            paras.Add(corpnamepara);

            if (!string.IsNullOrEmpty(corporation.CorpEName))
            {
                SqlParameter corpenamepara = new SqlParameter("@CorpEName", SqlDbType.VarChar, 80);
                corpenamepara.Value = corporation.CorpEName;
                paras.Add(corpenamepara);
            }

            if (!string.IsNullOrEmpty(corporation.TaxPayerId))
            {
                SqlParameter taxpayeridpara = new SqlParameter("@TaxPayerId", SqlDbType.VarChar, 80);
                taxpayeridpara.Value = corporation.TaxPayerId;
                paras.Add(taxpayeridpara);
            }

            if (!string.IsNullOrEmpty(corporation.CorpFullName))
            {
                SqlParameter corpfullnamepara = new SqlParameter("@CorpFullName", SqlDbType.VarChar, 80);
                corpfullnamepara.Value = corporation.CorpFullName;
                paras.Add(corpfullnamepara);
            }

            if (!string.IsNullOrEmpty(corporation.CorpFullEName))
            {
                SqlParameter corpfullenamepara = new SqlParameter("@CorpFullEName", SqlDbType.VarChar, 200);
                corpfullenamepara.Value = corporation.CorpFullEName;
                paras.Add(corpfullenamepara);
            }

            if (!string.IsNullOrEmpty(corporation.CorpAddress))
            {
                SqlParameter corpaddresspara = new SqlParameter("@CorpAddress", SqlDbType.VarChar, 400);
                corpaddresspara.Value = corporation.CorpAddress;
                paras.Add(corpaddresspara);
            }

            if (!string.IsNullOrEmpty(corporation.CorpEAddress))
            {
                SqlParameter corpeaddresspara = new SqlParameter("@CorpEAddress", SqlDbType.VarChar, 800);
                corpeaddresspara.Value = corporation.CorpEAddress;
                paras.Add(corpeaddresspara);
            }

            if (!string.IsNullOrEmpty(corporation.CorpTel))
            {
                SqlParameter corptelpara = new SqlParameter("@CorpTel", SqlDbType.VarChar, 40);
                corptelpara.Value = corporation.CorpTel;
                paras.Add(corptelpara);
            }

            if (!string.IsNullOrEmpty(corporation.CorpFax))
            {
                SqlParameter corpfaxpara = new SqlParameter("@CorpFax", SqlDbType.VarChar, 40);
                corpfaxpara.Value = corporation.CorpFax;
                paras.Add(corpfaxpara);
            }

            if (!string.IsNullOrEmpty(corporation.CorpZip))
            {
                SqlParameter corpzippara = new SqlParameter("@CorpZip", SqlDbType.VarChar, 20);
                corpzippara.Value = corporation.CorpZip;
                paras.Add(corpzippara);
            }

            SqlParameter corptypepara = new SqlParameter("@CorpType", SqlDbType.Int, 4);
            corptypepara.Value = corporation.CorpType;
            paras.Add(corptypepara);

            SqlParameter isselfpara = new SqlParameter("@IsSelf", SqlDbType.Bit, 1);
            isselfpara.Value = corporation.IsSelf;
            paras.Add(isselfpara);

            SqlParameter corpstatuspara = new SqlParameter("@CorpStatus", SqlDbType.Int, 4);
            corpstatuspara.Value = (int)Common.StatusEnum.已录入;
            paras.Add(corpstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            Corporation corporation = new Corporation();

            int indexCorpId = dr.GetOrdinal("CorpId");
            corporation.CorpId = Convert.ToInt32(dr[indexCorpId]);

            int indexParentId = dr.GetOrdinal("ParentId");
            if (dr["ParentId"] != DBNull.Value)
            {
                corporation.ParentId = Convert.ToInt32(dr[indexParentId]);
            }

            int indexCorpCode = dr.GetOrdinal("CorpCode");
            if (dr["CorpCode"] != DBNull.Value)
            {
                corporation.CorpCode = Convert.ToString(dr[indexCorpCode]);
            }

            int indexCorpName = dr.GetOrdinal("CorpName");
            corporation.CorpName = Convert.ToString(dr[indexCorpName]);

            int indexCorpEName = dr.GetOrdinal("CorpEName");
            if (dr["CorpEName"] != DBNull.Value)
            {
                corporation.CorpEName = Convert.ToString(dr[indexCorpEName]);
            }

            int indexTaxPayerId = dr.GetOrdinal("TaxPayerId");
            if (dr["TaxPayerId"] != DBNull.Value)
            {
                corporation.TaxPayerId = Convert.ToString(dr[indexTaxPayerId]);
            }

            int indexCorpFullName = dr.GetOrdinal("CorpFullName");
            if (dr["CorpFullName"] != DBNull.Value)
            {
                corporation.CorpFullName = Convert.ToString(dr[indexCorpFullName]);
            }

            int indexCorpFullEName = dr.GetOrdinal("CorpFullEName");
            if (dr["CorpFullEName"] != DBNull.Value)
            {
                corporation.CorpFullEName = Convert.ToString(dr[indexCorpFullEName]);
            }

            int indexCorpAddress = dr.GetOrdinal("CorpAddress");
            if (dr["CorpAddress"] != DBNull.Value)
            {
                corporation.CorpAddress = Convert.ToString(dr[indexCorpAddress]);
            }

            int indexCorpEAddress = dr.GetOrdinal("CorpEAddress");
            if (dr["CorpEAddress"] != DBNull.Value)
            {
                corporation.CorpEAddress = Convert.ToString(dr[indexCorpEAddress]);
            }

            int indexCorpTel = dr.GetOrdinal("CorpTel");
            if (dr["CorpTel"] != DBNull.Value)
            {
                corporation.CorpTel = Convert.ToString(dr[indexCorpTel]);
            }

            int indexCorpFax = dr.GetOrdinal("CorpFax");
            if (dr["CorpFax"] != DBNull.Value)
            {
                corporation.CorpFax = Convert.ToString(dr[indexCorpFax]);
            }

            int indexCorpZip = dr.GetOrdinal("CorpZip");
            if (dr["CorpZip"] != DBNull.Value)
            {
                corporation.CorpZip = Convert.ToString(dr[indexCorpZip]);
            }

            int indexCorpType = dr.GetOrdinal("CorpType");
            if (dr["CorpType"] != DBNull.Value)
            {
                corporation.CorpType = Convert.ToInt32(dr[indexCorpType]);
            }

            int indexIsSelf = dr.GetOrdinal("IsSelf");
            if (dr["IsSelf"] != DBNull.Value)
            {
                corporation.IsSelf = Convert.ToBoolean(dr[indexIsSelf]);
            }

            int indexCorpStatus = dr.GetOrdinal("CorpStatus");
            corporation.CorpStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexCorpStatus]);

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            corporation.CreatorId = Convert.ToInt32(dr[indexCreatorId]);

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            corporation.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                corporation.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                corporation.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return corporation;
        }

        public override string TableName
        {
            get
            {
                return "Corporation";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            Corporation corporation = (Corporation)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter corpidpara = new SqlParameter("@CorpId", SqlDbType.Int, 4);
            corpidpara.Value = corporation.CorpId;
            paras.Add(corpidpara);

            SqlParameter parentidpara = new SqlParameter("@ParentId", SqlDbType.Int, 4);
            parentidpara.Value = corporation.ParentId;
            paras.Add(parentidpara);

            if (!string.IsNullOrEmpty(corporation.CorpCode))
            {
                SqlParameter corpcodepara = new SqlParameter("@CorpCode", SqlDbType.VarChar, 80);
                corpcodepara.Value = corporation.CorpCode;
                paras.Add(corpcodepara);
            }

            SqlParameter corpnamepara = new SqlParameter("@CorpName", SqlDbType.VarChar, 40);
            corpnamepara.Value = corporation.CorpName;
            paras.Add(corpnamepara);

            if (!string.IsNullOrEmpty(corporation.CorpEName))
            {
                SqlParameter corpenamepara = new SqlParameter("@CorpEName", SqlDbType.VarChar, 80);
                corpenamepara.Value = corporation.CorpEName;
                paras.Add(corpenamepara);
            }

            if (!string.IsNullOrEmpty(corporation.TaxPayerId))
            {
                SqlParameter taxpayeridpara = new SqlParameter("@TaxPayerId", SqlDbType.VarChar, 80);
                taxpayeridpara.Value = corporation.TaxPayerId;
                paras.Add(taxpayeridpara);
            }

            if (!string.IsNullOrEmpty(corporation.CorpFullName))
            {
                SqlParameter corpfullnamepara = new SqlParameter("@CorpFullName", SqlDbType.VarChar, 80);
                corpfullnamepara.Value = corporation.CorpFullName;
                paras.Add(corpfullnamepara);
            }

            if (!string.IsNullOrEmpty(corporation.CorpFullEName))
            {
                SqlParameter corpfullenamepara = new SqlParameter("@CorpFullEName", SqlDbType.VarChar, 200);
                corpfullenamepara.Value = corporation.CorpFullEName;
                paras.Add(corpfullenamepara);
            }

            if (!string.IsNullOrEmpty(corporation.CorpAddress))
            {
                SqlParameter corpaddresspara = new SqlParameter("@CorpAddress", SqlDbType.VarChar, 400);
                corpaddresspara.Value = corporation.CorpAddress;
                paras.Add(corpaddresspara);
            }

            if (!string.IsNullOrEmpty(corporation.CorpEAddress))
            {
                SqlParameter corpeaddresspara = new SqlParameter("@CorpEAddress", SqlDbType.VarChar, 800);
                corpeaddresspara.Value = corporation.CorpEAddress;
                paras.Add(corpeaddresspara);
            }

            if (!string.IsNullOrEmpty(corporation.CorpTel))
            {
                SqlParameter corptelpara = new SqlParameter("@CorpTel", SqlDbType.VarChar, 40);
                corptelpara.Value = corporation.CorpTel;
                paras.Add(corptelpara);
            }

            if (!string.IsNullOrEmpty(corporation.CorpFax))
            {
                SqlParameter corpfaxpara = new SqlParameter("@CorpFax", SqlDbType.VarChar, 40);
                corpfaxpara.Value = corporation.CorpFax;
                paras.Add(corpfaxpara);
            }

            if (!string.IsNullOrEmpty(corporation.CorpZip))
            {
                SqlParameter corpzippara = new SqlParameter("@CorpZip", SqlDbType.VarChar, 20);
                corpzippara.Value = corporation.CorpZip;
                paras.Add(corpzippara);
            }

            SqlParameter corptypepara = new SqlParameter("@CorpType", SqlDbType.Int, 4);
            corptypepara.Value = corporation.CorpType;
            paras.Add(corptypepara);

            SqlParameter isselfpara = new SqlParameter("@IsSelf", SqlDbType.Bit, 1);
            isselfpara.Value = corporation.IsSelf;
            paras.Add(isselfpara);

            SqlParameter corpstatuspara = new SqlParameter("@CorpStatus", SqlDbType.Int, 4);
            corpstatuspara.Value = corporation.CorpStatus;
            paras.Add(corpstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel Load(UserModel user, int empId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string cmdText = "select c.* from dbo.CorpDept cd inner join dbo.Corporation c on cd.CorpId = c.CorpId inner join dbo.DeptEmp de on de.DeptId = cd.DeptId where de.EmpId=@empId";

                SqlParameter[] paras = new SqlParameter[1];
                paras[0] = new SqlParameter("@empId", empId);

                DataTable dt = SqlHelper.ExecuteDataTable(ConnectString, cmdText, paras, CommandType.Text);

                List<Corporation> corporations = new List<Corporation>();

                foreach (DataRow dr in dt.Rows)
                {
                    Corporation corporation = new Corporation();
                    corporation.CorpId = Convert.ToInt32(dr["CorpId"]);

                    if (dr["ParentId"] != DBNull.Value)
                    {
                        corporation.ParentId = Convert.ToInt32(dr["ParentId"]);
                    }
                    if (dr["CorpCode"] != DBNull.Value)
                    {
                        corporation.CorpCode = Convert.ToString(dr["CorpCode"]);
                    }
                    if (dr["CorpName"] != DBNull.Value)
                    {
                        corporation.CorpName = Convert.ToString(dr["CorpName"]);
                    }
                    if (dr["CorpEName"] != DBNull.Value)
                    {
                        corporation.CorpEName = Convert.ToString(dr["CorpEName"]);
                    }
                    if (dr["TaxPayerId"] != DBNull.Value)
                    {
                        corporation.TaxPayerId = dr["TaxPayerId"].ToString();
                    }
                    if (dr["CorpFullName"] != DBNull.Value)
                    {
                        corporation.CorpFullName = Convert.ToString(dr["CorpFullName"]);
                    }
                    if (dr["CorpFullEName"] != DBNull.Value)
                    {
                        corporation.CorpFullEName = Convert.ToString(dr["CorpFullEName"]);
                    }
                    if (dr["CorpAddress"] != DBNull.Value)
                    {
                        corporation.CorpAddress = Convert.ToString(dr["CorpAddress"]);
                    }
                    if (dr["CorpEAddress"] != DBNull.Value)
                    {
                        corporation.CorpEAddress = Convert.ToString(dr["CorpEAddress"]);
                    }
                    if (dr["CorpTel"] != DBNull.Value)
                    {
                        corporation.CorpTel = Convert.ToString(dr["CorpTel"]);
                    }
                    if (dr["CorpFax"] != DBNull.Value)
                    {
                        corporation.CorpFax = Convert.ToString(dr["CorpFax"]);
                    }
                    if (dr["CorpZip"] != DBNull.Value)
                    {
                        corporation.CorpZip = Convert.ToString(dr["CorpZip"]);
                    }
                    if (dr["CorpType"] != DBNull.Value)
                    {
                        corporation.CorpType = Convert.ToInt32(dr["CorpType"]);
                    }
                    if (dr["CorpStatus"] != DBNull.Value)
                    {
                        corporation.CorpStatus = (Common.StatusEnum)Enum.Parse(typeof(Common.StatusEnum), dr["CorpStatus"].ToString());
                    }
                    if (dr["CreatorId"] != DBNull.Value)
                    {
                        corporation.CreatorId = Convert.ToInt32(dr["CreatorId"]);
                    }
                    if (dr["CreateTime"] != DBNull.Value)
                    {
                        corporation.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
                    }
                    if (dr["LastModifyId"] != DBNull.Value)
                    {
                        corporation.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
                    }
                    if (dr["LastModifyTime"] != DBNull.Value)
                    {
                        corporation.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
                    }
                    corporations.Add(corporation);
                }
                result.AffectCount = dt.Rows.Count;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = corporations;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel GetCorpList(UserModel user, int isSelf)
        {
            ResultModel result = new ResultModel();
            string sql = string.Empty;
            try
            {

                sql = string.Format("select CorpId,CorpName from NFMT_User.dbo.Corporation where IsSelf = {0}", isSelf);

                DataTable dt = SqlHelper.ExecuteDataTable(this.ConnectString, sql, null, CommandType.Text);

                if (dt != null && dt.Rows.Count > 0)
                {
                    result.AffectCount = dt.Rows.Count;
                    result.Message = "获取企业列表成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = dt;
                }
                else
                {
                    result.Message = "获取企业列表失败";
                    result.ResultStatus = -1;
                }
            }
            catch (Exception e)
            {
                result.Message = string.Format("获取企业列表失败,{0}", e.Message);
                result.ResultStatus = -1;
            }

            return result;
        }

        public ResultModel LoadAuthSelfCorp(UserModel user)
        {
            ResultModel result = new ResultModel();

            try
            {
                NFMT.Authority.CorpAuth auth = new NFMT.Authority.CorpAuth();
                auth.AuthColumnNames.Add("corp.CorpId");
                result = auth.CreateAuthorityStr(user);

                string cmdText = string.Format("select corp.* from dbo.Corporation corp where corp.IsSelf=1 and corp.CorpStatus ={0} {1}", (int)StatusEnum.已生效, result.ReturnValue.ToString());

                DataTable dt = SqlHelper.ExecuteDataTable(ConnectString, cmdText, null, CommandType.Text);

                List<Corporation> corporations = new List<Corporation>();

                foreach (DataRow dr in dt.Rows)
                {
                    Corporation corporation = new Corporation();
                    corporation.CorpId = Convert.ToInt32(dr["CorpId"]);

                    if (dr["ParentId"] != DBNull.Value)
                    {
                        corporation.ParentId = Convert.ToInt32(dr["ParentId"]);
                    }
                    if (dr["CorpCode"] != DBNull.Value)
                    {
                        corporation.CorpCode = Convert.ToString(dr["CorpCode"]);
                    }
                    if (dr["CorpName"] != DBNull.Value)
                    {
                        corporation.CorpName = Convert.ToString(dr["CorpName"]);
                    }
                    if (dr["CorpEName"] != DBNull.Value)
                    {
                        corporation.CorpEName = Convert.ToString(dr["CorpEName"]);
                    }
                    if (dr["TaxPayerId"] != DBNull.Value)
                    {
                        corporation.TaxPayerId = dr["TaxPayerId"].ToString();
                    }
                    if (dr["CorpFullName"] != DBNull.Value)
                    {
                        corporation.CorpFullName = Convert.ToString(dr["CorpFullName"]);
                    }
                    if (dr["CorpFullEName"] != DBNull.Value)
                    {
                        corporation.CorpFullEName = Convert.ToString(dr["CorpFullEName"]);
                    }
                    if (dr["CorpAddress"] != DBNull.Value)
                    {
                        corporation.CorpAddress = Convert.ToString(dr["CorpAddress"]);
                    }
                    if (dr["CorpEAddress"] != DBNull.Value)
                    {
                        corporation.CorpEAddress = Convert.ToString(dr["CorpEAddress"]);
                    }
                    if (dr["CorpTel"] != DBNull.Value)
                    {
                        corporation.CorpTel = Convert.ToString(dr["CorpTel"]);
                    }
                    if (dr["CorpFax"] != DBNull.Value)
                    {
                        corporation.CorpFax = Convert.ToString(dr["CorpFax"]);
                    }
                    if (dr["CorpZip"] != DBNull.Value)
                    {
                        corporation.CorpZip = Convert.ToString(dr["CorpZip"]);
                    }
                    if (dr["CorpType"] != DBNull.Value)
                    {
                        corporation.CorpType = Convert.ToInt32(dr["CorpType"]);
                    }
                    if (dr["CorpStatus"] != DBNull.Value)
                    {
                        corporation.CorpStatus = (Common.StatusEnum)Enum.Parse(typeof(Common.StatusEnum), dr["CorpStatus"].ToString());
                    }
                    if (dr["CreatorId"] != DBNull.Value)
                    {
                        corporation.CreatorId = Convert.ToInt32(dr["CreatorId"]);
                    }
                    if (dr["CreateTime"] != DBNull.Value)
                    {
                        corporation.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
                    }
                    if (dr["LastModifyId"] != DBNull.Value)
                    {
                        corporation.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
                    }
                    if (dr["LastModifyTime"] != DBNull.Value)
                    {
                        corporation.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
                    }
                    corporations.Add(corporation);
                }

                result.AffectCount = dt.Rows.Count;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = corporations;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public override int MenuId
        {
            get
            {
                return 16;
            }
        }

        #endregion
    }
}
