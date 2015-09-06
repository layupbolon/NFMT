/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：Wf_NodeConditionDAL.cs
// 文件功能描述：节点条件表dbo.Wf_NodeCondition数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年7月11日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.WorkFlow.Model;
using NFMT.DBUtility;
using NFMT.WorkFlow.IDAL;
using NFMT.Common;

namespace NFMT.WorkFlow.DAL
{
    /// <summary>
    /// 节点条件表dbo.Wf_NodeCondition数据交互类。
    /// </summary>
    public class NodeConditionDAL : DataOperate, INodeConditionDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public NodeConditionDAL()
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
            NodeCondition wf_nodecondition = (NodeCondition)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@ConditionId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter conditionstatuspara = new SqlParameter("@ConditionStatus", SqlDbType.Int, 4);
            conditionstatuspara.Value = wf_nodecondition.ConditionStatus;
            paras.Add(conditionstatuspara);

            SqlParameter nodeidpara = new SqlParameter("@NodeId", SqlDbType.Int, 4);
            nodeidpara.Value = wf_nodecondition.NodeId;
            paras.Add(nodeidpara);

            if (!string.IsNullOrEmpty(wf_nodecondition.FieldName))
            {
                SqlParameter fieldnamepara = new SqlParameter("@FieldName", SqlDbType.VarChar, 50);
                fieldnamepara.Value = wf_nodecondition.FieldName;
                paras.Add(fieldnamepara);
            }

            if (!string.IsNullOrEmpty(wf_nodecondition.FieldValue))
            {
                SqlParameter fieldvaluepara = new SqlParameter("@FieldValue", SqlDbType.VarChar, 50);
                fieldvaluepara.Value = wf_nodecondition.FieldValue;
                paras.Add(fieldvaluepara);
            }

            SqlParameter conditiontypepara = new SqlParameter("@ConditionType", SqlDbType.Int, 4);
            conditiontypepara.Value = wf_nodecondition.ConditionType;
            paras.Add(conditiontypepara);

            SqlParameter logictypepara = new SqlParameter("@LogicType", SqlDbType.Int, 4);
            logictypepara.Value = wf_nodecondition.LogicType;
            paras.Add(logictypepara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            NodeCondition nodecondition = new NodeCondition();

            int indexConditionId = dr.GetOrdinal("ConditionId");
            nodecondition.ConditionId = Convert.ToInt32(dr[indexConditionId]);

            int indexConditionStatus = dr.GetOrdinal("ConditionStatus");
            nodecondition.ConditionStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexConditionStatus]);

            int indexNodeId = dr.GetOrdinal("NodeId");
            nodecondition.NodeId = Convert.ToInt32(dr[indexNodeId]);

            int indexFieldName = dr.GetOrdinal("FieldName");
            if (dr["FieldName"] != DBNull.Value)
            {
                nodecondition.FieldName = Convert.ToString(dr[indexFieldName]);
            }

            int indexFieldValue = dr.GetOrdinal("FieldValue");
            if (dr["FieldValue"] != DBNull.Value)
            {
                nodecondition.FieldValue = Convert.ToString(dr[indexFieldValue]);
            }

            int indexConditionType = dr.GetOrdinal("ConditionType");
            if (dr["ConditionType"] != DBNull.Value)
            {
                nodecondition.ConditionType = Convert.ToInt32(dr[indexConditionType]);
            }

            int indexLogicType = dr.GetOrdinal("LogicType");
            if (dr["LogicType"] != DBNull.Value)
            {
                nodecondition.LogicType = Convert.ToInt32(dr[indexLogicType]);
            }


            return nodecondition;
        }

        public override string TableName
        {
            get
            {
                return "Wf_NodeCondition";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            NodeCondition wf_nodecondition = (NodeCondition)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter conditionidpara = new SqlParameter("@ConditionId", SqlDbType.Int, 4);
            conditionidpara.Value = wf_nodecondition.ConditionId;
            paras.Add(conditionidpara);

            SqlParameter conditionstatuspara = new SqlParameter("@ConditionStatus", SqlDbType.Int, 4);
            conditionstatuspara.Value = wf_nodecondition.ConditionStatus;
            paras.Add(conditionstatuspara);

            SqlParameter nodeidpara = new SqlParameter("@NodeId", SqlDbType.Int, 4);
            nodeidpara.Value = wf_nodecondition.NodeId;
            paras.Add(nodeidpara);

            if (!string.IsNullOrEmpty(wf_nodecondition.FieldName))
            {
                SqlParameter fieldnamepara = new SqlParameter("@FieldName", SqlDbType.VarChar, 50);
                fieldnamepara.Value = wf_nodecondition.FieldName;
                paras.Add(fieldnamepara);
            }

            if (!string.IsNullOrEmpty(wf_nodecondition.FieldValue))
            {
                SqlParameter fieldvaluepara = new SqlParameter("@FieldValue", SqlDbType.VarChar, 50);
                fieldvaluepara.Value = wf_nodecondition.FieldValue;
                paras.Add(fieldvaluepara);
            }

            SqlParameter conditiontypepara = new SqlParameter("@ConditionType", SqlDbType.Int, 4);
            conditiontypepara.Value = wf_nodecondition.ConditionType;
            paras.Add(conditiontypepara);

            SqlParameter logictypepara = new SqlParameter("@LogicType", SqlDbType.Int, 4);
            logictypepara.Value = wf_nodecondition.LogicType;
            paras.Add(logictypepara);


            return paras;
        }

        #endregion
    }
}
