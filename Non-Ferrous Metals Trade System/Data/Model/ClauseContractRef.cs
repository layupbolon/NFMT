
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ClauseContract_.cs
// 文件功能描述：模板条款关联表dbo.ClauseContract_Ref实体类。
// 创建人：CodeSmith
// 创建时间： 2014年7月8日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Data.Model
{
    /// <summary>
    /// 模板条款关联表dbo.ClauseContract_Ref实体类。
    /// </summary>
    [Serializable]
    public class ClauseContractRef : IModel
    {
        #region 字段

        private int refId;
        private int masterId;
        private int clauseId;
        private int sort;
        private bool isChose;
        private Common.StatusEnum refStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.ClauseContract_Ref";

        #endregion

        #region 构造函数

        public ClauseContractRef()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 关联序号序号
        /// </summary>
        public int RefId
        {
            get { return refId; }
            set { refId = value; }
        }

        /// <summary>
        /// 合约模板序号
        /// </summary>
        public int MasterId
        {
            get { return masterId; }
            set { masterId = value; }
        }

        /// <summary>
        /// 合约条款序号
        /// </summary>
        public int ClauseId
        {
            get { return clauseId; }
            set { clauseId = value; }
        }

        /// <summary>
        /// 排序号
        /// </summary>
        public int Sort
        {
            get { return sort; }
            set { sort = value; }
        }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsChose
        {
            get { return isChose; }
            set { isChose = value; }
        }

        /// <summary>
        /// 关联状态
        /// </summary>
        private Common.StatusEnum RefStatus
        {
            get { return this.refStatus; }
            set { this.refStatus = value; }
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
            get { return this.refId; }
            set { this.refId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return this.refStatus; }
            set { this.refStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        public string DalName { get; set; }

        public string AssName { get; set; }

        private string dataBaseName = "NFMT_Basic";
        public string DataBaseName
        {
            get { return this.dataBaseName; }
        }
        #endregion
    }
}