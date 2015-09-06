/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：AuditEmpDAL.cs
// 文件功能描述：审核人表dbo.Wf_AuditEmp数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年11月12日
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
    /// 审核人表dbo.Wf_AuditEmp数据交互类。
    /// </summary>
    public class AuditEmpDAL : DataOperate, IAuditEmpDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public AuditEmpDAL()
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
            AuditEmp wf_auditemp = (AuditEmp)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@AuditEmpId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter auditemptypepara = new SqlParameter("@AuditEmpType", SqlDbType.Int, 4);
            auditemptypepara.Value = wf_auditemp.AuditEmpType;
            paras.Add(auditemptypepara);

            SqlParameter valueidpara = new SqlParameter("@ValueId", SqlDbType.Int, 4);
            valueidpara.Value = wf_auditemp.ValueId;
            paras.Add(valueidpara);

            SqlParameter auditempstatuspara = new SqlParameter("@AuditEmpStatus", SqlDbType.Int, 4);
            auditempstatuspara.Value = wf_auditemp.AuditEmpStatus;
            paras.Add(auditempstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            AuditEmp auditemp = new AuditEmp();

            auditemp.AuditEmpId = Convert.ToInt32(dr["AuditEmpId"]);

            if (dr["AuditEmpType"] != DBNull.Value)
            {
                auditemp.AuditEmpType = Convert.ToInt32(dr["AuditEmpType"]);
            }

            if (dr["ValueId"] != DBNull.Value)
            {
                auditemp.ValueId = Convert.ToInt32(dr["ValueId"]);
            }

            if (dr["AuditEmpStatus"] != DBNull.Value)
            {
                auditemp.AuditEmpStatus = (Common.StatusEnum)Convert.ToInt32(dr["AuditEmpStatus"]);
            }

            if (dr["CreatorId"] != DBNull.Value)
            {
                auditemp.CreatorId = Convert.ToInt32(dr["CreatorId"]);
            }

            if (dr["CreateTime"] != DBNull.Value)
            {
                auditemp.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
            }

            if (dr["LastModifyId"] != DBNull.Value)
            {
                auditemp.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
            }

            if (dr["LastModifyTime"] != DBNull.Value)
            {
                auditemp.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
            }


            return auditemp;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            AuditEmp auditemp = new AuditEmp();

            int indexAuditEmpId = dr.GetOrdinal("AuditEmpId");
            auditemp.AuditEmpId = Convert.ToInt32(dr[indexAuditEmpId]);

            int indexAuditEmpType = dr.GetOrdinal("AuditEmpType");
            if (dr["AuditEmpType"] != DBNull.Value)
            {
                auditemp.AuditEmpType = Convert.ToInt32(dr[indexAuditEmpType]);
            }

            int indexValueId = dr.GetOrdinal("ValueId");
            if (dr["ValueId"] != DBNull.Value)
            {
                auditemp.ValueId = Convert.ToInt32(dr[indexValueId]);
            }

            int indexAuditEmpStatus = dr.GetOrdinal("AuditEmpStatus");
            if (dr["AuditEmpStatus"] != DBNull.Value)
            {
                auditemp.AuditEmpStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexAuditEmpStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                auditemp.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                auditemp.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                auditemp.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                auditemp.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return auditemp;
        }

        public override string TableName
        {
            get
            {
                return "Wf_AuditEmp";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            AuditEmp wf_auditemp = (AuditEmp)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter auditempidpara = new SqlParameter("@AuditEmpId", SqlDbType.Int, 4);
            auditempidpara.Value = wf_auditemp.AuditEmpId;
            paras.Add(auditempidpara);

            SqlParameter auditemptypepara = new SqlParameter("@AuditEmpType", SqlDbType.Int, 4);
            auditemptypepara.Value = wf_auditemp.AuditEmpType;
            paras.Add(auditemptypepara);

            SqlParameter valueidpara = new SqlParameter("@ValueId", SqlDbType.Int, 4);
            valueidpara.Value = wf_auditemp.ValueId;
            paras.Add(valueidpara);

            SqlParameter auditempstatuspara = new SqlParameter("@AuditEmpStatus", SqlDbType.Int, 4);
            auditempstatuspara.Value = wf_auditemp.AuditEmpStatus;
            paras.Add(auditempstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetEmpIdsByAuditEmpId(UserModel user, int auditEmpId, Model.DataSource source)
        {
            ResultModel result = new ResultModel();

            try
            {
                int readyStatus = (int)Common.StatusEnum.已生效;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("declare @auditEmpId int,@AuditEmpType int,@valueId int ");
                sb.Append(Environment.NewLine);
                sb.AppendFormat("set @auditEmpId = {0} ", auditEmpId);
                sb.Append(Environment.NewLine);
                sb.AppendFormat("select @AuditEmpType = AuditEmpType from NFMT_WorkFlow.dbo.Wf_AuditEmp where AuditEmpId = @auditEmpId and AuditEmpStatus = {0}", readyStatus);
                sb.Append(Environment.NewLine);
                sb.AppendFormat("select @valueId = ValueId from NFMT_WorkFlow.dbo.Wf_AuditEmp where AuditEmpId = @auditEmpId and AuditEmpStatus = {0}", readyStatus);
                sb.Append(Environment.NewLine);
                sb.AppendFormat("if @AuditEmpType = {0} ", NFMT.Data.DetailProvider.Details(Data.StyleEnum.AuditEmpType)["Operater"].StyleDetailId);//操作员
                sb.Append(Environment.NewLine);
                sb.Append("begin");
                sb.Append(Environment.NewLine);
                sb.Append("	select @valueId as EmpId");
                sb.Append(Environment.NewLine);
                sb.Append("end");
                sb.Append(Environment.NewLine);
                sb.AppendFormat("else if @AuditEmpType = {0} ", NFMT.Data.DetailProvider.Details(Data.StyleEnum.AuditEmpType)["EmpRole"].StyleDetailId);//部门角色
                sb.Append(Environment.NewLine);
                sb.Append("begin");
                sb.Append(Environment.NewLine);
                sb.AppendFormat("	select ISNULL(er.EmpId,0) as EmpId from NFMT_User.dbo.RoleDept rd left join NFMT_User.dbo.Employee e on rd.DeptId = e.DeptId  left join NFMT_User.dbo.EmpRole er on e.EmpId = er.EmpId and er.RoleId = rd.RoleId where rd.RoleDeptId = @valueId and rd.RefStatus >= {0} and er.RefStatus >= {0} and e.WorkStatus = {1}", readyStatus, (int)NFMT.User.WorkStatusEnum.在职);
                sb.Append(Environment.NewLine);
                sb.Append("end");
                sb.Append(Environment.NewLine);
                sb.AppendFormat("else if @AuditEmpType = {0} ", NFMT.Data.DetailProvider.Details(Data.StyleEnum.AuditEmpType)["Leadership"].StyleDetailId);//部门上级领导
                sb.Append(Environment.NewLine);
                sb.Append("begin");
                sb.Append(Environment.NewLine);
                sb.Append("	declare @UserId int");
                sb.Append(Environment.NewLine);
                sb.AppendFormat("set @UserId = {0}", user.EmpId);
                sb.Append(Environment.NewLine);
                sb.Append("select ISNULL(e.EmpId,0) as EmpId from NFMT_User.dbo.Employee e left join NFMT_User.dbo.EmpRole er on e.EmpId = er.EmpId where e.DeptId = (select DeptId from NFMT_User.dbo.Employee where EmpId = @UserId) and er.RoleId = @valueId");
                sb.Append(Environment.NewLine);
                sb.Append("end");
                sb.Append(Environment.NewLine);
                sb.AppendFormat("else if @AuditEmpType = {0} ", NFMT.Data.DetailProvider.Details(Data.StyleEnum.AuditEmpType)["ApplyCorpFinanceManager"].StyleDetailId);//某公司财务经理
                sb.Append(Environment.NewLine);
                sb.Append("begin");
                sb.Append(Environment.NewLine);
                sb.Append("select ISNULL(e.EmpId,0) as EmpId from NFMT_User.dbo.Employee e inner join NFMT_User.dbo.Department d on e.DeptId = d.DeptId and d.DeptId in (34,37,39,54,59) inner join NFMT_User.dbo.EmpRole er on e.EmpId = er.EmpId and er.RoleId = 4 inner join NFMT_User.dbo.Corporation c on d.CorpId = c.CorpId where c.CorpId = @valueId ");
                sb.Append(Environment.NewLine);
                sb.Append("end");
                sb.Append(Environment.NewLine);
                sb.AppendFormat("else if @AuditEmpType = {0} ", NFMT.Data.DetailProvider.Details(Data.StyleEnum.AuditEmpType)["Role"].StyleDetailId);//角色
                sb.Append(Environment.NewLine);
                sb.Append("begin");
                sb.Append(Environment.NewLine);
                sb.AppendFormat(" select ISNULL(EmpId,0) as EmpId from NFMT_User.dbo.EmpRole where RoleId = @valueId and RefStatus = {0}", (int)Common.StatusEnum.已生效);
                sb.Append(Environment.NewLine);
                sb.Append("end");
                sb.Append(Environment.NewLine);
                sb.Append("else");
                sb.Append(Environment.NewLine);
                sb.Append("begin");
                sb.Append(Environment.NewLine);
                sb.Append("	select 0 as EmpId");
                sb.Append(Environment.NewLine);
                sb.Append("end");


                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sb.ToString(), null, CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result.Message = "获取审核人成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = dt;
                }
                else
                {
                    result.Message = "无审核人";
                    result.ResultStatus = 0;
                }
            }
            catch (Exception e)
            {
                result.Message = string.Format("获取失败,{0}", e.Message);
                result.ResultStatus = -1;
            }

            return result;
        }
        #endregion
    }
}
