
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SI.cs
// 文件功能描述：价外票dbo.Inv_SI实体类。
// 创建人：CodeSmith
// 创建时间： 2014年9月1日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Invoice.Model
{
    /// <summary>
    /// 价外票dbo.Inv_SI实体类。
    /// </summary>
    [Serializable]
    public class SI : IModel
    {
        #region 字段

        private int sIId;
        private int invoiceId;
        private int changeCurrencyId;
        private decimal changeRate;
        private decimal changeBala;
        private int payDept;
        private string tableName = "dbo.Inv_SI";
        private Common.StatusEnum status = StatusEnum.已录入;
        #endregion

        #region 构造函数

        public SI()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 价外票序号
        /// </summary>
        public int SIId
        {
            get { return sIId; }
            set { sIId = value; }
        }

        /// <summary>
        /// 发票序号
        /// </summary>
        public int InvoiceId
        {
            get { return invoiceId; }
            set { invoiceId = value; }
        }

        /// <summary>
        /// 折算币种
        /// </summary>
        public int ChangeCurrencyId
        {
            get { return changeCurrencyId; }
            set { changeCurrencyId = value; }
        }

        /// <summary>
        /// 汇率
        /// </summary>
        public decimal ChangeRate
        {
            get { return changeRate; }
            set { changeRate = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal ChangeBala
        {
            get { return changeBala; }
            set { changeBala = value; }
        }

        /// <summary>
        /// 成本部门
        /// </summary>
        public int PayDept
        {
            get { return payDept; }
            set { payDept = value; }
        }

        public int CreatorId { get; set; }
        public DateTime CreateTime { get; set; }
        public int LastModifyId { get; set; }
        public DateTime LastModifyTime { get; set; }

        /// <summary>
        /// 数据序号
        /// </summary>
        public int Id
        {
            get { return this.sIId; }
            set { this.sIId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return status; }
            set { status = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.Invoice.DAL.SIDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }

        private string assName = "NFMT.Invoice";
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