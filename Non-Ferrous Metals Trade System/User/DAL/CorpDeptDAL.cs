/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CorpDeptDAL.cs
// 文件功能描述：公司部门关联表dbo.CorpDept数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年9月17日
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
    /// 公司部门关联表dbo.CorpDept数据交互类。
    /// </summary>
    public class CorpDeptDAL : DataOperate , ICorpDeptDAL
    {
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public CorpDeptDAL()
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
            CorpDept corpdept = (CorpDept)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@CorpEmpId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter deptidpara = new SqlParameter("@DeptId", SqlDbType.Int, 4);
            deptidpara.Value = corpdept.DeptId;
            paras.Add(deptidpara);

            SqlParameter corpidpara = new SqlParameter("@CorpId", SqlDbType.Int, 4);
            corpidpara.Value = corpdept.CorpId;
            paras.Add(corpidpara);

            SqlParameter refstatuspara = new SqlParameter("@RefStatus", SqlDbType.Int, 4);
            refstatuspara.Value = corpdept.RefStatus;
            paras.Add(refstatuspara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            CorpDept corpdept = new CorpDept();

            int indexCorpEmpId = dr.GetOrdinal("CorpEmpId");
            corpdept.CorpEmpId = Convert.ToInt32(dr[indexCorpEmpId]);

            int indexDeptId = dr.GetOrdinal("DeptId");
            if (dr["DeptId"] != DBNull.Value)
            {
                corpdept.DeptId = Convert.ToInt32(dr[indexDeptId]);
            }

            int indexCorpId = dr.GetOrdinal("CorpId");
            if (dr["CorpId"] != DBNull.Value)
            {
                corpdept.CorpId = Convert.ToInt32(dr[indexCorpId]);
            }

            int indexRefStatus = dr.GetOrdinal("RefStatus");
            if (dr["RefStatus"] != DBNull.Value)
            {
                corpdept.RefStatus = Convert.ToInt32(dr[indexRefStatus]);
            }


            return corpdept;
        }

        public override string TableName
        {
            get
            {
                return "CorpDept";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            CorpDept corpdept = (CorpDept)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter corpempidpara = new SqlParameter("@CorpEmpId", SqlDbType.Int, 4);
            corpempidpara.Value = corpdept.CorpEmpId;
            paras.Add(corpempidpara);

            SqlParameter deptidpara = new SqlParameter("@DeptId", SqlDbType.Int, 4);
            deptidpara.Value = corpdept.DeptId;
            paras.Add(deptidpara);

            SqlParameter corpidpara = new SqlParameter("@CorpId", SqlDbType.Int, 4);
            corpidpara.Value = corpdept.CorpId;
            paras.Add(corpidpara);

            SqlParameter refstatuspara = new SqlParameter("@RefStatus", SqlDbType.Int, 4);
            refstatuspara.Value = corpdept.RefStatus;
            paras.Add(refstatuspara);


            return paras;
        }

        #endregion
    }
}
