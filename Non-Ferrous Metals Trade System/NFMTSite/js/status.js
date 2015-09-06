var sourceStatus = [
                { text: "全部", value: 0 },
                { text: "已作废", value: 1 },
                { text: "已关闭", value: 10 },
                { text: "已录入", value: 20 },
                { text: "已撤返", value: 25 },
                { text: "审核拒绝", value: 35 },
                { text: "待审核", value: 40 },
                { text: "已生效", value: 50 },
                { text: "已冻结", value: 60 },
                { text: "已完成", value: 80 }
];

var sourceNeedStatus = [
                { text: "全部", value: 0 },
                { text: "已作废", value: 1 },
                { text: "已关闭", value: 10 },
                { text: "已录入", value: 20 },
                { text: "已撤返", value: 25 },
                { text: "审核拒绝", value: 35 },
                { text: "待审核", value: 40 },
                { text: "已生效", value: 50 },
                { text: "已冻结", value: 60 },
                { text: "已完成", value: 80 }
];

var operateEnum = {
    查询: 260,
    录入: 261,
    修改: 262,
    提交审核: 263,
    撤返: 264,
    冻结: 265,
    解除冻结: 266,
    作废: 267,
    关闭: 268,
    执行完成: 269,
    确认完成撤销: 271,
    确认完成: 274,
    执行完成撤销: 275,
    交单: 281,
    银行承兑: 283,
    单据退回: 284
};

var statusEnum =
    {
        已作废: 1,
        已关闭: 10,
        已录入: 20,
        已撤返: 25,
        审核拒绝: 35,
        待审核: 40,
        已生效: 50,
        已完成: 80
    }

var FundsEnum =
    {
        ContractPayApply: 162,
        StockPayApply: 163,
        InvoicePayApply: 164
    }

var AttachTypeEnum = {
    ContractAttach: 228,
    StockInAttach: 229,
    StockLogAttach: 231,
    StockAttach: 232,
    StockOutAttach: 234,
    CashInAttach: 235,
    PaymentAttach: 236,
    InvoiceAttach: 238,
    SplitDocAttach: 245,
    CustomApplyAttach: 251,
    CustomAttach: 252,
    SubAttach: 253,
    OrderAttach: 279,
    PledgeAttach: 289,
    BusinessLiceneseAttach: 317,
    TaxAttach: 318,
    OrganizationAttach: 319,
    CertifyAttach: 320,
    BillAttach: 330,
    PurchaseDoubleContractAttach: 342,
    PurchaseContractAttach: 348,
    LibraryBillAttach: 349,
    WayBillAttach: 350,
    InvoiceScanningAttach: 351,
    PriceConfirmationAttach: 352,
    ContractStatementAttach: 353,
    CostBreakdownListAttach: 354
}

var AllotFromEnum =
{
    Receivable: 168,
    CorpReceivable: 169,
    ContractReceivable: 170
}

function CreateStatusDropDownList(id) {
    $("#" + id).jqxDropDownList({
        source: sourceStatus,
        selectedIndex: 0,
        height: 25,
        width: 100,
        displayMember: "text",
        valueMember: "value",
        autoDropDownHeight: true
    });
}

function CreateNeedStatusDropDownList(id) {
    $("#" + id).jqxDropDownList({
        source: sourceNeedStatus,
        selectedIndex: 0,
        height: 25,
        width: 100,
        displayMember: "text",
        valueMember: "value",
        autoDropDownHeight: true
    });
}

function CreateTyeDropDownList(id) {
    $("#" + id).jqxDropDownList({
        source: sourceNo,
        selectedIndex: 0,
        height: 25,
        width: 100,
        displayMember: "text",
        valueMember: "value",
        autoDropDownHeight: true
    });
}

function CreateSelectStatusDropDownList(id, selValue) {
    var obj = $("#" + id);
    obj.jqxDropDownList({
        source: sourceNeedStatus,
        selectedIndex: 0,
        height: 25,
        width: 100,
        displayMember: "text",
        valueMember: "value",
        autoDropDownHeight: true
    });

    obj.jqxDropDownList("val", selValue);
}

var CashInAllotEnum =
    {
        Corp: 246,
        Contract: 247,
        Stock: 248,
        Invoice: 249
    }

var orderTypeEnum =
    {
        临票制单指令: 277,
        终票制单指令: 278,
        无配货临票制单指令: 285,
        无配货终票制单指令: 287,
        替临制单指令: 288
    }

var MasterEnum = {
    信用证审核: 1,
    合约审核: 2,
    子合约审核: 3,
    入库登记审核: 4,
    质押申请审核: 5,
    出库申请审核: 6,
    质押审核: 7,
    出库审核: 8,
    收款登记审核: 9,
    合约收款分配: 10,
    合约付款申请: 11,
    库存收款分配: 12,
    库存付款申请: 13,
    公司收款分配: 14,
    财务付款关联合约审核: 15,
    财务付款关联库存审核: 16,
    财务发票审核: 17,
    回购申请审核: 18,
    回购审核: 19,
    直接终票审核: 20,
    价外票审核: 21,
    点价申请审核: 22,
    点价审核: 23,
    集团审核: 24,
    企业审核: 25,
    部门审核: 26,
    员工审核: 27,
    角色审核: 28,
    联系人审核: 29,
    财务发票分配审核: 30,
    止损申请审核: 31,
    止损审核: 32,
    拆单审核: 33,
    报关申请审核: 34,
    报关审核: 35,
    移库申请审核: 36,
    移库审核: 37,
    入库分配审核: 38,
    临票审核: 40,
    价外票付款申请审核: 41,
    库存净重回执审核: 42,
    制单指令审核: 43,
    制单审核: 44,
    付款申请: 47,
    采购主子合约审核: 48,
    客户审核: 49,
    价格确认单审核: 50,
    销售合约审核: 51,
    发票申请审核: 52,
    收款分配审核: 53,
    利息结算审核: 54,
    质押申请单审核: 55,
    赎回申请单审核: 56,
    预入库审核: 57,
    预入库转正式审核: 58
}
