/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：EmployeeContactDAL.cs
// 文件功能描述：联系人员工关系表dbo.EmployeeContact数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年7月15日
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
    /// 联系人员工关系表dbo.EmployeeContact数据交互类。
    /// </summary>
    public class EmployeeContactDAL : ApplyOperate, IEmployeeContactDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public EmployeeContactDAL()
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
            EmployeeContact employeecontact = (EmployeeContact)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@ECId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter contactidpara = new SqlParameter("@ContactId", SqlDbType.Int, 4);
            contactidpara.Value = employeecontact.ContactId;
            paras.Add(contactidpara);

            SqlParameter empidpara = new SqlParameter("@EmpId", SqlDbType.Int, 4);
            empidpara.Value = employeecontact.EmpId;
            paras.Add(empidpara);

            SqlParameter refstatuspara = new SqlParameter("@RefStatus", SqlDbType.Int, 4);
            refstatuspara.Value = (int)Common.StatusEnum.已生效;
            paras.Add(refstatuspara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            EmployeeContact employeecontact = new EmployeeContact();

            int indexECId = dr.GetOrdinal("ECId");
            employeecontact.ECId = Convert.ToInt32(dr[indexECId]);

            int indexContactId = dr.GetOrdinal("ContactId");
            employeecontact.ContactId = Convert.ToInt32(dr[indexContactId]);

            int indexEmpId = dr.GetOrdinal("EmpId");
            employeecontact.EmpId = Convert.ToInt32(dr[indexEmpId]);

            int indexRefStatus = dr.GetOrdinal("RefStatus");
            if (dr["RefStatus"] != DBNull.Value)
            {
                employeecontact.RefStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexRefStatus]);
            }


            return employeecontact;
        }

        public override string TableName
        {
            get
            {
                return "EmployeeContact";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            EmployeeContact employeecontact = (EmployeeContact)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter ecidpara = new SqlParameter("@ECId", SqlDbType.Int, 4);
            ecidpara.Value = employeecontact.ECId;
            paras.Add(ecidpara);

            SqlParameter contactidpara = new SqlParameter("@ContactId", SqlDbType.Int, 4);
            contactidpara.Value = employeecontact.ContactId;
            paras.Add(contactidpara);

            SqlParameter empidpara = new SqlParameter("@EmpId", SqlDbType.Int, 4);
            empidpara.Value = employeecontact.EmpId;
            paras.Add(empidpara);

            SqlParameter refstatuspara = new SqlParameter("@RefStatus", SqlDbType.Int, 4);
            refstatuspara.Value = employeecontact.RefStatus;
            paras.Add(refstatuspara);


            return paras;
        }

        #endregion

        #region 新增方法
        
        public ResultModel Complete(UserModel user, IModel obj)
        {
            throw new NotImplementedException();
        }

        public override int MenuId
        {
            get
            {
                return 81;
            }
        }

        #endregion
    }
}
