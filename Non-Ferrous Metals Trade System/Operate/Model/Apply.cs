
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：Apply.cs
// 文件功能描述：申请dbo.Apply实体类。
// 创建人：CodeSmith
// 创建时间： 2014年11月14日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Operate.Model
{
    /// <summary>
    /// 申请dbo.Apply实体类。
    /// </summary>
    [Serializable]
    public class Apply : IModel
    {
        #region 字段

        private int applyId;
        private string applyNo = String.Empty;
        private ApplyType applyType;
        private int empId;
        private DateTime applyTime;
        private Common.StatusEnum applyStatus;
        private int applyDept;
        private int applyCorp;
        private string applyDesc = String.Empty;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Apply";
        #endregion

        #region 构造函数

        public Apply()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 申请序号
        /// </summary>
        public int ApplyId
        {
            get { return applyId; }
            set { applyId = value; }
        }

        /// <summary>
        /// 申请编号
        /// </summary>
        public string ApplyNo
        {
            get { return applyNo; }
            set { applyNo = value; }
        }

        /// <summary>
        /// 申请类型
        /// </summary>
        public ApplyType ApplyType
        {
            get { return applyType; }
            set { applyType = value; }
        }

        /// <summary>
        /// 申请人
        /// </summary>
        public int EmpId
        {
            get { return empId; }
            set { empId = value; }
        }

        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime ApplyTime
        {
            get { return applyTime; }
            set { applyTime = value; }
        }

        /// <summary>
        /// 申请状态
        /// </summary>
        public Common.StatusEnum ApplyStatus
        {
            get { return applyStatus; }
            set { applyStatus = value; }
        }

        /// <summary>
        /// 申请部门
        /// </summary>
        public int ApplyDept
        {
            get { return applyDept; }
            set { applyDept = value; }
        }

        /// <summary>
        /// 申请公司
        /// </summary>
        public int ApplyCorp
        {
            get { return applyCorp; }
            set { applyCorp = value; }
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string ApplyDesc
        {
            get { return applyDesc; }
            set { applyDesc = value; }
        }

        /// <summary>
        /// 创建人序号
        /// </summary>
        public int CreatorId
        {
            get { return creatorId; }
            set { creatorId = value; }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }

        /// <summary>
        /// 最后修改人序号
        /// </summary>
        public int LastModifyId
        {
            get { return lastModifyId; }
            set { lastModifyId = value; }
        }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastModifyTime
        {
            get { return lastModifyTime; }
            set { lastModifyTime = value; }
        }

        /// <summary>
        /// 数据序号
        /// </summary>
        public int Id
        {
            get { return this.applyId; }
            set { this.applyId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return applyStatus; }
            set { applyStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.Operate.DAL.ApplyDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }

        private string assName = "NFMT.Operate";
        public string AssName
        {
            get { return this.assName; }
            set { this.assName = value; }
        }

        public string StatusName
        {
            get { return this.Status.ToString(); }
        }

        private string dataBaseName = "NFMT";
        public string DataBaseName
        {
            get { return this.dataBaseName; }
        }
        #endregion
    }
}