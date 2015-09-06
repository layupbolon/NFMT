/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：NodeOperateDAL.cs
// 文件功能描述：节点操作表dbo.Wf_NodeOperate数据交互类。
// 创建人：CodeSmith
// 创建时间： 2015年4月17日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.WorkFlow.Model;
using NFMT.DBUtility;
using NFMT.WorkFlow.IDAL;
using NFMT.Common;
using System.Linq;

namespace NFMT.WorkFlow.DAL
{
    /// <summary>
    /// 节点操作表dbo.Wf_NodeOperate数据交互类。
    /// </summary>
    public partial class NodeOperateDAL : DataOperate, INodeOperateDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public NodeOperateDAL()
        {
        }

        #endregion

        #region 数据库操作

        public override string ConnectString
        {
            get
            {
                return NFMT.DBUtility.SqlHelper.ConnectionStringWorkFlow;
            }
        }

        public override List<SqlParameter> CreateInsertParameters(IModel obj, ref SqlParameter returnValue)
        {
            NodeOperate wf_nodeoperate = (NodeOperate)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@OperateId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter nodeidpara = new SqlParameter("@NodeId", SqlDbType.Int, 4);
            nodeidpara.Value = wf_nodeoperate.NodeId;
            paras.Add(nodeidpara);

            if (!string.IsNullOrEmpty(wf_nodeoperate.OperateUrl))
            {
                SqlParameter operateurlpara = new SqlParameter("@OperateUrl", SqlDbType.VarChar, 200);
                operateurlpara.Value = wf_nodeoperate.OperateUrl;
                paras.Add(operateurlpara);
            }

            SqlParameter operatestatuspara = new SqlParameter("@OperateStatus", SqlDbType.Int, 4);
            operatestatuspara.Value = wf_nodeoperate.OperateStatus;
            paras.Add(operatestatuspara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            NodeOperate nodeoperate = new NodeOperate();

            nodeoperate.OperateId = Convert.ToInt32(dr["OperateId"]);

            if (dr["NodeId"] != DBNull.Value)
            {
                nodeoperate.NodeId = Convert.ToInt32(dr["NodeId"]);
            }

            if (dr["OperateUrl"] != DBNull.Value)
            {
                nodeoperate.OperateUrl = Convert.ToString(dr["OperateUrl"]);
            }

            if (dr["OperateStatus"] != DBNull.Value)
            {
                nodeoperate.OperateStatus = (Common.StatusEnum)Convert.ToInt32(dr["OperateStatus"]);
            }


            return nodeoperate;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            NodeOperate nodeoperate = new NodeOperate();

            int indexOperateId = dr.GetOrdinal("OperateId");
            nodeoperate.OperateId = Convert.ToInt32(dr[indexOperateId]);

            int indexNodeId = dr.GetOrdinal("NodeId");
            if (dr["NodeId"] != DBNull.Value)
            {
                nodeoperate.NodeId = Convert.ToInt32(dr[indexNodeId]);
            }

            int indexOperateUrl = dr.GetOrdinal("OperateUrl");
            if (dr["OperateUrl"] != DBNull.Value)
            {
                nodeoperate.OperateUrl = Convert.ToString(dr[indexOperateUrl]);
            }

            int indexOperateStatus = dr.GetOrdinal("OperateStatus");
            if (dr["OperateStatus"] != DBNull.Value)
            {
                nodeoperate.OperateStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexOperateStatus]);
            }


            return nodeoperate;
        }

        public override string TableName
        {
            get
            {
                return "Wf_NodeOperate";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            NodeOperate wf_nodeoperate = (NodeOperate)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter operateidpara = new SqlParameter("@OperateId", SqlDbType.Int, 4);
            operateidpara.Value = wf_nodeoperate.OperateId;
            paras.Add(operateidpara);

            SqlParameter nodeidpara = new SqlParameter("@NodeId", SqlDbType.Int, 4);
            nodeidpara.Value = wf_nodeoperate.NodeId;
            paras.Add(nodeidpara);

            if (!string.IsNullOrEmpty(wf_nodeoperate.OperateUrl))
            {
                SqlParameter operateurlpara = new SqlParameter("@OperateUrl", SqlDbType.VarChar, 200);
                operateurlpara.Value = wf_nodeoperate.OperateUrl;
                paras.Add(operateurlpara);
            }

            SqlParameter operatestatuspara = new SqlParameter("@OperateStatus", SqlDbType.Int, 4);
            operatestatuspara.Value = wf_nodeoperate.OperateStatus;
            paras.Add(operatestatuspara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetByNodeId(UserModel user, int nodeId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select * from dbo.Wf_NodeOperate where NodeId = {0} and OperateStatus = {1}", nodeId, (int)Common.StatusEnum.已生效);
                result = Load<Model.NodeOperate>(user, CommandType.Text, sql);
                if (result.AffectCount > 0)
                {
                    result.Message = "获取成功";
                    result.ResultStatus = 0;
                    List<Model.NodeOperate> nodeOperates = result.ReturnValue as List<Model.NodeOperate>;
                    result.ReturnValue = nodeOperates.First();
                }
                else
                {
                    result.Message = "获取失败";
                    result.ResultStatus = -1;
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
