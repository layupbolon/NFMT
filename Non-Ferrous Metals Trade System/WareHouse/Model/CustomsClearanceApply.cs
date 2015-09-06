
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CustomsClearanceApply.cs
// 文件功能描述：报关申请dbo.St_CustomsClearanceApply实体类。
// 创建人：CodeSmith
// 创建时间： 2014年10月29日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.WareHouse.Model
{
    /// <summary>
    /// 报关申请dbo.St_CustomsClearanceApply实体类。
    /// </summary>
    [Serializable]
    public class CustomsClearanceApply : IModel
    {
        #region 字段

        private int customsApplyId;
        private int applyId;
        private int assetId;
        private decimal grossWeight;
        private decimal netWeight;
        private int unitId;
        private int outCorpId;
        private int inCorpId;
        private int customsCorpId;
        private decimal customsPrice;
        private int currencyId;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.St_CustomsClearanceApply";
        #endregion

        #region 构造函数

        public CustomsClearanceApply()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 报关申请序号
        /// </summary>
        public int CustomsApplyId
        {
            get { return customsApplyId; }
            set { customsApplyId = value; }
        }

        /// <summary>
        /// 主申请序号
        /// </summary>
        public int ApplyId
        {
            get { return applyId; }
            set { applyId = value; }
        }

        /// <summary>
        /// 品种
        /// </summary>
        public int AssetId
        {
            get { return assetId; }
            set { assetId = value; }
        }

        /// <summary>
        /// 申请总毛重
        /// </summary>
        public decimal GrossWeight
        {
            get { return grossWeight; }
            set { grossWeight = value; }
        }

        /// <summary>
        /// 申请总净重
        /// </summary>
        public decimal NetWeight
        {
            get { return netWeight; }
            set { netWeight = value; }
        }

        /// <summary>
        /// 重量单位
        /// </summary>
        public int UnitId
        {
            get { return unitId; }
            set { unitId = value; }
        }

        /// <summary>
        /// 关外公司
        /// </summary>
        public int OutCorpId
        {
            get { return outCorpId; }
            set { outCorpId = value; }
        }

        /// <summary>
        /// 关内公司
        /// </summary>
        public int InCorpId
        {
            get { return inCorpId; }
            set { inCorpId = value; }
        }

        /// <summary>
        /// 报关公司
        /// </summary>
        public int CustomsCorpId
        {
            get { return customsCorpId; }
            set { customsCorpId = value; }
        }

        /// <summary>
        /// 报关单价
        /// </summary>
        public decimal CustomsPrice
        {
            get { return customsPrice; }
            set { customsPrice = value; }
        }

        /// <summary>
        /// 币种
        /// </summary>
        public int CurrencyId
        {
            get { return currencyId; }
            set { currencyId = value; }
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
            get { return this.customsApplyId; }
            set { this.customsApplyId = value; }
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

        private string dalName = "NFMT.WareHouse.DAL.CustomsClearanceApplyDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }

        private string assName = "NFMT.WareHouse";
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