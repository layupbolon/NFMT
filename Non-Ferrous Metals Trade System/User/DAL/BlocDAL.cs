/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：BlocDAL.cs
// 文件功能描述：集团dbo.Bloc数据交互类。
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
    /// 集团dbo.Bloc数据交互类。
    /// </summary>
    public class BlocDAL : DataOperate, IBlocDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public BlocDAL()
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
            Bloc bloc = (Bloc)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@BlocId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter blocnamepara = new SqlParameter("@BlocName", SqlDbType.VarChar, 200);
            blocnamepara.Value = bloc.BlocName;
            paras.Add(blocnamepara);

            if (!string.IsNullOrEmpty(bloc.BlocFullName))
            {
                SqlParameter blocfullnamepara = new SqlParameter("@BlocFullName", SqlDbType.VarChar, 400);
                blocfullnamepara.Value = bloc.BlocFullName;
                paras.Add(blocfullnamepara);
            }

            if (!string.IsNullOrEmpty(bloc.BlocEname))
            {
                SqlParameter blocenamepara = new SqlParameter("@BlocEname", SqlDbType.VarChar, 400);
                blocenamepara.Value = bloc.BlocEname;
                paras.Add(blocenamepara);
            }

            SqlParameter isselfpara = new SqlParameter("@IsSelf", SqlDbType.Bit, 1);
            isselfpara.Value = bloc.IsSelf;
            paras.Add(isselfpara);

            SqlParameter blocstatuspara = new SqlParameter("@BlocStatus", SqlDbType.Int, 4);
            blocstatuspara.Value = (int)Common.StatusEnum.已录入;
            paras.Add(blocstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            Bloc bloc = new Bloc();

            int indexBlocId = dr.GetOrdinal("BlocId");
            bloc.BlocId = Convert.ToInt32(dr[indexBlocId]);

            int indexBlocName = dr.GetOrdinal("BlocName");
            bloc.BlocName = Convert.ToString(dr[indexBlocName]);

            int indexBlocFullName = dr.GetOrdinal("BlocFullName");
            if (dr["BlocFullName"] != DBNull.Value)
            {
                bloc.BlocFullName = Convert.ToString(dr[indexBlocFullName]);
            }

            int indexBlocEname = dr.GetOrdinal("BlocEname");
            if (dr["BlocEname"] != DBNull.Value)
            {
                bloc.BlocEname = Convert.ToString(dr[indexBlocEname]);
            }

            int indexIsSelf = dr.GetOrdinal("IsSelf");
            if (dr["IsSelf"] != DBNull.Value)
            {
                bloc.IsSelf = Convert.ToBoolean(dr[indexIsSelf]);
            }

            int indexBlocStatus = dr.GetOrdinal("BlocStatus");
            if (dr["BlocStatus"] != DBNull.Value)
            {
                bloc.BlocStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexBlocStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            bloc.CreatorId = Convert.ToInt32(dr[indexCreatorId]);

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            bloc.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                bloc.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                bloc.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return bloc;
        }

        public override string TableName
        {
            get
            {
                return "Bloc";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            Bloc bloc = (Bloc)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter blocidpara = new SqlParameter("@BlocId", SqlDbType.Int, 4);
            blocidpara.Value = bloc.BlocId;
            paras.Add(blocidpara);

            SqlParameter blocnamepara = new SqlParameter("@BlocName", SqlDbType.VarChar, 200);
            blocnamepara.Value = bloc.BlocName;
            paras.Add(blocnamepara);

            if (!string.IsNullOrEmpty(bloc.BlocFullName))
            {
                SqlParameter blocfullnamepara = new SqlParameter("@BlocFullName", SqlDbType.VarChar, 400);
                blocfullnamepara.Value = bloc.BlocFullName;
                paras.Add(blocfullnamepara);
            }

            if (!string.IsNullOrEmpty(bloc.BlocEname))
            {
                SqlParameter blocenamepara = new SqlParameter("@BlocEname", SqlDbType.VarChar, 400);
                blocenamepara.Value = bloc.BlocEname;
                paras.Add(blocenamepara);
            }

            SqlParameter isselfpara = new SqlParameter("@IsSelf", SqlDbType.Bit, 1);
            isselfpara.Value = bloc.IsSelf;
            paras.Add(isselfpara);

            SqlParameter blocstatuspara = new SqlParameter("@BlocStatus", SqlDbType.Int, 4);
            blocstatuspara.Value = bloc.BlocStatus;
            paras.Add(blocstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetBlocList(UserModel user)
        {
            ResultModel result = new ResultModel();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            try
            {
                sb.Append("select BlocId,BlocName from NFMT_User.dbo.Bloc ");
                sb.AppendFormat(" where BlocStatus = {0} ", (int)Common.StatusEnum.已生效);

                DataTable dt = SqlHelper.ExecuteDataTable(this.ConnectString, sb.ToString(), null, CommandType.Text);

                if (dt != null && dt.Rows.Count > 0)
                {
                    result.AffectCount = dt.Rows.Count;
                    result.Message = "获取集团列表成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = dt;
                }
                else
                {
                    result.Message = "获取集团列表失败";
                    result.ResultStatus = -1;
                }
            }
            catch (Exception e)
            {
                result.Message = string.Format("获取集团列表失败,{0}", e.Message);
                result.ResultStatus = -1;
            }

            return result;
        }

        public ResultModel IsExistSelfBolc(UserModel user)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = " select 1 from NFMT_User.dbo.Bloc where IsSelf =1 ";
                object obj = SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, sql, null);
                if (obj != null)
                {
                    result.Message = "存在己方集团";
                    result.ResultStatus = -1;
                    result.ReturnValue = true;
                }
                else
                {
                    result.Message = "不存在己方集团";
                    result.ResultStatus = 0;
                    result.ReturnValue = false;
                }
            }
            catch (Exception e)
            {
                result.Message = string.Format("发生异常，{0}", e.Message);
                result.ResultStatus = -1;
                result.ReturnValue = true;
            }
            return result;
        }

        public override int MenuId
        {
            get
            {
                return 15;
            }
        }

        public ResultModel GetBlocByCorpId(UserModel user, int corpId)
        {
            string cmdText = string.Format("select bloc.* from dbo.Corporation corp left join dbo.Bloc bloc on corp.ParentId = bloc.BlocId where corp.CorpId ={0}",corpId);
            return Get(user, CommandType.Text, cmdText, null);
        }

        #endregion
    }
}
