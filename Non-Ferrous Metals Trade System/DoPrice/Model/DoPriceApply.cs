
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：DoPriceApply.cs
// 文件功能描述：点价申请dbo.DoPriceApply实体类。
// 创建人：CodeSmith
// 创建时间： 2014年6月9日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.DoPrice.Model
{
    /// <summary>
    /// 点价申请dbo.DoPriceApply实体类。
    /// </summary>
    [Serializable]
    public class DoPriceApply : IModel
    {
        #region 字段

        private int doPriceApplyId;
        private int applyId;
        private int applyDeptId;
        private int subContractId;
        private int contractId;
        private int doPriceDirection;
        private DateTime startTime;
        private DateTime endTime;
        private decimal minPrice;
        private decimal maxPrice;
        private int currencyId;
        private int doPriceBlocId;
        private int doPriceCorpId;
        private decimal doPriceWeight;
        private int mUId;
        private int assertId;
        private int doPricePersoinId;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.DoPriceApply";

        #endregion

        #region 构造函数

        public DoPriceApply()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 
        /// </summary>
        public int DoPriceApplyId
        {
            get { return doPriceApplyId; }
            set { doPriceApplyId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ApplyId
        {
            get { return applyId; }
            set { applyId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ApplyDeptId
        {
            get { return applyDeptId; }
            set { applyDeptId = value; }
        }

        public string ApplyDeptName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SubContractId
        {
            get { return subContractId; }
            set { subContractId = value; }
        }

        public string SubContractNo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ContractId
        {
            get { return contractId; }
            set { contractId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int DoPriceDirection
        {
            get { return doPriceDirection; }
            set { doPriceDirection = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal MinPrice
        {
            get { return minPrice; }
            set { minPrice = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal MaxPrice
        {
            get { return maxPrice; }
            set { maxPrice = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int CurrencyId
        {
            get { return currencyId; }
            set { currencyId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int DoPriceBlocId
        {
            get { return doPriceBlocId; }
            set { doPriceBlocId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int DoPriceCorpId
        {
            get { return doPriceCorpId; }
            set { doPriceCorpId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal DoPriceWeight
        {
            get { return doPriceWeight; }
            set { doPriceWeight = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int MUId
        {
            get { return mUId; }
            set { mUId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int AssertId
        {
            get { return assertId; }
            set { assertId = value; }
        }

        public string AssetName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int DoPricePersoinId
        {
            get { return doPricePersoinId; }
            set { doPricePersoinId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int CreatorId
        {
            get { return creatorId; }
            set { creatorId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int LastModifyId
        {
            get { return lastModifyId; }
            set { lastModifyId = value; }
        }

        /// <summary>
        /// 
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
            get { return this.doPriceApplyId; }
            set { this.doPriceApplyId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get;
            set;
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

        private string dataBaseName = "";
        public string DataBaseName
        {
            get { return this.dataBaseName; }
        }
        #endregion
    }
}