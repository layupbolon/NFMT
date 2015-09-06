/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：Operate.cs
// 文件功能描述：操作抽象类。
// 创建人：pekah.chow
// 创建时间： 2014-04-23
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using NFMT.DBUtility;

namespace NFMT.Common
{
    public abstract class Operate : IOperate
    {
        /// <summary>
        /// 返回是否允许对该实例进行相应操作。
        /// </summary>
        /// <param name="user">user</param>
        /// <param name="obj">判断实例对象</param>
        /// <param name="operate">操作类型</param>
        /// <returns>返回true则可以进行操作，返回false则不可进行操作。</returns>
        public virtual ResultModel AllowOperate(UserModel user, IModel obj, OperateEnum operate)
        {
            ResultModel result = new ResultModel();

            bool allow = false;

            switch (operate)
            {
                case OperateEnum.作废:
                    allow = (obj.Status >= StatusEnum.已录入 && obj.Status < StatusEnum.待审核) || obj.Status == StatusEnum.绑定合约;
                    break;
                case OperateEnum.修改:
                    allow = (obj.Status >= StatusEnum.已录入 && obj.Status < StatusEnum.待审核);
                    break;
                case OperateEnum.提交审核:
                    allow = (obj.Status >= StatusEnum.已录入 && obj.Status < StatusEnum.待审核);
                    break;
                case OperateEnum.撤返:
                    allow = obj.Status == StatusEnum.待审核;
                    break;
                case OperateEnum.冻结:
                    allow = obj.Status == StatusEnum.已生效;
                    break;
                case OperateEnum.解除冻结:
                    allow = obj.Status == StatusEnum.已冻结;
                    break;
                case OperateEnum.执行完成:
                    allow = obj.Status == StatusEnum.已生效;
                    break;
                case OperateEnum.确认完成:
                    allow = obj.Status == StatusEnum.已生效;
                    break;
                case OperateEnum.执行完成撤销:
                    allow = (obj.Status == StatusEnum.已完成 || obj.Status == StatusEnum.部分完成);
                    break;
                case OperateEnum.确认完成撤销:
                    allow = (obj.Status == StatusEnum.已完成 || obj.Status == StatusEnum.部分完成);
                    break;
                case OperateEnum.关闭:
                    allow = obj.Status == StatusEnum.已生效;
                    break;
                default:
                    allow = true;
                    break;
            }

            if (!allow)
            {
                result.ResultStatus = -1;
                result.Message = string.Format("{0}的数据不能进行{1}操作", obj.Status.ToString("F"), operate.ToString("F"));
                return result;
            }

            if (!this.OperateAuthority(user, operate))
            {
                result.ResultStatus = -1;
                result.Message = string.Format("没有当前数据的{0}权限", operate.ToString("F"));
                return result;
            }

            result.ResultStatus = 0;
            return result;
        }

        public virtual int MenuId
        {
            get { return 0; }
        }

        public virtual bool OperateAuthority(UserModel user, OperateEnum operate)
        {
            if (this.MenuId == 0)
                return true;

            string cmdText =
                string.Format(
                    "select count(*) from NFMT_User.dbo.AuthOperate ao where ao.EmpId ={0} and ao.MenuId ={1} and ao.OperateType ={2}  and ao.AuthOperateStatus >= {3}",
                    user.EmpId, this.MenuId, (int)operate, (int)StatusEnum.已生效);
            object obj = SqlHelper.ExecuteScalar(ConnectString, CommandType.Text, cmdText, null);
            int rows = 0;
            if (obj == null || !int.TryParse(obj.ToString(), out rows) || rows <= 0)
                return false;
            if (rows > 0)
                return true;

            return false;
        }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public virtual string ConnectString
        {
            get { return SqlHelper.ConnectionStringNFMT; }
        }

        public virtual ResultModel Get(UserModel user, int id)
        {
            ResultModel result = new ResultModel();

            if (id < 1)
            {
                result.Message = "序号不能小于1";
                return result;
            }

            List<SqlParameter> paras = new List<SqlParameter>();
            SqlParameter para = new SqlParameter("@id", SqlDbType.Int, 4);
            para.Value = id;
            paras.Add(para);

            return this.Get(user, CommandType.StoredProcedure, this.TableName + "Get", paras.ToArray());

            //SqlDataReader dr = null;
            //try
            //{
            //    dr = SqlHelper.ExecuteReader(ConnectString, CommandType.StoredProcedure, this.TableName + "Get", paras.ToArray());

            //    IModel model = null;

            //    if (dr.Read())
            //    {
            //        model = CreateModel(dr);

            //        result.AffectCount = 1;
            //        result.Message = "读取成功";
            //        result.ResultStatus = 0;
            //        result.ReturnValue = model;
            //    }
            //    else
            //    {
            //        result.Message = "读取失败或无数据";
            //        result.AffectCount = 0;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    result.ResultStatus = -1;
            //    result.Message = ex.Message;
            //}
            //finally
            //{
            //    if (dr != null)
            //        dr.Dispose();
            //}
            //return result;
        }

        public virtual ResultModel Get(UserModel user, CommandType cmdType, string cmdText, SqlParameter[] paras)
        {
            ResultModel result = new ResultModel();

            SqlDataReader dr = null;
            try
            {
                dr = SqlHelper.ExecuteReader(ConnectString, cmdType, cmdText, paras);

                IModel model = null;

                if (dr.Read())
                {
                    model = CreateModel(dr);

                    result.AffectCount = 1;
                    result.Message = "读取成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = model;
                }
                else
                {
                    result.Message = "读取失败或无数据";
                    result.AffectCount = 0;
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }
            finally
            {
                if (dr != null)
                    dr.Dispose();
            }
            return result;
        }

        public abstract string TableName { get; }

        public abstract IModel CreateModel(SqlDataReader dr);

        public virtual IModel CreateModel(DataRow dr) { return null; }

        public T CreateModel<T>(SqlDataReader dr)
        where T : class,IModel
        {
            IModel model = this.CreateModel(dr);
            T t = model as T;

            return t;
        }

        public virtual ResultModel Get(UserModel user, string dataBaseName, string tableName, int rowId)
        {
            ResultModel result = new ResultModel();
            SqlDataReader dr = null;
            string keyName = string.Empty, StatusName = string.Empty;

            if (string.IsNullOrEmpty(dataBaseName) || string.IsNullOrEmpty(tableName))
            {
                result.Message = "数据源中数据库名或表名为空";
                result.ResultStatus = -1;
                return result;
            }

            try
            {
                dr = SqlHelper.ExecuteReader(this.ConnectString, CommandType.Text, string.Format("select KeyName,StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = '{0}'", tableName.IndexOf("dbo.", StringComparison.Ordinal) > -1 ? tableName : "dbo." + tableName), null);

                if (dr.Read())
                {
                    keyName = dr["KeyName"].ToString();
                    StatusName = dr["StatusNameCode"].ToString();
                }
                dr = null;
                if (string.IsNullOrEmpty(keyName) || string.IsNullOrEmpty(StatusName))
                {
                    result.Message = "BDStatus表中主键或状态名称不存在";
                    result.ResultStatus = -1;
                    return result;
                }

                string sqlString = string.Format("select * from {0}.{1} WITH (NOLOCK) where {2}={3}", dataBaseName, tableName, keyName, rowId);//改成参数
                dr = SqlHelper.ExecuteReader(this.ConnectString, CommandType.Text, sqlString, null);

                AuditModel model = new AuditModel();
                if (dr.Read())
                {
                    model.Id = rowId;
                    model.Status = (StatusEnum)Enum.Parse(typeof(StatusEnum), dr[StatusName].ToString());
                    model.TableName = tableName;

                    result.ReturnValue = model;
                    result.ResultStatus = 0;
                }

            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }
            finally
            {
                if (dr != null)
                    dr.Dispose();
            }

            return result;
        }

        public virtual ResultModel Load<T>(UserModel user)
            where T : class,IModel
        {
            return this.Load<T>(user, CommandType.StoredProcedure, this.TableName + "Load");
            //ResultModel result = new ResultModel();

            //SqlDataReader dr = null;
            //try
            //{
            //    dr = SqlHelper.ExecuteReader(ConnectString, CommandType.StoredProcedure, this.TableName + "Load", null);
            //    List<T> models = new List<T>();

            //    int i = 0;
            //    while (dr.Read())
            //    {
            //        T t = this.CreateModel<T>(dr);
            //        models.Add(t);
            //        i++;
            //    }

            //    result.AffectCount = i;
            //    result.Message = "获取列表成功";
            //    result.ResultStatus = 0;
            //    result.ReturnValue = models;
            //}
            //catch (Exception ex)
            //{
            //    result.ResultStatus = -1;
            //    result.Message = ex.Message;
            //}
            //finally
            //{
            //    if (dr != null)
            //        dr.Dispose();
            //}
            //return result;
        }

        public virtual ResultModel Load<T>(UserModel user, CommandType cmdType, string cmdText)
            where T : class,IModel
        {
            ResultModel result = new ResultModel();

            SqlDataReader dr = null;
            try
            {
                dr = SqlHelper.ExecuteReader(ConnectString, cmdType, cmdText, null);
                List<T> models = new List<T>();

                int i = 0;
                while (dr.Read())
                {
                    T t = this.CreateModel<T>(dr);
                    models.Add(t);
                    i++;
                }

                result.AffectCount = i;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = models;
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }
            finally
            {
                if (dr != null)
                    dr.Dispose();
            }
            return result;
        }

        public virtual ResultModel Load(UserModel user)
        {
            ResultModel result = new ResultModel();

            SqlDataReader dr = null;
            try
            {
                dr = SqlHelper.ExecuteReader(ConnectString, CommandType.StoredProcedure, this.TableName + "Load", null);

                List<IModel> models = new List<IModel>();

                int i = 0;
                while (dr.Read())
                {
                    models.Add(CreateModel(dr));
                    i++;
                }

                result.AffectCount = i;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = models;
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }
            finally
            {
                if (dr != null)
                    dr.Dispose();
            }
            return result;
        }

        public virtual ResultModel Load(UserModel user, SelectModel select)
        {
            ResultModel result = this.Load(user, select, this.Authority);
            return result;
        }

        public virtual ResultModel Load(UserModel user, SelectModel select, IAuthority authority)
        {
            ResultModel result = new ResultModel();

            try
            {
                if (select == null)
                {
                    result.Message = "查询对象不能为null";
                    return result;
                }
                if (string.IsNullOrEmpty(select.TableName))
                {
                    result.Message = "查询表名不能为空";
                    return result;
                }

                List<SqlParameter> paras = new List<SqlParameter>();

                SqlParameter pageIndexPara = new SqlParameter("@pageIndex", SqlDbType.Int, 4);
                pageIndexPara.Value = select.PageIndex;
                paras.Add(pageIndexPara);

                SqlParameter pageSizePara = new SqlParameter("@pageSize", SqlDbType.Int, 4);
                pageSizePara.Value = select.PageSize;
                paras.Add(pageSizePara);

                SqlParameter columnNamePara = new SqlParameter("@columnName", SqlDbType.VarChar);
                columnNamePara.Value = select.ColumnName;
                paras.Add(columnNamePara);

                SqlParameter tableNamePara = new SqlParameter("@tableName", SqlDbType.VarChar);
                tableNamePara.Value = select.TableName;
                paras.Add(tableNamePara);

                SqlParameter orderStrPara = new SqlParameter("@orderStr", SqlDbType.VarChar);
                orderStrPara.Value = select.OrderStr;
                paras.Add(orderStrPara);

                SqlParameter whereStrPara = new SqlParameter("@whereStr", SqlDbType.VarChar);

                if (string.IsNullOrEmpty(select.WhereStr))
                    select.WhereStr = "1=1 ";

                select.WhereStr += authority.CreateAuthorityStr(user).ReturnValue.ToString();

                whereStrPara.Value = select.WhereStr;
                paras.Add(whereStrPara);

                SqlParameter rowCountPara = new SqlParameter();
                rowCountPara.Direction = ParameterDirection.InputOutput;
                rowCountPara.SqlDbType = SqlDbType.Int;
                rowCountPara.ParameterName = "@rowCount";
                rowCountPara.Size = 4;
                rowCountPara.Value = 0;
                paras.Add(rowCountPara);

                DataTable dt = SqlHelper.ExecuteDataTable(this.ConnectString, "dbo.SelectPager", paras.ToArray(), CommandType.StoredProcedure);

                if (dt != null)
                {
                    result.AffectCount = (int)rowCountPara.Value;
                    result.ResultStatus = 0;
                    result.Message = "获取数据成功";
                    result.ReturnValue = dt;
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "获取数据失败失败";
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public virtual ResultModel Insert(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();
            try
            {
                if (obj == null)
                {
                    result.Message = "插入对象不能为null";
                    return result;
                }

                SqlParameter returnValue = new SqlParameter();
                obj.CreatorId = user.EmpId;

                if ((int)obj.Status == 0)
                    obj.Status = StatusEnum.已录入;

                int i = SqlHelper.ExecuteNonQuery(ConnectString, CommandType.StoredProcedure, string.Format("{0}Insert", obj.TableName), CreateInsertParameters(obj, ref returnValue).ToArray());

                if (i > 0)
                {
                    result.AffectCount = i;
                    result.ResultStatus = 0;
                    result.Message = "添加成功";
                    result.ReturnValue = returnValue.Value;
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "添加失败";
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public virtual List<SqlParameter> CreateInsertParameters(IModel obj, ref SqlParameter returnValue) { return null; }

        public virtual ResultModel Update(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();

            try
            {
                if (obj == null)
                {
                    result.Message = "更新对象不能为null";
                    return result;
                }

                result = AllowOperate(user, obj, OperateEnum.修改);
                if (result.ResultStatus != 0)
                    return result;

                obj.LastModifyId = user.EmpId;

                int i = SqlHelper.ExecuteNonQuery(ConnectString, CommandType.StoredProcedure, string.Format("{0}Update", obj.TableName), CreateUpdateParameters(obj).ToArray());

                if (i == 1)
                {
                    result.Message = "更新成功";
                    result.ResultStatus = 0;
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "更新失败";
                }
                result.AffectCount = i;
                result.ReturnValue = i;
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public virtual List<SqlParameter> CreateUpdateParameters(IModel obj) { return null; }

        /// <summary>
        /// 数据提交审核
        /// </summary>
        /// <param name="user">当前操作用户</param>
        /// <param name="obj">提交审核数据对象实例</param>
        /// <returns></returns>
        public virtual ResultModel Submit(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();

            try
            {
                if (obj == null)
                {
                    result.ResultStatus = -1;
                    result.Message = "提交审核对象不能为null";
                    return result;
                }

                result = AllowOperate(user, obj, OperateEnum.提交审核);
                if (result.ResultStatus != 0)
                    return result;

                List<SqlParameter> paras = new List<SqlParameter>();

                SqlParameter idPara = new SqlParameter("@id", SqlDbType.Int, 4);
                idPara.Value = obj.Id;
                paras.Add(idPara);

                SqlParameter statusPara = new SqlParameter("@status", SqlDbType.Int, 4);
                statusPara.Value = (int)StatusEnum.待审核;
                paras.Add(statusPara);

                SqlParameter lastModifyIdPara = new SqlParameter("@lastModifyId", SqlDbType.Int, 4);
                lastModifyIdPara.Value = user.AccountId;
                paras.Add(lastModifyIdPara);

                int i = SqlHelper.ExecuteNonQuery(ConnectString, CommandType.StoredProcedure, string.Format("{0}UpdateStatus", obj.TableName), paras.ToArray());

                if (i == 1)
                {
                    result.Message = "提交审核成功";
                    result.ResultStatus = 0;
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "提交审核失败";
                }
                result.AffectCount = i;
                result.ReturnValue = i;
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public virtual ResultModel Audit(UserModel user, IModel obj, bool isPass)
        {
            ResultModel result = new ResultModel();

            try
            {
                if (obj == null)
                {
                    result.Message = "审核对象不能为null";
                    return result;
                }

                List<SqlParameter> paras = new List<SqlParameter>();

                SqlParameter idPara = new SqlParameter("@id", SqlDbType.Int, 4);
                idPara.Value = obj.Id;
                paras.Add(idPara);

                SqlParameter statusPara = new SqlParameter("@status", SqlDbType.Int, 4);
                paras.Add(statusPara);
                if (isPass)
                    statusPara.Value = (int)StatusEnum.已生效;
                else
                    statusPara.Value = (int)StatusEnum.审核拒绝;

                SqlParameter lastModifyIdPara = new SqlParameter("@lastModifyId", SqlDbType.Int, 4);
                lastModifyIdPara.Value = user.AccountId;
                paras.Add(lastModifyIdPara);

                int i = SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.StoredProcedure, string.Format("{0}UpdateStatus", obj.TableName), paras.ToArray());

                if (i == 1)
                {
                    result.Message = "审核成功";
                    result.ResultStatus = 0;
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "审核失败";
                }
                result.AffectCount = i;
                result.ReturnValue = i;
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public virtual ResultModel Goback(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();

            try
            {
                if (obj == null)
                {
                    result.Message = "撤返对象不能为null";
                    return result;
                }

                result = AllowOperate(user, obj, OperateEnum.撤返);
                if (result.ResultStatus != 0)
                    return result;

                List<SqlParameter> paras = new List<SqlParameter>();

                SqlParameter idPara = new SqlParameter("@id", SqlDbType.Int, 4);
                idPara.Value = obj.Id;
                paras.Add(idPara);

                SqlParameter statusPara = new SqlParameter("@status", SqlDbType.Int, 4);
                statusPara.Value = (int)StatusEnum.已撤返;
                paras.Add(statusPara);

                SqlParameter lastModifyIdPara = new SqlParameter("@lastModifyId", SqlDbType.Int, 4);
                lastModifyIdPara.Value = user.AccountId;
                paras.Add(lastModifyIdPara);

                //SqlParameter dbNamepara = new SqlParameter("@dbName", SqlDbType.VarChar, 200);
                //dbNamepara.Value = obj.DataBaseName;
                //paras.Add(dbNamepara);

                //SqlParameter tableNamepara = new SqlParameter("@tableName", SqlDbType.VarChar, 50);
                //tableNamepara.Value = obj.TableName;
                //paras.Add(tableNamepara);

                int i = SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.StoredProcedure, string.Format("{0}UpdateStatus", obj.TableName), paras.ToArray());

                if (i > 0)
                {
                    result.Message = "撤返成功";
                    result.ResultStatus = 0;
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "撤返失败";
                }
                result.AffectCount = i;
                result.ReturnValue = i;
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public virtual ResultModel Invalid(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();

            try
            {
                if (obj == null)
                {
                    result.Message = "作废对象不能为null";
                    return result;
                }

                result = AllowOperate(user, obj, OperateEnum.作废);
                if (result.ResultStatus != 0)
                    return result;

                List<SqlParameter> paras = new List<SqlParameter>();

                SqlParameter idPara = new SqlParameter("@id", SqlDbType.Int, 4);
                idPara.Value = obj.Id;
                paras.Add(idPara);

                SqlParameter statusPara = new SqlParameter("@status", SqlDbType.Int, 4);
                statusPara.Value = (int)StatusEnum.已作废;
                paras.Add(statusPara);

                SqlParameter lastModifyIdPara = new SqlParameter("@lastModifyId", SqlDbType.Int, 4);
                lastModifyIdPara.Value = user.AccountId;
                paras.Add(lastModifyIdPara);

                int i = SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.StoredProcedure, string.Format("{0}UpdateStatus", obj.TableName), paras.ToArray());

                if (i == 1)
                {
                    result.Message = "作废成功";
                    result.ResultStatus = 0;
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "作废失败";
                }
                result.AffectCount = i;
                result.ReturnValue = i;
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public virtual ResultModel Close(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();

            try
            {
                if (obj == null)
                {
                    result.Message = "关闭对象不能为null";
                    return result;
                }

                result = AllowOperate(user, obj, OperateEnum.关闭);
                if (result.ResultStatus != 0)
                    return result;

                List<SqlParameter> paras = new List<SqlParameter>();

                SqlParameter idPara = new SqlParameter("@id", SqlDbType.Int, 4);
                idPara.Value = obj.Id;
                paras.Add(idPara);

                SqlParameter statusPara = new SqlParameter("@status", SqlDbType.Int, 4);
                statusPara.Value = (int)StatusEnum.已关闭;
                paras.Add(statusPara);

                SqlParameter lastModifyIdPara = new SqlParameter("@lastModifyId", SqlDbType.Int, 4);
                lastModifyIdPara.Value = user.AccountId;
                paras.Add(lastModifyIdPara);

                int i = SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.StoredProcedure, string.Format("{0}UpdateStatus", obj.TableName), paras.ToArray());

                if (i == 1)
                {
                    result.Message = "关闭成功";
                    result.ResultStatus = 0;
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "关闭失败";
                }
                result.AffectCount = i;
                result.ReturnValue = i;
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 权限控制对象
        /// </summary>
        public abstract IAuthority Authority { get; }

        private static Dictionary<string, Operate> OperateCollection = new Dictionary<string, Operate>();

        /// <summary>
        /// 创建DAL对象
        /// </summary>
        /// <param name="model">要创建DAL对应的实体对象</param>
        /// <param name="isCache">是否通过缓存中获取对象</param>
        /// <returns></returns>
        public static Operate CreateOperate(IModel model, bool isCache = true)
        {
            return CreateOperate(model.DalName, model.AssName, isCache);
        }

        /// <summary>
        /// 创建DAL对象
        /// </summary>
        /// <param name="dalName">DAL对象类名</param>
        /// <param name="assName">DAL所在程序集名</param>
        /// <param name="isCache">是否通过缓存中获取对象</param>
        /// <returns></returns>
        public static Operate CreateOperate(string dalName, string assName, bool isCache = true)
        {
            Operate operate = null;
            lock (OperateCollection)
            {
                if (isCache && OperateCollection.ContainsKey(dalName))
                    operate = OperateCollection[dalName];
                else
                {
                    operate = Assembly.Load(assName).CreateInstance(dalName, false) as Operate;

                    if (isCache)
                    {
                        lock (OperateCollection)
                        {
                            if (!OperateCollection.ContainsKey(dalName) && operate != null)
                                OperateCollection.Add(dalName, operate);
                        }
                    }
                }
            }

            return operate;
        }
    }
}
