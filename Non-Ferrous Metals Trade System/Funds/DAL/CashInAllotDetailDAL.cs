/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CashInAllotDetailDAL.cs
// 文件功能描述：收款分配明细dbo.Fun_CashInAllotDetail数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年9月18日
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
    /// 收款分配明细dbo.Fun_CashInAllotDetail数据交互类。
    /// </summary>
    public class CashInAllotDetailDAL : ExecOperate, ICashInAllotDetailDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CashInAllotDetailDAL()
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

        /// <summary>
        /// 新增fun_cashinallotdetail信息
        /// </summary>
        /// <param name="user">当前操作用户</param>
        /// <param name="obj">CashInAllotDetail对象</param>
        /// <returns></returns>
        public override ResultModel Insert(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();
            try
            {
                CashInAllotDetail fun_cashinallotdetail = (CashInAllotDetail)obj;

                if (fun_cashinallotdetail == null)
                {
                    result.Message = "新增对象不能为null";
                    return result;
                }

                List<SqlParameter> paras = new List<SqlParameter>();
                SqlParameter detailidpara = new SqlParameter();
                detailidpara.Direction = ParameterDirection.Output;
                detailidpara.SqlDbType = SqlDbType.Int;
                detailidpara.ParameterName = "@DetailId";
                detailidpara.Size = 4;
                paras.Add(detailidpara);

                SqlParameter cashinidpara = new SqlParameter("@CashInId", SqlDbType.Int, 4);
                cashinidpara.Value = fun_cashinallotdetail.CashInId;
                paras.Add(cashinidpara);

                SqlParameter allotidpara = new SqlParameter("@AllotId", SqlDbType.Int, 4);
                allotidpara.Value = fun_cashinallotdetail.AllotId;
                paras.Add(allotidpara);

                SqlParameter allotbalapara = new SqlParameter("@AllotBala", SqlDbType.Decimal, 9);
                allotbalapara.Value = fun_cashinallotdetail.AllotBala;
                paras.Add(allotbalapara);

                SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
                detailstatuspara.Value = fun_cashinallotdetail.DetailStatus;
                paras.Add(detailstatuspara);

                SqlParameter allottypepara = new SqlParameter("@AllotType", SqlDbType.Int, 4);
                allottypepara.Value = fun_cashinallotdetail.AllotType;
                paras.Add(allottypepara);

                SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
                creatoridpara.Value = user.AccountId;
                paras.Add(creatoridpara);


                int i = SqlHelper.ExecuteNonQuery(ConnectString, CommandType.StoredProcedure, "Fun_CashInAllotDetailInsert", paras.ToArray());

                if (i == 1)
                {
                    result.AffectCount = i;
                    result.ResultStatus = 0;
                    result.Message = "CashInAllotDetail添加成功";
                    result.ReturnValue = detailidpara.Value;
                }
                else
                    result.Message = "CashInAllotDetail添加失败";
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 获取指定detailId的fun_cashinallotdetail对象
        /// </summary>
        /// <param name="user">当前操作用户</param>
        /// <param name="detailId">主键值</param>
        /// <returns></returns>
        public override ResultModel Get(UserModel user, int detailId)
        {
            ResultModel result = new ResultModel();

            if (detailId < 1)
            {
                result.Message = "序号不能小于1";
                return result;
            }

            List<SqlParameter> paras = new List<SqlParameter>();
            SqlParameter para = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            para.Value = detailId;
            paras.Add(para);

            SqlDataReader dr = null;

            try
            {
                dr = SqlHelper.ExecuteReader(ConnectString, CommandType.StoredProcedure, "Fun_CashInAllotDetailGet", paras.ToArray());

                CashInAllotDetail cashinallotdetail = new CashInAllotDetail();

                if (dr.Read())
                {
                    int indexDetailId = dr.GetOrdinal("DetailId");
                    cashinallotdetail.DetailId = Convert.ToInt32(dr[indexDetailId]);

                    int indexCashInId = dr.GetOrdinal("CashInId");
                    if (dr["CashInId"] != DBNull.Value)
                    {
                        cashinallotdetail.CashInId = Convert.ToInt32(dr[indexCashInId]);
                    }

                    int indexAllotId = dr.GetOrdinal("AllotId");
                    if (dr["AllotId"] != DBNull.Value)
                    {
                        cashinallotdetail.AllotId = Convert.ToInt32(dr[indexAllotId]);
                    }

                    int indexAllotBala = dr.GetOrdinal("AllotBala");
                    if (dr["AllotBala"] != DBNull.Value)
                    {
                        cashinallotdetail.AllotBala = Convert.ToDecimal(dr[indexAllotBala]);
                    }

                    int indexDetailStatus = dr.GetOrdinal("DetailStatus");
                    if (dr["DetailStatus"] != DBNull.Value)
                    {
                        cashinallotdetail.DetailStatus = (Common.StatusEnum)Enum.Parse(typeof(Common.StatusEnum), dr[indexDetailStatus].ToString());
                    }

                    int indexAllotType = dr.GetOrdinal("AllotType");
                    if (dr["AllotType"] != DBNull.Value)
                    {
                        cashinallotdetail.AllotType = Convert.ToInt32(dr[indexAllotType]);
                    }

                    int indexCreatorId = dr.GetOrdinal("CreatorId");
                    if (dr["CreatorId"] != DBNull.Value)
                    {
                        cashinallotdetail.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
                    }

                    int indexCreateTime = dr.GetOrdinal("CreateTime");
                    if (dr["CreateTime"] != DBNull.Value)
                    {
                        cashinallotdetail.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
                    }

                    int indexLastModifyId = dr.GetOrdinal("LastModifyId");
                    if (dr["LastModifyId"] != DBNull.Value)
                    {
                        cashinallotdetail.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
                    }

                    int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
                    if (dr["LastModifyTime"] != DBNull.Value)
                    {
                        cashinallotdetail.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
                    }

                    result.AffectCount = 1;
                    result.Message = "读取成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = cashinallotdetail;
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

        /// <summary>
        /// 获取fun_cashinallotdetail集合
        /// </summary>
        /// <param name="user">当前操作用户</param>
        /// <returns></returns>
        public override ResultModel Load(UserModel user)
        {
            ResultModel result = new ResultModel();
            try
            {
                DataTable dt = SqlHelper.ExecuteDataTable(ConnectString, "Fun_CashInAllotDetailLoad", null, CommandType.StoredProcedure);

                List<CashInAllotDetail> cashInAllotDetails = new List<CashInAllotDetail>();

                foreach (DataRow dr in dt.Rows)
                {
                    CashInAllotDetail cashinallotdetail = new CashInAllotDetail();
                    cashinallotdetail.DetailId = Convert.ToInt32(dr["DetailId"]);

                    if (dr["CashInId"] != DBNull.Value)
                    {
                        cashinallotdetail.CashInId = Convert.ToInt32(dr["CashInId"]);
                    }
                    if (dr["AllotId"] != DBNull.Value)
                    {
                        cashinallotdetail.AllotId = Convert.ToInt32(dr["AllotId"]);
                    }
                    if (dr["AllotBala"] != DBNull.Value)
                    {
                        cashinallotdetail.AllotBala = Convert.ToDecimal(dr["AllotBala"]);
                    }
                    if (dr["DetailStatus"] != DBNull.Value)
                    {
                        cashinallotdetail.DetailStatus = (Common.StatusEnum)Enum.Parse(typeof(Common.StatusEnum), dr["DetailStatus"].ToString());
                    }
                    if (dr["AllotType"] != DBNull.Value)
                    {
                        cashinallotdetail.AllotType = Convert.ToInt32(dr["AllotType"]);
                    }
                    if (dr["CreatorId"] != DBNull.Value)
                    {
                        cashinallotdetail.CreatorId = Convert.ToInt32(dr["CreatorId"]);
                    }
                    if (dr["CreateTime"] != DBNull.Value)
                    {
                        cashinallotdetail.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
                    }
                    if (dr["LastModifyId"] != DBNull.Value)
                    {
                        cashinallotdetail.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
                    }
                    if (dr["LastModifyTime"] != DBNull.Value)
                    {
                        cashinallotdetail.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
                    }
                    cashInAllotDetails.Add(cashinallotdetail);
                }
                result.AffectCount = dt.Rows.Count;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = cashInAllotDetails;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            CashInAllotDetail fun_cashinallotdetail = (CashInAllotDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = fun_cashinallotdetail.DetailId;
            paras.Add(detailidpara);

            SqlParameter cashinidpara = new SqlParameter("@CashInId", SqlDbType.Int, 4);
            cashinidpara.Value = fun_cashinallotdetail.CashInId;
            paras.Add(cashinidpara);

            SqlParameter allotidpara = new SqlParameter("@AllotId", SqlDbType.Int, 4);
            allotidpara.Value = fun_cashinallotdetail.AllotId;
            paras.Add(allotidpara);

            SqlParameter allotbalapara = new SqlParameter("@AllotBala", SqlDbType.Decimal, 9);
            allotbalapara.Value = fun_cashinallotdetail.AllotBala;
            paras.Add(allotbalapara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = fun_cashinallotdetail.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter allottypepara = new SqlParameter("@AllotType", SqlDbType.Int, 4);
            allottypepara.Value = fun_cashinallotdetail.AllotType;
            paras.Add(allottypepara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        public override string TableName
        {
            get { throw new NotImplementedException(); }
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 新增方法

        public ResultModel Load(UserModel user,int allotId,StatusEnum status = StatusEnum.已生效)
        {
            ResultModel result = new ResultModel();
            try
            {
                string cmdText = string.Format("select * from dbo.Fun_CashInAllotDetail where AllotId ={0} and DetailStatus >={1}",allotId,(int)status);
                DataTable dt = SqlHelper.ExecuteDataTable(ConnectString, cmdText, null, CommandType.Text);

                List<CashInAllotDetail> cashInAllotDetails = new List<CashInAllotDetail>();

                foreach (DataRow dr in dt.Rows)
                {
                    CashInAllotDetail cashinallotdetail = new CashInAllotDetail();
                    cashinallotdetail.DetailId = Convert.ToInt32(dr["DetailId"]);

                    if (dr["CashInId"] != DBNull.Value)
                    {
                        cashinallotdetail.CashInId = Convert.ToInt32(dr["CashInId"]);
                    }
                    if (dr["AllotId"] != DBNull.Value)
                    {
                        cashinallotdetail.AllotId = Convert.ToInt32(dr["AllotId"]);
                    }
                    if (dr["AllotBala"] != DBNull.Value)
                    {
                        cashinallotdetail.AllotBala = Convert.ToDecimal(dr["AllotBala"]);
                    }
                    if (dr["DetailStatus"] != DBNull.Value)
                    {
                        cashinallotdetail.DetailStatus = (Common.StatusEnum)Enum.Parse(typeof(Common.StatusEnum), dr["DetailStatus"].ToString());
                    }
                    if (dr["AllotType"] != DBNull.Value)
                    {
                        cashinallotdetail.AllotType = Convert.ToInt32(dr["AllotType"]);
                    }
                    if (dr["CreatorId"] != DBNull.Value)
                    {
                        cashinallotdetail.CreatorId = Convert.ToInt32(dr["CreatorId"]);
                    }
                    if (dr["CreateTime"] != DBNull.Value)
                    {
                        cashinallotdetail.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
                    }
                    if (dr["LastModifyId"] != DBNull.Value)
                    {
                        cashinallotdetail.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
                    }
                    if (dr["LastModifyTime"] != DBNull.Value)
                    {
                        cashinallotdetail.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
                    }
                    cashInAllotDetails.Add(cashinallotdetail);
                }
                result.AffectCount = dt.Rows.Count;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = cashInAllotDetails;
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
