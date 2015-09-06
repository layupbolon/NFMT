/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StyleEnum.cs
// 文件功能描述：方式，类型等枚举。
// 创建人：pekah.chow
// 创建时间： 2014年6月27日
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Data
{
    public enum StyleEnum
    {
        PayMatter = 1,
        付款事项 = PayMatter,

        DPType = 2,
        交货地类型 = DPType,

        PAType = 3,
        付款类型 = PAType,

        PayMode = 4,
        付款方式 = PayMode,

        CustomType = 5,
        报关状态 = CustomType,

        StockType = 6,
        库存类型 = StockType,

        CorpType = 7,
        公司类型 = CorpType,

        LogDirection = 8,
        流水方向 = LogDirection,

        InvioceType = 9,
        发票类型 = InvioceType,

        InvoiceDirection = 10,
        发票方向 = InvoiceDirection,

        TradeBorder = 11,
        贸易境区 = TradeBorder,

        ContractLimit = 12,
        合同时限 = ContractLimit,

        ContractSide = 13,
        合同对方 = ContractSide,

        HaveGoodsFlow = 14,
        贸易融资 = HaveGoodsFlow,

        PriceMode = 15,
        定价方式 = PriceMode,

        MarginMode = 16,
        价格保证金方式 = MarginMode,

        TradeDirection = 17,
        贸易方向 = TradeDirection,

        WorkStatus = 18,
        在职状态 = WorkStatus,

        BusinessType = 19,
        业务类型 = BusinessType,

        SmsType = 20,
        消息类别 = SmsType,

        ApplyType = 21,
        申请类型 = ApplyType,

        ConditionType = 22,
        条件类型 = ConditionType,

        LogicType = 23,
        逻辑类型 = LogicType,

        NodeType = 24,
        节点类型 = NodeType,

        LogSourceType = 25,
        流水来源类型 = LogSourceType,

        DeptType = 26,
        部门类型 = DeptType,

        CapitalType = 27,
        资本类型 = CapitalType,

        AttachType = 28,
        附件类型 = AttachType,

        DoPriceType = 29,
        作价方式 = DoPriceType,

        DoPriceReason = 30,
        点价动因 = DoPriceReason,

        PledgeType = 31,
        质押状态 = PledgeType,

        BillType = 32,
        单据类型 = BillType,

        ContractPayment = 33,
        合约付款方式 = ContractPayment,

        LogType = 34,
        流水类型 = LogType,

        FlowDirection = 35,
        收付方向 = FlowDirection,

        FundsLog = 36,
        资金流水类型 = FundsLog,

        DisplayType = 37,
        合约条款显示类型 = DisplayType,

        ValueType = 38,
        值类型 = ValueType,

        ValueRateType = 39,
        费率类型 = ValueRateType,

        DiscountBase = 40,
        贴现基准 = DiscountBase,

        WhoDoPrice = 41,
        点价方 = WhoDoPrice,

        SummaryPrice = 42,
        计价方式 = SummaryPrice,        

        FundsType =43,
        财务类型 = FundsType,

        AllotFrom = 44,
        分配来源 = AllotFrom,

        FeeType = 45,
        发票内容=FeeType,

        ReceiptType = 46,
        回执类型 =ReceiptType,

        ReadStatus = 47,
        已读状态 = ReadStatus,

        //AttachType = 48,
        //附件类型 = AttachType,

        PricingDirection = 49,
        点价方向 = PricingDirection,

        CashInAllotTypeEnum = 50,
        收款分配类型 = CashInAllotTypeEnum,

        AuditEmpType = 51,
        审核人类型 = AuditEmpType,

        OperateType = 52,
        操作类型 = OperateType,

        OrderType = 53,
        制单指令类型 = OrderType,

        报表类型 = 54,
        创建来源 = 55,
        交货方式 = 56,
        付款分配来源 = 57,
        出入库类型 = 58,
        计息方式 =59,
        客户类型 = 60,
        计息类型 =61,
        合约类型 =62
    }
}
