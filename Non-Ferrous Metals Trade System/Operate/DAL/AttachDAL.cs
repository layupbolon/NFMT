/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：AttachDAL.cs
// 文件功能描述：附件dbo.Attach数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年6月30日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.Operate.Model;
using NFMT.DBUtility;
using NFMT.Operate.IDAL;
using NFMT.Common;

namespace NFMT.Operate.DAL
{
    /// <summary>
    /// 附件dbo.Attach数据交互类。
    /// </summary>
    public class AttachDAL : DataOperate, IAttachDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public AttachDAL()
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

        public override List<SqlParameter> CreateInsertParameters(IModel obj, ref SqlParameter returnValue)
        {
            Attach attach = (Attach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@AttachId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            if (!string.IsNullOrEmpty(attach.AttachName))
            {
                SqlParameter attachnamepara = new SqlParameter("@AttachName", SqlDbType.VarChar, 200);
                attachnamepara.Value = attach.AttachName;
                paras.Add(attachnamepara);
            }

            if (!string.IsNullOrEmpty(attach.ServerAttachName))
            {
                SqlParameter serverattachnamepara = new SqlParameter("@ServerAttachName", SqlDbType.VarChar, 200);
                serverattachnamepara.Value = attach.ServerAttachName;
                paras.Add(serverattachnamepara);
            }

            if (!string.IsNullOrEmpty(attach.AttachExt))
            {
                SqlParameter attachextpara = new SqlParameter("@AttachExt", SqlDbType.VarChar, 20);
                attachextpara.Value = attach.AttachExt;
                paras.Add(attachextpara);
            }

            SqlParameter attachtypepara = new SqlParameter("@AttachType", SqlDbType.Int, 4);
            attachtypepara.Value = attach.AttachType;
            paras.Add(attachtypepara);

            if (!string.IsNullOrEmpty(attach.AttachInfo))
            {
                SqlParameter attachinfopara = new SqlParameter("@AttachInfo", SqlDbType.VarChar, 4000);
                attachinfopara.Value = attach.AttachInfo;
                paras.Add(attachinfopara);
            }

            SqlParameter attachlengthpara = new SqlParameter("@AttachLength", SqlDbType.Int, 4);
            attachlengthpara.Value = attach.AttachLength;
            paras.Add(attachlengthpara);

            if (!string.IsNullOrEmpty(attach.AttachPath))
            {
                SqlParameter attachpathpara = new SqlParameter("@AttachPath", SqlDbType.VarChar, 400);
                attachpathpara.Value = attach.AttachPath;
                paras.Add(attachpathpara);
            }

            SqlParameter attachstatuspara = new SqlParameter("@AttachStatus", SqlDbType.Int, 4);
            attachstatuspara.Value = attach.AttachStatus;
            paras.Add(attachstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            Attach attach = new Attach();

            int indexAttachId = dr.GetOrdinal("AttachId");
            attach.AttachId = Convert.ToInt32(dr[indexAttachId]);

            int indexAttachName = dr.GetOrdinal("AttachName");
            if (dr["AttachName"] != DBNull.Value)
            {
                attach.AttachName = Convert.ToString(dr[indexAttachName]);
            }

            int indexServerAttachName = dr.GetOrdinal("ServerAttachName");
            if (dr["ServerAttachName"] != DBNull.Value)
            {
                attach.ServerAttachName = Convert.ToString(dr[indexServerAttachName]);
            }

            int indexAttachExt = dr.GetOrdinal("AttachExt");
            if (dr["AttachExt"] != DBNull.Value)
            {
                attach.AttachExt = Convert.ToString(dr[indexAttachExt]);
            }

            int indexAttachType = dr.GetOrdinal("AttachType");
            if (dr["AttachType"] != DBNull.Value)
            {
                attach.AttachType = Convert.ToInt32(dr[indexAttachType]);
            }

            int indexAttachInfo = dr.GetOrdinal("AttachInfo");
            if (dr["AttachInfo"] != DBNull.Value)
            {
                attach.AttachInfo = Convert.ToString(dr[indexAttachInfo]);
            }

            int indexAttachLength = dr.GetOrdinal("AttachLength");
            if (dr["AttachLength"] != DBNull.Value)
            {
                attach.AttachLength = Convert.ToInt32(dr[indexAttachLength]);
            }

            int indexAttachPath = dr.GetOrdinal("AttachPath");
            if (dr["AttachPath"] != DBNull.Value)
            {
                attach.AttachPath = Convert.ToString(dr[indexAttachPath]);
            }

            int indexAttachStatus = dr.GetOrdinal("AttachStatus");
            if (dr["AttachStatus"] != DBNull.Value)
            {
                attach.AttachStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexAttachStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                attach.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                attach.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                attach.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                attach.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return attach;
        }

        public override string TableName
        {
            get
            {
                return "Attach";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            Attach attach = (Attach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = attach.AttachId;
            paras.Add(attachidpara);

            if (!string.IsNullOrEmpty(attach.AttachName))
            {
                SqlParameter attachnamepara = new SqlParameter("@AttachName", SqlDbType.VarChar, 200);
                attachnamepara.Value = attach.AttachName;
                paras.Add(attachnamepara);
            }

            if (!string.IsNullOrEmpty(attach.ServerAttachName))
            {
                SqlParameter serverattachnamepara = new SqlParameter("@ServerAttachName", SqlDbType.VarChar, 200);
                serverattachnamepara.Value = attach.ServerAttachName;
                paras.Add(serverattachnamepara);
            }

            if (!string.IsNullOrEmpty(attach.AttachExt))
            {
                SqlParameter attachextpara = new SqlParameter("@AttachExt", SqlDbType.VarChar, 20);
                attachextpara.Value = attach.AttachExt;
                paras.Add(attachextpara);
            }

            SqlParameter attachtypepara = new SqlParameter("@AttachType", SqlDbType.Int, 4);
            attachtypepara.Value = attach.AttachType;
            paras.Add(attachtypepara);

            if (!string.IsNullOrEmpty(attach.AttachInfo))
            {
                SqlParameter attachinfopara = new SqlParameter("@AttachInfo", SqlDbType.VarChar, 4000);
                attachinfopara.Value = attach.AttachInfo;
                paras.Add(attachinfopara);
            }

            SqlParameter attachlengthpara = new SqlParameter("@AttachLength", SqlDbType.Int, 4);
            attachlengthpara.Value = attach.AttachLength;
            paras.Add(attachlengthpara);

            if (!string.IsNullOrEmpty(attach.AttachPath))
            {
                SqlParameter attachpathpara = new SqlParameter("@AttachPath", SqlDbType.VarChar, 400);
                attachpathpara.Value = attach.AttachPath;
                paras.Add(attachpathpara);
            }

            SqlParameter attachstatuspara = new SqlParameter("@AttachStatus", SqlDbType.Int, 4);
            attachstatuspara.Value = attach.AttachStatus;
            paras.Add(attachstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="user"></param>
        /// <param name="obj"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        public ResultModel UpLoadFile(UserModel user, Attach attach, System.IO.Stream stream)
        {
            ResultModel result = new ResultModel();

            try
            {
                if (attach == null)
                {
                    result.Message = "新增对象不能为null";
                    return result;
                }
                if (stream == null)
                {
                    result.Message = "空文件";
                    return result;
                }

                string fullName = string.Format("{0}{1}/{2}.{3}", Common.DefaultValue.FilePath, attach.AttachPath, attach.AttachName, attach.AttachType);

                int i = 0;
                while (System.IO.File.Exists(fullName))
                {
                    fullName = string.Format("{0}{1}/{2}_{3}.{4}", Common.DefaultValue.FilePath, attach.AttachPath, attach.AttachName, i, attach.AttachType);
                    i++;
                }

                byte[] data = new Byte[attach.AttachLength];
                stream.Read(data, 0, attach.AttachLength);

                System.IO.FileStream file = new System.IO.FileStream(fullName, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                file.Write(data, 0, data.Length);
                file.Close();

                result.ResultStatus = 0;
                result.Message = "文件上传成功";
                result.ReturnValue = null;
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel UpLoadFiles(UserModel user, Dictionary<Attach, System.IO.Stream> kvps)
        {
            ResultModel result = new ResultModel();

            foreach (KeyValuePair<Attach, System.IO.Stream> kvp in kvps)
            {
                Attach attach = kvp.Key;
                System.IO.Stream stream = kvp.Value;

                result = this.UpLoadFile(user, attach, stream);
            }

            return result;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="user">当前操作用户</param>
        /// <param name="obj">附件对象</param>
        /// <param name="inputFile">System.Web.UI.HtmlControls.HtmlInputFile 对象</param>
        /// <returns></returns>
        public ResultModel UpLoadFile(UserModel user, Attach attach, System.Web.UI.HtmlControls.HtmlInputFile inputFile)
        {
            ResultModel result = new ResultModel();

            if (attach == null)
            {
                result.Message = "新增对象不能为null";
                return result;
            }
            if (inputFile == null || inputFile.PostedFile == null || inputFile.PostedFile.ContentLength == 0)
            {
                result.Message = "空文件";
                return result;
            }

            attach.AttachLength = inputFile.PostedFile.ContentLength;
            attach.AttachExt = System.IO.Path.GetExtension(inputFile.PostedFile.FileName);
            attach.AttachName = System.IO.Path.GetFileNameWithoutExtension(inputFile.PostedFile.FileName);

            return this.UpLoadFile(user, attach, inputFile.PostedFile.InputStream);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="user"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public ResultModel DownLoadFile(UserModel user, Attach attach)
        {
            ResultModel result = new ResultModel();

            if (attach == null)
            {
                result.Message = "下载附件对象不能为空";
                return result;
            }

            string fullName = string.Format("{0}{1}/{2}.{3}", Common.DefaultValue.FilePath, attach.AttachPath, attach.AttachName, attach.AttachType);
            System.IO.FileInfo file = new System.IO.FileInfo(fullName);

            if (file == null || file.Length == 0)
            {
                result.Message = "文件不存在";
                return result;
            }

            result.ResultStatus = 0;
            result.Message = "加载文件成功";
            result.ReturnValue = file;

            return result;
        }

        public ResultModel DownLoadFiles(UserModel user, List<Model.Attach> attachs)
        {
            ResultModel result = new ResultModel();

            if (attachs == null || attachs.Count == 0)
            {
                result.Message = "下载附件对象集合不能为空";
                return result;
            }

            List<System.IO.FileInfo> files = new List<System.IO.FileInfo>();

            foreach (Model.Attach attach in attachs)
            {
                string fullName = string.Format("{0}{1}/{2}.{3}", Common.DefaultValue.FilePath, attach.AttachPath, attach.AttachName, attach.AttachType);
                System.IO.FileInfo file = new System.IO.FileInfo(fullName);

                if (file != null && file.Length > 0)
                    files.Add(file);
            }

            result.ResultStatus = 0;
            result.Message = "加载文件列表成功";
            result.ReturnValue = files;

            return result;
        }

        /// <summary>
        /// 删除附件==该附件表数据作废
        /// </summary>
        /// <param name="user"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public ResultModel DeleteFile(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();
            return result;
        }

        public ResultModel UpdateAttachStatus(UserModel user, int attachId, int status)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update dbo.Attach set AttachStatus = {0} where AttachId = {1}", status, attachId);
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);
                if (i > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "更新成功";
                    result.AffectCount = i;
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "更新失败";
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }

            return result;
        }

        public ResultModel Load(UserModel user, string attachIds, int status = (int)Common.StatusEnum.已生效)
        {
            ResultModel result = new ResultModel();

            if (string.IsNullOrEmpty(attachIds))
            {
                result.Message = "附件序号错误";
                result.ResultStatus = -1;
                return result;
            }

            SqlDataReader dr = null;

            try
            {
                string sql = string.Format("select * from dbo.Attach where AttachId in ({0}) and AttachStatus = {1}", attachIds, status);
                dr = SqlHelper.ExecuteReader(ConnectString, CommandType.Text, sql, null);

                if (!dr.HasRows)
                {
                    result.ResultStatus = -1;
                    result.Message = "读取失败或无数据";
                    result.AffectCount = 0;
                    return result;
                }

                List<IModel> models = new List<IModel>();

                while (dr.Read())
                {
                    models.Add(CreateModel(dr));
                }

                result.AffectCount = dr.RecordsAffected;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = models;
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

        public ResultModel GetAttachByAttachIds(UserModel user, string aids)
        {
            if (!string.IsNullOrEmpty(aids))
            {
                string cmdText = string.Format("select * from NFMT.dbo.Attach where AttachId in ({0})", aids);
                return Load<NFMT.Operate.Model.Attach>(user, CommandType.Text, cmdText);
            }
            else
                return new ResultModel() { Message = "无附件", ResultStatus = 0 };
        }

        #endregion
    }
}
