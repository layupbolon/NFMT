/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：RoleMenuOperateDAL.cs
// 文件功能描述：角色菜单操作关联表dbo.RoleMenuOperate数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年9月29日
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
    /// 角色菜单操作关联表dbo.RoleMenuOperate数据交互类。
    /// </summary>
    public class RoleMenuOperateDAL : DataOperate, IRoleMenuOperateDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public RoleMenuOperateDAL()
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
            RoleMenuOperate rolemenuoperate = (RoleMenuOperate)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@RefID";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter roleidpara = new SqlParameter("@RoleId", SqlDbType.Int, 4);
            roleidpara.Value = rolemenuoperate.RoleId;
            paras.Add(roleidpara);

            SqlParameter menuidpara = new SqlParameter("@MenuId", SqlDbType.Int, 4);
            menuidpara.Value = rolemenuoperate.MenuId;
            paras.Add(menuidpara);

            SqlParameter refstatuspara = new SqlParameter("@RefStatus", SqlDbType.Int, 4);
            refstatuspara.Value = rolemenuoperate.RefStatus;
            paras.Add(refstatuspara);

            SqlParameter operateidpara = new SqlParameter("@OperateId", SqlDbType.Int, 4);
            operateidpara.Value = rolemenuoperate.OperateId;
            paras.Add(operateidpara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            RoleMenuOperate rolemenuoperate = new RoleMenuOperate();

            int indexRefID = dr.GetOrdinal("RefID");
            rolemenuoperate.RefID = Convert.ToInt32(dr[indexRefID]);

            int indexRoleId = dr.GetOrdinal("RoleId");
            if (dr["RoleId"] != DBNull.Value)
            {
                rolemenuoperate.RoleId = Convert.ToInt32(dr[indexRoleId]);
            }

            int indexMenuId = dr.GetOrdinal("MenuId");
            if (dr["MenuId"] != DBNull.Value)
            {
                rolemenuoperate.MenuId = Convert.ToInt32(dr[indexMenuId]);
            }

            int indexRefStatus = dr.GetOrdinal("RefStatus");
            if (dr["RefStatus"] != DBNull.Value)
            {
                rolemenuoperate.RefStatus = (StatusEnum)Convert.ToInt32(dr[indexRefStatus]);
            }

            int indexOperateId = dr.GetOrdinal("OperateId");
            if (dr["OperateId"] != DBNull.Value)
            {
                rolemenuoperate.OperateId = Convert.ToInt32(dr[indexOperateId]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                rolemenuoperate.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                rolemenuoperate.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                rolemenuoperate.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                rolemenuoperate.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return rolemenuoperate;
        }

        public override string TableName
        {
            get
            {
                return "RoleMenuOperate";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            RoleMenuOperate rolemenuoperate = (RoleMenuOperate)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter refidpara = new SqlParameter("@RefID", SqlDbType.Int, 4);
            refidpara.Value = rolemenuoperate.RefID;
            paras.Add(refidpara);

            SqlParameter roleidpara = new SqlParameter("@RoleId", SqlDbType.Int, 4);
            roleidpara.Value = rolemenuoperate.RoleId;
            paras.Add(roleidpara);

            SqlParameter menuidpara = new SqlParameter("@MenuId", SqlDbType.Int, 4);
            menuidpara.Value = rolemenuoperate.MenuId;
            paras.Add(menuidpara);

            SqlParameter refstatuspara = new SqlParameter("@RefStatus", SqlDbType.Int, 4);
            refstatuspara.Value = rolemenuoperate.RefStatus;
            paras.Add(refstatuspara);

            SqlParameter operateidpara = new SqlParameter("@OperateId", SqlDbType.Int, 4);
            operateidpara.Value = rolemenuoperate.OperateId;
            paras.Add(operateidpara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        //#region 新增方法

        //public bool AuthorityOperate(UserModel user, OperateEnum operate, MenuEnum menu)
        //{
        //    bool result = false;

        //    int readyStatus = (int)StatusEnum.已生效;
        //    int menuId = (int)menu;
        //    int operateId = (int)operate;
        //    int empId = user.EmpId;

        //    List<SqlParameter> paras = new List<SqlParameter>();
        //    SqlParameter para = new SqlParameter("@empId", SqlDbType.Int, 4);
        //    para.Value = empId;
        //    paras.Add(para);

        //    para = new SqlParameter("@menuId", SqlDbType.Int, 4);
        //    para.Value = menuId;
        //    paras.Add(para);

        //    para = new SqlParameter("@operateId", SqlDbType.Int, 4);
        //    para.Value = operateId;
        //    paras.Add(para);

        //    para = new SqlParameter("@readyStatus", SqlDbType.Int, 4);
        //    para.Value = readyStatus;
        //    paras.Add(para);

        //    try
        //    {
        //        string cmdText = "select COUNT(*) from dbo.RoleMenuOperate rmo inner join dbo.EmpRole er on er.RoleId = rmo.RoleId and er.EmpId =@empId where MenuId = @menuId and OperateId=@operateId and rmo.RefStatus=@readyStatus";

        //        object obj = SqlHelper.ExecuteScalar(ConnectString, CommandType.Text, cmdText, paras.ToArray());
        //        int i = 0;

        //        if (obj != null && int.TryParse(obj.ToString(), out i))
        //        {
        //            if (i > 0)
        //                result = true;
        //        }
        //    }
        //    catch
        //    {
        //        result = false;
        //    }

        //    return result;
        //}

        //#endregion

    }
}
